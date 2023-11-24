using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Collections
{
    public class Traders
    {
        public static TraderModel Peter = new TraderModel(TraderType.Universal, "Петя");

        public static void Initialize()
        {   
            List<ItemModel> items = new List<ItemModel>();
            items.Add(Items.Apple.Copy());
            items.Add(Items.Cheese.Copy());
           

            foreach (ItemModel item in items)
            {
                item.IsPossessed = false;
                Peter.AddItem(item);
            }            
        }
    }
}
