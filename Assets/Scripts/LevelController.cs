using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{

    // Start is called before the first frame update
    Animator animator;
    public GameObject LevelGate;
    public AudioClip OpenClip;

    private bool levelfinish = false;

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
    public void setLevelFinish()
    {
        levelfinish = true;
        LevelGate.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().color = Color.green;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Explorer player = other.gameObject.GetComponent<Explorer>();
        if (player != null && levelfinish)
        {
            player.BecomeInvi();
            //Win
            StartCoroutine(LoadNextScene());
        }
    }
    IEnumerator LoadNextScene()
    {
        animator.SetTrigger("Open");
        audioSource.PlayOneShot(OpenClip);
        yield return new WaitForSeconds(2f);
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
        GameInfo gameinfo = FindObjectOfType<GameInfo>();
        Explorer explorer = FindObjectOfType<Explorer>();
        gameinfo.setCanvas(true);
        gameinfo.setStat(explorer.speed, explorer.health, explorer.maxHealth, explorer.Energy, explorer.MaxEnergy);







    }
}
