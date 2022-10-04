using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    [Header("Obstacle System")]
    public Transform startPoint;
    public Transform endPoint;
    public GameObject[] obstacles; //Objects that contains different obstacle types which will be randomly activated
    [Header("restart Game")]
    [Tooltip("How long to wait before restarting the game")] 
    public float waitTime = 2.0f; 
    private void OnCollisionEnter(Collision collision) 
    { 
        // First check if we collided with the player 
        if (collision.gameObject.GetComponent<PlayerControllerA2>())
        { 
            // Destroy the player 
            Destroy(collision.gameObject); 
            // Call the function ResetGame after waitTime 
            // has passed 
            Invoke("ResetGame", waitTime); 
        } 
    } 

    /// <summary> 
    /// Will restart the currently loaded level 
    /// </summary> 
    private void ResetGame() 
    { 
        // Restarts the current level 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    } 
    

    public void ActivateRandomObstacle()
    {
        DeactivateAllObstacles();

        System.Random random = new System.Random();
        int randomNumber = random.Next(0, obstacles.Length);
        obstacles[randomNumber].SetActive(true);
    }

    public void DeactivateAllObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
}
