using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOkButton : MonoBehaviour
{
  [SerializeField] GameObject totorialManager;

  public void OnClick()
  {
    totorialManager.GetComponent<TutorialManager>().NextSection();
    gameObject.SetActive(false);
  }
}
