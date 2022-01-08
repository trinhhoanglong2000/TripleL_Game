using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingRock : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    private float pivot = 0f;
    private bool doOneTime = false;
    AudioSource audioSource;
    public AudioClip MiningClip;
    Animator animator;
    
    // Update is called once per frame
    void Start(){
        pivot = Time.deltaTime/2;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Light.pointLightOuterRadius >= 1){
            pivot = -Time.deltaTime/2;
        }
        else if (Light.pointLightOuterRadius <= 0.5){
            pivot = +Time.deltaTime/2;
        }
        Light.pointLightOuterRadius += pivot;
        //else if 
        
    }
    public void SoundController(bool type){
        if (!type && !doOneTime){
            audioSource.PlayOneShot(MiningClip);
            doOneTime= true;
        }
        else if (type && doOneTime){
            audioSource.Stop();
            doOneTime= false;
        }
    }
    IEnumerator DelayFade()
    {
        animator.SetTrigger("Fade");
        audioSource.Stop();
        yield return new WaitForSeconds(1f);
        //set Movement speed
        Destroy(gameObject);

    }
    public void comsume(){
        StartCoroutine(DelayFade());
        
    }
}
