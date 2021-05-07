using SUPlannerLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        ISpisRequest spisRequest;
        public Spis(ISpisRequest caller)
        {
            InitializeComponent();

            spisRequest = caller;
        }

        private void ulozSpisButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {


                SpisModel spis = new();
                spis.Cislo = (int)cisloSpisuIntegerUpDown.Value;
                spis.Zadatel = zadatelTextBox.Text;
                spis.Vec = vecTextBox.Text;
                spis.DatumPridani = (DateTime)podaniDatePicker.SelectedDate;
                spis.LimitniDatum = (DateTime)limitDatePicker.SelectedDate;
                GlobalConfig.Connection.CreateSpis(spis);

                spisRequest.SpisComplete();
                this.Close();
            }
        }

        private bool Validate()
        {
            bool isValid = true;

            

            if (!int.TryParse(cisloSpisuIntegerUpDown.Text, out int testCislo))
            {
                isValid = false;
                MessageBox.Show("Chybná hodnota v číselníku.");
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
    }
}
