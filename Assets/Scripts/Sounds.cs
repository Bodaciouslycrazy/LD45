using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
  public static Sounds Instance;

  [SerializeField]
  private GameObject soundPrefab;

  private void Awake()
  {
    Instance = this;
  }
  public void PlaySound(AudioClip sound, Vector3 position, float volume = 1f, float pitch = 1f)
  {
    GameObject obj = Instantiate(soundPrefab, position, Quaternion.identity);
    AudioSource audioSource = obj.GetComponent<AudioSource>();
    audioSource.clip = sound;
    audioSource.volume = volume;
    audioSource.pitch = pitch;
    audioSource.Play();
  }
}
