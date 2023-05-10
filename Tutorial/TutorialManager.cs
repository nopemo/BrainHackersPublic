using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class TutorialData
{
  public string SectionName;
  public List<GameObject> UnMaskObjects;
  public List<GameObject> TouchableObjects;
  public List<GameObject> AdditionalObjects;
  public bool IsUseOkButton;
  [TextArea] public string WindowText;
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
  [SerializeField] GameObject node1;
  [SerializeField] GameObject node2;
  [SerializeField] GameObject finalQ;
  [SerializeField] List<GameObject> touchableObjects;
  int currentTutorialSectionNumber = 0;
  Dictionary<string, TutorialData> tutorialData;
  void Start()
  {
    currentTutorialSectionNumber = 0;
    tutorialData = new Dictionary<string, TutorialData>();
    foreach (TutorialData tutorialDataItem in tutorialDataList)
    {
      tutorialData.Add(tutorialDataItem.SectionName, tutorialDataItem);
    }
    foreach (GameObject _touchableObject in touchableObjects)
    {
      _touchableObject.GetComponent<TouchableObject>().SetTouchable(false);
    }
    StartTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
    changeSceneButton.SetActive(false);
  }

  void Update()
  {
    if (tutorialData[tutorialOrder[currentTutorialSectionNumber]].IsExeptional)
    {
      if (tutorialOrder[currentTutorialSectionNumber] == "Node1")
      {
        if (node1.GetComponent<Node>().GetState() == 2)
        {
          NextSection();
        }
      }
      if (tutorialOrder[currentTutorialSectionNumber] == "Node2")
      {
        if (node2.GetComponent<Node>().GetState() == 2)
        {
          NextSection();
        }
      }
      else if (tutorialOrder[currentTutorialSectionNumber] == "FinalQ")
      {
        if (finalQ.GetComponent<FinalQButton>().GetState() == 2)
        {
          NextSection();
        }
      }
    }
  }

  public void NextSection()
  {
    StopTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
    if (currentTutorialSectionNumber < tutorialOrder.Count - 1)
    {
      currentTutorialSectionNumber++;
    }
    if (currentTutorialSectionNumber >= tutorialOrder.Count - 1)
    {
      changeSceneButton.SetActive(true);
    }
    StartTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
  }
  public void ForcelyMoveToFinalSection()
  {
    StopTutorialSection(tutorialOrder[currentTutorialSectionNumber]);
    currentTutorialSectionNumber = tutorialOrder.Count - 1;
    changeSceneButton.SetActive(true);
    StartTutorialSection("ForcelyMoveToFinalSection");
  }
  void StartTutorialSection(string sectionName)
  {
    // tutorialData[sectionName].UnMaskObject.SetActive(true);
    foreach (GameObject additionalObject in tutorialData[sectionName].AdditionalObjects)
    {
      additionalObject.SetActive(true);
    }
    foreach (GameObject unMaskObject in tutorialData[sectionName].UnMaskObjects)
    {
      unMaskObject.SetActive(true);
      if (unMaskObject.GetComponent<Animator>() != null)
      {
        unMaskObject.GetComponent<Animator>().SetTrigger("Start");
      }
    }
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
    foreach (GameObject additionalObject in tutorialData[sectionName].AdditionalObjects)
    {
      additionalObject.SetActive(false);
    }
    // tutorialData[sectionName].UnMaskObject.SetActive(false);
    foreach (GameObject unMaskObject in tutorialData[sectionName].UnMaskObjects)
    {
      unMaskObject.SetActive(false);
    }
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
