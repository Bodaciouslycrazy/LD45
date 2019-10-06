using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDestroyer : MonoBehaviour
{
  private AudioSource source;


  private void Awake()
  {
    source = GetComponent<AudioSource>();
  }

  private void Update()
  {
    if (!source.isPlaying)
    {
      Destroy(gameObject);
    }
  }
}
