 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectable : Collectable
{
  [SerializeField]
  private KeyType keyType;
  public enum KeyType
  {
    GOLD
  }

  [SerializeField]
  private BananaRotate rotator;
  public override void Collect(Player player)
  {
    base.Collect(player);
    rotator.enabled = false;
    player.AddKey(keyType);
  }
}
