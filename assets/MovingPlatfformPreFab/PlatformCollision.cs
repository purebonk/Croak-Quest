using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    public Transform objectToReparent;
    public Transform newParent;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToReparent.SetParent(newParent);
            Debug.Log("touch");
        }
        
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToReparent.SetParent(null);
        }
    }
}
