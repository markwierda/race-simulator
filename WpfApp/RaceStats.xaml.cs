using Controller;
using System;
using System.ComponentModel;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for CompetitionStats.xaml
    /// </summary>
    public partial class RaceStats : Window
    {
        private string _currentTrack;

        public RaceStats()
        {
            InitializeComponent();
            _currentTrack = Data.CurrentRace.Track.Name;
            CurrentTrack.Content = _currentTrack;

            Data.CurrentRace.StartNextRace += OnNextRace;

            Data.Competition.ParticipantTimeBroken.PropertyChanged += OnParticipantTimeBrokenChanged;
            TimeBrokenList.ItemsSource = Data.Competition.ParticipantTimeBroken.GetParticipantsOrderedByBest(_currentTrack);

            Data.Competition.ParticipantPerformanceBeforeAndAfter.PropertyChanged += OnParticipantPerformanceBeforeAndAfterChanged;
            PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.ParticipantPerformanceBeforeAndAfter.GetParticipantsOrderedByBest(_currentTrack);
        }

        private void OnNextRace(object sender, EventArgs e)
        {
            if (Data.CurrentRace != null)
            {
                _currentTrack = Data.CurrentRace.Track.Name;

                Dispatcher.Invoke(() =>
                {
                    CurrentTrack.Content = _currentTrack;
                    TimeBrokenList.ItemsSource = Data.Competition.ParticipantTimeBroken.GetParticipantsOrderedByBest(_currentTrack);
                    PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.ParticipantPerformanceBeforeAndAfter.GetParticipantsOrderedByBest(_currentTrack);
                });
            }
        }

        private void OnParticipantTimeBrokenChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                TimeBrokenList.ItemsSource = Data.Competition.ParticipantTimeBroken.GetParticipantsOrderedByBest(_currentTrack);
            });
        }

        private void OnParticipantPerformanceBeforeAndAfterChanged(object sender, PropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                PerformanceBeforeAndAfterList.ItemsSource = Data.Competition.ParticipantPerformanceBeforeAndAfter.GetParticipantsOrderedByBest(_currentTrack);
            });
        }
    }
}