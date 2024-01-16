using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    private AsyncOperation asyncOperation;

    public GeneralController general;

    private float volume;
    public List<AudioSource> sounds;

    private bool reloadThis;
    private bool reload;

    public GameObject volumeOn;
    public GameObject volumeOff;
    public GameObject loadingScreen;
    public Image loading;
    public GameObject settingScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    private float mode; // unique level
    public int howManyLevelsDone; // real number of last level
    private int levelMax; // how many levels total
    public float chosenLevel; // real number of level

    public int levelGemBonus;
    public TMP_Text levelGems;
    public TMP_Text levelCounter;
    public int gems;
    public int price1;
    public int price2;
    public TMP_Text price1text;
    public TMP_Text price2text;
    public List<TMP_Text> gemsText;

    // skills
    public float a1timer;
    public float a1timerMax;
    public Image a1activeskale;
    public bool a2active;

    // tips
    public Animator tipAnimator;

    public int tutorial1;
    public int tutorial2;
    public int tutorial3;

    public GameObject light;
    private bool lighton;
    public void Start()
    {
        Time.timeScale = 1;
        asyncOperation = SceneManager.LoadSceneAsync("Preloader");
        asyncOperation.allowSceneActivation = false;

        gems = PlayerPrefs.GetInt("gems");
        mode = PlayerPrefs.GetFloat("mode");
        levelMax = PlayerPrefs.GetInt("levelMax");
        volume = PlayerPrefs.GetFloat("volume");
        chosenLevel = PlayerPrefs.GetFloat("chosenLevel");
        howManyLevelsDone = PlayerPrefs.GetInt("howManyLevelsDone");

        tutorial1 = PlayerPrefs.GetInt("tutorial1");
        tutorial2 = PlayerPrefs.GetInt("tutorial2");
        tutorial3 = PlayerPrefs.GetInt("tutorial3");

        sounds[0].Play();
        if (volume == 1)
        {
            Sound(true);
        }
        else
        {
            Sound(false);
        }

        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        settingScreen.SetActive(false);
        loadingScreen.SetActive(false);

        tipAnimator.enabled = false;
        price1text.text = price1.ToString("0");
        price2text.text = price2.ToString("0");

        // levels
        if (mode != 0)
        {
            if (mode == 1)
            {
                general.norare = true;
                general.howManyKittensNeeded = 3;
                levelGemBonus = 100;
            }
            else if (mode == 2)
            {
                general.howManyKittensNeeded = 4;
                levelGemBonus = 100;
            }
            else if (mode == 3 || mode == 4 || mode == 5)
            {
                general.normalspeed = 1.25f;
                general.howManyKittensNeeded = 5;
                levelGemBonus = 150;
            }
            else if (mode == 6)
            {
                general.normalspeed = 1.25f;
                general.onlyrare = true;
                general.howManyKittensNeeded = 6;
                levelGemBonus = 200;
            }
            else if (mode == 7)
            {
                general.normalspeed = 1.25f;
                general.onlyrare = true;
                general.howManyKittensNeeded = 7;
                levelGemBonus = 200;
            }
            else if (mode == 8)
            {
                general.normalspeed = 1.5f;
                general.onlyrare = true;
                general.howManyKittensNeeded = 8;
                levelGemBonus = 200;
            }
            else if (mode == 9)
            {
                general.normalspeed = 1.5f;
                general.onlyrare = true;
                general.howManyKittensNeeded = 9;
                levelGemBonus = 200;
            }
            else if (mode == 10)
            {
                general.normalspeed = 1.5f;
                general.howManyKittensNeeded = 10;
                levelGemBonus = 250;
            }

        }

        levelGems.text = "+" + levelGemBonus.ToString("0");

        if (tutorial1 == 0)
        {
            //tutorial1 = 1;
            PlayerPrefs.SetInt("tutorial1", 1);
            PlayerPrefs.Save();
            tipAnimator.enabled = false;
            tipAnimator.Play("Basic");
            tipAnimator.enabled = true;
        }

        general.spawnkittens();
    }

    public void Update()
    {
        if (!lighton)
        {
            lighton = true;
            GameObject lightObj = Instantiate(light, transform.position, Quaternion.Euler(60, -30, 0));
            lightObj.transform.position = Vector3.zero;
        }

        if (!general.paused)
        {
            foreach (TMP_Text text in gemsText)
            {
                text.text = gems.ToString("0");
            }

            levelCounter.text = "COUNT: " + general.howManyKittens.ToString("0") + "/" + general.howManyKittensNeeded.ToString("0");

            // a1
            if (a1timer > 0)
            {
                a1timer -= Time.deltaTime;
                general.platformsSpeedIndex = 0.2f;
                a1activeskale.fillAmount = 1 - a1timer / a1timerMax;
            }
            else if (general.platformsSpeedIndex != general.normalspeed)
            {
                general.platformsSpeedIndex = general.normalspeed;
                a1activeskale.fillAmount = 1;
                general.iceParticles.gameObject.SetActive(false);
            }

           
        }
        if (loadingScreen.activeSelf == true)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = 0;
            }

            if (loading.fillAmount < 0.9f)
            {
                loading.fillAmount = Mathf.Lerp(loading.fillAmount, 1, Time.deltaTime * 2);
            }
            else
            {
                if (!reload)
                {
                    reload = true;
                    if (reloadThis)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                    else
                    {
                        asyncOperation.allowSceneActivation = true;
                    }
                }
            }
        }
        if (!loadingScreen.activeSelf)
        {
            foreach (AudioSource audio in sounds)
            {
                audio.volume = volume;
            }
        }
    }

    public void ExitMenu()
    {
        sounds[1].Play();

        general.paused = false;
        asyncOperation.allowSceneActivation = true;
        loading.fillAmount = 0;
        loadingScreen.SetActive(true);
        loading.enabled = false;
    }
    public void reloadScene()
    {
        sounds[1].Play();
        general.paused = false;
        loading.fillAmount = 0;
        reloadThis = true;
        loadingScreen.SetActive(true);
    }
    public void Sound(bool volumeBool)
    {
        if (volumeBool)
        {
            volumeOn.SetActive(true);
            volumeOff.SetActive(false);
            volume = 1;
        }
        else
        {
            volume = 0;
            volumeOn.SetActive(false);
            volumeOff.SetActive(true);
        }

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void closeIt()
    {
        sounds[1].Play();
        general.paused = false;
        settingScreen.SetActive(false);
    }

    public void Settings()
    {
        sounds[1].Play();
        general.paused = true;
        settingScreen.SetActive(true);
    }

    public void a1()
    {
        sounds[1].Play();
        
        if (gems >= price1)
        {
            if (a1timer <= 0)
            {
                gems -= price1;
                PlayerPrefs.SetInt("gems", gems);
                PlayerPrefs.Save();
                general.iceParticles.gameObject.SetActive(true);
                general.iceParticles.Play();
                a1timer = a1timerMax;
            }

        }
        else
        {
            tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }

    public void a2()
    {
        sounds[1].Play();

        if (gems >= price2)
        {
            Debug.Log("a2");
            gems -= price2;
            PlayerPrefs.SetInt("gems", gems);
            PlayerPrefs.Save();
            a2active = true;
            foreach (Candy script in general.everyCandyOnScene)
            {
                script.transform.localScale = new Vector3(script.transform.localScale.z*1.5f, script.transform.localScale.y, script.transform.localScale.z);
                script.mesh.mesh = general.specialCandy;
            }
            if (general.sweetinBowl != null)
            {
                general.sweetinBowl.transform.localScale = new Vector3(general.sweetinBowl.transform.localScale.z, general.sweetinBowl.transform.localScale.y, general.sweetinBowl.transform.localScale.z);
            }
            foreach (KittenController script in general.kittieScripts)
            {
                script.sprites[2].gameObject.SetActive(true);
                script.sprites[1].gameObject.SetActive(false);
                script.sprites[0].gameObject.SetActive(false);
            }
        }
        else
        {
            tipAnimator.enabled = false;
            tipAnimator.Play("Warning");
            tipAnimator.enabled = true;

        }
    }
    public void NextLevel()
    {
        sounds[1].Play();
        if (chosenLevel <= howManyLevelsDone + 1 && chosenLevel != levelMax)
        {
            chosenLevel += 1;
            mode += 1;
            if (mode > 10)
            {
                mode = 1;
            }


            PlayerPrefs.SetFloat("chosenLevel", chosenLevel);
            PlayerPrefs.SetFloat("mode", mode);
            PlayerPrefs.Save();
            reloadScene();
        }
    }
    public void a2off()
    {
        foreach (Candy script in general.everyCandyOnScene)
        {
            script.transform.localScale = new Vector3(script.transform.localScale.z, script.transform.localScale.y, script.transform.localScale.z);
            script.mesh.mesh = script.thismesh;
            general.cleara2();
        }
    }
}
