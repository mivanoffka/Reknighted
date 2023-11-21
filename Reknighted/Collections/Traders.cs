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
            for (int i = 0; i < 18; i++)
            {
                items.Add(new FoodModel(Items.Apple));
            }


            foreach (ItemModel item in items)
            {
                item.IsPossessed = false;
                Peter.Items.Add(item);
            }            
        }
    }
}
