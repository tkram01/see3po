using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using See3PO;
using QuickGraph.Algorithms;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;

namespace See3PO
{
    class QGPathFinder : PathFinder
    {
        AdjacencyGraph<string, Edge<string>> graph; 
        Dictionary<Edge<string>, double> edgeCost;
        FloorPlan fp;

        public QGPathFinder(FloorPlan fp)
        {
            graph = new AdjacencyGraph<string, Edge<string>>(true);
            edgeCost = new Dictionary<Edge<string>, double>(graph.EdgeCount);
            this.fp = fp;

        }

        public List<FloorTile> getPath()
        {

            // Add some vertices to the graph
            for (int i = 0; i < fp.getXTileNum(); i++)
            {
                for (int j = 0; j < fp.getYTileNum(); j++)
                {
                    if (fp.getWalkableValue(i, j) == 0)
                    {
                        graph.AddVertex(i & "_" & j);
                    }
                }
            }

            Console.WriteLine("ddd");

            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");
            graph.AddVertex("D");
            graph.AddVertex("E");
            graph.AddVertex("F");
            graph.AddVertex("G");
            graph.AddVertex("H");
            graph.AddVertex("I");
            graph.AddVertex("J");



            // Create the edges
            Edge<string> a_b = new Edge<string>("A", "B");
            Edge<string> a_d = new Edge<string>("A", "D");
            Edge<string> b_a = new Edge<string>("B", "A");
            Edge<string> b_c = new Edge<string>("B", "C");
            Edge<string> b_e = new Edge<string>("B", "E");
            Edge<string> c_b = new Edge<string>("C", "B");
            Edge<string> c_f = new Edge<string>("C", "F");
            Edge<string> c_j = new Edge<string>("C", "J");
            Edge<string> d_e = new Edge<string>("D", "E");
            Edge<string> d_g = new Edge<string>("D", "G");
            Edge<string> e_d = new Edge<string>("E", "D");
            Edge<string> e_f = new Edge<string>("E", "F");
            Edge<string> e_h = new Edge<string>("E", "H");
            Edge<string> f_i = new Edge<string>("F", "I");
            Edge<string> f_j = new Edge<string>("F", "J");
            Edge<string> g_d = new Edge<string>("G", "D");
            Edge<string> g_h = new Edge<string>("G", "H");
            Edge<string> h_g = new Edge<string>("H", "G");
            Edge<string> h_i = new Edge<string>("H", "I");
            Edge<string> i_f = new Edge<string>("I", "F");
            Edge<string> i_j = new Edge<string>("I", "J");
            Edge<string> i_h = new Edge<string>("I", "H");
            Edge<string> j_f = new Edge<string>("J", "F");

            // Add the edges
            graph.AddEdge(a_b);
            graph.AddEdge(a_d);
            graph.AddEdge(b_a);
            graph.AddEdge(b_c);
            graph.AddEdge(b_e);
            graph.AddEdge(c_b);
            graph.AddEdge(c_f);
            graph.AddEdge(c_j);
            graph.AddEdge(d_e);
            graph.AddEdge(d_g);
            graph.AddEdge(e_d);
            graph.AddEdge(e_f);
            graph.AddEdge(e_h);
            graph.AddEdge(f_i);
            graph.AddEdge(f_j);
            graph.AddEdge(g_d);
            graph.AddEdge(g_h);
            graph.AddEdge(h_g);
            graph.AddEdge(h_i);
            graph.AddEdge(i_f);
            graph.AddEdge(i_h);
            graph.AddEdge(i_j);
            graph.AddEdge(j_f);


            edgeCost.Add(a_b, 4);
            edgeCost.Add(a_d, 1);
            edgeCost.Add(b_a, 74);
            edgeCost.Add(b_c, 2);
            edgeCost.Add(b_e, 12);
            edgeCost.Add(c_b, 12);
            edgeCost.Add(c_f, 74);
            edgeCost.Add(c_j, 12);
            edgeCost.Add(d_e, 32);
            edgeCost.Add(d_g, 22);
            edgeCost.Add(e_d, 66);
            edgeCost.Add(e_f, 76);
            edgeCost.Add(e_h, 33);
            edgeCost.Add(f_i, 11);
            edgeCost.Add(f_j, 21);
            edgeCost.Add(g_d, 12);
            edgeCost.Add(g_h, 10);
            edgeCost.Add(h_g, 2);
            edgeCost.Add(h_i, 72);
            edgeCost.Add(i_f, 31);
            edgeCost.Add(i_h, 18);
            edgeCost.Add(i_j, 7);
            edgeCost.Add(j_f, 8);

            String startPoint = "A";
            String targetPoint = "I";

            //startPoint = txtStartPoint.Text;
            //targetPoint = txtTargetPoint.Text;

            DijkstraShortestPathAlgorithm<string, Edge<string>> dijkstra = new DijkstraShortestPathAlgorithm<string, Edge<string>>(graph, AlgorithmExtensions.GetIndexer<Edge<string>, double>(edgeCost));

            // Attach a Vertex Predecessor Recorder Observer to give us the paths
            QuickGraph.Algorithms.Observers.VertexPredecessorRecorderObserver<string, Edge<string>> predecessorObserver = new QuickGraph.Algorithms.Observers.VertexPredecessorRecorderObserver<string, Edge<string>>();
            predecessorObserver.Attach(dijkstra);

            // attach a distance observer to give us the shortest path distances
            VertexDistanceRecorderObserver<string, Edge<string>> distObserver = new VertexDistanceRecorderObserver<string, Edge<string>>(AlgorithmExtensions.GetIndexer<Edge<string>, double>(edgeCost));
            distObserver.Attach(dijkstra);

            // Run the algorithm with A set to be the source
            dijkstra.Compute(startPoint);

            String outString = "";

            outString = "Shortest distance: ";
            outString += distObserver.Distances[targetPoint] + "\n";

            

            IEnumerable<Edge<string>> path;
            if (predecessorObserver.TryGetPath(targetPoint, out path))
                foreach (var u in path)
                    outString += u + "\n";

            
            return null;
        }


    }
}
