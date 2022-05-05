using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magic : MonoBehaviour
{
    public GameObject FireBall;
    public GameObject BackFireBall;
    public GameObject Player;

    public float FireBallSpeed;
    private float NextFireBall;
    public float MP;
    public int numOfMana;
    public Image[] mana;
    public Sprite fullMana;
    public Sprite emptyMana;
    public ParticleSystem glow;
    public ParticleSystem fireParticleFront;
    public ParticleSystem fireParticleBack;

    private SpriteRenderer SR;
    private SpriteRenderer BSR;
    private CircleCollider2D CC;
    private CircleCollider2D BCC;

    public GameObject ManaPickup;
    private EdgeCollider2D PUC;

    public bool Fireing;
    public bool BackFireing;
    private bool TurnRight;
    private bool TurnLeft;
    private bool NoMana;


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

        MP = 0;

        PUC = ManaPickup.GetComponent<EdgeCollider2D>();
        PUC.enabled = true;

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

        for (int i = 0; i < mana.Length; i++)
        {
            if (i < MP)
            {
                mana[i].sprite = fullMana;
            }
            else
            {
                mana[i].sprite = emptyMana;
            }


            if (i < numOfMana)
            {
                mana[i].enabled = true;
            }
            else
            {
                mana[i].enabled = false;
            }


            if (MP >= 3)
            {
                glow.loop = true;
                if (Time.time > NextFireBall)
                {
                    if (TurnRight == true)
                    {
                        if (Input.GetKeyDown("p"))
                        {
                            FireBall.transform.position = PlayerPos;
                            Fireing = true;
                            BackFireing = false;
                            fireParticleFront.Play();
                            NextFireBall = Time.time + 2;
                            StartCoroutine(FireingCo());
                            MP = MP - 3;
                            glow.loop = false;
                            FindObjectOfType<AudioManager>().Play("Fireball");
                        }
                    }

                    if (TurnLeft == true)
                    {
                        if (Input.GetKeyDown("p"))
                        {
                            BackFireBall.transform.position = BackPlayerPos;
                            BackFireing = true;
                            Fireing = false;
                            fireParticleBack.Play();
                            NextFireBall = Time.time + 2;
                            StartCoroutine(BackFireingCo());
                            MP = MP - 3;
                            glow.loop = false;
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

            if (BackFireing == true)
            {
                BackFireBall.transform.Translate(BackFire * FireBallSpeed);
                BSR.enabled = true;
                BCC.enabled = true;
            }

        }

        IEnumerator FireingCo()
        {
            yield return new WaitForSeconds(1.5f);
            Fireing = false;
            SR.enabled = false;
            CC.enabled = false;
        }

        IEnumerator BackFireingCo()
        {
            yield return new WaitForSeconds(1.5f);
            BackFireing = false;
            BSR.enabled = false;
            BCC.enabled = false;
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (MP < 3)
        {
            if (NoMana == false)
            {
                if (other.gameObject.CompareTag("Mana"))
                {
                    MP = MP + 1;
                    Destroy(other.gameObject);
                    FindObjectOfType<AudioManager>().Play("Mana");
                    glow.Play();
                    StartCoroutine(NoManaCo());
                    NoMana = true;
                }

            }
        }

        IEnumerator NoManaCo()
        {
            yield return new WaitForSeconds(0.2f);
            NoMana = false;
        }

    }
}