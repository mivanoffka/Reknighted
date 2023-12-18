﻿using Reknighted.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Security.Policy;
using System.Windows;
using System.Runtime.CompilerServices;
using Reknighted.Controller;
using System.Text;
using System.Text.Json.Serialization;

namespace Reknighted.Model
{
    [JsonDerivedType(typeof(ItemModel))]
    [JsonDerivedType(typeof(DurableItem))]
    [JsonDerivedType(typeof(FoodModel), typeDiscriminator: "food")]
    [JsonDerivedType(typeof(ArmorModel), typeDiscriminator: "armor")]
    [JsonDerivedType(typeof(ArtefactModel), typeDiscriminator: "artefact")]
    [JsonDerivedType(typeof(PotionModel), typeDiscriminator: "potion")]
    [JsonDerivedType(typeof(WeaponModel), typeDiscriminator: "weapon")]
    [JsonPolymorphic(
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

    public abstract class ItemModel : IDisposable
    {
        protected string _name;
        protected string _description;
        protected int _price;

        [JsonIgnore]
        protected Image _image;
        protected bool _isPossessed = true;
        private Cell? _cell = null;
        [JsonInclude]
        public string _pathToImage;
        public string PathToImage 
        { 
            get { return _pathToImage; }
            set
            {
                _pathToImage = value;
                if (_pathToImage == "" || _pathToImage == null)
                {
                    _pathToImage = Images.Sources.QuestionMark;
                }
                _image = new Image();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                var path = Path.Combine(Environment.CurrentDirectory, _pathToImage);
                bitmap.UriSource = new Uri(path);
                bitmap.EndInit();
                _image.Source = bitmap;
            }
        }

        public string Name { get { return _name; } }
        public string Description { get { return "   " + _description;} }
        public int Price
        {
            get
            {
                if (IsPossessed)
                {
                    return SellPrice;
                }
                else
                {
                    return BuyPrice;
                }
            }
        }

        public int SellPrice
        {
            get
            {
                int value = _price;
                value = (int)(value * 0.8);
                if (Game.PlayerModel.Faction == Faction.Diamonds)
                {
                    value = (int)(value * 1.1);
                }

                return value;
            }
        }

        public int BuyPrice
        {
            get
            {
                int value = _price;
                value = (int)(value * 1.2);
                if (Game.PlayerModel.Faction == Faction.Diamonds)
                {
                    value = (int)(value * 0.9);
                }

                return value;
            }
        }

        [JsonIgnore]
        public Image Image { get { return _image; } }

        public bool IsPossessed
        {
            get => _isPossessed;
            set =>_isPossessed=value;
        }
        [JsonIgnore]
        public Cell? Cell { get => _cell; set => _cell = value; }

        public ItemModel(string name, string description, int basePrice, string imageSource = "")
        {
            _name = name;
            _description = description;
            _price = basePrice;
            PathToImage = imageSource;
        }
        public ItemModel(ItemModel itemModel) 
        {
            _name = itemModel.Name;
            _description= itemModel.Description;
            _price = itemModel.Price;
            _image = itemModel.Image;
        }
        public ItemModel()
        {

        }

        [JsonConstructor]
        public ItemModel(string name, string description, int price, bool isPossessed, Cell cell, string pathToImage)
        {
            _name = name;
            _description = description;
            _price = price;
            _isPossessed = isPossessed;
            _cell = cell;
            PathToImage = pathToImage;
        }


        public void MoveToCell(Cell newCell)
        {   

            if (this.GetType() != newCell.Filter && newCell.Filter != null)
            {
                return;
            }

            if (newCell?.ContentItem == this || this.IsPossessed != newCell?.IsPossessed)
            {
                return;   
            }

            try
            {
                var oldCell = this._cell;
                var bufferItem = newCell.ContentItem;

                int oldIndex = Game.InventoryCells.IndexOf(oldCell);

                Game.PlayerView!.Model![oldIndex] = bufferItem;
                if (bufferItem != null)
                {
                    bufferItem.Cell = oldCell;
                }

                int newIndex = Game.InventoryCells.IndexOf(newCell);
                Game.PlayerView!.Model![newIndex] = this;
                this.Cell = newCell;
            }
            catch (Exception ex)
            {
                Game.Message(ex.Message, MessageType.Error);
            }

        }

        public virtual string Help()
        {
            return string.Empty;
        }

        public virtual void Use()
        {

        }

        public virtual void SellTo(ITradeable buyer, bool showWarning = true)
        {
            ITradeable? customer = IsPossessed ? Game.CurrentTrader : Game.PlayerModel;
            ITradeable? seller = IsPossessed ?  Game.PlayerModel : Game.CurrentTrader;

            if (customer != null)
            {
                if (customer.Balance >= this.Price)
                {
                    seller.RemoveItem(this);
                    this.IsPossessed = !this.IsPossessed;
                    customer.AddItem(this);

                    int price = customer == Game.PlayerModel ? this.BuyPrice : this.SellPrice;

                    customer.Balance -= price;
                    seller.Balance += price;
                }
                else
                {
                    //MessageBox.Show("А денежек то у нас и не хватает!" + "\n\n" + customer.Balance + "/" + this.Price, "Упс...", MessageBoxButton.OK, MessageBoxImage.Error);
                    Game.Message($"{Game.app.FindResource("msgNoMoney")}", MessageType.Error);
                }

            }
        }

        public void Dispose()
        {
            
        }

        public abstract ItemModel Copy();
    }
}
