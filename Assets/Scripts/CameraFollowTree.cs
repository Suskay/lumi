using UnityEngine;
using System.Collections;

public class CameraFollowTree : MonoBehaviour
{
    public FollowShadow followShadow;
    public Vector3 offset;
    public float transitionDuration = 1f;

    private Vector3 targetPosition;
    private Coroutine movingCoroutine;

    void Start()
    {
        followShadow.OnJump += OnLumiJump;

        // Start a coroutine to set the initial camera position after all Start() methods are called
        StartCoroutine(SetInitialCameraPositionAfterFrame());
    }

    private IEnumerator SetInitialCameraPositionAfterFrame()
    {
        // Wait until the end of the first frame
        yield return new WaitForEndOfFrame();

        // Now, set the initial camera position
        SetInitialCameraPosition();
    }

    private void SetInitialCameraPosition()
    {
        if (followShadow != null && followShadow.currentShadowTransform != null)
        {
            Transform initialTreeTransform = followShadow.currentShadowTransform.parent;
            if (initialTreeTransform != null)
            {
                transform.position = initialTreeTransform.position + offset;
            }
        }
    }

    private void OnLumiJump(Transform newTree)
    {
        if (movingCoroutine != null)
        {
            StopCoroutine(movingCoroutine);
        }

        targetPosition = newTree.position + offset;
        movingCoroutine = StartCoroutine(MoveCamera(targetPosition));
    }

    private IEnumerator MoveCamera(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        movingCoroutine = null;
    }

    void OnDestroy()
    {
        if (followShadow != null)
        {
            followShadow.OnJump -= OnLumiJump;
        }
    }
}
