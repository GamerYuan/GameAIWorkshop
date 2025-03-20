using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyIdle : IState
{
    private EnemyStateController m_StateController;
    private EnemyController m_Enemy;

    private bool m_CanMove;

    private CancellationTokenSource m_MoveCountdownTokenSource;

    public EnemyIdle(EnemyStateController stateController, EnemyController enemy, bool canMove)
    {
        m_Enemy = enemy;
        m_StateController = stateController;
        m_CanMove = canMove;

        m_MoveCountdownTokenSource = new CancellationTokenSource();
    }

    public void Enter()
    {
        Debug.Log("Entering idle state");
        Task.Run(() => WaitAndMove(m_MoveCountdownTokenSource.Token));
    }

    public void Execute()
    {
        if (m_Enemy.IsPlayerInRange())
        {
            m_StateController.TransitionToState(new EnemyAggro(m_StateController, m_Enemy));
            return;
        }

        if (m_CanMove)
        {
            m_StateController.TransitionToState(new EnemyMoving(m_StateController, m_Enemy));
            return;
        }
    }

    public void Exit()
    {
        m_MoveCountdownTokenSource.Cancel();
    }

    private async void WaitAndMove(CancellationToken token)
    {
        try
        {
            await Task.Delay(2000, token);
            m_CanMove = true;
        }
        catch
        {
            // Do nothing
        }
    }
}
