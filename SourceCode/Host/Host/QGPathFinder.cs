﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph.Algorithms;
using QuickGraph;
using QuickGraph.Algorithms.ShortestPath;
using QuickGraph.Algorithms.Observers;
using System.Text.RegularExpressions;
using FloorPlanAndTile;
using FloorTile = FloorPlanAndTile.FloorTile;
using FloorPlan = FloorPlanAndTile.FloorPlan;

namespace Host
{
    public class QGPathFinder : PathFinder
    {
        AdjacencyGraph<string, Edge<string>> graph; 
        Dictionary<Edge<string>, double> edgeCost;
        Status m_status;
        FloorPlan fp;
        String startPoint;
        String targetPoint;
        Host m_parent;

        List<Edge<string>> edges;
        List<FloorTile> neighbors;

        public QGPathFinder(Status status, Host parent)
        {
            m_parent = parent;
            m_status = status;
            
            graph = new AdjacencyGraph<string, Edge<string>>(true);
            edgeCost = new Dictionary<Edge<string>, double>(graph.EdgeCount);
            this.fp = m_status.FloorPlan;
            if (this.m_status.Position != null)
            {
                startPoint = this.m_status.Position.location.X + "_" + this.m_status.Position.location.Y;
                m_parent.PostMessage("start: " + startPoint);
            }
            else
            {
                startPoint = "4_4";
            }
            if (this.m_status.EndPoint!= null)
            {
                targetPoint = this.m_status.EndPoint.X + "_" + this.m_status.EndPoint.Y;
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
                            edgeCost.Add(myedge, 7 - fp.getTile(i, j).openness(5));
                        }
                    }
                }
            }
        
        }

        public List<FloorTile> getPath()
        {
            if (this.m_status.Position != null)
            {
                startPoint = this.m_status.Position.location.X + "_" + this.m_status.Position.location.Y;
                m_parent.PostMessage("start: " + startPoint);
            }
            else
            {
                startPoint = "4_4";
            }
            if (this.m_status.EndPoint != null)
            {
                targetPoint = this.m_status.EndPoint.X + "_" + this.m_status.EndPoint.Y;
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



            return condenseList(retval);
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
            List<FloorTile> condensedList = new List<FloorTile>();

            FloorTile lastTile = path[0];


            condensedList.Add(path[0]);

            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].Position.Y == lastTile.Position.Y) // moving vertically
                {
                    while (i < path.Count && path[i].Position.Y == lastTile.Position.Y && i < path.Count - 1) // walk until the next turn
                    {
                        i++;
                    }
                }
                else                                           // moving horizontally
                {
                    while (i < path.Count && path[i].Position.X == lastTile.Position.X) // walk until the next turn
                    {
                        i++;
                    }
                }
                lastTile = path[i - 1];
                condensedList.Add(path[i - 1]); // add the turning point to the new list;
            }

            return condensedList;
        }
    }
}
