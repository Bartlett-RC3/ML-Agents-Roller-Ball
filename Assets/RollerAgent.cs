using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent : Agent
{
    Rigidbody rBody;
    public Transform Target;
    public float forceMultiplier = 10;

    void Start()
    {
        this.rBody = GetComponent<Rigidbody>();
    }

    private void ResetAgent()
    {
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        }
    }

    private void ResetTargetRandomPosition()
    {
        this.Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void OnEpisodeBegin()
    {
        ResetAgent();
        ResetTargetRandomPosition();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target positions -> 3 values
        sensor.AddObservation(this.Target.localPosition);

        // Agent positions -> 3 values
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity -> 2 values
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);

        // total observation size -> 8
    }

    private void Move(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        this.rBody.AddForce(controlSignal * this.forceMultiplier);

        // total action count (continous) -> 2
    }

    private void CloseToTargetReward()
    {
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, this.Target.localPosition);

        if (distanceToTarget < 1f)
        {
            AddReward(1.0f);
            EndEpisode();
        }
    }

    private void PunishFall()
    {
        AddReward(-10f);
    }

    private void OnPlaneCheck()
    {
        if (this.transform.localPosition.y < 0)
        {
            PunishFall();
            EndEpisode();
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Move(actionBuffers);
        CloseToTargetReward();
        OnPlaneCheck();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
