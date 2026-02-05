using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIdenx; //track di quale waypoint sta seguendo

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit() 
    { 

    }

    public void PatrolCycle()
    {
        //implementa la logica del Patrol
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            if (waypointIdenx < enemy.path.waypoints.Count - 1)
                waypointIdenx++;
            else 
                waypointIdenx = 0;
            enemy.Agent.SetDestination(enemy.path.waypoints[waypointIdenx].position);

        }
    }
}
