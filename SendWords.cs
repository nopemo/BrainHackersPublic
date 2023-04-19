using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class SendWords : MonoBehaviour, IPointerClickHandler
{
  GameObject InputWordsField;
  void Awake()
  {
    //change the chlid TMPro text
    InputWordsField = GameObject.Find("InputWordsField");
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    Debug.Log("Clicked on SendWords");
    InputWordsField.GetComponent<InputWordsField>().SendWords();
  }
}
