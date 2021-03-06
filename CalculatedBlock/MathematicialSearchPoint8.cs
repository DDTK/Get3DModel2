﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedBlock
{
    public class MathematicialSearchPoint8 : MathematicialSearchPoint
    {
        public MathematicialSearchPoint8()
        {
            double[,] currentX_Matrix = { { 0, 0, 1, 0, 0 }, { 0, 0, 2, 0, 0 }, { 1, 2, -12, 2, 1 },
                                          { 0, 0, 2, 0, 0 }, { 0, 0, 1, 0, 0 } };
            double[,] currentY_Matrix = { { 1, 0, 0, 0, 1 }, { 0, 2, 0, 2, 0 }, { 0, 0, -12, 0, 0 },
                                          { 0, 2, 0, 2, 0 }, { 1, 0, 0, 0, 1 } };
            xMatrix = currentX_Matrix;
            yMatrix = currentY_Matrix;
        }

        public override double gradientAtPoint(int x, int y)
        {
            double[,] core = getCore5x5(x, y);
            return Gradient(core);
        }

        public override double calculation(double xConvolution, double yConvolution)
        {
            return (xConvolution + yConvolution) / 2;
        }
    }
}
