using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileScript : MonoBehaviour
{
    private SpriteRenderer SR;
    private BoxCollider2D BC;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        BC = gameObject.GetComponent<BoxCollider2D>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Feet"))
        {
            StartCoroutine(Breaking());

        }
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Breaking());

        }
    }

    private IEnumerator Breaking ()
    {
        FindObjectOfType<AudioManager>().Play("Crack");
        anim.SetBool("isCrumbling", true);
        yield return new WaitForSeconds(0.75f);
        BC.enabled = false;
        SR.enabled = false;
        StartCoroutine(Fix());
    }

    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("isCrumbling", false);
        yield return new WaitForSeconds(2f);
        BC.enabled = true;
        SR.enabled = true;
        anim.SetBool("isRespawning", true);
        yield return new WaitForSeconds(0.65f);
        anim.SetBool("isRespawning", false);
    }
}
