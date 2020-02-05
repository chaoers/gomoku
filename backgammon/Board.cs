using System;
using System.Collections.Generic;
using System.Text;

namespace backgammon
{
    class Board
    {
        private static readonly int DISTANCE = 40;
        private static readonly int RADIUS = 5;

        public int[] placePosition(double x, double y)
        {
            var position = new int[2];
            position[0] = closeNode(x+30);
            position[1] = closeNode(y+30);
            if(position[0] != -1 && position[1] != -1)
            {
                return position;
            }
            else
            {
                return new int[] {-1,-1};
            }
        }
        public int closeNode(double position)
        {
            if(position%DISTANCE >= RADIUS && position%DISTANCE <= DISTANCE - RADIUS)
            {
                return -1;
            }
            else
            {
                if(position%DISTANCE >= RADIUS)
                {
                    return (int)position / DISTANCE;
                }
                else
                {
                    return (int)position / DISTANCE -1;
                }
            }
        }
    }
}
