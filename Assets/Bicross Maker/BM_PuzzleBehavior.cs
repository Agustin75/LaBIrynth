using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BicrossMaker
{
	public class BM_PuzzleBehavior : MonoBehaviour
	{
		[Header("Scriptable Objects")]
		[SerializeField]
		private SquareState currentFillType;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public virtual void ToggleFiller()
		{
			currentFillType.value = currentFillType == BicrossSquareState.Filled ? BicrossSquareState.Empty : BicrossSquareState.Filled;
		}
	}
}
