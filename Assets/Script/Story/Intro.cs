using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private List<GameObject> storyText;
    [SerializeField] private Image fadeText;

    [SerializeField] private Image deadgod;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bubble;

    [SerializeField] private int timeBetweenText;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro() 
    {
        // text 1
        storyText[0].SetActive(true);
        yield return FadeInText();
        yield return new WaitForSeconds(timeBetweenText);
        yield return FadeOutText();
        storyText[0].SetActive(false);

        // text 2
        storyText[1].SetActive(true);
        yield return FadeInText();
        yield return new WaitForSeconds(timeBetweenText);
        yield return FadeOutText();
        storyText[1].SetActive(false);

        // text 2
        deadgod.gameObject.SetActive(true);

        storyText[2].SetActive(true);
        yield return FadeInText();
        yield return new WaitForSeconds(timeBetweenText);
        yield return FadeOutText();
        storyText[2].SetActive(false);

        deadgod.gameObject.SetActive(false);

        // text 2
        player.SetActive(true);

        storyText[3].SetActive(true);
        yield return FadeInText();
        yield return new WaitForSeconds(timeBetweenText);
        yield return FadeOutText();
        storyText[3].SetActive(false);

        bubble.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadSceneAsync("Boss Room");

    }

    private IEnumerator FadeInText() 
    {
        while (fadeText.color.a > 0)
        {
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, fadeText.color.a - 0.5f * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator FadeOutText()
    {
        while (fadeText.color.a < 1)
        {
            fadeText.color = new Color(fadeText.color.r, fadeText.color.g, fadeText.color.b, fadeText.color.a + 0.5f * Time.deltaTime);
            yield return null;
        }
    }
}
