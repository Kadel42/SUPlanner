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
    public partial class SoupisSpisu : Window        
    {
        ISelectedSpisRequest selectedSpisRequest;
        private int indexOfUnusedNumber = 0;
        public SoupisSpisu(ISelectedSpisRequest caller)
        {
            InitializeComponent();
            selectedSpisRequest = caller;
            SetDefaultDate();
            SetDefaultCislo();
            WireUpSoupisSpisu();
        }

        private void SetDefaultCislo()
        {
            cisloPodkladTextBox.Text = SetRollerCisla()[0].ToString();
        }

        private List<int> SetRollerCisla()
        {
            List<PodkladModel> podklady = GlobalConfig.podkladFile.FullFilePath().LoadFileAll().ConvertToPodkladModels();
            List<int> cisla = new();
            List<int> unusedNumbers = new();
            int cislo = 1;
            List<PodkladModel> soupisSpisu = new();
            int spisId = selectedSpisRequest.SelectedSpis().Id;
            foreach (PodkladModel podklad in podklady)
            {

                if (podklad.SpisId == spisId)
                {
                    soupisSpisu.Add(podklad);
                }
            }
            foreach (PodkladModel podklad in soupisSpisu)
            {
                cisla.Add(podklad.Cislo);
            }
            int limit = 100;
            int i = 0;
            while (i < limit)
            {
                if (cisla.Contains(cislo))
                {
                    cislo++;
                    limit++;
                    i++;
                }
                else
                {
                    unusedNumbers.Add(cislo);
                    cislo++;
                    i++;
                }

                
            }
            return unusedNumbers;
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
            int spisId = selectedSpisRequest.SelectedSpis().Id;
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
            List<PodkladModel> podklady = GlobalConfig.podkladFile.FullFilePath().LoadFileAll().ConvertToPodkladModels();
            PodkladModel podkladModel = (PodkladModel)podkladyDataGrid.SelectedItem;

            if (!(podkladModel == null))
            {
                podklady.RemovePodkladFromFile(podkladModel.Id);
                WireUpSoupisSpisu();
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný prvek.");
            }

            
        }

        private void pridatSoupisSpisuButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                PodkladModel podklad = new();
                podklad.Cislo = SetRollerCisla()[0];
                podklad.SpisId = selectedSpisRequest.SelectedSpis().Id;
                podklad.Podklad = podkladTextBox.Text.Trim();
                podklad.DatumPridani = (DateTime)podkladDatePicker.SelectedDate;
                GlobalConfig.Connection.CreatePodklad(podklad);
                WireUpSoupisSpisu();
                SetDefaultCislo();
                podkladTextBox.Text = "";
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

        private void upPodkladCisloButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> indexes = SetRollerCisla();
            if (indexOfUnusedNumber < indexes.Count - 1)
            {
                indexOfUnusedNumber++;
                cisloPodkladTextBox.Text = indexes[indexOfUnusedNumber].ToString();
            }
        }

        private void downPodkladCisloButton_Click(object sender, RoutedEventArgs e)
        {
            if (indexOfUnusedNumber > 0)
            {
                indexOfUnusedNumber--;
                cisloPodkladTextBox.Text = SetRollerCisla()[indexOfUnusedNumber].ToString();
            }
        }
    }
}
