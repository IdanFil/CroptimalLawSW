using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CroptimalLabSW.View
{
    /// <summary>
    /// Interaction logic for ChromaCalibrationView.xaml
    /// </summary>
    public partial class ChromaCalibrationView : UserControl
    {
        public ChromaCalibrationView()
        {
            //if (Plot1 != null)
            //{
            //    Plot1.Model = null;
            //}
            InitializeComponent();


        }



        private void PositiveNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void doubleValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            char ch = e.Text[0];
            string textBefore = ((TextBox)sender).Text;

            if (Char.IsDigit(ch) || ch == '-' || ch == '.')
            {
                if (ch == '-' && textBefore.Length != 0)
                {
                    e.Handled = true;
                }
                else if (ch == '.' && (textBefore.Length == 0 || textBefore.IndexOf('.') != -1 || textBefore == "-"))
                {
                    e.Handled = true;
                }
                return;
            }
            e.Handled = true;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        Plot1.Model = null;
        //    }

        //    base.Dispose(disposing);
        //}
    }
}
