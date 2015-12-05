using GraphX;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Controls;

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
            ZoomControl.SetViewFinderVisibility(zoomctrl, Visibility.Visible);
            zoomctrl.ZoomToFill();
            
            
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
            }
        }
       

    }
}
