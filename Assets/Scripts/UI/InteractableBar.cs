using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractableBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Image barImage;
    private void Awake(){
        barImage = transform.Find("Bar").GetComponent<Image>();
        barImage.fillAmount = .3f;
    }
}
