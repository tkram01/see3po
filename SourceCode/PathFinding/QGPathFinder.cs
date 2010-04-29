using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;
using System.Text.RegularExpressions;
using See3PO;

namespace See3PO
{
    public class QGPathFinder : PathFinder
    {
        AdjacencyGraph<string, Edge<string>> graph; 
        Dictionary<Edge<string>, double> edgeCost;
        FloorPlan fp;
        String startPoint;
        String targetPoint;

        List<Edge<string>> edges;
        List<FloorTile> neighbors;

        String messages = "";

        public QGPathFinder(FloorPlan floorPlan)
        {
            this.messages += "- Checking Floor Plan...\n";
            graph = new AdjacencyGraph<string, Edge<string>>(true);
            edgeCost = new Dictionary<Edge<string>, double>(graph.EdgeCount);
            this.fp = floorPlan;

            if (this.fp.getStartTile() != null)
            {
                this.messages += "    Start Point is OK...\n";
                startPoint = this.fp.getStartTile().Position.X + "_" + this.fp.getStartTile().Position.Y;
            }
            else
            {
                this.messages += "    Start Point is not valid...\n";
                startPoint = "4_4";
            }

            if (this.fp.getTargetTile() != null)
            {
                this.messages += "    Target Point is OK...\n";
                targetPoint = this.fp.getTargetTile().Position.X + "_" + this.fp.getTargetTile().Position.Y;
            }
            else
            {
                this.messages += "    Target Point is not valid...\n";
                targetPoint = "4_6";
            }

            buildGraph();
        }

        public AdjacencyGraph<string, Edge<string>> buildGraph()
        {
            this.messages += "- Building Graph...\n";

            // Add some vertices to the graph
            for (int i = 0; i < fp.getXTileNum(); i++)
            {
                for (int j = 0; j < fp.getYTileNum(); j++)
                {
                    if (fp.getWalkableValue(i, j) == 0)
                    {
                        graph.AddVertex(fp.getTile(i, j).Position.X + "_" + fp.getTile(i, j).Position.Y);
                        
                    }

                    if (fp.getTile(i, j).endPoint)
                    {
                        this.targetPoint = i + "_" + j;
                    }
                }
            }
            this.messages += "    There are " + graph.VertexCount + " vertices.\n";

            edges = new List<Edge<string>>();
            neighbors = new List<FloorTile>();

            for (int i = 0; i < fp.getXTileNum(); i++)
            {
                for (int j = 0; j < fp.getYTileNum(); j++)
                {
                    if (fp.getWalkableValue(i, j) == 0)
                    {

                        neighbors = fp.getTile(i, j).getNeighbours();

                        for (int k = 0; k < neighbors.Count; k++)
                        {
                            Edge<string> myedge = new Edge<string>(
                                fp.getTile(i, j).Position.X + "_" + fp.getTile(i, j).Position.Y,
                                neighbors[k].Position.X + "_" + neighbors[k].Position.Y);
                            edges.Add(myedge);
                            graph.AddEdge(myedge);
                            //this.messages += "    edges: " + fp.getTile(i, j).Position.X + "_" + fp.getTile(i, j).Position.Y + "->" + neighbors[k].Position.X + "_" + neighbors[k].Position.Y + "\n";
                            edgeCost.Add(myedge, 1);
                        }
                    }
                }
            }

            this.messages += "    There are " + graph.EdgeCount + " edges.\n";

            return graph;
        
        }

        public List<FloorTile> getPath()
        {
            if (this.fp != null)
            {
                startPoint = this.fp.getStartTile().Position.X + "_" + this.fp.getStartTile().Position.Y;
                targetPoint = this.fp.getTargetTile().Position.X + "_" + this.fp.getTargetTile().Position.Y;
            }


            this.messages += "- Start Get Path\n";
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
            this.messages += "    Start Point: " + startPoint + ".\n";
            this.messages += "    Target Point: " + targetPoint + ".\n";

            String outString = "";

            //outString += distObserver.Distances[targetPoint] + "\n";

            IEnumerable<Edge<string>> path;
            if (predecessorObserver.TryGetPath(targetPoint, out path))
                foreach (var u in path)
                    outString += u + ";";

            List<FloorTile> retval = new List<FloorTile>();

            string[] outEdges = Regex.Split(outString, ";");
            if (outEdges.Length > 0)
            {
                for (int i = 0; i < outEdges.Length; i++)
                {
                    if (outEdges[i].Length > 0)
                    {
                        this.messages += outEdges[i] + "\n";
                        string[] outPoint = Regex.Split(outEdges[i], "->");
                        //start points
                        retval.Add(getTileByIndex(fp, outPoint[0]));
                    }
                }
                //add target
                retval.Add(getTileByIndex(fp, targetPoint));
            }

            this.messages += retval.Count.ToString()+ "\n";;

            if(retval.Count == 1 && retval[0].Equals(getTileByIndex(fp, targetPoint))){
                this.messages += "Can't find path. Start or end point is not walkable or no available walkable tiles" + "\n"; ;
                return null;
            }


            return retval;

            //return condenseList(retval);
        }

        public static FloorTile getTileByIndex(FloorPlan fp, string index)
        {
            string[] outPoint1 = Regex.Split(index, "_");

            return fp.getTile(
                System.Convert.ToInt32(outPoint1[0]),
                System.Convert.ToInt32(outPoint1[1]));
        }


        public String GetMessages()
        {
            return this.messages;
        }

        public AdjacencyGraph<string, Edge<string>> GetGraph()
        {
            return this.graph;
        }
    }
}
