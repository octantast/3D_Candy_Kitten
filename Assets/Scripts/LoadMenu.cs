using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public Image loading;
    public float timerforloading = 7.5f;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (timerforloading > 0)
        {
            float timeElapsed = Time.time - startTime;
            loading.fillAmount = timeElapsed / timerforloading;
            timerforloading -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}