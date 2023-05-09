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
    if (!System.Text.RegularExpressions.Regex.IsMatch(gameInputField.GetComponent<TMP_InputField>().text, @"^[a-zA-Z]+$"))
    {
      gameInputField.GetComponent<TMP_InputField>().text = "";
      return;
    }
  }
  public void OnClickSendButton()
  {
    if (!System.Text.RegularExpressions.Regex.IsMatch(gameInputField.GetComponent<TMP_InputField>().text, @"^[a-zA-Z]+$"))
    {
      gameInputField.GetComponent<TMP_InputField>().text = "";
      return;
    }
    if (gameKeyword == gameInputField.GetComponent<TMP_InputField>().text)
    {
      gameController.GetComponent<GameController>().SetCurrentProperties(gameNode.GetComponent<Node>().id, 1, 1, gameKeyword);
      gameController.GetComponent<GameController>().PlayAnimations();
      gameController.GetComponent<GameController>().ClearMiniGame(gameNode.GetComponent<Node>().id);
      gameObject.SetActive(false);
    }
    else
    {
      gameInputField.GetComponent<TMP_InputField>().text = "キーワードが違います。";
    }
  }
  public void SetGamenode(int _id)
  {
    gameNode = GameObject.Find($"Node{(_id > 90 ? "Game" : "Norm")} ({_id})");
  }
}
