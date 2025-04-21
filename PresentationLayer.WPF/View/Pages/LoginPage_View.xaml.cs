using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SASPayrolSystemProject
{

    public partial class MainWindow : Window
    {
        private bool isSignIn;
        public MainWindow()
        {
            isSignIn = true;
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnForgotPassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void updateFloatingPanel()
        {
            if (isSignIn)
            {
                // Start the 'Sign In' animation
                BeginStoryboard((Storyboard)FindResource("sbFloatingPanel1"));
                BeginStoryboard((Storyboard)FindResource("sbFloatingPanel2"));

                // Update UI elements for "Sign In" mode
                btnFloatingPanel.Content = "Sign In";
                tbFloatingTitle.Text = "Already have an account?";
                tbFloatingDisc.Text = "Sign in now and pick up right where you left off. Join our Strategic Assistant Staffing Family and move forward with us toward a smarter, more efficient future.";
            }
            else
            {
                // Start the 'Sign Up' animation
                BeginStoryboard((Storyboard)FindResource("sbFloatingPanel1Reversed"));
                BeginStoryboard((Storyboard)FindResource("sbFloatingPanel2Reversed"));

                // Update UI elements for "Sign Up" mode
                btnFloatingPanel.Content = "Sign Up";
                tbFloatingTitle.Text = "New here? Join us now!";
                tbFloatingDisc.Text = "Don't have an account yet? Sign up today and become a valued member of our ever-growing Strategic Assistant Staffing Family! Take the first step toward a smarter, more efficient future with us.";
            }

            // Toggle the state
            isSignIn = !isSignIn;
        }


        private void btnFloatingPanel_Click(object sender, RoutedEventArgs e)
        {
            updateFloatingPanel();
        }
    }
}