using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour {

    // Use this for initialization
    bool stopped;
    void Start () {
        stopped = false;
	}

    // Update is called once per frame
 
    void Update () {
        if ((Input.GetKeyDown(KeyCode.M)) && stopped == false)
        {
            GetComponent<AudioSource>().Pause();
            stopped = true;
        }
        else if ((Input.GetKeyDown(KeyCode.M)) && stopped == true)
        {
            GetComponent<AudioSource>().Play();
            stopped = false;
        }

    }
}
