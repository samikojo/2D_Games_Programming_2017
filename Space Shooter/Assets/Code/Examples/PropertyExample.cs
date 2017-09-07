using UnityEngine;
public class PropertyExample : MonoBehaviour
{
	private Collider _collider;

	public Collider CurrentCollider
	{
		get
		{
			//return GetComponent<Collider>();
			if (_collider == null)
			{
				_collider = GetComponent<Collider>();
				Debug.Log("Got component collider");
			}
			return _collider;
		}
	}

	//public int Score { get; set; }

	private int _score;
	public int Score
	{
		get { return _score; }
		set
		{
			_score = value;
			if(_score < 0)
			{
				_score = 0;
			}
		}
	}

	protected void Awake()
	{
		Debug.Log(CurrentCollider.name);
	}

	protected void Start()
	{
		Debug.Log(CurrentCollider.name);
	}
}

