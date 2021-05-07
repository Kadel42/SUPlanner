using SUPlannerLibraries;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SUPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISpisRequest
    {
        public MainWindow()
        {
            if (!Directory.Exists(GlobalConfig.filePath))
            {
                Directory.CreateDirectory(GlobalConfig.filePath);
            }
            GlobalConfig.InitializeConnections();
            
            
            InitializeComponent();
            WireUpDataGrid();
           
        }


        private void WireUpDataGrid()
        {
            List<SpisModel> model = GlobalConfig.spisFile.FullFilePath().LoadFile().ConvertToSpisModels();
            spisyDataGrid.ItemsSource = model; 
        }

        private void notesButton_Click(object sender, RoutedEventArgs e)
        {
            Notes notes = new();
            
            notes.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Spis spis = new(this);
            
            spis.Show();
        }

        private void soupisButton_Click(object sender, RoutedEventArgs e)
        {
            SoupisSpisu soupis = new();
            
            soupis.Show();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void SpisComplete()
        {
            WireUpDataGrid();
        }
    }
}
