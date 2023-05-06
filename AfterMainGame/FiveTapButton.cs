using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using UnityEngine.SceneManagement;

public class FiveTapButton : MonoBehaviour
{
  public int tapCount = 0;
  [SerializeField] float tapInterval = 0.5f;
  [SerializeField] int tapCountLimit = 5;
  [SerializeField] UnityEvent TapAction;
  private float lastTapTime;

  public void OnClick()
  {
    if (Time.time - lastTapTime < tapInterval)
    {
      tapCount++;
    }
    else
    {
      tapCount = 1;
    }

    lastTapTime = Time.time;

    if (tapCount == tapCountLimit)
    {
      ExecuteFiveTapAction();
    }
  }

  private void ExecuteFiveTapAction()
  {
    TapAction.Invoke();
    tapCount = 0;

  }
}
