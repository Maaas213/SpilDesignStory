using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    //adjust this to change speed
    public float speed = 2f;
    //adjust this to change how high it goes
    public float height = 0.3f;

    Vector2 pos;

    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {

        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        //set the object's Y to the new calculated Y
        transform.position = new Vector2(transform.position.x, newY);
    }
}
