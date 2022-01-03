using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mummy : MonoBehaviour
{
    public AudioClip AWakeClip;
    public float waitTime = 3f;
    public float speed = 2;
    Rigidbody2D rb;
    AIPath aIPath;
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);

    private bool Sleep = true;
    private bool Roar = false;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        aIPath = GetComponent<AIPath>();
        audioSource = GetComponent<AudioSource>();


    }


    // Update is called once per frame
    void Update()
    {

        if (!Sleep)
        {

        }
        else
        {

        }
        lookDirection = aIPath.desiredVelocity.normalized;

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", lookDirection.magnitude);

    }
    IEnumerator Awaking()
    {
        PlaySound(AWakeClip);
        yield return new WaitForSeconds(waitTime);
        //set Movement speed
        Roar = true;
        


    }
    public void PlaySound(AudioClip clip)
    {
        
        audioSource.PlayOneShot(clip);
    }
    // 
    void OnTriggerStay2D(Collider2D other)
    {
        if (Sleep && other.gameObject.tag == "PlayerLight")
        {

            float distance = Vector2.Distance(transform.position, other.gameObject.transform.position);
            Vector2 rayDirection = (other.gameObject.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance, LayerMask.GetMask("Wall"));

            if (hit.collider == null)
            { // null mean the raycast didn't hit the wall
                if (Sleep)
                {
                    StartCoroutine(Awaking());
                    Sleep = false;

                }

            }

        }
        if (!Sleep && other.gameObject.tag == "PlayerLightEnemy")
        {

            float distance = Vector2.Distance(transform.position, other.gameObject.transform.position);
            Vector2 rayDirection = (other.gameObject.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, distance, LayerMask.GetMask("Wall"));

            if (hit.collider == null)
            { // null mean the raycast didn't hit the wall

                if (Roar)
                {
                    
                    aIPath.maxSpeed = speed;
                }



            }

        }


    }
    void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "PlayerLightEnemy")
        {
           
            aIPath.maxSpeed = 0;

        }

    }

}
