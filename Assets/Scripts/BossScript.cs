using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public int HP;


    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0)
        {
            Destroy(gameObject);
            //SceneManager.LoadScene("Samplescene");

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
     if(other.gameObject.CompareTag("Melee"))
        {
            HP--;
        }
     if (other.gameObject.CompareTag("Lightning"))
        {
            HP = HP - 5;
        }
    }


}
