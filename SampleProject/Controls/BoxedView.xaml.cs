using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleProject.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoxedView : Grid
    {
        private const int NumberOfChildren = 4; // the 4 walls
        private int _numberOfChildrenAdded;

        private string _sides;
        public string Sides
        {
            get { return _sides; }
            set
            {
                _sides = value;
                SetSizesAndSides();
            }
        }

        public static BindableProperty SizesProperty = BindableProperty.Create(nameof(Sizes), typeof(string), typeof(BoxedView), string.Empty, BindingMode.TwoWay, null,
                                                                   (bindable, oldValue, newValue) => { (bindable as BoxedView).Sizes = (string)newValue; });

        private string _sizes;
        public string Sizes
        {
            get { return _sizes; }
            set
            {
                _sizes = value;
                SetSizesAndSides();
            }
        }

        public static BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(BoxedView), Color.LightGray, BindingMode.TwoWay, null,
                                                                   (bindable, oldValue, newValue) =>
                                                                   {
                                                                       (bindable as BoxedView).LeftWall.BackgroundColor = (Color)newValue;
                                                                       (bindable as BoxedView).RightWall.BackgroundColor = (Color)newValue;
                                                                       (bindable as BoxedView).TopWall.BackgroundColor = (Color)newValue;
                                                                       (bindable as BoxedView).BottomWall.BackgroundColor = (Color)newValue;
                                                                   });

        private Color _lineColor;
        public Color LineColor
        {
            get { return _lineColor; }
            set
            {
                _lineColor = value;
            }
        }


        public BoxedView()
        {
            InitializeComponent();
        }

        protected override void OnAdded(View view)
        {
            base.OnAdded(view);

            if (_numberOfChildrenAdded >= NumberOfChildren)
            {
                Children.Add(view, 1, 1);
            }

            _numberOfChildrenAdded++;
        }

        private void SetSizesAndSides()
        {
            if (!string.IsNullOrEmpty(Sides))
            {
                var sides = Sides.Split(',');

                LeftWall.IsVisible = sides[0].ToLowerInvariant() == "true";
                TopWall.IsVisible = sides[1].ToLowerInvariant() == "true";
                RightWall.IsVisible = sides[2].ToLowerInvariant() == "true";
                BottomWall.IsVisible = sides[3].ToLowerInvariant() == "true";
            }

            if (!string.IsNullOrEmpty(Sizes))
            {
                var sides = Sizes.Split(',');

                LeftWall.WidthRequest = int.Parse(sides[0]);
                ColumnDefinitions[0].Width = int.Parse(sides[0]);

                TopWall.HeightRequest = int.Parse(sides[1]);
                RowDefinitions[0].Height = int.Parse(sides[1]);

                RightWall.WidthRequest = int.Parse(sides[2]);
                ColumnDefinitions[2].Width = int.Parse(sides[2]);

                BottomWall.HeightRequest = int.Parse(sides[3]);
                RowDefinitions[2].Height = int.Parse(sides[3]);
            }
        }
    }
}