using System.Collections.Generic;
using System.Xml.Linq;
using Telstar_Logistics_Parcel_Delivery_Solution.Calculations;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Calculations;

public class RouteMapperServiceImpl : RouteMapperService
{

    public bool GetDuration()
    {
        return true;
    }


    public List<int> Execute(List<(int, int, int)> cityMap, int start, int end)
    {
        List<Node> nodes = new List<Node>();
        List<int> uniqueNodeIds = (from x in cityMap select x.Item1).Distinct().ToList();

        // Initialize the list of nodes
        uniqueNodeIds.ForEach(x_id => nodes.Add(new Node(x_id)));

        // Add edges to each node
        foreach (var (source, destination, distance) in cityMap)
        {
            Node sourceNode = nodes.Find(x => x.Id == source && x.Id != null);
            sourceNode.Edges.Add(new EdgesNonDb(source, destination, distance));
        }

        List<int> path = Dijkstra.ShortestPath(nodes, start, end);

        return path;

    }

}









class EdgesNonDb
{
    public int Source { get; set; }
    public int Destination { get; set; }
    public int Distance { get; set; }

    public EdgesNonDb(int source, int destination, int distance)
    {
        Source = source;
        Destination = destination;
        Distance = distance;
    }
}

class Node
{
    public int Id { get; set; }
    public List<EdgesNonDb> Edges { get; set; }

    public Node(int id)
    {
        Id = id;
        Edges = new List<EdgesNonDb>();
    }
}


class Dijkstra
{
    public static List<int> ShortestPath(List<Node> nodes, int source, int destination)
    {
        // Create dictionary to store the distance from the source node to each node
        Dictionary<int, int> distances = new Dictionary<int, int>();
        // Create dictionary to store the previous node in the shortest path
        Dictionary<int, int> previous = new Dictionary<int, int>();
        // Create a priority queue to store the unvisited nodes
        PriorityQueue<int> unvisited = new PriorityQueue<int>();

        // Initialize the distances and previous dictionaries and priority queue
        foreach (var node in nodes)
        {
            distances[node.Id] = int.MaxValue;
            previous[node.Id] = -1;
            unvisited.Enqueue(node.Id, int.MaxValue);
        }
        distances[source] = 0;
        unvisited.UpdatePriority(source, 0);
        Node currentNode;
        // While there are still unvisited nodes
        while (unvisited.Count > 0)
        {
            // Get the node with the smallest distance from the source node
            int current = unvisited.Dequeue();
            // If we have reached the destination node, we can stop
            if (current == destination) break;
            // Get the current node from the list of nodes
            currentNode = nodes.Find(x => x.Id == current);
            // For each edge of the current node
            foreach (var edge in currentNode.Edges)
            {
                // Get the destination node of the edge
                int neighbor = edge.Destination;
                // Calculate the distance from the source node to the neighbor node through the current node
                int distance = distances[current] + edge.Distance;
                // If this distance is less than the current distance stored in the distances dictionary
                if (distance < distances[neighbor])
                {
                    // Update the distance and the previous node
                    distances[neighbor] = distance;
                    previous[neighbor] = current;
                    unvisited.UpdatePriority(neighbor, distance);
                }
            }
        }
        // If we couldn't reach the destination node
        if (previous[destination] == -1)
        {
            return new List<int>();
        }
        // Reconstruct the shortest path
        List<int> path = new List<int>();
        int nextNode = destination;
        while (nextNode != -1)
        {
            path.Insert(0, nextNode);
            nextNode = previous[nextNode];
        }
        return path;
    }
}

class PriorityQueue<T>
{
    private Dictionary<T, int> _dictionary;

    public PriorityQueue() { _dictionary = new Dictionary<T, int>(); }
    public void Enqueue(T item, int priority) { _dictionary.Add(item, priority); }
    public T Dequeue() { var min = _dictionary.Aggregate((l, r) => l.Value < r.Value ? l : r).Key; _dictionary.Remove(min); return min; }
    public void UpdatePriority(T item, int priority) { _dictionary[item] = priority; }
    public int Count { get { return _dictionary.Count; } }
}



