using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public Image loading;
    public float timerforloading = 7.5f;

    private void Update()
    {
        loading.fillAmount = Mathf.Lerp(loading.fillAmount, 1, Time.deltaTime * 2);
        if (timerforloading > 0)
        {
            timerforloading -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
