using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayProperties : MonoBehaviour
{
  [SerializeField] List<GameObject> textProperties;
  [SerializeField] GameObject property;

  // Update is called once per frame
  void Update()
  {
    foreach (GameObject textProperty in textProperties)
    {
      textProperty.GetComponent<TextMeshProUGUI>().text = property.GetComponent<Property>().GetNumber(textProperty.name).ToString();
    }
  }
}
