using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Shot;

    public float Speed;
    public float NextShot;
    public bool StopMoving;

    private SpriteRenderer SSR;
    private CircleCollider2D SCC;
    private Rigidbody2D SRB;
    private Transform ST;

    private Vector3 OriPos;

    public float HP;

    public bool isDead = false;
    bool firing;

    public Animator cloudAnim;

    public ParticleSystem bigGlitchy;
    public ParticleSystem deathParticles;
    public ParticleSystem shotImplode;
    public ParticleSystem littleGlitchy;

    // Start is called before the first frame update
    void Start()
    {
        SSR = Shot.GetComponent<SpriteRenderer>();
        SCC = Shot.GetComponent<CircleCollider2D>();
        SRB = Shot.GetComponent<Rigidbody2D>();
        ST = Shot.GetComponent<Transform>();

        OriPos = new Vector3(Shot.transform.position.x, Shot.transform.position.y, Shot.transform.position.z);

        bigGlitchy.Play();
       
    }

   

   void Update() 
    { 
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 LeftShot = new Vector3(-0.1f, 0.0f, 0.0f);
        Vector3 RightShot = new Vector3(0.1f, 0.0f, 0.0f);
        if (gameObject.CompareTag("Shooter Left"))
        {
            if (StopMoving == false)
            {
                Shot.transform.Translate(LeftShot * Speed);
            }
        }
        if (gameObject.CompareTag("Shooter Right"))
        {
            if (StopMoving == false)
            {
                Shot.transform.Translate(RightShot * Speed);
            }
        }
        if (Time.time > NextShot)
        {
            Shot.transform.position = OriPos;
            NextShot = Time.time + 2;
            SSR.enabled = true;
            SCC.enabled = true;
            littleGlitchy.Play();
            StopMoving = false;
            StartCoroutine(ImplosionShot());
        }

        if(HP < 1)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator ImplosionShot()
    {

        yield return new WaitForSeconds(1.5f);
        StopMoving = true;
        shotImplode.Play();
        littleGlitchy.Stop();
        SSR.enabled = false;
        SCC.enabled = false;
        firing = true;
    }

    IEnumerator Death()
    {
        FindObjectOfType<AudioManager>().Play("ShooterDeath");
        deathParticles.Play();
        bigGlitchy.Stop();
        NextShot = Time.time + 100;
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
        isDead = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            HP = HP - 1;
            cloudAnim.SetBool("takingDamage", true);
            FindObjectOfType<AudioManager>().Play("Glitch");
            StartCoroutine(NoMoreDmg());
            
        }
        if (other.gameObject.CompareTag("Lightning"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("TankAttack"))
        {
            HP = HP - 3;
            cloudAnim.SetBool("takingDamage", true);
            FindObjectOfType<AudioManager>().Play("Glitch");
        }
        if (other.gameObject.CompareTag("FireBall"))
        {
            HP = HP - 3;
            cloudAnim.SetBool("takingDamage", true);
            FindObjectOfType<AudioManager>().Play("Glitch");
        }
    }

    IEnumerator NoMoreDmg()
    {
        yield return new WaitForSeconds(0.5f);
        cloudAnim.SetBool("takingDamage", false);
    }


}
