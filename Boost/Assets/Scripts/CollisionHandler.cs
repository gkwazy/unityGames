using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
     [SerializeField] float CrashDelay = 2f;
     [SerializeField] float WinDelay = 2f;
     AudioSource audioSource;
     [SerializeField] AudioClip winAudio;
     [SerializeField] AudioClip loseAudio;

    [SerializeField] ParticleSystem winParticle;
    [SerializeField] ParticleSystem loseParticle;

     bool isTransitioning = false; 
     bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        
        DebugKeys();

    }

    void DebugKeys(){
        if (Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C)){
            collisionDisable = !collisionDisable;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friend bummp");
                break;
            case "Finish":
                Debug.Log("Game Won");
                WinSequence();
                break;
            default:
                Debug.Log("Game Over, You Lose");
                CrashSequence();
                break;
        }
    }


    void WinSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(winAudio);
        winParticle.Play();
        Invoke("LoadNextLevel", WinDelay);
    }

    void CrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(loseAudio);
        loseParticle.Play();
        Invoke("ReloadLevel", CrashDelay);
    }
    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

}
