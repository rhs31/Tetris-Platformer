using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject dontdestroyobject = GameObject.Find("BGMSource");
        dontDestroyOnLoad dontdestroyscript = dontdestroyobject.GetComponent<dontDestroyOnLoad>();
        Text instruction = GetComponent<Text>();
        instruction.text = "hey";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
