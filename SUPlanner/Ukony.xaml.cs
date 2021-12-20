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
    /// Interaction logic for Ukony.xaml
    /// </summary>
    public partial class Ukony : Window
    {
        ISelectedSpisRequest selectedSpisRequest;

        public Ukony(ISelectedSpisRequest caller)
        {
            InitializeComponent();
            selectedSpisRequest = caller;
            WireUpUkony();
            SetDefaultDate();
        }

        private void WireUpUkony()
        {
            List<UkonModel> ukony = GlobalConfig.ukonFile.FullFilePath().LoadFileAll().ConvertToUkonModels();
            List<UkonModel> selectedSpisUkony = new();
            int spisId = selectedSpisRequest.SelectedSpis().Id;

            foreach (UkonModel ukon in ukony)
            {
                if (ukon.SpisId == spisId)
                {
                    selectedSpisUkony.Add(ukon);
                }
            }

            ukonyDataGrid.ItemsSource = selectedSpisUkony;

            
        }

        private void SetDefaultDate()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            datumUkonuDatePicker.SelectedDate = DateTime.Today;
        }

        private void pridatUkonButton_Click(object sender, RoutedEventArgs e)
        {

            if (Validate())
            {
                UkonModel ukon = new();
                ukon.SpisId = selectedSpisRequest.SelectedSpis().Id;
                ukon.CisloJednaci = cisloJednaciTextBox.Text.Trim();
                ukon.Typ = typUkonuTextBox.Text.Trim();
                ukon.Vydani = (DateTime)datumUkonuDatePicker.SelectedDate;
                GlobalConfig.Connection.CreateUkon(ukon);

                MessageBoxResult result = MessageBox.Show(
                    "Uložit do statistiky?", "Statistika", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes
                    );
                switch (result)
                {
                    
                    case MessageBoxResult.Yes:
                        StatistikaModel statistika = new();
                        statistika.CisloJednaci = cisloJednaciTextBox.Text.Trim();
                        statistika.Typ = typUkonuTextBox.Text.Trim();
                        statistika.DatumVydani = (DateTime)datumUkonuDatePicker.SelectedDate;
                        statistika.SpisZn = selectedSpisRequest.SelectedSpis().SpisZn;
                        statistika.Vec = selectedSpisRequest.SelectedSpis().Vec;
                        statistika.Zadatel = selectedSpisRequest.SelectedSpis().Zadatel;
                        GlobalConfig.Connection.CreateStatistika(statistika);
                        break;
                    case MessageBoxResult.No:
                        break;
                    
                }
                

                WireUpUkony();
                cisloJednaciTextBox.Text = "";
                typUkonuTextBox.Text = "";
            }
            


        }

        private bool Validate()
        {
            bool isValid = true;

            if (String.IsNullOrWhiteSpace(typUkonuTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Typ.");
            }

            if (String.IsNullOrWhiteSpace(cisloJednaciTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Číslo Jednací.");
            }

            if (!DateTime.TryParse(datumUkonuDatePicker.Text, out _))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Datum Přidání.");
            }

            return isValid;

        }

        private void odebratUkonButton_Click(object sender, RoutedEventArgs e)
        {
            List<UkonModel> ukony = GlobalConfig.ukonFile.FullFilePath().LoadFileAll().ConvertToUkonModels();
            UkonModel ukonModel = (UkonModel)ukonyDataGrid.SelectedItem;

            if (!(ukonModel == null))
            {
                ukony.RemoveUkonFromFile(ukonModel.Id);
                WireUpUkony();
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný prvek.");
            }

        }
    }
}
