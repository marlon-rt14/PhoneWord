using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace p_PhoneNumberTranslator
{
    public class MainPage : ContentPage
    {
        Entry txtPhoneNumber;
        Button btnTranslate;
        Button btnCallButton;
        string translatedNumber;

        public MainPage()
        {
            //Establecer el padding en 20 alrededor de la pagina
            this.Padding = new Thickness(20, 20, 20, 20);

            //Crear un espaciado de 15 entre las vistas del StackLayout
            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            //Crear los controles para la interfaz PhoneWord UI
            panel.Children.Add(new Label
            {
                Text = "Enter a PhoneWord: ",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });
            panel.Children.Add(txtPhoneNumber = new Entry
            {
                Text = "1-855-XAMARIN"
            });
            panel.Children.Add(btnTranslate = new Button
            {
                Text = "Translate",
                
            });
            panel.Children.Add(btnCallButton = new Button
            {
                Text = "Call"
            });
            panel.Children.Add(new Label
            {
                Text = "Desarrollado por Marlon Ruiz."
            });

            //Asignar un evento al boton btnTranslate
            btnTranslate.Clicked += BtnTranslate_Clicked;

            //Asignar un evento al boton Llamar
            btnCallButton.Clicked += BtnCallButton_Clicked;

            //Asignar al StackLayout la propiedad Content de MainPage
            this.Content = panel;
        }

        async void BtnCallButton_Clicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                "Marcar número", //Titulo
                "Te gustaría llamar al número " + translatedNumber + "?", //Mensaje
                "Si", //Botonos para cancelar y aceptar
                "No")
                )
            {
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Incapaz de llamar", "Número de teléfono inválido.", "Ok");
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Incapaz de llamar", "No se admite la marcación telefónica.", "Ok.");
                }
                catch (Exception)
                {
                    await DisplayAlert("Incapaz de llamar", "Fallo al realizar la llamada.", "Ok");
                }
            }   
        }

        private void BtnTranslate_Clicked(object sender, EventArgs e)
        {
            string enteredNumber = txtPhoneNumber.Text;
            translatedNumber = PhonewordTranslator.toNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                btnCallButton.IsEnabled = true;
                btnCallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                btnCallButton.IsEnabled = false;
                btnCallButton.Text = "Call";
            }
        }
    }
}
