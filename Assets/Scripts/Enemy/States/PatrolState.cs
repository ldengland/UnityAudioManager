using UnityEngine;

public class PatrolState : BaseState
{
    private int waypointIndex;
    public float waitTimer;

    public float detectionRange = 10f;
 
    public override void Enter()
    {
        
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        PatrolCycle();
        if(stateMachine.playerTransform == null)
            Debug.Log("Player is null! (PatrolState)");

        // Detect player and switch to PursuitState
        if (Vector3.Distance(enemy.transform.position, stateMachine.playerTransform.position) <= detectionRange)
        {
            stateMachine.ChangeState(stateMachine.pursuitState);
        }
    }

    public void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > 3f)
            {
                if(waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0f;
            }
        }

    }
}
