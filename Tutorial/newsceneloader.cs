using UnityEngine;
using UnityEngine.SceneManagement;

public class newsceneloader : MonoBehaviour
{
  //input the name of the scene you want to load in the inspector
  [SerializeField] string sceneName;
  public void ButtonIsClicked()
  {
    SceneManager.LoadScene(sceneName);
    Debug.Log("Scene " + sceneName + " is loaded");
  }
}
