using System;
using System.Collections;
using System.IO;
using System.Text;

namespace WatchFace.Parser.Models
{
    public class Header
    {
        //public const int HeaderSize = 40;
        public const int HeaderSize = 64;
        //public const int parametersSizePos = 32;
        public const int parametersSizePos = 56;
        private const string DialSignature = "HMDIAL\0";

        public string Signature { get; private set; } = DialSignature;
        public uint Unknown { get; set; }
        public byte[] UnknownWrite { get; set; }
        public uint ParametersSize { get; set; } = parametersSizePos;

        public bool IsValid => Signature == DialSignature;

        public void WriteTo(Stream stream)
        {
            var buffer = new byte[HeaderSize];
            for (var i = 0; i < buffer.Length; i++) buffer[i] = 0xff;

            Encoding.ASCII.GetBytes(Signature).CopyTo(buffer, 0);
            byte[] myB1 = new byte[1];
            myB1[0] = 0x06;
            myB1.CopyTo(buffer, 11);
            UnknownWrite.CopyTo(buffer, 16);
            //BitConverter.GetBytes(0x06).CopyTo(buffer, 11);
            BitConverter.GetBytes(Unknown).CopyTo(buffer, 52);
            BitConverter.GetBytes(ParametersSize).CopyTo(buffer, 56);
            BitConverter.GetBytes(HeaderSize).CopyTo(buffer, 60);
            stream.Write(buffer, 0, HeaderSize);
        }

        public static Header ReadFrom(Stream stream)
        {
            var buffer = new byte[HeaderSize];
            stream.Read(buffer, 0, HeaderSize);

            return new Header
            {
                Signature = Encoding.ASCII.GetString(buffer, 0, 7),
                Unknown = BitConverter.ToUInt32(buffer, 52),
                ParametersSize = BitConverter.ToUInt32(buffer, 56)
            };
        }
    }
}