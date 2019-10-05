using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField]
  private Transform following;
  [SerializeField]
  public float distance = 5f;
  [Header("Ring Height Function Vars")]
  [SerializeField]
  [Range(-1, 1)]
  private float topBound = 1f;
  [SerializeField]
  [Range(-1, 1)]
  private float bottomBound = -1;
  [SerializeField]
  private float steepness = 1f;
  [SerializeField]
  private float shift = 0f;

  [Space(10)]
  [SerializeField]
  [Range(0f,1f)]
  private float focalHeight = 0;

  [SerializeField]
  private float mouseSensitivity = 1f;
  [SerializeField]
  private float joystickSensitivity = 1f;
  
  private void Update()
  {
    //Manual camera rotation
    float mouseRotation = Input.GetAxis("MouseX") * mouseSensitivity + Input.GetAxis("Horizontal2") * Time.deltaTime * 10;
    float joystickRotation = Input.GetAxis("Horizontal2") * Time.deltaTime * 360 * joystickSensitivity;
    transform.RotateAround(following.position, Vector3.up, mouseRotation + joystickRotation);

    //Manage camera position
    Vector3 pos = transform.position;
    float normalizedRingHeight = -((topBound - bottomBound) * Mathf.Atan(steepness * (shift + following.GetComponent<Rigidbody>().velocity.y)) / Mathf.PI) + ((topBound + bottomBound) / 2);
    pos.y = following.position.y + (normalizedRingHeight * distance);

    float h = distance - (normalizedRingHeight * distance);
    float smallRadius = Mathf.Sqrt(h * (2 * distance - h));
    Vector3 discCenter = following.position + new Vector3(0, normalizedRingHeight * distance, 0);

    float dist = Vector3.Distance(pos, discCenter);
    pos -= discCenter;
    pos = pos.normalized * smallRadius;
    pos += discCenter;

    transform.position = pos;

    transform.LookAt(following.position + new Vector3(0, focalHeight * distance, 0), Vector3.up);
  }
}
