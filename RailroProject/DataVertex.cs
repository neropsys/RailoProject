using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphX;
using GraphX.PCL.Common.Models;

namespace RailroProject
{
    class DataVertex : VertexBase
    {
        public struct Location
        {
            public float longitude;
            public float latitude;
        }
        public Location location;
        public string StationName { get; set; }
        public override string ToString()
        {
            return StationName;
        }
        public DataVertex() : this("") { }
        public DataVertex(string text = "")
        {
            StationName = text;
        }
        public DataVertex(string text, float longitude, float latitude)
        {
            StationName = text;
            location.latitude = latitude;
            location.longitude = longitude;
            
        }
    }
}
