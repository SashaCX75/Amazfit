using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;

namespace WatchFace.Parser.Elements.AnimationElements
{
    public class MotiomAnimation
    {
        [ParameterId(1)]
        public long Unknown1 { get; set; }

        [ParameterId(2)]
        //[ParameterImagesCount]
        public Coordinates StartCoordinates { get; set; }

        [ParameterId(3)]
        //[ParameterImagesCount]
        public Coordinates EndCoordinates { get; set; }

        [ParameterId(4)]
        [ParameterImageIndex]
        public long ImageIndex { get; set; }

        [ParameterId(5)]
        public long SpeedAnimation { get; set; }

        [ParameterId(6)]
        public long TimeAnimation { get; set; }

        [ParameterId(7)]
        public long Unknown5 { get; set; }

        [ParameterId(8)]
        public long Unknown6 { get; set; }

        [ParameterId(9)]
        public long Unknown7 { get; set; }

        [ParameterId(10)]
        public bool Bounce { get; set; }
    }
}