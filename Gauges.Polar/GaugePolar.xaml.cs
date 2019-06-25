using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gauges.Polar
{
    /// <summary>
    /// Interaction logic for GaugePolar.xaml
    /// </summary>
    public partial class GaugePolar : UserControl, INotifyPropertyChanged
    {

        #region Interfaces Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChangedProperty(String property) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        #endregion

        #region Properties

        private Thickness location;

        public Thickness Location
        {
            get { return location; }
            set { location = value; OnChangedProperty("Location"); }
        }


        [Description(" "), Category("Special")]
        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }

        [Description("Angle of Indicator"), Category("Special")]
        public int Angle { get { return (int)GetValue(AngleProperty); } set { SetValue(AngleProperty, value); } }

        [Description("Distance from Orgin of Indicator"), Category("Special")]
        public int Radius { get { return (int)GetValue(RadiusProperty); } set { SetValue(RadiusProperty, value); } }

        public int IndicatorDiameter { get { return (int)GetValue(IndicatorDiameterProperty); } set { SetValue(IndicatorDiameterProperty, value); } }

        [Description(" "), Category("Special")]
        public int Division { get { return (int)GetValue(DivisionProperty); } set { SetValue(DivisionProperty, value); } }

        #endregion

        #region Dependency Property Register

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(GaugePolar),
                new PropertyMetadata("0", new PropertyChangedCallback(OnChangedLabel)));

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(int), typeof(GaugePolar),
                new PropertyMetadata(0, new PropertyChangedCallback(OnChangedAngle), new CoerceValueCallback(OnCoerceAngle)));

        public static readonly DependencyProperty DivisionProperty =
            DependencyProperty.Register("Division", typeof(int), typeof(GaugePolar),
                new PropertyMetadata(2, new PropertyChangedCallback(OnChangedDivision), new CoerceValueCallback(OnCoerceDivision)));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(GaugePolar),
                new PropertyMetadata(0, new PropertyChangedCallback(OnChangedRadius), new CoerceValueCallback(OnCoerceRadius)));

        public static readonly DependencyProperty IndicatorDiameterProperty =
            DependencyProperty.Register("IndicatorDiameter", typeof(int), 
                typeof(GaugePolar), new PropertyMetadata(8));


        #endregion

        #region Dependency Callback Methods

        private static object OnCoerceAngle(DependencyObject d, object baseValue) =>
            (int)baseValue < 0 ? 0 : (int)baseValue > 360 ? 360 : (int)baseValue;
        private static object OnCoerceRadius(DependencyObject d, object baseValue) =>
            (int)baseValue < 0 ? 0 : (int)baseValue > 100 ? 100 : (int)baseValue;

        private static object OnCoerceDivision(DependencyObject d, object baseValue) =>
            (int)baseValue < 1 ? 1 : (int)baseValue > 10 ? 10 : (int)baseValue;

        private static void OnChangedAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetIndicatorPosition(d);
        }

        private static void OnChangedRadius(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetIndicatorPosition(d);
        }

        private static void OnChangedLabel(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnChangedLocation(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static void OnChangedDivision(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var _this = (d as GaugePolar);
            var _division = 4; // (int)_this.Division < 1 ? 1 : (int)_this.Division;
            var _diameter = 200 / _division;

            //foreach (UIElement item in LogicalTreeHelper.GetChildren(_this.GridMain))
            //{
            //    if ((item is Ellipse) && (string)(item as Ellipse).Tag == "deletable")
            //    {
            //        _this.GridMain.Children.Remove(item);
            //    }
            //}

            for (int i = 1; i <= _division; i++)
            {
                var ellipse = new Ellipse();
                ellipse.Width = _diameter * i;
                ellipse.Height = _diameter * i;
                ellipse.Fill = new SolidColorBrush(Color.FromArgb(0, 222, 222, 222));
                ellipse.StrokeThickness = 1;
                ellipse.Stroke = new SolidColorBrush(Color.FromArgb(255, 221, 22, 221));
                ellipse.Tag = "deletable";
                _this.GridMain.Children.Add(ellipse);
            }
        }

        private static void SetIndicatorPosition(DependencyObject d)
        {
            var _this = (d as GaugePolar);
            var _angle = _this.Angle;
            var _radian = (Math.PI / 180) * _angle;
            var _orgin = 100 - (_this.IndicatorDiameter / 2);
            var _radius = _this.Radius - (_this.IndicatorDiameter / 2);

            _this.Location = new Thickness(
                _orgin + (_radius * Math.Cos(_radian)),
                _orgin + (_radius * Math.Sin(_radian)),
                0, 0);
        }

        #endregion

        public GaugePolar()
        {
            InitializeComponent();
        }
    }
}
