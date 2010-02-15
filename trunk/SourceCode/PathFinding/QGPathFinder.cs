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
            
            graph = new AdjacencyGraph<string, Edge<string>>(true);
            edgeCost = new Dictionary<Edge<string>, double>(graph.EdgeCount);
            this.fp = floorPlan;

            if (this.fp.getStartTile() != null)
            {
                startPoint = this.fp.getStartTile().Position.X + "_" + this.fp.getStartTile().Position.Y;
            }
            else
            {
                startPoint = "4_4";
            }

            if (this.fp.getTargetTile() != null)
            {
                targetPoint = this.fp.getTargetTile().Position.X + "_" + this.fp.getTargetTile().Position.Y;
            }
            else
            {
                targetPoint = "4_6";
            }

            //
            buildGraph();
        }

        public AdjacencyGraph<string, Edge<string>> buildGraph()
        {
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

            return graph;
        
        }

        public List<FloorTile> getPath()
        {

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
            this.messages += outString+ "\n";;

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
