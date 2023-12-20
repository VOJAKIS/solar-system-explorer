using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SolarSystemObject
{
	[SerializeField] public Material material;
	[SerializeField] public string name;
	[SerializeField] public float diameterInKilometers;
	[SerializeField] public float distanceFromSunInKilometers;
	[SerializeField] public float orbitalVelocityInKilometersPerSecond;
	[SerializeField] public float obliquityToOrbitInDegrees;

	public string fact;

	public List<Moon> moons;

	private float scaleFactor = 25f;
	private GameObject sphere;
	private GameObject wrapper;
	private Vector3 defaultPosition;

	private GameObject sun;

	public void GenerateSphere(Vector3 position)
	{
		// diameterInKilometers = 20000;
		wrapper = new GameObject();
		wrapper.name = name;

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.parent = wrapper.transform;
		sphere.GetComponent<MeshRenderer>().material = material;
		sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * (diameterInKilometers / scaleFactor);
		sphere.transform.Rotate(Vector3.forward, obliquityToOrbitInDegrees);
		sphere.AddComponent<Planet>();
		sphere.GetComponent<Planet>().setPlayer(GameObject.FindWithTag("Player"));
		defaultPosition = new Vector3(0.0f, 0.1f, distanceFromSunInKilometers * scaleFactor);

		wrapper.transform.position = defaultPosition;

		sun = GameObject.Find("Slnko");

		sphere.GetComponent<Renderer>().receiveShadows = false;

		// Generate moons
		InitializeMoons();
	}

	private void InitializeMoons()
	{
		foreach (Moon moon in moons)
		{
			moon.setParentWrapper(wrapper);
			moon.setScaleFactor(scaleFactor);
			moon.initialize();
		}
	}

	public GameObject getSphere()
	{
		return sphere;
	}

	public string getName()
	{
		return name;
	}

	public GameObject getWrapper()
	{
		return wrapper;
	}

	public string getFact()
	{
		return fact;
	}

	public void Rotate()
	{
		sphere.transform.Rotate(Vector3.up, -orbitalVelocityInKilometersPerSecond * Time.deltaTime / scaleFactor);
	}

	public void RotateAroundSun()
	{
		wrapper.transform.RotateAround(sun.transform.position, Vector3.up, Time.deltaTime * (-orbitalVelocityInKilometersPerSecond) / scaleFactor);
	}

	public void RotateMoons()
	{
		foreach (Moon moon in moons)
		{
			moon.Rotate();
		}
	}

	public List<Moon> getMoons()
	{
		return moons;
	}

	public void setScaleFactor(float scaleFactor)
	{
		this.scaleFactor = scaleFactor;
	}
}