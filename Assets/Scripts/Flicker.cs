using UnityEngine;
using System.Collections;

public class FlickeringLight : MonoBehaviour
{
    [Tooltip("The Light component that will flicker.")]
    public Light targetLight;

    [Tooltip("The minimum intensity the light will flicker down to.")]
    [Range(0f, 8f)]
    public float minIntensity = 0.5f;

    [Tooltip("The maximum intensity the light will flicker up to.")]
    [Range(0f, 8f)]
    public float maxIntensity = 2.0f;

    [Tooltip("How quickly the light intensity changes during a flicker.")]
    [Range(0.01f, 0.5f)]
    public float flickerSpeed = 0.1f;

    [Tooltip("How much randomness to add to the flicker speed. Higher values mean more erratic flickering.")]
    [Range(0f, 0.5f)]
    public float randomOffset = 0.1f;

    [Tooltip("If checked, the light will smoothly transition between flicker states.")]
    public bool smoothFlicker = true;

    private float originalIntensity;

    private Coroutine flickerCoroutine;

    void Awake()
    {
        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
            if (targetLight == null)
            {
                Debug.LogError("FlickeringLight: No Light component found on this GameObject or assigned in Inspector.");
                enabled = false; 
                return;
            }
        }
        originalIntensity = targetLight.intensity;
        if (minIntensity > maxIntensity)
        {
            minIntensity = maxIntensity;
        }
    }

    void OnEnable()
    {
        StartFlicker();
    }

    void OnDisable()
    {
        StopFlicker();
    }

    public void StartFlicker()
    {
        StopFlicker(); 
        flickerCoroutine = StartCoroutine(DoFlicker());
    }
    public void StopFlicker()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
    }

    IEnumerator DoFlicker()
    {
        while (true)
        {
            float target = Random.Range(minIntensity, maxIntensity);

            if (smoothFlicker)
            {
                float currentIntensity = targetLight.intensity;
                float timer = 0f;
                float duration = flickerSpeed + Random.Range(0f, randomOffset);

                while (timer < duration)
                {
                    targetLight.intensity = Mathf.Lerp(currentIntensity, target, timer / duration);
                    timer += Time.deltaTime;
                    yield return null;
                }
                targetLight.intensity = target;
            }
            else
            {
                targetLight.intensity = target;
                yield return new WaitForSeconds(flickerSpeed + Random.Range(0f, randomOffset));
            }
        }
    }
    void OnDrawGizmos()
    {
        if (targetLight != null)
        {
            Gizmos.color = targetLight.color;
            Gizmos.DrawWireSphere(targetLight.transform.position, targetLight.range);
        }
    }
}
