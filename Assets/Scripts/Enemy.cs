using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Attack;
    private SpriteRenderer SR;
    private BoxCollider2D BC;
    private float NextAttack;

    public Animator enemyAnim;
    


    // Start is called before the first frame update
    void Start()
    {
        SR = Attack.GetComponent<SpriteRenderer>();
        BC = Attack.GetComponent<BoxCollider2D>();

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

            enemyAnim.SetBool("isAttacking", true);

            StartCoroutine(AttackCo());


        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("TankAttack"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("FireBall"))
        {
            Destroy(gameObject);
        }
    }



    IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.7f);
        SR.enabled = false;
        BC.enabled = false;

        enemyAnim.SetBool("isAttacking", false);
    }

}
