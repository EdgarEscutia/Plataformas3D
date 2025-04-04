using System;
using UnityEngine;

public abstract class DecisionTreeNode_Condition : DecisionTreeNode
{
    public override void Execute()
    {
        int childrenToExecuteIndex = ConditionIsMeet() ? 0 : 1;

        transform.GetChild(childrenToExecuteIndex).GetComponent<DecisionTreeNode>().Execute();
    }

    protected abstract bool ConditionIsMeet();

}
