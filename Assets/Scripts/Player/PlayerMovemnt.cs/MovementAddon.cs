using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public abstract class MovementAddon : MonoBehaviour
{
	protected PlayerMovement playerMovement;

	protected Animator animator
	{
		get { return playerMovement.animator; }
	}

	protected bool isGrounded
	{
		get { return playerMovement.isGrounded; }
	}

	protected bool isWalking
	{
		get { return playerMovement.isWalking; }
	}

	protected virtual void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
	}

	public float speedMultiplier;

	public virtual void GetMovementForce(ref Vector3 movementForce) { }
}