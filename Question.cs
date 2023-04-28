using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
  [SerializeField] GameObject disActivateButtonBalloon;
  public void EvokeChildDisactivateButton()
  {
    disActivateButtonBalloon.GetComponent<DisactivateButtonBalloon>().DisactivateBalloon();
  }
}
