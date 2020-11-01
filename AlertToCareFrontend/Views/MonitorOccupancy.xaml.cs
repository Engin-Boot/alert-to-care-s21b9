using System.Windows.Controls;
using AlertToCareFrontend.ViewModels;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for MonitorOccuppany.xaml
    /// </summary>
    public partial class MonitorOccupancy : UserControl
    {
        public MonitorOccupancy()
        {
            InitializeComponent();
            DataContext = new MonitoringOccupancy();
        }
    }
}
