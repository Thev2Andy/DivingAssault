using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntro : MonoBehaviour
{
    public Rigidbody2D Player;
    public Camera Camera;
    public float LaunchSpeed;

    private void Awake()
    {
        Vector3 CameraCenter = Camera.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        Vector3 Direction = CameraCenter - Player.transform.position;

        Player.AddForce((Direction * LaunchSpeed));
        Destroy(this);
    }
}
