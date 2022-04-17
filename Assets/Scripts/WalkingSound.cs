using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{

    public bool soundPlaying = false;
    public bool OnGround;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var moveLeft = Input.GetKey("a");
        var moveRight = Input.GetKey("d");
        


        if (moveLeft)
        {

            if (OnGround == true && !soundPlaying)
            {
                FindObjectOfType<AudioManager>().Play("Steps2");
            }

        }


        else
        {
            FindObjectOfType<AudioManager>().Stop("Steps2");
        }

        if (moveRight)
        {
            if (OnGround == true && !soundPlaying)
            {
                
                FindObjectOfType<AudioManager>().Play("Steps1");
            }

        }

        else
        {
            FindObjectOfType<AudioManager>().Stop("Steps1");
        }


    }
}
