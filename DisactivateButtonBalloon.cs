using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class DisactivateButtonBalloon : MonoBehaviour, IPointerClickHandler
{
  public GameObject balloon;
  GameObject GameController;
  void Awake()
  {
    //find the parent balloon
    balloon = gameObject.transform.parent.gameObject;
    GameController = GameObject.Find("GameController");
  }
  void Start()
  {
    //move the balloon to (-360,-100,0)
    balloon.transform.position = new Vector3(-360, -100, 0);
    //disactivate the balloon
    DisactivateBalloon();
  }
  public void OnPointerClick(PointerEventData eventData)
  {
    Debug.Log("Clicked on DisactivateButtonBalloon");
    DisactivateBalloon();
  }

  public void DisactivateBalloon()
  {
    if (GameController.GetComponent<GameController>().currentIsSentCorrectAnswer)
    {
      GameController.GetComponent<GameController>().PlayAnimations();
    }
    GameController.GetComponent<GameController>().SetCurrentProperties(-1, -1, -1, "It has not been set yet.");
    balloon.transform.Find("QuestionImage").GetComponent<SpriteRenderer>().sprite = null;

    balloon.SetActive(false);
  }
}