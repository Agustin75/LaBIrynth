using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BicrossSquare : MonoBehaviour
{
	[SerializeField]
	// TODO: Might change when implementing drag behavior
	private Button squareButton;

	[SerializeField]
	private Image feedbackImage;

	[SerializeField]
	private SquareState currentFillType;

	[SerializeField]
	protected SpritesHolder spritesHolder;

	private BicrossSquareState squareState;

    // Start is called before the first frame update
    void Start()
    {
		squareState = BicrossSquareState.Undecided;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SquarePressed()
	{
		// TODO: Modify so it checks when the mouse moves into this Square AND if it's being held down, to allow to drag and fill

		if (squareState != BicrossSquareState.Undecided)
		{
			squareState = BicrossSquareState.Undecided;

			// Update Square graphics to show no Sprite
			feedbackImage.gameObject.SetActive(false);
			feedbackImage.sprite = null;
		}
		else
		{
			// Update squareState with the corresponding State
			squareState = currentFillType;

			// Update Square graphics to show the corresponding Sprite
			feedbackImage.sprite = spritesHolder.GetSquareSprite(squareState);
			feedbackImage.gameObject.SetActive(true);
		}
	}

	// Returns whether the Square is Filled, Empty or Undecided
	public BicrossSquareState GetSquareContent()
	{
		return squareState;
	}

	// Prevents the player from pressing the square once the puzzle is solved
	public void DisableButton()
	{
		squareButton.interactable = false;
	}

	// NOTE: Used for the Puzzle Editor, not to be used in-game
	public void SetSquareState(BicrossSquareState _state)
	{
		squareState = _state;
		feedbackImage.sprite = spritesHolder.GetSquareSprite(squareState);
		feedbackImage.gameObject.SetActive(true);
	}
}
