using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.ActivityElements;
using WatchFace.Parser.Elements.BasicElements;

namespace WatchFace.Parser.Elements
{
    public class Activity
    {
        //[ParameterId(1)]
        //public Number Steps { get; set; }

        //[ParameterId(2)]
        //public Number StepsGoal { get; set; }

        //[ParameterId(3)]
        //public Number Calories { get; set; }

        //[ParameterId(4)]
        //public Number Pulse { get; set; }

        //[ParameterId(5)]
        //public FormattedNumber Distance { get; set; }

        [ParameterId(1)]
        public CircleScale StepsGoal { get; set; }

        [ParameterId(2)]
        public Number Calories { get; set; }

        [ParameterId(3)]
        public Number Pulse { get; set; }

        [ParameterId(4)]
        public Distance Distance { get; set; }

        [ParameterId(5)]
        public FormattedNumber Steps { get; set; }

        [ParameterId(7)]
        public Image StarImage { get; set; }

        [ParameterId(8)]
        public Image CaloriesIcon { get; set; } // нет данных 

        [ParameterId(9)]
        public Image CircleRange { get; set; } // нет данных

        [ParameterId(11)]
        public CircleScale Goal2 { get; set; } // нет данных

        [ParameterId(12)]
        public IconSet ColouredSquares { get; set; } // нет данных

        [ParameterId(13)]
        public long NoDataImageIndex { get; set; } // нет данных

        //[ParameterId(14)]
        //public IconSet StepsSquaresUnknown { get; set; } // нет данных

        [ParameterId(15)]
        public long? CaloriesTextualIcon { get; set; } // нет данных

        [ParameterId(17)]
        public CaloriesGraph CaloriesGraph { get; set; } // нет данных

        [ParameterId(18)]
        public PulseGraph PulseGraph { get; set; } // нет данных

        [ParameterId(19)]
        public Number Unknown1 { get; set; } //PAI

        [ParameterId(21)]
        public Number PAI { get; set; } //PAI
    }
}