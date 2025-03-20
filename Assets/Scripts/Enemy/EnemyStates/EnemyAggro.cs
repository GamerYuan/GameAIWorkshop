using UnityEngine;

public class EnemyAggro : IState
{
    private EnemyStateController m_StateController;
    private EnemyController m_Enemy;

    public EnemyAggro(EnemyStateController stateController, EnemyController enemy)
    {
        m_Enemy = enemy;
        m_StateController = stateController;
    }

    public void Enter()
    {
        Debug.Log("Entering aggro state");
    }

    public void Execute()
    {
        if (!m_Enemy.IsPlayerInRange())
        {
            m_StateController.TransitionToState(new EnemyIdle(m_StateController, m_Enemy, false));
        }

        m_Enemy.MoveToPlayer();
    }

    public void Exit()
    {
        // do nothing
    }
}
