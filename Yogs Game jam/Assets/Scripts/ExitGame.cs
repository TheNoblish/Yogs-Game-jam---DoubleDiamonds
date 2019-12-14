using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitToDesktop()
    {
        //If we're in the Unity editor, turn off editor play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //Otherwise, quit the application
#else
        Application.Quit();
#endif
    }

}
