using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class SendWords : MonoBehaviour, IPointerClickHandler
{
  GameObject InputWordsField;
  bool isTouchable = true;

  void Awake()
  {
    SetTouchable(true);
    //change the chlid TMPro text
    InputWordsField = GameObject.Find("InputWordsField");
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (!isTouchable)
    {
      return;
    }
    Debug.Log("Clicked on SendWords");
    InputWordsField.GetComponent<InputWordsField>().SendWords();
  }
  public void SetTouchable(bool touchable)
  {
    isTouchable = touchable;
  }
  public bool GetTouchable()
  {
    return isTouchable;
  }
}
