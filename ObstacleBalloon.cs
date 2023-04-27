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
    gameObject.transform.Find("ObstacleText").GetComponent<TextMeshProUGUI>().text = mainText;
    balloon = gameObject.transform.Find("Balloon").gameObject;
    balloon.transform.Find("deleteObstacleText").GetComponent<TextMeshProUGUI>().text = subText;
  }

  private void Start()
  {
    Debug.Log("Start called");
    balloon.SetActive(false);
  }

  public void DisactivateAnimation()
  {
    // Set the "Balloon" game object active to show the animation
    balloon.SetActive(true);
    // Disactivate the EventSystem to prevent the user from clicking any buttons
    GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().enabled = false;

    // Run "DeleteObstacleBalloons" in Animator
    animator.SetTrigger("DeleteObstacleBalloons");
  }
  public void Deactivate()
  {
    //Activate the EventSystem
    GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().enabled = true;
    gameObject.SetActive(false);
  }
}
