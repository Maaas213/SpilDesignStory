using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destrcuable : MonoBehaviour
{

    private SpriteRenderer SR;
    private BoxCollider2D BC;
    
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        BC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy1"))
        {
            BC.enabled = false;
            SR.enabled = false;
        }

        if (other.gameObject.CompareTag("TankAttack"))
        {
            Destroy(gameObject);
        }
    }
}