using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Chromameter
{
    class ChromaResult : Measurement
    {
        private string _sampleID;
        public ChromaResult()
        {
        }

        public ChromaResult(string i_sampleID, int i_numOfLeds, double i_concentration, double[] i_detectorReading, double i_absorption, int i_repetition)
            : base(i_numOfLeds, i_concentration, i_detectorReading, i_absorption, i_repetition)
        {
            SampleID = i_sampleID;
        }

        public string SampleID
        {
            get { return _sampleID; }
            set { _sampleID = value; }
        }

    }
}
