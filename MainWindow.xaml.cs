using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI_notconsole_HARD
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double value = 0;           //Standardwerte festlegen
        int fromCurrency = -1;      //Standardwerte festlegen
        int toCurrency = -1;           //Standardwerte festlegen
        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();         //Shutdown Button
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);      //Fenster verschieben
            DragMove();
        }
        public MainWindow()
        {
            InitializeComponent();

        }

        private void convert_button_Click(object sender, RoutedEventArgs e)
        {
            string[] currency = new string[10] { "EUR", "USD", "JPY", "GBP", "AUD", "CAD", "CHF", "HKD", "KRW", "XAU" };        //Array für Currency Symbole erstellen
            double[] currencytoEUR = new double[10] { 1.00, 1.18, 126.32, 0.9, 1.65, 1.56, 1.08, 9.16, 1400.27, 0.000607 };     //Array für Umrechnungsfaktoren erstellen

            fromCurrency = this.fromcurrency_combobox.SelectedIndex;        //fromcurrency einlesen
            toCurrency = this.tocurrency_combobox.SelectedIndex;        //tocurrency einlesen
            try
            {
                value = Convert.ToDouble(this.valuetoconvert.Text);         //value einlesen
            }
            catch
            {
                this.valuetoconvert.Text = "0";         //falls keine eingabe gemacht wurde ist alles 0!
                value = 0;
            }
            double outputValue = (value / currencytoEUR[fromCurrency]) * currencytoEUR[toCurrency];         //Umrechnen

            this.Ergebnis_Textbox.Text = outputValue.ToString("0.00") + " " + Convert.ToString(currency[toCurrency]);         //Ergebnis ausgeben
            this.log_textbox.Text += (valuetoconvert + " " + currency[fromCurrency] + " --> " + outputValue.ToString("0.00") + " " + currency[toCurrency] + Environment.NewLine);       //Ergebnisse Loggen


        }
        private void swap_button_Click(object sender, RoutedEventArgs e)
        {

            (this.fromcurrency_combobox.SelectedIndex, this.tocurrency_combobox.SelectedIndex) = (this.tocurrency_combobox.SelectedIndex, this.fromcurrency_combobox.SelectedIndex);            //Swap-Button funktion, tauschen der combobox untereinander 
            (this.valuetoconvert.Text) = (this.Ergebnis_Textbox.Text);          //Swap-Button Funktion, value wird gleich dem ergebnis gesetzt
            this.Ergebnis_Textbox.Text = "";            //Swap-Button Funktion, Ergebnis wird gecleared
        }


        private void valuetoconvert_TextChanged(object sender, TextChangedEventArgs e)
        {

            // Lese den Text String der Textbox aus und tausche . zu ,
            string replace = Regex.Replace((this.valuetoconvert.Text), @"\.|[a-zA-Z;:_!§$%&/()=?*+'#üäö<>^°´`´ß""-]|[ ]", "");
            // Setze den so bereinigten Text zurück in die Textbox
            this.valuetoconvert.Text = replace;
            //Setze den Courser ans Ende des bereinigten Strings in der Textbox
            this.valuetoconvert.CaretIndex = replace.Length;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.valuetoconvert.Text = "";
            this.Ergebnis_Textbox.Text = "";
            this.log_textbox.Text = "";
            toCurrency = -1;
            fromCurrency = -1;
            fromcurrency_combobox.SelectedIndex = 0;
            tocurrency_combobox.SelectedIndex = 1;
        }
    }
}
