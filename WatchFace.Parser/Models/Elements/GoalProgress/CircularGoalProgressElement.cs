using System.Drawing;

namespace WatchFace.Parser.Models.Elements.GoalProgress
{
    public class CircularGoalProgressElement : CircularProgressElement
    {
        public CircularGoalProgressElement(Parameter parameter, Element parent, string name = null) :
            base(parameter, parent, name) { }

        public override void Draw(Graphics drawer, Bitmap[] resources, WatchState state)
        {
            int steps = state.Steps;
            if (steps > state.Goal) steps = state.Goal;
            Draw(drawer, resources, steps, state.Goal);
        }
    }
}