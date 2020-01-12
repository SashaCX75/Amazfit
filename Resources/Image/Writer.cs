using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BumpKit;
using NLog;
using Resources.Utils;

namespace Resources.Image
{

    public class Writer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly byte[] Signature = { (byte)'B', (byte)'M', 255, 255 };
        //private static readonly byte[] Signature = { (byte)'B', (byte)'M', (byte)'d', 0 };
        private readonly List<Color> _palette;

        private readonly BinaryWriter _writer;

        private int _bitsPerPixel;
        private int _height;
        private Bitmap _image;
        private int _paletteColors;
        private int _rowLengthInBytes;
        private int _transparency;
        private int _width;

        public Writer(Stream stream)
        {
            _writer = new BinaryWriter(stream);
            _palette = new List<Color>();
        }

        public void Write(Bitmap image)
        {
            _image = image;
            if (image.PixelFormat == PixelFormat.Format32bppArgb) _transparency = 1;
            _width = (ushort) image.Width;
            _height = (ushort) image.Height;

            //ExtractPalette();

            if (_bitsPerPixel == 3) _bitsPerPixel = 4;
            if (_bitsPerPixel == 0) _bitsPerPixel = 1;

            if (_bitsPerPixel > 4)
                throw new ArgumentException(
                    $"The image has {_bitsPerPixel} bit/pixel and can't be packed for using on the watches. Looks like dithering works incorrectly on the image."
                );

            _bitsPerPixel = 4;
            _rowLengthInBytes = (ushort) Math.Ceiling((decimal)(_width * _bitsPerPixel));

            _writer.Write(Signature);

            long pos = _writer.BaseStream.Position;
            WriteHeader();
            //WritePalette();
            //int colorType = ColorType.colorType;
            if (ColorType.colorType == 1) WriteImage1();
            else WriteImage2();

            //_writer.BaseStream.Position = pos;
            //using (BinaryReader _reader = new BinaryReader(_writer.BaseStream))
            //{
            //    byte[] b1 = _reader.ReadBytes(50);
            //}
        }

        private void ExtractPalette()
        {
            Logger.Trace("Extracting palette...");

            using (var context = _image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                for (var x = 0; x < _width; x++)
                {
                    var color = context.GetPixel(x, y);
                    if (_palette.Contains(color)) continue;

                    if (color.A < 0x80 && _transparency == 0)
                    {
                        Logger.Trace("Palette item {0}: R {1:X2}, G {2:X2}, B {3:X2}, Transaparent color",
                            _palette.Count, color.R, color.G, color.B
                        );
                        _palette.Insert(0, color);
                        _transparency = 1;
                    }
                    else
                    {
                        Logger.Trace("Palette item {0}: R {1:X2}, G {2:X2}, B {3:X2}",
                            _palette.Count, color.R, color.G, color.B
                        );
                        _palette.Add(color);
                    }
                }
            }

            var startIndex = _transparency == 0 ? 0 : 1;

            for (var i = startIndex; i < _palette.Count - 1; i++)
            {
                var minColor = (uint) _palette[i].ToArgb();
                var minIndex = i;
                for (var j = i + 1; j < _palette.Count; j++)
                {
                    var color = (uint) _palette[j].ToArgb();
                    if (color >= minColor) continue;

                    minColor = color;
                    minIndex = j;
                }

                if (minIndex == i) continue;

                var tmp = _palette[i];
                _palette[i] = _palette[minIndex];
                _palette[minIndex] = tmp;
            }

            _paletteColors = (ushort) _palette.Count;
            _bitsPerPixel = (ushort) Math.Ceiling(Math.Log(_paletteColors, 2));
        }

        private void WriteHeader()
        {
            Logger.Trace("Writing image header...");
            Logger.Trace("Width: {0}, Height: {1}, RowLength: {2}", _width, _height, _rowLengthInBytes);
            Logger.Trace("BPP: {0}, PaletteColors: {1}, Transaparency: {2}",
                _bitsPerPixel, _paletteColors, _transparency
            );

            //long pos = _writer.BaseStream.Position;
            _writer.Write(_width);
            _writer.Write(_height);
            _writer.Write(8*_rowLengthInBytes / _width);
            if (ColorType.colorType == 1) _writer.Write(24);
            else _writer.Write(32);
            //_writer.Write(32);
            //_writer.Write(8 * _bitsPerPixel);
            //_writer.Write(_paletteColors);
            _writer.Write(_transparency);
            // создаем объект BinaryReader
            //_writer.BaseStream.Position = pos;
            //using (BinaryReader _reader = new BinaryReader(_writer.BaseStream))
            //{
            //     byte[] b1 = _reader.ReadBytes(50);
            //}

            
        }

        private void WritePalette()
        {
            Logger.Trace("Writing palette...");
            foreach (var color in _palette)
            {
                _writer.Write(color.R);
                _writer.Write(color.G);
                _writer.Write(color.B);
                _writer.Write((byte) 0); // always 0 maybe padding
            }
        }

        private void WriteImage()
        {
            Logger.Trace("Writing image...");

            var paletteHash = new Dictionary<Color, byte>();
            byte i = 0;
            foreach (var color in _palette)
            {
                paletteHash[color] = i;
                i++;
            }

            using (var context = _image.CreateUnsafeContext())
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowData = new byte[_rowLengthInBytes];
                    var memoryStream = new MemoryStream(rowData);
                    var bitWriter = new BitWriter(memoryStream);
                    for (var x = 0; x < _width; x++)
                    {
                        var color = context.GetPixel(x, y);
                        if (color.A < 0x80 && _transparency == 1)
                        {
                            bitWriter.WriteBits(0, _bitsPerPixel);
                        }
                        else
                        {
                            var paletteIndex = paletteHash[color];
                            bitWriter.WriteBits(paletteIndex, _bitsPerPixel);
                        }
                    }

                    bitWriter.Flush();
                    _writer.Write(rowData);
                }
            }
        }

        private void WriteImage1()
        {
            Logger.Trace("Writing image...");
            
            using (var context = _image.CreateUnsafeContext())
            //using (var context = _image)
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowData = new byte[_rowLengthInBytes];
                    var memoryStream = new MemoryStream(rowData);
                    var bitWriter = new BitWriter(memoryStream);
                    for (var x = 0; x < _width; x++)
                    {
                        try
                        {
                            var color = context.GetPixel(x, y);
                            //bitWriter.Write(color.G);
                            //bitWriter.Write(color.R);
                            //bitWriter.Write(color.A);
                            //bitWriter.Write(color.B);

                            bitWriter.Write(color.B);
                            bitWriter.Write(color.G);
                            bitWriter.Write(color.R);
                            bitWriter.Write(color.A);

                            //_writer.Write(color.B);
                            //_writer.Write(color.G);
                            //_writer.Write(color.R);
                            //_writer.Write(color.A);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    bitWriter.Flush();
                    _writer.Write(rowData);
                }
            }
        }
        private void WriteImage2()
        {
            Logger.Trace("Writing image...");

            using (var context = _image.CreateUnsafeContext())
            //using (var context = _image)
            {
                for (var y = 0; y < _height; y++)
                {
                    var rowData = new byte[_rowLengthInBytes];
                    var memoryStream = new MemoryStream(rowData);
                    var bitWriter = new BitWriter(memoryStream);
                    for (var x = 0; x < _width; x++)
                    {
                        try
                        {
                            var color = context.GetPixel(x, y);
                            bitWriter.Write(color.G);
                            bitWriter.Write(color.R);
                            bitWriter.Write(color.A);
                            bitWriter.Write(color.B);

                            //bitWriter.Write(color.B);
                            //bitWriter.Write(color.G);
                            //bitWriter.Write(color.R);
                            //bitWriter.Write(color.A);

                            //_writer.Write(color.B);
                            //_writer.Write(color.G);
                            //_writer.Write(color.R);
                            //_writer.Write(color.A);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    bitWriter.Flush();
                    _writer.Write(rowData);
                }
            }
        }
    }
}