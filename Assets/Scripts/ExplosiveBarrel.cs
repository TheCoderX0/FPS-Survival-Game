using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public GameObject barrel;
    public GameObject explosion;

    public float explosionRange = 3f;

    private AudioSource audioSource;

    private void Awake()
    {
        barrel.SetActive(true);
        explosion.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    public void Explode()
    {

        barrel.SetActive(false);
        explosion.SetActive(true);

        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        //audioSource.Play();

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange);

        foreach(Collider enemy in enemies)
        {
            if (enemy.GetComponent<EnemyFollow>() != null)
            {
                enemy.GetComponent<EnemyFollow>().KillEnemy();
            }
        }

        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

}
