using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Chromameter
{
    class Measurment
    {
        private double _concentration;
        private double _detectorReading;
        private double _absorption;
        private int _repetition;

        public double Concentration
        {
            get { return _concentration; }
            set { _concentration = value; }
        }

        public double DetectorReading
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
