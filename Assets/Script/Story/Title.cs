using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    private float timeToStart = 1.5f;

    bool startIsPressed;

    private void Update()
    {
        if (!startIsPressed)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                startIsPressed = true;

                audio.Play();
            }
        }

        if (startIsPressed) 
        {
            timeToStart -= Time.deltaTime;
        }

        if (timeToStart <= 0) 
        {
            SceneManager.LoadSceneAsync("Intro");
        }
    }
}
