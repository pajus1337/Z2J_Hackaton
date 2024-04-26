using System;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hackaton_003_LinkedList
{
    /* 
     * custom LinkedList implementation for Hackaton 003
     * custom implementation of a doubly linked list, designed specifically for Hackaton 003. 
     * The implementation does not rely on indexes for node manipulation, enhancing flexibility and performance for specific use cases. 
     * The Node and NodeManager classes provide basic functionalities such as inserting at the beginning, end, and a specified position, as well as deleting nodes. Each node maintains references to both its next and previous nodes, allowing for efficient bidirectional traversal and manipulation. The main program demonstrates various operations to showcase the functionality of the linked list.
       
       Key features included:
     - Node creation with dynamic ID generation and data encapsulation.
     - Insertion and deletion from both ends of the list and at specific positions.
     - Comprehensive display and search functionalities for ease of debugging and testing.
     - Custom `ToString` method implementations for better node visibility during outputs.
    */

    public class Program
    {
        
        static void Main(string[] args)
        {
            var ll = new NodeManager();
            ll.Insert_At_Beginning("10");
            ll.Insert_At_End("20");
            ll.Insert_At_Beginning("15");
            ll.Insert_At_Beginning("5");
            ll.Insert_At_Position("16", "15");
            ll.Display();
            ll.Delete_From_Beginning();
            ll.Delete_Node("15");
            ll.Display();
            ll.Clear();
        }
    }

    public class Node
    {
        public static int NodeRef;
        public int Id { get; init; }
        public string Data { get; set; }
        public Node NextNode { get; set; }
        public Node LastNode { get; set; }

        public Node(string nodeData)
        {
            Id = ++NodeRef;
            Data = nodeData;
        }

        public override string ToString()
        {
            string lastNodeId = LastNode?.Id.ToString() ?? "null";
            string nextNodeId = NextNode?.Id.ToString() ?? "null";

            return $"Node Id: {Id}, Data: {Data} NextNode Id: {nextNodeId}, LastNode Id: {lastNodeId}";
        }
    }

    public class NodeManager
    {
        private List<Node> nodesCollecition { get; set; }
        private Node head;
        private Node tail;

        public NodeManager()
        {
            nodesCollecition = new List<Node>();
        }

        public void Insert_At_Beginning(string data)
        {
            Node nodeNewObj = new Node(data);

            if (nodesCollecition.Count == 0)
            {
                nodeNewObj.LastNode = null;
                head = nodeNewObj;
                tail = nodeNewObj;
            }

            else
            {
                nodeNewObj.NextNode = head;
                nodeNewObj.LastNode = null;
                head.LastNode = nodeNewObj;
                head = nodeNewObj;
            }

            nodesCollecition.Add(nodeNewObj);
            Console.WriteLine($"Sucessfuly added new node at beginning the Node Collection\r\n{head.ToString()}");
        }

        public void Insert_At_End(string data)
        {
            Node nodeNewObj = new Node(data);

            if (head == null && tail == null)
            {
                tail = nodeNewObj;
                tail.NextNode = null;
                head = tail;
            }

            else if (tail != null)
            {
                tail.NextNode = nodeNewObj;
                nodeNewObj.LastNode = tail;
                nodeNewObj.NextNode = null;
                tail = nodeNewObj;
            }

            nodesCollecition.Add(nodeNewObj);
            Console.WriteLine($"Sucessfuly added new node at end of the Node Collection\r\n{tail.ToString()}");
        }

        public void Insert_At_Position(string dataForNewNode, string placeItAfterNodeWithThisData)
        {
            Node nodeNewObj = new Node(dataForNewNode);
            Node nodeWithSearchedData = Search(placeItAfterNodeWithThisData);
            if (nodeWithSearchedData != null)
            {
                ConnectNewNodeInsert(nodeWithSearchedData, nodeNewObj);
                Console.WriteLine($"New node inserted sucessfully\r\n{nodeNewObj.ToString()}");
            }
            else
            {
                Console.WriteLine("Insterion of new Node failed.");
            }
        }

        public void Delete_From_Beginning()
        {
            if (nodesCollecition.Count == 0)
            {
                Console.WriteLine("Failed, Collection is Empty");
                return;
            }
            else if (nodesCollecition.Count > 1)
            {
                var newHeadNode = head.NextNode;
                newHeadNode.LastNode = null;
                nodesCollecition.Remove(head);
                Console.WriteLine($"Sucessfuly removed head node from collection\r\n{head.ToString()}");
                head = newHeadNode;
                Console.WriteLine($"New head node is now:\r\n{head.ToString()}");
                return;
            }
            else
            {
                nodesCollecition.Remove(head);
                Console.WriteLine("Sucessfully removed node from Collection, Collection is now Empty.");
                head = null;
                tail = null;
                return;
            }
        }

        public void Delete_From_End()
        {
            if (nodesCollecition.Count == 0)
            {
                Console.WriteLine("Failed, Collection is Empty");
                return;
            }
            else if (nodesCollecition.Count > 1)
            {
                var newTailNode = tail.LastNode;
                nodesCollecition.Remove(tail);
                Console.WriteLine($"Sucessfuly removed tail node from collection\r\n{tail.ToString()}");
                newTailNode.NextNode = null;
                tail = newTailNode;
                Console.WriteLine($"New tail node is now:\r\n{tail.ToString()}");
                return;
            }
            else
            {
                nodesCollecition.Remove(tail);
                Console.WriteLine("Sucessfully removed node from Collection, Collection is now Empty.");
                head = null;
                tail = null;
                return;

            }
        }

        public void Delete_Node(string containsData)
        {
            foreach (var node in nodesCollecition)
            {
                if (node.Data == containsData)
                {
                    if (node == tail)
                    {
                        Delete_From_End();
                        return;
                    }
                    else if (node == head)
                    {
                        Delete_From_Beginning();
                        return;
                    }
                    else
                    {
                        ConnectTwoNodes(node.LastNode, node.NextNode);
                        Console.WriteLine("Node Found, deleting.");
                        nodesCollecition.Remove(node);
                        Console.WriteLine($"Node {node.ToString()} Deleted");
                        return;
                    }
                }
            }

            Console.WriteLine("Node Not Found with this data");
        }

        public void Clear()
        {
            nodesCollecition.Clear();
            Console.WriteLine($"New Collection Size after Clean UP: {Size()}");
        }

        public string Display() => $"Node Collection size: {Size()}";

        public Node Search(string searchingData)
        {
            foreach (var node in nodesCollecition)
            {
                if (node.Data == searchingData)
                {
                    return node;
                }
            }
            return null;
        }

        public Node Search(int searchNodeById)
        {
            foreach (var node in nodesCollecition)
            {
                if (node.Id == searchNodeById)
                {
                    return node;
                }
            }

            Console.WriteLine($"Node with ID {searchNodeById}, not Found in Collection.");
            return null;
        }

        public int Size() => nodesCollecition.Count;

        private void ConnectTwoNodes(Node lastNode, Node nextNode)
        {
            lastNode.NextNode = nextNode;
            nextNode.LastNode = lastNode;
        }

        private void ConnectNewNodeInsert(Node nodeInCollection, Node newNode)
        {
            if (nodeInCollection.NextNode != null)
            {
                newNode.NextNode = nodeInCollection.NextNode;
                newNode.LastNode = nodeInCollection;
                nodeInCollection.NextNode = newNode;
            }
            else if (nodeInCollection == tail)
            {
                newNode.NextNode = null;
                newNode.LastNode = nodeInCollection;
                tail = newNode;
            }
        }
    }
}