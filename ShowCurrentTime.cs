using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public class ShowCurrentTime : MonoBehaviour
{
  [SerializeField] GameObject GameController;
  void Update()
  {
    float currentTime = GameController.GetComponent<GameController>().GetElipsedTime();
    //min:sec
    gameObject.GetComponent<TextMeshProUGUI>().text = "Remaining Time : " + Mathf.Floor(currentTime / 60) + ":" + Mathf.Floor(currentTime % 60);
  }
}
