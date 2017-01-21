using System;
using UnityEngine;

public class GGJ_DeathFadeTimer : MonoBehaviour
{
    private const float MAX_DEATH_TIMER = 4.0f;
    private const float MAX_DEATH_TIMER_FADE = 2.5f;

    private float _timer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Create the fadeout object
        var fadeObjectInOut = gameObject.AddComponent<GGJ_FadeObjectInOut>();
        fadeObjectInOut.fadeDelay = MAX_DEATH_TIMER_FADE;
        fadeObjectInOut.fadeInOnStart = false;
        fadeObjectInOut.fadeOutOnStart = true;
        fadeObjectInOut.fadeTime = MAX_DEATH_TIMER - MAX_DEATH_TIMER_FADE;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > MAX_DEATH_TIMER)
        {
            Destroy(gameObject);
        }
    }
}
