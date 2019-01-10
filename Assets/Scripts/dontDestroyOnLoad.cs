using UnityEngine;
using System.Collections;

public class dontDestroyOnLoad : MonoBehaviour
{

    private GameObject[] music;
    public int wins = 0;
    
    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("gameMusic");
        Destroy(music[1]);
    }

    // Update is called once per frame
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    
}