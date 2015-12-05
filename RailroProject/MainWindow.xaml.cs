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
using System.Windows.Shapes;

namespace RailroProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Data Structure";
            /*
            //// Data file I/O test code
            Data[] data = new Data[9];
            string txt = @"OD_201301.txt";
            char[] buf = txt.ToCharArray();

            for (int x = 0; x < 9; x++)
            {
                string buf2 = new string(buf);
                data[x] = new Data();
                data[x].make(buf2);
                buf[8]++;
            }*/
        }
    }
}
