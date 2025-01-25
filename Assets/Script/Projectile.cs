using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToDeath = 5f;

    private void Update()
    {
        timeToDeath -= Time.deltaTime;
        if (timeToDeath <= 0)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.left * speed);
    }
}
