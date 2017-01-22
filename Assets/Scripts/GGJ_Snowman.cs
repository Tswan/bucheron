using UnityEngine;
using System.Collections;

public class GGJ_Snowman : GGJ_EnemyController
{

    public GameObject snowball;
    public GameObject hand;
    private GGJ_PlayerController _playerController;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _playerController = GameObject.FindObjectOfType<GGJ_PlayerController>();
    }

    protected override Vector3 GetMovementDirection()
    {
        return Vector3.zero;
    }

    protected override float GetMovementSpeed()
    {
        return 0.0f;
    }

    private void Update()
    {
        // Check the player state
        if (_playerController.State == GGJ_PlayerController.PlayerState.Dead)
        {
            // Stop the animation
            myAnim.Stop();
        }
        else
        {
            // Rotate the snowman to face the player     
            RigidBody.rotation = Quaternion.LookRotation(
                new Vector3(_playerController.gameObject.transform.position.x, 0.0f, _playerController.gameObject.transform.position.z)
                    - new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z), 
                Vector3.up);
        }
    }

    private void throwSnowball()
    {
        myAnim.Play("throwSnowball");
    }

    private void makeSnowball()
    {
        if (_playerController.State != GGJ_PlayerController.PlayerState.Dead)
        {
            GameObject newSnowball = Instantiate(snowball, hand.transform.position, Quaternion.identity) as GameObject;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            throwSnowball();
        }

    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            myAnim.Stop();
        }
    }
}
