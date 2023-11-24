using Reknighted.Images;
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

namespace Reknighted.Model
{
    public abstract class ItemModel : IDisposable
    {
        protected string _name;
        protected string _description;
        protected int _price;
        protected Image _image;
        protected bool _isPossessed = true;
        private Cell? _cell = null;

        public string Name { get { return _name; } }
        public string Description { get { return _description;} }
        public int Price { get { return _price; } }
        public Image Image { get { return _image; } }

        public bool IsPossessed
        {
            get => _isPossessed;
            set =>_isPossessed=value;
        }
        public Cell? Cell { get => _cell; set => _cell = value; }

        public ItemModel(string name, string description, int basePrice, string imageSource = "")
        {
            _name = name;
            _description = description;
            _price = basePrice;


            if (imageSource == "")
            {
                imageSource = Images.Sources.QuestionMark;
            }
            _image = new Image();

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            var path = Path.Combine(Environment.CurrentDirectory, imageSource);
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            _image.Source = bitmap;

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

                Game.PlayerView!.PlayerModel![oldIndex] = bufferItem;
                if (bufferItem != null)
                {
                    bufferItem.Cell = oldCell;
                }

                int newIndex = Game.InventoryCells.IndexOf(newCell);
                Game.PlayerView!.PlayerModel![newIndex] = this;
                this.Cell = newCell;
            }
            catch (Exception ex)
            {
                Game.Error(ex.Message);
            }

        }

        public virtual string Information()
        {
            string result = string.Empty;
            result += "[ " + Name + " ] ";

            string editedDescription = "\n\n";

            int maxCounter = 30;
            int counter = 0;
            for (int i = 0; i < Description.Length; i++)
            {
                if (counter >= maxCounter && Description[i] == ' ')
                {
                    editedDescription += "\n";
                    counter = 0;
                }
                else
                {
                    editedDescription += Description[i];
                }
                counter++;

            }

            result += editedDescription;
            result += "\n\nЦена: " + _price;

            return result;
        }

        public virtual void Use()
        {

        }

        public virtual void SellTo(ITradeable buyer, bool showWarning = true)
        {
            bool isConfirmed = true;
            if (showWarning)
            {
                string caption = this.IsPossessed ? "Продажа" : "Покупка";
                string text = this.IsPossessed ? "Вы уверены, что хотите продать [ " + this.Name + " ] за " + this.Price + " тугриков?"
                                               : "Вы уверены, что хотите купить [ " + this.Name + " ] за " + this.Price + " тугриков?";

                var result = MessageBox.Show(text, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                isConfirmed = result == MessageBoxResult.Yes;
            }

            if (!isConfirmed)
            {   
                return;
            }

            ITradeable? customer = IsPossessed ? Game.CurrentTrader : Game.PlayerModel;
            ITradeable? seller = IsPossessed ?  Game.PlayerModel : Game.CurrentTrader;

            if (customer != null)
            {
                if (customer.Balance >= this.Price)
                {
                    seller.RemoveItem(this);
                    this.IsPossessed = !this.IsPossessed;
                    customer.AddItem(this); ;
                    customer.Balance -= this.Price;
                    seller.Balance += this.Price;
                }
                else
                {
                    MessageBox.Show("А денежек то у нас и не хватает!" + "\n\n" + customer.Balance + "/" + this.Price, "Упс...", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        public void Dispose()
        {
            
        }

        public ItemModel Copy()
        {
            if (GetType() == typeof(FoodModel))
            {
                return new FoodModel((FoodModel)this);
            }
            else if (GetType() == typeof(ArmorModel))
            {
                return new ArmorModel((ArmorModel)this);
            }
            else if (GetType() == typeof(WeaponModel))
            {
                return new WeaponModel((WeaponModel)this);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
