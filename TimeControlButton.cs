using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeControlButton : MonoBehaviour
{
  [SerializeField] int rate;
  [SerializeField] GameObject GameController;
  public void OnClick()
  {
    GameController.GetComponent<GameController>().AddRemainingTime(rate);
  }
}
