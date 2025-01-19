using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class FinalBoss : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer candle;


    private string[] correctSequence = {"L", "I", "E"}; 
    private List<string> playerSequence = new List<string>(); 
    private float inputCooldown = 1f;
    private float lastTime = 0f;
    private string lastSign = ""; 
    private bool acceptInput = true;
    private bool inZone = false;
    private bool hasFinished = false;
    private LeapProvider leapProvider;



       void Start()
    {
        leapProvider = FindObjectOfType<LeapServiceProvider>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerMovement player = other.GetComponent<PlayerMovement>(); //change players 
            inZone = true; 
            playerSequence.Clear();
            


        }
    }
    // void OnTriggerExit2D(Collider2D other) {
    //     if (other.CompareTag("Player")) {
    //         PlayerMovement player = other.GetComponent<PlayerMovement>(); //change players 
    //         inZone = true; 
    //         playerSequence.Clear();
            


    //     }
    // }

    void Update() {
        if (!inZone || hasFinished) {
            return;
        }
        if (!acceptInput) {
            if (Time.time >= lastTime + inputCooldown) {
                acceptInput = true; 
                lastSign="";
                
            }
            
            return;
        }

        Frame frame = leapProvider.CurrentFrame;

        if (frame.Hands.Count > 0) {
            Hand hand = frame.Hands[0];

            string currentSign = null;

            if (isLSign(hand)) {
                currentSign = "L";
            }
            else if (isISign(hand)) {
                currentSign = "I";
            }
            else if (isESign(hand)) {
                currentSign = "E";
            }

            if (currentSign != null && currentSign != lastSign)
            {
                lastSign = currentSign;
                AddToSequence(currentSign);
            }
        }
    }

    void AddToSequence(string sign)
    {
        playerSequence.Add(sign);
        Debug.Log($"Added {sign} to sequence. Current sequence: {string.Join(", ", playerSequence)}");
        
        // Start cooldown
        acceptInput = false;
        lastTime = Time.time;

        // Check if the sequence is correct so far
        bool isCorrectSoFar = true;
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (i >= correctSequence.Length || playerSequence[i] != correctSequence[i])
            {
                isCorrectSoFar = false;
                break;
            }
        }

        if (!isCorrectSoFar)
        {
            Debug.Log("Wrong sequence! Resetting...");
            ResetSequence();
            return;
        }

        if (playerSequence.Count == correctSequence.Length)
        {
            Debug.Log("Sequence Complete!");
            hasFinished = true;
            OnSequenceComplete();
        }
    }

    void ResetSequence () {
        playerSequence.Clear();
    }

    void OnSequenceComplete() {
        Debug.Log("YAYYYYYYYY");
        //sound??
        candle.GetComponent<BoxCollider2D> ().enabled = false;
    }

    bool isLSign(Hand hand) {
        return 
        (hand.Thumb.IsExtended &&
            hand.Index.IsExtended &&
            !hand.Middle.IsExtended &&
            !hand.Ring.IsExtended &&
            !hand.Pinky.IsExtended);
       
    }
    bool isISign(Hand hand) {
        return (hand.Pinky.IsExtended &&
            !hand.Thumb.IsExtended &&
            !hand.Index.IsExtended &&
            !hand.Middle.IsExtended &&
            !hand.Ring.IsExtended);
        
    }

    bool isESign(Hand hand) {
        return (hand.GrabStrength > 0.8f &&
            !hand.Thumb.IsExtended &&
            !hand.Index.IsExtended &&
            !hand.Middle.IsExtended &&
            !hand.Ring.IsExtended &&
            !hand.Pinky.IsExtended);
    }
}
       