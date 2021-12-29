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
    /// Interaction logic for Statistika.xaml
    /// </summary>
    public partial class Statistika : Window
    {
        public Statistika()
        {
            InitializeComponent();
            WireUpStatistika();
            SetDefaultDate();
        }

        private void WireUpStatistika()
        {
            

            List<StatistikaModel> statistikas = GlobalConfig.statistikaFile.FullFilePath().LoadFileAll().ConvertToStatModels();
            statistikaDataGrid.ItemsSource = statistikas;
        }

        private void SetDefaultDate()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            datumUkonuDatePicker.SelectedDate = DateTime.Today;

        }

        private void odebratStatButton_Click(object sender, RoutedEventArgs e)
        {
            List<StatistikaModel> statistiky = GlobalConfig.statistikaFile.FullFilePath().LoadFileAll().ConvertToStatModels();
            StatistikaModel statModel = (StatistikaModel)statistikaDataGrid.SelectedItem;

            if (!(statModel == null))
            {
                statistiky.RemoveStatistikaFromFile(statModel.Id);
                WireUpStatistika();
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný prvek.");
            }
        }

        private void pridatStatButton_Click(object sender, RoutedEventArgs e)
        {

            if (Validate())
            {
                StatistikaModel statistika = new();
                statistika.SpisZn = spisZnTextBox.Text.Trim();
                statistika.CisloJednaci = cisloJednaciTextBox.Text.Trim();
                statistika.Typ = typUkonuTextBox.Text.Trim();
                statistika.Vec = vecTextBox.Text.Trim();
                statistika.Zadatel = zadatelTextBox.Text.Trim();
                statistika.DatumVydani = (DateTime)datumUkonuDatePicker.SelectedDate;
                GlobalConfig.Connection.CreateStatistika(statistika);

                WireUpStatistika();
                spisZnTextBox.Text = "";
                cisloJednaciTextBox.Text = "";
                typUkonuTextBox.Text = "";
                vecTextBox.Text = "";
                zadatelTextBox.Text = "";
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

            if (String.IsNullOrWhiteSpace(spisZnTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Spisová Značka.");
            }

            if (String.IsNullOrWhiteSpace(vecTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Věc.");
            }

            if (String.IsNullOrWhiteSpace(zadatelTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Žadatel.");
            }

            return isValid;

        }

        private void tiskStatButton_Click(object sender, RoutedEventArgs e)
        {
            Tisk tisk = new();
            tisk.ShowDialog();
        }
    }
}
