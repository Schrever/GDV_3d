using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] private InputActionAsset IAA;
    [SerializeField] private string Mapname = "Player1";
    private InputActionMap IAM;
    private InputAction Jump;
    private InputAction Sprint;
    private InputAction Move;

    void Awake()
    {
        IAM = IAA.FindActionMap(Mapname);
        Jump = IAM.FindAction("Jump");
        Sprint = IAM.FindAction("Sprint");
        Move = IAM.FindAction("Move");

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Jump.WasPressedThisFrame())
        {
            //
        }
        else if (Jump.IsPressed())
        {
            //
        }
        else if (Jump.WasReleasedThisFrame())
        {
            //
        }

        if (Sprint.IsPressed())
        {
            //
        }

        Vector2 moveinput = Move.ReadValue<Vector2>();

        transform.Translate(transform.forward * moveinput.y * 5f * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up * moveinput.x * 100f * Time.deltaTime, Space.World);
    }
}
