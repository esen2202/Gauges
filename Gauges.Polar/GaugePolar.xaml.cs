using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        public Thickness Location { get { return location; } set { location = value; OnChangedProperty("Location"); } }

        private double radarX;

        public double RadarX { get { return radarX; } set { radarX = value; OnChangedProperty("RadarX"); } }

        private double radarY;

        public double RadarY { get { return radarY; } set { radarY = value; OnChangedProperty("RadarY"); } }

        [Description("Label"), Category("Special")]
        public string Label { get { return (string)GetValue(LabelProperty); } set { SetValue(LabelProperty, value); } }

        [Description("Angle of Indicator"), Category("Special")]
        public double Angle { get { return (double)GetValue(AngleProperty); } set { SetValue(AngleProperty, value); } }

        [Description("Distance from Orgin of Indicator"), Category("Special")]
        public double Radius { get { return (double)GetValue(RadiusProperty); } set { SetValue(RadiusProperty, value); } }

        [Description("Division Count"), Category("Special")]
        public int Division { get { return (int)GetValue(DivisionProperty); } set { SetValue(DivisionProperty, value); } }

        [Description("Division Stroke Color"), Category("Special")]
        public Brush DivisionStrokeColor { get { return (Brush)GetValue(DivisionStrokeColorProperty); } set { SetValue(DivisionStrokeColorProperty, value); } }

        [Description("Stroke Color"), Category("Special")]
        public Brush XAxisLineColor { get { return (Brush)GetValue(XAxisLineColorProperty); } set { SetValue(XAxisLineColorProperty, value); } }

        [Description("Stroke Color"), Category("Special")]
        public Brush YAxisLineColor { get { return (Brush)GetValue(YAxisLineColorProperty); } set { SetValue(YAxisLineColorProperty, value); } }

        [Description("Background Color"), Category("Special")]
        public Brush BackgroundColor { get { return (Brush)GetValue(BackgroundColorProperty); } set { SetValue(BackgroundColorProperty, value); } }

        [Description("Border Color"), Category("Special")]
        public Brush BorderColor { get { return (Brush)GetValue(BorderColorProperty); } set { SetValue(BorderColorProperty, value); } }

        [Description("Indicator Diameter"), Category("Special")]
        public double IndicatorDiameter { get { return (double)GetValue(IndicatorDiameterProperty); } set { SetValue(IndicatorDiameterProperty, value); } }

        [Description("Indicator Color"), Category("Special")]
        public Brush IndicatorColor { get { return (Brush)GetValue(IndicatorColorProperty); } set { SetValue(IndicatorColorProperty, value); } }

        [Description("Active Region Radius"), Category("Special")]
        public double ActiveRegionRadius { get { return (double)GetValue(ActiveRegionRadiusProperty); } set { SetValue(ActiveRegionRadiusProperty, value); } }

        [Description("Active Region Color"), Category("Special")]
        public Brush ActiveRegionColor { get { return (Brush)GetValue(ActiveRegionColorProperty); } set { SetValue(ActiveRegionColorProperty, value); } }

        [Description("Radar Line Angle"), Category("Special")]
        public double RadarAngle { get { return (double)GetValue(RadarAngleProperty); } set { SetValue(RadarAngleProperty, value); } }

        [Description("Radar Line Color"), Category("Special")]
        public Brush RadarLineColor { get { return (Brush)GetValue(RadarLineColorProperty); } set { SetValue(RadarLineColorProperty, value); } }

        [Description("Radar Line Stroke Thickness"), Category("Special")]
        public double RadarLineStrokeThickness { get { return (double)GetValue(RadarLineStrokeThicknessProperty); } set { SetValue(RadarLineStrokeThicknessProperty, value); } }


        #endregion

        #region Dependency Property Register

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(GaugePolar),
                new PropertyMetadata("0", new PropertyChangedCallback(OnChangedLabel)));

        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(GaugePolar),
                new PropertyMetadata(0.0, new PropertyChangedCallback(OnChangedAngle), new CoerceValueCallback(OnCoerceAngle)));

        public static readonly DependencyProperty DivisionProperty =
            DependencyProperty.Register("Division", typeof(int), typeof(GaugePolar),
                new PropertyMetadata(2, new PropertyChangedCallback(OnChangedDivision), new CoerceValueCallback(OnCoerceDivision)));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(double), typeof(GaugePolar),
                new PropertyMetadata(0.0, new PropertyChangedCallback(OnChangedRadius), new CoerceValueCallback(OnCoerceRadius)));

        public static readonly DependencyProperty DivisionStrokeColorProperty =
            DependencyProperty.Register("DivisionStrokeColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.AntiqueWhite)));

        public static readonly DependencyProperty XAxisLineColorProperty =
            DependencyProperty.Register("XAxisLineColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public static readonly DependencyProperty YAxisLineColorProperty =
            DependencyProperty.Register("YAxisLineColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public static readonly DependencyProperty IndicatorDiameterProperty =
            DependencyProperty.Register("IndicatorDiameter", typeof(double), typeof(GaugePolar),
                 new PropertyMetadata(8.0, new PropertyChangedCallback((d, e) => { }), new CoerceValueCallback(OnCoerceIndicatorDiameter)));

        public static readonly DependencyProperty IndicatorColorProperty =
            DependencyProperty.Register("IndicatorColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.BlueViolet)));

        public static readonly DependencyProperty ActiveRegionRadiusProperty =
            DependencyProperty.Register("ActiveRegionRadius", typeof(double), typeof(GaugePolar),
                new PropertyMetadata(60.0, new PropertyChangedCallback(OnChangedActiveRegion), new CoerceValueCallback(OnCoerceActiveRegion)));

        public static readonly DependencyProperty ActiveRegionColorProperty =
            DependencyProperty.Register("ActiveRegionColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public static readonly DependencyProperty RadarAngleProperty =
            DependencyProperty.Register("RadarAngle", typeof(double), typeof(GaugePolar), new PropertyMetadata(0.0, new PropertyChangedCallback(OnChangeRadarAngle)));

        public static readonly DependencyProperty RadarLineColorProperty =
            DependencyProperty.Register("RadarLineColor", typeof(Brush), typeof(GaugePolar), new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public static readonly DependencyProperty RadarLineStrokeThicknessProperty =
             DependencyProperty.Register("RadarLineStrokeThickness", typeof(double), typeof(GaugePolar), new PropertyMetadata(1.0));

        #endregion

        #region Dependency Callback Methods

        private static object OnCoerceAngle(DependencyObject d, object baseValue) =>
            (double)baseValue < 0 ? 0 : (double)baseValue > 360 ? 360 : (double)baseValue;
        private static object OnCoerceRadius(DependencyObject d, object baseValue) =>
            (double)baseValue < 0 ? 0 : (double)baseValue > 100 ? 100 : (double)baseValue;

        private static object OnCoerceDivision(DependencyObject d, object baseValue) =>
            (int)baseValue < 1 ? 1 : (int)baseValue > 10 ? 10 : (int)baseValue;

        private static object OnCoerceActiveRegion(DependencyObject d, object baseValue) =>
            (double)baseValue < 0 ? 0 : (double)baseValue > 200 ? 200 : (double)baseValue;

        private static object OnCoerceIndicatorDiameter(DependencyObject d, object baseValue) =>
            (double)baseValue < 1 ? 1 : (double)baseValue > 12 ? 12 : (double)baseValue;
        private static void OnChangedActiveRegion(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

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
            CreateDivisionEllipse(d);
        }

        private static void OnChangeRadarAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetRadarLineRotation(d);

        }

        private static void SetRadarLineRotation(DependencyObject d)
        {
            var _this = (d as GaugePolar);
            var _angle = _this.RadarAngle;
            var _radian = (Math.PI / 180) * _angle;
            var _orgin = 100;
            var _radius = 100;
             
            _this.RadarX = _orgin + (_radius * Math.Cos(_radian));
            _this.RadarY = _orgin + (_radius * Math.Sin(_radian));
        }

        private static void CreateDivisionEllipse(DependencyObject d)
        {
            var _this = (d as GaugePolar);
            var _division = (int)_this.Division < 1 ? 1 : (int)_this.Division;
            var _diameter = 200 / _division;

            _this.GridDrawing.Children.Clear();

            for (int i = 1; i <= _division; i++)
            {
                var ellipse = new Ellipse();
                ellipse.Width = _diameter * i;
                ellipse.Height = _diameter * i;
                ellipse.Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                ellipse.StrokeThickness = 1;

                Binding bind = new Binding("DivisionStrokeColor");
                bind.Source = _this;
                ellipse.SetBinding(Ellipse.StrokeProperty, bind);

                ellipse.Tag = "ellipse";
                _this.GridDrawing.Children.Add(ellipse);
            }
        }

        private static void SetIndicatorPosition(DependencyObject d)
        {
            var _this = (d as GaugePolar);
            var _angle = _this.Angle;
            var _radian = (Math.PI / 180) * _angle;
            var _orgin = 100 - (_this.IndicatorDiameter / 2);
            var _radiusPercentRatio = (100 - (_this.IndicatorDiameter / 2)) / 100;
            var _radius = _this.Radius * _radiusPercentRatio;//- (_this.IndicatorDiameter / 2);

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
