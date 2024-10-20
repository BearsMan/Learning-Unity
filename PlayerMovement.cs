using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float crawlSpeed = 1f;
    public float crouchSpeed = 2f;
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    private Vector3 velocity;
    public float jumpHeight;
    public bool isRunning = false;
    public bool crouched = false;
    public bool prone = false;
    public bool CanControl
    {
        get
        {
            if (HUD.Instance.conversationScreen.activeSelf)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    public Animator anim;
    public bool grounded;
    public LayerMask groundMask;
    public CharacterController body;
    public Transform cameraSystem;
    public Transform head;
    public float currentSpeed
    {
        get
        {
            if (prone)
            {
                return crawlSpeed;
            }
            if (crouched)
            {
                return crouchSpeed;
            }
            return isRunning ? runSpeed : walkSpeed;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanControl)
        {
            return;
        }
        Interaction();
        grounded = Physics.CheckSphere(transform.position + (transform.up * (body.radius - 0.1f)), body.radius, groundMask);
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (crouched)
            {
                TryUncrouch();
            }
            else
            {
                if (prone)
                {
                    float distance = 1.5f;
                    if (!Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector2.up, distance))
                    {
                        prone = false;
                    }
                }
                crouched = true;
            }
        }

        if (isRunning && crouched)
        {
            TryUncrouch();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (prone)
            {

                float distance = 1.8f;
                if (!Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector2.up, distance))
                {
                    prone = false;
                    crouched = false;
                }
            }
            else
            {
                // crouched = false;
                prone = true;
            }
        }
       
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded && !prone && !crouched)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
        }

        UpdateAnimations();
        UpdateHeight();
    }

    private void TryUncrouch()
    {
        float distance = 1.8f;
        if (!Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector2.up, distance))
        {
            crouched = false;
            prone = false;
        }
    }

    public void UpdateAnimations()
    {
        Vector3 velo = velocity;
        velo.y = 0;
        anim.SetBool("Crouching", crouched);
        anim.SetFloat("SpeedForward", velo.magnitude);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Prone", prone);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = cameraSystem.forward * z + cameraSystem.right * x;

        body.Move(move * currentSpeed * Time.deltaTime);

        velocity.y += Physics.gravity.y * Time.deltaTime;
        body.Move(velocity * Time.deltaTime);

        velocity.x = move.x;
        velocity.z = move.z;

        if (move.normalized != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(move.normalized, Vector3.up);
    }

    private void UpdateHeight()
    {
        float distance = head.position.y - transform.position.y + 0.2f;
        body.height = distance;
        body.center = Vector3.up * ((distance / 2));
    }

    private IInteractable currentInteractable;
    public void Interaction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5f))
        {
            //Debug.Log("Hovering over " + hit.transform.name);
            if (hit.transform.TryGetComponent(out IInteractable other))
            {
                if (currentInteractable != other)
                {
                    currentInteractable?.StopHover();
                    currentInteractable = other;
                    currentInteractable.StartHover();
                }
                if (other.CanInteract(true))
                {
                    HUD.Instance.SetCursorType(HUD.CursorTypes.active);
                    if (Input.GetMouseButtonDown(0))
                    {
                        other.Interact(this);
                    }
                    return;
                }
                else
                {
                    HUD.Instance.SetCursorType(HUD.CursorTypes.notActive);
                    return;
                }
            }
        }
        HUD.Instance.SetCursorType(HUD.CursorTypes.standard);
        currentInteractable?.StopHover();
        currentInteractable = null;
    }
}
