using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DijkstraStudent
{
    public class DijkstraAlgorithm
    {
        public DisplayGraphDelegate DisplayGraph { get; set; }

        public DijkstraAlgorithm()
        {
            // ...
        }

        public void CalculateShortestPaths(Graph graph, Node startingNode)
        {
            // step 1...        
            foreach (var n in graph.Nodes)
            {
                n.Distance = int.MaxValue;
                n.ShortestPathKnown = false;
            }
            startingNode.Distance = 0;

            // step 2...

            // continue the algorithm, while there are more nodes to check
            while (true)
            {
                if (graph.Nodes.All(x => x.ShortestPathKnown)) break;
                // step 3...
                var node = graph.Nodes
                    .Where(x => !x.ShortestPathKnown)
                    .OrderBy(x => x.Distance)
                    .ThenBy(x => Guid.NewGuid())
                    .First();

                node.ShortestPathKnown = true;
                var neighbours = graph.Edges.Where(x => x.A == node || x.B == node);
                var neighbourNodes = neighbours
                    .Where(x => !x.B.ShortestPathKnown || !x.A.ShortestPathKnown)
                    .Select(x => x.B == node ? x.A : x.B);
                //node.ShortestPathKnown = true;
                
                // update interface
                DisplayGraph(startingNode);

                // step 4...
                foreach (var neighbour in neighbourNodes)
                {
                    var edge = neighbours
                        .Single(x => x.A == node && x.B == neighbour || x.B == node && x.A == neighbour);
                    var distance = edge.Weight + node.Distance;
                    if (neighbour.Distance > distance)
                        neighbour.Distance = distance;
                    neighbour.ParentNode = node;                    
                }

                // update interface
                DisplayGraph(startingNode);
            }
        }
    }
}