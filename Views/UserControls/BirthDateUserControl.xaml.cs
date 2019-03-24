using Zadontseva_01.Tools.Managers;
using Zadontseva_01.ViewModels;
using System.Windows.Controls;

namespace Zadontseva_01.Views.UserControls
{
    /// <summary>
    /// Interaction logic for BirthDateUserControl.xaml
    /// </summary>
    public partial class BirthDateUserControl : UserControl, INavigatable
    {
        public BirthDateUserControl()
        {
            InitializeComponent();
            DataContext = new BirthDateViewModel();
        }
    }
}
