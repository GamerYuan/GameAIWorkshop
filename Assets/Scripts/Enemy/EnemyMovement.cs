using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private NavMeshAgent m_NavMeshAgent;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            m_NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveTo(Vector3 position)
        {
            m_NavMeshAgent.SetDestination(position);
        }

        public void StopNavigation()
        {
            m_NavMeshAgent.ResetPath();
        }

        public bool HasReachedDestination()
        {
            return !m_NavMeshAgent.pathPending && m_NavMeshAgent.remainingDistance <= m_NavMeshAgent.stoppingDistance;
        }
    }
}
