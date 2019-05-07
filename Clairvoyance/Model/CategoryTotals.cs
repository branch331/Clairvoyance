using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Clairvoyance.Model
{
    public class CategoryTotals : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string category;
        private double totalHours;

        public CategoryTotals(string categoryIn, double totalHoursIn)
        {
            Category = categoryIn;
            TotalHours = totalHoursIn;
        }
        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    NotifyPropertyChanged("Category");
                }
            }
        }

        public double TotalHours
        {
            get { return totalHours; }
            set
            {
                if (totalHours != value)
                {
                    totalHours = value;
                    NotifyPropertyChanged("TotalHours");
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
