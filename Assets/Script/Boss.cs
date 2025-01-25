using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private GameObject shootingLocation;
    [SerializeField] private float hp = 100;

    [SerializeField] float attackTimer = 3;
    private float timeToAttack;

    private void Start()
    {
        timeToAttack = attackTimer;
    }

    private void Update()
    {
        timeToAttack -= Time.deltaTime;
        if (timeToAttack <= 0) 
        {
            Attack();

            timeToAttack = attackTimer;
        }

        // Die
        if (hp <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    private void Attack() 
    {
        Instantiate(enemyProjectile, shootingLocation.transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player Projectile") 
        {
            hp -= 1;
        }
    }
}
