//the parts of this script pertaining to moving a tetromino were written using a tutorial
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tetroscript : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1;
  

    bool controllingBlocks = true;
    bool placed = false;

    private float verticalSpeed = 0.05f;
    private float horizontalSpeed = 0.1f;
    private float buttonDownWaitMax = 0.2f;

    private float verticalTimer = 0;
    private float horizontalTimer = 0;
    private float buttonDownWaitTimer = 0;

    bool movedImmediateHorizontal;
    bool movedImmediateVertical;
    void Start()
    {
        GameObject dontdestroyobject = GameObject.Find("BGMSource");
        dontDestroyOnLoad dontdestroyscript = dontdestroyobject.GetComponent<dontDestroyOnLoad>();
        fallSpeed *= (dontdestroyscript.wins + 1);
    }
    void Update()
    {
        if (!placed)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (controllingBlocks)
                    controllingBlocks = false;
                else
                    controllingBlocks = true;
            }
            if (controllingBlocks)
            {
                if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    movedImmediateHorizontal = false;
                    movedImmediateVertical = false;
                    horizontalTimer = 0;
                    verticalTimer = 0;
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    if (movedImmediateHorizontal)
                    {
                        if (buttonDownWaitTimer < buttonDownWaitMax) //adds delay to tetromino movement when button is held down
                        {
                            buttonDownWaitTimer += Time.deltaTime;
                            return;
                        }
                        if (horizontalTimer < horizontalSpeed) //makes smooth movement when button is held rather than tapped once
                        {
                            horizontalTimer += Time.deltaTime;
                            return;
                        }
                    }
                        if (!movedImmediateHorizontal) // player is now holding down key rather than pressing it once
                            movedImmediateHorizontal = true;
                        horizontalTimer = 0;
                        transform.position += new Vector3(3, 0, 0);
                        if (CheckIfValidPosition())
                        {
                            FindObjectOfType<GameScript>().UpdateGrid(this);
                        }
                        else
                            transform.position += new Vector3(-3, 0, 0);
                    
                    
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (movedImmediateHorizontal)
                    {
                        if (buttonDownWaitTimer < buttonDownWaitMax) //adds delay to tetromino movement when button is held down
                        {
                            buttonDownWaitTimer += Time.deltaTime;
                            return;
                        }
                        if (horizontalTimer < horizontalSpeed) //makes smooth movement when button is held rather than tapped once
                        {
                            horizontalTimer += Time.deltaTime;
                            return;
                        }
                    }
                        horizontalTimer = 0;
                        if (!movedImmediateHorizontal) // player is now holding down key rather than pressing it once
                            movedImmediateHorizontal = true;
                        transform.position += new Vector3(-3, 0, 0);
                        if (CheckIfValidPosition())
                        {
                            FindObjectOfType<GameScript>().UpdateGrid(this);
                        }
                        else
                            transform.position += new Vector3(3, 0, 0);

                    
                    
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    transform.Rotate(0, 0, 90);
                    if (CheckIfValidPosition())
                        FindObjectOfType<GameScript>().UpdateGrid(this);
                    else
                        transform.Rotate(0, 0, -90);
                }
                if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed) 
                {
                    if (movedImmediateVertical)
                    {
                        if (buttonDownWaitTimer < buttonDownWaitMax) //adds delay to tetromino movement when button is held down
                        {
                            buttonDownWaitTimer += Time.deltaTime;
                            return;
                        }
                        if (verticalTimer < verticalSpeed) //makes smooth movement when button is held rather than tapped once
                        {
                            verticalTimer += Time.deltaTime;
                            return;
                        }
                    }
                    if (!movedImmediateVertical)
                        movedImmediateVertical = true;
                    verticalTimer = 0;
                    transform.position += new Vector3(0, -3, 0);
                    if (CheckIfValidPosition())
                        FindObjectOfType<GameScript>().UpdateGrid(this);
                    else
                    {
                        transform.position += new Vector3(0, 3, 0);
                        controllingBlocks = false;
                        FindObjectOfType<GameScript>().SpawnNextTetromino();
                        placed = true;

                    }
                    fall = Time.time;
                }
                else if (Input.GetKey(KeyCode.R))
                    SceneManager.LoadScene("main");

            }
            //transform.position += new Vector3(0, -3, 0);
        }

        /*void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "floor")
            {
                GameObject nextTetromino = (GameObject)Instantiate(Resources.Load("Tetromino_J", typeof(GameObject)), new Vector2(5.0f, 20f), Quaternion.identity);
            }
        }*/
    }
    bool CheckIfValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<GameScript>().Round(mino.position);
            if (FindObjectOfType<GameScript>().isInGrid(pos) == false)
            {
                return false;
            }
            if (FindObjectOfType<GameScript>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<GameScript>().GetTransformAtGridPosition(pos).parent != transform)
             {
                return false;
             }
        }
        return true;
    }
}
