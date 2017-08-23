LRU (Least Recently Used) Cache

We need to randomly access key-value pairs, but we need to also track when they were accessed.  
A hash table data structure is ideal for random acces, with an average storage and retrieval complexity of O(1).  
However a worst-case scenario can be O(n), if there is a collision
The C# Dictionary class is used for the hash table structure.

The Queue data structure will be used for tracking when keys were last used.  
The C# Queue class will be used for the queue data structure, and Enqueue and deque both have O(1) complexity.