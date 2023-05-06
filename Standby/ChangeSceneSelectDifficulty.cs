using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSelectDifficulty : MonoBehaviour
{
  //input the name of the scene you want to load in the inspector
  private string sceneName;
  public void ButtonIsClicked()
  {
    sceneName = Property.Instance.GetString("Difficulty") + "MainGame";
    SceneManager.LoadScene(sceneName);
    Debug.Log("Scene " + sceneName + " is loaded");
  }
}
