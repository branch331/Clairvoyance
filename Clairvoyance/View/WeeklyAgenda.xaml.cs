using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Clairvoyance.ViewModel;

namespace Clairvoyance.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WeeklyAgenda : Window
    {
        private WeeklyAgendaViewModel weeklyAgendaVM = new WeeklyAgendaViewModel();

        public WeeklyAgenda()
        {
            InitializeComponent();
            this.DataContext = weeklyAgendaVM;
        }

        private void Detailed_Button_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.Show();
        }
    }
}
