using WatchFace.Parser.Models.Elements.GoalProgress;

namespace WatchFace.Parser.Models.Elements
{
    public class ActivityElement : ContainerElement
    {
        public ActivityElement(Parameter parameter, Element parent = null, string name = null) :
            base(parameter, parent, name) { }

        public StepsElement Steps { get; set; }
        //public StepsGoalElement StepsGoal { get; set; }
        public CircularGoalProgressElement StepsGoal { get; set; }
        public CaloriesElement Calories { get; set; }
        public PulseElement Pulse { get; set; }
        public DistanceElement Distance { get; set; }
        public GoalReachedElement StarImage { get; set; }

        protected override Element CreateChildForParameter(Parameter parameter)
        {
            //switch (parameter.Id)
            //{
            //    case 1:
            //        Steps = new StepsElement(parameter, this);
            //        return Steps;
            //    case 2:
            //        StepsGoal = new StepsGoalElement(parameter, this);
            //        return StepsGoal;
            //    case 3:
            //        Calories = new CaloriesElement(parameter, this);
            //        return Calories;
            //    case 4:
            //        Pulse = new PulseElement(parameter, this);
            //        return Pulse;
            //    case 5:
            //        Distance = new DistanceElement(parameter, this);
            //        return Distance;
            //    default:
            //        return base.CreateChildForParameter(parameter);
            //}
            switch (parameter.Id)
            {
                case 1:
                    StepsGoal = new CircularGoalProgressElement(parameter, this);
                    return StepsGoal;
                case 2:
                    Calories = new CaloriesElement(parameter, this);
                    return Calories;
                case 3:
                    Pulse = new PulseElement(parameter, this);
                    return Pulse;
                case 4:
                    Distance = new DistanceElement(parameter, this);
                    return Distance;
                case 5:
                    Steps = new StepsElement(parameter, this);
                    return Steps;
                case 7:
                    StarImage = new GoalReachedElement(parameter, this);
                    return StarImage;
                default:
                    return base.CreateChildForParameter(parameter);
            }
        }
    }
}