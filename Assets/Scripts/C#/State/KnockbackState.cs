using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackState : EnemyState
{
    private Vector2 goalPosition;
    public KnockbackState (Vector2 goalPosition)
    {
        this.goalPosition = goalPosition;
    }
    public override bool AchievedGoal(Entity entity)
    {
        if(Vector2.Distance(entity.position, goalPosition) <= 0.2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
