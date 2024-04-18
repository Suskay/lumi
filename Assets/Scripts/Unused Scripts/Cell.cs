using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isOccupied;
    public Cell(bool isOccupied)
    {
        this.isOccupied = isOccupied;
    }

}
