using UnityEngine;

public class DynamicCrystalEffect : MonoBehaviour
{
    [Header("Material References")]
    [Tooltip("The MeshRenderer component of the crystal.")]
    public MeshRenderer crystalRenderer;

    [Header("Shine/Pulse Settings")]
    [Tooltip("Enable / Disable the glowing pulse effect.")]
    public bool enablePulse = true;
    [Tooltip("The base emission color of the crystal (from the shader).")]
    public Color baseEmissionColor = Color.white;
    [Tooltip("How frequently the crystal pulses (pulses per second).")]
    public float pulseFrequency = 1f;
    [Tooltip("Maximum multiplier for the emission color during pulse (brighter).")]
    [Range(1f, 5f)] 
    public float maxPulseIntensity = 3f;
    [Tooltip("Minimum multiplier for the emission color during pulse (dimmer).")]
    [Range(0f, 1f)] 
    public float minPulseIntensity = 0.1f;

    private Material _crystalMaterialInstance; 
    private float _currentPulseValue; 

    
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
    private static readonly int EmissionMultiplierID = Shader.PropertyToID("_EmissionMultiplier"); 

    void Awake()
    {
      
        if (crystalRenderer == null)
        {
            crystalRenderer = GetComponent<MeshRenderer>();
        }

        
        if (crystalRenderer == null)
        {
            Debug.LogError("DynamicCrystalEffect: No MeshRenderer found on this GameObject or assigned. Please assign it or add a MeshRenderer.", this);
            enabled = false;
            return; 
        }
        _crystalMaterialInstance = crystalRenderer.material;
        if (!_crystalMaterialInstance.HasProperty(EmissionColorID) || !_crystalMaterialInstance.HasProperty(EmissionMultiplierID))
        {
            Debug.LogWarning("DynamicCrystalEffect: The assigned shader does not have '_EmissionColor' or '_EmissionMultiplier' properties. Pulsing functionality will be disabled.", this);
            enablePulse = false; 
        }
        else
        {
            
            _crystalMaterialInstance.SetColor(EmissionColorID, baseEmissionColor);
            _crystalMaterialInstance.SetFloat(EmissionMultiplierID, minPulseIntensity); 
        }
    }

    void Update()
    {
      
        if (enablePulse)
        {
         
            float sineWaveValue = (Mathf.Sin(Time.time * pulseFrequency * Mathf.PI * 2) * 0.5f + 0.5f);

            
            _currentPulseValue = Mathf.Lerp(minPulseIntensity, maxPulseIntensity, sineWaveValue);

            
            _crystalMaterialInstance.SetFloat(EmissionMultiplierID, _currentPulseValue);
        }
    }

    void OnDestroy()
    {
 
        if (_crystalMaterialInstance != null)
        {
            Destroy(_crystalMaterialInstance);
        }
    }

    public void SetCrystalColor(Color newColor)
    {
        if (_crystalMaterialInstance != null)
        {
            baseEmissionColor = newColor; 
            _crystalMaterialInstance.SetColor(EmissionColorID, baseEmissionColor); 
        }
    }
}