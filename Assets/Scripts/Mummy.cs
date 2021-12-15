using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : MonoBehaviour
{


    private bool Sleep = true;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (!Sleep)
        {
            //Replace following lines with your own magic
            //Debug.Log("Vampire Secure Area!!");
        }
        else
        {
            //Debug.Log("Too much Light!!");
        }
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
