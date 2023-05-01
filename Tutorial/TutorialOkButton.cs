using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOkButton : MonoBehaviour
{
  [SerializeField] GameObject totorialManager;
  //log the last clicked button time
  float lastClickedTime = 0f;
  void Awake()
  {
    lastClickedTime = Time.time;
  }

  public void OnClick()
  {
    if (Time.time - lastClickedTime < 0.5f)
    {
      return;
    }
    // totorialManager.GetComponent<TutorialManager>().NextSection();
    Debug.Log("TutorialOkButton is clicked");
    lastClickedTime = Time.time;
    gameObject.SetActive(false);
  }
}
