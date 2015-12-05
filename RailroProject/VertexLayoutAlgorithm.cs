using System.Collections.Generic;
using System.Threading;
using GraphX.Measure;
using GraphX.PCL.Common.Interfaces;
using GraphX.PCL.Logic.Algorithms.LayoutAlgorithms;
using QuickGraph;

namespace RailroProject
{
    class VertexLayoutAlgorithm : IExternalLayout<DataVertex>
    {
        
        private readonly IVertexAndEdgeListGraph<DataVertex, DataEdge> _graph;

        IDictionary<DataVertex, Point> _vertexPositions = new Dictionary<DataVertex, Point>();

        public IDictionary<DataVertex, Size> VertexSizes { get; set; }

        public IDictionary<DataVertex, System.Windows.Point> VertexPositions
        {
            get;
            set;
        }

        public VertexLayoutAlgorithm(IVertexAndEdgeListGraph<DataVertex, DataEdge> graph)
        {
            _graph = graph;
        }
        public void Compute(CancellationToken cancellationToken)
        {
         
            foreach (var vertex in _graph.Vertices)
            {
                _vertexPositions.Add(vertex, new Point(vertex.location.longitude, vertex.location.latitude));

            }
        }



        public bool NeedVertexSizes
        {
            get { return false; }
        }

        public bool SupportsObjectFreeze
        {
            get { return true; }
        }

        IDictionary<DataVertex, Point> IExternalLayout<DataVertex>.VertexPositions
        {
            get { return _vertexPositions; }
        }
        
    }
}
