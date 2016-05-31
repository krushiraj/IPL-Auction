using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Networking.Proximity;
using Test_IPL.Pages;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Test_IPL
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
            PeerFinder.Stop();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }


         protected override void OnNavigatedTo(NavigationEventArgs e)
         {
             Frame rootFrame = Window.Current.Content as Frame;
             string myPages = "";
             foreach (PageStackEntry page in rootFrame.BackStack)
             {
                 myPages += page.SourcePageType.ToString() + "\n";
             }
             // stackCount.Text = myPages;
             if (rootFrame.CanGoBack)
             { // Show UI in title bar if opted-in and in-app backstack is not empty.
                 SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
             }
             else
             { // Remove the UI from the title bar if in-app back stack is empty. 
                 SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
             }
         } 

        /// <summary> 
                /// Invoked when this page is about to be displayed in a Frame. 
                /// </summary> 
                /// <param name="e">Event data that describes how this page was reached.  The Parameter 
                /// property is typically used to configure the page.</param> 
       

        private void JoinGameClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(JoinGamePage));
        }

        private void HostGameClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HostGamePage));
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
        }

        private void Instructions_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstructionsPage));
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            ShowExitDialog();
        }
        private async void ShowExitDialog()
        {
            ExitDialog.PrimaryButtonText = "Yes";
            ExitDialog.SecondaryButtonText = "No";
            ContentDialogResult exitDialog = await ExitDialog.ShowAsync();            
            if (exitDialog == ContentDialogResult.Primary)
            {
                App.Current.Exit();
            }
            else
            {
                // User pressed Cancel or the back arrow.
                this.Frame.Navigate(typeof(MenuPage));
            }

        }

    }
}
