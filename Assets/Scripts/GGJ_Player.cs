using UnityEngine;
using System.Collections;

public class GGJ_Player : MonoBehaviour
{
    private int _hitPoints;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void HurtPlayer(int damage)
    {
        // TODO: Play audio

        // TODO: Shake camera

        // Decrement health
        _hitPoints -= damage;
        if (_hitPoints >= 0)
        {
            // TODO: Kill player
        }
    }
}
