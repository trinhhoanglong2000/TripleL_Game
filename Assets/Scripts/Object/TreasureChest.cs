using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public AudioClip OpenClip;
    public GameObject Dialog;
    private bool doOneTime = false;
    // Update is called once per frame
    // Timer controls
    //private float holdTime = 2.0f;

    public KeyCode key;
    private bool opened = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    // void Update()
    // {

    //     // Starts the timer from when the key is pressed
    //     if (Input.GetKeyDown(key))
    //     {
    //         startTime = Time.time;
    //         timer = startTime;

    //     }

    //     // Adds time onto the timer so long as the key is pressed
    //     if (Input.GetKey(key) && held == false)
    //     {
    //         animator.SetBool("Open",true);
    //         Debug.Log("Check1");
    //         timer += Time.deltaTime;
    //         // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
    //         if (timer > (startTime + holdTime))
    //         {
    //             //held = true;
    //             ButtonHeld();
    //         }
    //     }
    //     else {
    //         Debug.Log("Check");
    //         animator.SetBool("Open",false);
    //     }

    // }
    public void OpenAction()
    {
        animator.SetBool("Open", true);
        if (!doOneTime)
        {
            audioSource.PlayOneShot(OpenClip);
            doOneTime = true;
        }
    }
    public void CloseAction()
    {
        animator.SetBool("Open", false);
        audioSource.Stop();
        doOneTime = false;
    }
    // Method called after held for required time
    IEnumerator DelayFade()
    {
        animator.SetTrigger("Fade");
        audioSource.Stop();
        yield return new WaitForSeconds(1f);
        //FindObjectOfType<GameInfo>().PlusPoint();
        FindObjectOfType<Explorer>().ChangeHealth(1);
        FindObjectOfType<Explorer>().RechareLight(1);

        Destroy(gameObject);

    }
    public void Open()
    {
        if (!opened)
        {
            Debug.Log("OPENED");
            opened = true;
            doOneTime = false;
            Dialog.SetActive(true);
            StartCoroutine(DelayFade());
        }
    }
}
