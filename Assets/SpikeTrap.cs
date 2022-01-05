using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;


    void Start()
    {

        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
            animator.SetTrigger("Trigger");

        }

    }
}
