using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInput : MonoBehaviour
{
    public GravitylessMovement Movement;

    private void Update() {
        Movement.Set(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
}
