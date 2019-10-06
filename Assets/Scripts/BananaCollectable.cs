using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCollectable : Collectable
{
  [SerializeField]
  private BananaRotate rotator;
  public override void Collect(Player player)
  {
    base.Collect(player);
    rotator.enabled = false;
  }
}
