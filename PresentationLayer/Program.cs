using PresentationLayer.Presenters.Base;
using PresentationLayer.Views;
using PresentationLayer.Views.Custom_Message_Box;
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
            .RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXtfeHVXR2BdUUJwXUVWYUA=");

        IUnityContainer UnityC = new UnityContainer();
        UnityC.RegisterType<IBasePresenter, BasePresenter>(new HierarchicalLifetimeManager());

        var basePresenter = UnityC.Resolve<IBasePresenter>();

        //Application.Run(form);
        //Application.Run((Login_Form)basePresenter.LoginView);
        //Application.Run(new ForgotPassword_View());
        //Application.Run(new Employee_Dashboard(servicesManager.CurrentUser, servicesManager));
        //Application.Run(new Dashboard_Employee());
        //Application.Run(new FileLeave_Form());
        //Application.Run(new Edit_Information());
        Application.Run(new DialogBox(null,"I am Tim",dBoxType.Success));
    }
}