using Reknighted.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reknighted.Collections
{
    public class Fighters
    {   
        public static Fighter Simon = new Fighter("Саймон", new ItemModel[] {Items.Sword.Copy(), Items.Helmet.Copy(), null}, Items.Helmet, Items.PathTo("blue_tower"), new System.Windows.Point(100, 100));
        public static Fighter HeartsGuardian = new Fighter("Червонный гвардеец", new ItemModel[] { Items.Weapons["hearts_sword"], Items.Armors["guardian_helmet"], null }, Items.Weapons["hearts_sword"], Items.PathTo("red_tower"), new System.Windows.Point(300, 50));
    }
}
