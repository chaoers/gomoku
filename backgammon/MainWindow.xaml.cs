﻿using System;
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
        private bool isWinBool = true;
        public static MainWindow mainWindow;
        public bool color = true;
        // 1-黑 0-白
        private static Board mainboard = new Board();
        public MainWindow()
        {
            InitializeComponent();
    
            MouseDown += (s, e) => pan_MouseDown(s, e); // 监听鼠标移动
            addButton();

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

        private BlackPiece addBlack(int X, int Y)
        {
            var black = new BlackPiece();
            pan.Children.Add(black);
            Canvas.SetLeft(black, X-15);
            Canvas.SetTop(black, Y-15);
            logText.AppendText("添加黑棋\n");
            return black;
        }
        private WhitePiece addWhite(int X, int Y)
        {
            var white = new WhitePiece();
            pan.Children.Add(white);
            Canvas.SetLeft(white, X-15);
            Canvas.SetTop(white, Y-15);
            logText.AppendText("添加白棋\n");
            return white;
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
            for (int i = 0; i < 15; i++)
            {
                pan.UnregisterName("line_0" + i);
                pan.UnregisterName("line_1" + i);
            }
        }
        private void finishDraw(object sender, EventArgs eventArgs)
        {
            logText.AppendText("构建棋盘完成\n");
            isWinBool = false;
        }
        private void addButton()
        {
            Button btn = new Button()
            {
                Name = "startButton",
                Content = "构建棋盘",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(266, 266, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Height = 58,
                Width = 104,
            };
            btn.Click += new RoutedEventHandler(drawButton);
            pan.Children.Add(btn);
            pan.RegisterName("drawButton", btn);
        }

        private void drawButton(object sender, RoutedEventArgs e)
        {
            pan.Children.Clear();
            Button btn = pan.FindName("drawButton") as Button;
            pan.UnregisterName("drawButton");
            this.startDraw();
        }

        private void logText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pan_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.GetPosition(pan).X >= 0 && e.GetPosition(pan).X <= 570 
                && e.GetPosition(pan).Y >=0 && e.GetPosition(pan).Y <= 570 && isWinBool == false)
            {
                int[] position = mainboard.placePosition(e.GetPosition(pan).X, e.GetPosition(pan).Y);
                if(position[0] != -1) 
                {
                    downPiece(position[0], position[1]);
                }
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.GetPosition(pan).X >= 0 && e.GetPosition(pan).X <= 570
                && e.GetPosition(pan).Y >= 0 && e.GetPosition(pan).Y <= 570 && isWinBool == false)
            {
                int[] position = mainboard.placePosition(e.GetPosition(pan).X, e.GetPosition(pan).Y);
                if (position[0] != -1)
                {
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void downPiece(int X,int Y)
        {
            if(color == true)
            {
                mainboard.pieceNum[X, Y] = 1;
                addBlack(X * 40 + 10, Y * 40 + 10);
                var win = isWin(X, Y);
                if(win == true)
                {
                    logText.AppendText("游戏结束，黑棋胜利\n");
                    mainboard.clearPiece();
                    addButton();
                    isWinBool = true;
                    color = true;
                }
                else
                {
                    color = false;
                }
            }
            else
            {
                mainboard.pieceNum[X, Y] = 2;
                addWhite(X * 40 + 10, Y * 40 + 10);
                var win = isWin(X, Y);
                if (win == true)
                {
                    logText.AppendText("游戏结束，白棋胜利\n");
                    mainboard.clearPiece();
                    addButton();
                    isWinBool = true;
                    color = true;
                }
                else
                {
                    color = true;
                }
            }
        }

        private bool isWin(int X,int Y)
        {
            var Z = color == true ? 1 : 2;
            // 横向查找
            var left = 0;
            for(int i = X;i >= 0; i--)
            {
                if(mainboard.pieceNum[i,Y] != Z)
                {
                    break;
                }
                left++;
            }
            var right = 0;
            for (int i = X; i < 15; i++)
            {
                if (mainboard.pieceNum[i, Y] != Z)
                {
                    break;
                }
                right++;
            }
            if(left + right > 5)
            {
                return true;
            }
            // 竖向查找
            var top = 0;
            for (int i = Y; i >= 0; i--)
            {
                if (mainboard.pieceNum[X, i] != Z)
                {
                    break;
                }
                top++;
            }
            var bottom = 0;
            for (int i = Y; i < 15; i++)
            {
                if (mainboard.pieceNum[X, i] != Z)
                {
                    break;
                }
                bottom++;
            }
            if(top + bottom > 5)
            {
                return true;
            }
            // 左上到右下查找
            var left_top = 0;
            for (int [] i = {X,Y}; i[0]>=0&&i[1] >= 0; i[0]--,i[1]--)
            {
                if (mainboard.pieceNum[i[0], i[1]] != Z)
                {
                    break;
                }
                left_top++;
            }
            var right_bottom = 0;
            for (int[] i = { X, Y }; i[0] < 15 && i[1] <= 15; i[0]++, i[1]++)
            {
                if (mainboard.pieceNum[i[0], i[1]] != Z)
                {
                    break;
                }
                right_bottom++;
            }
            if (left_top + right_bottom > 5)
            {
                return true;
            }
            // 右上到左下查找
            var right_top = 0;
            for (int[] i = { X, Y }; i[0] < 15 && i[1] >= 0; i[0]++, i[1]--)
            {
                if (mainboard.pieceNum[i[0], i[1]] != Z)
                {
                    break;
                }
                right_top++;
            }
            var left_bottom = 0;
            for (int[] i = { X, Y }; i[0] >= 0 && i[1] < 15; i[0]--, i[1]++)
            {
                if (mainboard.pieceNum[i[0], i[1]] != Z)
                {
                    break;
                }
                left_bottom++;
            }
            if (right_top + left_bottom > 5)
            {
                return true;
            }
            return false;
        }
    }
}
