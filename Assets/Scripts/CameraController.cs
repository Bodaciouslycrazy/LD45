using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private Transform following;
  [SerializeField]
  private float distance = 5f;
  [SerializeField]
  private float ringHeight = 2.5f;
  [SerializeField]
  private float focalHeight = 0;

  private void Update()
  {
    Vector3 pos = transform.position;
    pos.y = following.position.y + ringHeight;

    float h = distance - ringHeight;
    float smallRadius = Mathf.Sqrt(h * (2 * distance - h));
    Vector3 discCenter = following.position + new Vector3(0, ringHeight, 0);

    float dist = Vector3.Distance(pos, discCenter);
    pos -= discCenter;
    pos = pos.normalized * smallRadius;
    pos += discCenter;

    transform.position = pos;

    transform.LookAt(following.position + new Vector3(0, focalHeight, 0), Vector3.up);
  }
}
