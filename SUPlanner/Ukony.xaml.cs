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
    /// Interaction logic for Ukony.xaml
    /// </summary>
    public partial class Ukony : Window, ISelectedSpisRequest
    {
        public Ukony(ISelectedSpisRequest caller)
        {
            InitializeComponent();
        }

        public SpisModel SelectedSpis()
        {
            throw new NotImplementedException();
        }
    }
}
