using System.Collections.Generic;
using WatchFace.Parser.Attributes;

namespace WatchFace.Parser.Elements.BasicElements
{
    public class IconSet
    {
        [ParameterId(1)]
        [ParameterImageIndex]
        public long ImageIndex { get; set; }

        [ParameterId(2)]
        [ParameterImagesCount]
        public List<Coordinates> Coordinates { get; set; }
    }
}