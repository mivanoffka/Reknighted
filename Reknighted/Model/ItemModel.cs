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

namespace Reknighted.Model
{
    public abstract class ItemModel
    {
        protected string _name;
        protected string _description;
        protected int _basePrice;
        protected Image _image;
        protected bool _isPossessed = true;
        private Cell? _cell = null;

        public string Name { get { return _name; } }
        public string Description { get { return _description;} }
        public int BasePrice { get { return _basePrice; } }
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
            _basePrice = basePrice;


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
            _basePrice = itemModel.BasePrice;
            _image = itemModel.Image;
        }

        public ItemModel()
        {

        }

        public void MoveToCell(Cell newCell)
        {   
            if (newCell?.ContentItem == this || this.IsPossessed != newCell?.IsPossessed)
            {
                return;   
            }

            try
            {
                var oldCell = this._cell;
                var bufferItem = newCell.ContentItem;

                int oldIndex = Game.InventoryCells.IndexOf(oldCell);
                Game.PlayerView!.PlayerModel!.Items[oldIndex] = bufferItem;
                if (bufferItem != null)
                {
                    bufferItem.Cell = oldCell;
                }

                int newIndex = Game.InventoryCells.IndexOf(newCell);
                Game.PlayerView!.PlayerModel!.Items[newIndex] = this;
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
            result += "\n\nЦена: " + _basePrice;

            return result;
        }

    }
}
