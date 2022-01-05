using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    Animator animator;
    public float TimeActive = 1f;
    bool isActive;
    private float Timer;

    void Start()
    {
        Timer = TimeActive;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isActive)
        {
            Timer -= Time.deltaTime;
           
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();

        if (controller != null)
        {
            isActive = true;
            animator.SetBool("Trigger", true);
            if (Timer <0)
                controller.ChangeHealth(-1);
             
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();

        if (controller != null)
        {
            animator.SetBool("Trigger", false);
            isActive = false;
            Timer = TimeActive;

        }
    }

}
