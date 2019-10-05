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

    Vector3 targetAngle = new Vector3(zCircle, 0, -xCircle);
    Vector3 currentAngle = transform.rotation.eulerAngles;

    Vector3 rotatePoint = pointSupplyer.GetPoint();

    rotationParent.position = rotatePoint;
    rotationParent.rotation = Quaternion.Euler(currentAngle);

    transform.SetParent(rotationParent, true);
    rotationParent.rotation = Quaternion.Lerp(Quaternion.Euler(currentAngle), Quaternion.Euler(targetAngle), 0.2f);
    transform.SetParent(null, true);
  }
}