using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using Lib.Drawing.Text;

namespace App.Helpers;

public class AvaloniaTextBoxTextarea : ITextarea
{
	private readonly TextBox _textBox;
	private readonly List<List<char>> _state;

	public AvaloniaTextBoxTextarea(TextBox textBox)
	{
		_textBox = textBox;
		_state = new List<List<char>>();
	}

	public void Write(string value, PointerPosition startingPosition)
	{
		var (line, column) = startingPosition;

		AddLinesIfNeeded(line);
		AddColumnsIfNeeded(line, column, value.Length);

		for (var i = 0; i < value.Length; i++)
		{
			_state[line][column + i] = value[i];
		}

		UpdateControl();
	}

	public void Write(char value, PointerPosition position)
	{
		var (line, column) = position;

		AddLinesIfNeeded(line);
		AddColumnsIfNeeded(line, column);

		_state[line][column] = value;

		UpdateControl();
	}

	private void AddLinesIfNeeded(int requestedLine)
	{
		while (_state.Count <= requestedLine)
		{
			_state.Add(new List<char>());
		}
	}

	private void AddColumnsIfNeeded(int requestedLine, int requestedColumn, int amount = 1)
	{
		AddLinesIfNeeded(requestedLine);

		while (_state[requestedLine].Count < requestedColumn + amount)
		{
			_state[requestedLine].Add(' ');
		}
	}

	private void UpdateControl()
	{
		var builder = new StringBuilder();

		foreach (var charList in _state)
		{
			builder.AppendLine(string.Concat(charList));
		}

		_textBox.Text = builder.ToString();
	}
}
