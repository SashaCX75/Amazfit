using WatchFace.Parser.Attributes;

namespace WatchFace.Parser.Elements.TimeElements
{
    public class Pm
    {
        [ParameterId(1)]
        public long X { get; set; }

        [ParameterId(2)]
        public long Y { get; set; }
    }
}