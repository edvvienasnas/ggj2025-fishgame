using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private GameObject shootingLocation;
    [SerializeField] private Animation shakeAnim;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private float hp = 100;

    [SerializeField] float attackTimer = 3;
    private float timeToAttack;

    private int slowAttacks;
    private int fastAttacks;
    private int manyAttacks;

    private void Start()
    {
        timeToAttack = attackTimer;

        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    private void Update()
    {
        timeToAttack -= Time.deltaTime;
        if (timeToAttack <= 0) 
        {
            if (slowAttacks < 4)
            {
                Attack();
                slowAttacks++;
            }

            else if (slowAttacks >= 4 && fastAttacks <= 4)
            {
                Attack().Shoot(0.5f);
                fastAttacks++;
            }

            else if (fastAttacks >= 4 && manyAttacks <= 15)
            {
                Attack().Shoot(0.1f);
                attackTimer = 0.5f;
                manyAttacks++;
            }

            else if (manyAttacks >= 15 && fastAttacks <= 8)
            {
                Attack().Shoot(0.5f);
                fastAttacks++;
            }

            else if (fastAttacks >= 8) 
            {
                attackTimer = 1f;

                slowAttacks = 0;
                fastAttacks = 0;
                manyAttacks = 0;
            }

            timeToAttack = attackTimer;
        }

        // Die
        if (hp <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    private BossProjectile Attack() 
    {
        return Instantiate(enemyProjectile, shootingLocation.transform.position, Quaternion.identity).GetComponent<BossProjectile>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player Projectile") 
        {
            if (!shakeAnim.isPlaying) 
            {
                shakeAnim.Play();
            }

            hp -= 1;
            hpSlider.value = hp;
        }
    }
}
