using WatchFace.Parser.Attributes;

namespace WatchFace.Parser.Elements.ShortcutElements
{
    public class Rectangle
    {
        [ParameterId(1)]
        public long TopLeftX { get; set; }

        [ParameterId(2)]
        public long TopLeftY { get; set; }

        [ParameterId(3)]
        public long Width { get; set; }

        [ParameterId(4)]
        public long Height { get; set; }
    }
}