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
            if (!shakeAnim.isPlaying) 
            {
                shakeAnim.Play();
            }

            hp -= 1;
            hpSlider.value = hp;
        }
    }
}
