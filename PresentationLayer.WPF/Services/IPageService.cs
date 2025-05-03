using System.Windows.Controls;

namespace PresentationLayer.WPF.Services
{
    public interface IPageService
    {
        TPage GetPage<TPage>() where TPage : UserControl;
    }
}