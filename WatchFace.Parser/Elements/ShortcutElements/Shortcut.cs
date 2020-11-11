using WatchFace.Parser.Attributes;

namespace WatchFace.Parser.Elements.ShortcutElements
{
    public class Shortcut
    {
        [ParameterId(1)]
        public Rectangle Element { get; set; }
    }
}