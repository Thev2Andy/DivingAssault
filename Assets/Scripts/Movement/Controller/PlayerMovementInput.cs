using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
    public GravitylessMovement Movement;
    public Camera Camera;

    private void Update()
    {
        Vector3 ViewportLocation = Camera.WorldToViewportPoint(Movement.Rigidbody.transform.position);
        if ((ViewportLocation.x == Mathf.Clamp01(ViewportLocation.x) && ViewportLocation.y == Mathf.Clamp01(ViewportLocation.y))) {
            Movement.Set(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
    }
}
