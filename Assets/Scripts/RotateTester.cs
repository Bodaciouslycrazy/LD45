using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTester : MonoBehaviour
{
  [SerializeField]
  private PointSupplyer pointSupplyer;
  [SerializeField]
  private Transform rotationParent;
  [SerializeField]
  private Transform camera;

  [SerializeField]
  private float timeScale = 1f;
  [SerializeField]
  private float amplitude = 5f;


  private void Update()
  {
    //float x = Mathf.Cos(Time.time * 2 * Mathf.PI * timeScale) * amplitude;
    //float z = Mathf.Sin(Time.time * 2 * Mathf.PI * timeScale) * amplitude;
  }

  private void FixedUpdate()
  {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    float xCircle = x * Mathf.Sqrt(1 - z * z / 2) * amplitude;
    float zCircle = z * Mathf.Sqrt(1 - x * x / 2) * amplitude;


    Vector3 forward = camera.forward;
    forward.y = 0;
    Quaternion camRotation = Quaternion.LookRotation(forward, Vector3.up);
    Quaternion targetAngle = Quaternion.AngleAxis(zCircle, camRotation * Vector3.right) * Quaternion.AngleAxis(-xCircle, camRotation * Vector3.forward);

    Quaternion currentAngle = transform.rotation;

    Vector3 rotatePoint = pointSupplyer.GetPoint();

    rotationParent.position = rotatePoint;
    rotationParent.rotation = currentAngle;

    transform.SetParent(rotationParent, true);
    rotationParent.rotation = Quaternion.Lerp(currentAngle, targetAngle, 0.2f);
    transform.SetParent(null, true);
  }
}