using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public AudioClip HitClip;

    AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(HitClip);

        }
        Mummy controllerMummy = other.GetComponent<Mummy>();
        Debug.Log(controllerMummy);
        if (controllerMummy != null)
        {
            controllerMummy.ChangeHealth(-1);
            animator.SetTrigger("Trigger");
            audioSource.PlayOneShot(HitClip);

        }
        RedMummy controllerRedMummy = other.GetComponent<RedMummy>();
        if (controllerRedMummy != null)
        {
            controllerRedMummy.ChangeHealth(-1);
            animator.SetTrigger("Trigger");
            audioSource.PlayOneShot(HitClip);

        }


    }
}
