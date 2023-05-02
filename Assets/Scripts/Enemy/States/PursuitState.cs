using UnityEngine;

public class PursuitState : BaseState
{
    public float chaseDistanceThreshold = 15f;

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        ChasePlayer();

        // Check if the player is far enough to switch back to PatrolState
        if (Vector3.Distance(enemy.transform.position, stateMachine.playerTransform.position) > chaseDistanceThreshold)
        {
            stateMachine.ChangeState(stateMachine.patrolState);
        }
    }

    private void ChasePlayer()
    {
        enemy.Agent.SetDestination(stateMachine.playerTransform.position);
    }
}