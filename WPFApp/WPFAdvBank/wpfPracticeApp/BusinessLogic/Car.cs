using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace wpfPracticeApp.BusinessLogic
{
    public class Car : Notifier
    {
        private double speed;
        public double Speed 
        {
            get { return speed; }
            set 
            { 
                if(value > 350)
                {
                    speed = 350;
                }else
                {
                    speed = value; //속성값 변경된 것을 알려줌(프로그램에서)
                }
                OnPropertyChanged("Speed");
            } 
        } //property(속성)은 대문자
        //public double Speed { get; set; } 동일

        private Color mainColor;

        public Color MainColor 
        {
            get { return mainColor; }
            set
            {
                mainColor = value;
                OnPropertyChanged("MainColor");
            }
        } 
        public Human Driver { get; set; }
    }

    public class Human
    {
        public string Name { get; set; }

        public bool HasDriveLicense { get; set; }
    }
}
