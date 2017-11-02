using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Heap {

    Tile[] heapTiles;
    int itemCount;

    //replace openList with Heap

    //add heapIndex, compareTo to tile class
    //compare fCost of node then hCost
    //item.compareTo(item2) should return pos if item is less than item2, neg if item is greater than item2

    //create heap
    public Heap(int maxSize)
    {
        heapTiles = new Tile[maxSize];
    }

    //add item into heap and sort it into position
    public void Add(Tile item)
    {
        item.heapIndex = itemCount;
        heapTiles[itemCount] = item;
        SortUp(item);
        itemCount++;
    }

	public void Reset () {
		itemCount = 0;
	}

    //remove first tile and resort heap
    public Tile RemoveFirst()
    {
        Tile first = heapTiles[0];
        itemCount--;
        heapTiles[0] = heapTiles[itemCount];
        heapTiles[0].heapIndex = 0;
        SortDown(heapTiles[0]);
        return first;
    }

	public void Remove (Tile item){

		heapTiles [item.heapIndex].fitness = float.MinValue;
		UpdateItem (item);
		RemoveFirst ();
	}

    //resort item position
    public void UpdateItem(Tile item)
    {
        SortUp(item);
    }

    
    public int Count
    {
        get
        {
            return itemCount;
        }
    }

    //does heap contain item
    public bool Contains(Tile item)
    {
        return Equals(heapTiles[item.heapIndex], item);
    }

    //sort tile lower into heap
    void SortDown(Tile item)
    {
        while (true)
        {
            int leftChildIndex = item.heapIndex * 2 + 1;
            int rightChildIndex = (item.heapIndex * 2) + 2;
            int swapIndex = 0;

            if (leftChildIndex < itemCount)
            {
                swapIndex = leftChildIndex;

                if (rightChildIndex < itemCount)
                {
                    if (heapTiles[leftChildIndex].CompareTo(heapTiles[rightChildIndex]) < 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }

                if (item.CompareTo(heapTiles[swapIndex]) < 0)
                {
                    Swap(item, heapTiles[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    //sort item higher into heap
    void SortUp(Tile item)
    {
        int indexOfParent = (item.heapIndex - 1) / 2;

        while (true)
        {
            Tile parent = heapTiles[indexOfParent];

            if (item.CompareTo(parent) > 0)
            {
                Swap(item, parent);
            }
            else
            {
                break;
            }
            indexOfParent = (item.heapIndex - 1) / 2;
        }
    }

    //swap tiles
    void Swap(Tile item1, Tile item2)
    {
        heapTiles[item1.heapIndex] = item2;
        heapTiles[item2.heapIndex] = item1;
        int tempIndex = item1.heapIndex;
        item1.heapIndex = item2.heapIndex;
        item2.heapIndex = tempIndex;
    }
}