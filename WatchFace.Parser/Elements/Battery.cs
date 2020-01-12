using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;
using WatchFace.Parser.Elements.AnalogDialFaceElements;

namespace WatchFace.Parser.Elements
{
    public class Battery
    {
        [ParameterId(1)]
        public Number Text { get; set; }

        [ParameterId(2)]
        public ImageSet Images { get; set; }

        [ParameterId(3)]
        public IconSet Icons { get; set; }

        [ParameterId(4)]
        public ClockHand ClockHand { get; set; }

        [ParameterId(5)]
        public long? Unknown5 { get; set; }

        [ParameterId(6)]
        public Image Percent { get; set; }

        [ParameterId(7)]
        public CircleScale Circle { get; set; }
    }
}