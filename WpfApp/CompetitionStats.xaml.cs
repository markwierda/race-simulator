using Controller;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CompetitionStats.xaml
    /// </summary>
    public partial class CompetitionStats : Window
    {
        public CompetitionStats()
        {
            InitializeComponent();
            BestPoints.ItemsSource = Data.Competition.ParticipantPoints.GetParticipantsOrderedByBest();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BestPoints.ItemsSource = Data.Competition.ParticipantPoints.GetParticipantsOrderedByBest();
        }
    }
}