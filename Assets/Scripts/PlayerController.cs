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

    [Header("Die")]
    private bool isDie = false;
    public Vector3 cameraRotationWhenDie;
    public Vector3 cameraPositionWhenDie;
    public float cameraMoveTime;

    [HideInInspector]
    public bool canLook = true;
    public bool canFire = false;

    private Rigidbody _rigidbody;
    public Animator playerAnimator;

    private bool isCrouch = false;

    private float appliedMoveSpeed;

    public GameObject weaponSwapUIGO;
    private UIWeaponSwap weaponSwapPopupUI;
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

        playerStat.OnDie += Die;
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        appliedMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if(canFire && !isDie)
        {
            playerShooter.Fire();
        }
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
        if (isDie)
        {
            return;
        }
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

        if (SelectPopupPrefab != null && SelectPopupPrefab.activeInHierarchy)
        {
            _rigidbody.velocity = Vector3.zero;
            canLook = false;
            return;
        }
        _rigidbody.velocity = dir;
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

    public void OnShotInput(InputAction.CallbackContext context)
    {
        if (SelectPopupPrefab == null || !SelectPopupPrefab.activeInHierarchy)
        {
            if(context.phase == InputActionPhase.Started)
            {
              canFire = true;
            }
            else if(context.phase == InputActionPhase.Canceled)
            {
              canFire = false;
            }
        }
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
            if(weaponSwapPopupUI == null)
            {
                weaponSwapPopupUI = PopupUIManager.Instance.OpenPopupUI<UIWeaponSwap>();
                weaponSwapUIGO = weaponSwapPopupUI.gameObject;
            }
            weaponSwapUIGO.SetActive(true);
        }
        else if (context.canceled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            canLook = true;
            // 이 부분은 UIManager.ShowUI로 대체해야 함 
            playerShooter.SwapWeapon(weaponSwapPopupUI.curSelectedWeapon);
            weaponSwapUIGO.SetActive(false);
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
                    popupUI.closeButton.onClick.AddListener(ToggleCursor);
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

    private void Die()
    {
        ToggleCursor();
        isDie = true;
        StartCoroutine(CameraMoveWhenDie());
    }

    IEnumerator CameraMoveWhenDie()
    {
        Vector3 startPos = cameraContainer.localPosition;
        Quaternion startRot = cameraContainer.localRotation;
        float time = 0f;
        while (time < cameraMoveTime)
        {
            cameraContainer.localPosition = Vector3.Lerp(startPos, cameraPositionWhenDie, time / cameraMoveTime);
            cameraContainer.localRotation = Quaternion.Lerp(startRot, Quaternion.Euler(cameraRotationWhenDie), time / cameraMoveTime);
            time += Time.deltaTime;
            yield return null;
        }
        PopupUIManager.Instance.OpenPopupUI<UIGameOver>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.down);
    }

    public void ToggleCursor()
    {
        Cursor.lockState = canLook ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !canLook;
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
