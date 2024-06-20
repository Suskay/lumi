using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShadow : MonoBehaviour
{
    
    public float defaultLength = 2.0f; // default length of the shadow
    public float length = 2.0f; // current length of the shadow
    private float count = 0;
    private float currentRotation = 0f; // Current rotation angle

    private int boostLevel = 0;
    public float boostedSpeedMultiplier = 50f;  // Speed multiplier when boosted

    void Start()
    {
        Quaternion q = Quaternion.Euler(0, 0, 0);
        transform.rotation = q;
    }
    void Update()
    {
        // rotation angle is calculated in Manager Class for all trees, it should always be same
        // but each tree still does its own shadow scaling calculation(because we might have trees of different height, shape)
        float rotationSpeed = ShadowManager.rotationSpeed;
        //transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
        currentRotation = ShadowManager.currentRotation;
        transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        float widthFactor = CalculateShadowWidth(currentRotation);
        transform.localScale = new Vector3(widthFactor, transform.localScale.y, transform.localScale.z);
        boostLevel = ShadowManager.boostLevel; //so i just take boostLvl 
    }
    

    private float CalculateShadowWidth(float rotationAngle)
    {
        float normalizedAngle = rotationAngle / 360f; // Normalize angle to range [0, 1]
        
        float change = Mathf.Sin(normalizedAngle * Mathf.PI)/2f; // Sin function makes size change smoother
        
        return defaultLength - change; 
    }
    
}
