﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class szeneSwitch : MonoBehaviour
{
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
            //SceneManager.LoadScene (sceneName:"SphereView");
            SceneManager.LoadScene (sceneName:scene);

    }
}
