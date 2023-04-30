using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class TutorialData
{
  public string SectionName;
  public GameObject UnMaskObject;
  public List<GameObject> TouchableObjects;
  public bool IsUseOkButton;
  public string WindowText;
  public bool IsExeptional;
  public GameObject TriggerObject;
}
public class TutorialManager : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] List<string> tutorialOrder;
  [SerializeField] List<TutorialData> tutorialDataList;
  [SerializeField] GameObject okButton;
  [SerializeField] GameObject changeSceneButton;
  [SerializeField] GameObject textWindow;
  int currentTutorialSectionNumber = 0;
  Dictionary<string, TutorialData> tutorialData;
  void Start()
  {
    currentTutorialSectionNumber = 0;
    StartTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
    tutorialData = new Dictionary<string, TutorialData>();
    foreach (TutorialData tutorialDataItem in tutorialDataList)
    {
      tutorialData.Add(tutorialDataItem.SectionName, tutorialDataItem);
    }
  }

  public void NextSection()
  {
    StopTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
    currentTutorialSectionNumber++;
    if (currentTutorialSectionNumber >= tutorialOrder.Count - 1)
    {
      changeSceneButton.SetActive(true);
    }
    StartTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
  }
  void StartTutorialSection(string sectionName)
  {
    tutorialData[sectionName].UnMaskObject.SetActive(true);
    okButton.SetActive(tutorialData[sectionName].IsUseOkButton);
    List<GameObject> _touchableObjects = tutorialData[sectionName].TouchableObjects;
    foreach (GameObject touchableObject in _touchableObjects)
    {
      touchableObject.GetComponent<TutorialDetectTouch>().SetTouchable(true);
    }
    textWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = tutorialData[sectionName].WindowText;
    tutorialData[sectionName].TriggerObject.GetComponent<TutorialDetectTouch>().SetTriggerObject(true);
  }
  void StopTutorialSection(string sectionName)
  {
    tutorialData[sectionName].UnMaskObject.SetActive(false);
    okButton.SetActive(false);
    changeSceneButton.SetActive(false);
    List<GameObject> _touchableObjects = tutorialData[sectionName].TouchableObjects;
    foreach (GameObject touchableObject in _touchableObjects)
    {
      touchableObject.GetComponent<TutorialDetectTouch>().SetTouchable(false);
    }
    textWindow.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "";
    tutorialData[sectionName].TriggerObject.GetComponent<TutorialDetectTouch>().SetTriggerObject(false);
  }
  public string GetCurrentSectionName()
  {
    return tutorialOrder[currentTutorialSectionNumber];
  }
}
