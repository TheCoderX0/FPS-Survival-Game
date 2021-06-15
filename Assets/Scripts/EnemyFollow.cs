using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public int enemyHealth = 100;
    public int damageGiven = 10;
    public int bulletDamage = 40;

    public NavMeshAgent enemy;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);

        if (enemyHealth <= 0)
        {
            ScoreManager.scoreValue += 10;
        }
    }

    public void OnTriggerEnter (Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damageGiven);
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            enemyHealth -= bulletDamage;

            if(enemyHealth <= 0)
            {
                KillEnemy();
            }
        }
    }

    public void KillEnemy()
    {
        ScoreManager.instance.AddScore();
        ScoreManager.scoreValue += 10;

        Destroy(gameObject);
    }

}
