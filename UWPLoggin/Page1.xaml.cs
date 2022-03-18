using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;
using UWPLoggin;
using System.Text.RegularExpressions;

// https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPLoggin
{
    /// <summary>
    /// Sida för att registrera en ny användare
    /// </summary>
    public sealed partial class Page1 : Page
    {        
        public Page1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Metod för att kontrollera om lösenordet matchar det givna Regex-skriptet
        /// </summary>
        /// <param name="password">Angav lösenord vid registrering av ny användare</param>
        /// <returns>True eller False, matchar eller nej</returns>
        bool IsPassword(string password)
        {
            Regex rx = new Regex (@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");          
            return rx.IsMatch(password);

        }

        /// <summary>
        /// En metod som kontrollerar korrektheten av att fylla i registreringsformuläret 
        /// och skickar meddelanden vid ifyllningsfel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegisterButtonPage1_Click(object sender, RoutedEventArgs e)
        {
            string username = MyTextBoxLogin.Text;
            string password = MyTextBoxPassword.Text;

            //Om fälten i registreringsformuläret
            //inte är ifyllda, eller ett av fälten
            //inte är ifyllt, visas en dialogruta
            //med ett meddelande.
            if (username == "" || password == "") 
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Fill in all the fields!",
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();

                Frame.Navigate(typeof(MenuPage));
            }

            //Om alla fält är ifyllda och lösenordet skapas enligt Regex-skriptet

            else if (IsPassword(password))
            {
                User newUser = new User(username, password);

                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Done!",
                    Content = " Are you registered!! " +
                    "\nYour Login is: " + newUser.Username + " and password is: " + newUser.Password,

                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
                var parameters = new UserItem();
                parameters.Name = MyTextBoxLogin.Text;
                parameters.Password = MyTextBoxPassword.Text;
                Frame.Navigate(typeof(MainPage), parameters);//Skickar data om ett nytt objekt av klass Person till
                                                             //inloggningssidan för att läggas till i listan users.
            }

            //Om alla fält är ifyllda men lösenordet inte matchar Regex-skriptet
            else
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Password must be at least 6 characters long and contain at one letter and one number",                  
                    PrimaryButtonText = "OK"
                };
                await dialog.ShowAsync();
                MyTextBoxLogin.Text = "";
                MyTextBoxPassword.Text = "";
                Frame.Navigate(typeof(MenuPage));
            }          
        }
    }
}
