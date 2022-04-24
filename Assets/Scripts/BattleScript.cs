using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fungus;

public class BattleScript : MonoBehaviour
{

    public float Speed = 2;
    //public float FireballSpeed;
    public float JumpForce;
    //public float NextLightning;
    public float NextMelee;
    private float NextTankAttack;
    //public float NextShield;
    //public float NextFireball;
    public float HP;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Rigidbody2D RB;

    public bool OnGround;
    //public bool HealerShield;

    public GameObject Melee;
    //public GameObject Lightning;
    public GameObject TankAttack;
    public GameObject BackMelee;
    //public GameObject BackLightning;
    public GameObject BackTankAttack;
    //public GameObject Fireball;
    //public GameObject BackFireball;
    public GameObject Feet;

    private SpriteRenderer SR;
    private CapsuleCollider2D CC;

    private SpriteRenderer MSR;
    private BoxCollider2D MBC;
    //private SpriteRenderer LSR;
    //private BoxCollider2D LBC;
    private SpriteRenderer TASR;
    private CapsuleCollider2D TACC;
    private SpriteRenderer FBSR;
    private CircleCollider2D FBCC;
    private Rigidbody2D FBRB;
    private BoxCollider2D FBC;
    private Transform FBT;


    private SpriteRenderer BMSR;
    private BoxCollider2D BMBC;
    //private SpriteRenderer BLSR;
    //private BoxCollider2D BLBC;
    private SpriteRenderer BTASR;
    private CapsuleCollider2D BTACC;
    private SpriteRenderer BFBSR;
    private CircleCollider2D BFBCC;
    private Rigidbody2D BFBRB;
    private Transform BFBT;

    private Vector3 OriPos1;
    private Vector3 OriPos2;

    private bool TurnRight;
    private bool TurnLeft;
    private bool Invul;

    public Animator anim;
    private Animator meleeAnim;
    private Animator backMeleeAnim;
    private Animator tankAttackAnim;
    private Animator backTankAttackAnim;

    public KeyCode[] interactionskeys;
    public Flowchart flowchart;
    public bool inDialog;
    public Interactive currentInteractive;

    public ParticleSystem dust;

    //public bool soundPlaying;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        MSR = Melee.GetComponent<SpriteRenderer>();
        MBC = Melee.GetComponent<BoxCollider2D>();
        //LSR = Lightning.GetComponent<SpriteRenderer>();
        //LBC = Lightning.GetComponent<BoxCollider2D>();
        BMSR = BackMelee.GetComponent<SpriteRenderer>();
        BMBC = BackMelee.GetComponent<BoxCollider2D>();
        //BLSR = BackLightning.GetComponent<SpriteRenderer>();
        //BLBC = BackLightning.GetComponent<BoxCollider2D>();
        TASR = TankAttack.GetComponent<SpriteRenderer>();
        TACC = TankAttack.GetComponent<CapsuleCollider2D>();
        BTASR = BackTankAttack.GetComponent<SpriteRenderer>();
        BTACC = BackTankAttack.GetComponent<CapsuleCollider2D>();
        //FBSR = Fireball.GetComponent<SpriteRenderer>();
        //FBCC = Fireball.GetComponent<CircleCollider2D>();
        //BFBSR = BackFireball.GetComponent<SpriteRenderer>();
        //BFBCC = BackFireball.GetComponent<CircleCollider2D>();
        FBC = Feet.GetComponent<BoxCollider2D>();
        //FBT = Fireball.GetComponent<Transform>();
        //BFBT = BackFireball.GetComponent<Transform>();
        //FBRB = Fireball.GetComponent<Rigidbody2D>();
        //BFBRB = BackFireball.GetComponent<Rigidbody2D>();



        SR = GetComponent<SpriteRenderer>();
        CC = GetComponent<CapsuleCollider2D>();

        MSR.enabled = false;
        MBC.enabled = false;
        //LSR.enabled = false;
        //LBC.enabled = false;
        TASR.enabled = false;
        TACC.enabled = false;
        BMSR.enabled = false;
        BMBC.enabled = false;
        //BLSR.enabled = false;
        //BLBC.enabled = false;
        BTACC.enabled = false;
        BTASR.enabled = false;
        //FBSR.enabled = false;
        //FBCC.enabled = false;
        //BFBSR.enabled = false;
        //BFBCC.enabled = false;

        TurnRight = true;
        TurnLeft = false;
        HP = 3;
        Invul = false;

        anim = GetComponent<Animator>();
        meleeAnim = Melee.GetComponent<Animator>();
        backMeleeAnim = BackMelee.GetComponent<Animator>();
        tankAttackAnim = TankAttack.GetComponent<Animator>();
        backTankAttackAnim = BackTankAttack.GetComponent<Animator>();

   
        //OriPos1 = new Vector3(Fireball.transform.position.x, Fireball.transform.position.y, Fireball.transform.position.z);
        //OriPos2 = new Vector3(BackFireball.transform.position.x, BackFireball.transform.position.y, BackFireball.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        Interact();

        if (!inDialog)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            
            if ((Input.GetKey("a") || Input.GetKey("d")) && OnGround == true)
            {
                CreateDust();
            }

            //RB.AddForce(movement * Speed, ForceMode2D.Impulse);
            transform.Translate(movement * Speed);

            anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));

            Vector3 Jump = new Vector3(0.0f, 1, 0.0f);
            if (OnGround == true)
            {
                if (Input.GetKeyDown("w"))
                {

                    //transform.Translate(Jump * JumpForce);
                    RB.AddForce(Jump * JumpForce);
                    FindObjectOfType<AudioManager>().Play("Jump");
                    OnGround = false;
                    anim.SetBool("isJumping", true);
                    CreateDust();

                    //StartCoroutine(DoubleJumpCoRoutine());

                }
            }
            if (OnGround == true)
            {
                anim.SetBool("isJumping", false);
            }

            if (OnGround == false)
            {
                FindObjectOfType<AudioManager>().Stop("Steps2");
                FindObjectOfType<AudioManager>().Stop("Steps1");
            }



            /*if (Time.time > NextLightning)
            {
                if (TurnRight == true)
                {
                    if (Input.GetKeyDown("l"))
                    {
                        LSR.enabled = true;
                        LBC.enabled = true;
                        NextLightning = Time.time + 5;
                        StartCoroutine(LightningCoRoutine());
                    }
                }
                if (TurnLeft == true)
                {
                    if (Input.GetKeyDown("l"))
                    {
                        BLSR.enabled = true;
                        BLBC.enabled = true;
                        NextLightning = Time.time + 5;
                        StartCoroutine(BackLightningCoRoutine());
                    }
                }
            }*/

            if (Time.time > NextMelee)
            {
                if (TurnRight == true)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        MSR.enabled = true;
                        MBC.enabled = true;

                        FindObjectOfType<AudioManager>().Play("Sword");

                        meleeAnim.SetBool("isAttacking", true);

                        NextMelee = Time.time + 0.2f;
                        StartCoroutine(MeleeCoRoutine());
                    }
                }
                if (TurnLeft == true)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        BMSR.enabled = true;
                        BMBC.enabled = true;

                        FindObjectOfType<AudioManager>().Play("Sword");

                        backMeleeAnim.SetBool("isAttacking", true);

                        NextMelee = Time.time + 0.2f;
                        StartCoroutine(BackMeleeCoRoutine());


                    }
                }


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
            }

            if (Time.time > NextTankAttack)
            {
                if (TurnRight == true)
                {
                    if (Input.GetKeyDown("t"))
                    {
                        TASR.enabled = true;
                        TACC.enabled = true;

                        tankAttackAnim.SetBool("isHitting", true);

                        NextTankAttack = Time.time + 5;
                        StartCoroutine(TankAttackCoRoutine());
                        FindObjectOfType<AudioManager>().Play("Ax");

                    }
                }

                if (TurnLeft == true)
                {
                    if (Input.GetKeyDown("t"))
                    {
                        BTASR.enabled = true;
                        BTACC.enabled = true;

                        backTankAttackAnim.SetBool("isHitting", true);

                        StartCoroutine(BackTankAttackCoRoutine());
                        NextTankAttack = Time.time + 5;
                    }
                }
            }

            /*
            if ( Time.time > NextFireball)
            { 
                if(Input.GetKeyDown("f"))
                {
                    {
                        if (TurnRight == true)
                        {
                            Vector3 FireballShot = new Vector3(1.0f, 0.0f, 0.0f);
                            Fireball.transform.Translate(FireballShot * FireballSpeed);
                            FBSR.enabled = true;
                            FBCC.enabled = true;
                            StartCoroutine(FireBallCoRoutine());
                            NextFireball = Time.time + 3;
                        }
                        if (TurnLeft == true)
                        {
                            Vector3 BackFireballShot = new Vector3(-1.0f, 0.0f, 0.0f);
                            BackFireball.transform.Translate(BackFireballShot * FireballSpeed);
                            BFBCC.enabled = true;
                            BFBSR.enabled = true;
                            StartCoroutine(BackFireballCoRoutine());
                            NextFireball = Time.time + 3;
                        }
                    }
                }
            }
            */
                

            if (TurnRight == true)
            {
                SR.flipX = true;
            }
            if (TurnLeft == true)
            {
                SR.flipX = false;
            }

            if (HP == 0)
            {
                SceneManager.LoadScene("Demo Level");
            }

            if (HP > numOfHearts)
            {
                HP = numOfHearts;
            }

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < HP)
                {
                    hearts[i].sprite = fullHeart;
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                }


                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
        
    }
    private void FixedUpdate()
    {



    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        //OnGround = true;

        
        if (other.gameObject.CompareTag("Enemy1"))
        {
            SceneManager.LoadScene("Level 1");
        }
        if(other.gameObject.CompareTag("Enemy2"))
        {
            SceneManager.LoadScene("Level 2");
        }

        if (other.gameObject.CompareTag("Goal1")) 
        {
            SceneManager.LoadScene("Level 2");
        }

        if (Invul == false)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                HP = HP - 1;
                Invul = true;

                anim.SetBool("takingDamage", true);

                StartCoroutine(InvulCo());
                
            }
            if (other.gameObject.CompareTag("Spike"))
            {
                HP = HP - 1;
                Invul = true;

                anim.SetBool("takingDamage", true);

                StartCoroutine(InvulCo());
            }
        }


        if (other.gameObject.CompareTag("Death"))
        {
            HP = 0;
        }
    }

    IEnumerator InvulCo()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("takingDamage", false);
        yield return new WaitForSeconds(1);
        Invul = false;
        
    }

    IEnumerator Flash()
    {
        yield return new WaitForSeconds(0.1f);
        
        
    }

    IEnumerator MeleeCoRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        MSR.enabled = false;
        MBC.enabled = false;
        meleeAnim.SetBool("isAttacking", false);
    }

    /*
    IEnumerator LightningCoRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        LSR.enabled = false;
        LBC.enabled = false;
    }*/

    /*
    IEnumerator BackLightningCoRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        BLSR.enabled = false;
        BLBC.enabled = false;
    }
    */

    IEnumerator BackMeleeCoRoutine()
    {
        yield return new WaitForSeconds(0.7f);
        BMSR.enabled = false;
        BMBC.enabled = false;
        backMeleeAnim.SetBool("isAttacking", false);
    }

    IEnumerator TankAttackCoRoutine ()
    {
        yield return new WaitForSeconds(0.7f);
        TASR.enabled = false;
        TACC.enabled = false;

        tankAttackAnim.SetBool("isHitting", false);

    }

    IEnumerator BackTankAttackCoRoutine ()
    {
        yield return new WaitForSeconds(0.7f);
        BTASR.enabled = false;
        BTACC.enabled = false;

        backTankAttackAnim.SetBool("isHitting", false);
    }

    /*
    IEnumerator FireBallCoRoutine ()
    {
      yield return new WaitForSeconds(3f);
        FBCC.enabled = false;
        FBSR.enabled = false;
        Fireball.transform.position = OriPos1;
    }
    
    IEnumerator BackFireballCoRoutine ()
    {
       yield return new WaitForSeconds(3f);
        BFBSR.enabled = false;
        BFBCC.enabled = false;
        BackFireball.transform.position = OriPos2;
    }
    */

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
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Stop("Ambience1");
    }

    public void ExitBlock()
    {
        inDialog = false;
        FindObjectOfType<AudioManager>().Play("Theme");
        FindObjectOfType<AudioManager>().Play("Ambience1");
    }

    void CreateDust()
    {
        dust.Play();
    }
}
