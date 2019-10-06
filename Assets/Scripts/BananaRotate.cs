using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaRotate : MonoBehaviour
{
  [SerializeField]
  private float speed = 1f;
  [SerializeField]
  private Vector3 rotationAxis = new Vector3(0, 0, 1);
  private void Update()
  {
    transform.Rotate(rotationAxis * speed * Time.deltaTime * 360, Space.Self);
  }
}
