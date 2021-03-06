﻿using Controller;
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
        private RaceStats RaceStats;
        private CompetitionStats CompetitionStats;

        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            SubscribeEvents();
        }

        private void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            Display(e.Track);
        }

        private void OnRaceFinished(object sender, RaceFinishedEventArgs e)
        {
            Data.Competition.AwardPoints(e.FinishedParticipants);
        }

        private void OnStartNextRace(object sender, EventArgs e)
        {
            UnsubscribeEvents();
            Images.Clear();
            Data.NextRace();

            if (Data.CurrentRace != null)
            {
                SubscribeEvents();
                Display(Data.CurrentRace.Track);
            }
        }

        private void Display(Track track)
        {
            Track.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    Track.Source = null;
                    Track.Source = Visualize.DrawTrack(track);
                })
            );
        }

        private void SubscribeEvents()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.RaceFinished += OnRaceFinished;
            Data.CurrentRace.StartNextRace += OnStartNextRace;

            Dispatcher.Invoke(() =>
            {
                Data.CurrentRace.DriversChanged += ((DataContext)this.DataContext).OnDriversChanged;
                Data.CurrentRace.DriversChanged += ((DataContext)this.DataContext).OnDriversChanged;
            });
        }

        private void UnsubscribeEvents()
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
            Data.CurrentRace.RaceFinished -= OnRaceFinished;
            Data.CurrentRace.StartNextRace -= OnStartNextRace;
        }

        private void MenuItem_RaceStats_Click(object sender, RoutedEventArgs e)
        {
            RaceStats = new RaceStats();
            RaceStats.Show();
        }

        private void MenuItem_CompetionStats_Click(object sender, RoutedEventArgs e)
        {
            CompetitionStats = new CompetitionStats();
            CompetitionStats.Show();
        }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}