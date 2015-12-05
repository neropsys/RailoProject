﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace RailroProject
{
    class DataReader
    {
        public DataReader(string fileName)
        {
            this.fileName = fileName;
        }
        private const float originLong = 37.368345f;
        private const float originLat = 126.648967f;
        public List<DataVertex> getStationNode()
        {
            List<DataVertex> ret = new List<DataVertex>();
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string path = System.IO.Directory.GetParent(projectPath.ToString()).FullName + "/";
            string buf;
            path = path + fileName;
            System.IO.StreamReader file = new System.IO.StreamReader(path, Encoding.UTF8);
            if (!File.Exists(path))
            {
                MessageBox.Show("Error reading file :" + fileName);
                Application.Current.Shutdown();
            }
            while ((buf = file.ReadLine()) != null)
            {
                string[] data = buf.Split(' ');
                ret.Add(new DataVertex(data[0], (float.Parse(data[1])-originLong)*1000, -(float.Parse(data[2])-originLat)*1000));
            }
            ret.Add(new DataVertex("0, 0 in screen", 0, 0));
            file.Close();
            return ret;
        }

        private string fileName;
    }
}
