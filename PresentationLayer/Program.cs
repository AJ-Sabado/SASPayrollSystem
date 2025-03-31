using PresentationLayer.Presenters.Base;
using PresentationLayer.Views;
using Unity;
using Unity.Lifetime;

namespace PresentationLayer;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        Syncfusion.Licensing
            .SyncfusionLicenseProvider
            .RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cVGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEBjWn9ZcnRQQGNaU0xxXw==");

        IUnityContainer UnityC = new UnityContainer();
        UnityC.RegisterType<IBasePresenter, BasePresenter>(new HierarchicalLifetimeManager());

        var basePresenter = UnityC.Resolve<IBasePresenter>();
        ApplicationConfiguration.Initialize();

        //Application.Run(form);
        Application.Run((Dashboard_Employee)basePresenter.DashboardEmployeeView);
        //Application.Run(new ForgotPassword(servicesManager));
        //Application.Run(new Employee_Dashboard(servicesManager.CurrentUser, servicesManager));
    }
}