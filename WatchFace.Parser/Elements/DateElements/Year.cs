using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;

namespace WatchFace.Parser.Elements.DateElements
{
    public class Year
    {
        [ParameterId(2)]
        public OneLineYear OneLine { get; set; }
    }
}