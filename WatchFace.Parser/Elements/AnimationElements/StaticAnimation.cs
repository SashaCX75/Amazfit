using System.Collections.Generic;
using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;

namespace WatchFace.Parser.Elements.AnimationElements
{
    public class StaticAnimation
    {
        [ParameterId(1)]
        public AnimationImage Image { get; set; }

        [ParameterId(2)]
        public long SpeedAnimation { get; set; }

        [ParameterId(3)]
        public long Unknown2 { get; set; }

        [ParameterId(4)]
        public long TimeAnimation { get; set; }

        [ParameterId(5)]
        public long Pause { get; set; }
    }
}