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
    /// Interaction logic for Tisk.xaml
    /// </summary>
    public partial class Tisk : Window
    {
        public Tisk()
        {
            InitializeComponent();
            WireUpStatistika();
            SetDefaultDate();
        }

        private void WireUpStatistika()
        {


            List<StatistikaModel> statistikas = GlobalConfig.statistikaFile.FullFilePath().LoadFileAll().ConvertToStatModels();
            tiskDataGrid.ItemsSource = statistikas;
        }

        private void SetDefaultDate()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            doDatePicker.SelectedDate = DateTime.Today;
            
        }

        private void filtrovatButton_Click(object sender, RoutedEventArgs e)
        {
            List<StatistikaModel> filteredStatistikas = new();
            List<StatistikaModel> statistikas = GlobalConfig.statistikaFile.FullFilePath().LoadFileAll().ConvertToStatModels();

            foreach (StatistikaModel statistika in statistikas)
            {
               
                if (statistika.Vec.ToLower().Contains(vecTiskTextBox.Text.Trim().ToLower()) &&
                    statistika.Typ.ToLower().Contains(typUkonuTiskTextBox.Text.Trim().ToLower()) &&
                    statistika.Zadatel.ToLower().Contains(zadatelTiskTextBox.Text.Trim().ToLower()) &&
                    (statistika.DatumVydani > odDatePicker.SelectedDate ||
                    odDatePicker.SelectedDate == null) &&
                    statistika.DatumVydani < doDatePicker.SelectedDate)

                {
                    filteredStatistikas.Add(statistika);
                }

            }
            tiskDataGrid.ItemsSource = filteredStatistikas;
            InitializeComponent();
        }

        private void tiskButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(tiskDataGrid, "Statistika");
            }

        }
    }
}
