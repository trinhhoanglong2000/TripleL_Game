using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Explorer : MonoBehaviour
{
    //public 


    [Header("Stats")]
    public float speed = 10.0f;
    [Range(0, 11)]
    public int health;
    [Range(0, 11)]

    public int maxHealth;
    public float Energy;
    public float MaxEnergy;

    public int props_health { get { return health; } }
    public int props_maxhealth { get { return maxHealth; } }
    public float props_Energy { get { return Energy; } }
    public float props_MaxEnergy { get { return MaxEnergy; } }
    public float props_speed { get { return speed; } }



    public float timeInvincible = 2.0f;
    public float Eneryrate = 1f;
    public float delayTurnOn = 1f;
    public int currentHealth { get { return health; } }
    public float maxLight = 3f, minLight = 1f;
    public bool LightOn = false;

    public KeyCode interactKey;

    [Header("Reference gameobjects")]
    public UnityEngine.Experimental.Rendering.Universal.Light2D Light;
    [Header("UI GameObject")]
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyheart;
    public Image EnergyBar;
    public Image EnergyIcon;
    public GameObject ImageProgressBar;
    public ParticleSystem blood;
    [Header("Audio ")]

    public AudioClip HitClip;
    public AudioClip Walking;
    public float progressTime = 2f;
    private float LerpSpeed;
    private bool isDead = false;
    public GameObject FaintUI;
    // Timer ---------------------------------------------
    private float timer = 0f;


    //Physic and Movement
    Rigidbody2D rigidbody2d;
    float deltaX;
    float deltaY;
    //Animation
    Animator animator;
    Vector2 lookDirection = new Vector2(0, -1);
    private bool outEnergy = false;
    private Image processbar;

    private bool isDelay = false;
    private GameObject Lightcollider, LightcolliderEnemy;



    //==================={Invincible when take dame} ==============

    bool isInvincible;
    float invincibleTimer;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        blood.Stop();
        rigidbody2d = GetComponent<Rigidbody2D>();
        Lightcollider = GameObject.FindGameObjectWithTag("PlayerLightEnemy");
        LightcolliderEnemy = GameObject.FindGameObjectWithTag("PlayerLight");
        LerpSpeed = 3f * Time.deltaTime;
        processbar = ImageProgressBar.transform.Find("Bar").GetComponent<Image>();
        if (LightOn)
        {
            Light.pointLightOuterRadius = maxLight;
        }
        else
        {
            Light.pointLightOuterRadius = minLight;

        }
        Lightcollider.SetActive(LightOn);
        LightcolliderEnemy.SetActive(LightOn);
        audioSource = GetComponent<AudioSource>();

        //get Stats



    }
    public void setStat(float a, int b, int c, float d, float e)
    {
        speed = a;
        health = b;
        maxHealth = c;
        Energy = d;
        MaxEnergy = e;
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
        //===========================|UI Energy|=======================
        EnergyBar.fillAmount = Mathf.Lerp(EnergyBar.fillAmount, Energy / MaxEnergy, LerpSpeed);
        //EnergyBar.fillAmount = Energy / MaxEnergy;
        Color energycolor = Color.Lerp(Color.red, Color.green, Energy / MaxEnergy);
        EnergyBar.color = energycolor;
        EnergyIcon.color = energycolor;

        //light slowly turn on or turn off

        if (!LightOn || outEnergy)
        {
            Light.pointLightOuterRadius = Mathf.Clamp(Light.pointLightOuterRadius - Time.deltaTime * 4f, minLight, maxLight);
            //Light.pointLightOuterRadius = Mathf.Lerp(Light.pointLightOuterRadius, 1f, LerpSpeed);

        }
        else
        {
            Light.pointLightOuterRadius = Mathf.Clamp(Light.pointLightOuterRadius + Time.deltaTime * 4f, minLight, maxLight);
        }

        // check invincible
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        //check energy
        if (Mathf.Approximately(Energy, 0.0f))
        {
            outEnergy = true;
            if (LightOn)
            {
                Lightcollider.SetActive(false);
                LightcolliderEnemy.SetActive(false);
                //Light.pointLightOuterRadius = 1f;
            }

        }
        else
        {
            outEnergy = false;

            if (LightOn)
            {
                Lightcollider.SetActive(true);
                LightcolliderEnemy.SetActive(true);
                //Light.pointLightOuterRadius = 3f;

            }
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
        if (Input.GetKeyDown(KeyCode.O) && !isDead)
        {
            //delay when turn on
            if (isDelay)
            {
                return;
            }
            LightOn = !LightOn;

            Lightcollider.SetActive(LightOn);
            LightcolliderEnemy.SetActive(LightOn);

            StartCoroutine(DelayTurnOn());
        }

        //INTERACTABLE ----------------------------------
        // Starts the timer from when the key is pressed
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1f, LayerMask.GetMask("Interactable"));
            if (hit.collider != null)
            {
                timer = 0f;
                processbar.fillAmount = 0;
                ImageProgressBar.SetActive(true);

            }

        }

        // Adds time onto the timer so long as the key is pressed
        if (Input.GetKey(interactKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1f, LayerMask.GetMask("Interactable"));
            if (hit.collider != null)
            {
                //ImageProgressBar.SetActive(true);

                timer += Time.deltaTime;
                processbar.fillAmount = timer / progressTime;
                TreasureChest chest = hit.collider.GetComponent<TreasureChest>();
                GlowingRock rock = hit.collider.GetComponent<GlowingRock>();
                Debug.Log("Raycast has hit the object " + hit.collider);

                //Interact with chest
                if (chest != null)
                {
                    chest.OpenAction();
                    if (timer > (progressTime))
                    {
                        chest.Open();
                        ImageProgressBar.SetActive(false);
                    }
                }
                //Interact with glowing rock
                if (rock != null)
                {
                    rock.SoundController(false);
                    if (timer > (progressTime))
                    {
                        rock.comsume();
                        ImageProgressBar.SetActive(false);
                        RechareLight(10);
                    }
                }


            }
            else
            {
                GameObject[] tags = GameObject.FindGameObjectsWithTag("TreasureChest");

                foreach (GameObject tag in tags)
                {
                    TreasureChest chest = tag.GetComponent<TreasureChest>();
                    chest.CloseAction();
                }
                tags = GameObject.FindGameObjectsWithTag("GlowingRock");
                foreach (GameObject tag in tags)
                {
                    GlowingRock rock = tag.GetComponent<GlowingRock>();
                    rock.SoundController(true);
                }
            }
        }
        if (Input.GetKeyUp(interactKey))
        {
            ImageProgressBar.SetActive(false);
            GameObject[] tags = GameObject.FindGameObjectsWithTag("TreasureChest");

            foreach (GameObject tag in tags)
            {
                TreasureChest chest = tag.GetComponent<TreasureChest>();
                chest.CloseAction();
            }
            tags = GameObject.FindGameObjectsWithTag("GlowingRock");
            foreach (GameObject tag in tags)
            {
                GlowingRock rock = tag.GetComponent<GlowingRock>();
                rock.SoundController(true);
            }
        }
    }
    IEnumerator DelayTurnOn()
    {
        isDelay = true;
        yield return new WaitForSeconds(delayTurnOn);
        //set Movement speed
        isDelay = false;


    }
    IEnumerator DelayBlood()
    {
        blood.Play();
        yield return new WaitForSeconds(1f);
        //set Movement speed
        blood.Stop();


    }
    void FixedUpdate()
    {
        //Energy Down
        if (LightOn)
        {
            Energy = Mathf.Clamp(Energy - Eneryrate * Time.deltaTime, 0, MaxEnergy);
        }
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
            StartCoroutine(DelayBlood());

            PlaySound(HitClip);
        }
        health = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (health == 0)
        {
            StartCoroutine(Dead());
        }
    }

    public void RechareLight(int amount)
    {

        Energy = Mathf.Clamp(Energy + amount, 0, MaxEnergy);

    }
    IEnumerator Dead()
    {
        animator.SetTrigger("Dead");
        Lightcollider.SetActive(false);
        LightcolliderEnemy.SetActive(false);
        LightOn = false;
        isDead = true;
        speed = 0;
        yield return new WaitForSeconds(1f);
        FaintUI.SetActive(true);

    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void WalkingSound()
    {
        PlaySound(Walking);
    }
}
