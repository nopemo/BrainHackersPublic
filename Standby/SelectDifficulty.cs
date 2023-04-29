using System.Collections.Generic;
using UnityEngine;

public class SelectDifficulty
{
  GameObject property;

  void Start()
  {
    property = GameObject.Find("Property");
  }

  public void IsSelected()
  {
    property.GetComponent<Property>().SetString("Difficulty", "Normal");
  }
}
