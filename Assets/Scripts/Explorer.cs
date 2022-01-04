using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Explorer : MonoBehaviour
{
    //public 
    [SerializeField] float speed = 10.0f;
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;

    //Physic and Movement
    Rigidbody2D rigidbody2d;
    float deltaX;
    float deltaY;
    //Animation
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);
    private bool LightOn = true;
    private GameObject Lightcollider, LightcolliderEnemy;
    // ================={UI and Health}======================
    public int health;
    public int maxHealth;
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;

    //==================={Invincible when take dame} ==============
    public float timeInvincible = 2.0f;
    public int currentHealth { get { return health; } }
    bool isInvincible;
    float invincibleTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        Lightcollider = GameObject.FindGameObjectWithTag("PlayerLightEnemy");
        LightcolliderEnemy = GameObject.FindGameObjectWithTag("PlayerLight");

    }

    // Update is called once per frame
    void Update()
    {
        //UI HEALTH

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyheart;

            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;

            }
        }

        //
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        deltaX = horizontal * Time.fixedDeltaTime * speed;
        deltaY = vertical * Time.fixedDeltaTime * speed;
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //
        if (Input.GetKeyDown(KeyCode.O))
        {

            LightOn = !LightOn;

            Lightcollider.SetActive(LightOn);
            LightcolliderEnemy.SetActive(LightOn);
            if (!LightOn)
            {
                Light.pointLightOuterRadius = 1f;

            }
            else
            {
                Light.pointLightOuterRadius = 3f;
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + deltaX;
        position.y = position.y + deltaY;
        rigidbody2d.MovePosition(position);
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
        
        health = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
    }

}
