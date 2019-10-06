using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{

  private void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Player")
    {
      LevelController.CurrentLevel.FinishLevel();
    }
  }
}
