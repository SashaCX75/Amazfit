using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;
using Newtonsoft.Json;
using WatchFace.Parser.JsonConverters;

namespace WatchFace.Parser.Elements.ActivityElements
{
    public class Distance
    {
        [ParameterId(1)]
        public Number Number { get; set; }

        [ParameterId(2)]
        [ParameterImageIndex]
        public long? SuffixImageIndex { get; set; }

        [ParameterId(3)]
        [ParameterImageIndex]
        public long? DecimalPointImageIndex { get; set; }

        [JsonConverter(typeof(ColorJsonConverter))]
        [ParameterId(4)]
        public long Color { get; set; }
    }
}