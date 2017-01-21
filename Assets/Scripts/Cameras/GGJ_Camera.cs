using System;

using UnityEngine;

public class GGJ_Camera : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform CameraTransform { get; set; }

    private Vector3 _originalPosition;

    private void Awake()
    {
        if (CameraTransform == null)
        {
            // Default to self 
            CameraTransform = transform;
        }
    }

    private void OnEnable()
    {
        _originalPosition = CameraTransform.localPosition;
    }

    private void Update()
    {
        CameraTransform.localPosition = CalculatePosition();
    }

    protected virtual Vector3 CalculatePosition()
    {
        // Find average position of players
        // For ease of development we'll do this every update so we don't have to manage re-finding players when they're destroyed and respawned
        var players = GameObject.FindObjectsOfType<GGJ_Player>();
        var averagePosition = Vector3.zero;
        foreach (var player in players)
        {
            averagePosition += player.gameObject.GetComponent<Rigidbody>().position;
        }
        averagePosition /= players.Length;

        // Return the averaged position
        return new Vector3(averagePosition.x, _originalPosition.y, _originalPosition.z);
    }
}
