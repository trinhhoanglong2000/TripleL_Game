using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RedMummy : MonoBehaviour
{
    public AudioClip AWakeClip;
    public float waitTime = 3f;
    public float speed = 2;
    public int damage = 1;
    public float timeInvincible = 2.0f;
    public int maxHealth = 20;
    
    public int health;
    [Range(0, 20)]
    Rigidbody2D rb;
    
    AIPath aIPath;
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);

    private bool Sleep = true;
    private bool Roar = false;
    private bool SleepAfterHit = false;
    private bool Stand = false;
    AudioSource audioSource;
    //==================={Invincible when take dame} ==============

    bool isInvincible;
    float invincibleTimer;
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
        //Invicible when getting hit
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        //slowly run 
        if (Stand)
        {
            aIPath.maxSpeed = Mathf.Clamp(aIPath.maxSpeed - Time.deltaTime, 0, speed);
        }

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
    IEnumerator Idleing()
    {

        SleepAfterHit = true;
        //aIPath.maxSpeed = 0;

        Stand = true;

        yield return new WaitForSeconds(waitTime);
        //set Movement speed
        SleepAfterHit = false;

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // 
    void OnCollisionEnter2D(Collision2D other)
    {
        Explorer player = other.gameObject.GetComponent<Explorer>();

        if (player != null)
        {
            player.ChangeHealth(-damage);
            StartCoroutine(Idleing());
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (SleepAfterHit) return;
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
                    Stand = false;
                }



            }else{
                //awkae but is block my wall
                Stand = true;
            }

        }


    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        health = Mathf.Clamp(health + amount, 0, maxHealth);

    }
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "PlayerLightEnemy")
        {

            //aIPath.maxSpeed = 0;
            Stand = true;

        }

    }

}
