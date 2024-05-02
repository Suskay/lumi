using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenConnection : MonoBehaviour
{
    public float connectionLength = 2.0f;
    public int connectionDirection = -1;
    // 0 = south (z-), 1 = west(x-), 2 = north(z+), 3 = east(x+)
    //-1 = no connection
    // Trees should not be generated in south direction 
}
  