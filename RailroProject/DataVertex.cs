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
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
        public DataVertex() : this("") { }
        public DataVertex(string text = "")
        {
            Text = text;
        }
    }
}
