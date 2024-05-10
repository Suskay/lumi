using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FollowShadow : MonoBehaviour
{
    public Transform currentShadowTransform;
    public ShadowOverlapDetector currentShadowOverlapDetector;
    public List<Transform> overlappingShadows = new List<Transform>();
    public ParticleSystem boostParticles;  // Reference to the particle system

    private int successfulJumps = 0;  // Track successful jumps
    private int boostLevel = 0;   // Flag to check if speed is boosted
    public static bool isClockwise = true; //determines rotation direction
    public delegate void JumpEventHandler(Transform newTree);
    public event JumpEventHandler OnJump;

    private void OnEnable()
    {
        ShadowOverlapDetector.OnShadowOverlapEnter += HandleShadowOverlapEnter;
        ShadowOverlapDetector.OnShadowOverlapExit += HandleShadowOverlapExit;
    }

    private void OnDisable()
    {
        ShadowOverlapDetector.OnShadowOverlapEnter -= HandleShadowOverlapEnter;
        ShadowOverlapDetector.OnShadowOverlapExit -= HandleShadowOverlapExit;
    }

    void Start()
    {
        //fixed the thing where it just started with random tree
        if (currentShadowTransform == null)
        {
            
            // Find the "StartingTree" object in the scene
            GameObject startObject = GameObject.Find("StartingTree");

            // Check if the "Start" object is found
            if (startObject != null)
            {
                // Find the ShadowOverlapDetector script on the "Shadow" child object
                currentShadowOverlapDetector = startObject.GetComponentInChildren<ShadowOverlapDetector>();
                
                // Check if the script is found
                if (currentShadowOverlapDetector != null)
                {
                    Debug.Log("Overlap detector found");
                    currentShadowTransform = startObject.transform.Find("RotationWrapper/Shadow");
                    currentShadowOverlapDetector.isCurrent = true;
                }
                else
                {
                    Debug.LogError("ShadowOverlapDetector script not found on the 'Shadow' child object.");
                }
                UpdatePlayerPosition();
            }
            else
            {
                Debug.LogError("Start object not found in the scene.");
            }
            
        }

        // Set the player's initial position to the center of the current shadow
        UpdatePlayerPosition();
        
        
    }

    void Update()
    {
        UpdatePlayerPosition();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
            if (overlappingShadows.Count != 0)
            {
                Debug.Log("Jumping to new shadow, shadow count: "+overlappingShadows.Count);
                SwitchToNewShadow();
                successfulJumps++;
                CheckForSpeedBoost();
                ScoreManager.Instance.AddJumpPoints(boostLevel);
            }
            else
            {
                ResetBoost();
                successfulJumps = 0;
            }
        }
    }

    private void CheckForSpeedBoost()
    {
            if (successfulJumps >= 3 && boostLevel < 3)
            {
                boostLevel += 1;
                successfulJumps = 0;
                ShadowManager.SetBoosted(boostLevel);
                SoundManager.Instance.PlayBoostActivatedSound();
                boostParticles.Play();
            }
        
    }

    private void ResetBoost()
    {
        if (boostLevel>=1)
        {
            SoundManager.Instance.PlayUnsuccessfulJumpSound();
            boostLevel = 0;
            ShadowManager.SetBoosted(0);
            successfulJumps = 0;
            boostParticles.Stop();
        }
    }

    private void UpdatePlayerPosition()
    {
        if (currentShadowTransform != null)
        {
            Vector3 shadowCenter = currentShadowTransform.position;
            transform.position = new Vector3(shadowCenter.x, transform.position.y, shadowCenter.z);
        }
    }

    private void HandleShadowOverlapEnter(GameObject shadow)
    {
        Transform shadowTransform;

        // Check if the collided object is the additional collider
        
        if (shadow.CompareTag("AdditionalShadowHitbox"))
        {
            Debug.Log("Additional shadow hitbox detected.");
            // Find the corresponding main shadow object
            shadowTransform = FindCorrespondingShadow(shadow.transform);
        }
        else
        {
            shadowTransform = shadow.transform;
        }
        if (shadowTransform != null && !overlappingShadows.Contains(shadowTransform))
        {
            overlappingShadows.Add(shadowTransform);
        }
    }

    private void HandleShadowOverlapExit(GameObject shadow)
    {
        if (shadow.CompareTag("AdditionalShadowHitbox"))
        {
            Transform shadowTransform = FindCorrespondingShadow(shadow.transform);
            if (shadowTransform != null)
            {
                overlappingShadows.Remove(shadowTransform);
            }
        }
        else
        {
            overlappingShadows.Remove(shadow.transform);
        }
    }
    
    // Method to find the corresponding main shadow object for an additional collider
    private Transform FindCorrespondingShadow(Transform additionalShadowTransform)
    {
        // find the main shadow object, which is a child, of a sibling (RotationWrapper, of the parent object)
        Transform shadowTransform = additionalShadowTransform.parent.Find("RotationWrapper/Shadow").transform;
        
        // If not found as a direct child, you might need to implement more complex logic to locate the main shadow
        if (shadowTransform == null)
        {
            Debug.LogError("Corresponding main shadow not found for additional collider.");
        }

        return shadowTransform;
    }

    private void SwitchToNewShadow()
    {
        Transform newShadow = overlappingShadows[0];

        if (newShadow != null)
        {
            currentShadowOverlapDetector.isCurrent = false; // set old one to false, new to true
            currentShadowOverlapDetector = newShadow.GetComponentInChildren<ShadowOverlapDetector>();
            currentShadowOverlapDetector.isCurrent = true;
            currentShadowTransform = newShadow;
            overlappingShadows.Clear();

            UpdatePlayerPosition();
            RotateShadow currentRotateShadow = currentShadowTransform.parent.GetComponent<RotateShadow>();
            if (currentRotateShadow != null)
            {
                CheckForSpeedBoost();
            }

            CheckpointTree checkpointTree = newShadow.GetComponentInParent<CheckpointTree>();
            if (checkpointTree != null && !checkpointTree.isUsed)
            {
                TimerAndMovement.IncreaseTimer(15f);
                checkpointTree.UseTree();
                SurvivalStatsManager.IncrementCheckpointsReached();
                Debug.Log("Checkpoint reached");
            }

            Tree tree = newShadow.GetComponentInParent<Tree>();
            if (tree != null && !tree.isUsed)
            {
                TimerAndMovement.IncreaseTimer(0.7f);
                tree.UseTree();
                SurvivalStatsManager.IncrementTotalJumps();
                Debug.Log("Tree reached");
            }

            // Add case for EndTree
            EndTree endTree = newShadow.GetComponentInParent<EndTree>();
            if (endTree != null && !endTree.isUsed)
            {
                endTree.UseTree();
                Debug.Log("End tree reached");
            }

            OnJump?.Invoke(currentShadowTransform.parent);
        }
    }

    private bool IsOverlappingWithCurrentShadow(Transform shadowTransform)
    {
        Collider currentShadowCollider = currentShadowTransform.GetComponent<Collider>();
        Collider otherShadowCollider = shadowTransform.GetComponent<Collider>();
        
        Debug.Log(currentShadowCollider, otherShadowCollider);

        if (currentShadowCollider == null || otherShadowCollider == null)
        {
            return false;
        }
        
        Debug.Log("Current shadow bounds: " + currentShadowCollider.bounds);

        return currentShadowCollider.bounds.Intersects(otherShadowCollider.bounds);
    }
    
}