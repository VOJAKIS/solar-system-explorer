using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public Behaviour halo;

	// public static float scaleFactor = 1000f;
	// public float diameterInKilometers = 12756f;
	// public float orbitalVelocityInKilometersPerSecond = 29.8f;
	// public float obliquityToOrbitInDegrees = 23.4f;
	// public float distanceFromTheSun = 3f;

	// Start is called before the first frame update
	void Start()
	{
		if (halo != null)
		{
			halo.enabled = false;
		}

		// Vector3 scaleObjectTo = new Vector3(0.1f, 0.1f, 0.1f) * (diameterInKilometers / scaleFactor);
		// transform.localScale = scaleObjectTo;
		// transform.Rotate(Vector3.forward, obliquityToOrbitInDegrees);
		// transform.position = transform.position + new Vector3(0.0f, 0.0f, distanceFromTheSun);
	}

	void OnMouseOver()
	{
		if (halo != null)
		{
			halo.enabled = true;
		}
	}

	// https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html
	void OnMouseDown()
	{
		// https://docs.unity3d.com/ScriptReference/GameObject.FindWithTag.html
		GameObject player = GameObject.FindWithTag("Player");
		player.transform.SetParent(transform.parent);
		print(transform.parent);
	}

	void OnMouseExit()
	{
		if (halo != null)
		{
			halo.enabled = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		RotateObject();
	}

	void RotateObject()
	{
		// transform.Rotate(Vector3.up, -orbitalVelocityInKilometersPerSecond * Time.deltaTime);
	}
}
