using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioSource pop;

    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Animator animator;

    [SerializeField] private Image damageIndicator;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    [SerializeField] private float hp = 10;
    [SerializeField] private int mp = 5;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float dropSpeed;

    private bool isFalling;

    private void Start()
    {
        hpSlider.maxValue = hp;
        mpSlider.maxValue = mp;

        hpSlider.value = hp;
        mpSlider.value = mp;
    }

    private void Update()
    {
        Vector3 vertical = new Vector3();
        Vector3 horizontal = new Vector3();

        // Move player
        if (!isFalling) 
        {
            animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));

            if (Input.GetAxisRaw("Horizontal") != 0)
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
                //if (!shoot.isPlaying) 
                //{
                    shoot.Play();
                //}
                Instantiate(projectile, shootingPoint.transform.position, Quaternion.identity);
            }
        }

        // drown
        if (!bubble.activeSelf)
        {
            if (!damageIndicator.gameObject.activeSelf)
            {
                damageIndicator.gameObject.SetActive(true);
            }

            hpSlider.value = hp;
            hp -= 1 * Time.deltaTime;


        }
        else 
        {
            if (damageIndicator.gameObject.activeSelf) 
            {
                damageIndicator.gameObject.SetActive(false);
            }
        }

        if (hp <= 0) 
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    private IEnumerator DropToBottom() 
    {
        isFalling = true;
        animator.SetTrigger("isFalling");

        bubble.SetActive(false);
        pop.Play();

        while (transform.position.y > 0.1f) 
        {
            transform.Translate(0, -1 * dropSpeed, 0);
            camera.transform.Translate(0, -1 * dropSpeed, 0);

            yield return null;
        }

        animator.SetTrigger("hasFell");
    }

    public void HasFinishedFalling() 
    {
        isFalling = false;

        mp--;
        mpSlider.value = mp;

        if (mp > 0) 
        {
            bubble.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Boss Projectile" && !isFalling) 
        {
            StartCoroutine(DropToBottom());
        }
    }
}
