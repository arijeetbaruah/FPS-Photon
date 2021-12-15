using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class PlayerController : MonoBehaviour, IPunObservable
{
    [SerializeField] private float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Item[] items;

    private bool grounded;
    private Vector3 smoothMoveVelocity;
    private Vector3 moveAmount;
    private float verticalLookRotation;
    private int itemIndex;
    private int previousitemIndex = -1;

    private PhotonView pv;
    private Rigidbody rb;

    public bool IsGrounded
    {
        set
        {
            grounded = value;
        }
        get
        {
            return grounded;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (pv.IsMine)
        {
            EquipItem(0);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    private void Update()
    {
        if (!pv.IsMine)
        {
            return;
        }

        Look();
        Move();
        Jump();
        ItemEquipControl();

        if (Input.GetAxis("Fire1") != 0)
        {
            items[itemIndex].Use();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ((Gun)items[itemIndex])?.Reload();
        }
    }

    private void ItemEquipControl()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown($"{i + 1}"))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (itemIndex == items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (itemIndex == 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    private void EquipItem(int _index)
    {
        if (_index == previousitemIndex) return;

        itemIndex = _index;
        items[itemIndex].gameObject.SetActive(true);
        items[itemIndex].UpdateUI();

        if (previousitemIndex != -1)
        {
            items[previousitemIndex].gameObject.SetActive(false);
        }

        previousitemIndex = itemIndex;
    }

    private void Jump()
    {
        if (Input.GetAxis("Jump") != 0 && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, dir * (Input.GetAxis("Fire3") != 0 ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(itemIndex);
        }
        else
        {
            itemIndex = (int) stream.ReceiveNext();
            EquipItem(itemIndex);
        }
    }
}
