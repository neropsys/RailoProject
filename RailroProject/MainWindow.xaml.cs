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
        private List<DataVertex> stations;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Data Structure";
            ZoomControl.SetViewFinderVisibility(zoomctrl, Visibility.Visible);
            zoomctrl.ZoomToFill();

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
            DataReader stationReader = new DataReader("2line.txt");
            stations = stationReader.getStationNode();

            GraphArea_Setup();
            Area.GenerateGraph(true, true);
            zoomctrl.TranslateX = stations.Last<DataVertex>().location.longitude;
            zoomctrl.TranslateY = stations.Last<DataVertex>().location.latitude;
        }
        private DataGraph Graph_Setup()
        {
            DataGraph graph = new DataGraph();
            foreach (DataVertex station in stations)
            {
                graph.AddVertex(station);
            }
            return graph;
            
        }
        private void GraphArea_Setup()
        {
            var logicCore = new GraphLogicCore() { Graph = Graph_Setup() };
            var layoutAlgorithm = new VertexLayoutAlgorithm(logicCore.Graph);
            //logicCore.ExternalLayoutAlgorithm = layoutAlgorithm;
            //logicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Custom;
            //logicCore.DefaultLayoutAlgorithmParams = logicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            //((KKLayoutParameters)logicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;
            logicCore.ExternalLayoutAlgorithm = layoutAlgorithm;

            logicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;

            logicCore.DefaultOverlapRemovalAlgorithmParams.HorizontalGap = 80;
            logicCore.DefaultOverlapRemovalAlgorithmParams.VerticalGap = 80;
            logicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;
            logicCore.AsyncAlgorithmCompute = false;
            Area.LogicCore = logicCore;
            foreach (var vertex in Area.VertexList)
            {
                vertex.Value.SetPosition(vertex.Key.location.longitude, vertex.Key.location.latitude, false);

            }
            Area.RelayoutGraph(true);
        }
       

    }
}
