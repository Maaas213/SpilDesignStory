using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileScript : MonoBehaviour
{
    private SpriteRenderer SR;
    private BoxCollider2D BC;

    // Start is called before the first frame update
    void Start()
    {
        BC = gameObject.GetComponent<BoxCollider2D>();
        SR = gameObject.GetComponent<SpriteRenderer>();
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
        yield return new WaitForSeconds(0.3f);
        BC.enabled = false;
        SR.enabled = false;
        StartCoroutine(Fix());
    }

    private IEnumerator Fix()
    {
        yield return new WaitForSeconds(4);
        BC.enabled = true;
        SR.enabled = true;
    }
}
