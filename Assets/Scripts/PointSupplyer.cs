using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSupplyer : MonoBehaviour
{
  private void Start()
  {
    GetComponent<Rigidbody>().maxAngularVelocity = 100;
  }

  public Vector3 GetPoint()
  {
    return transform.position - new Vector3(0,transform.localScale.y / 2 ,0);
  }
}
