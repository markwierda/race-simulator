using Controller;
using Model;
using System;
using System.Windows;
using System.Windows.Threading;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() 
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
            Data.CurrentRace.StartNextRace += OnStartNextRace;
        }

        private void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            Track.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    Track.Source = null;
                    Track.Source = Visualize.DrawTrack(e.Track);
                })
            );
        }

        private void OnRaceFinished(object sender, RaceFinishedEventArgs e)
        {
            Data.Competition.AwardPoints(e.FinishedParticipants);
        }

        private void OnStartNextRace(object sender, EventArgs e)
        {
            Images.Clear();

            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceFinished -= OnRaceFinished;
            Data.CurrentRace.StartNextRace -= OnStartNextRace;

            Data.NextRace();

            if (Data.CurrentRace != null)
            {
                Data.CurrentRace.DriversChanged += OnDriversChanged;
                Data.CurrentRace.RaceFinished += OnRaceFinished;
                Data.CurrentRace.StartNextRace += OnStartNextRace;

                Track.Dispatcher.BeginInvoke(
                    DispatcherPriority.Render,
                    new Action(() =>
                    {
                        Track.Source = null;
                        Track.Source = Visualize.DrawTrack(Data.CurrentRace.Track);
                    })
                );
            }
        }
    }
}
