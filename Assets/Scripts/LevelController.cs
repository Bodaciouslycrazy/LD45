using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelController : MonoBehaviour
{
  public static LevelController CurrentLevel;

  const string scenePrefix = "Level";

  [SerializeField]
  private int LevelNumber = 1;
  [SerializeField]
  private TextMeshProUGUI text;
  [SerializeField]
  private float time = 60f;

  private bool counting = false;

  private void Awake()
  {
    CurrentLevel = this;
  }
  private void Start()
  {
    StartLevel();
  }

  private void Update()
  {
    if (counting)
    {
      time -= Time.deltaTime;

      if(time <= 0)
      {
        time = 0f;
      }
    }
    text.text = string.Format("{0:00.00}", time);
  }

  //Starts the timer and player movement.
  public void StartLevel()
  {
    WorldRotator.Instance.activated = true;
    Camera.main.GetComponent<CameraController>().activated = true;
    counting = true;
  }

  //Finishes the level, submits the time, and loads next level
  public void FinishLevel()
  {
    counting = false;
    ScoreTracker.Instance.updateLevelData(LevelNumber, time);
    Player.Instance.FinishLevel();
    string nextScene = scenePrefix + (LevelNumber+1).ToString();
    StartCoroutine(LoadSceneDelay(nextScene, 2f));
  }

  //Restarts the level
  public void RestartLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void FallOut()
  {
    counting = false;
    WorldRotator.Instance.activated = false;
    Camera.main.GetComponent<CameraController>().SetMode(CameraController.CameraMode.FALLING);
    StartCoroutine(LoadSceneDelay(SceneManager.GetActiveScene().name, 1.5f));
  }

  private IEnumerator LoadSceneDelay(string name, float delay)
  {
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene(name);
  }

  private void OnApplicationQuit()
  {
    ScoreTracker.Instance.Save();
  }
}
