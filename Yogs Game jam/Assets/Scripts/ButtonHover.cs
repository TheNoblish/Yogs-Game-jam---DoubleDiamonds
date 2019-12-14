using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHover()
    {
        Debug.Log("test");
        text.color = Color.cyan;
    }

    public void onRelease()
    {
        text.color = Color.white;
    }
}
