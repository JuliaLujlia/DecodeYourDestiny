using UnityEngine;
using System.Collections; // For IEnumerator

public class DynamicCrystalEffect : MonoBehaviour
{
    [Header("Material References")]
    [Tooltip("The MeshRenderer component of the crystal.")]
    public MeshRenderer crystalRenderer;

    [Header("Rotation Settings")]
    [Tooltip("Speed of rotation around the Y-axis.")]
    public float rotationSpeed = 30f;

    [Header("Shine/Pulse Settings")]
    [Tooltip("Enable / Disable the glowing pulse effect.")]
    public bool enablePulse = true;
    [Tooltip("The base emission color of the crystal (from the shader).")]
    public Color baseEmissionColor = Color.white;
    [Tooltip("How frequently the crystal pulses (pulses per second).")]
    public float pulseFrequency = 1f;
    [Tooltip("Maximum multiplier for the emission color during pulse.")]
    [Range(1f, 5f)] // Clamp value for reasonable glow
    public float maxPulseIntensity = 3f;
    [Tooltip("Minimum multiplier for the emission color during pulse.")]
    [Range(0f, 1f)] // Ensure it doesn't go below 0
    public float minPulseIntensity = 0.1f; // Keep a slight glow even at min

    private Material _crystalMaterialInstance; // To avoid modifying shared material
    private float _currentPulseValue;

    // Shader Property IDs (good practice for performance)
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");
    private static readonly int EmissionMultiplierID = Shader.PropertyToID("_EmissionMultiplier"); // Name of your exposed multiplier property

    void Awake()
    {
        // Get the renderer if not assigned in Inspector
        if (crystalRenderer == null)
        {
            crystalRenderer = GetComponent<MeshRenderer>();
        }

        if (crystalRenderer == null)
        {
            Debug.LogError("DynamicCrystalEffect: No MeshRenderer found on this GameObject or assigned. Please assign it or add a MeshRenderer.", this);
            enabled = false; // Disable script if no renderer
            return;
        }

        // Create an instance of the material to avoid modifying the shared asset
        _crystalMaterialInstance = crystalRenderer.material;

        // Ensure the shader has the emission properties
        if (!_crystalMaterialInstance.HasProperty(EmissionColorID) || !_crystalMaterialInstance.HasProperty(EmissionMultiplierID))
        {
            Debug.LogWarning("DynamicCrystalEffect: The assigned shader does not have '_EmissionColor' or '_EmissionMultiplier' properties. Disabling pulse.", this);
            enablePulse = false;
        }
        else
        {
            // Set initial emission color from script if not already set by shader default
            _crystalMaterialInstance.SetColor(EmissionColorID, baseEmissionColor);
            _crystalMaterialInstance.SetFloat(EmissionMultiplierID, minPulseIntensity); // Start at minimum
        }
    }

    void Update()
    {
        // Continuous Rotation (no interaction needed)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        // Dynamic Shine/Pulse Effect (no interaction needed)
        if (enablePulse)
        {
            // Use a sine wave for smooth pulsing between min and max intensity
            // Mathf.PingPong is also good for a back-and-forth motion
            _currentPulseValue = Mathf.Lerp(minPulseIntensity, maxPulseIntensity, (Mathf.Sin(Time.time * pulseFrequency * Mathf.PI * 2) * 0.5f + 0.5f));
            _crystalMaterialInstance.SetFloat(EmissionMultiplierID, _currentPulseValue);
        }
    }

    void OnDestroy()
    {
        // Clean up the instantiated material when the GameObject is destroyed
        if (_crystalMaterialInstance != null)
        {
            Destroy(_crystalMaterialInstance);
        }
    }

    // Optional: Call this from an external script to change base color
    public void SetCrystalColor(Color newColor)
    {
        if (_crystalMaterialInstance != null)
        {
            baseEmissionColor = newColor; // Update base for pulsing
            _crystalMaterialInstance.SetColor(EmissionColorID, baseEmissionColor);
        }
    }
}