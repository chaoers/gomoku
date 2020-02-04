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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace backgammon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var point1 = new Point(10, 10);
            var point2 = new Point(100, 10);
            var line1 = new LineGeometry(point1, point1);
            var path =
                    new Path()
                    {
                        Name = "path",
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        Data = line1
                    };
            pan.Children.Add(path);
            var animation = new PointAnimation(point1, point2, new Duration(TimeSpan.FromSeconds(3)));
            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(animation);
            RegisterName("path", line1);
            Storyboard.SetTargetName(animation, "path");
            Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));
            MouseDown += (s, e) => myStoryboard.Begin(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            player.Content = "点击事件";
        }
    }
}
