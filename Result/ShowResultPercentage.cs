using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowResultPercentage : MonoBehaviour
{
  [SerializeField] GameObject percentageText;
  [SerializeField] GameObject SuccessImage;
  [SerializeField] GameObject FailureImage;
  void Start()
  {
    float percentage = 0;
    for (int i = 1; i <= 50; i++)
    {
      if (Property.Instance.GetFlag("Norm" + i.ToString()))
      {
        percentage += 0.5f;
      }
    }
    for (int i = 96; i < 100; i++)
    {
      if (Property.Instance.GetFlag("Game" + i.ToString()))
      {
        percentage += 15;
      }
    }
    if (Property.Instance.GetFlag("isGameCleared"))
    {
      SuccessImage.SetActive(true);
      FailureImage.SetActive(false);
      percentageText.GetComponent<TextMeshProUGUI>().color = new Color(0f, 1f, 1f);
      percentage += 20;
    }
    else
    {
      SuccessImage.SetActive(false);
      FailureImage.SetActive(true);
      //change color to FFB800
      percentageText.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.72f, 0f);
    }
    percentageText.GetComponent<TextMeshProUGUI>().text = percentage.ToString("F1");
  }
}
