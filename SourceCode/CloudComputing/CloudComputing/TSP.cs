using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CloudComputing
{
    class TSP
    {

        private static int INFINITY = Int32.MaxValue;

        public double MinCost(double[,] distance, List<Int32> path)
        {
            Dictionary<Index, Double> minCostDP = new Dictionary<Index, double>();
            Dictionary<Index, Int32> parent = new Dictionary<Index, int>();

            List<HashSet<Int32>> allSets = GenerateCombinations(distance.GetLength(0) - 1);

            foreach (HashSet<Int32> set in allSets)
            {
                for (int currentVertex = 1; currentVertex < distance.GetLength(0); currentVertex++)
                {
                    if (set.Contains(currentVertex))
                        continue;
                    Index index = Index.CreateIndex(currentVertex, set);
                    double minCost = INFINITY;
                    int minPrevVertex = 0;
                    //can't iterate and modify the set at the same time
                    HashSet<Int32> copySet = new HashSet<int>(set);
                    foreach (int prevVertex in set)
                    {
                        double cost = distance[prevVertex, currentVertex] + GetCost(copySet, prevVertex, minCostDP);
                        if (cost < minCost)
                        {
                            minCost = cost;
                            minPrevVertex = prevVertex;
                        }
                    }
                    if (set.Count == 0)
                        minCost = distance[0, currentVertex];

                    minCostDP.Add(index, minCost);
                    parent.Add(index, minPrevVertex);
                }
            }

            HashSet<Int32> set_final = new HashSet<int>();
            for (int i = 1; i < distance.GetLength(0); i++)
            {
                set_final.Add(i);
            }
            double min = Double.MaxValue;
            int prevVertex_final = -1;
            HashSet<Int32> copySet_final = new HashSet<int>(set_final);
            foreach (int k in set_final)
            {
                double cost = distance[k, 0] + GetCost(copySet_final, k, minCostDP);
                if (cost < min)
                {
                    min = cost;
                    prevVertex_final = k;
                }
            }

            parent.Add(Index.CreateIndex(0, set_final), prevVertex_final);
            GeneratePath(parent, distance.GetLength(0), path);
            return min;

        }

        private void GeneratePath(Dictionary<Index, int> parent, int length, List<int> path)
        {

            HashSet<Int32> set = new HashSet<int>();
            for (int i = 0; i < length; i++)
            {
                set.Add(i);
            }
            int start = 0;
            while (set.Count > 0)
            {
                path.Add(start);
                set.Remove(start);
                parent.TryGetValue(Index.CreateIndex(start, set), out start);
            }
            path.Add(0);
            path.Reverse();
        }

        private double GetCost(HashSet<int> copySet, int prevVertex, Dictionary<Index, double> minCostDP)
        {
            double cost = Double.MaxValue;
            copySet.Remove(prevVertex);
            Index index = Index.CreateIndex(prevVertex, copySet);
            minCostDP.TryGetValue(index, out cost);
            copySet.Add(prevVertex);
            return cost;
        }

        public List<HashSet<Int32>> GenerateCombinations(int length)
        {
            List<HashSet<Int32>> allSets = new List<HashSet<Int32>>();
            int[] nums = new int[length];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = i + 1;
            }
            BackTrackingSolution(allSets, nums, new HashSet<Int32>(), 0);
            allSets.Sort((o1, o2) => o1.Count - o2.Count);
            //sort sets based on their size    

            return allSets;

        }

        private void BackTrackingSolution(List<HashSet<int>> allSets, int[] nums, HashSet<int> hashSet, int startIndex)
        {
            HashSet<int> copy = new HashSet<int>(hashSet);
            allSets.Add(copy);

            for (int i = startIndex; i < nums.Length; i++)
            {
                hashSet.Add(nums[i]);
                BackTrackingSolution(allSets, nums, hashSet, i + 1);
                hashSet.Remove(nums[i]);
            }
        }
    }

    class Index
    {
        int currentVertex;
        HashSet<Int32> vertexSet;

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            Index ind = (Index)obj;
            if (currentVertex != ind.currentVertex) return false;
            return !(vertexSet != null ? !vertexSet.SetEquals(ind.vertexSet) : ind.vertexSet != null);

        }

        public override int GetHashCode()
        {
            int result = currentVertex;
            StringBuilder sb = new StringBuilder();
            sb.Append(currentVertex);
            if (vertexSet != null && vertexSet.Count == 0)
            {
                sb.Append(":").Append("E");
            }
            else if (vertexSet != null && vertexSet.Count > 0)
            {
                foreach (int i in vertexSet)
                {
                    sb.Append(":").Append(i);
                }
            }
            return sb.ToString().GetHashCode();
        }

        internal static Index CreateIndex(int vertex, HashSet<Int32> vertexSet)
        {
            Index i = new Index();
            i.currentVertex = vertex;
            i.vertexSet = vertexSet;
            return i;
        }

    }
}