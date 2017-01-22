using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour
{

    private Vector3 target;
    Rigidbody rb;
    Vector3 direction;
    // Use this for initialization
    void Start()
    {
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        print(target);
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.Normalize(target - transform.position) * 50, ForceMode.Force);
        rb.velocity = Vector3.Normalize(target - transform.position) * 30;
        
    }

    void OnCollisionEnter(Collision col)
    {
        var playerController = col.gameObject.GetComponent<GGJ_PlayerController>();
        if (playerController != null)
        {
            playerController.Stats.TakeDamage(gameObject, 5);
        }

        Destroy(gameObject);

    }

    // Update is called once per frame
}
