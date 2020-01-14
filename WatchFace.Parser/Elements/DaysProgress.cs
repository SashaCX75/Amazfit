using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements
{
    public class DaysProgress
    {
        [ParameterId(1)]
        public ClockHand AnalogMonth { get; set; }

        [ParameterId(2)]
        public ClockHand UnknownField2 { get; set; }

        [ParameterId(3)]
        public ClockHand AnalogDOW { get; set; }
    }
}