using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BicrossField", menuName = "Bicross Field")]
public class BicrossPuzzle : ScriptableObject
{
	[HideInInspector]
	[SerializeField]
	private int rows, columns;

	[SerializeField]
	private List<BicrossSquareState> field;

	[SerializeField]
	private List<Hints> rowHints, columnHints;

	public void Initialize(int _rows, int _columns, List<BicrossSquareState> _field, List<Hints> _rowHints, List<Hints> _columnHints)
	{
		rows = _rows;
		columns = _columns;
		field = _field;
		rowHints = new List<Hints>();
		columnHints = new List<Hints>();

		// Hard copy to prevent overriding from BicrossMaker (not needed in final version)
		for (int r = 0; r < rows; r++)
		{
			rowHints.Add(new Hints(new List<HintTypes>(_rowHints[r].listOfHints)));
		}
		for (int c = 0; c < columns; c++)
		{
			columnHints.Add(new Hints(new List<HintTypes>(_columnHints[c].listOfHints)));
		}
	}

	public int GetNumRows()
	{
		return rows;
	}

	public int GetNumColumns()
	{
		return columns;
	}

	public List<BicrossSquareState> GetSolution()
	{
		return field;
	}

	public List<Hints> GetRowHints()
	{
		return rowHints;
	}

	public List<Hints> GetColumnHints()
	{
		return columnHints;
	}

	public BicrossSize GetBicrossSize()
	{
		if (rows == 5 && columns == 5)
			return BicrossSize.FiveXFive;
		else if (rows == 10 && columns == 10)
			return BicrossSize.TenXTen;
		else if (rows == 15 && columns == 15)
			return BicrossSize.FifteenXFifteen;

		// TODO: Will have to implement a way to handle these ones if they are to be used
		return BicrossSize.CustomSize;
	}
}
