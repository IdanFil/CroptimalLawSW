using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CroptimalLabSW.Model.Chromameter
{
    class Calibration
    {
        private string _configurationName;
        private ObservableCollection<int> _configuration;
        private ObservableCollection<Measurement> m_measurments;
        private double[] _backgroundReading;
        private int _polynomicOrder;
        private double _regression;
        private string _elenemtName;
        private double _coefficientA;
        private double _coefficientB;
        private double _coefficientC;

        public Calibration()
        {
            m_measurments = new ObservableCollection<Measurement>();
        }

        public ObservableCollection<Measurement> Measurments
        {
            get { return m_measurments; }
            set { m_measurments = value; }
        }

        public double[] BackgroundReading
        {
            get { return _backgroundReading; }
            set { _backgroundReading = value; }
        }

        public int PolynomicOrder
        {
            get { return _polynomicOrder; }
            set { _polynomicOrder = value; }
        }

        public double Regression
        {
            get { return _regression; }
            set { _regression = value; }
        }

        public string ElenemtName
        {
            get { return _elenemtName; }
            set { _elenemtName = value; }
        }

        public double CoefficientA
        {
            get { return _coefficientA; }
            set { _coefficientA = value; }
        }

        public double CoefficientB
        {
            get { return _coefficientB; }
            set { _coefficientB = value; }
        }

        public double CoefficientC
        {
            get { return _coefficientC; }
            set { _coefficientC = value; }
        }

        public ObservableCollection<int> Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }

        public string ConfigurationName
        {
            get { return _configurationName; }
            set { _configurationName = value; }
        }

        public void addMesurement(Measurement i_measur)
        {
            m_measurments.Add(i_measur);
        }

        public void addMesurement(int i_numOfLeds, double i_concentration, double[] i_detectorReading, double i_absorption, int i_repetition)
        {
            m_measurments.Add(new Measurement(i_numOfLeds, i_concentration, i_detectorReading, i_absorption, i_repetition));
        }

        public void removeMesurement()
        {

        }

        public void setBGReading(int detectorNum, double reading)
        {
            BackgroundReading[detectorNum] = reading;
        }

    }
}
