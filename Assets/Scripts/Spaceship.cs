using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spaceshp : MonoBehaviour
{
	public float traverseSpeed = 50;
	public int rotateCameraSpeed = 10;

	public Slider traverseSpeedSlider;

	private int zoomMultiplier = 20;
	private int zoomLevelCurrent = 3;
	private int zoomLevelMaximum = 25;
	private int zoomLevelMinimum = 1;

	// Start is called before the first frame update
	void Start()
	{
		this.traverseSpeed = traverseSpeedSlider.value;
		traverseSpeedSlider.onValueChanged.AddListener(delegate {
			ChangeTraverseSpeed();
		});
	}

	// Update is called once per frame
	void Update()
	{
		RotateCamera();
		MouseZoomInAndOut();

		MoveTowardsCurrentParent();
	}

	void MoveTowardsCurrentParent()
	{
		Transform parent = transform.parent;

		Vector3 currentTargetPosition = new Vector3(transform.position.x, parent.transform.Find("Sphere").transform.localScale.y, parent.position.z);
		Vector3 moveTowards = Vector3.MoveTowards(transform.position, currentTargetPosition, traverseSpeed * Time.deltaTime);

		transform.position = moveTowards;

		// float step = rotateCameraSpeed * Time.deltaTime; // calculate distance to move
		// transform.position = Vector3.MoveTowards(transform.position, parent.position, step);
	}

	// https://discussions.unity.com/t/how-do-i-make-the-camera-zoom-in-and-out-with-the-mouse-wheel/36739/2
	void MouseZoomInAndOut()
	{
		float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peace of code is written by JelleWho https://github.com/jellewie

		//If the scrollwheel has changed
		if (scrollWheelChange != 0)
		{
			if (zoomLevelCurrent <= zoomLevelMinimum && scrollWheelChange > 0)
			{
				return;
			}
			if (zoomLevelCurrent >= zoomLevelMaximum && scrollWheelChange < 0)
			{
				return;
			}

			zoomLevelCurrent += (scrollWheelChange > 0) ? -1 : 1;
			Camera.main.transform.position += Camera.main.transform.forward * scrollWheelChange * zoomMultiplier;
		}
	}

	void RotateCamera()
	{
		// Clip to rotationX -60 and 24
		if (Input.GetMouseButton(1))
		{
			Camera.main.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotateCameraSpeed / 2);

			float angleX = Input.GetAxis("Mouse Y") * rotateCameraSpeed;
			float localRotationXOfCamera = Camera.main.transform.localEulerAngles.x;
			print("Local rotation of camera x: " + localRotationXOfCamera);
			if (localRotationXOfCamera + angleX >= 0 && localRotationXOfCamera + angleX <= 70)
			{
				Camera.main.transform.RotateAround(transform.position, Camera.main.transform.right, angleX);
			}
		}
	}

	void ChangeTraverseSpeed()
	{
		this.traverseSpeed = traverseSpeedSlider.value;
	}
}
