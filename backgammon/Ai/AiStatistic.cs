using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon.Ai
{
    class AiStatistic
    {
        private int[,] table;
        public void init(int size)
        {
            table = new int[size,size];
        }
    }
}
