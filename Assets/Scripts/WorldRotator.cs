using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotator : MonoBehaviour
{
  public static WorldRotator Instance = null;

  //[SerializeField]
  //private PointSupplyer pointSupplyer;
  [SerializeField]
  private Transform rotationParent;
  [SerializeField]
  private Transform camera;

  [SerializeField]
  private float amplitude = 5f;

  private void Start()
  {
    Instance = this;
  }

  private void FixedUpdate()
  {
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    float xCircle = x * Mathf.Sqrt(1 - z * z / 2) * amplitude;
    float zCircle = z * Mathf.Sqrt(1 - x * x / 2) * amplitude;

    //Change our axis relative to camera facing
    Vector3 forward = camera.forward;
    forward.y = 0;
    Quaternion camRotation = Quaternion.LookRotation(forward, Vector3.up);
    Quaternion targetAngle = Quaternion.AngleAxis(zCircle, camRotation * Vector3.right) * Quaternion.AngleAxis(-xCircle, camRotation * Vector3.forward);

    Quaternion currentAngle = transform.rotation;

    //Get point to rotate around
    Vector3 rotatePoint = PointSupplyer.Instance.GetPoint();

    //most empty object to that point and the current rotation
    rotationParent.position = rotatePoint;
    rotationParent.rotation = currentAngle;

    //Set parent, rotate, then remove parent
    transform.SetParent(rotationParent, true);
    rotationParent.rotation = Quaternion.Lerp(currentAngle, targetAngle, 0.2f);
    transform.SetParent(null, true);
  }
}