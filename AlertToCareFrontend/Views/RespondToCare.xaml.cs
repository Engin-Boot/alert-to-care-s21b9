namespace AlertToCareFrontend.Views
{
    /// <summary>
    /// Interaction logic for RespondToCare.xaml
    /// </summary>
    public partial class RespondToCare
    {
        public RespondToCare()
        {
            InitializeComponent();
            DataContext = new ViewModels.RespondToCare();
        }
    }
}
