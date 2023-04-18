using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public class ObstacleBalloon : MonoBehaviour
{
  [SerializeField] string mainText;
  [SerializeField] string subText;
  void Awake()
  {
    gameObject.transform.Find("ObstacleText").GetComponent<TextMeshProUGUI>().text = mainText;
    gameObject.transform.Find("ObstacleDeleteText").GetComponent<TextMeshProUGUI>().text = subText;
  }

  void DisactivateAnimation()
  {
    //disactivate the balloon
    Debug.Log("Disactivate Obstacle Balloon");
  }
}
