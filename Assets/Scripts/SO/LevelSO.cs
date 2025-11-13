using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GoalCondition
{
    public string idTarget;
    public int count;


    public GoalCondition(string idTarget, int count)
    {
        this.idTarget = idTarget;
        this.count = count;
    }
}

[CreateAssetMenu(fileName = "Level_New", menuName = "Create new Level Data")]
public class LevelSO : ScriptableObject
{
    public string nameLevel;
    public string nameScene;
    public List<GoalCondition> conditions;
    [TextArea]
    public string goalDescription;
}
