using System;
using System.Collections.Generic;

namespace ProgrammingExercises
{
    class LRUCache
    {
        private Dictionary<string, string> values;
        private Queue<string> keyQueue;
        private int cacheSize;

        public LRUCache(int size)
        {
            if (size > 0)
            {
                values = new Dictionary<string, string>(size);
                keyQueue = new Queue<string>(size);
                cacheSize = size;
                return;
            }

            throw new ArgumentException();
        }

        //Accepts a string array where key is index 1, and value is index 2, and returns result of set
        public string Set(string[] input)
        {
            //We should have a key and a value in indeces 1 and 2
            if(input[1].Length>0 && input[2].Length>0)
            {
                //If we already have the key, just update its value, and the key queue
                if(values.ContainsKey(input[1]))
                {
                    values[input[1]] = input[2];
                    //If the value at the head was accessed, it's no longer the least recently used
                    if (input[1] == keyQueue.Peek())
                    {
                        string key = keyQueue.Dequeue();
                        //Put at the end of the queue
                        keyQueue.Enqueue(key);
                    }
                    return "SET OK";
                }

                //If we haven't filled up the cache, set key, value, and enqueue key
                if(values.Count < cacheSize)
                {
                    values.Add(input[1], input[2]);
                    keyQueue.Enqueue(input[1]);
                    return "SET OK";
                }
                else
                {
                    //Get the least recently used key to remove from the dictionary
                    string removeKey = keyQueue.Dequeue();
                    //If the key is not in the dictionary, there is an unanticipated error
                    if (!values.Remove(removeKey)) return "PROGRAM ERROR";

                    values.Add(input[1], input[2]);
                    keyQueue.Enqueue(input[1]);
                    return "SET OK";
                }
            }

            return "ERROR";
        }

        //Accepts a string of the desired key, and returns the corresponding value if found
        public string Get(string input)
        {
           string value;
            if (values.TryGetValue(input, out value))
           {
                //If the value at the head was accessed, it's no longer the least recently used
                if(input == keyQueue.Peek())
                {
                    string key = keyQueue.Dequeue();
                    //Put at the end of the queue
                    keyQueue.Enqueue(key);
                }
                return "GOT " + value;
           }

            return "NOTFOUND";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //First input should give size of cache
            string rawInput = Console.ReadLine();
            //Make sure we a valid first line
            if (rawInput.Length == 0) return;
            string[] inputArray = rawInput.Split(' ');
             
            //Make sure we have valid input for cache size
            if (inputArray.Length != 2 && inputArray[0].ToLower() != "size")
            {
                Console.WriteLine("ERROR");
                //Pause to show error
                Console.ReadLine();
                return;
            }

            int cacheSize = Int32.Parse(inputArray[1]);
            LRUCache myCache = new LRUCache(cacheSize);
            Console.WriteLine("SIZE OK");

            bool exitFlag = false;

            //Loop until input is EXIT
            while (!exitFlag)
            {
                rawInput = Console.ReadLine();
                if (rawInput.ToLower() == "exit")
                {
                    exitFlag = true;
                }
                else
                {
                    inputArray = rawInput.Split(' ');
                    
                    //Want to make this case independant
                    switch(inputArray[0].ToLower())
                    {
                        case "set":
                            if(inputArray.Length == 3)
                            {
                                Console.WriteLine(myCache.Set(inputArray));
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                            break;
                        case "get":
                            if(inputArray.Length == 2)
                            {
                                Console.WriteLine(myCache.Get(inputArray[1]));
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
            }
        }
    }
}
