using PresentationLayer.Presenters.Base;
using PresentationLayer.Views;
using PresentationLayer.Views.FileLeaveForm;
using Unity;
using Unity.Lifetime;

namespace PresentationLayer;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Syncfusion.Licensing
            .SyncfusionLicenseProvider
            .RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cVGhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEBjWn9ZcnRQQGNaU0xxXw==");

        IUnityContainer UnityC = new UnityContainer();
        UnityC.RegisterType<IBasePresenter, BasePresenter>(new HierarchicalLifetimeManager());

        var basePresenter = UnityC.Resolve<IBasePresenter>();

        //Application.Run(form);
        Application.Run((Login_Form)basePresenter.LoginView);
        //Application.Run(new ForgotPassword(servicesManager));
        //Application.Run(new Employee_Dashboard(servicesManager.CurrentUser, servicesManager));
        //Application.Run(new Dashboard_Employee());
        //Application.Run(new FileLeave_Form());
        //Application.Run(new Edit_Information());
    }
}