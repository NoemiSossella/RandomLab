using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;

    public void Initialise()
    {
        patrolState = new PatrolState();
        changeState(patrolState);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void changeState(BaseState newState)
    {
        //controlla se activeState != null
        if (activeState != null)
        {
            //fa un cleanup on actionState
            activeState.Exit();
        }
        //cambia in un nuovo stato
        activeState = newState;

        //controlla davvero che non sia null
        if (activeState != null)
        {
            //nuovo Stato
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();

        }
    }
}
