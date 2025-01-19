// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// // Create empty GameObject named "SoundManager"
// // Attach this script:

// public class Sound : MonoBehaviour
// {
//     public static Sound Instance;

//     [Header("Audio Sources")]
//     public AudioSource musicSource;
//     public AudioSource sfxSource;

//     [Header("Sound Effects")]
//     public AudioClip doorOpenSound;
    
//     public AudioClip runeSound;
//     public AudioClip jumpSound;
//     public AudioClip walking; 
//     [Header("Background Music")]
//     public AudioClip backgroundMusic;

//     private float runeSoundCooldown = 2f; // Half second cooldown
//     private float lastRuneTime;

//     // private float walkCooldown = 7f; 
//     // private float lastWalk; 

  


//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//       public void rune()
//     {
//         // Check if enough time has passed since last play
//         if (Time.time >= lastRuneTime + runeSoundCooldown)
//         {
//             if (runeSound != null)
//             {
//                 sfxSource.PlayOneShot(runeSound);
//                 lastRuneTime = Time.time; // Update last play time
//             }
//         }
//     }

//     // public void walk() {
      
//     //         sfxSource.PlayOneShot(walking);
            

        
//     // }
//     public void doorOpen()
//     {
//         if (doorOpenSound != null)
//         {
//             sfxSource.PlayOneShot(doorOpenSound);
//         }
//     }
//     // public void doorClose()
//     // {
//     //     if (doorCloseSound != null)
//     //     {
//     //         sfxSource.PlayOneShot(doorCloseSound, doorCloseVolume);
//     //     }
//     // }
//     public void jump()
//     {
//         if (jumpSound != null)
//         {
//             sfxSource.PlayOneShot(jumpSound);
//         }
//     }

//     public void StartBackgroundMusic()
//     {
//         if (backgroundMusic != null && !musicSource.isPlaying)
//         {
//             musicSource.clip = backgroundMusic;
//             musicSource.loop = true;
//             musicSource.Play();
//         }
//     }
// }