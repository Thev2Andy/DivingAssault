using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public GameObject BrokenPrefab;
    public Rigidbody2D Rigidbody;
    public AudioSource AudioSource;
    public AudioClip BreakSound;
    public AudioClip HitSound;
    public GameObject[] PossibleItems;
    public float ExplosionForce;
    public float ExplosionRadius;
    [Range(0f, 100f)] public float DropChance;
    public int LabelIndexOverride = -1;
    public List<string> Labels;
    public TMP_Text LabelText;
    public int Health;

    // Private / Hidden variables.
    [HideInInspector] public float InitialHealth;


    public void Damage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0)
        {
            GameObject BrokenCrate = Instantiate(BrokenPrefab, this.transform.position, this.transform.rotation);
            Rigidbody2D[] BrokenCrateFragments = BrokenCrate.GetComponentsInChildren<Rigidbody2D>(true);

            BrokenCrate.GetComponent<AudioSource>()?.PlayOneShot(BreakSound);

            bool SpawnDrop = Random.Range(0f, 100f) <= DropChance;
            if (SpawnDrop && PossibleItems.Length > 0)
            {
                GameObject Drop = Instantiate(PossibleItems[Random.Range(0, PossibleItems.Length)], BrokenCrate.transform.position, BrokenCrate.transform.rotation);
                if (Drop.TryGetComponent<Rigidbody2D>(out Rigidbody2D DropRigidbody)) {
                    DropRigidbody.velocity = Rigidbody.velocity;
                    DropRigidbody.angularVelocity = Rigidbody.angularVelocity;
                }
            }

            for (int I = 0; I < BrokenCrateFragments.Length; I++)
            {
                BrokenCrateFragments[I].velocity = Rigidbody.velocity;
                BrokenCrateFragments[I].angularVelocity = Rigidbody.angularVelocity;

                BrokenCrateFragments[I].AddExplosionForce(ExplosionForce, this.transform.position, ExplosionRadius);
            }


            TMP_Text LabelFragment = BrokenCrate.GetComponentInChildren<TMP_Text>(true);
            LabelFragment.text = LabelText.text;


            Destroy(this.gameObject);
        }

        if (Health > 0) {
            AudioSource.PlayOneShot(HitSound);
        }
    }



    private void Awake()
    {
        Health = Mathf.Max(Health, 1);
        InitialHealth = Health;

        if (Labels.Count > 0 && Labels.Count > LabelIndexOverride) {
            LabelText.text = Labels[((LabelIndexOverride < 0) ? Random.Range(0, Labels.Count) : LabelIndexOverride)];
        }
    }
}
