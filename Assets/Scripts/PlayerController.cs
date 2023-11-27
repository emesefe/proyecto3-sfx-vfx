using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody; // = null
    private float forceMagnitude = 7.5f;

    private bool isOnTheGround;
    public bool isGameOver;

    private Animator playerAnimator;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

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
        if (collision.gameObject.CompareTag("Ground")) // Hemos colisionado con el suelo
        {
            isOnTheGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle")) // Hemos colisionado con un obstáculo
        {
            Debug.Log("GAME OVER");
            isGameOver = true;
        }
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * forceMagnitude, 
            ForceMode.Impulse);
        isOnTheGround = false;
        
        // Animación de salto
        playerAnimator.SetTrigger("Jump_trig");
    }
}
