using UnityEngine;
using System.Collections;

public class GGJ_CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform CameraTransform { get; set; }

    // How long the object should shake for.
    [HideInInspector]
	public float ShakeTime;

    // Amplitude of the shake. A larger value shakes the camera harder.
	public float ShakeIntensity;
	public float DecreaseFactor;

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