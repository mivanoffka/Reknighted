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

namespace Reknighted.Model
{
    public class ItemModel
    {
        private string _name;
        private string _description;
        private int _basePrice;
        private Image _image;

        public static ItemModel DefaultItem = new ItemModel("Неизвестный предмет", "Он пока ещё мало изучен, потому что от него получали больше звездюлей, чем информации.", -1);

        public string Name { get { return _name; } }
        public string Description { get { return _description;} }
        public int BasePrice { get { return _basePrice; } }
        public Image Image { get { return _image; } }

        public ItemModel(string name, string description, int basePrice, string imageSource = "")
        {
            _name = name;
            _description = description;
            _basePrice = basePrice;


            if (imageSource == "")
            {
                imageSource = Images.Images.QuestionMark;
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

        public string Information()
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

            return result;
        }

    }
}
