using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 8.5f;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform holdPoint;

    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    public event EventHandler OnPickupSomething;
    public event EventHandler OnDropSomething;
    public event EventHandler<OnSeclectedCounterChangedEventArgs> OnSeclectedCounterChanged;
    public class OnSeclectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    private void Awake()
    {
        //Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are more than one Player Instance!");
        }
    }


    private void Start()
    {
        inputManager.OnInteractAction += InputManager_OnInteractAction;
        inputManager.OnInteractAlternateAction += InputManager_OnInteractAlternateAction;
    }

    private void InputManager_OnInteractAlternateAction(object sender, EventArgs e)
    {
        //InteractAlternate of selectedCounter
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get input
        Vector2 inputMove = inputManager.GetPlayerInputMovement();
        isWalking = inputMove != Vector2.zero;

        //Debug.Log(inputMove.x.ToString() + "--" + inputMove.y.ToString() );

        Movement(inputMove);
        Interact(inputMove);
    }

    public bool IsWalking { get {  return isWalking; } }

    //Player interact
    private void Interact(Vector2 inputMove)
    {
        Vector3 interactDir = new Vector3(inputMove.x, 0f, inputMove.y);
        float raycastDistance = 2f;

        if (interactDir != Vector3.zero)
        {
            lastInteractDir = interactDir;
        }

        //Raycast to process interact
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, raycastDistance, counterLayerMask))
        {
            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    ChangedSelectedCounter(baseCounter);
                }
            }
            else
            {
                ChangedSelectedCounter(null);
            }
        }
        else
        {
            ChangedSelectedCounter(null);
        }
    }

    //Player movement
    private void Movement(Vector2 inputMove)
    {
        if (isWalking)
        {
            //Move Player
            Vector3 moveDir = new Vector3(inputMove.x, 0f, inputMove.y);
            float playerHeight = 2f;
            float playerRadius = 0.7f;
            float moveDistance = moveSpeed * Time.deltaTime;

            bool canMove = !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDir, moveDistance);
            if (canMove)
            {
                transform.position += moveDir * moveDistance;
            }
            else
            {
                //Case: Only move to X
                moveDir = new Vector3(inputMove.x, 0f, 0f); //moveDirX

                canMove = (moveDir.x > 0.7f || moveDir.x < -0.7f) && !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDir, moveDistance);
                if (canMove)
                {
                    transform.position += moveDir * moveDistance;
                }
                else
                {
                    //Case: Only move to Z
                    moveDir = new Vector3(0f, 0f, inputMove.y); //moveDirZ
                    canMove = (moveDir.z > 0.7f || moveDir.z < -0.7f)&& !Physics.CapsuleCast(transform.position, transform.position + transform.up * playerHeight, playerRadius, moveDir, moveDistance);
                    if (canMove)
                    {
                        transform.position += moveDir * moveDistance;
                    }
                }
            }

            //Rotation Player
            if (moveDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), rotateSpeed * Time.deltaTime);
            }
        }
    }

    private void ChangedSelectedCounter(BaseCounter baseCounter)
    {
        selectedCounter = baseCounter;

        OnSeclectedCounterChanged?.Invoke(this, new OnSeclectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetHoldPoint() { return holdPoint; }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void SetKitchenObject(KitchenObject kitchenObject) 
    {
        this.kitchenObject = kitchenObject;

        //Handle Event
        OnPickupSomething?.Invoke(this, EventArgs.Empty);
    }

    public void ClearKitchenObject() 
    { 
        this.kitchenObject = null; 

        //Handle Event
        OnDropSomething?.Invoke(this, EventArgs.Empty); 
    }

    public bool HasKitchenObject() { return kitchenObject != null; }

    public bool GetIsWalking()
    {
        return isWalking;
    }
}
