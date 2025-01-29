using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float m_MovementSpeed = 1f;
    [SerializeField] private float m_RunningSpeed = 2f;
    [SerializeField] private float m_JumpSpeed = 1f;
    [SerializeField] private float m_LookSpeed = 0.1f;
    [SerializeField] private Transform m_CameraLocation;

    private CharacterController m_CharacterController;
    private Camera m_Camera;

    [Header("Debug")]
    [SerializeField] private Vector3 m_PlayerVelocity;
    [SerializeField] private Vector2 m_MoveInput;
    private bool m_IsJump;
    private bool m_IsRunning;

    private const float GRAVITY = -9.81f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        m_Camera.transform.position = m_CameraLocation.transform.position;
        m_Camera.transform.SetParent(transform);
    }

    private void Update()
    {
        var grounded = m_CharacterController.isGrounded;

        if (grounded)
        {
            if (m_PlayerVelocity.y < 0)
            {
                m_PlayerVelocity.y = 0f;
            }
            Vector3 forward = (m_IsRunning ? m_RunningSpeed : m_MovementSpeed) * m_MoveInput.y * transform.forward;
            Vector3 right = (m_IsRunning ? m_RunningSpeed : m_MovementSpeed) * m_MoveInput.x * transform.right;

            m_PlayerVelocity = new Vector3(forward.x + right.x, m_PlayerVelocity.y, forward.z + right.z);
        }

        if (m_IsJump)
        {
            m_IsJump = false;
            if (grounded) m_PlayerVelocity.y += Mathf.Sqrt(m_JumpSpeed * -2.0f * GRAVITY);
        }

        m_PlayerVelocity.y += GRAVITY * Time.deltaTime;
        m_CharacterController.Move(m_PlayerVelocity * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        m_MoveInput = ctx.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext _)
    {
        if (!m_IsJump)
        {
            m_IsJump = true;
        }
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        if (m_Camera == null) return;

        Vector2 look = ctx.ReadValue<Vector2>() * m_LookSpeed;
        transform.Rotate(Vector3.up, look.x);
        m_Camera.transform.localEulerAngles = new Vector3(
            Mathf.Clamp((m_Camera.transform.eulerAngles.x + 90 - look.y) % 360, 10, 170) - 90, 0, 0);
    }

    public void Run(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            m_IsRunning = true;
        }
        else if (ctx.canceled)
        {
            m_IsRunning = false;
        }
    }
}
