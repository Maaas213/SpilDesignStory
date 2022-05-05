using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Shot;

    public float Speed;
    public float NextShot;

    private SpriteRenderer SSR;
    private CircleCollider2D SCC;
    private Rigidbody2D SRB;
    private Transform ST;

    private Vector3 OriPos;

    public float HP;

    public Animator cloudAnim;

    public ParticleSystem bigGlitchy;
    public ParticleSystem deathParticles;

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

    // Update is called once per frame
    void Update()
    {
        Vector3 LeftShot = new Vector3(-0.1f, 0.0f, 0.0f);
        Vector3 RightShot = new Vector3(0.1f, 0.0f, 0.0f);
        if (gameObject.CompareTag("Shooter Left"))
        {
            Shot.transform.Translate(LeftShot * Speed);
        }
        if (gameObject.CompareTag("Shooter Right"))
        {
            Shot.transform.Translate(RightShot * Speed);
        }
        if (Time.time > NextShot)
        {
            Shot.transform.position = OriPos;
            NextShot = Time.time + 2;
            SSR.enabled = true;
            SCC.enabled = true;
        }

        if(HP < 1)
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        deathParticles.Play();
        bigGlitchy.Stop();
        NextShot = Time.time + 10;
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            HP = HP - 1;
            cloudAnim.SetBool("takingDamage", true);
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
        }
        if (other.gameObject.CompareTag("FireBall"))
        {
            HP = HP - 3;
            cloudAnim.SetBool("takingDamage", true);
        }
    }

    IEnumerator NoMoreDmg()
    {
        yield return new WaitForSeconds(0.5f);
        cloudAnim.SetBool("takingDamage", false);
    }

}
