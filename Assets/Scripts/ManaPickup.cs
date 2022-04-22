﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Mana");
        }
        if (other.gameObject.CompareTag("Feet"))
        {
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Mana");
        }
    }



}
