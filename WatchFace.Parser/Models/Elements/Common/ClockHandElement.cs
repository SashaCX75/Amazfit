using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using WatchFace.Parser.Interfaces;

namespace WatchFace.Parser.Models.Elements.Common
{
    public abstract class ClockHandElement : CompositeElement, IDrawable
    {
        protected ClockHandElement(Parameter parameter, Element parent, string name = null) :
            base(parameter, parent, name) { }

        public bool OnlyBorder { get; set; }
        public Color color { get; set; }
        public CoordinatesElement CenterOffset { get; set; }
        public List<CoordinatesElement> Shape { get; set; } = new List<CoordinatesElement>();
        public ImageElement clockHand { get; set; }

        public abstract void Draw(Graphics drawer, Bitmap[] resources, WatchState state);

        public void Draw(Graphics drawer, Bitmap[] resources, double value, double total)
        {
            var angle = value * 360 / total;
            var points = Shape.Select(point => RotatePoint(point, angle)).ToArray();
            if (points.Count() > 0)
            {
                if (OnlyBorder)
                {
                    drawer.DrawPolygon(new Pen(color), points);
                }
                else
                {
                    drawer.FillPolygon(new SolidBrush(color), points, FillMode.Alternate);
                    //drawer.DrawPolygon(new Pen(color, 1), points);
                }
            }
            //clockHand?.Draw(drawer, resources);
            clockHand?.DrawClockHand(drawer, resources, (float)angle, 
                new Point((int)CenterOffset.X, (int)CenterOffset.Y));
        }

        private Point RotatePoint(CoordinatesElement element, double degrees)
        {
            var radians = degrees / 180 * Math.PI;
            var x = element.X * Math.Cos(radians) + element.Y * Math.Sin(radians);
            var y = element.X * Math.Sin(radians) - element.Y * Math.Cos(radians);
            return new Point((int) Math.Floor(x + CenterOffset.X), (int) Math.Floor(y + CenterOffset.Y));
        }

        protected override Element CreateChildForParameter(Parameter parameter)
        {
            switch (parameter.Id)
            {
                case 1:
                    OnlyBorder = parameter.Value > 0;
                    return new ValueElement(parameter, this);
                case 2:
                    color = Color.FromArgb(0xff, Color.FromArgb((int) parameter.Value));
                    return new ValueElement(parameter, this);
                case 3:
                    CenterOffset = new CoordinatesElement(parameter, this);
                    return CenterOffset;
                case 4:
                    var point = new CoordinatesElement(parameter, this);
                    Shape.Add(point);
                    return point;
                case 5:
                    clockHand = new ImageElement(parameter, this);
                    return clockHand;
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}