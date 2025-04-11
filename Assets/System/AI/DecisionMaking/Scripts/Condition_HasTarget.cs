using UnityEngine;

public class Condition_HasTarget : DecisionTreeNode_Condition
{
    protected override bool ConditionIsMeet()
    {
        //Debug.Log($"enemy: {enemy}", enemy);
        //Debug.Log($"enemy.HasTarget(): {enemy.HasTarget()}");
        return enemy.HasTarget();
    }
}
