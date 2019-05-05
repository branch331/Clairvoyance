using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Clairvoyance.ViewModel
{
    class StatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string testString;

        public StatisticsViewModel()
        {
            testString = "hey";
        }

        public string TestString
        {
            get { return testString; }
            set
            {
                if (value != null)
                {
                    testString = value;
                    NotifyPropertyChanged("TestString");
                }
            }
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
