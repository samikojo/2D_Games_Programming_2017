using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritanceExample : MonoBehaviour
{
	protected virtual void Start()
	{
		Print("Start");
	}

	public virtual void Print(string message)
	{
		Debug.Log(message);
	}
}
