using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelBuilder : MonoBehaviour
{
    
    public GameObject startRoom;
    public GameObject endRoom;
    public GameObject[] rooms;
    public int levelLength;
  



    // Start is called before the first frame update
    void Start()
    {
        setupLevel();
    }

    
    public void setupLevel()
    {
        // create a number of cells based on the levelLength variable
        for (int x = 0; x <= levelLength; x++)
        {
            // create starting cell
            if ( x == 0) 
            {
                GameObject instance = Instantiate(startRoom, new Vector3(0 + (x * 20), 0f, 0f), Quaternion.identity) as GameObject;
            }
            // create the last cell
            else if (x == levelLength)
            {
                GameObject instance = Instantiate(endRoom, new Vector3(0 + (x * 20), 0f, 0f), Quaternion.identity) as GameObject;
            }
            // put down a random cell if there shouldnt be anythign specific 
            else
            {
                GameObject instance = Instantiate(rooms[Random.Range(0,rooms.Length)], new Vector3(0 + (x * 20), 0f, 0f), Quaternion.identity) as GameObject;
            }

        }
    }

}
