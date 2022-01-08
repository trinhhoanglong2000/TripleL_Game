using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingRock : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    private float pivot = 0f;
    Animator animator;
    // Update is called once per frame
    void Start(){
        pivot = Time.deltaTime/2;
        animator = GetComponent<Animator>();
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
    IEnumerator DelayFade()
    {
        animator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        //set Movement speed
        Destroy(gameObject);


    }
    public void comsume(){
        StartCoroutine(DelayFade());
        
    }
}
