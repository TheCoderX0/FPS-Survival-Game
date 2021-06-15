using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 40f;
    public float speed = 10f;
    public float lifeDuration = 2f;

    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //Make bullet move.
        transform.position += transform.forward * speed * Time.deltaTime;

        //Check if the bullet should be destroyed.
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
    /*
    public void OnTriggerEnter(Collider other)
    {
        if (Collision.gameObject.CompareTag("Enemy"))
        {

        }
    }
    */

}
