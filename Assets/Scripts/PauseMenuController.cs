using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    private GameSession gameSession;
    private Coroutine currentCoroutine;

    AudioClip slowDownClip;
    AudioClip speedUpClip;

    void Awake() {
        AudioLoader audioLoader = FindObjectOfType<AudioLoader>();

        AudioClip loadedSlowdownAudioClip = audioLoader.GetAudioClipByName("slow_down");

        if (loadedSlowdownAudioClip) {
            slowDownClip = loadedSlowdownAudioClip;
        }

        AudioClip loadedSpeedupAudioClip = audioLoader.GetAudioClipByName("speed_up");

        if (loadedSpeedupAudioClip) {
            speedUpClip = loadedSpeedupAudioClip;
        }
    }

    void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gameSession.GetState() == State.paused) {
                pauseMenu.SetActive(false);
                gameSession.SetState(State.playing);

                //enable the game again
                SpeedUpGame();
                
            } else if(gameSession.GetState() == State.playing) {
                pauseMenu.SetActive(true);
                gameSession.SetState(State.paused);

                //disable the game
                SlowDownGame();
            }
        }
    }

    void SlowDownGame() {
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        AudioSource.PlayClipAtPoint(slowDownClip, Camera.main.transform.position);

        currentCoroutine = StartCoroutine(SlowDown());
    }

    void SpeedUpGame() {
        if (currentCoroutine != null) {
            StopCoroutine(currentCoroutine);
        }

        AudioSource.PlayClipAtPoint(speedUpClip, Camera.main.transform.position);

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

            yield return new WaitForSeconds(duration/steps);
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

            yield return new WaitForSeconds(duration/steps);
        }
    }
}
