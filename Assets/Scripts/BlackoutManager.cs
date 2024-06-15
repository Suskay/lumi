using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutManager : MonoBehaviour {
    public Image vignetteImage;
    public float duration = 2.0f;
    public static bool isAnimationPlaying = false;

    private void Start()
    {
        ResetVignette();
    }

    public void StartVignetteAnimation() {
        StartCoroutine(AnimateVignette());
    }

    public IEnumerator AnimateVignette() {
        isAnimationPlaying = true;
        float time = 0;
        Material mat = vignetteImage.material;
        float startRadius = 1.5f; // Start with a circle larger than the screen

        while (time < duration) {
            time += Time.deltaTime;
            float normalizedTime = time / duration;
            mat.SetFloat("_ClosingRadius", Mathf.Lerp(startRadius, 0f, normalizedTime));
            yield return null;
        }
        isAnimationPlaying = false;
    }
    
    public void ResetVignette() {
        Material mat = vignetteImage.material;
        mat.SetFloat("_ClosingRadius", 1.5f);
    }
}