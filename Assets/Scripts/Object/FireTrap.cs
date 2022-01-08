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
    private void Enter()
    {
        isActive = true;
        animator.SetBool("Trigger", true);
    }
    private void Exit()
    {
        animator.SetBool("Trigger", false);
        isActive = false;
        Timer = TimeActive;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();

        if (controller != null)
        {
            Enter();
            if (Timer < 0)
                controller.ChangeHealth(-1);

        }
        Mummy controllerMummy = other.GetComponent<Mummy>();
        if (controllerMummy != null)
        {
            Enter();
            if (Timer < 0)
                controllerMummy.ChangeHealth(-1);

        }
        RedMummy controllerRedMummy = other.GetComponent<RedMummy>();
        if (controllerRedMummy != null)
        {
            Enter();
            if (Timer < 0)
                controllerRedMummy.ChangeHealth(-1);

        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();

        if (controller != null)
        {
            Exit();

        }
        Mummy controllerMummy = other.GetComponent<Mummy>();
        if (controllerMummy != null)
        {
            Exit();
        }
        RedMummy controllerRedMummy = other.GetComponent<RedMummy>();
        if (controllerRedMummy != null)
        {
           Exit();
        }
    }

}
