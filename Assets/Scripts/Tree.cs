using System;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool isUsed = false; // Flag to check if the checkpoint tree has been used

    // method that turns isUsed to true and plays particle effect on tree object
    public void UseTree()
    {
        isUsed = true;
        
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, 6.5f);
    }
    
   
}