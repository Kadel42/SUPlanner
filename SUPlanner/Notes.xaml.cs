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
    /// Interaction logic for Notes.xaml
    /// </summary>
    public partial class Notes : Window
    {
        ISelectedSpisRequest selectedSpisRequest;
        public Notes(ISelectedSpisRequest caller)
        {
            InitializeComponent();
            selectedSpisRequest = caller;
            WireUpNotes();
        }

        private void WireUpNotes()
        {
            string notesText = selectedSpisRequest.SelectedSpis().Notes;
            notesTextBox.Text = notesText;
        }

        private void ulozNoteButton_Click(object sender, RoutedEventArgs e)
        {
            SpisModel model = selectedSpisRequest.SelectedSpis();
            model.Notes = notesTextBox.Text;
            List<SpisModel> spisy = GlobalConfig.spisFile.FullFilePath().LoadFileAll().ConvertToSpisModels();
            spisy.RemoveSpisFromFile(model.Id);
            GlobalConfig.Connection.CreateSpis(model);
            this.Close();
        }
    }
}
