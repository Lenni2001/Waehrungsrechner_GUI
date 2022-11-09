using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);      //Fenster verschieben
            DragMove();
        }
        public MainWindow()
        {
            InitializeComponent();      //Standard

        }

        private void convert_button_Click(object sender, RoutedEventArgs e)
        {
            string[] currency = new string[10] { "EUR", "USD", "JPY", "GBP", "AUD", "CAD", "CHF", "HKD", "KRW", "XAU" };        //Array für Currency Symbole erstellen
            double[] currencytoEUR = new double[10] { 1.00, 1.18, 126.32, 0.9, 1.65, 1.56, 1.08, 9.16, 1400.27, 0.000607 };     //Array für Umrechnungsfaktoren erstellen

            fromCurrency = this.combobox_fromcurrency.SelectedIndex;        //fromcurrency einlesen
            toCurrency = this.combobox_tocurrency.SelectedIndex;        //tocurrency einlesen
            try
            {
                value = Convert.ToDouble(this.textbox_valuetoconvert.Text);         //value einlesen
            }
            catch
            {
                this.textbox_valuetoconvert.Text = "0";         //falls keine eingabe gemacht wurde ist alles 0!
                value = 0;
            }

            double outputValue = (value / currencytoEUR[fromCurrency]) * currencytoEUR[toCurrency];         //Umrechnen
            double exchange = outputValue / Convert.ToDouble(textbox_valuetoconvert.Text);          //Umrechungsfaktoren in beider Richtungen bestimmen
            this.textbox_ergebnis.Text = outputValue.ToString("0.00") + " " + Convert.ToString(currency[toCurrency]);         //Ergebnis ausgeben
            this.log_textbox.Text += (textbox_valuetoconvert + " " + currency[fromCurrency] + " --> " + outputValue.ToString("0.00") + " " + currency[toCurrency] + " with an exchange Rate of: " + currencytoEUR[toCurrency] +  Environment.NewLine);       //Ergebnisse Loggen
            this.textbox_exchangerate.Text =  exchange.ToString("0.00");            //Umrechnungsfaktor einsetzen

        }
        private void swap_button_Click(object sender, RoutedEventArgs e)
        {

            (this.combobox_fromcurrency.SelectedIndex, this.combobox_tocurrency.SelectedIndex) = (this.combobox_tocurrency.SelectedIndex, this.combobox_fromcurrency.SelectedIndex);            //Swap-Button funktion, tauschen der combobox untereinander 
            (this.textbox_valuetoconvert.Text) = (this.textbox_ergebnis.Text);          //Swap-Button Funktion, value wird gleich dem ergebnis gesetzt
            this.textbox_ergebnis.Text = "";            //Swap-Button Funktion, Ergebnis wird gecleared
        }


        private void valuetoconvert_TextChanged(object sender, TextChangedEventArgs e)
        {

            // Lese den Text String der Textbox aus und tausche . zu ,
            string replace = Regex.Replace((this.textbox_valuetoconvert.Text), @"\.|[a-zA-Z;:_!§$%&/()=?*+'#üäö<>^°´`´ß""-]|[ ]", "");
            // Setze den so bereinigten Text zurück in die Textbox
            this.textbox_valuetoconvert.Text = replace;
            //Setze den Courser ans Ende des bereinigten Strings in der Textbox
            this.textbox_valuetoconvert.CaretIndex = replace.Length;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Alles Variablen auf Null setzen
            this.textbox_valuetoconvert.Text = "";
            this.textbox_ergebnis.Text = "";
            this.log_textbox.Text = "";
            this.textbox_exchangerate.Text = "";
            toCurrency = -1;
            fromCurrency = -1;
            combobox_fromcurrency.SelectedIndex = 0;
            combobox_tocurrency.SelectedIndex = 1;
        }

        private void valuetoconvert_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textbox_valuetoconvert.Text = "";

        }
    }
}
