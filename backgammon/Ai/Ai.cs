using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class Ai
    {
        public static AiConfig aiConfig = new AiConfig();
        
        public static AiBoard aiBoard = new AiBoard();
        public AiOpening aiOpening = new AiOpening();

        public int[] begin(){
            var p = new int[] {-1,-1};
            if(aiBoard.allSteps.Length > 1)
            {
                p = aiOpening.match(aiBoard);
            }
            return p;
        }
    }
}
