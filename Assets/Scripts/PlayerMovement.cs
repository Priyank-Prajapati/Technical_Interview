using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;

    public float moveSpeed = 5f;
    public float turnTime = 0.1f;
    public float jumpHeight = 2f;
    public float gravity = -10f;

    private CharacterController controller;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    private MeleeAttack meleeAttack;
    private Animator animator;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        meleeAttack = GetComponent<MeleeAttack>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the player grounded
        }

        // Get input for movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Move the player according to the camera angle
        Vector3 direction = new Vector3(moveX, 0f, moveZ).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float forwardAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, forwardAngle, ref turnSmoothVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 forwardDir = Quaternion.Euler(0f, forwardAngle, 0f) * Vector3.forward;
            controller.Move(forwardDir.normalized * moveSpeed * Time.deltaTime);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //check player attack
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("meleeAttack");
            meleeAttack.TriggerAttack();
        }

        if(controller.transform.position.y < -2f)
        {
            FindObjectOfType<GameManager>().GameOver(false);
        }
    }
}
