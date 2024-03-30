using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    public Vector3 finishPos = Vector3.zero;
    [SerializeField]
    public float speed = 0.5f;

    public Vector3 startPos;
    public float trackPercent = 0f;
    public int direction = 1;


    void Start()
    {
        startPos = transform.position;

    }
     void OnDrawGizmos() { 
  Gizmos.color = Color.red;
  Gizmos.DrawLine(transform.position, finishPos);
 }


    void Update()
    {
        trackPercent += direction * speed * Time.deltaTime;
        float x = (finishPos.x - startPos.x) * trackPercent + startPos.x;
        float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
        transform.position = new Vector3(x,y, startPos.z);
        if ((direction ==1 && trackPercent > 0.9f) ||(direction == -1 && trackPercent < 0.1f))
        {
            direction *= -1;
        }
    }
}
