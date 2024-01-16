using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinkerController : MonoBehaviour
{
    public TouchController touches;
    public GeneralController generalController;

    private float targetposition;
    public int index; // 1 or -1

    private int lastgenerated = -1;
    private int generated;

    public List<float> candyPositionsY;

    private void Start()
    {
        targetposition = -240f * index; //new Vector3(transform.localPosition.x, transform.localPosition.y - 120f * index, transform.localPosition.z);
        spawnCandies();

    }

    void Update()
    {
        if (!generalController.paused)
        {
            if (transform.localPosition.y != targetposition)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, targetposition, transform.localPosition.z), generalController.platformsSpeed * generalController.platformsSpeedIndex * Time.deltaTime);
            }
            else
            {
                spawnCandies();
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 360 * index, transform.localPosition.z);

            }

        }

    }

    private void spawnCandies()
    {
        // candies respawn
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                generalController.everyCandyOnScene.Remove(child.gameObject.GetComponent<Candy>());
                Destroy(child.gameObject);
            }
        }

        if(touches.lastparent == this.gameObject)
        {
            touches.lastparent = null;
        }

        foreach (float positionTransform in candyPositionsY)
        {
            //float possibility;
            //  possibility = Random.Range(0, 20);
            // if (possibility > 0) // empty or not
            //{


            List<int> integerList = new List<int>();
            for (int i = 0; i < generalController.candies.candiesToSpawn.Count; i++)
            {
                integerList.Add(i);
            }
            if(lastgenerated != -1)
            {
                integerList.Remove(lastgenerated); // so they don`t repeat
            }

            generated = Random.Range(0, integerList.Count);
            lastgenerated = generated;

            GameObject candyObj;
                candyObj = Instantiate(generalController.candies.candiesToSpawn[generated], transform.position, Quaternion.identity, this.transform);
                candyObj.transform.localPosition = new Vector3(14, positionTransform, 2);
                candyObj.transform.localScale = new Vector3(3f, 1f, 3f);
                candyObj.GetComponent<Candy>().general = generalController;
                generalController.everyCandyOnScene.Add(candyObj.GetComponent<Candy>());
           // }
        }

    }
}
