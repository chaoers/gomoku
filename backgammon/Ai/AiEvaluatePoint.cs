using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon.Ai
{
    static class AiEvaluatePoint
    {
        static void scorePoint(int b, int px, int py, int role, int dir)
        {
            var result = 0;
            var radius = 8;
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

            if(dir == 0)
            {
                reset();
                for (int i = py+1;true; i++)
                {
                    if(i >= 15)
                    {
                        block++;
                        break;
                    }
                }
            }

        }
    }
}
