using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DecisionTreeNode_Condition : DecisionTreeNode
{
    public override void Execute()
    {
        //Debug.Log($"{this.GetType()}", this);
        int childrenToExecuteIndex = ConditionIsMeet() ? 0 : 1;
        //Debug.Log($"Meets condition: {childrenToExecuteIndex == 0}", this);

        transform.GetChild(childrenToExecuteIndex).GetComponent<DecisionTreeNode>().Execute();
    }

    protected abstract bool ConditionIsMeet();

}
