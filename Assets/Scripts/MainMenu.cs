﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator doorAnim;
    public GameObject Canvas;
    public GameObject Menu;

    public ParticleSystem[] mainMenuGlow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        StartCoroutine("DoorOpen");
    }

    IEnumerator DoorOpen()
    {
        doorAnim.SetBool("isOpening", true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Canvas.GetComponent<FadeToBlack>().FadeBlackOutSquare());
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Demo Level");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT...");
        Application.Quit();
    }

    
}
