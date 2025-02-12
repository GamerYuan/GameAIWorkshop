using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class EnemyBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float m_MovementSpeed = 1f;
    [SerializeField] private float m_AggroRange = 5f;

    [Header("Object References")]
    [SerializeField] private Transform m_PlayerTransform;
    [SerializeField] private Transform m_NavmeshDestination;

    private Rigidbody m_Rigidbody;

    [Header("Debug")]
    [SerializeField] private EnemyStates m_CurrentState = EnemyStates.Idle;
    [SerializeField] private bool m_CanMove = true;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, m_PlayerTransform.position) < m_AggroRange)
        {
            m_CurrentState = EnemyStates.Aggro;
        }

        switch (m_CurrentState)
        {
            case EnemyStates.Idle:
                ExecuteIdle();
                break;
            case EnemyStates.Moving:
                ExecuteMoving();
                break;
            case EnemyStates.Aggro:
                ExecuteAggro();
                break;
        }
    }

    private void ExecuteIdle()
    {
        if (m_CanMove)
        {
            StartCoroutine(MoveStateCoroutine());
        }
    }

    private void ExecuteMoving()
    {
        m_Rigidbody.linearVelocity = transform.forward * m_MovementSpeed;
    }

    private void ExecuteAggro()
    {
        if (Vector3.Distance(transform.position, m_PlayerTransform.position) > m_AggroRange + 1f)
        {
            m_CurrentState = EnemyStates.Idle;
            return;
        }

        transform.LookAt(new Vector3(m_PlayerTransform.position.x, transform.position.y, m_PlayerTransform.position.z));
        m_Rigidbody.linearVelocity = transform.forward * m_MovementSpeed;
    }

    private IEnumerator MoveStateCoroutine()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        m_CanMove = false;
        m_CurrentState = EnemyStates.Moving;

        yield return new WaitForSeconds(2f);
        if (m_CurrentState == EnemyStates.Moving) m_CurrentState = EnemyStates.Idle;

        yield return new WaitForSeconds(2f);
        m_CanMove = true;
    }

    public enum EnemyStates
    {
        Idle,
        Moving,
        Aggro
    }
}
