using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : MonoBehaviour
{

    private Rigidbody2D PRB;
    public BattleScript BS;
    public float PushForce;
    private bool Invul;
    private bool TurnRight;
    private bool TurnLeft;

    // Start is called before the first frame update
    void Start()
    {
        PRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("d"))
        {
            TurnRight = true;
            TurnLeft = false;
        }
        if (Input.GetKeyDown("a"))
        {
            TurnLeft = true;
            TurnRight = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 PushBack = new Vector2(-2, 2);
        Vector2 PushBack2 = new Vector2(2, 2);

        if (Invul == false)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {

                BS.enabled = false;
                if (TurnRight == true)
                {
                    PRB.AddForce(PushBack * PushForce, ForceMode2D.Impulse);
                }
                if(TurnLeft == true)
                {
                    PRB.AddForce(PushBack2 * PushForce, ForceMode2D.Impulse);
                }

                StartCoroutine(PushCo());
                Invul = true;
                
            }
            


        }
    }

    IEnumerator PushCo()
    {
        yield return new WaitForSeconds(0.7f);
        BS.enabled = true;
        Invul = false;
     }
}
