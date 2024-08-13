using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public LayerMask terrainLayer;
    int gridSize = 1;
    public List<Vector3> path;

    [Header("Objects")]
    [SerializeField] protected Creature myCreature;
    [SerializeField] protected Creature targetCreature;
    [SerializeField] protected GameObject marker;

    [Header("Config")]
    [SerializeField] float sightRange = 5;
    [SerializeField] LayerMask terrainMask;
    [SerializeField] LayerMask sightMask;


    float sightRangeSquared;
    protected void Awake(){
        sightRangeSquared = sightRange * sightRange;
        path = new List<Vector3>();
    }

    protected void SearchForTarget(){

        foreach(Creature c in CreatureManager.singleton.GetCreatures()){
            //
            if(c == myCreature){
                continue; //we don't wanna target ourselves!
            }
            if(CanSeeTarget(c)){
                targetCreature = c;
            }
        }
    }


     public bool CanSeeTarget(Creature tempTarget){
        if(tempTarget == null){ //this was the issue
            return false;
        }
        if((myCreature.transform.position - tempTarget.transform.position).sqrMagnitude > sightRangeSquared){
            return false;
        }
        if(Physics2D.Linecast(myCreature.transform.position,tempTarget.transform.position,sightMask)){
            return false;
        }
        return true;
    }

    public bool CanSeeTarget(){
        return CanSeeTarget(targetCreature);
    }

    public void ClearPath(){
        path.Clear();
    }


    //pathfinding
    public List<Vector3> AStarPath(Vector3 startPos, Vector3 endPos)
    {

        startPos = RoundVectorToInt(startPos);
        endPos = RoundVectorToInt(endPos);

        //this will be the path we output
        path = new List<Vector3>();

        //if we can't walk on start position or end position, then return empty path
        if (!IsWalkable(startPos) || !IsWalkable(endPos))
        {
            return path; //can't reach destination so return here
        }

        PriorityQueue<Node> openList = new PriorityQueue<Node>(); //nodes we are considering
        HashSet<Node> closedList = new HashSet<Node>(); //nodes we've already explored

        Node startNode = new Node(startPos);
        Node endNode = new Node(endPos);

        openList.Enqueue(startNode);
        int attempts = 1000;
        while (openList.Count > 0 && attempts > 0)
        {
            attempts -=1;
            Node currentNode = openList.Dequeue();

            if (currentNode.Equals(endNode)) //we've found the path, load all nodes into list and return
            {
                while (currentNode != null)
                {
                    path.Add(currentNode.Position);
                    currentNode = currentNode.Parent;
                }

                return path;
            }

            closedList.Add(currentNode);

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor) || !IsWalkable(neighbor.Position))
                {
                    continue;
                }

                float newMovementCostToNeighbor = currentNode.GCost + GetSquaredDistance(currentNode,neighbor); //use 1 because 1 space away
                if (newMovementCostToNeighbor < neighbor.GCost || !openList.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = GetSquaredDistance(neighbor, endNode);
                    neighbor.Parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Enqueue(neighbor);
                    }
                }
            }
        }

        return path; // Return an empty path if no path is found
    }

    protected bool IsWalkable(Vector3 position)
    {
        return !Physics2D.OverlapCircle(position, 0.1f, terrainLayer);
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        Vector3[] directions = new Vector3[]
        {
            new Vector3(gridSize, 0, 0),
            new Vector3(-gridSize, 0, 0),
            new Vector3(0, gridSize, 0),
            new Vector3(0, -gridSize, 0),
            new Vector3(gridSize, gridSize, 0),
            new Vector3(gridSize, -gridSize, 0),
            new Vector3(-gridSize, gridSize, 0),
            new Vector3(-gridSize, -gridSize, 0)
        };

        foreach (Vector3 direction in directions)
        {
            Vector3 neighborPos = node.Position + direction;
            neighbors.Add(new Node(neighborPos));
        }

        return neighbors;
    }

    private float GetSquaredDistance(Node a, Node b)
    {
        float dstX = Mathf.Abs(a.Position.x - b.Position.x);
        float dstY = Mathf.Abs(a.Position.y - b.Position.y);

        return dstX * dstX + dstY * dstY;
    }

    private class Node : IComparer<Node>
    {
        public Vector3 Position;
        public Node Parent;
        public float GCost;
        public float HCost;
        public float FCost => GCost + HCost;

        public Node(Vector3 position)
        {
            Position = position;
        }

        public int Compare(Node x, Node y)
        {
            return x.FCost.CompareTo(y.FCost);
        }

        public override bool Equals(object obj)
        {
            if (obj is Node other)
            {
                return Position == other.Position;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }

    private class PriorityQueue<T> where T : IComparer<T>
    {
        private List<T> items = new List<T>();

        public int Count => items.Count;

        public void Enqueue(T item)
        {
            items.Add(item);
            int ci = items.Count - 1;
            while (ci > 0)
            {
                int pi = (ci - 1) / 2;
                if (items[ci].Compare(items[ci], items[pi]) >= 0)
                {
                    break;
                }
                var tmp = items[ci];
                items[ci] = items[pi];
                items[pi] = tmp;
                ci = pi;
            }
        }

        public T Dequeue()
        {
            int li = items.Count - 1;
            T frontItem = items[0];
            items[0] = items[li];
            items.RemoveAt(li);

            --li;
            int pi = 0;
            while (true)
            {
                int ci = pi * 2 + 1;
                if (ci > li)
                {
                    break;
                }
                int rc = ci + 1;
                if (rc <= li && items[ci].Compare(items[ci], items[rc]) > 0)
                {
                    ci = rc;
                }
                if (items[pi].Compare(items[pi], items[ci]) <= 0)
                {
                    break;
                }
                var tmp = items[pi];
                items[pi] = items[ci];
                items[ci] = tmp;
                pi = ci;
            }

            return frontItem;
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }
    }

    Vector3 RoundVectorToInt(Vector3 vector)
    {
        return new Vector3(
            Mathf.Round(vector.x),
            Mathf.Round(vector.y),
            0
        );
    }
}
