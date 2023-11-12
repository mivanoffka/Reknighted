using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reknighted.Model;

namespace Reknighted.Collections
{
    class Items
    {
        public static ItemModel Apple = new ItemModel("Яблоко", "Падает от яблони не сильно далеко.", 50, Images.Images.Apple);
        public static ItemModel Sword = new ItemModel("Меч", "Им можно кого-то убить. Наверное...", 300, Images.Images.Sword);

        public static void Initialize()
        {
        }
    }
}
