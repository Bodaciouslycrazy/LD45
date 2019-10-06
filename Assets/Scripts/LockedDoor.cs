using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
  [SerializeField]
  private KeyCollectable.KeyType requiredKey;

  [SerializeField]
  private Transform leftDoor;
  [SerializeField]
  private Transform rightDoor;
  private bool opened = false;

  private void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Player")
    {
      Player pl = other.GetComponent<Player>();

      if (pl.HasKey(requiredKey))
      {
        //Open door
        StartCoroutine(OpenDoor());
      }
    }
  }

  private IEnumerator OpenDoor()
  {
    if (opened)
    {
      yield break;
    }

    opened = true;
    float speed = 40f;
    float maxAngle = 80f;

    float curAngle = 0f;

    do
    {
      curAngle += Time.deltaTime * speed;
      leftDoor.localRotation = Quaternion.Euler(new Vector3(0, curAngle, 0));
      rightDoor.localRotation = Quaternion.Euler(new Vector3(0, -curAngle, 0));
      yield return new WaitForEndOfFrame();
    } while (curAngle < maxAngle);
  }
}
