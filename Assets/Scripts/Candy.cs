using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public GeneralController general;

    public Vector3 thisPosition;
    public bool returntoplace;
    public bool returntobowl;
    public int sweetIndex;

    public bool kitten;
    public bool plate;

    public MeshFilter mesh;
    public Mesh thismesh;

   // public Vector3 requiredSize = new Vector3(3, 1, 3);

    void Start()
    {
        if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
        }

        this.enabled = false;
    }

    void Update()
    {
        if (!general.paused)
        {
            if (returntoplace && transform.parent == null && !returntobowl)
            {
                general.everyCandyOnScene.Remove(this);
                Destroy(this.gameObject);
            }
            else if ((returntoplace || returntobowl) && transform.localPosition != thisPosition && thisPosition != null)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, thisPosition, 10); // * Time.deltaTime
            }
            else if (returntoplace)// && transform.localScale.x == requiredSize.x)
            {
                returntoplace = false;
                returntobowl = false;
                this.enabled = false;
            }

            //transform.localScale = Vector3.Lerp(transform.localScale, requiredSize , 10 * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kitten"))
        {
            plate = false;
            kitten = true;
        }
        if (other.gameObject.CompareTag("Plate"))
        {
            plate = true;
            kitten = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Kitten"))
        {
            plate = false;
            kitten = true;
        }
        if (other.gameObject.CompareTag("Plate"))
        {
            plate = true;
            kitten = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Kitten"))
        {
            kitten = false;
        }
        if (other.gameObject.CompareTag("Plate"))
        {
            plate = false;
        }
    }


}
