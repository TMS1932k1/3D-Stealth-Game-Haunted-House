using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LEVEL_LIST_DATA", menuName = "Create new LevelList Data")]
public class LevelListSO : ScriptableObject
{
    public List<LevelSO> levels;
}
