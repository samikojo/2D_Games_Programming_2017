using UnityEngine;

public class ChildClassExample : InheritanceExample {

	protected override void Start()
	{
		Print("Start");
	}

	public override void Print(string message)
	{
		Debug.LogError(message);
	}
}

