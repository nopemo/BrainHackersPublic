using System.Collections.Generic;
using UnityEngine;
public class SelectHard : MonoBehaviour
{
  GameObject property;

  void Start()
  {
    property = GameObject.Find("Property");
  }
  public void IsSelected()
  {
    GameObject property = GameObject.Find("Property");
    property.GetComponent<Property>().SetString("Difficulty", "Hard");
  }
  void Update()
  {
    if (property.GetComponent<Property>().GetString("Difficulty") == "Hard")
    {
      GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f);
    }
    else
    {
      GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 1f);
    }
  }
}
