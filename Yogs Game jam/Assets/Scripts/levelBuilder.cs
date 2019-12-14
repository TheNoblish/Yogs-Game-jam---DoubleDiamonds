using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelBuilder : MonoBehaviour
{
    public GameObject[] Tiles;
    public GameObject groundEnemy;
    public Texture2D startRoom;
    public Texture2D endRoom;
    public Texture2D[] rooms;
    public int levelLength;
  



    // Start is called before the first frame update
    void Start()
    {
        setupLevel();
    }

    
    public void setupLevel()
    {
        for (int x = 0; x <= levelLength; x++)
        {
            if ( x == 0)
            {
                placeRoom(startRoom, new Vector3(0 + (x*10), 0f, 0f));
            }
            else if (x == levelLength)
            {
                placeRoom(endRoom, new Vector3(0 + (x * 10), 0f, 0f));
            }
            else
            {
                placeRoom(rooms[Random.Range(0,rooms.Length)], new Vector3(0 + (x * 10), 0f, 0f));
            }

        }
    }

    public void placeRoom(Texture2D room, Vector3 position)
    {
        int height = room.height;
        int width = room.width;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (room.GetPixel(x, y) == Color.black)
                {
                    GameObject instance = Instantiate(Tiles[Random.Range(0, Tiles.Length)], new Vector3(position.x + x, position.y + y, 0f), Quaternion.identity) as GameObject;
                }
                else if (room.GetPixel(x, y) == Color.red)
                {
                    GameObject instance = Instantiate(groundEnemy, new Vector3(position.x + x, position.y + y, 0f), Quaternion.identity) as GameObject;
                }

            }


        }
    }
}
