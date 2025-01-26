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

    [SerializeField] float attackTimerSlow = 1;
    float attackTimer;
    [SerializeField] float attackTimerFast = 0.5f;
    [SerializeField] float attackFast = 0.1f;
    [SerializeField] float attackSlow = 0.5f;
    private float timeToAttack;

    [SerializeField] private int slowAttackAmount;
    [SerializeField] private int fastAttackAmount;
    [SerializeField] private int manyAttacksAmount;
    [SerializeField] private int fastAttackAmountTwo;

    private int slowAttacks;
    private int fastAttacks;
    private int manyAttacks;

    private void Start()
    {
        timeToAttack = attackTimerSlow;
        attackTimer = timeToAttack;

        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    private void Update()
    {
        timeToAttack -= Time.deltaTime;
        if (timeToAttack <= 0) 
        {
            if (slowAttacks < slowAttackAmount)
            {
                Attack();
                slowAttacks++;
            }

            else if (slowAttacks >= slowAttackAmount && fastAttacks <= fastAttackAmount)
            {
                Attack().Shoot(attackFast);
                fastAttacks++;
            }

            else if (fastAttacks >= fastAttackAmount && manyAttacks <= manyAttacksAmount)
            {
                Attack().Shoot(attackSlow);
                attackTimer = attackTimerFast;
                manyAttacks++;
            }

            else if (manyAttacks >= manyAttacksAmount && fastAttacks <= fastAttackAmountTwo)
            {
                Attack().Shoot(attackFast);
                fastAttacks++;
            }

            else if (fastAttacks >= fastAttackAmountTwo) 
            {
                attackTimer = attackTimerSlow;

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
