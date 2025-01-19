using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private int jumpCount; // Tracks the number of jumps
    private float speed2;
    private bool canJump = true; 
    private bool inSignZone = false;
    private float moveSpeed = 50f;

    private LeapProvider leapProvider;

   private Stone currentStone; 
   private Stone2 stone2;
   private Stone3 stone3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        body.freezeRotation = true;
        leapProvider = FindObjectOfType<LeapServiceProvider>();
        if (leapProvider == null) {
            Debug.Log("No Leap");
        }

    }

    public void SetCurrentStone(Stone stone)
    {
        currentStone = stone;
        inSignZone = (stone != null);
        Debug.Log(inSignZone ? "Entered stone zone!" : "Left stone zone!");
    }

    public void SetCurrentStone2(Stone2 stone)
    {
        stone2 = stone;
        inSignZone = (stone != null);
        Debug.Log(inSignZone ? "Entered stone zone!" : "Left stone zone!");
    }

    public void SetCurrentStone3(Stone3 stone)
    {
        stone3 = stone;
        inSignZone = (stone != null);
        Debug.Log(inSignZone ? "Entered stone zone!" : "Left stone zone!");
    }
 
       
   
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            inSignZone = true;
            Debug.Log("Entered sign detection zone!");
        }
    }    
     void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stone"))
        {
            inSignZone = false;
            Debug.Log("Left sign detection zone!");
        }
    }

    // Update is called once per frame
     void Update()
    {
        // float horizontalInput = Input.GetAxis("Horizontal");
        speed2 = speed * 1.2f;   
        // body.linearVelocity = n  ew Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        // //flips player when left or right
        // if(horizontalInput > 0.01f)
        //     transform.localScale = new Vector3(7, 7, 1);

        // else if(horizontalInput < -0.01f)
        //     transform.localScale = new Vector3(-7, 7, 1);

        // // Allow jumping if grounded or if jumpCount is less than 2
        // // if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        // // {
        // //     Jump();
        // // }

        Frame frame = leapProvider.CurrentFrame;
       if (frame.Hands.Count > 0)
       {
           
           Hand hand = frame.Hands[0];
           float moveDirection = hand.PalmPosition.x;
   
           body.linearVelocity = new Vector2(moveDirection*moveSpeed, body.linearVelocity.y);
           if (moveDirection< 0 ) {
            transform.localScale = new Vector3(-7, 7, 1);
           }
           else if (moveDirection> 0) {
             transform.localScale = new Vector3(7, 7, 1);
           }
          
           if (inSignZone)
            {
              
                CheckForASign(hand);
                CheckForISign(hand);
                CheckForLSign(hand);
                CheckForESign(hand);
                
            }
        else if (hand.PinchStrength > 0.8f && jumpCount < 2 && canJump)
           {
               body.linearVelocity = new Vector2(body.linearVelocity.x, speed2); // Zero out y velocity
            //    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Sound.Instance.jump();
               jumpCount++;
               canJump = false;  // Prevent multiple jumps from one pinch
               grounded=false; 
           }
           else if (hand.PinchStrength < 0.4f)
           {
               canJump = true;  // Allow another jump when pinch is released
           }
           

        //Set animator parameters
        anim.SetBool("run", moveDirection != 0);
        anim.SetBool("grounded", grounded);
    }
    }

    // private void Jump()
    // {
    //     body.linearVelocity = new Vector2(body.linearVelocity.x, speed2);
    //     jumpCount++; // Increment the jump count
    //     grounded = false; // Player is not grounded after jumping
    // }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovingPlatform")
        {
            grounded = true;
            jumpCount = 0; // Reset the jump count when the player lands
            Sound.Instance.land();
        }
    }
        void CheckForASign(Hand hand)
    {
        // ASL 'A': Fist closed with thumb resting against side
        bool isThumbOut = hand.Thumb.IsExtended;
        bool otherFingersClosed = 
            !hand.Index.IsExtended && 
            !hand.Middle.IsExtended && 
            !hand.Ring.IsExtended && 
            !hand.Pinky.IsExtended;
        bool isFistClosed = hand.GrabStrength > 0.8f;

        if (isThumbOut && otherFingersClosed && isFistClosed)
        {
            Debug.Log("A Sign Detected");
            OnASignDetected();
        }
    }


void CheckForISign(Hand hand)
    {
        if (hand.Pinky.IsExtended &&
            !hand.Thumb.IsExtended &&
            !hand.Index.IsExtended &&
            !hand.Middle.IsExtended &&
            !hand.Ring.IsExtended)
        {
            OnISignDetected();
        }
    }

    void CheckForLSign(Hand hand)
    {
        if (hand.Thumb.IsExtended &&
            hand.Index.IsExtended &&
            !hand.Middle.IsExtended &&
            !hand.Ring.IsExtended &&
            !hand.Pinky.IsExtended)
        {
            OnLSignDetected();
        }
    }

    void CheckForESign(Hand hand)
    {
        if (hand.GrabStrength > 0.8f &&
            !hand.Thumb.IsExtended &&
            !hand.Index.IsExtended &&
            !hand.Middle.IsExtended &&
            !hand.Ring.IsExtended &&
            !hand.Pinky.IsExtended) {
                OnESignDetected();
            }
    }



void OnASignDetected()
{
    Debug.Log("'A' sign detected! Opening door...");
    
}

void OnISignDetected()
    {
        Debug.Log("'I' sign detected! Performing action...");
        if (stone2 != null && stone2.requiredSign == "I")
        {
            stone2.OnCorrectSign();
        }
        
    }

    void OnLSignDetected()
    {
        Debug.Log("'L' sign detected! Performing action...");
        if (currentStone != null && currentStone.requiredSign == "A")
        {
            currentStone.OnCorrectSign();
        }
    }

    void OnESignDetected()
    {
        Debug.Log("'E' sign detected! Performing action...");
        if (stone3 != null && stone3.requiredSign == "E")
        {
            stone3.OnCorrectSign();
        }
    }
}
