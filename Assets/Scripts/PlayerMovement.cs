using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private float movementSpeed;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private InteractionManager interactionManager;

	[Header("Scriptable Objects")]
	[SerializeField]
	private BoolVariable isPlayerTeleporting;

	[SerializeField]
	private WarpSO currWarp;

	[Header("Events")]
	[SerializeField]
	private GameEvent onWarpTouched;
	[SerializeField]
	private GameEvent onWarpActivated;

	private Vector2 currDirection;

	// Start is called before the first frame update
	void Start()
	{
		currDirection = Vector2.zero;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		InteractableObject interactable = collision.gameObject.GetComponent<InteractableObject>();
		// If the object the player collided with is interactable
		if (interactable)
		{
			// Set it in the Interaction Manager
			interactionManager.SetObjectInContact(interactable);
		}
	}

	public void OnCollisionExit2D(Collision2D collision)
	{
		InteractableObject interactable = collision.gameObject.GetComponent<InteractableObject>();
		// If the object the player stopped colliding with is interactable
		if (interactable)
		{
			// Remove it from the Interaction Manager
			interactionManager.ObjectLostContact();
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		Warp warpItem = collision.gameObject.GetComponent<Warp>();
		if (warpItem)
		{
			// Ignore the collisions with Warps if the player is teleporting
			if (isPlayerTeleporting)
			{
				isPlayerTeleporting.value = false;
				return;
			}

			// Update the last warp touched
			currWarp.warpID = warpItem.GetWarpID();

			// If the warp was inactive
			if (!warpItem.IsWarpActive())
			{
				// TODO: Play Warp Activate sound (Or raise Warp Activate Event so it's handled somewhere else)

				// Activate the warp
				warpItem.Activate();

				// Raise the Warp Activated Event
				onWarpActivated.Raise();
			}

			// TODO: Temporary code, might have to change if animation to the center of the Warp is implemented
			rb.velocity = Vector2.zero;

			// Move player to the center of the Warp (TODO: Use animation instead of instant teleporting)
			transform.position = warpItem.transform.position;

			// TODO: Raise the OnTeleporter Event when the player reaches the center of the teleporter
			// TODO: This will probably be raised from an animation, but it could be something else
			onWarpTouched.Raise();

			return;
		}

		//Stairs stairs = collision.gameObject.GetComponent<Stairs>();
		//// If the object is the stairs
		//if (stairs)
		//{
		//	// Show the "Take the stairs" Menu
		//	stairs.TakeStairs();
		//}

		Upgrade upgrade = collision.gameObject.GetComponent<Upgrade>();
		if (upgrade)
		{
			upgrade.Obtained();
		}
	}

	public void Move(int _xDirection, int _yDirection)
	{
		rb.velocity = new Vector2(_xDirection * movementSpeed, _yDirection * movementSpeed);
	}

	public void GamePaused()
	{
		rb.velocity = Vector2.zero;
	}
}
