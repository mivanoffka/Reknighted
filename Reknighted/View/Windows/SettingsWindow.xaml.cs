using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Reknighted.Controller
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;
            ServerPort.Text = IPaddr.IP + ":" + IPaddr.Port;
            //Заполняем меню смены языка:
            cmbLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                cmbLanguage.Items.Add(menuLang);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in cmbLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }

        private void buttonIP_Click(object sender, RoutedEventArgs e)
        {
            string[] iptext = ServerPort.Text.Split(":");
            if (iptext.Length != 2 || !IPAddress.TryParse(iptext[0], out var ip) || iptext[1].Length != 5)
            {
                MessageBox.Show(Game.app.FindResource("msgIPParseError").ToString());
                return;
            }
            IPaddr.IP = IPAddress.Parse(iptext[0]);
            IPaddr.Port = int.Parse(iptext[1]);
        }
    }
}
