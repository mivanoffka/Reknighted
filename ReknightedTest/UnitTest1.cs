using Reknighted;
using Reknighted.Controller;
using System.Globalization;
using System.Windows;

namespace ReknightedTest
{
    public class UnitTest1
    {
        [Fact]
        [STAThread]
        public void SuccesfullCultureChange()
        {
            Thread thread = new Thread(() =>
            {
                StartWindow startWindow = new StartWindow();
                App.Language = new CultureInfo("en-EN");

                Assert.Equal(new CultureInfo("en-EN"), Reknighted.Properties.Settings.Default.DefaultLanguage);
            });
            thread.SetApartmentState(ApartmentState.STA);

        }

        [Fact]
        [STAThread]
        public void IPCheck()
        {
            Thread thread = new Thread(() =>
            {
                StartWindow startWindow = new StartWindow();
                Assert.Equal(12345, IPaddr.Port);
            });
            thread.SetApartmentState(ApartmentState.STA);

        }
    }
}