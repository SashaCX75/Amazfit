using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NLog;
using Resources.Models;
using WatchFace.Parser.Models;
using WatchFace.Parser;
using Header = WatchFace.Parser.Models.Header;
using System;

namespace WatchFace.Parser
{
    public static class ColorType
    {
        public static int colorType = 1;
    }

    public class Writer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly List<IResource> _images;

        private readonly Stream _stream;

        public Writer(Stream stream, List<IResource> images, int colorType)
        {
            _stream = stream;
            _images = images;
            ColorType.colorType = colorType;
        }

        public void Write(IList<Parameter> descriptor)
        {
            Logger.Trace("Encoding parameters...");
            var encodedParameters = new Dictionary<byte, MemoryStream>(descriptor.Count);
            foreach (var parameter in descriptor)
            {
                Logger.Trace("Parameter: {0}", parameter.Id);
                var memoryStream = new MemoryStream();
                foreach (var child in parameter.Children)
                    child.Write(memoryStream);
                encodedParameters[parameter.Id] = memoryStream;
            }

            Logger.Trace("Encoding offsets and lengths...");
            var parametersPositions = new List<Parameter>(descriptor.Count + 1);
            var offset = (long) 0;
            foreach (var encodedParameter in encodedParameters)
            {
                var encodedParameterId = encodedParameter.Key;
                var encodedParameterLength = (Int16)encodedParameter.Value.Length;
                parametersPositions.Add(new Parameter(encodedParameterId, new List<Parameter>
                {
                    new Parameter(1, offset),
                    new Parameter(2, encodedParameterLength)
                }));
                offset += encodedParameterLength;
            }
            parametersPositions.Insert(0, new Parameter(1, new List<Parameter>
            {
                new Parameter(1, offset),
                new Parameter(2, _images.Count)
            }));

            var encodedParametersPositions = new MemoryStream();
            foreach (var parametersPosition in parametersPositions)
                parametersPosition.Write(encodedParametersPositions);

            Logger.Trace("Writing header...");
            //byte[] modelByte = new byte[8] { 0x28, 0x00, 0x8c, 0xea, 0x00, 0x00, 0x01, 0xbc };
            byte[] modelByte = Model.modelByte;
            //int m = (int)offset % 4;
            var header = new Header
            {
                ParametersSize = (uint) encodedParametersPositions.Length,
                //Unknown = (uint)(offset - 8),
                Unknown = (uint)(offset),
                //Unknown = 0x159, // Maybe some kind of layers (the bigger number needed for more complex watch faces)
                UnknownWrite = modelByte
            };
            header.WriteTo(_stream);

            Logger.Trace("Writing parameters offsets and lengths...");
            encodedParametersPositions.Seek(0, SeekOrigin.Begin);
            encodedParametersPositions.WriteTo(_stream);

            Logger.Trace("Writing parameters...");
            foreach (var encodedParameter in encodedParameters)
            {
                var stream = encodedParameter.Value;
                stream.Seek(0, SeekOrigin.Begin);
                stream.WriteTo(_stream);
            }

            //while (m > 0)
            //{
            //    _stream.WriteByte(0xff);
            //    m--;
            //}
            Logger.Trace("Writing images...");
            new Resources.Writer(_stream).Write(_images, ColorType.colorType);
        }
    }
}