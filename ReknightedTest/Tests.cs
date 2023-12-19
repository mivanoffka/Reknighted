using Reknighted;
using Reknighted.Controller;
using Reknighted.Model.Entities;
using Reknighted.Model.Items;
using System.Globalization;
using System.Windows;

namespace ReknightedTest
{
    public class Tests
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

        [Fact]
        [STAThread]
        public void InventoryCheck_1()
        {
            string expected = "null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, ";

            PlayerModel player = new PlayerModel(Reknighted.Model.Faction.Hearts);
            string actual = ItemsToString(player.Items);


            Assert.Equal(actual, expected);
        }

        [Fact]
        [STAThread]
        public void InventoryCheck_2()
        {
            FoodModel item = new FoodModel("test", "test", 0, 0, "test");
            string expected = "test, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, ";


            PlayerModel player = new PlayerModel(Reknighted.Model.Faction.Hearts);
            player.AddItem(item.Copy(), true);
            string actual = ItemsToString(player.Items);


            Assert.Equal(actual, expected);
        }

        [Fact]
        public void InventoryCheck_3()
        {
            FoodModel item = new FoodModel("test", "test", 0, 0, "test");
            string expected = "null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, ";

            PlayerModel player = new PlayerModel(Reknighted.Model.Faction.Hearts);
            player.AddItem(item, true);
            player.RemoveItem(item, true);
            string actual = ItemsToString(player.Items);

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void InventoryCheck_4()
        {
            FoodModel item = new FoodModel("test", "test", 0, 0, "test");
            string expected = "test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, test, ";

            PlayerModel player = new PlayerModel(Reknighted.Model.Faction.Hearts);
            for (int i = 0; i < 27; i++)
            {
                player.AddItem(item.Copy(), true);
            }

            string actual = ItemsToString(player.Items);


            Assert.Equal(actual, expected);
        }

        private string ItemsToString(List<Reknighted.Model.Items.ItemModel?> lst)
        {
            string names = "";

            foreach (var i in lst)
            {   
                if (i != null)
                {
                    names += i.Name + ", ";
                }
                else
                {
                    names += "null, ";
                }

            }

            return names;
        }
    }
}