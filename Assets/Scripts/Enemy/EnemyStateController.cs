using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyStateController : MonoBehaviour
    {
        public IState CurrentState { get; private set; }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            CurrentState = new EnemyIdle(this, GetComponent<EnemyController>(), true);
        }

        // Update is called once per frame
        void Update()
        {
            CurrentState?.Execute();
        }

        public void TransitionToState(IState state)
        {
            Debug.Log("Transitioning to state: " + state.GetType().Name);
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }
    }
}
