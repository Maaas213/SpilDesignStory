using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private SpriteRenderer SR;
    private CircleCollider2D CC;

    public ParticleSystem glitch;


    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        CC = GetComponent<CircleCollider2D>();

        glitch.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SR.enabled = false;
            CC.enabled = false;
        }
    }
}
