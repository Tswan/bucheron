using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GGJ_FadeTextInOut : MonoBehaviour
{
    private enum FadeState
    {
        FadingIn,
        Alive,
        FadingOut,
    };

    public float FadeTime;
    public float LifeTime;

    private FadeState _state;
    private float _lifeTimer;
    private Text _text;

    private void Start()
    {
        _state = FadeState.FadingIn;
        _lifeTimer = 0.0f;
        _text = gameObject.GetComponent<Text>();

        //_text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0.0f);
        _text.CrossFadeAlpha(1.0f, FadeTime, false);
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;
        switch (_state)
        {
            case FadeState.FadingIn:
                if (_lifeTimer > FadeTime)
                {
                    _state = FadeState.Alive;
                }
                break;

            case FadeState.Alive:
                if (_lifeTimer > LifeTime)
                {
                    _state = FadeState.FadingOut;
                    _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1.0f);
                    _text.CrossFadeAlpha(0.0f, FadeTime, false);
                }
                break;

            case FadeState.FadingOut:
                if (_lifeTimer > FadeTime)
                {
                    Destroy(this);
                }
                break;
        }
    }
}