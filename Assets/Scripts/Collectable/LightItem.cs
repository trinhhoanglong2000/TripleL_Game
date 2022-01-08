using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightItem : MonoBehaviour
{
    public AudioClip CollectClip;
    public int rechargeAmount = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();

        if (controller != null)
        {
            // controller.RechareLight(rechargeAmount);
            Debug.Log(FindObjectOfType<GameInfo>());
            FindObjectOfType<GameInfo>().PlusPoint();
            controller.PlaySound(CollectClip);
            Destroy(gameObject);
        }
    }
}
