using UnityEngine;

/// <summary>
/// GhostFloat: Makes a 3D object drift smoothly in all axes with subtle rotation.
/// Attach to any GameObject. Tune parameters in the Inspector.
/// </summary>
public class GhostFloat : MonoBehaviour
{
    [Header("Vertical Float")]
    [Tooltip("How high/low the object bobs up and down")]
    public float verticalAmplitude = 0.4f;

    [Tooltip("Speed of the vertical bob")]
    public float verticalFrequency = 0.8f;

    [Header("Horizontal Drift")]
    [Tooltip("Max horizontal wander distance")]
    public float horizontalAmplitude = 0.2f;

    [Tooltip("Speed of the horizontal drift (uses Perlin noise)")]
    public float horizontalFrequency = 0.3f;

    [Header("Rotation Sway")]
    [Tooltip("Max tilt in degrees on each axis")]
    public float rotationAmplitude = 4f;

    [Tooltip("Speed of the rotation sway")]
    public float rotationFrequency = 0.5f;

    [Header("Noise Seeds (auto-set on Awake)")]
    [Tooltip("Unique offsets so multiple ghosts don't move in sync")]
    public float seedX = 0f;
    public float seedY = 100f;
    public float seedZ = 200f;

    // The object's resting position, captured on start
    private Vector3 _originPosition;
    private Quaternion _originRotation;

    private void Awake()
    {
        // Randomise seeds so multiple instances are out of phase
        seedX = Random.Range(0f, 999f);
        seedY = Random.Range(0f, 999f);
        seedZ = Random.Range(0f, 999f);
    }

    private void Start()
    {
        _originPosition = transform.localPosition;
        _originRotation = transform.localRotation;
    }

    private void Update()
    {
        float t = Time.time;

        // --- Position ---

        // Vertical: clean sine wave for a predictable, gentle bob
        float vertical = Mathf.Sin(t * verticalFrequency * Mathf.PI * 2f)
                         * verticalAmplitude;

        // Horizontal: Perlin noise mapped from [0,1] to [-1,1] for smooth wander
        float driftX = (Mathf.PerlinNoise(seedX, t * horizontalFrequency) * 2f - 1f)
                       * horizontalAmplitude;
        float driftZ = (Mathf.PerlinNoise(seedZ, t * horizontalFrequency) * 2f - 1f)
                       * horizontalAmplitude;

        transform.localPosition = _originPosition + new Vector3(driftX, vertical, driftZ);

        // --- Rotation ---

        // Tilt gently using independent Perlin channels per axis
        float tiltX = (Mathf.PerlinNoise(seedX + 50f, t * rotationFrequency) * 2f - 1f)
                      * rotationAmplitude;
        float tiltY = (Mathf.PerlinNoise(seedY + 50f, t * rotationFrequency) * 2f - 1f)
                      * rotationAmplitude;
        float tiltZ = (Mathf.PerlinNoise(seedZ + 50f, t * rotationFrequency) * 2f - 1f)
                      * rotationAmplitude;

        transform.localRotation = _originRotation
                                  * Quaternion.Euler(tiltX, tiltY, tiltZ);
    }
}