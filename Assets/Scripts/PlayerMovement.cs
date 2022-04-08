using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;


public class PlayerMovement : MonoBehaviour
{
    public float Speed = 2;
    public float DashForce;
    public float JumpForce;
    public float NextDash;

    private Rigidbody2D RB;

    public bool OnGround;
    public bool DoubleJump;

    public KeyCode[] interactionskeys;
    public Flowchart flowchart;
    public bool inDialog;
    public Interactive currentInteractive;



    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        
        if (!inDialog)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            //RB.AddForce(movement * Speed, ForceMode2D.Impulse);
            transform.Translate(movement * Speed);

            Vector3 Jump = new Vector3(0.0f, 1, 0.0f);
            if (OnGround == true)
            {
                if (Input.GetKeyDown("w"))
                {

                    //transform.Translate(Jump * JumpForce);
                    RB.AddForce(Jump * JumpForce);
                    OnGround = false;

                    StartCoroutine(DoubleJumpCoRoutine());

                }
            }

            if (DoubleJump == true)
            {
                if (Input.GetKeyDown("w"))
                {
                    RB.AddForce(Jump * JumpForce);
                    DoubleJump = false;
                }
            }

            if (Time.time > NextDash)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RB.AddForce(movement * DashForce, ForceMode2D.Impulse);
                    NextDash = Time.time + 5;
                }
            }
        }
       
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        OnGround = true;
        DoubleJump = false;
        if (other.gameObject.CompareTag("Enemy1"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("BattleScene");
        }
    }

    IEnumerator DoubleJumpCoRoutine()
    {  
        yield return new WaitForSeconds(0.1f);
        DoubleJump = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactive interactive = collision.GetComponent<Interactive>();
        if (interactive)
        {
            currentInteractive = interactive;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentInteractive = null;
    }

    private void Interact()
    {
        if (!currentInteractive)
        {
            return;
        }

        if (currentInteractive.autoInteractive == true)
        {
            ExecuteBlock();
        }

        foreach (KeyCode keycode in interactionskeys)
        {
            if (Input.GetKeyDown(keycode))
            {
                ExecuteBlock();
            }
        }
    }

    private void ExecuteBlock()
    {
        flowchart.ExecuteBlock(currentInteractive.blockName);
        inDialog = true;
    }

    public void ExitBlock()
    {
        inDialog = false;
    }

}
