using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private SpriteRenderer SR;
    private CircleCollider2D CC;

    public Shooter S;

    public ParticleSystem glitch;
    public ParticleSystem shotImplode;


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
            shotImplode.Play();
            glitch.Stop();
            SR.enabled = false;
            CC.enabled = false;
            S.StopMoving = true;
        }
    }

}
