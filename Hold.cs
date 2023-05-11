using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
  [SerializeField] GameObject DebugController;
  [SerializeField] string objectName;
  public void OnPointerDown(PointerEventData eventData)
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
  public void OnPointerExit(PointerEventData eventData)
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
