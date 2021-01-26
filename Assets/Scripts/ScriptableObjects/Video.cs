using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Video", menuName = "Video/New Video")]
public class Video : ScriptableObject
{
    public string videoName;
    public VideoClip videoClip;
    public string description;
}
