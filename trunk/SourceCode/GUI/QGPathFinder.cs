using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using See3PO;
using QuickGraph.Algorithms;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;
using System.Text.RegularExpressions;

namespace See3PO
{
    class QGPathFinder : PathFinder
    {
        AdjacencyGraph<string, Edge<string>> graph; 
        Dictionary<Edge<string>, double> edgeCost;
        Status m_status;
        FloorPlan fp;
        String startPoint;
        String targetPoint;
        MainForm m_parent;

        List<Edge<string>> edges;
        List<FloorTile> neighbors;

        public QGPathFinder(Status status, MainForm parent)
        {
            m_parent = parent;
            m_status = status;
            
            graph = new AdjacencyGraph<string, Edge<string>>(true);
            edgeCost = new Dictionary<Edge<string>, double>(graph.EdgeCount);
            this.fp = m_status.floorPlan;
            if (this.m_status.position != null)
            {
                startPoint = this.m_status.position.location.X + "_" + this.m_status.position.location.Y;
                m_parent.PostMessage("start: " + startPoint);
            }
            else
            {
                startPoint = "4_4";
            }
            if (this.m_status.endPoint!= null)
            {
                targetPoint = this.m_status.endPoint.X + "_" + this.m_status.endPoint.Y;
                m_parent.PostMessage("end: " + targetPoint);
            }
            else
            {
                targetPoint = "4_6";
            }

            buildGraph();
        }

        public void buildGraph() {
            // Add some vertices to the graph
            for (int i = 0; i < fp.getXTileNum(); i++)
            {
                for (int j = 0; j < fp.getYTileNum(); j++)
                {
                    if (fp.getWalkableValue(i, j) == 0)
                    {
                        graph.AddVertex(fp.getTile(i, j).Position.X + "_" + fp.getTile(i, j).Position.Y);
                        //m_parent.PostMessage(fp.getTile(i, j).Position.X + "_" + fp.getTile(i, j).Position.Y);
                    }

                    if (fp.getTile(i, j).endPoint)
                    {
                        this.targetPoint = i + "_" + j;
                    }
                }
            }

            edges = new List<Edge<string>>();
            neighbors = new List<FloorTile>();

            for (int i = 0; i < fp.getXTileNum(); i++)
            {
                for (int j = i; j < fp.getYTileNum(); j++)
                {
                    if (fp.getWalkableValue(i, j) == 0)
                    {

                        neighbors = fp.getTile(i, j).getNeighbours();

                        //m_parent.PostMessage(neighbors.Count);
                        for (int k = 0; k < neighbors.Count; k++)
                        {
                            Edge<string> myedge = new Edge<string>(
                                fp.getTile(i, j).Position.X + "_" + fp.getTile(i, j).Position.Y,
                                neighbors[k].Position.X + "_" + neighbors[k].Position.Y);
                            edges.Add(myedge);
                            graph.AddEdge(myedge);
                            edgeCost.Add(myedge, 1);
                        }
                    }
                }
            }
        
        }

        public List<FloorTile> getPath()
        {
            if (this.m_status.position != null)
            {
                startPoint = this.m_status.position.location.X + "_" + this.m_status.position.location.Y;
                m_parent.PostMessage("start: " + startPoint);
            }
            else
            {
                startPoint = "4_4";
            }
            if (this.m_status.endPoint != null)
            {
                targetPoint = this.m_status.endPoint.X + "_" + this.m_status.endPoint.Y;
                m_parent.PostMessage("end: " + targetPoint);
            }
            else
            {
                targetPoint = "4_6";
            }

           



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
                        m_parent.PostMessage(outEdges[i]);
                        string[] outPoint = Regex.Split(outEdges[i], "->");
                        //start points
                        retval.Add(getTileByIndex(fp, outPoint[0]));
                    }
                }
                //add target
                retval.Add(getTileByIndex(fp, targetPoint));
            }

            m_parent.PostMessage(retval.Count.ToString());
            m_parent.PostMessage(outString);

            if(retval.Count == 1 && retval[0].Equals(getTileByIndex(fp, targetPoint))){
                m_parent.PostMessage("Can't find path. Start or end point is not walkable or no available walkable tiles");
                return null;
            }

            return retval;
        }

        public static FloorTile getTileByIndex(FloorPlan fp, string index)
        {
            string[] outPoint1 = Regex.Split(index, "_");

            return fp.getTile(
                System.Convert.ToInt32(outPoint1[0]),
                System.Convert.ToInt32(outPoint1[1]));
        }

        public List<FloorTile> condenseList(List<FloorTile> path)
        {
            Boolean vertical = true;

            foreach (FloorTile tile in path)
            {
               
            }
        }

    }
}
