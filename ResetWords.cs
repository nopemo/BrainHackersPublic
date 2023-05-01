using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class ResetWords : MonoBehaviour, IPointerClickHandler
{
  GameObject InputWordsField;
  bool isTouchable = true;
  void Awake()
  {
    //change the chlid TMPro text
    SetTouchable(true);
    InputWordsField = GameObject.Find("InputWordsField");
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (!isTouchable)
    {
      return;
    }
    Debug.Log("Clicked on ResetWords");
    InputWordsField.GetComponent<InputWordsField>().ResetWords();
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
