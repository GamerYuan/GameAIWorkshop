using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_AggroRange;

    private EnemyMovement m_EnemyMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_EnemyMovement = GetComponent<EnemyMovement>();
    }

    public void MoveTo(Vector3 position)
    {
        m_EnemyMovement.MoveTo(position);
    }

    public void MoveToPlayer()
    {
        m_EnemyMovement.MoveTo(m_Player.transform.position);
    }

    public void StopNavigation()
    {
        m_EnemyMovement.StopNavigation();
    }

    public bool HasReachedDestination()
    {
        return m_EnemyMovement.HasReachedDestination();
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, m_Player.transform.position) < m_AggroRange;
    }
}
