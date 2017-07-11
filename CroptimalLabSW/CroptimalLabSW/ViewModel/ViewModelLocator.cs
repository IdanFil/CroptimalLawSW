/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CroptimalLabSW"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace CroptimalLabSW.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        private BottomMenuViewModel m_bottomMenuViewModel;

        public BottomMenuViewModel BottomMenuViewModel
        {
            get
            {
                if (m_bottomMenuViewModel == null)
                {
                    m_bottomMenuViewModel = new BottomMenuViewModel();
                }
                return m_bottomMenuViewModel;
            }
        }

        private MenuViewModel m_menuViewModel;

        public MenuViewModel MenuViewModel
        {
            get
            {
                if (m_menuViewModel == null)
                {
                    m_menuViewModel = new MenuViewModel();
                }
                return m_menuViewModel;
            }
        }

        private MainViewModel m_mainViewModel;

        public MainViewModel MainViewModel
        {
            get
            {
                if (m_mainViewModel == null)
                {
                    m_mainViewModel = new MainViewModel();
                }
                return m_mainViewModel;
            }
        }

        private DisplayViewModel m_displayViewModel;

        public DisplayViewModel DisplayViewModel
        {
            get
            {
                if (m_displayViewModel == null)
                {
                    m_displayViewModel = new DisplayViewModel();
                }
                return m_displayViewModel;
            }
        }
        private ChromaCalibrationViewModel m_chromaCalibrationViewModel;

        public ChromaCalibrationViewModel ChromaCalibrationViewModel
        {
            get
            {
                if (m_chromaCalibrationViewModel == null)
                {
                    m_chromaCalibrationViewModel = new ChromaCalibrationViewModel();
                }
                return m_chromaCalibrationViewModel;
            }
        }

        private ChromaConfigurationViewModel m_chromaConfigurationViewModel;

        public ChromaConfigurationViewModel ChromaConfigurationViewModel
        {
            get
            {
                if (m_chromaConfigurationViewModel == null)
                {
                    m_chromaConfigurationViewModel = new ChromaConfigurationViewModel();
                }
                return m_chromaConfigurationViewModel;
            }
        }

    }
}