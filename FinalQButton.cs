using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
public class FinalQButton : MonoBehaviour
{
  // If this button is clicked, run the ActivateQuestionBalloon() function.
  public void OnClick()
  {
    ActivateQuestionBalloon();
  }

  GameObject QuestionBalloon;
  GameObject GameController;
  public int state = 1;
  public string questionImageName = "FinalQ";
  public string questionText = "これさえ答えられれば...！";
  public string correctAnswer = "ホーキング";
  void Awake()
  {
    QuestionBalloon = GameObject.Find("Question");
    GameController = GameObject.Find("GameController");
  }
  void ActivateQuestionBalloon()
  {
    // Activate the balloon
    QuestionBalloon.SetActive(true);

    // Change the image of the balloon child "QuestionImage" to /Assets/Resources/Questions/Qxx.png
    if (state == 2)
    {
      // Add the clear text below the image.
      Debug.Log("Question of Node " + state + " is activated, but it is already cleared.");
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = "ゲームをすでにクリアしています。";
    }
    else
    {
      // Add the question text below the image.
      Debug.Log("Question of Node " + state + " is activated, and it is not cleared yet.");
      QuestionBalloon.transform.Find("QuestionText").gameObject.GetComponent<TextMeshProUGUI>().text = questionText;
    }
    // Get the "QuestionImage" child object of the balloon
    GameObject questionImageObj = QuestionBalloon.transform.Find("QuestionImage").gameObject;
    // Load the image from the Resources/Questions folder based on the id
    Sprite questionSprite = Resources.Load<Sprite>("Questions/" + questionImageName);
    Debug.Log("Question of Node " + state + " is activated, and load image from " + "Questions/" + questionImageName);
    // Set the loaded image to the "QuestionImage" object using SpriteRenderer
    questionImageObj.GetComponent<SpriteRenderer>().sprite = questionSprite;
    GameController.GetComponent<GameController>().SetCurrentProperties(51, 1, 0, correctAnswer);
    GameController.GetComponent<GameController>().currentIsSentCorrectAnswer = false;
  }
}
