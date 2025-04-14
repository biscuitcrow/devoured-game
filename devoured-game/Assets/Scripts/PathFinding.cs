using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Assuming that the graph node values, neighbours, and weights for each node are already set up 
public class GraphNode
{
    public Vector2 coordinates;
    public float distanceToStart;
    public float distanceToEnd;
    public float distanceValue;

    public bool isShaded;
    public bool isRoad;
    public bool isBuilding;
    public bool IsSheltered;
    public bool isObstacle; //For tiles that cannot be walked on like building walls

    public List<GraphNode> neighbourNodes;
    public List<float> neighbourWeights; // Holds the distance from this node to the corresponding neighbour node. 1 for vertical/horizontal nodes and 1.41 for diagonal nodes.
}


// I'm sorry, this solution is incomplete.
// I could not think of a workable way to implement the likes and dislikes of the characters.
// My initial idea was to use the bools to add preference multiplier to the weights for likes and completely omit the disliked tiles.
// Unfortunately, I could not figure out how to ensure the "as much as possible" condition. 

public class PathFinding : MonoBehaviour
{
    // Assuming that these characteristics are independent of each other with some data not pictured on the map, like outdoor tree shade and sheltered walkways
    // Assuming each individual employee would like their own different optimal path

    public GraphNode startNode;
    public GraphNode endNode;
    public GraphNode currentNode;
    public List<GraphNode> optimalPath;

    List<float> valueList = new List<float>();
    List<GraphNode> viableNodes = new List<GraphNode>();


    void CalculateDistanceValuesForNeighbouringNodes()
    {
        // Calculating distance values for neighbouring nodes
        foreach (GraphNode node in currentNode.neighbourNodes)
        {
            viableNodes.Add(node);

            float distanceToStart = DistanceBetweenTwoNodes(node, startNode);
            node.distanceToStart = distanceToStart;

            float distanceToEnd = DistanceBetweenTwoNodes(node, endNode);
            node.distanceToEnd = distanceToEnd;

            // The smaller this value is, the closer the node to the optimal path
            float distanceValue = distanceToStart + distanceToEnd;
            node.distanceValue = distanceValue;

            valueList.Add(distanceValue);
        }
    }

    private void SearchNeighbouringNodesForOptimalNode()
    {
        GraphNode optimalNode = null;
        float currentLowestDist;

        CalculateDistanceValuesForNeighbouringNodes();

        currentLowestDist = valueList.Min();
        foreach (GraphNode node in viableNodes)
        {
            if (node.distanceValue == currentLowestDist)
            {
                optimalNode = node;
            }
        }


        // Handles the case if multiple nodes have the same distanceValue
        // We select the node closest to the end point
        if (viableNodes.Count > 1)
        {
             
            float lowestDistanceValue = int.MaxValue;
            
            foreach (GraphNode node in viableNodes)
            {
                if (node.distanceToEnd < lowestDistanceValue)
                {
                    lowestDistanceValue = node.distanceToEnd;
                    optimalNode = node;
                }
            }
        }

        if (optimalNode != null)
        {
            optimalPath.Add(optimalNode);
        }
    }

    // Calculates the shortest distance from 1 point to another
    private float DistanceBetweenTwoNodes(GraphNode nodeA, GraphNode nodeB)
    {
        float distanceX = Mathf.Abs(nodeA.coordinates.x - nodeB.coordinates.y);
        float distanceY = Mathf.Abs(nodeA.coordinates.y - nodeB.coordinates.y);

        float min = Mathf.Min(distanceX, distanceY);
        float max = Mathf.Min(distanceX, distanceY);

        float distance = min * Mathf.Sqrt(2) + (max - min);
        return distance;
    }
     
}

