using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections;
using TMPro;

public class BackToInitStateExceptTeamNumber : MonoBehaviour
{
  public void Onclick()
  {
    int _teamNumber = Property.Instance.GetNumber("TeamNumber");
    int _boothNumber = Property.Instance.GetNumber("BoothNumber");
    Property.Instance.ClearAll();
    Property.Instance.SetNumber("TeamNumber", _teamNumber);
    Property.Instance.SetNumber("BoothNumber", _boothNumber);
    SceneManager.LoadScene("SelectDifficulty");
  }
}
