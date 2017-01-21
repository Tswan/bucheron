using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeTime;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeIntensity;
    public float decreaseFactor;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            //Defulat to self 
            camTransform = transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeIntensity;
            shakeTime -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeTime = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}