using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralController : MonoBehaviour
{
    public UI ui;
    public CandyController candies;
    public TouchController touches;

    public float platformsSpeed;
    public float platformsSpeedIndex;
    public float normalspeed = 1;

    public int timermax; // for every kitten
    public int howManyKittens;
    public int howManyKittensNeeded;

    public GameObject kittyParent;
    public List<GameObject> kittesToSpawn;
    public List<KittenController> kittieScripts = new List<KittenController>();
    public int currentKittyCandy;
    public int currentTouchedKitten;

    //a2
    public Mesh specialCandy;
    public Material specialCandymaterial;
    public List<Candy> everyCandyOnScene;

    // mix
    public int bowlContent;
    public GameObject sweetinBowl;

    public bool paused;
    public bool onlyrare;
    public bool norare;

    // effects

    public ParticleSystem craftSparcles;
    public ParticleSystem kittenDoneParticles;
    public ParticleSystem iceParticles;

    public void spawnkittens()
    {
        platformsSpeedIndex = normalspeed;
        for (int i = 0; i < howManyKittensNeeded; i++)
        {
            GameObject kitten;
            kitten = Instantiate(kittesToSpawn[Random.Range(0, kittesToSpawn.Count - 1)], transform.position, Quaternion.identity, kittyParent.transform);
            kitten.transform.localPosition = new Vector3(i, 0, 0);
            
            kitten.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            KittenController script;
            script = kitten.GetComponent<KittenController>();
            script.general = this;
            script.targetPosition.x = i;
            script.thisNumber = i;
            kittieScripts.Add(script);

            script.thistimer = Random.Range(10, timermax);

            // which candies
            if (i == 0 && ui.tutorial1 != 0 && ui.tutorial2 == 0)
            {
                PlayerPrefs.SetInt("tutorial2", 1);
                PlayerPrefs.Save();
                ui.tipAnimator.enabled = false;
                ui.tipAnimator.Play("Table");
                ui.tipAnimator.enabled = true;
                script.sweetIndex = 0;
            }
            else
            {
                int rareornot;
                if (!norare)
                {
                    rareornot = Random.Range(0, 5);
                    if (rareornot > 1 && !onlyrare)
                    {
                        script.sweetIndex = Random.Range(3, 7);
                    }
                    else
                    {
                        script.sweetIndex = Random.Range(0, 3);
                    }
                }
                else if (norare) // only simple candies
                {
                    script.sweetIndex = Random.Range(3, 7);
                }
            }

            if (script.sweetIndex < 3)
            {
                script.sprites[1].material.color = candies.combinedCandiesForCloud[script.sweetIndex];
            }
            else
            {
                script.sprites[0].material.color = candies.simpleCandiesForCloud[script.sweetIndex - 3];
            }
        }
        cleara2();
        currentKittyCandy = kittieScripts[0].sweetIndex;
        kittieScripts[0].timerText.gameObject.SetActive(true);

        //kittieScripts[1].thistimer += kittieScripts[0].thistimer;
        kittieScripts[1].timerText.gameObject.SetActive(true);
    }

    public void kittenDone()
    {
        // effect + next kitten or win


        howManyKittens += 1;

        if(howManyKittens >= 1)
        {
            if (ui.tutorial2 != 0 && ui.tutorial3 == 0 && ui.gems > ui.price1)
            {
                ui.tutorial3 = 1;
                PlayerPrefs.SetInt("tutorial3", ui.tutorial3);
                PlayerPrefs.Save();
                ui.tipAnimator.enabled = false;
                ui.tipAnimator.Play("Bonuses");
                ui.tipAnimator.enabled = true;
            }
        }


        // after effects

        ui.sounds[2].Play();
        kittenDoneParticles.transform.position = kittieScripts[currentTouchedKitten].gameObject.transform.position;
        kittenDoneParticles.gameObject.SetActive(true);
       kittenDoneParticles.Play();


        kittieScripts[currentTouchedKitten].gameObject.SetActive(false);

        if (howManyKittens == howManyKittensNeeded)
        {
            ui.levelCounter.text = "COUNT: " + howManyKittens.ToString("0") + "/" + howManyKittensNeeded.ToString("0");
            win();
        }
        else
        {
            // if next
            if (currentTouchedKitten == howManyKittens - 1)
            {
                currentKittyCandy = kittieScripts[howManyKittens].sweetIndex;


                Debug.Log("next");

            }
            else
            {
            }

            foreach (KittenController script in kittieScripts)
            {
                if (script.thisNumber > currentTouchedKitten)
                {
                    script.animator.Play("Walk");
                    script.targetPosition.x -= 1;
                }
            }

            everyCandyOnScene.Remove(touches.holdedCandy.GetComponent<Candy>());
            Destroy(touches.holdedCandy);
            touches.candyClear();
        }



        if (kittieScripts.Count >= howManyKittens + 1 && currentTouchedKitten != kittieScripts.Count - 1)
        {
            //kittieScripts[currentTouchedKitten + 1].thistimer += kittieScripts[currentTouchedKitten].thistimer;
            kittieScripts[currentTouchedKitten + 1].timerText.gameObject.SetActive(true);
        }
        if (kittieScripts.Count >= howManyKittens + 2 && currentTouchedKitten < kittieScripts.Count - 2)
        {
            // kittieScripts[currentTouchedKitten + 2].thistimer += timermax;
            kittieScripts[currentTouchedKitten + 2].timerText.gameObject.SetActive(true);
        }

        if (ui.a2active)
        {
            ui.a2active = false;
            ui.a2off();
        }
    }

    public void win()
    {
        ui.tipAnimator.gameObject.SetActive(false);
        paused = true;
        Debug.Log("win");

        ui.winScreen.SetActive(true);
        if (ui.chosenLevel > ui.howManyLevelsDone)
        {
            PlayerPrefs.SetInt("howManyLevelsDone", (int)ui.chosenLevel);
        }

        ui.gems += ui.levelGemBonus;
        PlayerPrefs.SetInt("gems", ui.gems);
        PlayerPrefs.Save();



    }

    public void lose()
    {
        ui.tipAnimator.gameObject.SetActive(false);
           Debug.Log("lose");
        paused = true;
        ui.loseScreen.SetActive(true);

    }

    public void cleara2()
    {
        foreach(KittenController kitt in kittieScripts)
        {
            kitt.sprites[2].gameObject.SetActive(false);
            if (kitt.sweetIndex < 3)
            {
                kitt.sprites[1].gameObject.SetActive(true);
            }
            else
            {
                kitt.sprites[0].gameObject.SetActive(true);
            }
        }

    }
}
