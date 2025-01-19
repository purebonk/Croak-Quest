// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine;
// using Leap;  // For Leap Motion classes


// public class PlayerS : MonoBehaviour
// {
//    private float moveSpeed = 70f;
//    private float jumpForce = 20f;
//    private int jumpCount = 0;
//    private int maxJumps = 2;
//    private bool canJump = true;  // Add debounce for jumping
//    private bool inSignZone = false;
//    private bool isDebugging = true; 



//    private Rigidbody2D rb;
//    private LeapProvider leapProvider;
//    private HandPoseDetector poseDetector;
//    private Stone currentStone; 


//    void Start()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        leapProvider = FindObjectOfType<LeapServiceProvider>();
//    }

// public void SetCurrentStone(Stone stone)
//     {
//         currentStone = stone;
//         inSignZone = (stone != null);
//         Debug.Log(inSignZone ? "Entered stone zone!" : "Left stone zone!");
//     }

//    void OnCollisionEnter2D(Collision2D collision)
//    {
//     if (collision.gameObject.CompareTag("Ground")) {
//         jumpCount = 0;  
//     }
       
//    }
//    void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Stone"))
//         {
//             inSignZone = true;
//             Debug.Log("Entered sign detection zone!");
//         }
//     }
//      void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Stone"))
//         {
//             inSignZone = false;
//             Debug.Log("Left sign detection zone!");
//         }
//     }


//    void Update()
//    {    
        
//        Frame frame = leapProvider.CurrentFrame;
//        if (frame.Hands.Count > 0)
//        {
           
//            Hand hand = frame.Hands[0];
//            float moveDirection = hand.PalmPosition.x;
//            rb.linearVelocity = new Vector2(moveDirection*moveSpeed, body.linearVelocity.y);
//            if (moveDirection< 0 ) {
//             transform.localScale = new Vector3(-7, 7, 1);
//            }
//            else if (moveDirection> 0) {
//              transform.localScale = new Vector3(7, 7, 1);
//            }
          
//            if (inSignZone)
//             {
              
//                 CheckForASign(hand);
//                 CheckForISign(hand);
//                 CheckForLSign(hand);
//             }
     
          
          
//            else if (hand.PinchStrength > 0.8f && jumpCount < maxJumps && canJump)
//            {
//                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Zero out y velocity
//                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
//                 Sound.Instance.jump();
//                jumpCount++;
//                canJump = false;  // Prevent multiple jumps from one pinch
//            }
//            else if (hand.PinchStrength < 0.4f)
//            {
//                canJump = true;  // Allow another jump when pinch is released
//            }
           
//        }
//    }


//     void CheckForASign(Hand hand)
//     {
//         // ASL 'A': Fist closed with thumb resting against side
//         bool isThumbOut = hand.Thumb.IsExtended;
//         bool otherFingersClosed = 
//             !hand.Index.IsExtended && 
//             !hand.Middle.IsExtended && 
//             !hand.Ring.IsExtended && 
//             !hand.Pinky.IsExtended;
//         bool isFistClosed = hand.GrabStrength > 0.8f;

//         if (isThumbOut && otherFingersClosed && isFistClosed)
//         {
//             Debug.Log("A Sign Detected");
//             OnASignDetected();
//         }
//     }


// void CheckForISign(Hand hand)
//     {
//         if (hand.Pinky.IsExtended &&
//             !hand.Thumb.IsExtended &&
//             !hand.Index.IsExtended &&
//             !hand.Middle.IsExtended &&
//             !hand.Ring.IsExtended)
//         {
//             OnISignDetected();
//         }
//     }

//     void CheckForLSign(Hand hand)
//     {
//         if (hand.Thumb.IsExtended &&
//             hand.Index.IsExtended &&
//             !hand.Middle.IsExtended &&
//             !hand.Ring.IsExtended &&
//             !hand.Pinky.IsExtended)
//         {
//             OnLSignDetected();
//         }
//     }



// void OnASignDetected()
// {
//     Debug.Log("'A' sign detected! Opening door...");
//     if (currentStone != null && currentStone.requiredSign == "A")
//         {
//             currentStone.OnCorrectSign();
//         }
// }

// void OnISignDetected()
//     {
//         Debug.Log("'I' sign detected! Performing action...");
        
//     }

//     void OnLSignDetected()
//     {
//         Debug.Log("'L' sign detected! Performing action...");
//     }

// }