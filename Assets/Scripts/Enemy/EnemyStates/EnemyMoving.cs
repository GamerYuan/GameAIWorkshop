using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyMoving : IState
    {
        private EnemyStateController m_StateController;
        private EnemyController m_Enemy;

        private Vector3 m_Destination;

        public EnemyMoving(EnemyStateController stateController, EnemyController enemy)
        {
            m_Enemy = enemy;
            m_StateController = stateController;
        }

        public void Enter()
        {
            Debug.Log("Entering moving state");
            var enemyPos = m_Enemy.transform.position;
            var randomPoint = Random.insideUnitCircle * 10;
            m_Destination = new Vector3(enemyPos.x + randomPoint.x, enemyPos.y, enemyPos.z + randomPoint.y);
            m_Enemy.MoveTo(m_Destination);
        }

        public void Execute()
        {
            if (m_Enemy.IsPlayerInRange())
            {
                m_StateController.TransitionToState(new EnemyAggro(m_StateController, m_Enemy));
                return;
            }

            if (m_Enemy.HasReachedDestination())
            {
                m_StateController.TransitionToState(new EnemyIdle(m_StateController, m_Enemy, false));
                return;
            }
        }

        public void Exit()
        {
            m_Enemy.StopNavigation();
        }
    }
}
