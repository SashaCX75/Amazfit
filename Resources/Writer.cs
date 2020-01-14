using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NLog;
using Resources.Models;

namespace Resources
{
    public static class ColorType
    {
        public static int colorType = 1;
    }

    public class Writer
    {
        private const int OffsetTableItemLength = 4;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Stream _stream;

        public Writer(Stream stream)
        {
            _stream = stream;
        }

        public void Write(List<IResource> resources, int colorType)
        {
            ColorType.colorType = colorType;
            var offsetsTable = new byte[resources.Count * OffsetTableItemLength];
            var encodedResources = new MemoryStream[resources.Count];

            var offset = (uint) 0;
            for (var i = 0; i < resources.Count; i++)
            {
                Logger.Trace("Resource {0} offset is {1}...", i, offset);
                var offsetBytes = BitConverter.GetBytes(offset);
                offsetBytes.CopyTo(offsetsTable, i * OffsetTableItemLength);

                var encodedImage = new MemoryStream();
                Logger.Debug("Encoding resource {0}...", i);
                resources[i].WriteTo(encodedImage);
                offset += (uint) encodedImage.Length;
                encodedResources[i] = encodedImage;
            }

            //_stream.WriteByte(3);
            //_stream.WriteByte(3);
            //_stream.WriteByte(3);
            long s = _stream.Position % 4;
            s = 4 - s;
            for (int i = 0; i < offsetsTable.Length; i=i+4)
            {
                offsetsTable[i] = (byte)(offsetsTable[i] + s);
            }
            Logger.Trace("Writing resources offsets table");
            _stream.Write(offsetsTable, 0, offsetsTable.Length);

            while (s > 0)
            {
                _stream.WriteByte(0xff);
                s--;
            }

            //_stream.WriteByte(3);
            //_stream.WriteByte(3);
            //_stream.WriteByte(3);
            Logger.Debug("Writing resources");
            foreach (var encodedImage in encodedResources)
            {
                encodedImage.Seek(0, SeekOrigin.Begin);
                encodedImage.CopyTo(_stream);
            }
        }
    }
}