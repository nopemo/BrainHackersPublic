using UnityEngine;
using System.Collections;

class DisactivateTemporaryWindow : MonoBehaviour
{
  [SerializeField] GameObject gameWindow;
  void Start()
  {
    gameWindow.transform.position = new Vector3(0, 0, 0);
    gameWindow.SetActive(false);
  }
  public void OnClick()
  {
    gameWindow.SetActive(false);
  }
}
