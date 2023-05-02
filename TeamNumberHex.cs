using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamNumberHex : MonoBehaviour
{
  // Start is called before the first frame update
  bool isSetTeamNumber = false;
  void Update()
  {
    if (Property.Instance != null)
    {
      if (!isSetTeamNumber)
      {
        SetTeamNumber(Property.Instance.GetNumber("TeamNumber"));
        isSetTeamNumber = true;
      }
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
