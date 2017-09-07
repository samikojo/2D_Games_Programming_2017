using UnityEngine;
public class AccessModifierExample : MonoBehaviour
{
	public ChildClassExample Child;

	protected void Start()
	{
		Child.Print("Access modifier start");
	}
}
