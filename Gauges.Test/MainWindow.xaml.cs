using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gauges.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Storyboard sb = new Storyboard();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStartRadar_Click(object sender, RoutedEventArgs e)
        {   
            DoubleAnimation da = new DoubleAnimation(0, 360, TimeSpan.FromMilliseconds(1300));

            //SineEase easingFunction = new SineEase();
            //easingFunction.EasingMode = EasingMode.EaseIn;
            //da.EasingFunction = easingFunction;

            Storyboard.SetTarget(da, this.GaugePolar1);
            Storyboard.SetTargetProperty(da, new PropertyPath(Gauges.Polar.GaugePolar.RadarAngleProperty));
            sb.Children.Add(da);
   
            sb.RepeatBehavior = RepeatBehavior.Forever;

            sb.Begin();
        }

        private void BtnStopRadar_Click(object sender, RoutedEventArgs e)
        {
            sb?.Stop();
        }
    }
}
