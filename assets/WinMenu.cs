using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu; // Reference to the Restart menu
    public GameObject player; // Reference to the player GameObject
    private Vector3 spawnPosition; // Position to teleport the player back to

    private void Start()
    {
        // Save the player's initial position as the spawn point
        if (player != null)
        {
            spawnPosition = player.transform.position;
        }
        else
        {
            Debug.LogError("Player reference not assigned!");
        }

        winMenu.SetActive(false);
    }

    public void quit()
    {
        Debug.Log("Quit button clicked");
        Application.Quit(); // Quit the application
    }

    public void playAgain()
    {
        Debug.Log("Play Again button clicked");

        // Hide the win menu
        winMenu.SetActive(false);

        // Reset the player's position to the spawn point
        if (player != null)
        {
            player.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError("Player reference not assigned!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             Sound.Instance.startWinMusic();
            winMenu.SetActive(true);
            Cursor.visible = true;
        }
    }
}
