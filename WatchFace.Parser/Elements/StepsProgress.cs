using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements
{
    public class StepsProgress
    {
        [ParameterId(1)]
        public ImageSet Images1 { get; set; }

        [ParameterId(2)]
        public IconSet Sliced { get; set; }

        [ParameterId(3)]
        public CircleScale Circle { get; set; }

        [ParameterId(4)]
        public ImageSet Images4 { get; set; }

        [ParameterId(5)]
        public CircleScale Unknown5 { get; set; }

        [ParameterId(6)]
        public ClockHand ClockHand { get; set; }
    }
}