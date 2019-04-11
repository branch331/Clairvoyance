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

namespace Clairvoyance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WeeklyAgendaViewModel weeklyAgendaVM = new WeeklyAgendaViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = weeklyAgendaVM;
        }

        private void Submit_Task_Click(object sender, RoutedEventArgs e)
        {
            weeklyAgendaVM.addTaskToDay("Mon");
            System.Windows.MessageBox.Show(
                weeklyAgendaVM.DaysToDisplay[0].TaskList[0].TaskName + "\n" +
                weeklyAgendaVM.DaysToDisplay[0].TaskList[0].TaskCategory + "\n" +
                weeklyAgendaVM.DaysToDisplay[0].TaskList[0].TaskDescription + "\n" +
                weeklyAgendaVM.DaysToDisplay[0].TaskList[0].TaskStartDateTime + "\n" +
                weeklyAgendaVM.DaysToDisplay[0].TaskList[0].TaskEndDateTime + "\n" +
                weeklyAgendaVM.DaysToDisplay[0].TaskList[0].TaskTimeInterval);
        }
    }
}
