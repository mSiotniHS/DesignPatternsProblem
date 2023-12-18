namespace App.CommandFramework;

public abstract class ACommand : ICommand
{
	public void Execute()
	{
		Register();
		DoExecute();
	}

	private void Register()
	{
		var cm = CommandManager.Instance;
		cm.Register(this);
	}

	protected abstract void DoExecute();
}
