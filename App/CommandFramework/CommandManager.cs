using System.Collections.Generic;

namespace App.CommandFramework;

public class CommandManager
{
	private static CommandManager? _instance;
	public static CommandManager Instance => _instance ??= new CommandManager();

	private readonly List<ICommand> _commandHistory;

	private CommandManager()
	{
		_commandHistory = new List<ICommand>();
	}

	public void Register(ICommand command)
	{
		if (_commandHistory.Contains(command)) return;
		_commandHistory.Add(command);
	}

	public void Undo()
	{
		if (_commandHistory.Count == 1) return;

		_commandHistory.RemoveAt(_commandHistory.Count - 1);
		foreach (var command in _commandHistory)
		{
			command.Execute();
		}
	}
}
