using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static int globalTileCount;

    public static float farLeft, farRight, bottom, top;

    void Start()
    {
        globalTileCount = 0;
        farLeft = 0;
        farRight = 0;
        bottom = 0;
        top = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("main");
        }
    }
}
