using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    Animator animator;



    // Update is called once per frame
    // Timer controls
    private float startTime = 0f;
    private float timer = 0f;
    public float holdTime = 2.0f;
    private bool held = false;
    public KeyCode key;
    void Start()
    {
        animator = GetComponent<Animator>();

    }
    void Update()
    {
    
        // Starts the timer from when the key is pressed
        if (Input.GetKeyDown(key))
        {
            startTime = Time.time;
            timer = startTime;
            
        }

        // Adds time onto the timer so long as the key is pressed
        if (Input.GetKey(key) && held == false)
        {
            animator.SetBool("Open",true);
            Debug.Log("Check1");
            timer += Time.deltaTime;
            // Once the timer float has added on the required holdTime, changes the bool (for a single trigger), and calls the function
            if (timer > (startTime + holdTime))
            {
                //held = true;
                ButtonHeld();
            }
        }
        else {
            Debug.Log("Check");
            animator.SetBool("Open",false);
        }

    }

    // Method called after held for required time
    void ButtonHeld()
    {
        Debug.Log("held for " + holdTime + " seconds");
    }
}
