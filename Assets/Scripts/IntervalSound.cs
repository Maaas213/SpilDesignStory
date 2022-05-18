using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalSound : MonoBehaviour
{
    public BattleScript scriptA;
    public Shooter scriptB;
    public Enemy scriptC;
    bool inDialog;
    bool cloudDead;
    bool enemyDead;
    private float intervalTime;


    // Start is called before the first frame update
    void Start()
    {
        inDialog = scriptA.inDialog;
        cloudDead = scriptB.isDead;
        enemyDead = scriptC.isDead;  
        
    }

    // Update is called once per frame
    void Update()
    {
        float desiredInterval = 0;

         if (inDialog == false && cloudDead == false)
       {
            desiredInterval = Random.Range(20, 40);
            FindObjectOfType<AudioManager>().Play("Glitch");
        }

        if (inDialog == true || cloudDead == true)
        {
            desiredInterval = 0;
        }

        if (desiredInterval > 0)
        {
            FindObjectOfType<AudioManager>().Play("Glitch");
        }

        if (intervalTime <= 0)
        {
            intervalTime += desiredInterval;
        }
    }
}
