using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Chromameter
{
    class ChromaCalculator
    {
        public double[] calculateABS(double[] i_backgrounAVG, double[] i_measureAVG)
        {
            double[] AbsArr = new double[i_backgrounAVG.Length];
            for(int i = 0; i < i_backgrounAVG.Length; i++)
            {
                if(i_backgrounAVG[i] != 0)
                {
                    AbsArr[i] = calculateABS(i_backgrounAVG[i] ,i_measureAVG[i]);
                }
            }
            return AbsArr;
        }

        public double calculateABS(double i_backgrounAVG, double i_measureAVG)
        {
            return -(Math.Log10(i_backgrounAVG / i_measureAVG));
        }

        public double calculateABS_NNO3(double[] i_backgrounAVG, double[] i_measureAVG)
        {
            double[] absArr = calculateABS(i_backgrounAVG, i_measureAVG);
            double absA = 0, absB = 0;
            double countAbs = 0;
            for(int i = 0; i < absArr.Length; i++)
            {
                if(absArr[i] != 0)
                {
                    if(countAbs == 0)
                    {
                        absA = absArr[i];
                        countAbs++;
                    }
                    else
                    {
                        absB = absArr[i];
                    }
                }
            }
            return absA - 2 * absB;
        }

        public double calculateABS_NNO3(double i_ABS_A, double i_ABS_B)
        {
            return i_ABS_A - 2 * i_ABS_B;
        }
    }
}
