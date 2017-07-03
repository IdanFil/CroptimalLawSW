using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CroptimalLabSW.Model
{
    class ChromaCalibrationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _avgNum;
        private double _bgReading;
        private int _repetitions;
        private double _concentration;
        private int _polynomial;

        public int Polynomial
        {
            get { return _polynomial; }
            set { _polynomial = value; }
        }


        public double Concentration
        {
            get { return _concentration; }
            set { _concentration = value; }
        }


        public int Repetitions
        {
            get { return _repetitions; }
            set { _repetitions = value; }
        }


        public double BGReading
        {
            get { return _bgReading; }
            set { _bgReading = value; }
        }


        public int AVGNum
        {
            get { return _avgNum; }
            set { _avgNum = value; }
        }


        public ChromaCalibrationModel()
        {

        }

        private void RaisePropertyChanged(string i_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(i_propertyName));
            }
        }
    }
}
