using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using MahApps.Metro.Controls;
using System.Threading;

namespace CroptimalLabSW.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        private double _marginThickness;

        public MenuView()
        {
            InitializeComponent();
            _marginThickness = btnSettings.Margin.Bottom;
        }

        private void mouseEnter(object sender, MouseEventArgs e)
        {
            Tile senderTile = (Tile)sender;

            Thread tileAnimationThread = new Thread(() => {
                for (int i = 1; i <= 5; i++)
                {
                    senderTile.Dispatcher.Invoke(() =>
                    {
                        senderTile.Margin = new Thickness(_marginThickness - i, _marginThickness - i, _marginThickness - i, _marginThickness - i);
                    });
                    Thread.Sleep(30);
                }
            });

            tileAnimationThread.Start();
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            Tile senderTile = (Tile)sender;

            senderTile.Margin = new Thickness(_marginThickness, _marginThickness, _marginThickness, _marginThickness); ;
        }
    }
}
