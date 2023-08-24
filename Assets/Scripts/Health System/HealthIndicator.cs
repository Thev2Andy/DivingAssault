using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour
{
    public SpriteRenderer Indicator;
    public Gradient Colors;
    public HealthSystem HealthSystem;
    public float ColorInterpolationTime;

    // Private / Hidden variables..
    private float RedVelocity;
    private float GreenVelocity;
    private float BlueVelocity;
    private float AlphaVelocity;


    public void Update()
    {
        float HealthPercentage = ((float)(HealthSystem.Health)) / ((float)(HealthSystem.InitialHealth));
        Color EvaluatedColor = Colors.Evaluate(Mathf.Clamp01(HealthPercentage));

        Color InterpolatedColor = new Color(1f, 1f, 1f, 1f);
        InterpolatedColor.r = Mathf.SmoothDamp(Indicator.color.r, EvaluatedColor.r, ref RedVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));
        InterpolatedColor.g = Mathf.SmoothDamp(Indicator.color.g, EvaluatedColor.g, ref GreenVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));
        InterpolatedColor.b = Mathf.SmoothDamp(Indicator.color.b, EvaluatedColor.b, ref BlueVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));
        InterpolatedColor.a = Mathf.SmoothDamp(Indicator.color.a, EvaluatedColor.a, ref AlphaVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));

        Indicator.color = InterpolatedColor;
    }
}
