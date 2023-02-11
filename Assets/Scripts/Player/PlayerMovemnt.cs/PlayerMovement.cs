using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	#region variables
	//Refs
	[HideInInspector] public Rigidbody rb;
	[SerializeField] Transform neck;
	MovementAddon[] addons;

	//Controls (for unity events)
	[HideInInspector] public bool playerCanMove = true;
	[HideInInspector] public bool cameraCanMove = true;
	public void SetCameraCanMove(bool cameraCanMove) { this.cameraCanMove = cameraCanMove; }
	public void SetPlayerCanMove(bool playerCanMove) { this.playerCanMove = playerCanMove; }

	//States
	[HideInInspector] public bool isWalking = false;
	[HideInInspector] public bool isGrounded = false;

	//User Input vars
	[HideInInspector] public Vector3 moveInput = Vector3.zero;
	[HideInInspector] public Vector3 lookInput = Vector3.zero; //Used in Swayer

	//Camera
	public float mouseSensitivity = 2f;
	[SerializeField] float maxLookAngle = 50f;
	float yaw = 0.0f;
	float pitch = 0.0f;

	//Speeds
	[SerializeField] float walkSpeed = 5f;
	[SerializeField] float maxVelocityChange = 10f;

	public Animator animator;

	//Grounded
	public bool IsGrounded() { return isGrounded; }
	public float maxRaycastDist = 1;


	#endregion

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		addons = GetComponents<MovementAddon>();
	}
	void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void Update()
	{
		CheckGround();
		if (playerCanMove) UpdateMovement();
		if (cameraCanMove) UpdateCamera();
	}

	void UpdateMovement()
	{
		moveInput = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical")
		).normalized;

		animator.SetFloat("Walk", moveInput.magnitude);
	}

	void UpdateCamera()
	{
		lookInput = new Vector3(
			Input.GetAxis("Mouse X"),
			0,
			Input.GetAxis("Mouse Y")
		);

		yaw = transform.localEulerAngles.y + lookInput.x * mouseSensitivity;
		pitch -= mouseSensitivity * lookInput.z;

		// Clamp pitch between lookAngle
		pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);
		transform.localEulerAngles = new Vector3(0, yaw, 0);
		neck.transform.localEulerAngles = new Vector3(pitch, 0, 0);
	}

	void FixedUpdate()
	{
		bool wantsToMove = moveInput.x != 0 || moveInput.z != 0;
		isWalking = wantsToMove && playerCanMove && isGrounded;

		Vector3 targetVelocity = transform.TransformDirection(moveInput) * walkSpeed;

		foreach (MovementAddon addon in addons)
		{
			addon.GetMovementForce(ref targetVelocity);
		}

		Vector3 velocityDifference = (targetVelocity - rb.velocity);
		Vector3 velocityChange = new Vector3(
			Mathf.Clamp(velocityDifference.x, -maxVelocityChange, maxVelocityChange),
			0,
			Mathf.Clamp(velocityDifference.z, -maxVelocityChange, maxVelocityChange)
		);

		rb.AddForce(velocityChange, ForceMode.VelocityChange);
	}

	void CheckGround()
	{
		Vector3 origin = transform.position + Vector3.up * 0.001f;
		Vector3 direction = transform.TransformDirection(Vector3.down);

		if (Physics.Raycast(origin, direction, out RaycastHit hit, maxRaycastDist))
		{
			Debug.DrawRay(origin, direction * hit.distance, Color.red);
			isGrounded = true;
		}
		else
		{
			Debug.DrawRay(origin, direction * maxRaycastDist, Color.red);
			isGrounded = false;
		}
	}
}