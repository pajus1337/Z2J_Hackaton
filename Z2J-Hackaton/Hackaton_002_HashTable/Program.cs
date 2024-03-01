using System.Collections;
using System.Drawing;
using System;
using System.Text;
using System.Reflection;

namespace Hackaton_002_HashTable
{
    /*
     * This project implements a custom HashTable data structure as part of the Z2J Community Hackathon Solution. 
     * The goal of this task was to deepen understanding of core programming concepts by manually creating mechanisms that are often provided by programming languages themselves.
     * By developing a custom HashTable, this project explores the intricacies of hash function implementation, collision handling, and dynamic data storage.
     * The HashTable supports basic operations such as insert, remove, search, and contains, along with an additional function to count the total number of elements stored.
     * This implementation is a hands-on approach to learning about data structures, offering insights into how key-value pairs are stored, accessed,
     * and managed efficiently in a collection.
     * */

    public class Program
    {
        static void Main(string[] args)
        {
            HashTable hash_table = new HashTable(10);
            hash_table.insert("key1", "value1");
            hash_table.insert("key2", "value2");
            Console.WriteLine(hash_table.search("key1"));
            hash_table.remove("key1");
            Console.WriteLine(hash_table.contains("key1"));
            Console.WriteLine(hash_table.count_elements());
        }
    }

    public class HashTable
    {
        private int _size;
        private List<DummyObject>[] _hashTable;

        public HashTable(int arraySize)
        {
            _size = arraySize;

            _hashTable = new List<DummyObject>[_size];

            for (int i = 0; i < _size; i++)
            {
                _hashTable[i] = new List<DummyObject>();
            }
        }

        private int GetIndexBasedOnHash(int hashValue) => Math.Abs(hashValue % _hashTable.Length);

        public int hash_function(string key) => key.GetHashCode();

        public void insert(string key, string value)
        {
            int hash = hash_function(key);
            int index = GetIndexBasedOnHash(hash);

            if (contains(key))
            {
                Console.WriteLine($"Failed to add new object, {key} already exist in database.");
            }
            else
            {
                _hashTable[index].Add(new DummyObject(key, value));
            }
        }

        public void remove(string key)
        {
            int hash = hash_function(key);
            int index = GetIndexBasedOnHash(hash);

            for (int i = 0; i < _hashTable[index].Count; i++)
            {
                if (_hashTable[index][i].Key == key)
                {
                    _hashTable[index].RemoveAt(i);
                    break;
                }
            }
        }

        public string search(string key)
        {
            int hash = hash_function(key);
            int index = GetIndexBasedOnHash(hash);

            for (int i = 0; i < _hashTable[index].Count; i++)
            {
                if (_hashTable[index][i].Key == key)
                {
                   return _hashTable[index][i].Value;
                }
            }
            return $"{key} Not Found in database, check input and try again.";
        }

        public bool contains(string key)
        {
            int hash = hash_function(key);
            int index = GetIndexBasedOnHash(hash);

            for (int i = 0; i < _hashTable[index].Count; i++)
            {
                if (_hashTable[index][i].Key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public int count_elements()
        {
            int count = 0;

            for (int i = 0; i < _hashTable.Length; i++)
            {
                count += _hashTable[i].Count;
            }
            return count;
        }
    }

    public class DummyObject
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DummyObject(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}