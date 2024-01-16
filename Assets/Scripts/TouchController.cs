using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public GeneralController general;

    public GameObject holdedCandy;
    private Candy holdedCandyScript;
    private Rigidbody holdedCandyrb;
    public GameObject lastparent;

    public float candyspeed;

    public bool isDragging;

    public void Update()
    {
        if (!general.paused)
        {
            touchInput();
        }
    }

   public void touchInput()
    {
        if (Application.isEditor)
        {
            Vector3 touchPos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(touchPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Sweet"))
                    {
                        Debug.Log("i touched collider");
                        holdedCandy = hit.collider.gameObject;
                        if (holdedCandy.transform.parent != null)
                        {
                            lastparent = holdedCandy.transform.parent.gameObject;
                        }
                        holdedCandyrb = holdedCandy.GetComponent<Rigidbody>();
                        holdedCandyrb.isKinematic = true;
                        holdedCandyScript = holdedCandy.GetComponent<Candy>();
                        holdedCandyScript.enabled = true;
                        holdedCandyScript.thisPosition = holdedCandy.transform.localPosition;
                        holdedCandy.transform.SetParent(null);
                        isDragging = true;
                    }
                }

            }
            if (Input.GetMouseButton(0))
            {
                if (isDragging)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touchPos);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        Vector3 newPosition = new Vector3(hit.point.x, 0.6f, hit.point.z);                       
                        holdedCandy.transform.position = Vector3.MoveTowards(holdedCandy.transform.position, newPosition, candyspeed * Time.deltaTime);                       
                    }

                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                endTouch();
            }

        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(touchPos);
            RaycastHit hit;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                   // Ray ray = Camera.main.ScreenPointToRay(touchPos);
                   // RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.CompareTag("Sweet"))
                        {
                            Debug.Log("i touched collider");
                            holdedCandy = hit.collider.gameObject;
                            if (holdedCandy.transform.parent != null)
                            {
                                lastparent = holdedCandy.transform.parent.gameObject;
                            }
                            holdedCandyrb = holdedCandy.GetComponent<Rigidbody>();
                            holdedCandyrb.isKinematic = true;
                            holdedCandyScript = holdedCandy.GetComponent<Candy>();
                            holdedCandyScript.enabled = true;
                            holdedCandyScript.thisPosition = holdedCandy.transform.localPosition;
                            holdedCandy.transform.SetParent(null);
                            isDragging = true;
                        }
                    }
                    break;
                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        // Ray ray = Camera.main.ScreenPointToRay(touchPos);
                        // RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Vector3 newPosition = new Vector3(hit.point.x, 0.6f, hit.point.z);
                            holdedCandy.transform.position = Vector3.MoveTowards(holdedCandy.transform.position, newPosition, candyspeed * Time.deltaTime);
                        }

                    }
                    break;
                case TouchPhase.Ended:
                    endTouch();

                    break;

            }
        }
    }

    public void endTouch()
    {
        if (holdedCandy != null)
        {
            if (!holdedCandyScript.kitten && !holdedCandyScript.plate)
            {
                //nope
                if (holdedCandyScript.sweetIndex != general.bowlContent)
                {
                    platereturn();
                }
                else
                {
                    // return in bowl!
                    holdedCandyScript.returntobowl = true;
                    holdedCandyScript.returntoplace = true;
                    holdedCandyScript.kitten = false;
                    holdedCandyScript.plate = false;
                    holdedCandyrb.isKinematic = false;
                    candyClear();
                }
            }
            else if (holdedCandyScript.kitten)
            {
                if (general.ui.a2active)
                {
                    general.kittenDone();
                }
                else
                {
                    if (holdedCandyScript.sweetIndex == general.kittieScripts[general.currentTouchedKitten].sweetIndex)
                    {
                        // good     
                        if (holdedCandyScript.sweetIndex == general.bowlContent)
                        {
                            general.bowlContent = 0;
                        }
                        general.kittenDone();

                    }
                    else
                    {
                        //nope
                        if (holdedCandyScript.sweetIndex != general.bowlContent)
                        {
                            platereturn();
                        }
                        else
                        {
                            // return in bowl!
                            holdedCandyScript.returntobowl = true;
                            holdedCandyScript.returntoplace = true;
                            holdedCandyScript.kitten = false;
                            holdedCandyScript.plate = false;
                            holdedCandyrb.isKinematic = false;
                            candyClear();
                        }
                    }
                }
            }
            else if (holdedCandyScript.plate)
            {
                plateInteraction();

            }
        }
    }

    public void plateInteraction()
    {
        // new position
        if (general.bowlContent == 0)
        {
            general.sweetinBowl = holdedCandy;
            general.bowlContent = holdedCandyScript.sweetIndex;
            holdedCandyScript.kitten = false;
            holdedCandyScript.plate = false;
            holdedCandyrb.isKinematic = false;
            candyClear();
        }
        // mix or not
        else
        {
            // possible combos
            if((general.bowlContent == 4 && holdedCandyScript.sweetIndex == 7) || (general.bowlContent == 7 && holdedCandyScript.sweetIndex == 4))
            {
                general.bowlContent = 2; // blue crafted!
                craft();

            }
            else if ((general.bowlContent == 6 && holdedCandyScript.sweetIndex == 7) || (general.bowlContent == 7 && holdedCandyScript.sweetIndex == 6))
            {
                general.bowlContent = 0; // red crafted!
                craft();


            }
            else if ((general.bowlContent == 5 && holdedCandyScript.sweetIndex == 7) || (general.bowlContent == 7 && holdedCandyScript.sweetIndex == 5))
            {
                general.bowlContent = 1; // green crafted!
                craft();

            }
            else
            {
                general.ui.sounds[2].Play();
                general.craftSparcles.gameObject.SetActive(true);
                general.craftSparcles.Play();
                // cleaning bowl
                general.bowlContent = 0;
                general.everyCandyOnScene.Remove(holdedCandy.gameObject.GetComponent<Candy>());
                general.everyCandyOnScene.Remove(general.sweetinBowl.GetComponent<Candy>());
                Destroy(holdedCandy.gameObject);
                Destroy(general.sweetinBowl);

            }
            candyClear();
        }

    }

    public void platereturn()
    {
        if (lastparent != null)
        {
            holdedCandy.transform.SetParent(lastparent.transform);
        }
        holdedCandyScript.returntoplace = true;
        holdedCandyScript.kitten = false;
        holdedCandyScript.plate = false;
        holdedCandyrb.isKinematic = false;
        candyClear();
    }

    public void candyClear()
    {
        holdedCandyScript = null;
        holdedCandyrb = null;
        holdedCandy = null;
        isDragging = false;
    }

    public void craft()
    {
        general.ui.sounds[2].Play();
        general.craftSparcles.gameObject.SetActive(true);
        general.craftSparcles.Play();

        // effect
        GameObject candyObj;
        candyObj = Instantiate(general.candies.specialcandiesToSpawn[general.bowlContent], general.sweetinBowl.transform.position, Quaternion.identity);
        
        candyObj.transform.localScale = general.sweetinBowl.transform.localScale;

        candyObj.GetComponent<Candy>().general = general;

        // destroy previous
        general.everyCandyOnScene.Remove(holdedCandy.gameObject.GetComponent<Candy>());
        general.everyCandyOnScene.Remove(general.sweetinBowl.GetComponent<Candy>());
        Destroy(holdedCandy.gameObject);
        Destroy(general.sweetinBowl);
        general.sweetinBowl = candyObj;
        general.everyCandyOnScene.Add(candyObj.GetComponent<Candy>());
    }
}
