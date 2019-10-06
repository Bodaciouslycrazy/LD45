using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRotator : MonoBehaviour
{
  public static WorldRotator Instance = null;

  public bool activated = true;
  [SerializeField]
  private Transform mainCamera;

  [SerializeField]
  private float amplitude = 25f;

  private void Awake()
  {
    Instance = this;
  }

  private void FixedUpdate()
  {
    float x = 0f;
    float z = 0f;
    if (activated)
    {
      x = Input.GetAxis("Horizontal");
      z = Input.GetAxis("Vertical");
    }
    float xCircle = x * Mathf.Sqrt(1 - z * z / 2) * amplitude;
    float zCircle = z * Mathf.Sqrt(1 - x * x / 2) * amplitude;

    //Change our axis relative to camera facing
    Vector3 forward = mainCamera.forward;
    forward.y = 0;
    Quaternion camRotation = Quaternion.LookRotation(forward, Vector3.up);
    Quaternion targetAngle = Quaternion.AngleAxis(zCircle, camRotation * Vector3.right) * Quaternion.AngleAxis(-xCircle, camRotation * Vector3.forward);
    Quaternion currentAngle = transform.rotation;
    Quaternion newAngle = Quaternion.Lerp(currentAngle, targetAngle, 10f * Time.deltaTime);
    Quaternion delta = newAngle * Quaternion.Inverse(currentAngle);

    Vector3 deltaAxis = Vector3.zero;
    float deltaAngle = 0;
    delta.ToAngleAxis(out deltaAngle, out deltaAxis);
    //Get point to rotate around
    Vector3 rotatePoint = PointSupplyer.Instance.GetPoint();

    transform.RotateAround(rotatePoint, deltaAxis, deltaAngle);
    Player.Instance.RotateVelocity(delta);
  }
}