using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Moon
{
	[SerializeField] private Material material;
	[SerializeField] private string name;
	[SerializeField] private float diameterInKilometers;
	[SerializeField] private float orbitalVelocityInKilometersPerSecond;
	[SerializeField] private float obliquityToOrbitInDegrees;
	[SerializeField] private float meanOrbitalVelocityInKilometersPerSecond;

	[SerializeField] private float distanceFromPlanetToRotateAroundInKilomters;
	[SerializeField] private string fact;

	private GameObject planetToRotateAround;

	private float scaleFactor = 25f;
	private GameObject sphere;
	private GameObject wrapper;
	private GameObject parentWrapper;
	private GameObject trailRenderer;

	public void initialize()
	{
		CreateWrapper();
		CreateSphere();
		AddTrailComponent();

	}

	void AddTrailComponent()
	{
		TrailRenderer thisTrailRenderer = wrapper.AddComponent<TrailRenderer>() as TrailRenderer;
		TrailRenderer otherTrailRenderer = trailRenderer.GetComponent<TrailRenderer>() as TrailRenderer;

		thisTrailRenderer.widthCurve = otherTrailRenderer.widthCurve;
		thisTrailRenderer.time = otherTrailRenderer.time / 5;
		thisTrailRenderer.endColor = otherTrailRenderer.endColor;
		thisTrailRenderer.startColor = otherTrailRenderer.startColor;
		thisTrailRenderer.SetMaterials(otherTrailRenderer.materials.ToList());
		thisTrailRenderer.minVertexDistance = otherTrailRenderer.minVertexDistance;
		thisTrailRenderer.emitting = otherTrailRenderer.emitting;
		thisTrailRenderer.generateLightingData = otherTrailRenderer.generateLightingData;
		thisTrailRenderer.motionVectorGenerationMode = otherTrailRenderer.motionVectorGenerationMode;
	}

	public void Rotate()
	{
		// TODO: Rotate wrapper around parent wrapper
		wrapper.transform.RotateAround(parentWrapper.transform.position, Vector3.up, Time.deltaTime * (-meanOrbitalVelocityInKilometersPerSecond) / scaleFactor);

		sphere.transform.Rotate(Vector3.up, -orbitalVelocityInKilometersPerSecond * Time.deltaTime);
	}

	private void CreateWrapper()
	{
		wrapper = new GameObject();
		wrapper.name = name;
		wrapper.transform.SetParent(parentWrapper.transform);
		float x = distanceFromPlanetToRotateAroundInKilomters / 1000;
		wrapper.transform.localPosition = new Vector3(x, wrapper.transform.position.y, wrapper.transform.position.z);
	}

	private void CreateSphere()
	{
		sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.name = "Sphere";
		sphere.transform.parent = wrapper.transform;
		sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * (diameterInKilometers / scaleFactor);
		// sphere.transform.localPosition = new Vector3(sphere.transform.position.x, sphere.transform.position.y, distanceFromPlanetToRotateAroundInKilomters / 1000);
		sphere.transform.localPosition = Vector3.zero;
		sphere.transform.Rotate(Vector3.forward, obliquityToOrbitInDegrees);

		sphere.GetComponent<MeshRenderer>().material = material;
		sphere.AddComponent<Planet>();
		sphere.GetComponent<Planet>().setPlayer(GameObject.FindWithTag("Player"));

		sphere.GetComponent<Renderer>().receiveShadows = false;
	}

	public void setScaleFactor(float scaleFactor)
	{
		this.scaleFactor = scaleFactor;
	}

	public void setParentWrapper(GameObject parentWrapper)
	{
		this.parentWrapper = parentWrapper;
	}

	public string getName()
	{
		return name;
	}

	public string getFact()
	{
		return fact;
	}
	
	public void setTrailRenderer(GameObject trailRenderer)
	{
		this.trailRenderer = trailRenderer;
	}
}
