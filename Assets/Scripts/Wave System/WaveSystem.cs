using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    // Public / Accessible variables..
    [Header("Objects.")]
    public ObjectDefinition[] AvailableObjects;

    [Header("Spawning.")]
    public SpawnRule[] SpawnRules;
    public float BaseWaveInterval;
    public float StartDelay;
    public bool DifficultyAffectsSpawnInterval;
    public float TimeAllowedOutsideViewport;
    public float LinearLaunchSpeed;
    public float AngularLaunchSpeed;

    [Header("References.")]
    public HealthSystem HealthSystem;
    public Camera Camera;
    public AudioSource AudioSource;
    public AudioClip WaveSound;

    [Header("UI.")]
    public CanvasGroup WaveUI;
    public TMP_Text WaveText;
    public float WaveUIInterpolationTime;
    public float WaveUIDuration;

    // Private / Hidden variables..
    private List<SpawnedObject> SpawnedObjects;
    private float WaveUILifetime;
    private float WaveUIVelocity;
    private float WaveCountdown;

    // Properties..
    public int Wave { get; private set; }

    // Singletons..
    public static WaveSystem Instance;


    public IEnumerator SpawnWave(bool ShowUI = true)
    {
        WaveCountdown = BaseWaveInterval / ((DifficultyAffectsSpawnInterval) ? Mathf.Clamp(((int.Parse((Settings.Get("Difficulty", 1).ToString()))) + 1), 1, int.MaxValue) : 1f);
        Wave += 1;
        if (ShowUI) {
            this.ShowWaveUI();
        }


        if (SpawnedObjects != null)
        {
            for (int I = 0; I < SpawnedObjects.Count; I++)
            {
                if (SpawnedObjects[I].Object == null) {
                    SpawnedObjects[I] = null;
                    continue;
                }

                Vector2 ViewportLocation = Camera.WorldToViewportPoint(SpawnedObjects[I].Object.transform.position);
                if ((ViewportLocation.x != Mathf.Clamp01(ViewportLocation.x) || ViewportLocation.y != Mathf.Clamp01(ViewportLocation.y)) && (System.DateTime.Now - SpawnedObjects[I].Time).TotalSeconds >= TimeAllowedOutsideViewport) {
                    Destroy(SpawnedObjects[I].Object);
                    SpawnedObjects[I] = null;
                }
            }

            SpawnedObjects.RemoveAll((ObjectToRemove) => { return ObjectToRemove == null; });
        }

        else {
            SpawnedObjects = new List<SpawnedObject>();
        }


        foreach (SpawnRule CurrentSpawnRule in SpawnRules)
        {
            if (Evaluator.Converter.AsBooleanFromIntegerString(Evaluator.Evaluate(CurrentSpawnRule.Condition, new WaveSystemContext(Wave))))
            {
                foreach (SpawnRule.Object SpawnruleObject in CurrentSpawnRule.Objects)
                {
                    if (!SpawnruleObject.PerInstanceDelay) {
                        yield return new WaitForSeconds(SpawnruleObject.Delay);
                    }

                    for (int I = 0; I < SpawnruleObject.Count; I++)
                    {
                        if (SpawnruleObject.PerInstanceDelay) {
                            yield return new WaitForSeconds(SpawnruleObject.Delay);
                        }


                        int ObjectDefinitionIndex = -1;
                        for (int J = 0; J < AvailableObjects.Length; J++)
                        {
                            if (SpawnruleObject.Identifier == AvailableObjects[J].Identifier) {
                                ObjectDefinitionIndex = J;
                            }
                        }


                        Vector2 SpawnPoint = Camera.ViewportToWorldPoint(this.GetRandomPositionOutsideViewport());
                        GameObject SpawnedObject = Instantiate(AvailableObjects[ObjectDefinitionIndex].Object, SpawnPoint, Quaternion.identity);
                        SpawnedObjects.Add(new WaveSystem.SpawnedObject(SpawnedObject, System.DateTime.Now));
                        if (AvailableObjects[I].Launch && SpawnedObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D Rigidbody))
                        {
                            Vector2 LaunchTarget = Camera.ViewportToWorldPoint(new Vector3(Random.value, Random.value));
                            Rigidbody.velocity = (LaunchTarget - SpawnPoint).normalized * LinearLaunchSpeed;
                            Rigidbody.angularVelocity = Random.value * AngularLaunchSpeed;
                        }
                    }
                }
            }
        }
    }

    public void ShowWaveUI()
    {
        WaveText.text = $"Wave <color=lightblue>{Wave}</color>.";
        WaveUILifetime = WaveUIDuration;
        AudioSource.PlayOneShot(WaveSound);
    }

    private Vector2 GetRandomPositionOutsideViewport() {
        return new Vector2(((Random.value < 0.5f) ? Random.Range(-2f, -0.5f) : Random.Range(1.5f, 2f)), ((Random.value < 0.5f) ? Random.Range(-2f, -0.5f) : Random.Range(1.5f, 2f)));
    }

    private void Update()
    {
        WaveUILifetime = Mathf.Max((WaveUILifetime - Time.deltaTime), 0f);
        WaveUI.alpha = Mathf.SmoothDamp(WaveUI.alpha, ((WaveUILifetime > 0f) ? 1f : 0f), ref WaveUIVelocity, (1 - Mathf.Exp(-WaveUIInterpolationTime * Time.deltaTime)));

        WaveCountdown = Mathf.Max((WaveCountdown - Time.deltaTime), 0f);
        if (WaveCountdown <= 0f && !HealthSystem.IsDead) {
            StartCoroutine(SpawnWave(true));
        }
    }

    private void Start() {
        WaveCountdown = StartDelay;
    }



    private void Awake()
    {
        if (Instance != this && Instance != null) Destroy(this);
        Instance = this;
    }



    [System.Serializable] public class SpawnRule
    {
        public SpawnRule.Object[] Objects;
        public string Condition;



        [System.Serializable] public class Object
        {
            public string Identifier;
            public float Delay;
            public float Count;
            public bool PerInstanceDelay;
        }
    }

    [System.Serializable] public class ObjectDefinition
    {
        public string Identifier;
        public GameObject Object;
        public bool Launch;
    }

    public class SpawnedObject
    {
        public GameObject Object;
        public System.DateTime Time;



        public SpawnedObject(GameObject Object, System.DateTime Time) {
            this.Object = Object;
            this.Time = Time;
        }
    }
}
