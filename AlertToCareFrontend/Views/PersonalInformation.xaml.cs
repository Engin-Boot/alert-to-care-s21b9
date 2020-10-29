using System.Windows.Controls;

namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for PersonalInformation.xaml
    /// </summary>
    public partial class PersonalInformation : UserControl
    {
        public PersonalInformation()
        {

            InitializeComponent();
            DataContext = new ViewModels.PersonalInformation();
        }

        
    }
}
