using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float gravity = 10f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        float h = Input.GetAxis("Horizontal"); 
        float v = Input.GetAxis("Vertical");   

        
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = canMove ? (isRunning ? runSpeed : walkSpeed) : 0f;

        
        moveDirection = (forward * v + right * h).normalized * speed;

        
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        
        characterController.Move(moveDirection * Time.deltaTime);

        
        Vector3 lookDir = new Vector3(moveDirection.x, 0, moveDirection.z);
        if (lookDir.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }
}
