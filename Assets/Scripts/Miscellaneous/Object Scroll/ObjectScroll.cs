using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroll : MonoBehaviour
{
    public Transform Object;
    public float Speed;
    public float BottomPoint;
    public float TopPoint;

    private void Update()
    {
        Vector3 ScrollDelta = Vector2.zero;
        ScrollDelta.y = Speed * Time.deltaTime;

        Object.transform.position += ScrollDelta;

        if (Object.transform.position.y >= TopPoint) {
            Object.transform.position = new Vector3(Object.transform.position.x, BottomPoint, Object.transform.position.z);
        }
    }
}
