using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float backwardSpeed = 3.5f;
    public float rotationSpeed = 120f;

    [Header("Refs")]
    public Transform cameraTransform;

    CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical"); 

        Vector3 forwardCam = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 rightCam = Vector3.Scale(cameraTransform.right, new Vector3(1, 0, 1)).normalized;

        if (h != 0f)
        {
            transform.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime);
        }

        Vector3 move = Vector3.zero;

        if (v > 0f)
        {
            Vector3 dir = transform.forward;
            move = dir * speed * Time.deltaTime;
        }
        
        else if (v < 0f)
        {
            Vector3 dir = -transform.forward;
            move = dir * backwardSpeed * Time.deltaTime;
        }

        cc.Move(move);
    }
}
