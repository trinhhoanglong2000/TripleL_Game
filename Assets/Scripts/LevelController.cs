using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{

    // Start is called before the first frame update
    Animator animator;
    public GameObject LevelGate;

    private bool levelfinish = false;


    void Start()
    {
        animator = GetComponent<Animator>();

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
            //Win
            StartCoroutine(LoadNextScene());
        }
    }
    IEnumerator LoadNextScene()
    {
        animator.SetTrigger("Open");

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
