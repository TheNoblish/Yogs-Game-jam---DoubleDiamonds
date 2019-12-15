using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour
{
    public GameObject menu;
    bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!isOpen)
            {
                OpenMenu();
                isOpen = true;
            } else
            {
                CloseMenu();
                isOpen = false;
            }
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }
}
