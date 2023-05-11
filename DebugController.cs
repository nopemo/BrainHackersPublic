using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DebugController : MonoBehaviour
{
  bool isDebugMode = false;
  bool isMemoryClick = false;
  bool isResetClick = false;
  [SerializeField] float holdSeconds; // 長押しにかかる時間。変更してください。
  [SerializeField] GameObject DebugCanvas;
  float time;
  void Start()
  {
    time = 0;
    isDebugMode = false;
    isMemoryClick = false;
    isResetClick = false;
    Property.Instance.SetFlag("IsDebugMode", false);
    DebugCanvas.SetActive(false);
  }
  public void SetHold(string objectName, bool isPressing)
  {
    if (objectName == "Memory")
    {
      isMemoryClick = isPressing;
    }
    else if (objectName == "Reset")
    {
      isResetClick = isPressing;
    }
  }
  void Update()
  {
    if (!isDebugMode)
    {
      DebugCanvas.SetActive(false);
      if (isMemoryClick && isResetClick)
      {
        time += Time.deltaTime;
        if (time > holdSeconds)
        {
          isDebugMode = true;
          Property.Instance.SetFlag("IsDebugMode", true);
          time = 0;
        }
      }
      else
      {
        time = 0;
      }
    }
    else
    {
      DebugCanvas.SetActive(true);
      if (isMemoryClick && isResetClick)
      {
        time += Time.deltaTime;
        if (time > holdSeconds)
        {
          isDebugMode = false;
          Property.Instance.SetFlag("IsDebugMode", false);
          time = 0;
        }
      }
      else
      {
        time = 0;
      }
    }
  }
  public void ChangeIsDebugMode()
  {
    isDebugMode = !isDebugMode;
    Property.Instance.SetFlag("IsDebugMode", isDebugMode);
  }
}
