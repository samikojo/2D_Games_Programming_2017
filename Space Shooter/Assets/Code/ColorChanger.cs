using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	public Color[] AvailableColors;
	public SpriteRenderer Sprite;

	private int _currentIndex = 0;

	private void Awake()
	{
		// Called when GameObject is activated first time.
		Debug.Log("Awake");
		if (Sprite == null)
		{
			// The reference to the sprite is not set. Try to get the reference
			// by looking for a SpriteRenderer component
			// from the GameObject to which this ColorChanger is attached to.
			Sprite = GetComponent<SpriteRenderer>();
		}

		if( AvailableColors.Length == 0 )
		{
			Debug.LogError("No colors available!");
		}
	}

	private void Start()
	{
		// Called when GameObject is activated first time.
		Debug.Log("Start");
	}

	private void OnEnable()
	{
		// Called every time GameObject (or the component) is activated.
		Debug.Log("OnEnable");
	}


	// Update is called once per frame
	void Update()
	{
		// We want to change the color of the Sprite every time user clicks the
		// spacebar.

		// First we need to check if user did press the spacebar.
		if ( Input.GetKeyDown(KeyCode.Space) )
		{
			// Spacebar is pressed down. Let's change the color. All available colors are stored in the AvailableColors array.
			// The SpriteRenderer has a property called 'color' which we can set.
			// Indexing an array starts from 0.
			Sprite.color = AvailableColors[_currentIndex];

			_currentIndex++;
			if (_currentIndex >= AvailableColors.Length)
			{
				// If _currentColor is bigger or as big as the length of the 
				// AvailableColors array, we have reached the end of the array
				// and we should start from the beginning.
				_currentIndex = 0;
			}
		}
		// Run every frame
		Debug.Log("Update");
	}

	private void FixedUpdate()
	{
		// Called every physics frame (50 times / second by default)
		Debug.Log("FixedUpdate");
	}

	private void OnDisable()
	{
		// Called every time this component is disabled.
		Debug.Log("OnDisable");
	}

	private void OnDestroy()
	{
		// Called just before object is destroyed.
		Debug.Log("OnDestroy");
	}


}
