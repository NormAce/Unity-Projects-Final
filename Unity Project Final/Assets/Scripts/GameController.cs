using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text modeText;
    private bool gameOver;
    private bool restart;
    private bool mode;
    private int score;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    private Particles particles;
    private BGScroller bGScroller;
    private Particles2 particles2;
    void Start()
    {
        gameOver = false;
        restart = false;
        mode = false;
        restartText.text = "";
        gameOverText.text = "";
        modeText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
        musicSource.clip = musicClipOne;
        musicSource.Play();
        GameObject particlesObject = GameObject.FindWithTag("Particles");
        if (particlesObject != null)
        {
            particles = particlesObject.GetComponent<Particles>();
        }
        if (particles == null)
        {
            Debug.Log("Cannot find 'Particles' script");
        }
        GameObject bGScrollerObject = GameObject.FindWithTag("BGScroller");
        if (bGScrollerObject != null)
        {
            bGScroller = bGScrollerObject.GetComponent<BGScroller>();
        }
        if (particles == null)
        {
            Debug.Log("Cannot find 'BGScroller' script");
        }
        GameObject particles2Object = GameObject.FindWithTag("Particles2");
        if (particlesObject != null)
        {
            particles2 = particles2Object.GetComponent<Particles2>();
        }
        if (particles == null)
        {
            Debug.Log("Cannot find 'Particles2' script");
        }
    }
    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
        if (mode)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                SceneManager.LoadScene("Space Shooter Hard Mode");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'Z' for Restart";
                restart = true;
                modeText.text = "Press 'H' for Hard Mode";
                mode = true;
                break;
            }
        }
    }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            gameOverText.text = "You win! Game created by Cameron Brito.";
            gameOver = true;
            restart = true;
            mode = true;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            particles.hSliderValue = 100.0f;
            bGScroller.scrollSpeed = -25f;
            particles2.hSliderValue = 100.0f;
        }
    }
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        musicSource.clip = musicClipThree;
        musicSource.Play();
    }
}
