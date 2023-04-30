using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDetectTouch : MonoBehaviour
{
  [SerializeField] GameObject tutorialManager;
  List<string> tutorialSections;
  bool isTouchable;
  bool isTriggerObject;
  void Start()
  {
    SetTouchable(false);
    tutorialSections = new List<string>();
  }
  void OnClick()
  {
    if (isTriggerObject)
    {
      tutorialManager.GetComponent<TutorialManager>().NextSection();
    }
  }
  public void SetTouchable(bool _isTouchable)
  {
    isTouchable = _isTouchable;
    //if the object has a collider, set it to be enabled or disabled, else if it has button, set it to be enabled or disabled
    if (gameObject.GetComponent<Collider>() != null)
    {
      gameObject.GetComponent<Collider>().enabled = isTouchable;
    }
    else if (gameObject.GetComponent<UnityEngine.UI.Button>() != null)
    {
      gameObject.GetComponent<UnityEngine.UI.Button>().enabled = isTouchable;
    }
  }
  public void SetTriggerObject(bool _isTriggerObject)
  {
    isTriggerObject = _isTriggerObject;
  }
}
