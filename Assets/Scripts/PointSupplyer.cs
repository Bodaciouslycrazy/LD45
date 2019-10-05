using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSupplyer : MonoBehaviour
{
  public static PointSupplyer Instance = null;

  private void Start()
  {
    Instance = this;
  }

  public Vector3 GetPoint()
  {
    return transform.position - new Vector3(0,transform.localScale.y / 2 ,0);
  }
}
