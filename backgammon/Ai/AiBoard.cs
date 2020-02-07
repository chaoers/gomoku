using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiBoard
    {
        class Step {
            private int role;
        }
        private Step[] currentSteps = new Step[0];
        private Step[] allSteps = new Step[0];
        private int count = 0;
        private int total = 0;
        
        public int[,] board;

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
            }
        }
    }
}
