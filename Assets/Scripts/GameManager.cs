using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UI ui;
    public PlayerController player;
    public GatesController gatesController;

    public enum GameState
    {
        Playing,
        Dead,
        Fail,
        Passed
    }

    public static GameState state;

    [Header("Gate Settings")]
    public float distance = 1.0f;

    public float positionTolerance = 0.5f;
    public float angleTolerance = 0.5f;

    [Header("Game Rules")]
    public int maxGatesCount = 10;
    public int gateTimeBonusValue = 5;
    public int countdownMaxValue = 5;

    private Gate gateCached;
    private int gatesPassed;
    private float currentCountdownValue;

    // Start is called before the first frame update
    void Start()
    {
        gateCached = gatesController.SpawnGate(GetNextGatePosition(player.transform), GetNextGateRotation(player.transform.rotation));
        gateCached = gatesController.SpawnGate(GetNextGatePosition(gateCached.Transform), GetNextGateRotation(gateCached.Transform.rotation));
        state = GameState.Playing;
        currentCountdownValue = countdownMaxValue;
        ui.UpdateGateCounter(gatesPassed, maxGatesCount);
        ui.UpdateTimer(currentCountdownValue, countdownMaxValue);
    }

    void Update()
    {
        currentCountdownValue -= Time.deltaTime;
        ui.UpdateTimer(currentCountdownValue, countdownMaxValue);
        if(currentCountdownValue <= 0)
        {
            state = GameState.Fail;
            ui.ShowGameOverScreenFail();
        }
    }

    public void GateTrigger(GameObject gate)
    {
        currentCountdownValue += gateTimeBonusValue;
        ui.UpdateTimer(currentCountdownValue, countdownMaxValue);
        ui.UpdateGateCounter(++gatesPassed, maxGatesCount);
        if(gatesPassed >= maxGatesCount)
        {
            state = GameState.Passed;
            ui.ShowGameOverScreenPassed();
        }
        gateCached = gatesController.SpawnGate(GetNextGatePosition(gateCached.transform), GetNextGateRotation(gateCached.transform.rotation));
        gatesController.Disable(gate.GetComponent<Gate>());
    }

    public void PlaneGotCrashed()
    {
        state = GameState.Dead;
        ui.ShowGameOverScreenDeath();
    }

    private Vector3 GetNextGatePosition(Transform relativePosition)
    {
        Vector3 offset = relativePosition.position + relativePosition.forward * distance;

        Vector3 newRandomPosition = new Vector3(
            Random.Range(-positionTolerance, positionTolerance),
            Random.Range(-positionTolerance, positionTolerance),
            Random.Range(-positionTolerance, positionTolerance)
        );

        return offset + newRandomPosition;
    }

    private Quaternion GetNextGateRotation(Quaternion relativeRotation)
    {
        Quaternion newRandomRotation = Quaternion.Euler(
            Random.Range(-angleTolerance, angleTolerance),
            Random.Range(-angleTolerance, angleTolerance),
            Random.Range(-angleTolerance, angleTolerance)
        );

        return newRandomRotation * relativeRotation;
    }

    public void OnRestartClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
