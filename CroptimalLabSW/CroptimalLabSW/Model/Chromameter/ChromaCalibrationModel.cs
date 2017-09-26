using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CroptimalLabSW.Model.DB;
using System.Threading;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using MathNet.Numerics;

namespace CroptimalLabSW.Model.Chromameter
{
    class ChromaCalibrationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DBOptions m_DBOptions;
        private ChromaManage m_chromaManage;
        private ChromaCalculator m_chromaCalculator;

        private PlotModel _plotModel = null;
        private Calibration m_calibration;
        private int? _avgNum;
        private int? _repetitions;
        private double? _concentration;
        private string _newElementName;
        private string _selectedElement;

        private ObservableCollection<string> _elementsList;
        private ObservableCollection<string> _configurationsList;
        private ObservableCollection<int> _selectedConfParams;
        private ObservableCollection<int> _polynomialOrderOptions;

        private ScatterSeries m_measureSeries;
        private FunctionSeries m_polynomSeries;


        #region Constructor
        public ChromaCalibrationModel()
        {
            m_DBOptions = DBOptions.Instance();
            m_chromaManage = ChromaManage.Instance();
            m_calibration = new Calibration();
            m_chromaCalculator = new ChromaCalculator();
            m_DBOptions.NewChromaConfigureAdded += updateConfigurationsList;
            _elementsList = new ObservableCollection<string>(m_DBOptions.getChromameterElementsName());
            ConfigurationsList = m_DBOptions.getChromameterConfigurationNames();
            PolynomialOrderOptions = new ObservableCollection<int> { 1, 2 };
        }
        #endregion

        #region Property


        public PlotModel PlotModel
        {
            get
            {
                if (_plotModel == null)
                {
                    SetUpModel();
                }
                return _plotModel;
            }
            set
            {
                _plotModel = value;
                RaisePropertyChanged("PlotModel");
            }
        }

        private void initGraph()
        {
            m_measureSeries = new ScatterSeries();
            m_polynomSeries = new FunctionSeries();

            m_measureSeries.MarkerFill = OxyColors.RosyBrown;
            m_measureSeries.MarkerType = MarkerType.Diamond;

            PlotModel.Series.Add(m_measureSeries);
            PlotModel.Series.Add(m_polynomSeries);
        }

        public ObservableCollection<string> ElementsList
        {
            get { return _elementsList; }
            set
            {
                _elementsList = value;
                RaisePropertyChanged("ElementsList");
            }
        }

        public ObservableCollection<string> ConfigurationsList
        {
            get { return _configurationsList; }
            set
            {
                _configurationsList = value;
                RaisePropertyChanged("ConfigurationsList");
            }
        }

        public string SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                _selectedElement = value;
                RaisePropertyChanged("SelectedElement");
            }
        }


        public string NewElementName
        {
            get { return _newElementName; }
            set
            {
                _newElementName = value;
                RaisePropertyChanged("NewElementName");
            }
        }


        public double Regression
        {
            get { return m_calibration.Regression; }
            set
            {
                m_calibration.Regression = value;
                RaisePropertyChanged("Regression");
            }
        }


        public int PolynomialOrder
        {
            get { return m_calibration.PolynomicOrder; }
            set
            {
                m_calibration.PolynomicOrder = value;
                RaisePropertyChanged("PolynomialOrder");
            }
        }

        public ObservableCollection<int> PolynomialOrderOptions
        {
            get { return _polynomialOrderOptions; }
            set
            {
                _polynomialOrderOptions = value;
                RaisePropertyChanged("PolynomialOrderOptions");
            }
        }


        public double? Concentration
        {
            get { return _concentration; }
            set
            {
                _concentration = value;
                RaisePropertyChanged("Concentration");
            }
        }


        public int? Repetitions
        {
            get { return _repetitions; }
            set
            {
                _repetitions = value;
                RaisePropertyChanged("Repetitions");
            }
        }


        public double[] BGReading
        {
            get { return m_calibration.BackgroundReading; }
            set
            {
                m_calibration.BackgroundReading = value;
                RaisePropertyChanged("BGReading");
            }
        }

        public ObservableCollection<Measurement> Measurments
        {
            get { return m_calibration.Measurments; }
            set
            {
                m_calibration.Measurments = value;
                RaisePropertyChanged("Measurments");
            }
        }


        public int? AVGNum
        {
            get { return _avgNum; }
            set
            {
                _avgNum = value;
                RaisePropertyChanged("AVGNum");
            }
        }

        public string SelectedConf
        {
            get { return m_calibration.ConfigurationName; }
            set
            {
                m_calibration.ConfigurationName = value;
                getConfiguration();
                RaisePropertyChanged("SelectedConf");
            }
        }

        public ObservableCollection<int> SelectedConfParams
        {
            get { return _selectedConfParams; }
            set
            {
                _selectedConfParams = value;
                RaisePropertyChanged("SelectedConfParams");
            }
        }

        private void SetUpModel()
        {
            _plotModel = new PlotModel();
            _plotModel.TextColor = OxyColors.SteelBlue;
            var dateAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80, Title = "Abs", Position = AxisPosition.Bottom, AxislineColor = OxyColors.SteelBlue };
            _plotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Concentration", Position = AxisPosition.Left, AxislineColor = OxyColors.SteelBlue };
            _plotModel.Axes.Add(valueAxis);
            initGraph();
        }

        private void toRemove()
        {
            OxyPlot.Series.ScatterSeries punktySerii = new OxyPlot.Series.ScatterSeries();
            punktySerii.MarkerFill = OxyColors.RosyBrown;
            punktySerii.MarkerType = MarkerType.Diamond;
            punktySerii.Points.Add(new OxyPlot.Series.ScatterPoint(1, 1));
            punktySerii.Points.Add(new OxyPlot.Series.ScatterPoint(4, 3));
            punktySerii.Points.Add(new OxyPlot.Series.ScatterPoint(8, 15));
            PlotModel.Series.Add(punktySerii);
            //////////////////////////
            Func<double, double> f1 = (x) => x * x;
            Func<double, double> f2 = (y) => y * y* y;
            FunctionSeries a = new FunctionSeries(f1, f2, -10, 10, 1f);
             PlotModel.Series.Add(a);
        }

        private void printMeasurementsToGraph()
        {
            ScatterSeries punktySerii = new ScatterSeries();
            PlotModel.Series.Clear();
            foreach (Measurement measure in Measurments)
            {
                punktySerii.Points.Add(new OxyPlot.Series.ScatterPoint(measure.Absorption, measure.Concentration));
            }
            PlotModel.Series.Add(punktySerii);
            PlotModel.InvalidatePlot(true);
        }

        public void calculate()
        {
            double[] absArr = new double[Measurments.Count];
            double[] concArr = new double[Measurments.Count];
            for(int i = 0; i < Measurments.Count; i++)
            {
                absArr[i] = Measurments[i].Absorption;
                concArr[i] = Measurments[i].Concentration;
            }
            Regression = GoodnessOfFit.RSquared(absArr, concArr);
            Regression = Math.Round(Regression, 6);
            Func<double,double> func = Fit.PolynomialFunc(absArr, concArr, PolynomialOrder);
            FunctionSeries d = new FunctionSeries(func, absArr.Min(), absArr.Max(), 0.0001f);
            m_polynomSeries.Points.Clear();
            m_polynomSeries.Points.AddRange(d.Points);
            PlotModel.InvalidatePlot(true);
        }

        public void setConf()
        {
            m_chromaManage.setMaskLEDs(getConfLEDs());
        }

        public bool[] getConfLEDs()
        {
            bool[] confLEDs = new bool[SelectedConfParams.Count];
            for (int i = 0; i < SelectedConfParams.Count; i++)
            {
                if (SelectedConfParams[i] > 0)
                {
                    confLEDs[i] = true;
                }
                else
                {
                    confLEDs[i] = false;
                }
            }
            return confLEDs;
        }

        public bool getConfiguration()
        {
            SelectedConfParams = m_DBOptions.getConfiguretion(SelectedConf);
            return SelectedConfParams != null ? true : false;
        }

        public void readBG()
        {
            m_calibration.BackgroundReading = m_chromaManage.measureAVG(SelectedConfParams, (int)AVGNum);
        }

        public void measure()
        {
             int ledsCounter = 0;
            for (int i = 0; i < SelectedConfParams.Count; i++)
            {
                if (SelectedConfParams[i] > 0)
                {
                    ledsCounter++;
                }
            }
            if (ledsCounter == 1)
            {
                oneLEDMeasure();
            }
            else if(ledsCounter == 2)
            {
                twoLEDsMeasure();
            }
        }

        private int findOpenLed()
        {
            for (int i = 0; i < SelectedConfParams.Count; i++)
            {
                if(SelectedConfParams[i] != 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void oneLEDMeasure()
        {
            Measurement measure = new Measurement();
            int openLed = findOpenLed();
            for (int i = 0; i < Repetitions; i++)
            {
                measure.NumOfLeds = 1;
                measure.Concentration = (double)Concentration;
                measure.DetectorReading = m_chromaManage.measureAVG(SelectedConfParams, (int)AVGNum);
                measure.Absorption = m_chromaCalculator.calculateABS(m_calibration.BackgroundReading[openLed], measure.DetectorReading[openLed]);
                measure.Repetition = i + 1;
                m_calibration.addMesurement(measure);
                printMeasurementToGraph(measure);
            }
        }

        private void twoLEDsMeasure()
        {
            Measurement measure = new Measurement();
            for (int i = 0; i < Repetitions; i++)
            {
                measure.NumOfLeds = 2;
                measure.Concentration = (double)Concentration;
                measure.DetectorReading = m_chromaManage.measureAVG(SelectedConfParams, (int)AVGNum);
                measure.Absorption = m_chromaCalculator.calculateABS_NNO3(m_calibration.BackgroundReading, measure.DetectorReading);
                measure.Repetition = i + 1;
                m_calibration.addMesurement(measure);
                printMeasurementToGraph(measure);
            }
        }

        private void printMeasurementToGraph(Measurement i_measure)
        {
            m_measureSeries.Points.Add(new ScatterPoint(i_measure.Absorption, i_measure.Concentration));
            PlotModel.InvalidatePlot(true);
        }

        public void updateConfigurationsList(object sender, EventArgs e)
        {
            ConfigurationsList = m_DBOptions.getChromameterConfigurationNames();
        }

        #endregion
         
        private void RaisePropertyChanged(string i_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(i_propertyName));
            }
        }
    }
}
