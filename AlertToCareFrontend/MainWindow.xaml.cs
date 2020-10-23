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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlertToCareFrontend.Views;

namespace AlertToCareFrontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static Views.RespondToCare respondToCare = new RespondToCare();
        static Views.MonitorOccupancy monitorOccupancy = new MonitorOccupancy();
       
       private  UserControl respondtocareControl { get; set; }
        private UserControl monitoroccupancyControl { get; set; }
        private void BackButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            

           
                    //this.monitoroccupancyControl.Resources.Remove(this);
            
        }

        public static bool IsWindowOpen<T>(string name = "") where T : UserControl
        {
            return string.IsNullOrEmpty(name)
                ? Application.Current.Windows.OfType<T>().Any()
                : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
       
    }
}
