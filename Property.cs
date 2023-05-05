using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
  public static Property Instance { get; private set; }

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private Dictionary<string, bool> flags = new Dictionary<string, bool>();
  private Dictionary<string, int> numbers = new Dictionary<string, int>();
  private Dictionary<string, string> strings = new Dictionary<string, string>();

  public void SetFlag(string flagName, bool value)
  {
    flags[flagName] = value;
    Debug.Log("Flag set: " + flagName + " in " + gameObject.name + "and the value is " + flags[flagName]);
  }

  public bool GetFlag(string flagName)
  {
    if (flags.ContainsKey(flagName))
    {
      Debug.Log("Flag found: " + flagName + " in " + gameObject.name + "and the value is " + flags[flagName]);
      return flags[flagName];
    }
    Debug.Log("Flag not found: " + flagName + " in " + gameObject.name);
    return false;
  }
  public void SetNumber(string numberName, int value)
  {
    numbers[numberName] = value;
  }
  public int GetNumber(string numberName)
  {
    if (numbers.ContainsKey(numberName))
    {
      return numbers[numberName];
    }
    return -1;
  }
  public void SetString(string stringName, string value)
  {
    strings[stringName] = value;
  }
  public string GetString(string stringName)
  {
    if (strings.ContainsKey(stringName))
    {
      return strings[stringName];
    }
    return "Not Found";
  }
}
