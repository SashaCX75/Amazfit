using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.ShortcutElements;

namespace WatchFace.Parser.Elements
{
    public class Shortcuts
    {
        [ParameterId(1)]
        public Shortcut Steps { get; set; }

        [ParameterId(2)]
        public Shortcut Pulse { get; set; }

        [ParameterId(3)]
        public Shortcut Weather { get; set; }

        [ParameterId(4)]
        public Shortcut Unknown1 { get; set; }

        [ParameterId(5)]
        public Shortcut Unknown2 { get; set; }

        [ParameterId(6)]
        public Shortcut Unknown3 { get; set; }

        [ParameterId(7)]
        public Shortcut Unknown4 { get; set; }
    }
}