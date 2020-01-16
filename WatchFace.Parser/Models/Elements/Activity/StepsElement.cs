using System.Collections.Generic;
using System.Drawing;
using WatchFace.Parser.Helpers;
using WatchFace.Parser.Interfaces;

namespace WatchFace.Parser.Models.Elements
{
    public class StepsElement : CompositeElement, IDrawable
    {
        public StepsElement(Parameter parameter, Element parent, string name = null) :
            base(parameter, parent, name) { }

        public NumberElement Number { get; set; }
        public long SuffixMilesImageIndex { get; set; }
        public long DecimalPointImageIndex { get; set; }

        public void Draw(Graphics drawer, Bitmap[] resources, WatchState state)
        {
            //var steps = 0;
            //var decimals = 0;
            //if (state.Steps >= 10000)
            //{
            //    steps = state.Steps / 10000;
            //    decimals = state.Steps - steps * 10000;
            //}
            //else if (state.Steps >= 1000)
            //{
            //    steps = state.Steps / 1000;
            //    decimals = state.Steps - steps * 1000;
            //}
            //else if (state.Steps >= 100)
            //{
            //    steps = state.Steps / 100;
            //    decimals = state.Steps - steps * 100;
            //}
            //else if (state.Steps >= 10)
            //{
            //    steps = state.Steps / 10;
            //    decimals = state.Steps - state.Steps * 10;
            //}

            //var images = Number.GetImagesForNumber(resources, steps);
            var images = new List<Bitmap>();
            //images.Add(resources[DecimalPointImageIndex]);
            images.AddRange(Number.GetImagesForNumber(resources, state.Steps));
            if (SuffixMilesImageIndex>0) images.Add(resources[SuffixMilesImageIndex]);

            DrawerHelper.DrawImages(drawer, images, (int)Number.Spacing, Number.Alignment, Number.GetBox());
        }

        protected override Element CreateChildForParameter(Parameter parameter)
        {
            switch (parameter.Id)
            {
                case 1:
                    Number = new NumberElement(parameter, this, nameof(Number));
                    return Number;
                case 2:
                    SuffixMilesImageIndex = parameter.Value;
                    return new ValueElement(parameter, this, nameof(SuffixMilesImageIndex));
                case 3:
                    DecimalPointImageIndex = parameter.Value;
                    return new ValueElement(parameter, this, nameof(DecimalPointImageIndex));
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}