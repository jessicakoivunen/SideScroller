using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackround : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 startPos;
    private float Width;

    // Start is called before the first frame update
    void Start()
    {
        //Get starting position and width
        startPos = transform.position;
        Width = GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Move left
        transform.position += Vector3.left * speed * Time.deltaTime;

        //Repeat background
        if (transform.position.x < startPos.x - Width / 2)
        {
            transform.position = startPos;
        }
    }
}
