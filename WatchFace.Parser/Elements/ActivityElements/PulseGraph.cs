using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements.ActivityElements
{
    public class PulseGraph
    {
        [ParameterId(2)]
        public ClockHand ClockHand { get; set; }
    }
}