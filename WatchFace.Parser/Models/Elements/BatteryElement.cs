using WatchFace.Parser.Models.Elements.Battery;

namespace WatchFace.Parser.Models.Elements
{
    public class BatteryElement : ContainerElement
    {
        public BatteryElement(Parameter parameter, Element parent = null, string name = null) :
            base(parameter, parent, name) { }

        public BatteryNumberElement Text { get; set; }
        public BatteryImageSetElement Icon { get; set; }
        public BatteryLinearProgressElement Linear { get; set; }
        public BettaryPersentElement BettaryPersent { get; set; }
        public BatteryCircularProgressElement Circular { get; set; }

        protected override Element CreateChildForParameter(Parameter parameter)
        {
            switch (parameter.Id)
            {
                case 1:
                    Text = new BatteryNumberElement(parameter, this);
                    return Text;
                case 2:
                    Icon = new BatteryImageSetElement(parameter, this);
                    return Icon;
                case 3:
                    Linear = new BatteryLinearProgressElement(parameter, this);
                    return Linear;
                case 6:
                    BettaryPersent = new BettaryPersentElement(parameter, this);
                    return BettaryPersent;
                case 7:
                    Circular = new BatteryCircularProgressElement(parameter, this);
                    return Circular;
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}