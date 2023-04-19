using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public class InputWordsField : MonoBehaviour
{
  string InputWordsFieldText = "";
  private int limitLength = 9;

  GameObject GameController;

  void Awake()
  {
    //change the chlid TMPro text
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = InputWordsFieldText;
    GameController = GameObject.Find("GameController");
  }

  public void SetFromWordButton(string word)
  {
    if (InputWordsFieldText.Length + word.Length <= limitLength)
    {
      InputWordsFieldText += word;
    }
    else
    {
      //until the limit length
      InputWordsFieldText += word.Substring(0, limitLength - InputWordsFieldText.Length);
    }
    //change the chlid TMPro text
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = InputWordsFieldText;
  }

  public void ResetWords()
  {
    InputWordsFieldText = "";
    //change the chlid TMPro text
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = InputWordsFieldText;
  }
  public void SendWords()
  {
    GameController.GetComponent<GameController>().CheckAnswer(InputWordsFieldText);
    ResetWords();
  }
}
