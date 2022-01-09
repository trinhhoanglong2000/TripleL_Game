using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoralScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Explorer player = other.gameObject.GetComponent<Explorer>();
        if (player != null)
        {
            FindObjectOfType<Tutorial>().RunDiaLog(index);
            Destroy(gameObject);
        }
        if ((index == 4|| index ==9) && other.gameObject.tag == "PlayerLight")
        {
            StartCoroutine(DelayTrigger());
        }
    }
    IEnumerator DelayTrigger()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<Tutorial>().RunDiaLog(index);

        Destroy(gameObject);
    }
}
