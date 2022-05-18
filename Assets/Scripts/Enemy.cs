using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Attack;
    private SpriteRenderer SR;
    private BoxCollider2D BC;
    private CapsuleCollider2D CC;
    private float NextAttack;
    public float HP;

    public Animator enemyAnim;

    public ParticleSystem blood;

    public MoveBallBackAndForth Movement;

    public bool isDead = false;

    



    // Start is called before the first frame update
    void Start()
    {
        SR = Attack.GetComponent<SpriteRenderer>();
        BC = Attack.GetComponent<BoxCollider2D>();

        CC = GetComponent<CapsuleCollider2D>();

        enemyAnim = GetComponent<Animator>();
    }

   

    // Update is called once per frame
    void Update()
    {
        if(Time.time > NextAttack)
        {
            SR.enabled = true;
            BC.enabled = true;
            NextAttack = Time.time + Random.Range(2, 6);
            Movement.enabled = false;

            enemyAnim.SetBool("isAttacking", true);

            StartCoroutine(AttackCo());


        }
        if (HP < 1)
        {
            Movement.enabled = false;
            enemyAnim.SetBool("takingDamage", false);
            enemyAnim.SetBool("isDead", true);
            isDead = true;
            StartCoroutine(Death());
            CC.enabled = false;
        }

        /*if (isDead == false && inDialog == false)
        {
            if(moaningActive == false)
            {
                StartCoroutine(_coroutine);
            }

            else
            {
                doneNow();
            }
            
        }


        /*if (inDialog)
        {
            StopCoroutine(Moaning());
            moaningActive = false;
        }*/

        /*if (!inDialog && moaningActive == false && isDead == false)
        {
            StartCoroutine(Moaning());
            //moaningActive = true;
        }*/

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            //CC.enabled = false;
            Movement.enabled = false;
            HP = HP - 1;
            enemyAnim.SetBool("takingDamage", true);
            FindObjectOfType<AudioManager>().Play("EnemyDamage");
            FindObjectOfType<AudioManager>().Play("EnemyDamage2");
            FindObjectOfType<AudioManager>().Play("Moaning");
            StartCoroutine(BloodAct()); 
        }
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("TankAttack"))
        {
            HP = HP - 3;
            enemyAnim.SetBool("takingDamage", true);
            StartCoroutine(BloodAct());
            //Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("FireBall"))
        {
            HP = HP - 3;
            //Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Moaning");
        }


    }



    IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.6f);
        SR.enabled = false;
        BC.enabled = false;

        enemyAnim.SetBool("isAttacking", false);
        Movement.enabled = true;
    }

    IEnumerator BloodAct()
    {
        
        BloodSpray();
        yield return new WaitForSeconds(1f);
        enemyAnim.SetBool("takingDamage", false);
        Movement.enabled = true;
        //Destroy(gameObject);

    }
    IEnumerator Death()
    {
        BloodSpray();
        FindObjectOfType<AudioManager>().Play("EnemyDeath");
        yield return new WaitForSeconds(2.8f);
        Destroy(gameObject);
    }

    void BloodSpray()
    {
        blood.Play();
    }

}

