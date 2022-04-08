using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject Attack;
    private SpriteRenderer SR;
    private BoxCollider2D BC;
    private float NextAttack;
    


    // Start is called before the first frame update
    void Start()
    {
        SR = Attack.GetComponent<SpriteRenderer>();
        BC = Attack.GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > NextAttack)
        {
            SR.enabled = true;
            BC.enabled = true;
            NextAttack = Time.time + Random.Range(2, 6);
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
    }



    IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.5f);
        SR.enabled = false;
        BC.enabled = false;
    }

    }
