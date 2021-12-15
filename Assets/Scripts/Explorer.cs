using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    //public 
    [SerializeField] float speed = 10.0f;

    //Physic and Movement
    Rigidbody2D rigidbody2d;
    float deltaX;
    float deltaY;
    //Animation
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        deltaX = horizontal * Time.fixedDeltaTime * speed;
        deltaY = vertical * Time.fixedDeltaTime * speed;
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + deltaX;
        position.y = position.y + deltaY;
        rigidbody2d.MovePosition(position);
    }

}
