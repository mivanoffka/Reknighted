using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Reknighted.Controller;
using Reknighted.Model.Items;

namespace Reknighted
{

    public partial class Cell : UserControl
    {
        private Point _position = new Point(0, 0);
        private bool _isPointed = false;
        private ItemModel? _contentItem = null;
        private bool _isPossessed = true;
        private Type? _filter = null;

        public bool IsPossessed
        {
            get => _isPossessed;
            set => _isPossessed = value;
        }

        [JsonIgnore]
        public ItemModel? ContentItem
        {
            get { return _contentItem; }
            set {
                _contentItem = value;

                if (value == null)
                {
                    indicator.Visibility = Visibility.Visible;
                }
                else
                {
                    indicator.Visibility = Visibility.Collapsed;
                }
            }
        }

        public Point Position
        {
            get 
            {
                return _position;
            } 
            set
            { 
                _position = value;
                this.Margin = new Thickness(_position.X, _position.Y, 0, 0);
                
            } 
        }
        public bool IsPointed
        {
            get
            {
                return _isPointed;
            }
            set
            {
                _isPointed = value;

                if (value == true)
                {

                    this.Opacity = 1d;
                    //this.border.BorderBrush = new SolidColorBrush(Color.FromRgb(202, 162, 72));
                    this.border.BorderThickness = new Thickness(1.5);


                }
                else
                {
                    this.Opacity = 0.75d;
                    //this.border.BorderBrush = new SolidColorBrush(Colors.Gray);
                    this.border.BorderThickness = new Thickness(1);

                }
            }
        }

        [JsonIgnore]
        public Type? Filter { get => _filter; 
            set
            {
                _filter = value;

                if (_filter == null)
                {
                    indicator.Text = " ";
                }
                if (_filter == typeof(WeaponModel))
                {
                    indicator.Text = $"{Game.app.FindResource("nWeapon")}";
                }
                if (_filter == typeof(ArmorModel))
                {
                    indicator.Text = $"{Game.app.FindResource("nArmor")}";
                }
                if (_filter == typeof(ArtefactModel))
                {
                    indicator.Text = $"{Game.app.FindResource("nArtefact")}";
                }
            }
        }

        public Cell() : this(new Point(0, 0))
        {
            this.Filter = null;
        }
        public Cell(Point position)
        {
            Game.AllCells.Add(this);
            Position = position;
            InitializeComponent();
            this.BorderBrush = new SolidColorBrush(Colors.Gray);
        }
        public Cell(Point position, Type filter) : this(position) 
        {
            this.Filter = filter;
        }
    }
}
