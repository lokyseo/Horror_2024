using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public Vector3 worldPosition;

    public Node(bool _walable, Vector3 _worldPos)
    {
        walkable = _walable;
        worldPosition = _worldPos;
    }
}
