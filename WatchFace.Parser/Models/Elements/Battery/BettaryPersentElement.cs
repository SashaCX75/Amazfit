using System.Drawing;

namespace WatchFace.Parser.Models.Elements.Battery
{
    public class BettaryPersentElement : ImageElement
    {
        public BettaryPersentElement(Parameter parameter, Element parent, string name = null) :
            base(parameter, parent, name)
        { }

        public override void Draw(Graphics drawer, Bitmap[] images, WatchState state)
        {
            Draw(drawer, images);
        }
    }
}