using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;       //List

namespace RailroProject
{
    class Algorithm
    {
        Data OD;
        List<List<int>> localFlow;
        List<int> lineIndex;
        int numberOfNode;

        //constructor
        public Algorithm(Data OD)
        {
            //data initialize
            this.OD = new Data(OD);
            numberOfNode = Data.Node.size();
            //flow initialize
            localFlow = new List<List<int>>();
            for (int i = 0; i < Data.Node.size(); i++)
            {
                List<int> tempList = new List<int>();
                for (int j = 0; j < Data.Node.size(); j++)
                {
                    tempList.Add(OD.flow[i, j]);
                }
                this.localFlow.Add(tempList);
            }
            //lineIndex initialize
            lineIndex = new List<int>();
            for (int i = 0; i < Data.Node.size(); i++)
            {
                this.lineIndex.Add(i);
            }
        }
        
        //deletes nodes with given indexes. Input is a list of indexes to be deleted.
        public void deleteNode(List<int> nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                for (int j = 0; j < lineIndex.Count; j++)
                {
                    if (lineIndex[j] == nodeList[i])
                    {
                        localFlow.RemoveAt(j);
                        for (int k = 0; k < localFlow.Count; k++)
                        {
                            localFlow[k].RemoveAt(j);
                        }
                        lineIndex.RemoveAt(j);
                        break;
                    }
                }
            }
            numberOfNode -= nodeList.Count;
        }


        //Stoer-Wagner Algorithm. 
        //reference : http://www.ahmedshamsularefin.id.au/acm-icpc/code-samples/18-c-c/174-stoer-wagner-min-cut-algorithm
        public List<int> GetMinCut()
        {
            int N = numberOfNode;
            List<int> cut = new List<int>();
            List<int> best_cut = new List<int>();
            List<int> used = new List<int>();
            int best_weight = -1;
            int[,] flow = new int[N, N];

            //initialize flow
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    flow[i, j] = localFlow[i][j];
                }
            }

            for (int i = 0; i < N; i++) { used.Add(0); }

            for (int phase = N - 1; phase >= 0; phase--)
            {
                int[] w = new int[N];
                for (int m = 0; m < N; m++) { w[m] = flow[0, m]; }
                List<int> added = new List<int>(used);
                int prev, last = 0;

                for (int i = 0; i < phase; i++)
                {
                    prev = last;
                    last = -1;
                    for (int j = 1; j < N; j++)
                    {
                        if (added[j] == 0 && (last == -1 || w[j] > w[last]))
                        {
                            last = j;
                         }
                    }
                    if (i == phase - 1)
                    {
                        for (int j = 0; j < N; j++) flow[prev, j] += flow[last, j];
                        for (int j = 0; j < N; j++) flow[j, prev] = flow[prev, j];
                        used[last] = 1;
                        cut.Add(lineIndex[last]);
                        //cut.Add(last);
                        if (best_weight == -1 || w[last] < best_weight)
                        {
                            best_cut = new List<int>(cut);
                            best_weight = w[last];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < N; j++)
                        {
                            w[j] += flow[last, j];
                        }
                        added[last] = 1;
                    }
                }
            }
            return best_cut; 
        }

    }
}
