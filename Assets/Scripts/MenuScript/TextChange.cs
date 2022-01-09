using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Events; 
using UnityEngine.EventSystems; 
 
public class TextChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Text>().color = Color.red; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Text>().color = Color.black; 
    }
    public void FillBlackText()
    {
        GetComponent<Text>().color = Color.black;
    }
}