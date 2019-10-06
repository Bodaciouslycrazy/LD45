using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectable : MonoBehaviour
{
  private static List<Collectable> allCollectables = new List<Collectable>();

  public float requiredVolume = 0.5f;
  public float volume = 0.3f;

  private Collider collider;

  private void Awake()
  {
    collider = GetComponent<Collider>();
    allCollectables.Add(this);
  }

  private void OnDestroy()
  {
    allCollectables.Remove(this);
  }

  public virtual void Collect(Player player)
  {
    collider.enabled = false;
    transform.SetParent(player.transform, true);
  }

  private void SetCollectable(bool canCollect)
  {
    collider.isTrigger = canCollect;
    tag = canCollect ? "Collectable" : "Untagged";
  }

  public static void EvaluateCollectables(float volume)
  {
    foreach(Collectable c in allCollectables)
    {
      c.SetCollectable(volume >= c.requiredVolume);
    }
  }
}
