using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region CONSTANTS
    private const string OBSTACLE_TAG = "Obstacle";
    private const string GROUND_TAG = "Ground";

    private const string JUMP_TRIG = "Jump_trig";
    private const string DEATH_BOOL = "Death_b";
    private const string DEATH_TYPE_INT = "DeathType_int";
    #endregion
    
    private Rigidbody playerRigidbody; // = null
    private float forceMagnitude = 7.5f;

    private bool isOnTheGround;
    public bool isGameOver;

    private Animator playerAnimator;

    [SerializeField] private ParticleSystem deathParticleSystem;
    [SerializeField] private ParticleSystem dirtParticleSystem;

    private AudioSource playerAudioSource;
    [SerializeField] private AudioClip jumpClip;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();

        isOnTheGround = true;
        isGameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround && !isGameOver)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG)) // Hemos colisionado con el suelo
        {
            isOnTheGround = true;
            dirtParticleSystem.Play();
        }

        if (collision.gameObject.CompareTag(OBSTACLE_TAG)) // Hemos colisionado con un obstáculo
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        isGameOver = true;
        
        // Animación Muerte
        int randomDeath = Random.Range(1, 3);
        playerAnimator.SetBool(DEATH_BOOL, true);
        playerAnimator.SetInteger(DEATH_TYPE_INT, randomDeath);
        
        // Sistema de partículas
        deathParticleSystem.Play();
        dirtParticleSystem.Stop();
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * forceMagnitude, 
            ForceMode.Impulse);
        isOnTheGround = false;
        
        // Animación de salto
        playerAnimator.SetTrigger(JUMP_TRIG);
        
        // Sistema de partículas
        dirtParticleSystem.Stop();
    }
}
