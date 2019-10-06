using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public enum CameraMode
  {
    NORMAL,
    FALLING
  }

  [SerializeField]
  private Transform following;
  [SerializeField]
  public float distance = 3.5f;
  [Header("Ring Height Function Vars")]
  [SerializeField]
  [Range(-1, 1)]
  private float topBound = 0.95f;
  [SerializeField]
  [Range(-1, 1)]
  private float bottomBound = 0.1f;
  [SerializeField]
  private float steepness = .4f;
  [SerializeField]
  private float shift = 0f;

  [Space(10)]
  [SerializeField]
  [Range(0f,1f)]
  private float focalHeight = 0.2f;

  [SerializeField]
  private float mouseSensitivity = 3f;
  [SerializeField]
  private float joystickSensitivity = 1f;

  private float vy = 0f;
  private CameraMode currentMode = CameraMode.NORMAL;

  public void SetMode(CameraMode newMode)
  {
    currentMode = newMode;
  }

  private void Update()
  {
    switch(currentMode)
    {
      case CameraMode.NORMAL:
        NormalUpdate();
        break;
      case CameraMode.FALLING:
        FallingUpdate();
        break;
    }
  }

  private void NormalUpdate()
  {
    //Manual camera rotation
    float mouseRotation = Input.GetAxis("MouseX") * mouseSensitivity + Input.GetAxis("Horizontal2") * Time.deltaTime * 10;
    float joystickRotation = Input.GetAxis("Horizontal2") * Time.deltaTime * 360 * joystickSensitivity;
    transform.RotateAround(following.position, Vector3.up, mouseRotation + joystickRotation);

    //Manage camera position
    Vector3 pos = transform.position;
    vy = Mathf.Lerp(vy, following.GetComponent<Rigidbody>().velocity.y, 10f * Time.deltaTime);
    float normalizedRingHeight = -((topBound - bottomBound) * Mathf.Atan(steepness * (shift + vy)) / Mathf.PI) + ((topBound + bottomBound) / 2);
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

  private void FallingUpdate()
  {
    transform.LookAt(following.position + (Vector3.up * focalHeight * distance), Vector3.up);
  }
}
