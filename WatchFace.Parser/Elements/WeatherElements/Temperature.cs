using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;

namespace WatchFace.Parser.Elements.WeatherElements
{
    public class Temperature
    {
        //[ParameterId(1)]
        //public TemperatureNumber Current { get; set; }

        [ParameterId(1)]
        public Number Current { get; set; }

        [ParameterId(2)]
        public TodayTemperature Today { get; set; }

        [ParameterId(3)]
        public TemperatureSymbols Symbols { get; set; }
    }
}