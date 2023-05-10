using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialDetectTouch : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] GameObject tutorialManager;
  bool isTouchable;
  bool isTriggerObject;
  public void OnPointerClick(PointerEventData eventData)
  {
    if (isTriggerObject)
    {
      tutorialManager.GetComponent<TutorialManager>().NextSection();
    }
  }
  public void SetTouchable(bool _isTouchable)
  {
    isTouchable = _isTouchable;
    //check if the object has a collider or a button component
    if (isTouchable)
    {
      //change the layer to "Touchable"
      gameObject.layer = 6;
    }
    else
    {
      //change the layer to "Untouchable"
      gameObject.layer = 7;
    }
    if (gameObject.GetComponent<Node>() != null)
    {
      gameObject.GetComponent<Node>().SetTouchable(isTouchable);
    }
    if (gameObject.GetComponent<InputWord>() != null)
    {
      gameObject.GetComponent<InputWord>().SetTouchable(isTouchable);
    }
    if (gameObject.GetComponent<ResetWords>() != null)
    {
      gameObject.GetComponent<ResetWords>().SetTouchable(isTouchable);
    }
    if (gameObject.GetComponent<SendWords>() != null)
    {
      gameObject.GetComponent<SendWords>().SetTouchable(isTouchable);
    }
    if (gameObject.GetComponent<DisactivateButtonBalloon>() != null)
    {
      gameObject.GetComponent<DisactivateButtonBalloon>().SetTouchable(isTouchable);
    }
    if (gameObject.GetComponent<CircleCollider2D>() != null)
    {
      gameObject.GetComponent<CircleCollider2D>().enabled = isTouchable;
    }
    if (gameObject.GetComponent<BoxCollider2D>() != null)
    {
      gameObject.GetComponent<BoxCollider2D>().enabled = isTouchable;
    }
    if (gameObject.GetComponent<UnityEngine.UI.Button>() != null)
    {
      gameObject.GetComponent<UnityEngine.UI.Button>().enabled = isTouchable;
    }
    if (gameObject.GetComponent<Image>() != null)
    {
      gameObject.GetComponent<Image>().raycastTarget = isTouchable;
    }
  }
  public void SetTriggerObject(bool _isTriggerObject)
  {
    isTriggerObject = _isTriggerObject;
  }
}
