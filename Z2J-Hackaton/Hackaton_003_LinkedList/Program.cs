using System;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hackaton_003_LinkedList
{
    /*
     * Custom LinkedList Implementation for Hackaton 003
     * This implementation of a doubly linked list is tailored specifically for Hackaton 003, 
     * featuring enhancements that boost flexibility and performance for specialized use cases.
     * The list is designed without reliance on index-based node manipulation, 
     * supporting more dynamic data handling and modifications.
     * 
     * The Node and NodeManager classes form the core of this implementation, 
     * facilitating essential operations such as insertions at the beginning, the end, 
     * and at specified positions, as well as deletions of nodes. Each node maintains references
     * to both its next and previous nodes, enabling efficient bidirectional traversal and manipulation.
     * 
     * Major features of this implementation include:
     * - Node creation with auto-incremented ID for unique identification and encapsulated data handling.
     * - Efficient insertion and deletion operations at both ends of the list, as well as precise insertions at specified positions, 
         managed through a dynamic size tracking system to optimize performance.
     * - An enhanced display function that provides immediate visibility into the current size of the list, aiding in debugging and real-time data management.
     * - Advanced search functionalities that allow quick retrieval of nodes by their data or ID, leveraging the doubly linked structure for efficient scanning 
         from both ends of the list.
     * - Custom `ToString` method implementations in the Node class to improve the visibility and debugging output of node states, 
         facilitating easier tracking of node relationships and data during operations.
     *
     * The main program section demonstrates various operations, showcasing the robust capabilities and versatility of the linked list in managing a dynamic set of data entries effectively.
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
            ll.Display();
            ll.Insert_At_Position("16", "15");
            ll.Insert_At_Beginning("5");
            ll.Display();
            ll.Delete_From_End();
            ll.Delete_Node("15");
            ll.Display();
            ll.Clear();
            ll.Insert_At_Beginning("3");
            ll.Delete_From_Beginning();
            ll.Display();
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
        private Node head;
        private Node tail;
        private int collectionSize = 0;

        public void Insert_At_Beginning(string data)
        {
            Node nodeNewObj = new Node(data);

            if (collectionSize == 0)
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

            ++collectionSize;
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

            ++collectionSize;
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
                collectionSize++;
            }
            else
            {
                Console.WriteLine("Insterion of new Node failed.");
            }
        }

        public void Delete_From_Beginning()
        {
            if (collectionSize == 0)
            {
                Console.WriteLine("Failed, Collection is Empty");
                return;
            }
            else if (collectionSize > 1)
            {
                Node NodeToRemove;
                NodeToRemove = head;
                var newHeadNode = head.NextNode;
                newHeadNode.LastNode = null;
                head = null;
                Console.WriteLine($"Sucessfuly removed head node from collection\r\n{NodeToRemove.ToString()}");
                NodeToRemove = null;
                head = newHeadNode;
                Console.WriteLine($"New head node is now:\r\n{head.ToString()}");
                --collectionSize;
                return;
            }
            else
            {
                head = null;
                tail = null;
                --collectionSize;
                Console.WriteLine("Sucessfully removed node from Collection, Collection is now Empty.");
                return;
            }
        }

        public void Delete_From_End()
        {
            if (collectionSize == 0)
            {
                Console.WriteLine("Failed, Collection is Empty");
                return;
            }
            else if (collectionSize > 1)
            {
                var newTailNode = tail.LastNode;
                var NodeToRemove = tail;
                tail = null;
                Console.WriteLine($"Sucessfuly removed tail node from collection\r\n{NodeToRemove.ToString()}");
                NodeToRemove = null;
                newTailNode.NextNode = null;
                tail = newTailNode;
                Console.WriteLine($"New tail node is now:\r\n{tail.ToString()}");
                --collectionSize;
                return;
            }
            else
            {
                head = null;
                tail = null;
                Console.WriteLine("Sucessfully removed node from Collection, Collection is now Empty.");
                --collectionSize;
                return;

            }
        }

        public void Delete_Node(string containsData)
        {

            if (tail.Data == containsData)
            {
                Delete_From_End();
                return;
            }

            else if (head.Data == containsData)
            {
                Delete_From_Beginning();
                return;
            }
            else
            {
                Node currentNode = head.NextNode;
                while (currentNode != tail)
                {
                    if (currentNode.Data == containsData)
                    {
                        Console.WriteLine("Node Found, deleting.");
                        Node nodeToRemove = currentNode;
                        ConnectTwoNodes(currentNode.LastNode, currentNode.NextNode);
                        currentNode = null;
                        Console.WriteLine($"Node {nodeToRemove.ToString()} Deleted");
                        nodeToRemove = null;
                        --collectionSize;
                        return;
                    }
                    currentNode = currentNode.NextNode;
                }
                Console.WriteLine("Node Not Found with this data");
                return;
            }
        }

        public void Clear()
        {
            Node currentNode = tail;

            while (currentNode != head)
            {
                Node nodeToRemove = currentNode;
                currentNode = currentNode.LastNode;
                nodeToRemove = null;
                --collectionSize;
            }
            head = null;
            tail = null;
            --collectionSize;
            Console.WriteLine($"New Collection Size after Clean UP: {collectionSize}");
        }

        public void Display()
        {
            Console.WriteLine($"Node Collection size: {collectionSize}");
        }

        public Node Search(string searchingData)
        {
            Node currentNode = head;

            if (tail.Data == searchingData)
            {
                return tail;
            }

            else
            {
                while (currentNode != null)
                {
                    if (currentNode.Data == searchingData)
                    {
                        return currentNode;
                    }
                    currentNode = currentNode.NextNode;
                }
                Console.WriteLine($"Node with ID {searchingData}, not Found in Collection.");
                return null;
            }
        }

        public Node Search(int searchNodeById)
        {
            Node currentNode = head;

            if (tail.Id == searchNodeById)
            {
                return tail;
            }

            else
            {
                while (currentNode != null)
                {
                    if (currentNode.Id == searchNodeById)
                    {
                        return currentNode;
                    }
                    currentNode = currentNode.NextNode;
                }
                Console.WriteLine($"Node with ID {searchNodeById}, not Found in Collection.");
                return null;
            }
        }

        public int Size() => collectionSize;

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