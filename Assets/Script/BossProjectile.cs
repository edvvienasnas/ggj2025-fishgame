using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float timeToDeath = 5f;

    private Player player;

    void Start() 
    {
        player = FindFirstObjectByType<Player>();

        transform.LookAt(player.transform);
    }

    private void Update()
    {
        timeToDeath -= Time.deltaTime;
        if (timeToDeath <= 0)
        {
            Destroy(this.gameObject);
        }


        transform.Translate(Vector3.forward * baseSpeed);
    }

    public void Shoot(float speed) 
    {
        baseSpeed = speed;
    }
}
