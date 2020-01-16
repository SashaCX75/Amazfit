using System.Collections.Generic;
using System.Drawing;
using WatchFace.Parser.Interfaces;
using WatchFace.Parser.Models;

namespace WatchFace.Parser
{
    public static class CenterOffset
    {
        public static int X = 227;
        public static int Y = 227;
    }
    public static class Model
    {
        public static byte[] modelByte = new byte[8] { 0x28, 0x00, 0x8c, 0xea, 0x00, 0x00, 0x01, 0xbc }; // gtr 47
    }

    public class PreviewGenerator
    {

        public static IEnumerable<Image> CreateAnimation(List<Parameter> descriptor, Bitmap[] images,
            IEnumerable<WatchState> states, int CenterOffsetX, int CenterOffsetY, byte[] modelByte)
        {
            CenterOffset.X = CenterOffsetX;
            CenterOffset.Y = CenterOffsetY;
            Model.modelByte = modelByte;

            var previewWatchFace = new Models.Elements.WatchFace(descriptor);
            foreach (var watchState in states)
            {
                using (var image = CreateFrame(previewWatchFace, images, watchState))
                {
                    yield return image;
                }
            }
        }

        public static Image CreateImage(IEnumerable<Parameter> descriptor, Bitmap[] images, WatchState state, 
            int CenterOffsetX, int CenterOffsetY)
        {
            CenterOffset.X = CenterOffsetX;
            CenterOffset.Y = CenterOffsetY;
            var previewWatchFace = new Models.Elements.WatchFace(descriptor);
            return CreateFrame(previewWatchFace, images, state);
        }

        private static Image CreateFrame(IDrawable watchFace, Bitmap[] resources, WatchState state)
        {
            var preview = new Bitmap(CenterOffset.X *2, CenterOffset.Y *2);
            var graphics = Graphics.FromImage(preview);
            watchFace.Draw(graphics, resources, state);
            return preview;
        }
    }
}