using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements
{
    public class StepsProgress
    {
        [ParameterId(1)]
        public Image GoalImage { get; set; }

        [ParameterId(2)]
        public IconSet Sliced { get; set; }

        [ParameterId(3)]
        public CircleScale Circle { get; set; }

        [ParameterId(5)]
        public ClockHand ClockHand { get; set; }
    }
}