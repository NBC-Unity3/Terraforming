using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float crouchMoveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    [HideInInspector] public Vector2 mouseDelta;

    [Header("Fire")]
    public PlayerShooter playerShooter; // 사용할 총
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;
    private Animator playerAnimator;

    private bool isCrouch = false;

    private float appliedMoveSpeed;

    public GameObject weaponSwapUI;
    public GameObject SelectPopupPrefab;

    // 일단은 controller가 instance여서 controller에서 inventory에 접근할 수 있게 함. PlayerManager에서 관리하면 좋을 것 같음
    public PlayerInventory inventory;

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory>();
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        appliedMoveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if(canLook)
        {
            CameraLook();
        }
    }
    
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= appliedMoveSpeed;
        dir.y = _rigidbody.velocity.y;

        if (SelectPopupPrefab != null && SelectPopupPrefab.activeInHierarchy)
        {
            _rigidbody.velocity = Vector3.zero;
            canLook = false;
            return;
        }

        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        _rigidbody.velocity = dir;
        canLook = true;
        playerAnimator.SetFloat("MoveX", curMovementInput.x);
        playerAnimator.SetFloat("MoveY", curMovementInput.y);
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnLookInput(InputAction.CallbackContext context) 
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnimator.SetBool("Jump", true);
            }

        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouch = !isCrouch;
            Debug.Log(isCrouch);
            playerAnimator.SetBool("Crouch", isCrouch);

            if(isCrouch)
            {
                appliedMoveSpeed = crouchMoveSpeed;
            }
            else
            {
                appliedMoveSpeed = moveSpeed;
            }
        }
    }

    public void OnShotInput()
    {
        playerShooter.Fire();
    }

    public void OnReloadInput()
    {
        playerShooter.Reload();
        playerAnimator.SetBool("Reload", true);
    }

    public void OnWeaponSwapInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cursor.lockState = CursorLockMode.None;
            canLook = false;
            UIWeaponSwap popupUI = PopupUIManager.Instance.OpenPopupUI<UIWeaponSwap>();
            weaponSwapUI = popupUI.gameObject;
            weaponSwapUI.SetActive(true);
        }
        else if (context.canceled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            canLook = true;
            // 이 부분은 UIManager.ShowUI로 대체해야 함 
            weaponSwapUI.SetActive(false);
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context) //퀘스트나 회복, 상점 이용을 위한
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("console");
        if (context.phase == InputActionPhase.Started)
        {
            if (Physics.Raycast(ray, out hit, 5f, layerMask))
            {
                if (SelectPopupPrefab == null) 
                {
                    Cursor.lockState = CursorLockMode.None;
                    canLook = false;
                    SelectPopupUI popupUI = PopupUIManager.Instance.OpenPopupUI<SelectPopupUI>();
                    SelectPopupPrefab = popupUI.gameObject;
                }
                Cursor.lockState = CursorLockMode.None;
                SelectPopupPrefab.SetActive(true);
            }
        }
    }


    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (Vector3.up * 0.01f) , Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f)+ (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (Vector3.up * 0.01f), Vector3.down),
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                Debug.Log("true");
                return true;
            }
        }
        Debug.Log("false");

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    private void OnAnimatorIK(int layerIndex) {

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
