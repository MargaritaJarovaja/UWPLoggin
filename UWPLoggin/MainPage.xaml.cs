using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UWPLoggin;


//  https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace UWPLoggin
{
    /// <summary>
    /// Registrerad lösenordskontrollsida
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<User> users = new List<User>();
        public MainPage()
        {
            this.InitializeComponent();
            users.Add(new User("Margarita", "2011"));            
        }

        /// <summary>
        /// Metod för att komma åt data på registreringssidan
        /// </summary>
        /// <param name="e">händelser med egenskapen Parameter-nytt objekt av klass Person.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UserItem userItem = new UserItem();            
            base.OnNavigatedTo(e);
            var parameters = (UserItem)e.Parameter;

            if (parameters != null)
            {
                string regName = parameters.Name;
                string regPass = parameters.Password;
                users.Add(new User(regName, regPass));
            }
        }

        /// <summary>
        /// Metod för att kontrollera om användaren är registrerad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginButtonMainPage_Click(object sender, RoutedEventArgs e)
        {
            string username = MyTextBoxLogin.Text;
            string password = MyTextBoxPassword.Text;

            foreach (User u in users)
            {
                if (u.Username == username && u.Password == password)
                {                 
                    ContentDialog msDialog = new ContentDialog() 
                    {
                        Title = "\t\nYou are logged in!",                        
                        PrimaryButtonText = "OK"
                    };

                    await msDialog.ShowAsync();
                    Frame.Navigate(typeof(Page2),users);                  
                }
                else
                {
                    MyTextBoxLogin.Text = "";
                    MyTextBoxPassword.Text = "";
                }
               
            }
        }

        /// <summary>
        /// Metod för att skicka till registreringssidan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterButtonMainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page1));
        }
    }
}
