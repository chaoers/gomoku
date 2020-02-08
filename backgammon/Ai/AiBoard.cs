using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiBoard
    {
        class AiStatistic
        {
            private int[,] table;
            public void init(int size)
            {
                table = new int[size, size];
            }
        }

        class Step
        {
            private int role;
        }
        // class Zobrist
        // {
        //     private int[,] com = new int[15,15];
        //     private int[,] hum = new int[15,15];
        //     public void init()
        //     {
        //         com = new int[15,15];
        //         hum = new int[15,15];
        //     }
        // }

        private AiStatistic statistic = new AiStatistic();
        private int[,] board = new int[15, 15];
        private Step[] currentSteps = new Step[0];
        private Step[] allSteps = new Step[0];
        private int count = 0;
        private int total = 0;

        // scoreCache[role][dir][row,column]
        private int[][][,] scoreCache = new int[3][][,] {new int[4][,]{new int[15,15],new int[15,15],new int[15,15],new int[15,15]},new int[4][,]{new int[15,15],new int[15,15],new int[15,15],new int[15,15]},new int[4][,]{new int[15,15],new int[15,15],new int[15,15],new int[15,15]}};

        // 储存双方得分
        private int[,] comScore = new int[15,15];
        private int[,] humScore = new int[15,15];

        // 传入棋子矩阵和位数
        public AiBoard(int[,] _board, int size)
        {
            board = _board;
            statistic.init(size);
        }

        private void initScore()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if(board[i,j] == (int)AiConfig.player.empty)
                    {
                        
                    }
                }
            }
        }

        private bool hasNeighbor(int x, int y, int distance, int count)
        {
            var startX = x-distance;
            var endX = x+distance;
            var startY = y-distance;
            var endY = y+distance;
            for (int i = startX; i < endX; i++)
            {
                if(i<0 || i>=15) continue;
                for (int j = startY; j < endY; j++)
                {
                    if(j<0 || j>=15) continue;
                    if(i == x && j == y) continue;
                    if(board[i,j] != (int)AiConfig.player.empty)
                    {
                        count--;
                        if(count <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public int[] gen(int role)
        {
            if (count <= 0)
            {
                return new int[] { 7, 7 };
            }
            int[] fives;
            int[] comfours;
            int[] humfours;
            int[] comblockedfours;
            int[] humblockedfours;
            int[] comtwothrees;
            int[] humtwothrees;
            int[] comthrees;
            int[] humthrees;
            int[] comtwos;
            int[] humtwos;
            int[] neighbors;

            var reverseRole = role == 1 ? 2 : 1;
            // 找到双方的最后进攻点
            int[] attackPoints;
            int[] defendPoints;

            // 默认情况下 我们遍历整个棋盘。但是在开启star模式下，我们遍历的范围就会小很多
            // 只需要遍历以两个点为中心正方形。
            // 注意除非专门处理重叠区域，否则不要把两个正方形分开算，因为一般情况下这两个正方形会有相当大的重叠面积，别重复计算了
            if (AiConfig.starspread)
            {
                // var i = this.currentSteps.length - 1
                // while(i >= 0) {
                //   var p = this.currentSteps[i]
                //   if (reverseRole === R.com && p.scoreCom >= S.THREE
                //     || reverseRole === R.hum && p.scoreHum >= S.THREE) {
                //     defendPoints.push(p)
                //     break
                //   }
                //   i -= 2
                // }

                // var i = this.currentSteps.length - 2
                // while(i >= 0) {
                //   var p = this.currentSteps[i]
                //   if (role === R.com && p.scoreCom >= S.THREE
                //     || role === R.hum && p.scoreHum >= S.THREE) {
                //     attackPoints.push(p)
                //     break;
                //   }
                //   i -= 2
                // }
                if (attackPoints.Length != 0)
                {
                    // attackPoints.push(this.currentSteps[0].role === role ? this.currentSteps[0] : this.currentSteps[1])
                }
                if (defendPoints.Length != 0)
                {
                    // defendPoints.push(this.currentSteps[0].role === reverseRole? this.currentSteps[0] : this.currentSteps[1])
                }

                for (int i = 0; i < board.Length; i++)
                {

                }
            }
        }
    }
}
