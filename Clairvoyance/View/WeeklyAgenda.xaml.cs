using System.Windows;
using Clairvoyance.ViewModel;
using MahApps.Metro.Controls;

namespace Clairvoyance.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WeeklyAgenda : MetroWindow
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
