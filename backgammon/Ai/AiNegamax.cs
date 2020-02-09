using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiNegamax
    {
        class Step
        {
            public int score = 0;
            public int step = 0;
            public List<AiBoard.Point> steps = new List<AiBoard.Point>();
        }
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
                board.put(p, role);
                var step = new List<AiBoard.Point> { p };

            }
        }

        private AiBoard.Point r(int deep, int alpha, int beta, int role, int step, List<AiBoard.Point> steps, int spread)
        {
            // if(config.cache) {
            //   var c = Cache[board.zobrist.code]
            //   if(c) {
            //     if(c.deep >= deep) { // 如果缓存中的结果搜索深度不比当前小，则结果完全可用
            //       cacheGet ++
            //       // 记得clone，因为这个分数会在搜索过程中被修改，会使缓存中的值不正确
            //       return {
            //         score: c.score.score,
            //         steps: steps,
            //         step: step + c.score.step,
            //         c: c
            //       }
            //     } else {
            //       // 如果缓存的结果中搜索深度比当前小，那么任何一方出现双三及以上结果的情况下可用
            //       // TODO: 只有这一个缓存策略是会导致开启缓存后会和以前的结果有一点点区别的，其他几种都是透明的缓存策略
            //       if (math.greatOrEqualThan(c.score, SCORE.FOUR) || math.littleOrEqualThan(c.score, -SCORE.FOUR)) {
            //         cacheGet ++
            //         return c.score
            //       }
            //     }
            //   }
            // }

            var _e = board.evaluate(role);

            var leaf = new Step();
            leaf.score = _e;
            leaf.step = step;
            leaf.steps = steps;

            count++;
            // 搜索到底 或者已经胜利
            // 注意这里是小于0，而不是1，因为本次直接返回结果并没有下一步棋
            if(deep <= 0 || )
        }
    }
}
