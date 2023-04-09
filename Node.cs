using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;

public class Node : MonoBehaviour, IPointerClickHandler
{
  public int isGameNode = 0;
  public int state = 0;
  public int id = 0;
  /* 0 = not active
   * 1 = active but not clear
   * 2 = clear
   */
  public GameObject Balloon;// = Resources.Load("Fukidashi") as GameObject;
  //make a list of nodes that are near less than 300px
  public List<GameObject> nearNodes = new List<GameObject>();
  GameObject Edge;
  void Awake()
  {
    id = ExtractNumberFromObjectName(gameObject);
    if (gameObject.name.Contains("Game"))
    {
      isGameNode = 1;
    }
    else if (gameObject.name.Contains("Norm") && id <= 3)
    {
      state = 1;
    }
  }

  void Start()
  {
    int[] limitDistance = GameObject.Find("GameController").GetComponent<GameController>().limitDistance;
    // nodeSprites = Resources.LoadAll<Sprite>("node");
    ChangeState(state);
    Edge = Resources.Load("Edge") as GameObject;
    List<GameObject> nearNodes = makeNearNodes(limitDistance);

    //print my uid to the child text in the hierarchy
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = id.ToString();
    //change the sorting layer and order in layer of the text
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingLayerName = "Node";
    gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().canvas.sortingOrder = 1;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    Debug.Log("Clicked on node");
    if (state >= 1)
    {
      GenerateBaloon(state, id);
    }
  }

  public void ChangeState(int state)
  {
    this.state = state;
    // GetComponent<SpriteRenderer>().sprite = nodeSprites[state];
    if (state == 0)
    {
      //change the color to FF0000
      GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.6f, 0.6f);
    }
    else if (state == 1)
    {
      //change the color to FFFF00
      GetComponent<SpriteRenderer>().color = new Color(0.721f, 0.984f, 0.235f);
    }
    else if (state == 2)
    {
      //change the color to 00FF00
      GetComponent<SpriteRenderer>().color = new Color(1f, 0.011f, 0.901f);
    }
  }
  void LoadImageByName(string name, GameObject obj)
  {
    // Sprite sprite = Resources.Load<Sprite>(name);
    // obj.GetComponent<SpriteRenderer>().sprite = sprite;
  }

  void GenerateBaloon(int state, int id)
  {
    GameObject balloon = Instantiate(Balloon, transform.position, Quaternion.identity);
    // Change the image depending on the id.
    // LoadImageByName("QImage" + id, balloon);
    if (state == 2)
    {
      // Add the clear text below the image.
      Debug.Log("Add the clear text below the image.");
    }

  }
  List<GameObject> makeNearNodes(int[] limitDistance)
  {
    List<GameObject> nodeNorms = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeNorm"));
    List<GameObject> nearNodes = new List<GameObject>();
    for (int i = 0; i < nodeNorms.Count; i++)
    {
      if (Vector3.Distance(transform.position, nodeNorms[i].transform.position) < limitDistance[isGameNode])
      {
        nearNodes.Add(nodeNorms[i]);
      }
    }
    List<GameObject> nodeGames = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodeGame"));
    for (int i = 0; i < nodeGames.Count; i++)
    {
      if (Vector3.Distance(transform.position, nodeGames[i].transform.position) < limitDistance[1])
      {
        nearNodes.Add(nodeGames[i]);
      }
    }
    return nearNodes;
  }
  int ExtractNumberFromObjectName(GameObject obj)
  {
    string objectName = obj.name;
    // Use regular expressions to match only the digits in the object name
    Match match = Regex.Match(objectName, @"\d+");
    if (match.Success)
    {
      // If a number is found, convert it to an integer and return it
      return int.Parse(match.Value);
    }
    else
    {
      Debug.LogWarning("No number found in the object name");
      return -1;
    }
  }
}
