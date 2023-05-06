using System.Collections.Generic;
using UnityEngine;
public class SelectDifficultyButton : MonoBehaviour
{
  [SerializeField] string difficulty;
  public void IsSelected()
  {
    Property.Instance.SetString("Difficulty", difficulty);
  }
  void Update()
  {
    if (Property.Instance.GetString("Difficulty") == difficulty)
    {
      GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }
    else
    {
      GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
    }
  }
}
