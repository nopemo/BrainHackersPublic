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
  void Awake()
  {
    InitializeText();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    Debug.Log("Clicked on InputWord");
    InputWordsField.GetComponent<InputWordsField>().SetFromWordButton(wordText);
  }
  public void InitializeText()
  {
    //change the chlid TMPro text
    InputWordsField = GameObject.Find("InputWordsField");
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = wordText;
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = fontSize;
    if (wordText.Length > 3)
    {
      gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 32;
    }
  }
}
