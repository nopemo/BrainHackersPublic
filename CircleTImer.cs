using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircleTimer : MonoBehaviour
{
  [SerializeField] private float totalTime = 120.0f; // 制限時間を40秒に設定
  private Image timerImage;
  //Load from Resources/CircleTimer1.png
  private Sprite circleTimerSprite;
  private bool isImageChanged = false;
  private GameObject childTMProText;

  void Awake()
  {
    circleTimerSprite = Resources.Load<Sprite>("CircleTimer1");
    childTMProText = gameObject.transform.GetChild(0).gameObject;
  }
  void Start()
  {
    timerImage = GetComponent<Image>();
    childTMProText.GetComponent<TextMeshProUGUI>().text = (int)(totalTime / 60) + "";
  }

  public void UpdateTime(double elapsedSeconds)
  {
    if (!isImageChanged && elapsedSeconds >= totalTime * 19 / 20)
    {
      timerImage.sprite = circleTimerSprite;
      isImageChanged = true;
    }
    if (elapsedSeconds >= totalTime)
    {
      timerImage.fillAmount = 0;
      childTMProText.GetComponent<TextMeshProUGUI>().text = "0";
    }
    else
    {
      if (isImageChanged)
      {
        timerImage.fillAmount = (float)(1 - (elapsedSeconds - totalTime * 19 / 20) / (totalTime * 1 / 20));
      }
      else
      {
        timerImage.fillAmount = (float)(1 - (elapsedSeconds / totalTime));
      }
      //if remained time is less than 60 seconds, show the remained time in seconds
      if (totalTime - elapsedSeconds < 60)
      {
        childTMProText.GetComponent<TextMeshProUGUI>().text = (int)(totalTime - elapsedSeconds) + 1 + "";
      }
      else
      {
        childTMProText.GetComponent<TextMeshProUGUI>().text = (int)(totalTime - elapsedSeconds) / 60 + 1 + "";
      }
    }
  }
}
