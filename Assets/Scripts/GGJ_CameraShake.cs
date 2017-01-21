using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform CameraTransform { get; set; }

    // How long the object should shake for.
    public float ShakeTime { get; set; }

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float ShakeIntensity { get; set; }
    public float DecreaseFactor { get; set; }

    private Vector3 OriginalPosition;

    void Awake()
    {
        if (CameraTransform == null)
        {
            //Defulat to self 
            CameraTransform = transform;
        }
    }

    void OnEnable()
    {
        OriginalPosition = CameraTransform.localPosition;
    }

    void Update()
    {
        if (ShakeTime > 0)
        {
            CameraTransform.localPosition = OriginalPosition + Random.insideUnitSphere * ShakeIntensity;
            ShakeTime -= Time.deltaTime * DecreaseFactor;
        }
        else
        {
            ShakeTime = 0f;
            CameraTransform.localPosition = OriginalPosition;
        }
    }
}