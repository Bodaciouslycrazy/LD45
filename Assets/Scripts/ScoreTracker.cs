﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[Serializable]
public class LevelData
{
  public int level;
  public List<float> runTimes;
}

[Serializable]
public class ScoreTracker
{
  private static ScoreTracker instance;

  public List<LevelData> levels;
  const string defaultFile = "/save.json";
  private ScoreTracker() {
    string path = Application.persistentDataPath + defaultFile;
    if (File.Exists(path))
    {
      Debug.Log("Reading file from " + path);
      readData(path);
    }
    else
    {
      Debug.Log("Couldn't find save. Creating new.");
      levels = new List<LevelData>();
    }
  }

  public static ScoreTracker Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new ScoreTracker();
      }
      return instance;
    }
  }

  public void updateLevelData(int level, float runTime)
  {
    LevelData data = null;

    foreach (LevelData l in levels)
    {
      if (l.level == level)
      {
        data = l;
      }
    }

    if (data == null)
    {
      data = new LevelData();
      data.level = level;
      data.runTimes = new List<float>();
      levels.Add(data);
    }

    data.runTimes.Add(runTime);
    while (data.runTimes.Count > 10)
    {
      int indexOfMin = 0;
      float min = data.runTimes[0];

      for (int i = 0; i < data.runTimes.Count; i++)
      {
        float r = data.runTimes[i];

        if (r < min)
        {
          indexOfMin = i;
          min = r;
        }
      }

      data.runTimes.RemoveAt(indexOfMin);
    }
  }

  public void Save()
  {
    string path = Application.persistentDataPath + defaultFile;
    writeData(path);
  }

  public void readData(string filename)
  {
    string json = File.ReadAllText(filename);
    JsonUtility.FromJsonOverwrite(json, this);
  }

  public void writeData(string filename)
  {
    string json = JsonUtility.ToJson(this);
    File.WriteAllText(filename, json);
  }
}