using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class BoostManager : MonoBehaviour
{
    private  int successfulJumps;
    private  int boostLevel;
    private  bool isBoosted;
    private  double boostModifier;
    
    // Start is called before the first frame update
    void Start()
    {
        successfulJumps = 0;
        boostLevel = 0;
        isBoosted = false;
        boostModifier = 1.0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void CheckForSpeedBoost()
    {
        if (successfulJumps >= 3 && !isBoosted)
        {
            isBoosted = true;
            ShadowManager.SetBoosted(true);
            SoundManager.Instance.PlayBoostActivatedSound();
            FollowShadow.boostParticles.Play();
        }
    }
    public  void ResetBoost()
    {
        if (isBoosted)
        {
            SoundManager.Instance.PlayUnsuccessfulJumpSound();
            isBoosted = false;
            ShadowManager.SetBoosted(false);
            successfulJumps = 0;
            FollowShadow.boostParticles.Stop();
        }
        else
        {
            successfulJumps = 0;
        }
    }
    
    
}
*/