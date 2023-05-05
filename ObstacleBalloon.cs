using UnityEngine;
using System.Collections;
using TMPro;

public class ObstacleBalloon : MonoBehaviour
{
  [SerializeField] private string mainText;
  [SerializeField] private string subText;
  private GameObject balloon;
  private Animator animator;

  private void Awake()
  {
    animator = GetComponent<Animator>();
    balloon = gameObject.transform.Find("Balloon").gameObject;
    InitializeTexts();
  }

  private void Start()
  {
    Debug.Log("Start called");
    balloon.SetActive(false);
  }

  public void DisactivateAnimation()
  {
    balloon.SetActive(true);
    animator.SetTrigger("DeleteObstacleBalloons");
  }
  public void Deactivate()
  {
    gameObject.SetActive(false);
  }
  public void DisactivateAnimationAfterAllMiniGamesCleared()
  {
    balloon.SetActive(true);
    balloon.transform.Find("deleteObstacleText").GetComponent<TextMeshProUGUI>().text = "これは関係なかったね。";
    animator.SetTrigger("DeleteObstacleBalloons");
  }
  public void InitializeTexts()
  {
    gameObject.transform.Find("ObstacleText").GetComponent<TextMeshProUGUI>().text = mainText;
    balloon = gameObject.transform.Find("Balloon").gameObject; balloon.transform.Find("deleteObstacleText").GetComponent<TextMeshProUGUI>().text = subText;
  }
}
