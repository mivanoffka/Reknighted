using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Reknighted.Model
{

    public enum Location
    {
        Masquarade, Hearts, Clubs, Spades, Diamonds, ShowRoom


    }
    
    public enum MessageType
    {
        Information, Win, Loose, Error
    }

    public enum TraderType
    {
        Universal, Food, Armor, Weapon, Artefact
    }

    public enum Faction
    {
        Hearts, Spades, Clubs, Diamonds
    }

    public enum Buff
    {
        Health, Damage, Protection, Wealth
    }

}
