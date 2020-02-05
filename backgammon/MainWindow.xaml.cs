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
            var storyboard = new Storyboard();
            for (int i = 0; i < 15; i++)
            {
                var start = new Point(10+i*40,10);
                var end = new Point(10+i*40,570);
                
                var line = new LineGeometry(start,start);
                RegisterName("line_0"+i,line);

                var path = new Path()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Data = line
                };

                pan.Children.Add(path);
                var animation = new PointAnimation(start, end, new Duration(TimeSpan.FromSeconds(3)))
                {
                    BeginTime = TimeSpan.FromMilliseconds(i * 200)
                };
                storyboard.Children.Add(animation);
                Storyboard.SetTargetName(animation, "line_0"+i);
                Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));
            }
            for (int i = 0; i < 15; i++)
            {
                var start = new Point(10, 10+i*40);
                var end = new Point(570, 10+i*40);

                var line = new LineGeometry(start, start);
                RegisterName("line_1" + i, line);

                var path = new Path()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Data = line
                };

                pan.Children.Add(path);
                var animation = new PointAnimation(start, end, new Duration(TimeSpan.FromSeconds(3)))
                {
                    BeginTime = TimeSpan.FromMilliseconds(i * 200)
                };
                storyboard.Children.Add(animation);
                Storyboard.SetTargetName(animation, "line_1" + i);
                Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));
            }
            storyboard.Begin(this);
        }
    }
}
