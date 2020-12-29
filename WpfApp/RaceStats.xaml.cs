using Controller;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CompetitionStats.xaml
    /// </summary>
    public partial class RaceStats : Window
    {
        public RaceStats()
        {
            InitializeComponent();
            BestPoints.ItemsSource = Data.Competition.ParticipantPoints.GetParticipantsOrderedByBest();
            BestTimeBroken.ItemsSource = Data.Competition.ParticipantTimeBroken.GetParticipantsOrderedByBest();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BestPoints.ItemsSource = Data.Competition.ParticipantPoints.GetParticipantsOrderedByBest();
            BestTimeBroken.ItemsSource = Data.Competition.ParticipantTimeBroken.GetParticipantsOrderedByBest();
        }
    }
}