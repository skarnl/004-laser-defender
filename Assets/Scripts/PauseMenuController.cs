using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    private GameSession gameSession;

    AudioClip slowDownClip;
    AudioClip speedUpClip;

    GameSpeedController gameSpeedController;

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
        gameSpeedController = FindObjectOfType<GameSpeedController>();
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
        AudioSource.PlayClipAtPoint(slowDownClip, Camera.main.transform.position);

        gameSpeedController.SlowDownGame();
    }

    void SpeedUpGame() {
        AudioSource.PlayClipAtPoint(speedUpClip, Camera.main.transform.position);

        gameSpeedController.SpeedUpGame();
    }
}
