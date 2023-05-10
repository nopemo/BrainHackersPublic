using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hold : MonoBehaviour, IPointerClickHandler
{
  [SerializeField] GameObject DebugController;
  [SerializeField] string objectName;
  public void OnPointerClick(PointerEventData eventData)
  {
    if (objectName == "Memory")
    {
      DebugController.GetComponent<DebugController>().SetHold("Memory", true);
    }
    else if (objectName == "Reset")
    {
      DebugController.GetComponent<DebugController>().SetHold("Reset", true);
    }
  }
  public void OnPointerUp(PointerEventData eventData)
  {
    if (objectName == "Memory")
    {
      DebugController.GetComponent<DebugController>().SetHold("Memory", false);
    }
    else if (objectName == "Reset")
    {
      DebugController.GetComponent<DebugController>().SetHold("Reset", false);
    }
  }
}
