using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mummy : MonoBehaviour
{

    Rigidbody2D rb;
    AIPath aIPath;
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);

    private bool Sleep = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        aIPath = GetComponent<AIPath>();
        
    }


    // Update is called once per frame
    void Update()
    {
        
        if (!Sleep)
        {
            //Debug.Log(aIPath.maxSpeed);
            aIPath.maxSpeed = 1;
        }
        else
        {

        }
        lookDirection = aIPath.desiredVelocity.normalized;
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed",lookDirection.magnitude);

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerLight")
        {

            float distance = Vector2.Distance(transform.position, other.gameObject.transform.position);
            Vector2 rayDirection = (other.gameObject.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance, LayerMask.GetMask("Wall"));

            if (hit.collider == null)
            { // null mean the raycast didn't hit the wall
                Sleep = false;
                Debug.Log("Too much Light!!");
            }

            // else { inTheShadow = false; }
        }

    }

}
