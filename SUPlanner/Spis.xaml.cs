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
    /// Interaction logic for Spis.xaml
    /// </summary>
    public partial class Spis : Window
    {
        private int indexOfUnusedNumber = 0;
        ISpisRequest spisRequest;
        public Spis(ISpisRequest caller)
        {
            
            InitializeComponent();
            SetDefaultDate();
            SetDefaultCislo();
            spisRequest = caller;
        }

        private void SetDefaultCislo()
        {
            cisloTextBox.Text = SetRollerNumbers()[0].ToString();
        }

        private List<int> SetRollerNumbers() // Makes a list of unused numbers
        {
            
            List<SpisModel> spisy = GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
            List<int> cisla = new();
            List<int> unusedNumbers = new();
            int cislo = 1;

            foreach (SpisModel spis in spisy)
            {
                cisla.Add(spis.Cislo);
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
            podaniDatePicker.SelectedDate = DateTime.Today;
            limitDatePicker.SelectedDate = DateTime.Today.AddDays(30);
        }

        private void ulozSpisButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {


                SpisModel spis = new();
                spis.Cislo = int.Parse(cisloTextBox.Text);
                spis.SpisZn = spisZnTextBox.Text;
                spis.Zadatel = zadatelTextBox.Text;
                spis.Vec = vecTextBox.Text;
                spis.DatumPridani = (DateTime)podaniDatePicker.SelectedDate;
                spis.LimitniDatum = (DateTime)limitDatePicker.SelectedDate;
                spis.Typ = typComboBox.SelectedValue.ToString();
                GlobalConfig.Connection.CreateSpis(spis);

                spisRequest.SpisComplete();
                this.Close();
            }
        }

        private bool Validate()
        {
            bool isValid = true;

            

            if (!int.TryParse(cisloTextBox.Text, out int testCislo))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v číselníku.");
            }
            if (String.IsNullOrWhiteSpace(spisZnTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Spisová značka.");
            }

            if (String.IsNullOrWhiteSpace(zadatelTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Žadatel.");
            }
            if (String.IsNullOrWhiteSpace(vecTextBox.Text))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Věc.");
            }
            bool podaniValid = DateTime.TryParse(podaniDatePicker.Text, out DateTime testPodani);
            if (!podaniValid)
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Datum Podání.");
            }
            if (!DateTime.TryParse(limitDatePicker.Text, out DateTime testLimit))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v poli Limitní Datum.");
            }

            return isValid;
            
        }

        private void upCisloButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> indexes = SetRollerNumbers();
            if (indexOfUnusedNumber < indexes.Count - 1)
            {
                indexOfUnusedNumber++;
                cisloTextBox.Text = indexes[indexOfUnusedNumber].ToString();
            }
            
        }

        private void downCisloButton_Click(object sender, RoutedEventArgs e)
        {
            if (indexOfUnusedNumber > 0)
            {
                indexOfUnusedNumber--;
                cisloTextBox.Text = SetRollerNumbers()[indexOfUnusedNumber].ToString();
            }
        }
    }
}
