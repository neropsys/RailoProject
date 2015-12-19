using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace RailroProject
{
    //This class analyses data. Member variables can be added.
    class DataAnalyzer
    {

        //compute similartiy of flow[x,y] and flow[y,x] and write it to a file.
        public void analyseSimilarity(Data data)
        {
            Encoding encode = System.Text.Encoding.GetEncoding("ks_c_5601-1987");
            Stream s = File.OpenWrite("dds.txt");
            using (StreamWriter sr = new StreamWriter(s, encode))
            {
                for (int i = 0; i < Data.Node.size(); i++)
                {
                    for (int j = i + 1; j < Data.Node.size(); j++)
                    {
                        double similarity;
                        if (data.get(i, j) < data.get(j, i))
                        {
                            similarity = (double)data.get(i, j) / (double)data.get(j, i);
                        }
                        else
                        {
                            similarity = (double)data.get(j, i) / (double)data.get(i, j);
                        }
                        sr.WriteLine(data.nodeLineGet(i) + "\t" + data.nodeLineGet(j) + "\t" + similarity);
                    }
                }
            }
        }
    }
}
