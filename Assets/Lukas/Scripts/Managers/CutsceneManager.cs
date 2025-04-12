using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slides;
    [SerializeField] private int[] slideDurations;
    private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();
    private int currentSlideIndex = 0;
    private float timer;
    void Start()
    {
        timer = slideDurations[currentSlideIndex];

        for (int i = 0; i < slides.Length; i++)
        {
            CanvasGroup canvasGroup = slides[i].GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroups.Add(canvasGroup);
            }
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return) || timer < 0)
        {
            showNextSlide();
        }
    }

    void showNextSlide()
    {
        int nextSlideIndex = currentSlideIndex + 1;

        if (nextSlideIndex == slides.Length)
        {
            SceneManager.LoadScene("Main menu");
        }
        else if (currentSlideIndex < slides.Length)
        {
            hideSlide(currentSlideIndex);
        }

        if (nextSlideIndex < slides.Length)
        {
            showSlide(nextSlideIndex);
            timer = slideDurations[nextSlideIndex];
        }

        currentSlideIndex++;
    }

    void showSlide(int index)
    {
        CanvasGroup canvasGroup = canvasGroups[index];
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }

    void hideSlide(int index)
    {
        CanvasGroup canvasGroup = canvasGroups[index];
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }
}
