using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBallBackAndForth : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;
    //public MoveBallBackAndForth MBBAFScript;

    private Animator enemyAnim;
    private SpriteRenderer SR;

    Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;

        enemyAnim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();

        //if (gameObject.CompareTag("Mover"))
        {
            //MBBAFScript.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()

    {
        enemyAnim.SetBool("isMoving", true);

        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
            SR.flipX = false;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
            SR.flipX = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (gameObject.CompareTag("Mover"))
        {
            //if (other.gameObject.CompareTag("Player"))
            {
                //MBBAFScript.enabled = true;
            }
        }
    }
}
