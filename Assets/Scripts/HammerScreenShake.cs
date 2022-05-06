using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScreenShake : MonoBehaviour
{
    public float Speed;

    public GameObject Camera;


    private Vector3 OriPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            OriPos = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 Shake1 = new Vector3(0.5f, 0.5f, 0.0f);
        Camera.transform.Translate(Shake1 * Speed);
        StartCoroutine(Shake1Co());
    }

    IEnumerator Shake1Co()
    {
        Vector3 Shake2 = new Vector3(-0.5f, -0.5f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        Camera.transform.Translate(Shake2 * Speed);
        StartCoroutine(Shake2Co());
    }
    IEnumerator Shake2Co()
        {
        Vector3 Shake3 = new Vector3(0.5f, 0.5f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        Camera.transform.Translate(Shake3 * Speed);
        StartCoroutine(Shake3Co());
        }
    IEnumerator Shake3Co()
    {
        Vector3 Shake4 = new Vector3(-0.5f, -0.5f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        Camera.transform.Translate(Shake4 * Speed);
        StartCoroutine(Shake4Co());
    }
    IEnumerator Shake4Co()
    {
        Vector3 Shake5 = new Vector3(0.5f, 0.5f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        Camera.transform.Translate(Shake5 * Speed);
        StartCoroutine(Shake5Co());
    }
    IEnumerator Shake5Co()
    {
        Vector3 Shake6 = new Vector3(-0.5f, -0.5f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        Camera.transform.Translate(Shake6 * Speed);
        StartCoroutine(Shake6Co());
    }
    IEnumerator Shake6Co()
    {
        Vector3 Shake7 = new Vector3(0.5f, 0.5f, 0.0f);
        yield return new WaitForSeconds(0.05f);
        Camera.transform.Translate(Shake7 * Speed);
        StartCoroutine(Shake7Co());
    }
    IEnumerator Shake7Co()
    {
        
        yield return new WaitForSeconds(0.2f);
        Camera.transform.position = OriPos;
    }
}
