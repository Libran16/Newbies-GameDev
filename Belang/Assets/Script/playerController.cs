using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    CameraController cameraController;

    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var moveInput = (new Vector3(h, 0, v)).normalized;

        var moveDir = cameraController.transform.rotation * moveInput;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
