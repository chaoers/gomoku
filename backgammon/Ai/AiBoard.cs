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
        private int[][][,] scoreCache = new int[3][][,] { new int[4][,] { new int[15, 15], new int[15, 15], new int[15, 15], new int[15, 15] }, new int[4][,] { new int[15, 15], new int[15, 15], new int[15, 15], new int[15, 15] }, new int[4][,] { new int[15, 15], new int[15, 15], new int[15, 15], new int[15, 15] } };

        // 储存双方得分
        private int[,] comScore = new int[15, 15];
        private int[,] humScore = new int[15, 15];

        // 传入棋子矩阵和位数
        public AiBoard(int[,] _board, int size)
        {
            board = _board;
            statistic.init(size);
            initScore();
        }

        private void initScore()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (board[i, j] == (int)AiConfig.player.empty)
                    {
                        if (hasNeighbor(i, j, 2, 2)) //必须是有邻居的才行
                        {
                            var cs = scorePoint(i, j, (int)AiConfig.player.com, -1);
                            var hs = scorePoint(i, j, (int)AiConfig.player.hum, -1);
                            comScore[i, j] = cs;
                            humScore[i, j] = hs;
                        }
                    }
                    else if (board[i, j] == (int)AiConfig.player.com) // 对电脑打分，玩家此位置分数为0
                    {
                        comScore[i, j] = scorePoint(i, j, (int)AiConfig.player.com, -1);
                        humScore[i, j] = 0;
                    }
                    else if (board[i, j] == (int)AiConfig.player.hum) // 对玩家打分，电脑位置分数为0
                    {
                        humScore[i, j] = scorePoint(i, j, (int)AiConfig.player.hum, -1);
                        comScore[i, j] = 0;
                    }
                }
            }
        }

        private bool hasNeighbor(int x, int y, int distance, int count)
        {
            var startX = x - distance;
            var endX = x + distance;
            var startY = y - distance;
            var endY = y + distance;
            for (int i = startX; i < endX; i++)
            {
                if (i < 0 || i >= 15) continue;
                for (int j = startY; j < endY; j++)
                {
                    if (j < 0 || j >= 15) continue;
                    if (i == x && j == y) continue;
                    if (board[i, j] != (int)AiConfig.player.empty)
                    {
                        count--;
                        if (count <= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /*
        * 启发式评价函数
        * 这个是专门给某一个位置打分的，不是给整个棋盘打分的
        * 并且是只给某一个角色打分
        */
        private int scorePoint(int px, int py, int role, int dir)
        /*
        * 表示在当前位置下一个棋子后的分数
        * 为了性能考虑，增加了一个dir参数，如果没有传入则默认计算所有四个方向，如果传入值，则只计算其中一个方向的值
        */
        {
            var result = 0;
            // var radius = 8;
            var empty = 0;
            var count = 0;
            var block = 0;
            var secondCount = 0;

            void reset()
            {
                count = 1;
                block = 0;
                empty = -1;
                secondCount = 0;
            }

            if (dir == 0 || dir == -1)
            {
                reset();
                for (int i = py + 1; true; i++)
                {
                    if (i >= 15)
                    {
                        block++;
                        break;
                    }
                    var t = board[px, i];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && i < 15 - 1 && board[px, i + 1] == role)
                        {
                            empty = count;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                for (int i = py - 1; true; i--)
                {
                    if (i < 0)
                    {
                        block++;
                        break;
                    }
                    var t = board[px, i];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && i > 0 && board[px, i - 1] == role)
                        {
                            empty = 0;  //注意这里是0，因为是从右往左走的
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        secondCount++;
                        if (empty != -1) empty++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                count += secondCount;

                scoreCache[role][0][px, py] = countToScore(count, block, empty);
            }
            result += scoreCache[role][0][px, py];

            if (dir == 1 || dir == -1)
            {
                reset();
                for (int i = px + 1; true; i++)
                {
                    if (i >= 15)
                    {
                        block++;
                        break;
                    }
                    var t = board[i, py];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && i < 15 - 1 && board[i + 1, py] == role)
                        {
                            empty = count;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                for (int i = px - 1; true; i--)
                {
                    if (i < 0)
                    {
                        block++;
                        break;
                    }
                    var t = board[i, py];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && i > 0 && board[i - 1, py] == role)
                        {
                            empty = 0;  //注意这里是0，因为是从右往左走的
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        secondCount++;
                        if (empty != -1) empty++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                count += secondCount;

                scoreCache[role][1][px, py] = countToScore(count, block, empty);
            }
            result += scoreCache[role][1][px, py];

            if (dir == 2 || dir == -1)
            {
                reset();
                for (int i = 1; true; i++)
                {
                    var x = px + i;
                    var y = py + i;
                    if (x >= 15 || y >= 15)
                    {
                        block++;
                        break;
                    }
                    var t = board[x, y];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && (x < 15 - 1 && y < 15 - 1) && board[x + 1, y + 1] == role)
                        {
                            empty = count;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                for (int i = 1; true; i++)
                {
                    var x = px - i;
                    var y = py - i;
                    if (x < 0 || y < 0)
                    {
                        block++;
                        break;
                    }
                    var t = board[x, y];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && (x > 0 && y > 0) && board[x - 1, y - 1] == role)
                        {
                            empty = 0;  //注意这里是0，因为是从右往左走的
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        secondCount++;
                        if (empty != -1) empty++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                count += secondCount;

                scoreCache[role][2][px, py] = countToScore(count, block, empty);
            }
            result += scoreCache[role][2][px, py];

            if (dir == 3 || dir == -1)
            {
                reset();
                for (int i = 1; true; i++)
                {
                    var x = px + i;
                    var y = py - i;
                    if (x < 0 || y < 0 || x >= 15 || y >= 15)
                    {
                        block++;
                        break;
                    }
                    var t = board[x, y];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && (x < 15 - 1 && y < 15 - 1) && board[x + 1, y - 1] == role)
                        {
                            empty = count;
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                for (int i = 1; true; i++)
                {
                    var x = px - i;
                    var y = py + i;
                    if (x < 0 || y < 0 || x >= 15 || y >= 15)
                    {
                        block++;
                        break;
                    }
                    var t = board[x, y];
                    if (t == (int)AiConfig.player.empty)
                    {
                        if (empty == -1 && (x > 0 && y > 0) && board[x - 1, y + 1] == role)
                        {
                            empty = 0;  //注意这里是0，因为是从右往左走的
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (t == role)
                    {
                        secondCount++;
                        if (empty != -1) empty++;
                        continue;
                    }
                    else
                    {
                        block++;
                        break;
                    }
                }

                count += secondCount;

                scoreCache[role][3][px, py] = countToScore(count, block, empty);
            }
            result += scoreCache[role][3][px, py];

            return result;
        }

        private int countToScore(int count, int block, int empty)
        {
            if (empty <= 0)
            {
                if (count >= 5) return (int)AiConfig.score.five;
                if (block == 0)
                {
                    switch (count)
                    {
                        case 1: return (int)AiConfig.score.one;
                        case 2: return (int)AiConfig.score.two;
                        case 3: return (int)AiConfig.score.three;
                        case 4: return (int)AiConfig.score.four;
                    }
                }
                if (block == 1)
                {
                    switch (count)
                    {
                        case 1: return (int)AiConfig.score.block_one;
                        case 2: return (int)AiConfig.score.block_two;
                        case 3: return (int)AiConfig.score.block_three;
                        case 4: return (int)AiConfig.score.block_four;
                    }
                }
            }
            else if (empty == 1 || empty == count - 1)
            {
                if (count >= 6) return (int)AiConfig.score.five;
                if (block == 0)
                {
                    switch (count)
                    {
                        case 2: return (int)AiConfig.score.two / 2;
                        case 3: return (int)AiConfig.score.three;
                        case 4: return (int)AiConfig.score.block_four;
                        case 5: return (int)AiConfig.score.four;
                    }
                }
                if (block == 1)
                {
                    switch (count)
                    {
                        case 2: return (int)AiConfig.score.block_two;
                        case 3: return (int)AiConfig.score.block_three;
                        case 4: return (int)AiConfig.score.block_four;
                        case 5: return (int)AiConfig.score.block_four;
                    }
                }
            }
            else if (empty == 2 || empty == count - 2)
            {
                if (count >= 7) return (int)AiConfig.score.five;
                if (block == 0)
                {
                    switch (count)
                    {
                        case 3: return (int)AiConfig.score.three;
                        case 4:
                        case 5: return (int)AiConfig.score.block_four;
                        case 6: return (int)AiConfig.score.four;
                    }
                }
                if (block == 1)
                {
                    switch (count)
                    {
                        case 3: return (int)AiConfig.score.block_three;
                        case 4: return (int)AiConfig.score.block_four;
                        case 5: return (int)AiConfig.score.block_four;
                        case 6: return (int)AiConfig.score.four;
                    }
                }
                if (block == 2)
                {
                    switch (count)
                    {
                        case 4:
                        case 5:
                        case 6: return (int)AiConfig.score.block_four;
                    }
                }
            }
            else if (empty == 3 || empty == count - 3)
            {
                if (count >= 8) return (int)AiConfig.score.five;
                if (block == 0)
                {
                    switch (count)
                    {
                        case 4:
                        case 5: return (int)AiConfig.score.three;
                        case 6: return (int)AiConfig.score.block_four;
                        case 7: return (int)AiConfig.score.four;
                    }
                }
                if (block == 1)
                {
                    switch (count)
                    {
                        case 4:
                        case 5:
                        case 6: return (int)AiConfig.score.block_four;
                        case 7: return (int)AiConfig.score.four;
                    }
                }
                if (block == 1)
                {
                    switch (count)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 7: return (int)AiConfig.score.block_four;
                    }
                }
            }
            else if (empty == 4 || empty == count - 4)
            {
                if (count >= 9) return (int)AiConfig.score.five;
                if (block == 0)
                {
                    switch (count)
                    {
                        case 5:
                        case 6:
                        case 7:
                        case 8: return (int)AiConfig.score.four;
                    }
                }
                if (block == 1)
                {
                    switch (count)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 7: return (int)AiConfig.score.block_four;
                        case 8: return (int)AiConfig.score.four;
                    }
                }
                if (block == 2)
                {
                    switch (count)
                    {
                        case 5:
                        case 6:
                        case 7:
                        case 8: return (int)AiConfig.score.block_four;
                    }
                }
            }
            else if (empty == 4 || empty == count - 4)
            {
                return (int)AiConfig.score.five;
            }

            return 0;
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
