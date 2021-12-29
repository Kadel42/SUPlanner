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
            System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
            if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                tiskDataGrid.Measure(pageSize);
                tiskDataGrid.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(tiskDataGrid, Title);
            }
        }
    }
}
