using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{

    public TextMeshProUGUI pauseText;

    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            pauseText.gameObject.SetActive(true);
            paused = true;
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
        {
            pauseText.gameObject.SetActive(false);
            paused = false;
            Time.timeScale = 1.0f;
        }
    }
}
