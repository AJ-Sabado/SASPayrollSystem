using MahApps.Metro.Converters;
using PresentationLayer.WPF.View.Windows.PopUps.CustomMessageBox;

namespace PresentationLayer.WPF.Services
{
    public class MyMessageBox
    {
        private readonly IServiceProvider _serviceProvider;

        public MyMessageBox(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MyMessageBoxResult? ShowDialog(string message = null, MyMessageBoxType type = MyMessageBoxType.Warning, byte[] salt = null, byte[] passwordHash = null)
        {
            salt = salt ?? [];
            passwordHash = passwordHash ?? [];

            MyMessageBoxResult? result = null;

            if (type == MyMessageBoxType.Warning)
            {
                var dialog = DIGetRequiredService<Warning_View>(_serviceProvider);
                if (!string.IsNullOrEmpty(message))
                    dialog.MessageText = message;
                bool? dialogResult = dialog.ShowDialog();
                result = new MyMessageBoxResult()
                {
                    DialogResult = dialogResult,
                    MyMessageBoxDialogResult = dialog.Result
                };
            }
            else if (type == MyMessageBoxType.Error)
            {
                var dialog = DIGetRequiredService<Error_View>(_serviceProvider);
                if (!string.IsNullOrEmpty(message))
                    dialog.MessageText = message;
                bool? dialogResult = dialog.ShowDialog();
                result = new MyMessageBoxResult()
                {
                    DialogResult = dialogResult,
                    MyMessageBoxDialogResult = dialog.Result
                };
            }
            else if (type == MyMessageBoxType.Confirmation)
            {
                var dialog = DIGetRequiredService<Question_View>(_serviceProvider);
                if (!string.IsNullOrEmpty(message))
                    dialog.MessageText = message;
                bool? dialogResult = dialog.ShowDialog();
                result = new MyMessageBoxResult()
                {
                    DialogResult = dialogResult,
                    MyMessageBoxDialogResult = dialog.Result
                };
            }
            else if (type == MyMessageBoxType.Success)
            {
                var dialog = DIGetRequiredService<Success_View>(_serviceProvider);
                if (!string.IsNullOrEmpty(message))
                    dialog.MessageText = message;
                bool? dialogResult = dialog.ShowDialog();
                result = new MyMessageBoxResult()
                {
                    DialogResult = dialogResult,
                    MyMessageBoxDialogResult = dialog.Result
                };
            }
            else if (salt.Length != 0 && passwordHash.Length != 0 && type == MyMessageBoxType.Password)
            {
                var dialog = DIGetRequiredService<PasswordPrompt_View>(_serviceProvider);
                dialog.Salt = salt;
                dialog.PasswordHash = passwordHash;
                bool? dialogResult = dialog.ShowDialog();
                result = new MyMessageBoxResult()
                {
                    DialogResult = dialogResult,
                    PasswordMatch = dialog.PasswordMatch
                };
            }
            return result;
        }

        private T DIGetRequiredService<T>(IServiceProvider serviceProvider) where T : class
        {
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService<T>(serviceProvider);
        }
    }

    public class MyMessageBoxResult
    {
        public bool? DialogResult { get; set; }
        public MyMessageBoxDialogResult MyMessageBoxDialogResult { get; set; } = MyMessageBoxDialogResult.None;
        public bool PasswordMatch { get; set; } = false;
    }

    public enum MyMessageBoxType
    {
        Success,
        Warning,
        Error,
        Confirmation,
        Password
    }

    public enum MyMessageBoxDialogResult
    {
        None,
        Okay,
        Yes,
        No,
        Cancel
    }
}
