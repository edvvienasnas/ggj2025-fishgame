using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private GameObject projectile;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float dropSpeed;

    private void Update()
    {
        Vector3 vertical = new Vector3();
        Vector3 horizontal = new Vector3();

        // Move player
        if (Input.GetAxisRaw("Horizontal") != 0 ) 
        {
            horizontal = transform.position + new Vector3(
                0,
                0,
                Input.GetAxisRaw("Horizontal") * moveSpeed
                );
        }
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            vertical = transform.position + new Vector3(
                0,
                Input.GetAxisRaw("Vertical") * moveSpeed,
                0
                );
        }

        if (horizontal.z <= 15 && horizontal.z >= -10) 
        {
            transform.Translate(
                new Vector3(
                    0,
                    0,
                    Input.GetAxisRaw("Horizontal") * moveSpeed
                    )
                );

            // Move camera
            camera.transform.Translate(
                new Vector3(
                    Input.GetAxisRaw("Horizontal") * moveSpeed,
                    0,
                    0
                    )
                );
        }

        if (vertical.y <= 12 && vertical.y >= 0)
        {
            transform.Translate(
                new Vector3(
                    0,
                    Input.GetAxisRaw("Vertical") * moveSpeed,
                    0
                    )
                );

            // Move camera
            camera.transform.Translate(
                new Vector3(
                    0,
                    Input.GetAxisRaw("Vertical") * moveSpeed,
                    0
                    )
                );
        }

        // Shoot projectile
        if (Input.GetButtonDown("Fire1")) 
        {
            Instantiate(projectile, shootingPoint.transform.position, Quaternion.identity);
        }
    }

    private void DropToBottom() 
    {
        transform.Translate(0, -1 * dropSpeed, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Boss Projectile") 
        {
            Debug.Log("player hit");
        }
    }
}
