using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolarSystem : MonoBehaviour
{
	public GameObject directionalLight;

	public GameObject saturnRings;

	public GameObject factSheetWrapper;
	public TextMeshProUGUI factSheetText;
	public TextMeshProUGUI factSheetTitle;
	public Button factSheetShowButton;
	public Button factSheetHideButton;

	[SerializeField]
	public SolarSystemObject Sun;

	public SolarSystemObject Mercury;
	public SolarSystemObject Venus;
	public SolarSystemObject Earth;
	public SolarSystemObject Mars;
	public SolarSystemObject Jupiter;
	public SolarSystemObject Saturn;
	public SolarSystemObject Uranus;
	public SolarSystemObject Neptune;

	private SolarSystemObject[] solarSystemObjects;

	public Button closerToSunButton;
	public Button fartherFromSunButton;
	public TextMeshProUGUI travellingErrorText;
	private int hideErrorTextAfterSeconds = 4;
	public Button homeButton;

	public GameObject player;

	void Start()
	{
		solarSystemObjects = new SolarSystemObject[9] { Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune };
		foreach (SolarSystemObject solarSystemObject in solarSystemObjects)
		{
			Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
			solarSystemObject.GenerateSphere(pos);
		}

		// Moving the spaceship to earth
		GameObject firstGameObjectFound = Earth.getWrapper();
		player.transform.SetParent(firstGameObjectFound.transform);

		// Traverse
		closerToSunButton.onClick.AddListener(MoveSpaceshipCloserToSun);
		fartherFromSunButton.onClick.AddListener(MoveSpaceshipFartherFromSun);
		travellingErrorText.enabled = false;
		homeButton.onClick.AddListener(MoveSpaceshipToEarth);

		// Directional light
		directionalLight.transform.SetParent(Sun.getWrapper().transform);
		directionalLight.SetActive(true);

		// Fact sheet
		factSheetWrapper.SetActive(false);
		factSheetHideButton.onClick.AddListener(HideFactSheet);
		factSheetText.text = Earth.getFact();
		factSheetShowButton.onClick.AddListener(ShowFactSheet);

		// Saturn rings
		AddSaturnRings();
	}

	// Update is called once per frame
	void Update()
	{
		foreach (var solarSystemObject in solarSystemObjects)
		{
			solarSystemObject.Rotate();
		}
	}

	void UpdateFactSheet()
	{
		string playersParentName = player.transform.parent.name;
		int index = -1;
		for (int i = 0; i < solarSystemObjects.Length; i++)
		{
			if (playersParentName == solarSystemObjects[i].name)
			{
				index = i;
				break;
			}
		}

		if (index < 0)
		{
			return;
		}

		factSheetText.text = solarSystemObjects[index].getFact();
		factSheetTitle.text = solarSystemObjects[index].getName();
	}

	void ShowFactSheet()
	{
		factSheetShowButton.gameObject.SetActive(false);
		factSheetWrapper.SetActive(true);
		UpdateFactSheet();
	}

	void HideFactSheet()
	{
		factSheetWrapper.SetActive(false);
		factSheetShowButton.gameObject.SetActive(true);
	}

	void MoveSpaceshipToEarth()
	{
		MoveShaceshipToParent(Earth.getWrapper().transform);
	}

	void MoveSpaceshipCloserToSun()
	{
		string playersParentName = player.transform.parent.name;

		int index = -1;
		for (int i = 0; i < solarSystemObjects.Length; i++)
		{
			if (playersParentName == solarSystemObjects[i].name)
			{
				index = i;
				break;
			}
		}

		if (index <= 0)
		{
			ShowAndHideErrorText();
			return;
		}

		MoveShaceshipToParent(solarSystemObjects[index - 1].getWrapper().transform);
	}

	void MoveSpaceshipFartherFromSun()
	{
		string playersParentName = player.transform.parent.name;

		int index = -1;
		for (int i = 0; i < solarSystemObjects.Length; i++)
		{
			if (playersParentName == solarSystemObjects[i].name)
			{
				index = i;
				break;
			}
		}

		if (index == -1 || index >= solarSystemObjects.Length - 1)
		{
			ShowAndHideErrorText();
			return;
		}

		MoveShaceshipToParent(solarSystemObjects[index + 1].getWrapper().transform);
	}

	void ShowAndHideErrorText()
	{
		if (travellingErrorText.enabled) return;

		StartCoroutine(ShowAndHideErrorTextAfterSeconds(hideErrorTextAfterSeconds));
	}

	IEnumerator ShowAndHideErrorTextAfterSeconds(int timeoutSeconds)
	{
		travellingErrorText.enabled = true;
		yield return new WaitForSeconds(timeoutSeconds);
		travellingErrorText.enabled = false;
	}

	void MoveShaceshipToParent(Transform parent)
	{
		player.transform.SetParent(parent);
		UpdateFactSheet();
	}

	void AddSaturnRings()
	{
		saturnRings.transform.SetParent(Saturn.getSphere().transform);
		saturnRings.transform.localPosition = new Vector3(0, 0, 0);
		saturnRings.transform.localScale = new Vector3(1, 1, 1);
		// saturnRings.transform.rotation.z = 0;
		// saturnRings.transform.Rotate(0, 0, 0, Space.World);
		saturnRings.transform.localRotation = Quaternion.identity;
	}
}
