using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarTimer : MonoBehaviour
{
  private float totalTime = 2400; // 制限時間を40秒に設定
  private float imageSize = 0.0f;
  //Load from Resources/BarTimer1.png
  private Sprite barTimerMinutesMax;
  private Sprite barTimerMinutesMin;
  private Sprite barTimerSecondsMax;
  private Sprite barTimerSecondsMin;
  private Sprite barTimerMinutesText;
  private Sprite barTimerSecondsText;
  [SerializeField] private GameObject childTMProText;
  [SerializeField] private GameObject childTimerMax;
  [SerializeField] private GameObject childTimerMin;
  [SerializeField] private GameObject childTextBGimage;
  [SerializeField] private GameObject childNokori;
  [SerializeField] private float leftMargin = 0;
  [SerializeField] private float indicatedScale = 0;
  [SerializeField] private float distanceOfScales = 0;
  // [SerializeField] private float debugElapsed = 0;
  // [SerializeField] private float debugTotalTime = 0;

  void Awake()
  {
    barTimerMinutesMax = Resources.Load<Sprite>("BarTimer/0508_timer_minutes_max");
    barTimerMinutesMin = Resources.Load<Sprite>("BarTimer/0508_timer_minutes_zero");
    barTimerSecondsMax = Resources.Load<Sprite>("BarTimer/0508_timer_seconds_max");
    barTimerSecondsMin = Resources.Load<Sprite>("BarTimer/0508_timer_seconds_zero");
    barTimerMinutesText = Resources.Load<Sprite>("BarTimer/timer_minutes_text");
    barTimerSecondsText = Resources.Load<Sprite>("BarTimer/timer_seconds_test");
  }
  void Start()
  {
    imageSize = childTimerMax.GetComponent<Image>().sprite.rect.width;
    totalTime = Property.Instance.GetNumber("TotalTime");
    UpdateTime(0);
  }


  public void UpdateTime(double elapsedSeconds)
  {
    if (elapsedSeconds >= totalTime - 60)
    {
      //change color to #D4AE08
      childTMProText.GetComponent<TextMeshProUGUI>().color = new Color(0.831f, 0.682f, 0.031f);
      childTimerMax.GetComponent<Image>().sprite = barTimerSecondsMax;
      childTimerMin.GetComponent<Image>().sprite = barTimerSecondsMin;
      //change color to #150E00
      childTextBGimage.GetComponent<Image>().color = new Color(0.082f, 0.055f, 0f, 0.7f);
      childNokori.GetComponent<Image>().sprite = barTimerSecondsText;
      float _imageRate = (leftMargin + (indicatedScale + distanceOfScales) * Mathf.Ceil((float)(totalTime - elapsedSeconds))) / imageSize;
      childTimerMax.GetComponent<Image>().fillAmount = _imageRate;
    }
    else
    {
      childTMProText.GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f);
      childTimerMax.GetComponent<Image>().sprite = barTimerMinutesMax;
      childTimerMin.GetComponent<Image>().sprite = barTimerMinutesMin;
      //change color to #041E2C
      childTextBGimage.GetComponent<Image>().color = new Color(0.015f, 0.117f, 0.172f, 0.7f);
      childNokori.GetComponent<Image>().sprite = barTimerMinutesText;
      float _imageRate = (leftMargin + (indicatedScale + distanceOfScales) * Mathf.Ceil((float)(totalTime - elapsedSeconds) / 40)) / imageSize; ;
      childTimerMax.GetComponent<Image>().fillAmount = _imageRate;
    }
    if (totalTime - elapsedSeconds > 60 * 100)
    {
      childTMProText.GetComponent<TextMeshProUGUI>().text = "--";
    }
    else if (elapsedSeconds >= totalTime)
    {
      childTimerMax.GetComponent<Image>().fillAmount = 0;
      childTMProText.GetComponent<TextMeshProUGUI>().text = "0";
    }
    else
    {
      if (elapsedSeconds >= totalTime - 60)

      {
        childTMProText.GetComponent<TextMeshProUGUI>().text = ((int)(totalTime - elapsedSeconds + 1)).ToString();
        Debug.Log("elapsedSeconds: " + elapsedSeconds.ToString() + " _imageRate: " + _imageRate.ToString());
      }
      else
      {
        childTMProText.GetComponent<TextMeshProUGUI>().text = ((int)((totalTime - elapsedSeconds) / 60 + 1)).ToString();
        Debug.Log("elapsedSeconds: " + elapsedSeconds.ToString() + " _imageRate: " + _imageRate.ToString());
      }
    }
  }
}
