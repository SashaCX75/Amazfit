using WatchFace.Parser.Attributes;

namespace WatchFace.Parser.Elements.BasicElements
{
    public class Sector
    {
        [ParameterId(1)]
        public long StartAngle { get; set; }

        [ParameterId(2)]
        public long EndAngle { get; set; }
    }
}