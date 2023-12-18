using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SolarSystemObject
{
	[SerializeField]
	public Material material;

	[SerializeField]
	public string name;

	[SerializeField]
	public float diameterInKilometers;

	[SerializeField]
	public float distanceFromSunInKilometers;

	[SerializeField]
	public float orbitalVelocityInKilometersPerSecond;

	[SerializeField]
	public float obliquityToOrbitInDegrees;

	private float scaleFactor = 25f;
	private GameObject sphere;
	private GameObject wrapper;
	private Vector3 defaultPosition;

	public void GenerateSphere(Vector3 position)
	{
		// diameterInKilometers = 20000;
		wrapper = new GameObject();
		wrapper.name = name;

		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.transform.parent = wrapper.transform;
		sphere.GetComponent<SphereCollider>().radius = diameterInKilometers / 100000;

		sphere.GetComponent<MeshRenderer>().material = material;
		sphere.transform.localScale =  new Vector3(0.1f, 0.1f, 0.1f) * (diameterInKilometers / scaleFactor);
		sphere.transform.Rotate(Vector3.forward, obliquityToOrbitInDegrees);
		sphere.AddComponent<Planet>();


		sphere.transform.Rotate(Vector3.forward, obliquityToOrbitInDegrees);
		defaultPosition = new Vector3(0.0f, 0.1f, distanceFromSunInKilometers * 4);

		wrapper.transform.position = defaultPosition;
	}

	public GameObject getWrapper()
	{
		return wrapper;
	}

	public void Rotate()
	{
		sphere.transform.Rotate(Vector3.up, -orbitalVelocityInKilometersPerSecond * Time.deltaTime);
	}
}