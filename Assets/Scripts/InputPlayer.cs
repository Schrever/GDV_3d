using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private InputActionAsset IAA;
    [SerializeField] private string Mapname = "Player1";
    [SerializeField] private float jumpForce = 5f;

    private InputActionMap IAM;
    private InputAction Jump;
    private InputAction Sprint;
    private InputAction Move;

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded = true;

    void Awake()
    {
        IAM = IAA.FindActionMap(Mapname);
        Jump = IAM.FindAction("Jump");
        Sprint = IAM.FindAction("Sprint");
        Move = IAM.FindAction("Move");
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 moveinput = Move.ReadValue<Vector2>();

        if (Jump.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");
        }

        transform.Translate(transform.forward * moveinput.y * 5f * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up * moveinput.x * 250f * Time.deltaTime, Space.World);

        float speed = (moveinput.magnitude > 0) ? (Sprint.IsPressed() ? 2f : 1f) : 0f;

        animator.SetFloat("Speed", speed);
        animator.SetBool("Grounded", isGrounded);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
