using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeControlButton : MonoBehaviour
{
  [SerializeField] int rate;
  [SerializeField] GameObject GameController;
  [SerializeField] GameObject InputTimeFieldText;
  public void OnClick()
  {
    string ValOfInputTimeField = InputTimeFieldText.GetComponent<TextMeshProUGUI>().text;
    Debug.Log("ValOfInputTimeField: " + ValOfInputTimeField);
    int ValOfInputTimeFieldInt;
    if (int.TryParse(ValOfInputTimeField, out ValOfInputTimeFieldInt))
    {
      GameController.GetComponent<GameController>().AddRemainingTime(rate * ValOfInputTimeFieldInt);
    }
    else
    {
      GameController.GetComponent<GameController>().AddRemainingTime(rate * 10);
    }
  }
}
