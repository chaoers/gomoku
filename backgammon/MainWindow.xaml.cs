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
        public static MainWindow mainWindow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.StartDraw();
        }

        private void StartDraw()
        {
            var point_1_1 = new Point(10, 10);
            var point_1_2 = new Point(10, 50);
            var point_1_3 = new Point(10, 90);
            var point_1_4 = new Point(10, 130);
            var point_1_5 = new Point(10, 170);
            var point_1_6 = new Point(10, 210);
            var point_1_7 = new Point(10, 250);
            var point_1_8 = new Point(10, 290);
            var point_1_9 = new Point(10, 330);
            var point_1_10 = new Point(10, 370);
            var point_1_11 = new Point(10, 410);
            var point_1_12 = new Point(10, 450);
            var point_1_13 = new Point(10, 490);
            var point_1_14 = new Point(10, 530);
            var point_1_15 = new Point(10, 570);

            var point_15_1 = new Point(570, 10);
            var point_15_2 = new Point(570, 50);
            var point_15_3 = new Point(570, 90);
            var point_15_4 = new Point(570, 130);
            var point_15_5 = new Point(570, 170);
            var point_15_6 = new Point(570, 210);
            var point_15_7 = new Point(570, 250);
            var point_15_8 = new Point(570, 290);
            var point_15_9 = new Point(570, 330);
            var point_15_10 = new Point(570, 370);
            var point_15_11 = new Point(570, 410);
            var point_15_12 = new Point(570, 450);
            var point_15_13 = new Point(570, 490);
            var point_15_14 = new Point(570, 530);
            var point_15_15 = new Point(570, 570);

            var point_2_1 = new Point(50, 10);
            var point_3_1 = new Point(50, 50);
            var point_4_1 = new Point(50, 90);
            var point_5_1 = new Point(50, 130);
            var point_6_1 = new Point(50, 170);
            var point_7_1 = new Point(50, 210);
            var point_8_1 = new Point(50, 250);
            var point_9_1 = new Point(50, 290);
            var point_10_1 = new Point(50, 330);
            var point_11_1 = new Point(50, 370);
            var point_12_1 = new Point(50, 410);
            var point_13_1 = new Point(50, 450);
            var point_14_1 = new Point(50, 490);

            var point_2_15 = new Point(570, 10);
            var point_3_15 = new Point(570, 50);
            var point_4_15 = new Point(570, 90);
            var point_5_15 = new Point(570, 130);
            var point_6_15 = new Point(570, 170);
            var point_7_15 = new Point(570, 210);
            var point_8_15 = new Point(570, 250);
            var point_9_15 = new Point(570, 290);
            var point_10_15 = new Point(570, 330);
            var point_11_15 = new Point(570, 370);
            var point_12_15 = new Point(570, 410);
            var point_13_15 = new Point(570, 450);
            var point_14_15 = new Point(570, 490);

            var line1 = new LineGeometry(point1, point1);
            RegisterName("path", line1);
            var path =
                    new Path()
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 2,
                        Data = line1
                    };
            pan.Children.Add(path);
            var animation = new PointAnimation(point1, point2, new Duration(TimeSpan.FromSeconds(3)));
            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(animation);
            Storyboard.SetTargetName(animation, "path");
            Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));
            myStoryboard.Begin(this);
        }
    }
}
