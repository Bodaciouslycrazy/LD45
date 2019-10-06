using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingPlatform : MonoBehaviour
{
  [Serializable]
  private class Target
  {
    public Vector3 position;
    public Vector3 rotation;
    public float time;
    public bool linear = false;
  }

  [SerializeField]
  private Target[] targetPositions;
  //private Rigidbody rigidbody;
  private float curTime = 0f;
  private int curIndex = 0;

  private void Start()
  {
    //rigidbody = GetComponent<Rigidbody>();
  }
  private void Update()
  {
    int nextIndex = (curIndex + 1) % targetPositions.Length;
    curTime += Time.deltaTime;

    while(curTime >= targetPositions[nextIndex].time)
    {
      curTime -= targetPositions[nextIndex].time;
      curIndex = (curIndex + 1) % targetPositions.Length;
      nextIndex = (curIndex + 1) % targetPositions.Length;
    }

    float lerpAmount = curTime / targetPositions[nextIndex].time;
    if (!targetPositions[nextIndex].linear)
    {
      lerpAmount = 0.5f * Mathf.Sin(Mathf.PI * (lerpAmount - 0.5f)) + 0.5f;
    }
    Vector3 targetPos = Vector3.Lerp(targetPositions[curIndex].position, targetPositions[nextIndex].position, lerpAmount);
    Quaternion targetRotation = Quaternion.Lerp(Quaternion.Euler(targetPositions[curIndex].rotation), Quaternion.Euler(targetPositions[nextIndex].rotation), lerpAmount);
    transform.localPosition = targetPos;
    transform.localRotation = targetRotation;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    for(int i = 0; i < targetPositions.Length; i++)
    {
      int j = (i + 1) % targetPositions.Length;
      Gizmos.DrawSphere(targetPositions[i].position, 0.2f);
      Gizmos.DrawLine(targetPositions[i].position, targetPositions[j].position);
    }
  }
}
