using System.Collections.Generic;
using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.AnimationElements;

namespace WatchFace.Parser.Elements
{
    public class Animation
    {
        [ParameterId(1)]
        public List<MotiomAnimation> MotiomAnimation { get; set; }

        [ParameterId(2)]
        public StaticAnimation StaticAnimation { get; set; }
    }
}