using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements.ActivityElements
{
    public class CaloriesGraph
    {
        [ParameterId(3)]
        public ClockHand ClockHand { get; set; }
    }
}