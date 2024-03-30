using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    [SerializeField]
    public float smoothTime = 0.2f;
    
    public Vector3 velocity = Vector3.zero;
     public bool followPlayer = true;
     public bool rising = false; 
    public Transform goldenPlatform; 
    public float riseSpeed = 2f;
    public float TargetHeight;
  void LateUpdate()
{
    if (target != null && followPlayer)
    {
        
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    if (rising && goldenPlatform != null)
    {
        goldenPlatform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
        if (goldenPlatform.position.y >= TargetHeight)
        {
            rising = false;
        }
    }
}
     public void StopFollowingPlayer()
    {
        followPlayer = false;
    }
      public void AdjustSmoothness(bool smooth)
    {
        if (smooth)
        {
            smoothTime = 0.2f;
        }
        else
        {
            smoothTime = 0.05f;
        }
    }
     public void StartRising(Transform platformTransform)
    {
        rising = true;
        goldenPlatform = platformTransform;
       TargetHeight = 20.2f; 
    }
}