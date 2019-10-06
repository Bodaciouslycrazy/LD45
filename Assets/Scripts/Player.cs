using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  public static Player Instance;

  private CameraController cameraController;
  [SerializeField]
  private float density = 1.5f;
  [SerializeField]
  private AudioClip impactSound;
  [SerializeField]
  private float minImpactForce = 0f;
  [SerializeField]
  private float maxImpactForce = 0f;

  private SphereCollider sphere;
  private Rigidbody rigidbody;
  private float volume = 0f;
  private List<KeyCollectable.KeyType> collectedKeys;

  private const float cameraDistMult = 6;

  private void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    sphere = GetComponent<SphereCollider>();
    cameraController = Camera.main.GetComponent<CameraController>();
    collectedKeys = new List<KeyCollectable.KeyType>();
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
    Instance = this;
  }
  public void AddVolume(float vol)
  {
    SetVolume(volume + vol);
  }

  public void AddKey(KeyCollectable.KeyType keyType)
  {
    //Debug.Log("ADDED KEY");
    collectedKeys.Add(keyType);
  }

  public bool HasKey(KeyCollectable.KeyType keyType)
  {
    return collectedKeys.Contains(keyType);
  }

  public void RotateVelocity(Quaternion delta)
  {
    Vector3 vel = rigidbody.velocity;
    vel = delta * vel;
    rigidbody.velocity = vel;
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Collectable")
    {
      Collectable collectable = other.gameObject.GetComponent<Collectable>();
      if(volume >= collectable.requiredVolume)
      {
        AddVolume(collectable.volume);
        collectable.Collect(this);
      }
    }
    else if(other.gameObject.tag == "Fall")
    {
      LevelController.CurrentLevel.FallOut();
    }
  }
  private void OnCollisionEnter(Collision collision)
  {
    float force = collision.impulse.magnitude;
    //Debug.Log(force);
    if(force > minImpactForce)
    {
      float slope = 1f / (maxImpactForce - minImpactForce);
      float vol = Mathf.Clamp(slope * (force - minImpactForce), 0f, 1f);
      Sounds.Instance.PlaySound(impactSound, transform.position, vol);
    }
  }

  public void FinishLevel()
  {
    StartCoroutine(FinishLevelCoroutine());
  }

  private IEnumerator FinishLevelCoroutine()
  {
    WorldRotator.Instance.activated = false;
    rigidbody.useGravity = false;
    rigidbody.drag = 1f;
    Camera.main.GetComponent<CameraController>().SetMode(CameraController.CameraMode.FALLING);
    yield return new WaitForSeconds(0.5f);
    Explode();
    yield return new WaitForSeconds(0.1f);

    rigidbody.mass = 1;
    float timeFlying = 1.5f;
    while(timeFlying > 0)
    {
      rigidbody.AddForce(Vector3.up * Time.deltaTime * 10000f);
      timeFlying -= Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
  }

  private void Explode()
  {
    while(transform.childCount > 0)
    {
      Transform item = transform.GetChild(0);
      item.SetParent(null);
      Rigidbody rb = item.gameObject.AddComponent<Rigidbody>();
      rb.mass = .25f;
      rb.AddExplosionForce(Random.Range(2.5f,4f), transform.position, 10f, 1f, ForceMode.Impulse);
    }
  }
}
