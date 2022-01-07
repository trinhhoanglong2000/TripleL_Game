using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    Animator animator;


    // Update is called once per frame
    // Timer controls
    private float holdTime = 2.0f;
    
    public KeyCode key;
    void Start()
    {
        animator = GetComponent<Animator>();

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
    public void OpenAction(){
        animator.SetBool("Open",true);
    }
    public void CloseAction(){
        animator.SetBool("Open",false);
    }
    // Method called after held for required time
    public void Open()
    {
        Debug.Log("OPENED");
    }
}