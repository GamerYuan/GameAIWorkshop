using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
        name: "Detect Player",
        description: "Detects if player is within a certain radius",
        category: "Action/Utility",
        story: "[Agent] detects [Player] within [Radius] units")]
public partial class DetectPlayer : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<float> Radius = new(10f);

    protected override Status OnStart()
    {
        if (Agent.Value == null || Player.Value == null)
        {
            return Status.Failure;
        }
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null || Player.Value == null)
        {
            return Status.Failure;
        }

        if (Vector3.Distance(Agent.Value.transform.position, Player.Value.transform.position) <= Radius.Value)
        {
            return Status.Success;
        }

        return Status.Running;
    }
}
