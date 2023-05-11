using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamNumInputField : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] GameObject inputField;
  [SerializeField] GameObject confirmButton;
  [SerializeField] string savePropertyName = "TeamNumber";

  public void ButtonIsClicked()
  {
    //read the input field
    //set the team number to property.GetComponent<Property>().SetTeamNumber()
    //if button is clicked, this function is called

    //get the input field
    string input = inputField.GetComponent<TMPro.TMP_InputField>().text;
    //set the team number
    Property.Instance.SetNumber(savePropertyName, int.Parse(input));
  }

  // Update is called once per frame

}
