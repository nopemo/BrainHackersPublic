using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamNumberHex : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    GameObject property = GameObject.Find("Property");
    if (property != null)
    {
      Debug.Log("Property found and TeamNumber is " + property.GetComponent<Property>().GetNumber("TeamNumber"));
      SetTeamNumber(property.GetComponent<Property>().GetNumber("TeamNumber"));
    }
    else
    {
      Debug.Log("Property not found");
    }
  }
  public void SetTeamNumber(int teamNumber)
  {
    //convert teamNumber to hex
    string _childtext = teamNumber.ToString("D2");
    //set the text to hex
    //find the child with the name "TeamNumber"
    GameObject child = transform.Find("TeamNumber").gameObject;
    //set the text
    child.GetComponent<TextMeshProUGUI>().text = _childtext;
  }

}
