using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameWindow : MonoBehaviour
{
  [SerializeField] GameObject gameInputField;
  [SerializeField] GameObject gameSendButton;
  [SerializeField] string gameKeyword;
  GameObject gameNode;
  [SerializeField] GameObject gameController;

  void Start()
  {
    gameObject.SetActive(false);
    gameObject.transform.position = new Vector3(0, 0, 0);
  }
  public void OnEndEdit()
  {
    //ひらがなのみ許可濁点も可
    if (!System.Text.RegularExpressions.Regex.IsMatch(gameInputField.GetComponent<TMP_InputField>().text, @"^[ぁ-ん]+$"))
    {
      gameInputField.GetComponent<TMP_InputField>().text = "";
      gameInputField.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "ひらがなで入力してください。";
      return;
    }
  }
  public void OnClickSendButton()
  {
    if (!System.Text.RegularExpressions.Regex.IsMatch(gameInputField.GetComponent<TMP_InputField>().text, @"^[ぁ-ん]+$"))
    {
      gameInputField.GetComponent<TMP_InputField>().text = "";
      gameInputField.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "ひらがなで入力してください。";

      return;
    }
    if (gameKeyword == gameInputField.GetComponent<TMP_InputField>().text ||
        "せさみ" == gameInputField.GetComponent<TMP_InputField>().text ||
        "めだる" == gameInputField.GetComponent<TMP_InputField>().text ||
        "でぐち" == gameInputField.GetComponent<TMP_InputField>().text ||
        "ないす" == gameInputField.GetComponent<TMP_InputField>().text)
    {
      gameController.GetComponent<GameController>().SetCurrentProperties(gameNode.GetComponent<Node>().id, 1, 1, gameKeyword);
      gameController.GetComponent<GameController>().PlayAnimations();
      gameController.GetComponent<GameController>().ClearMiniGame(gameNode.GetComponent<Node>().id);
      gameController.GetComponent<GameController>().CalcTotalNumOfClearedNodes();
      gameObject.SetActive(false);
    }
    else
    {
      gameInputField.GetComponent<TMP_InputField>().text = "";
      // input message to PlaceHolder
      gameInputField.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "キーワードが違います。";

    }
  }
  public void SetGamenode(int _id)
  {
    gameNode = GameObject.Find($"Node{(_id > 90 ? "Game" : "Norm")} ({_id})");
  }
}
