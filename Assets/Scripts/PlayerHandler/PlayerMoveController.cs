using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private GameSettingsScriptableObject gameSettings;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cam;
    private Vector3 playerVelocity;
    private float playerSpeed;
    private float turnSmoothTime;
    private float turnSmoothVelocity;
    private float jumpHeight;
    private float gravityValue;
    private bool groundedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        // default values
        turnSmoothTime = 0.1f;
        playerSpeed = 6f;
        jumpHeight = 1f;
        gravityValue = -9.81f;
    }
    // Update is called once per frame
    void Update()
    {
        // get input speed from web browser
        if (gameSettings.playerSpeed != 0) 
            playerSpeed = gameSettings.playerSpeed;
        
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            animator.SetBool("isJumping", false);
        }
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        // we take vertical axis for z movement (i.e. "w" and "s")
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;
        
        // Enable Idle Animation
        if (move.magnitude == 0f && groundedPlayer)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isStationary", true);
        }

        // Enable movements and runnig animation
        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection.normalized * Time.deltaTime * playerSpeed);
            animator.SetBool("isStationary", false);
        }

           
        // Changes the height position of the player and enables jumping animation
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            if (!animator.GetBool("isStationary")) 
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            animator.SetBool("isJumping", true);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
    }
}