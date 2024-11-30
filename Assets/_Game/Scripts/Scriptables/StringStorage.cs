using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringStorage", menuName = "Game/StringStorage")]
public class StringStorage : ScriptableObject
{
    [field: SerializeField, Space(5)] public StringStorageType StringStorageType { get; private set; }
    [field: SerializeField, Space(5)] public List<string> FirstTimeStrings { get; private set; }
    [field: SerializeField, Space(5)] public List<string> NextTimeStrings { get; private set; }
}
