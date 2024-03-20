using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundClipsSO : ScriptableObject
{
    public List<AudioClip> chop;
    public List<AudioClip> deliveryFail; 
    public List<AudioClip> deliverySuccess;
    public List<AudioClip> footstep;
    public List<AudioClip> objectDrop;
    public List<AudioClip> objectPickup;
    public List<AudioClip> panSizzle;
    public List<AudioClip> trash;
    public List<AudioClip> warning;
}
