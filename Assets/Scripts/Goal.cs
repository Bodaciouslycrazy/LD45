using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
  [SerializeField]
  private string nextSceneName = "sceneName";

  private void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Player")
    {
      other.GetComponent<Player>().FinishLevel(nextSceneName);
    }
  }
}
