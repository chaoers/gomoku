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
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public MainWindow()
        {
            InitializeComponent();
            logText.AppendText("初始化完成\n");
        }

        private void multiplayer(object sender, RoutedEventArgs e)
        {
            logText.AppendText("切换为玩家对战\n");
        }
        private void singleplayer(object sender, RoutedEventArgs e)
        {
            logText.AppendText("切换为AI对战\n");
        }

        private void addBlack(double X, double Y)
        {
            var black = new BlackPiece();
            pan.Children.Add(black);
            Canvas.SetLeft(black, X-15);
            Canvas.SetTop(black, Y-15);
            logText.AppendText("添加黑棋\n");
        }
        private void addWhite(double X, double Y)
        {
            var white = new WhitePiece();
            pan.Children.Add(white);
            Canvas.SetLeft(white, X-15);
            Canvas.SetTop(white, Y-15);
            logText.AppendText("添加白棋\n");
        }

        private void startDraw()
        {
            logText.AppendText("开始构建棋盘\n");
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
                    BeginTime = TimeSpan.FromMilliseconds(i * 214)
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
            storyboard.Completed += finishDraw;
            storyboard.Begin(this);
        }

        private void finishDraw(object sender, EventArgs eventArgs)
        {
            logText.AppendText("构建棋盘完成\n");
        }

        private void drawButton(object sender, RoutedEventArgs e)
        {
            startButton.Visibility = Visibility.Collapsed;
            this.startDraw();
        }

        private void logText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var black = new BlackPiece();
            pan.Children.Add(black);
            Canvas.SetLeft(black, e.GetPosition(pan).X - 15);
            Canvas.SetTop(black, e.GetPosition(pan).Y - 15);
            logText.AppendText("添加黑棋\n");
        }
    }
}
