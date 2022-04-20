using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallGone : MonoBehaviour
{

    private SpriteRenderer SR;
    private CircleCollider2D CC;

    public Magic MagicScript;

    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        CC = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            SR.enabled = false;
            CC.enabled = false;
            MagicScript.Fireing = false;
            MagicScript.BackFireing = false;
        }
        if (other.gameObject.CompareTag("Shooter Right"))
        {
            SR.enabled = false;
            CC.enabled = false;
            MagicScript.Fireing = false;
            MagicScript.BackFireing = false;
        }
        if (other.gameObject.CompareTag("Shooter Left"))
        {
            SR.enabled = false;
            CC.enabled = false;
            MagicScript.Fireing = false;
            MagicScript.BackFireing = false;
        }
        if (other.gameObject.CompareTag("Spike"))
        {
            SR.enabled = false;
            CC.enabled = false;
            MagicScript.Fireing = false;
            MagicScript.BackFireing = false;
        }


    }
}
