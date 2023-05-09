using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public class InputWord : MonoBehaviour, IPointerClickHandler
{
  public string wordText = "アアア";
  public int fontSize = 36;
  GameObject InputWordsField;
  bool isTouchable = true;
  [SerializeField] GameObject InputWordText;

  void Awake()
  {
    SetTouchable(true);
    InitializeText();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (!isTouchable)
    {
      return;
    }
    InputWordsField.GetComponent<InputWordsField>().SetFromWordButton(wordText);
  }
  public void InitializeText()
  {
    //change the chlid TMPro text
    InputWordsField = GameObject.Find("InputWordsField");
    InputWordText.GetComponent<TextMeshProUGUI>().text = wordText;
    InputWordText.GetComponent<TextMeshProUGUI>().fontSize = fontSize;
    if (wordText.Length > 3)
    {
      InputWordText.GetComponent<TextMeshProUGUI>().fontSize = 32;
    }
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
