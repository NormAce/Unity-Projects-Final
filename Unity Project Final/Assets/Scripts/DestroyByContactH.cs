using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContactH : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameControllerH gameControllerH;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameControllerH");
        if (gameControllerObject != null)
        {
            gameControllerH = gameControllerObject.GetComponent<GameControllerH>();
        }
        if (gameControllerH == null)
        {
            Debug.Log("Cannot find 'GameControllerH' script");
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
    }
}
