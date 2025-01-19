using UnityEngine;

public class finalChallengeCollide : MonoBehaviour
{
    public GameObject tutorialMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialMessage.SetActive(false);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialMessage.SetActive(true);
           
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialMessage.SetActive(false);
        }
    }
}
