using Controller;
using System.ComponentModel;
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

            StartTime.Content = $"StartTime: {Data.CurrentRace.StartTime}";

            Data.Competition.ParticipantPoints.PropertyChanged += OnParticipantPointsChanged;
            PointsList.ItemsSource = Data.Competition.ParticipantPoints.GetParticipantsOrderedByBest(null);
        }

        private void OnParticipantPointsChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                PointsList.ItemsSource = Data.Competition.ParticipantPoints.GetParticipantsOrderedByBest(null);
            });
        }
    }
}