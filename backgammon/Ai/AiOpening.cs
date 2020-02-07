using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class AiOpening
    {
        public int[] match(AiBoard board)
        {
            var s = board.allSteps;
            // if (board.board[s[0][0]][s[0][1]] !== 1) return false
            if (s.Length > 2) return new int[] {-1,-1};
            // if (math.containPoint([[6,7],[7,6],[8,7],[7,8]], s[1])) return huayue(board)
            // else if (math.containPoint([[6,6],[8,8],[8,6],[6,8]], s[1])) return puyue(board)
            return new int[] {-1,-1};
        }
    }
}
