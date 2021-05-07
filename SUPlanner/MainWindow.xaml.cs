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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (!Directory.Exists(GlobalConfig.filePath))
            {
                Directory.CreateDirectory(GlobalConfig.filePath);
            }
            GlobalConfig.InitializeConnections();
            
            
            InitializeComponent();
           
        }


        private void WireUpDataGrid()
        {
            
        }

        private void notesButton_Click(object sender, RoutedEventArgs e)
        {
            Notes notes = new Notes();
            
            notes.Show();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Spis spis = new Spis();
            
            spis.Show();
        }

        private void soupisButton_Click(object sender, RoutedEventArgs e)
        {
            SoupisSpisu soupis = new SoupisSpisu();
            
            soupis.Show();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
