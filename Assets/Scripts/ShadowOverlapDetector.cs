using System;
using UnityEngine;

public class ShadowOverlapDetector : MonoBehaviour
{
    // Event to notify when this shadow overlaps with another
    public delegate void ShadowOverlapAction(GameObject overlappingShadow);
    public static event ShadowOverlapAction OnShadowOverlapEnter;
    public static event ShadowOverlapAction OnShadowOverlapExit;
    public bool isCurrent = false;

    private void OnTriggerEnter(Collider other)
    {
        // log other object's tag
        Debug.Log("Overlap Enter: " + other.tag);
        if (isCurrent && (other.CompareTag("Shadow") || other.CompareTag("AdditionalShadowHitbox")))
        {
            Debug.Log(other.tag);
            Debug.Log("Overlap Enter");
            // Notify that this shadow started overlapping with another
            OnShadowOverlapEnter?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCurrent && (other.CompareTag("Shadow") || other.CompareTag("AdditionalShadowHitbox")))
        {
            Debug.Log("Overlap Exit");
            // Notify that this shadow stopped overlapping with another
            OnShadowOverlapExit?.Invoke(other.gameObject);
        }
    }
}
