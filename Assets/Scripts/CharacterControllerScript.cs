using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControllerScript : MonoBehaviour {

    public float speed = 5.0f;
    Animator anim;
    public float moveSpeed = 5.0f;
    public float jumpHeight = 5.0f;
    
    // Use this for initialization
    float distToGround;
    bool isGrounded()
    {
        return (GetComponent<Rigidbody2D>().velocity.y >= -.01f && GetComponent<Rigidbody2D>().velocity.y <= .01f);
    }
    bool jumped = false;
    bool isDead;
    bool controllingBlocks = true;
    public Text loseText;
    public Text winsText;
    int wins;
    void Start ()
    {

        GameObject dontdestroyobject = GameObject.Find("BGMSource");
        dontDestroyOnLoad dontdestroyscript = dontdestroyobject.GetComponent<dontDestroyOnLoad>();
        wins = dontdestroyscript.wins;
        loseText.text = "";
        winsText.text = "Wins in a row: " + wins.ToString();
        anim = GetComponent<Animator>();
        isDead = false;
    }
	void Update ()
    {
        GameObject dontdestroyobject = GameObject.Find("BGMSource");
        dontDestroyOnLoad dontdestroyscript = dontdestroyobject.GetComponent<dontDestroyOnLoad>();


        if (!isDead && !controllingBlocks)
        {
            anim.SetFloat("Velocity", moveSpeed);
            if (Input.GetKeyDown(KeyCode.C))
                controllingBlocks = true;
            if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) && isGrounded())
            {
                jumped = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
                GetComponent<AudioSource>().Play();
            }
            if (isGrounded())
                jumped = false;
            if (jumped && !isGrounded())
            {
                anim.CrossFade("idlejump", .01f);
            }
            if (Input.GetKeyDown(KeyCode.C))
                controllingBlocks = true;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
                if (!jumped)
                    anim.CrossFade("walkleft", .01f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
                if (!jumped)
                    anim.CrossFade("walkright", .01f);
            }
            else if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("main");
                dontdestroyscript.wins = 0;
            }
            else if (!jumped)
                anim.CrossFade("idle", .01f);
        }
        else if(controllingBlocks)
        {
            isDead = false;
            if (Input.GetKeyDown(KeyCode.C))
                controllingBlocks = false;
        }
        else if (isDead)
        {

            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("main");
            }
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject dontdestroyobject = GameObject.Find("BGMSource");
        dontDestroyOnLoad dontdestroyscript = dontdestroyobject.GetComponent<dontDestroyOnLoad>();
        if (other.tag == "deadly" && !isDead)
        {
            
            loseText.text = "You died.\n Press r to restart.";
            anim.CrossFade("Death", .01f);
            isDead = true;
            other.GetComponent<AudioSource>().Play();
            dontdestroyscript.wins = 0;
        }
        if (other.tag == "door")
        {

            
            loseText.text = "You win! \n Press r to play again!";
            isDead = true;
            dontdestroyscript.wins++;
            
        }
    }
}
