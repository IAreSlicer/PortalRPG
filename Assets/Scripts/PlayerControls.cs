using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float rotateSpeed = 0.8f;
    public float gravity = 20.0f;
    public float velocity = 0f;

    Vector3 previous = new Vector3(0, 0, 0);

    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;
    private Transform playerCamera;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().transform;
    }
    

    void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        print(velocity);

        if (transform.rotation.x != 0) 
        {
            float tmp = transform.rotation.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, tmp, 0, 1), 0.85f);
        }
        
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);

        playerCamera.Rotate(-Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);
        if (playerCamera.localRotation.eulerAngles.y != 0)
        {
            playerCamera.Rotate(Input.GetAxis("Mouse Y") * rotateSpeed, 0, 0);
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal") * speed, moveDirection.y, Input.GetAxis("Vertical") * speed);
        moveDirection = transform.TransformDirection(moveDirection);

        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump")) moveDirection.y = jumpSpeed;
            else moveDirection.y = 0;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
