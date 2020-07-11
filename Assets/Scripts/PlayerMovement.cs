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

	public void Move(int _xDirection, int _yDirection)
	{
		rb.velocity = new Vector2(_xDirection * movementSpeed, _yDirection * movementSpeed);
	}

	public void GamePaused()
	{
		rb.velocity = Vector2.zero;
	}
}
