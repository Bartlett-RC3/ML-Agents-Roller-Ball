# ML-Agents-Roller-Ball

This is a repository that is being used for the Intro to ML-Agents turorial series. This is inspired by the **Making a New Learning Environment** tutorial bei ML-Agents: https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Learning-Environment-Create-New.md

The following is included:

- [X] Roller Ball agent implemented using ML-Agents package
- [X] Tracking custom metrics
- [X] Imitation learning
  - [X] Human baseline setup
- [X] Vector Observations
- [X] Visual Observations
- [X] Multi-Agents setup
- [X] Multi-Agent non-parameter sharing
- [X] Learning rate, gamma, batch-size, buffer-size ablations
- [ ] Memory module
- [ ] Curiosity
- [X] Training ML-Agents in terminal only
- [ ] Batch-training using bash


## Tracking Custom Metrics

This projects includes implemented custom metrics [here](https://github.com/Bartlett-RC3/ML-Agents-Roller-Ball/blob/main/Assets/RollerAgent.cs#L21).

Simplified code snippet to record custom metrics:

```cs
...
StatsRecorder statsRecorder;
float customMetrics = 0f;

void Start()
{
    // get StatsRecorder instance
    this.statsRecorder = Academy.Instance.StatsRecorder;
}
...
public override void OnEpisodeBegin()
{
    // record custom metrics
    this.statsRecorder.Add("Custom Metrics/My Metric", this.customMetrics);
    // reset custom metrics
    this.customMetrics = 0f;
}
...
public override void OnActionReceived(ActionBuffers actionBuffers)
{
    // add to custom metrics
    this.customMetrics += Mathf.Abs(actionBuffers.ContinuousActions[0]);
}
...
```

## Docs on: Learning rate, gamma, batch-size, buffer-size ablations

https://github.com/gzrjzcx/ML-agents/blob/master/docs/Training-PPO.md
