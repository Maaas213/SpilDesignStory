using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public GameObject FireBall;
    public GameObject BackFireBall;
    public GameObject Player;

    public float FireBallSpeed;
    private float NextFireBall;
    public float MP;

    private SpriteRenderer SR;
    private SpriteRenderer BSR;
    private CircleCollider2D CC;
    private CircleCollider2D BCC;

    public bool Fireing;
    public bool BackFireing;
    private bool TurnRight;
    private bool TurnLeft;


    private Vector3 PlayerPos;
    private Vector3 BackPlayerPos;


    // Start is called before the first frame update
    void Start()
    {
        SR = FireBall.GetComponent<SpriteRenderer>();
        CC = FireBall.GetComponent<CircleCollider2D>();
        BSR = BackFireBall.GetComponent<SpriteRenderer>();
        BCC = BackFireBall.GetComponent<CircleCollider2D>();

        SR.enabled = false;
        CC.enabled = false;
        BSR.enabled = false;
        BCC.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            TurnRight = true;
            TurnLeft = false;
        }

        if (Input.GetKeyDown("a"))
        {
            TurnLeft = true;
            TurnRight = false;
        }

        PlayerPos = new Vector3(Player.transform.position.x + 1, Player.transform.position.y, Player.transform.position.z);
        BackPlayerPos = new Vector3(Player.transform.position.x - 1, Player.transform.position.y, Player.transform.position.z);

        if(MP >= 3)
        {
            if (Time.time > NextFireBall)
            {
                if (TurnRight == true)
                {
                    if (Input.GetKeyDown("f"))
                    {
                        FireBall.transform.position = PlayerPos;
                        Fireing = true;
                        BackFireing = false;
                        NextFireBall = Time.time + 2;
                        StartCoroutine(FireingCo());
                        MP = MP - 3;
                        FindObjectOfType<AudioManager>().Play("Fireball");
                    }
                }

                if (TurnLeft == true)
                {
                    if (Input.GetKeyDown("f"))
                    {
                        BackFireBall.transform.position = BackPlayerPos;
                        BackFireing = true;
                        Fireing = false;
                        NextFireBall = Time.time + 2;
                        StartCoroutine(BackFireingCo());
                        MP = MP - 3;
                        FindObjectOfType<AudioManager>().Play("Fireball");
                    }

                }

            }

        }

        Vector3 Fire = new Vector3(0.1f, 0.0f, 0.0f);
        Vector3 BackFire = new Vector3(-0.1f, 0.0f, 0.0f);

        if (Fireing == true)
        {
            FireBall.transform.Translate(Fire * FireBallSpeed);
            SR.enabled = true;
            CC.enabled = true;
        }

        if(BackFireing == true)
        {
            BackFireBall.transform.Translate(BackFire * FireBallSpeed);
            BSR.enabled = true;
            BCC.enabled = true;
        }

    }

    IEnumerator FireingCo()
    {
        yield return new WaitForSeconds(2);
        Fireing = false;
        SR.enabled = false;
        CC.enabled = false;
    }

    IEnumerator BackFireingCo()
    {
        yield return new WaitForSeconds(2);
        BackFireing = false;
        BSR.enabled = false;
        BCC.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (MP < 9)
        {
            if (other.gameObject.CompareTag("Mana"))
            {
                MP = MP + 1;
            }
        }
    }
}
