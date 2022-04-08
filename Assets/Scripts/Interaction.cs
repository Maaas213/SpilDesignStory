using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{

    public GameObject GrahamText;

    // Start is called before the first frame update
    void Start()
    {
        GrahamText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Graham1"))
        {
            if(Input.GetKeyDown("i"))
            {
                GrahamText.SetActive(true);
                if(Input.GetKeyDown("i"))
                {
                    GrahamText.SetActive(false);
                }

            }
        }

    }
}
