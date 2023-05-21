using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip runningSound;

    public bool canMove = true;
    
    private bool lookingRight;
    private Vector2 movement;

    private void Update()
    {
        if (!canMove)
        {
            movement = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.sqrMagnitude > 0 && !audioSource.isPlaying)
        {
            PlayRunningSound();
        }
        else if (movement.sqrMagnitude == 0)
        {
            StopRunningSound();
        }
    }

    private void PlayRunningSound()
    {
        audioSource.clip = runningSound;
        audioSource.Play();
    }

    private void StopRunningSound()
    {
        audioSource.Stop();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateAnimation();
        FlipPlayer();
    }

    private void MovePlayer()
    {
        movement = movement.normalized;
        rb.velocity = movement * moveSpeed;
    }

    private void UpdateAnimation()
    {
        anim.SetBool("Running", movement.sqrMagnitude > 0);
    }

    private void FlipPlayer()
    {
        if ((movement.x > 0 && !lookingRight) || (movement.x < 0 && lookingRight))
        {
            lookingRight = !lookingRight;
            transform.localScale = new Vector3(lookingRight ? -1 : 1, 1, 1);
        }
    }
}
