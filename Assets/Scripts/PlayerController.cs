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
    public LayerMask groundLayerMask;

    [Header("Jump")]
    public float jumpForce;
    public int jumpSteminaValue;

    [Header("Run")]
    public float runSpeed;
    public int runSteminaValue;
    private bool isRun;

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
    public Animator playerAnimator;

    private bool isCrouch = false;

    private float appliedMoveSpeed;

    public GameObject weaponSwapUI;
    public GameObject SelectPopupPrefab;

    // 일단은 controller가 instance여서 controller에서 inventory에 접근할 수 있게 함. PlayerManager에서 관리하면 좋을 것 같음
    public PlayerInventory inventory;
    public PlayerStat playerStat;

    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        inventory = GetComponent<PlayerInventory>();
        playerStat = GetComponent<PlayerStat>();
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
        if (isRun)
        {
            if(!playerStat.UseStemina(runSteminaValue * Time.deltaTime))
            {
                isRun = false;
            }
            else
            {
                appliedMoveSpeed = runSpeed;
            }
        }
        else if (isCrouch)
        {
            appliedMoveSpeed = crouchMoveSpeed;
        }
        else
        {
            appliedMoveSpeed = moveSpeed;
        }

        dir *= appliedMoveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

        Debug.Log(curMovementInput);

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

            if(curMovementInput.y < 0.5)
            {
                isRun = false;
            }
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isRun = false;
            curMovementInput = Vector2.zero;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && curMovementInput.y >= 0.5)
        {
            isRun = true;
            if (isCrouch)
            {
                isCrouch = !isCrouch;
                playerAnimator.SetBool("Crouch", isCrouch);
                cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, 1.5f, cameraContainer.localPosition.z);
            }
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isRun = false;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            if (IsGrounded() && playerStat.UseStemina(jumpSteminaValue))
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
            playerAnimator.SetBool("Crouch", isCrouch);

            if(isCrouch)
            {
                isRun = false;
                cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, 0.75f, cameraContainer.localPosition.z);
            }
            else
            {
                cameraContainer.localPosition = new Vector3(cameraContainer.localPosition.x, 1.5f, cameraContainer.localPosition.z);
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
        if (context.phase == InputActionPhase.Started)
        {
            if (true) //플레이어 시야에 오브젝트가 있으며, 일정거리 이하일 때 실행되어야함. popupUI 켜져있을 때 움직이면 안됨. 회복버튼 누르면 움직이면 안됨.
            {
                Cursor.lockState = CursorLockMode.None;
                canLook = false;
                //popupUI 삭제 안할거기 때문에 prefab으로 저장이 필요함.
                SelectPopupUI popupUI = PopupUIManager.Instance.OpenPopupUI<SelectPopupUI>();
                SelectPopupPrefab = popupUI.gameObject;
                //카메라 고정도 필요
            }
            SelectPopupPrefab.SetActive(true);
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
