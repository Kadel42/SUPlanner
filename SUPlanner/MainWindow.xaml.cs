using SUPlannerLibraries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SUPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISpisRequest, ISelectedSpisRequest
    {

        
        public MainWindow()
        {
            if (!Directory.Exists(GlobalConfig.filePath))
            {
                Directory.CreateDirectory(GlobalConfig.filePath);
            }
            GlobalConfig.InitializeConnections();

            // Sets normal format for DatePicker
            // TODO - find a way to make this a global setting
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            InitializeComponent();
            WireUpDataGrid();
           
        }


        private void WireUpDataGrid()
        {
            List<SpisModel> model = GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
            spisyDataGrid.ItemsSource = model;
            
        }

        private void WireUpDataGrid(string typ)
        {
            List<SpisModel> models = GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
            List<SpisModel> modelsToRemove = new();
            foreach (SpisModel model in models)
            {
                if (model.Typ != typ)
                {
                    modelsToRemove.Add(model);
                }
            }

            foreach (SpisModel removeModel in modelsToRemove)
            {
                models.Remove(removeModel);
            }
            
            spisyDataGrid.ItemsSource = models;
        }

        private void notesButton_Click(object sender, RoutedEventArgs e)
        {
            if (spisyDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Nebyl vybrán žádný spis.");
            }
            else
            {
                Notes notes = new(this);

                notes.Show();
            }
            
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            spisyDataGrid.SelectedIndex = -1;
            Spis spis = new(this);
            
            spis.Show();
        }

        private void soupisButton_Click(object sender, RoutedEventArgs e)
        {
            if (spisyDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Nebyl vybrán žádný spis.");
            }

            else
            {
                SoupisSpisu soupis = new(this);

                soupis.Show();
            }
            
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)   
        {
            List<SpisModel> spisy = GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
            List<PodkladModel> podklady = GlobalConfig.podkladFile.FullFilePath().LoadFileAll().ConvertToPodkladModels();
            SpisModel spisModel = (SpisModel)spisyDataGrid.SelectedItem;
            if (!(spisModel==null))
            {
                spisy.RemoveSpisFromFile(spisModel.Id);
                podklady.RemovePodkladBySpisId(spisModel.Id);
                WireUpDataGrid();
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný prvek.");
            }
            
            
        }

        public void SpisComplete()
        {
            WireUpDataGrid();
        }

        private void vseComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            WireUpDataGrid();
        }

        private void zadostiComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            WireUpDataGrid("Žádosti");
        }

        private void zahajeniComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            WireUpDataGrid("Zahájení");
        }

        private void vyzvyComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            WireUpDataGrid("Výzvy");
        }

        private void rozhodnutiComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            WireUpDataGrid("Rozhodnutí");
        }

        public SpisModel SelectedSpis()
        {
            SpisModel model = (SpisModel)spisyDataGrid.SelectedItem;


            return model;
            
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (spisyDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Nebyl vybrán žádný spis.");
            }
            else
            {
                Spis spis = new(this);

                spis.Show();
            }
            
        }

        private void statistikaButton_Click(object sender, RoutedEventArgs e)
        {
            Statistika statistika = new();
            statistika.Show();
        }

        private void ukonyButton_Click(object sender, RoutedEventArgs e)
        {
            if (spisyDataGrid.SelectedIndex == -1)
            {
                MessageBox.Show("Nebyl vybrán žádný spis.");
            }
            else
            {
                Ukony ukony = new(this);

                ukony.Show();
            }
            
        }
    }
}
