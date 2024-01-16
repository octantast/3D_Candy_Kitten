using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KittenController : MonoBehaviour
{
    public GeneralController general;

    public float speed;

    public float thistimer;
    public int sweetIndex;
    public int thisNumber;

    public List<SpriteRenderer> sprites;
    public Vector3 targetPosition;

    public TMP_Text timerText;

    public Animator animator;

    public void Update()
    {
        if (!general.paused)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
            if (timerText.gameObject.activeSelf)
            {
                if (thistimer > 0)
                {
                    thistimer -= Time.deltaTime * general.platformsSpeedIndex;
                }
                else
                {
                    general.lose();
                }
            }
            timerText.text = thistimer.ToString("0") + "s";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sweet"))
        {
            general.currentTouchedKitten = thisNumber;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Sweet"))
        {
            general.currentTouchedKitten = thisNumber;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sweet"))
        {
            //general.currentTouchedKitten = 0;
        }
    }
}
