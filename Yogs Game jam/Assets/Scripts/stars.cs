using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stars : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, transform.position.y, 0);
        if (transform.position.x <= (player.transform.position.x - 36))
        {
            transform.position = new Vector3(player.transform.position.x + 36, transform.position.y, 0);

        }
        else if (transform.position.x >= (player.transform.position.x + 36))
        {
            transform.position = new Vector3(player.transform.position.x - 36, transform.position.y, 0);

        }
    }
}
