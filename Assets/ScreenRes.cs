using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes : MonoBehaviour
{
    private void Awake()
    {
        //Set screen size for Standalone
#if UNITY_STANDALONE
        Screen.SetResolution(729, 1080, true);
        Screen.fullScreen = false;
#endif
        DontDestroyOnLoad(this.gameObject);
    }
}
