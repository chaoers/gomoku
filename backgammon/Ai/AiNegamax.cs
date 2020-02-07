using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiNegamax
    {
        private static int MAX = (int)AiConfig.score.five*10;
        private static int MIN = -1*MAX;
        private int count = 0;
        private int PVcut;
        private int ABcut;
        private int cacheCount = 0;
        private int cacheGet = 0;

        public void deepAll(int role, int deep)
        {
            
        }
    }
}
