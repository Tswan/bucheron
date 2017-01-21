using UnityEngine;

public class GGJ_CameraShake : GGJ_Camera
{
    // How long the object should shake for.
    [HideInInspector]
	public float ShakeTime;

    // Amplitude of the shake. A larger value shakes the camera harder.
	public float ShakeIntensity;
	public float DecreaseFactor;

    protected override Vector3 CalculatePosition()
    {
        // Calculate the base position
        var position = base.CalculatePosition();

        // Apply camera shake
        if (ShakeTime > 0.0f)
        {
            position += Random.insideUnitSphere * ShakeIntensity;
            ShakeTime -= Time.deltaTime * DecreaseFactor;
        }
        else
        {
            ShakeTime = 0.0f;
        }

        // Return the position
        return position;
    }
}