using SUPlannerLibraries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SUPlanner
{
    /// <summary>
    /// Interaction logic for SoupisSpisu.xaml
    /// </summary>
    public partial class SoupisSpisu : Window           // TODO - Wire up Odebrat Button
    {
        ISelectedSpisRequest selectedSpisRequest;
        public SoupisSpisu(ISelectedSpisRequest caller)
        {
            InitializeComponent();
            selectedSpisRequest = caller;
            SetDefaultDate();
            WireUpSoupisSpisu();
        }

        private void SetDefaultDate()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            podkladDatePicker.SelectedDate = DateTime.Today;
        }

            private void WireUpSoupisSpisu()
        {
            List<PodkladModel> podklady = GlobalConfig.podkladFile.FullFilePath().LoadFileAll ().ConvertToPodkladModels();
            List<PodkladModel> soupisSpisu = new();
            int spisId = selectedSpisRequest.SelectedSpis();
            foreach (PodkladModel podklad in podklady)
            {
                
                if (podklad.SpisId == spisId)
                {
                    soupisSpisu.Add(podklad);
                }
            }
            podkladyDataGrid.ItemsSource = soupisSpisu;
        }

        private void odebratSoupisSpisuButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void pridatSoupisSpisuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                PodkladModel podklad = new();
                podklad.SpisId = selectedSpisRequest.SelectedSpis();
                podklad.Podklad = podkladTextBox.Text;
                podklad.DatumPridani = (DateTime)podkladDatePicker.SelectedDate;
                GlobalConfig.Connection.CreatePodklad(podklad);
                WireUpSoupisSpisu();
            }
            
            
        }

        private bool Validate()
        {
            bool isValid = true;

            if (String.IsNullOrWhiteSpace(podkladTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Podklad.");
            }

            if (!DateTime.TryParse(podkladDatePicker.Text, out DateTime testLimit))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Datum Přidání.");
            }

            return isValid;

        }
    }
}
