using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitylessMovement : MonoBehaviour
{
    public Rigidbody2D Rigidbody;
    public float Acceleration;
    public float MaxSpeed;

    public float TiltAmount;
    public float TiltMaxSpeed;
    public bool FlipTilt;
    public AnimationCurve TiltGraph;
    public float TiltInterpolationTime;


    // Private / Hidden variables..
    private Vector2 MovementAxes;
    private float TiltVelocity;


    private void FixedUpdate()
    {
        Vector2 MovementForce = MovementAxes * Acceleration;
        Rigidbody.AddForce(MovementForce);
        Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, MaxSpeed);


        float HorizontalSpeed = ((Rigidbody.velocity * Vector2.right).magnitude);
        float TiltSpeedProgress = Mathf.Clamp01((HorizontalSpeed / TiltMaxSpeed));
        float TiltTweenTime = TiltGraph.Evaluate(TiltSpeedProgress);
        float TiltAngle = ((TiltTweenTime * TiltAmount * ((Rigidbody.velocity.x <= 0f) ? 1f : -1f)) * ((!FlipTilt) ? 1f : -1f));

        float CurrentTiltAngle = this.transform.eulerAngles.z;
        if ((TiltAngle - this.transform.eulerAngles.z) > 180f) {
            CurrentTiltAngle += 360f;
        }

        else if ((TiltAngle - CurrentTiltAngle) < -180f) {
            CurrentTiltAngle -= 360f;
        }

        float InterpolatedTiltAngle = Mathf.SmoothDamp(CurrentTiltAngle, TiltAngle, ref TiltVelocity, (1 - Mathf.Exp(-TiltInterpolationTime * Time.deltaTime)));
        Vector3 TiltRotation = new Vector3(0f, 0f, InterpolatedTiltAngle);
        this.transform.eulerAngles = TiltRotation;


        MovementAxes = Vector2.zero;
    }

    public void Set(Vector2 MovementAxes) {
        this.MovementAxes = new Vector2(Mathf.Clamp(MovementAxes.x, -1f, 1f), Mathf.Clamp(MovementAxes.y, -1f, 1f));
    }
}