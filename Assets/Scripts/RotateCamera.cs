using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public GameObject objectToRotate;
	public float multiplier = 2;
	
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float angle = Time.deltaTime * multiplier;
		objectToRotate.transform.Rotate(Vector3.up, angle, Space.Self);
	}
}
