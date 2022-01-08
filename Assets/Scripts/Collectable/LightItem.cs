using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightItem : MonoBehaviour
{
    public AudioClip CollectClip;
    public int rechargeAmount = 3;
         void OnTriggerEnter2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();
        
        if (controller != null)
        {
            controller.RechareLight(rechargeAmount);
            controller.PlaySound(CollectClip);
            Destroy(gameObject);
        }
    }
}
