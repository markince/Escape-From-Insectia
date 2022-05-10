using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration 
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 injuredThrow = new Vector2(0.0f, 18.0f);
    [SerializeField] Vector2 deathThrow   = new Vector2(25.0f, 25.0f);
    [SerializeField] AudioClip playerInjuredSFX = null;
    [SerializeField] AudioClip playerDeadSFX = null;
    [SerializeField] AudioClip playerJump = null;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject InstantDeathPanel;

    // Variables
    int hazardDamageAmount = 10;
    int enemyDamageAmount = 20;
    public static int tutorialLevelWaypoint = 1;
    public static bool showInstantDeathTutorial = false;

    // States
    bool playerIsAlive = true;
    bool facingLeft = false;
    bool invincible = false;

    // Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    float startGravityScale;
    bool playerHasHorizontalSpeed;

    private Material _mat;
    private Color[] _colors = { Color.yellow, Color.red };

    SpriteRenderer[] sprites; // Used in flashing of sprite when injured

    Color startColor; // Original colour of the sprite


    private void Awake()
    {

        this._mat = GetComponent<SpriteRenderer>().material;

    }
    
    // ********************************************************************
    // Start Function - Called once at the start                          *
    // ********************************************************************

    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        startGravityScale = myRigidBody.gravityScale;
        startColor = GetComponent<SpriteRenderer>().material.color;

        // Place the player at the correct position (waypoint) when they die in the tutorial level
        if (GameSession.playedTutorial == false)
        {
            if (tutorialLevelWaypoint == 1)
            {
                transform.position = new Vector2(-8.5f, 3.5f);
            }
            else if (tutorialLevelWaypoint == 2)
            {
                transform.position = new Vector2(7.5f, 71.6f);
            }
        }
        else
        {
            transform.position = new Vector2(-8.5f, 4.0f);
        }


    }

    // ********************************************************************
    // Update Function - called once per frame                            *
    // ********************************************************************
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            FindObjectOfType<GameSession>().ProcessPlayerInstantDeath();

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.position = new Vector2(7.5f, 84.0f);

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.position = new Vector2(29.5f, 60.0f);

        }


        if (!playerIsAlive)
        {
            // Stop the player from moving if dead
            return;
        }

        Run();
        ClimbLadder();
        Jump();
        FlipPlayerSprite();
        InstantDeath();

        // Check for waypoints
        if (transform.position.y > 70.0f)
        {
            tutorialLevelWaypoint = 2;

            if (!showInstantDeathTutorial)
            {
                InstantDeathPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }

        // Call injuered and dead functions
        if (GameSession.playerHealth > 9)
        {
            Injured();
        }
        else if (GameSession.playerHealth <= 9)
        {
            Dead();
        }
    }

    // ********************************************************************
    // Functions                                                          *
    // ********************************************************************

    public AudioSource PlayAudioClipAtPoint(Vector3 position, float spatialBlend, AudioClip audioClip)
    {

        GameObject tempAudioClip = new GameObject("TmpAudio");
        tempAudioClip.transform.position = position;
        AudioSource audio_source = tempAudioClip.AddComponent<AudioSource>();
        audio_source.spatialBlend = spatialBlend;         // Set the spatial blend
        audio_source.clip = audioClip;
        audio_source.Play();
        Destroy(tempAudioClip, audioClip.length); // Destroy the game object after clip has finised playing
        return audio_source;
    }


    private void Run()
    {
        float controlFling = Input.GetAxis("Horizontal"); // value is from -1 to +1
        Vector2 playerVelocity = new Vector2((controlFling / 2.0f) * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;


        // Check to see which direction the player is facing and rotate the
        // firepoint so that the player shoots the gun in the correct direction
        if (myRigidBody.position.x < firePoint.transform.position.x)
        {
            firePoint.transform.rotation = Quaternion.identity;
        }
        else
        {
            firePoint.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }

        // Start the running animation

        // Is the player running?
        playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void ClimbLadder()
    {

        // Is the player not touching the ladder?
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            // Stops the player getting stuck in the climbing animation when leaving the ladder
            myAnimator.SetBool("Climbing", false);

            // When not on the ladder, set gravity scale to normal
            myRigidBody.gravityScale = startGravityScale;

            return;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;

        myRigidBody.gravityScale = 0.0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        // Firstly check too see if the player not touching the ground 
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return; // if so, don't do anything (stops the player double jumping)
        }

        // Else make the player jump
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0.0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;

            PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, playerJump);

        }
    }

    private void Injured()
    {
        // Does the player touch an enemy or a hazard?
        // Is the player touching an enemy?
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "BeeBullet")))
        {
            if (!invincible)
            {
                invincible = true;

                // Fling the player a few yards
                GetComponent<Rigidbody2D>().velocity = injuredThrow;

                // Play sound effect
                PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, playerInjuredSFX);

                StartCoroutine(FlashSprite(0.2f, 0.1f));

                // Scrolling combat text
                CombatTextManager.Instance.CreateText(transform.position, "-" + enemyDamageAmount.ToString(), Color.red);


                FindObjectOfType<GameSession>().ProcessPlayerInjured(enemyDamageAmount);

                Invoke("resetInvulnerability", 1);
            }

        }
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            if (!invincible)
            {
                invincible = true;

                // Fling the player a few yards
                GetComponent<Rigidbody2D>().velocity = injuredThrow;

                // Play sound effect
                PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, playerInjuredSFX);

                StartCoroutine(FlashSprite(0.2f, 0.1f));

                // Scrolling combat text
                CombatTextManager.Instance.CreateText(transform.position, "-" + hazardDamageAmount.ToString(), Color.red);


                FindObjectOfType<GameSession>().ProcessPlayerInjured(hazardDamageAmount);

                Invoke("resetInvulnerability", 1);

            }
        }
    }

    void resetInvulnerability()
    {
        invincible = false;
    }

    private void InstantDeath()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("RisingWater")))
        {
            FindObjectOfType<GameSession>().ProcessPlayerInstantDeath();

        }
    }


    private void Dead()
    {
        // Is the player touching an enemy?
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards", "RisingWater")))
        {
            playerIsAlive = false;
            

            // Run the death animation
            myAnimator.SetTrigger("Dying");

            // Fling the player a few yards
            GetComponent<Rigidbody2D>().velocity = deathThrow;

            StartCoroutine(PauseOnDeath());
        }
    }

    IEnumerator PauseOnDeath()
    {
        PlayAudioClipAtPoint(Camera.main.transform.position, 0.0F, playerDeadSFX);

        yield return new WaitForSeconds(5);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    private void FlipPlayerSprite()
    {

        // Reverses the current scaling of the x axis if the player is moving horizontally
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // Mathf.Epsilon = The smallest value a float can have different from 0

        if (playerHasHorizontalSpeed)
        {
            // Roate the player sprite depending on which button is pressed
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(myRigidBody.velocity.x), transform.localScale.y);
        }

    }

    IEnumerator FlashSprite(float time, float intervalTime)
    {
        float elapsedTime = 0f;
        int index = 0;
        while (elapsedTime < time)
        {
            _mat.color = _colors[index % 2];

            elapsedTime += Time.deltaTime;
            index++;
            yield return new WaitForSeconds(intervalTime);
        }
        GetComponent<SpriteRenderer>().material.color = startColor;

    }


    bool GetPlayerHasHorizontalSpeed()
    {
        return playerHasHorizontalSpeed;
    }
}
