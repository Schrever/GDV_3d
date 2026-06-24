using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacterController : MonoBehaviour
{
    public InputActionAsset inputAsset;
    public string mapName = "Player1";
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float rotationSpeed = 150f;
    public float jumpHeight = 2f;
    public float gravity = -20f;


    private InputAction moveAction;
    private InputAction sprintAction;
    private InputAction jumpAction;

    private CharacterController characterController;
    private Animator animator;
    private float verticalVelocity;
    

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        InputActionMap map  = inputAsset.FindActionMap(mapName);
        moveAction          = map.FindAction("Move");
        sprintAction        = map.FindAction("Sprint");
        jumpAction          = map.FindAction("Jump");
    }

    void OnEnable()  { inputAsset.FindActionMap(mapName).Enable(); }
    void OnDisable() { inputAsset.FindActionMap(mapName).Disable(); }

    void Update()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();

        float speed = movementInput.y * moveSpeed;
        // Sprint
        if (sprintAction.IsPressed())
            speed *= 2;

        // Voorwaartse beweging wordt later verwerkt in de Move methode
        Vector3 move = transform.forward * speed * Time.deltaTime;

        // Rotatie (links/rechts draaien)
        transform.Rotate(Vector3.up * movementInput.x * rotationSpeed * Time.deltaTime);

        // Zwaartekracht en springen
        if (characterController.isGrounded)
        {

            Debug.Log("on ground");
            verticalVelocity = -1f; // kleine downward force om grounded te blijven

            if (jumpAction.WasPressedThisFrame())
            {

                Debug.Log("jump");
                // Sprong-formule: v = sqrt(2 * |g| * h)
                verticalVelocity = Mathf.Sqrt(2f * Mathf.Abs(gravity) * jumpHeight);
                animator.SetTrigger("Jump");
            }
        }
        else
        {
            // Niet op de grond: zwaartekracht toepassen
            verticalVelocity += gravity * Time.deltaTime;
        }


        //verticale snelheid meegeven via de move vector
        move.y = verticalVelocity * Time.deltaTime;

        characterController.Move(move);

        // Animator aansturen voor rennen en landen
        animator.SetFloat("Speed", movementInput.y);
        animator.SetBool("Grounded", characterController.isGrounded);
    }
}