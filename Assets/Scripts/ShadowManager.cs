using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    
    public static float startingRotationSpeed = 50f;
    public static float rotationSpeed;
    private static float count = 0;
    public static float currentRotation = 0f; // Current rotation angle
    public static int boostLevel = 0;
    public static float boostedSpeedMultiplier = 1.3f;  // Speed multiplier when boosted
    public static float maxBoostedSpeed = 200f; // Maximum speed when boosted
    private bool isRotating = false;
    private static float accumulatedSpeed = 0;
    float newCount;
    public BlackoutManager blackoutManager;
    
    void Update()
    {
        
        // Calculation for angle to rotate shadow around the tree
        if (Input.GetKey(KeyCode.Space) && !GameManager.Instance.BlockInput())
        {
            if (!isRotating)
            {
                isRotating = true;
            }
            
            count += 2f * Time.deltaTime;//reaches max speed when count == 4.1
            if (count >= 4.1f) count = 4.1f;
            rotationSpeed = startingRotationSpeed + accumulatedSpeed + Mathf.Pow(count, 2) + count * 2;
            
            if ( boostLevel >=1 )
            {
                rotationSpeed = startingRotationSpeed + accumulatedSpeed + (20 + (Mathf.Pow(count, 2) + count * 2)) * boostModifier();  // Apply speed boost
                maxBoostedSpeed = startingRotationSpeed + boostModifier() * startingRotationSpeed;
                rotationSpeed = Mathf.Min(rotationSpeed, maxBoostedSpeed * boostModifier() / 1.5f); // Limit the speed when boosted
                // increment total boost time
                SurvivalStatsManager.IncrementTotalBoostTime(Time.deltaTime);
            }
            else
            {
                if (rotationSpeed >= startingRotationSpeed * 1.5f)
                    rotationSpeed = startingRotationSpeed * 1.5f;
            }

            //if (rotationSpeed >= startingRotationSpeed * 1.5f) rotationSpeed = startingRotationSpeed * 1.5f;
            currentRotation += rotationSpeed * Time.deltaTime;
            currentRotation %= 360; // Keep the rotation value between 0 and 360
            transform.Rotate( rotationSpeed * Time.deltaTime * Vector3.up);
        }
        else
        {
            if (isRotating)
            {
                count = count / 2;
                accumulatedSpeed = (rotationSpeed - startingRotationSpeed) / 2;
                isRotating = false;
            }

            accumulatedSpeed -= Time.deltaTime * accumulatedSpeed/ 2;
            rotationSpeed = 0;
            count -= Time.deltaTime * count / 2;
            if (count < 0) count = 0;
        }
    }
    public static void SetBoosted(int boostLevel)
    {
            ShadowManager.boostLevel = boostLevel;
    }

    private float boostModifier()
    {
        return Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt(boostLevel)))* boostedSpeedMultiplier;
    }

    public static void reset()
    {
        count = 0;
        rotationSpeed = 0;
        currentRotation = 0;
        boostLevel = 0;
        accumulatedSpeed = 0;
    }
}