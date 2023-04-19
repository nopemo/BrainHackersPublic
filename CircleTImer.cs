using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
  [SerializeField] private float totalTime = 40.0f; // 制限時間を40秒に設定
  private Image timerImage;

  void Start()
  {
    timerImage = GetComponent<Image>();
  }

  public void UpdateTime(double elapsedSeconds)
  {
    Debug.Log(elapsedSeconds + " / " + totalTime);
    if (elapsedSeconds >= totalTime)
    {
      timerImage.fillAmount = 0;
    }
    else
    {
      timerImage.fillAmount = (float)(1 - (elapsedSeconds / totalTime));
    }
  }
}
