using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Chromameter
{
    public class Measurement
    {
        private int _numOfLeds;
        private double _concentration;
        private double[] _detectorReading;
        private double _absorption;
        private int _repetition;

        public Measurement()
        {
        }

        public Measurement(int i_numOfLeds, double i_concentration, double[] i_detectorReading, double i_absorption, int i_repetition)
        {
            NumOfLeds = i_numOfLeds;
            Concentration = i_concentration;
            DetectorReading = i_detectorReading;
            Absorption = i_absorption;
            Repetition = i_repetition;
        }

        public int NumOfLeds
        {
            get { return _numOfLeds; }
            set { _numOfLeds = value; }
        }

        public double Concentration
        {
            get { return _concentration; }
            set { _concentration = value; }
        }

        public double[] DetectorReading
        {
            get { return _detectorReading; }
            set { _detectorReading = value; }
        }

        public double Absorption
        {
            get { return _absorption; }
            set { _absorption = value; }
        }

        public int Repetition
        {
            get { return _repetition; }
            set { _repetition = value; }
        }


    }
}
