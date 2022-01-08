using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour

{
public AudioClip CollectClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        Explorer controller = other.GetComponent<Explorer>();

        if (controller != null)
        {
            controller.ChangeHealth(1);
            controller.PlaySound(CollectClip);
            Destroy(gameObject);
        }
    }
}
