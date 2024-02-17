using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using CameraShake;

public class HealthSystem : MonoBehaviour
{
    public GameObject RagdollPrefab;
    public SpriteRenderer PlayerRenderer;
    public GameObject DeathUI;
    public float DeathScreenDelay;
    public Color[] PlayerColorPalette;
    public float ColorInterpolationTime;
    public Color DamageColor;
    public GameObject[] PlayerControllerObjects;
    public GameObject[] ObjectsToDisable;
    public float KnockbackMultiplier;
    public float StaticRagdollTorqueMultiplier;
    public float RagdollTorqueForce;
    public Rigidbody2D Rigidbody;
    public AudioSource AudioSource;
    public AudioClip DeathSound;
    public AudioClip HitSound;
    public Inventory Inventory;
    public Volume HurtFXVolume;
    public int Health;

    // Private / Hidden variables..
    [HideInInspector] public Color SelectedColor;
    [HideInInspector] public int InitialHealth;
    [HideInInspector] public int LastHealth;
    [HideInInspector] public bool IsDead;
    [HideInInspector] public bool Stunned;
    private bool WasDamaged;
    private float RedVelocity;
    private float GreenVelocity;
    private float BlueVelocity;
    private float AlphaVelocity;


    private void Update()
    {
        if (!PauseMenu.Instance.IsPaused)
        {
            float HurtWeightTarget = (1f - (((float)Health) / ((float)InitialHealth)));
            HurtFXVolume.weight = Mathf.Lerp(HurtFXVolume.weight, HurtWeightTarget, (1 - Mathf.Exp(-0.95f * Time.deltaTime)));


            Color InterpolatedColor = new Color(1f, 1f, 1f, 1f);
            InterpolatedColor.r = Mathf.SmoothDamp(PlayerRenderer.color.r, SelectedColor.r, ref RedVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));
            InterpolatedColor.g = Mathf.SmoothDamp(PlayerRenderer.color.g, SelectedColor.g, ref GreenVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));
            InterpolatedColor.b = Mathf.SmoothDamp(PlayerRenderer.color.b, SelectedColor.b, ref BlueVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));
            InterpolatedColor.a = Mathf.SmoothDamp(PlayerRenderer.color.a, SelectedColor.a, ref AlphaVelocity, (1 - Mathf.Exp(-ColorInterpolationTime * Time.deltaTime)));

            PlayerRenderer.color = InterpolatedColor;


            if (Health < LastHealth && !WasDamaged)
            {
                int Difference = LastHealth - Health;
                Health = LastHealth;
                this.Damage(Difference, Random.insideUnitCircle);
            }


            if (Health <= 0 && !IsDead)
            {
                GameObject RagdollInstance = Instantiate(RagdollPrefab, this.transform.position, this.transform.rotation);
                RagdollInstance.GetComponent<SpriteRenderer>().color = SelectedColor;

                Rigidbody2D RagdollRigidbody = RagdollInstance.GetComponent<Rigidbody2D>();
                RagdollRigidbody.velocity = Rigidbody.velocity;
                RagdollRigidbody.angularVelocity = Rigidbody.angularVelocity;
                RagdollRigidbody.AddTorque((RagdollTorqueForce * -((!float.IsNaN((RagdollRigidbody.velocity.x / Mathf.Abs(RagdollRigidbody.velocity.x))) ? (RagdollRigidbody.velocity.x / Mathf.Abs(RagdollRigidbody.velocity.x)) : 1f))) * ((RagdollRigidbody.velocity.x == 0f) ? (StaticRagdollTorqueMultiplier * ((Random.Range(-1, 2) >= 0) ? 1 : -1)) : 1f));


                PromptController.Instance.Clear();
                Inventory?.Drop(false);
                IsDead = true;



                for (int I = 0; I < PlayerControllerObjects.Length; I++) {
                    PlayerControllerObjects[I].SetActive(false);
                }

                for (int I = 0; I < ObjectsToDisable.Length; I++) {
                    ObjectsToDisable[I].SetActive(false);
                }

                Rigidbody.simulated = false;
                StartCoroutine(EnableDeathUI());
            }


            LastHealth = Health;
            WasDamaged = false;
        }
    }

    public void Damage(int Damage, Vector2? DamageLocation = null)
    {
        if (!PauseMenu.Instance.IsPaused)
        {
            if (!IsDead)
            {
                DamageLocation = ((DamageLocation != null) ? DamageLocation : new Vector2(this.transform.position.x, this.transform.position.y));
                Vector2 KnockbackDirection = new Vector2(this.transform.position.x, this.transform.position.y) - new Vector2((((Vector2)DamageLocation).x), (((Vector2)DamageLocation).y));
                KnockbackDirection.Normalize();
                Damage = Mathf.Max(Damage, 0);

                Rigidbody.velocity += (KnockbackDirection * (Damage / 5) * KnockbackMultiplier);

                if ((Health - Damage) < Health) {
                    PlayerRenderer.color = DamageColor;
                }


                WasDamaged = true;
                Health -= Damage;

                if (CameraShaker.Instance != null) {
                    CameraShaker.Instance.ShakeOnce((9.5f * (Damage / 100f) * (float.Parse((Settings.Get("Screenshake Intensity", 1f).ToString())))), 1.95f, 0.15f, 0.85f, (Vector3.one * 0.15f), Vector3.one);
                }

                AudioSource.PlayOneShot(((Health <= 0) ? DeathSound : HitSound));
            }
        }
    }

    public IEnumerator EnableDeathUI()
    {
        yield return new WaitForSeconds(DeathScreenDelay);
        DeathUI.SetActive(true);
    }



    private void Awake()
    {
        PlayerRenderer.color = PlayerColorPalette[Random.Range(0, PlayerColorPalette.Length)];
        SelectedColor = PlayerRenderer.color;

        Health = Mathf.Max(Health, 1);
        InitialHealth = Health;
    }
}
