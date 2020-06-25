using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MusicPlayerResources : ScriptableObject
{
    [Header("Shortcuts")]
    public KeyCode nextKey;
    public KeyCode previousKey;
    public Track[] trackList;
}
