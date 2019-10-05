using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private CameraController cameraController;
  [SerializeField]
  private float density = 1.5f;
  private SphereCollider sphere;
  private Rigidbody rigidbody;
  private float volume = 0f;

  private const float cameraDistMult = 5;

  private void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    sphere = GetComponent<SphereCollider>();
    cameraController = Camera.main.GetComponent<CameraController>();
    rigidbody.maxAngularVelocity = 150;
    volume = (4f / 3f) * Mathf.PI * Mathf.Pow(sphere.radius, 3);
    SetVolume(volume);
  }


  public void SetVolume(float vol)
  {
    volume = vol;
    float oldRadius = sphere.radius;
    float newRadius = Mathf.Pow(volume * (3f / 4f) / Mathf.PI, 1f / 3f);
    cameraController.distance = cameraDistMult * newRadius;
    transform.position += new Vector3(0, newRadius - oldRadius, 0);
    sphere.radius = newRadius;
    rigidbody.mass = density * volume;
    Collectable.EvaluateCollectables(volume);
  }
  public void AddVolume(float vol)
  {
    SetVolume(volume + vol);
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Collectable")
    {
      Collectable collectable = other.gameObject.GetComponent<Collectable>();

      if(volume >= collectable.requiredVolume)
      {
        AddVolume(collectable.volume);
        collectable.Collect(transform);
        //other.collider.enabled = false;
        //other.transform.SetParent(transform, true);
      }
    }
  }
}
