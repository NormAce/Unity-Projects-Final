using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactRedH : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameControllerH gameControllerH;
    private PlayerController playerController;

    void Start()
    {
        GameObject gameControllerHObject = GameObject.FindWithTag("GameControllerH");
        if (gameControllerHObject != null)
        {
            gameControllerH = gameControllerHObject.GetComponent<GameControllerH>();
        }
        if (gameControllerH == null)
        {
            Debug.Log("Cannot find 'GameControllerH' script");
        }
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //        if (other.tag == "Boundary" || other.tag == "Enemy")
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameControllerH.GameOver();
        }
        gameControllerH.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
        playerController.fireRate = 0.5f;
    }
}
