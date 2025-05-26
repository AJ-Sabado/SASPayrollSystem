using System.Windows;
using PresentationLayer.WPF.ViewModel.PopUpViewModel;

namespace PresentationLayer.WPF.View.Windows.PopUps
{
    /// <summary>
    /// Interaction logic for AddHolidays_View.xaml
    /// </summary>
    public partial class AddHolidays_View : Window
    {
        public AddHolidays_View(AddHolidays_ViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
