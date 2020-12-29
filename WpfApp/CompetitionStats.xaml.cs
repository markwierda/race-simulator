using Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;

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
