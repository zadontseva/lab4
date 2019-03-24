using Zadontseva_01.Tools.Managers;
using Zadontseva_01.ViewModels;
using System.Windows.Controls;

namespace Zadontseva_01.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AllPersonsUserControl.xaml
    /// </summary>
    public partial class AllPersonsUserControl : UserControl, INavigatable
    {
        public AllPersonsUserControl()
        {
            InitializeComponent();
            DataContext = new AllPersonsViewModel();
        }

        private void CheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
