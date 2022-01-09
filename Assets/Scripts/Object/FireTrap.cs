using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTrap : MonoBehaviour
{
    Animator animator;
    public float TimeActive = 1f;
    bool isActive;
    private float Timer;
    AudioSource audioSource;

    void Start()
    {
        Timer = TimeActive;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
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
        if (!isActive){
            audioSource.Play();
        }
        isActive = true;
        animator.SetBool("Trigger", true);

    }
    private void Exit()
    {
        
        animator.SetBool("Trigger", false);
        audioSource.Stop();
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
