using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : Singleton
{
    private Coroutine currentCoroutine;

    public void SlowDownGame() {
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SlowDown());
    }

    public void SpeedUpGame() {
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(SpeedUp());
    }

    private IEnumerator SlowDown() {
        float duration = 0.15f;
        float steps = 10f;
        float decreasePerStep = 1/steps;
        
        for(var i = 0; i < steps; i++) {
            Time.timeScale -= decreasePerStep;

            if (Time.timeScale < 0.1) {
                Time.timeScale = 0;
                yield break;
            }

            yield return new WaitForSecondsRealtime(duration/steps);
        }
    }

    private IEnumerator SpeedUp() {
        float duration = 0.2f;
        float steps = 10f;
        float increasePerStep = 1/steps;
        
        for(var i = 0; i < steps; i++) {
            Time.timeScale += increasePerStep;

            if (Time.timeScale > 1) {
                yield break;
            }

            yield return new WaitForSecondsRealtime(duration/steps);
        }
    }
}
