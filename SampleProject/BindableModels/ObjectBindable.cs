using SampleProject.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.BindableModels
{
    public class ObjectBindable : ModelBaseBindable
    {
        private string fullName;
        private string vipLevelCode;
        private string levelCode;
        private StatusLevel statusType;
        private string classOfTravel;
        private List<string> points = new List<string>();
        private string point;
        private Random rd = new Random();

        public ObjectBindable()
        {
            points.Add("ABC");
            points.Add("DEF");
            points.Add("GHI");
            points.Add("JKL");
            points.Add("MNO");
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged("FullName");
            }
        }

        public string LevelCode
        {
            get { return levelCode; }
            set
            {
                levelCode = value;
                OnPropertyChanged("LevelCode");
            }
        }

        public string VIPLevelCode
        {
            get { return vipLevelCode; }
            set
            {
                vipLevelCode = value;
                OnPropertyChanged("VIPLevelCode");
            }
        }

        public StatusLevel StatusType
        {
            get { return statusType; }
            set
            {
                statusType = value;
                OnPropertyChanged("StatusType");
            }
        }

        public string ClassOfTravel
        {
            get { return classOfTravel; }
            set
            {
                classOfTravel = value;
                OnPropertyChanged("ClassOfTravel");
            }
        }

        public List<string> Points
        {
            get { return points; }
            set
            {
                points = value;
                OnPropertyChanged("Points");
            }
        }

        public string Point
        {
            get
            {
                return Points[rd.Next(0, 5)];
            }
        }
    }
}
