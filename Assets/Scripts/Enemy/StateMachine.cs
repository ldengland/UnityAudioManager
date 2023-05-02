using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public Transform playerTransform;

    public BaseState activeState;

    public PatrolState patrolState;

    public PursuitState pursuitState;

    public void Initialize()
    {
        patrolState = new PatrolState();
        pursuitState = new PursuitState();
        
        ChangeState(patrolState);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
            activeState.Perform();
    }

    public void ChangeState(BaseState newState)
    {
        if (activeState != null)
            activeState.Exit();

        activeState = newState;

        if(activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }
}
