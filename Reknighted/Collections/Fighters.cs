using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Collections
{
    public class Fighters
    {   
        public static Fighter Simon = new Fighter("Саймон", new ItemModel[] {Items.Sword.Copy(), Items.Helmet.Copy(), null});
    }
}
