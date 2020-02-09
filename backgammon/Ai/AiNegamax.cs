using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiNegamax
    {
        private AiBoard board;
        private static int MAX = (int)AiConfig.score.five * 10;
        private static int MIN = -1 * MAX;
        private int count = 0;
        private int PVcut;
        private int ABcut;
        private int cacheCount = 0;
        private int cacheGet = 0;

        private List<int> Cache;

        AiNegamax(AiBoard _board)
        {
            board = _board;
        }

        public void deepAll(int role, int deep)
        {
            var candidates = board.gen(role);
        }

        private void deeping(List<AiBoard.Point> candidates, int role, int deep)
        {
            var startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds;
            Cache.Clear(); // 每次开始迭代的时候清空缓存。这里缓存的主要目的是在每一次的时候加快搜索，而不是长期存储。事实证明这样的清空方式对搜索速度的影响非常小（小于10%)

            var bestScore;
            for (int i = 2; i <= deep; i += 2)
            {
                bestScore = negamax();
            }
        }

        private void negamax(List<AiBoard.Point> candidates, int role, int deep, int alpha, int beta)
        {
            count = 0;
            ABcut = 0;
            PVcut = 0;
            board.currentSteps.Clear();

            for (int i = 0; i < candidates.Count; i++)
            {
                var p = candidates[i];
            }
        }
    }
}
