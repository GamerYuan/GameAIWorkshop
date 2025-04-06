using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Gameplay.Enemy.Tests.PlayMode
{
    public class EnemyTest
    {
        private EnemyStateController controller;
        private EnemyController enemy;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene(1);
            yield return null;

            var playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
            Assert.IsNotNull(playerPrefab, "Could not load player prefab");
            var player = MonoBehaviour.Instantiate(playerPrefab, new Vector3(0f, 20f, 20f), Quaternion.identity);
            Debug.Log(player.transform.position);
            yield return null;

            var enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
            Assert.IsNotNull(enemyPrefab, "Could not load enemy prefab");
            var enemy = MonoBehaviour.Instantiate(enemyPrefab, new Vector3(0f, 1f, 10f), Quaternion.identity);
            Debug.Log(enemy.transform.position);
            controller = enemy.GetComponent<EnemyStateController>();
            this.enemy = controller.GetComponent<EnemyController>();
            yield return null;
        }

        [UnityTest]
        public IEnumerator EnemyStateController_TransitionTo_Success()
        {
            Assert.IsNotNull(controller);
            controller.TransitionToState(new EnemyIdle(controller, controller.GetComponent<EnemyController>(), false));
            yield return null;
            Assert.AreEqual(typeof(EnemyIdle), controller.CurrentState.GetType());
            yield return null;
        }
    }
}
