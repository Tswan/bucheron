using System;
using UnityEngine;

namespace Assets.Scripts.Cameras
{
    public class GGJ_Parallax : MonoBehaviour
    {
        // Class Variables
        
        public float Speed;
        
        private float _originalZoom;

        // Use this for initialization
        private void Start()
        {
            // Store some defaults
            _originalZoom = Camera.main.orthographicSize;

            Speed = .9f;
            transform.position = new Vector3(0, 0, 10.0f); // behind camera.

            // Forcebly reset the camera before rendering
            ResetScale(_originalZoom);
        }

        // Changed from 'Update' (which was triggered automatically per frame) to 'ResetPosition' (which gets triggered by CsUserInterface).
        // Because if the UI code ran after this code, the star position was always one frame out of date.
        public void ResetPosition()
        {
            transform.position = new Vector3
            (
                Camera.main.transform.position.x * Speed,
                Camera.main.transform.position.y * Speed,
                transform.position.z
            );
        }

        // Called whenever the camera z-axis changes (zoom)
        public void ResetScale(float zoom)
        {
            zoom = zoom / _originalZoom;
            transform.localScale = new Vector3
            (
                zoom * Camera.main.pixelWidth,
                zoom * 0.0f,
                zoom * Camera.main.pixelHeight
            );
        }
    }
}
