using System.Windows.Controls;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for ICULayoutConfiguration.xaml
    /// </summary>
    public partial class IcuLayoutConfiguration : UserControl
    {
        public IcuLayoutConfiguration()
        {
            InitializeComponent();
            DataContext = new ViewModels.IcuLayoutConfiguration();
        }
    }
}
