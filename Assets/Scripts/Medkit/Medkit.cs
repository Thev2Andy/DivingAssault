using CameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public AudioClip PickupSound;
    public float PickupSoundVolume;
    public GameObject PickupEffect;

    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (Collision.gameObject.tag == "Player" && Collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem PlayerHealthSystem) && PlayerHealthSystem.Health < PlayerHealthSystem.InitialHealth)
        {
            PlayerHealthSystem.Health = PlayerHealthSystem.InitialHealth;
            Camera.main.GetComponent<AudioSource>()?.PlayOneShot(PickupSound, PickupSoundVolume);

            Instantiate(PickupEffect, PlayerHealthSystem.transform.position, Quaternion.identity);

            if (CameraShaker.Instance != null) {
                CameraShaker.Instance.ShakeOnce((19.5f * (float.Parse((Settings.Get("Screenshake Intensity", 1f).ToString())))), 1f, 0f, 0.65f);
            }

            Destroy(this.gameObject);
        }
    }
}
