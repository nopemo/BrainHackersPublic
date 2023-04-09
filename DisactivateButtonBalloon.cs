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
    GameController.GetComponent<GameController>().SetCurrentProperties(-1, 0, "It has not been set yet.");
    balloon.transform.Find("QuestionImage").GetComponent<SpriteRenderer>().sprite = null;
    balloon.SetActive(false);
  }
}
