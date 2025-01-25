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
        // Move player
        transform.Translate(
            new Vector3(
                0,
                Input.GetAxisRaw("Vertical") * moveSpeed, 
                Input.GetAxisRaw("Horizontal") * moveSpeed
                )
            );

        // Move camera
        camera.transform.Translate(
            new Vector3(
                Input.GetAxisRaw("Horizontal") * moveSpeed,
                Input.GetAxisRaw("Vertical") * moveSpeed,
                0
                )
            );

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
