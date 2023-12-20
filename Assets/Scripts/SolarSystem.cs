using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SolarSystem : MonoBehaviour
{
	public float scaleFactor = 50.0f;

	public GameObject directionalLight;

	public GameObject saturnRings;

	public GameObject factSheetWrapper;
	public TextMeshProUGUI factSheetText;
	public TextMeshProUGUI factSheetTitle;
	public Button factSheetShowButton;
	public Button factSheetHideButton;

	public Button closerToSunButton;
	public Button fartherFromSunButton;
	public TextMeshProUGUI travellingErrorText;
	private int hideErrorTextAfterSeconds = 4;
	public Button homeButton;

	public GameObject player;

	public Canvas UI;

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
	public SolarSystemObject Pluto;

	private SolarSystemObject[] solarSystemObjects;

	void Start()
	{
		solarSystemObjects = new SolarSystemObject[] { Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto };
		foreach (SolarSystemObject solarSystemObject in solarSystemObjects)
		{
			Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
			solarSystemObject.setScaleFactor(scaleFactor);
			solarSystemObject.GenerateSphere(pos);
		}

		// Moving the spaceship to earth
		player.transform.SetParent(Earth.getWrapper().transform);

		// Traverse
		closerToSunButton.onClick.AddListener(MoveSpaceshipCloserToSun);
		fartherFromSunButton.onClick.AddListener(MoveSpaceshipFartherFromSun);
		travellingErrorText.enabled = false;
		homeButton.onClick.AddListener(MoveSpaceshipToEarth);

		// Directional light
		directionalLight.transform.SetParent(Sun.getWrapper().transform);
		directionalLight.transform.localPosition = Vector3.zero;
		directionalLight.SetActive(true);

		// Fact sheet
		factSheetWrapper.SetActive(false);
		factSheetHideButton.onClick.AddListener(HideFactSheet);
		factSheetText.text = Earth.getFact();
		factSheetShowButton.onClick.AddListener(ShowFactSheet);

		// Saturn rings
		AddSaturnRings();

		// HideUI();
		float seconds = 5;
		StartCoroutine(DisplayUIAfterSeconds(seconds));
	}

	// Update is called once per frame
	void Update()
	{
		foreach (var solarSystemObject in solarSystemObjects)
		{
			if (solarSystemObject != Sun)
			{
				solarSystemObject.RotateAroundSun();
			}

			solarSystemObject.Rotate();
			solarSystemObject.RotateMoons();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}

		UpdateFactSheet();
	}

	IEnumerator DisplayUIAfterSeconds(float seconds)
	{
		HideUI();
		yield return new WaitForSeconds(seconds);
		ShowUI();
	}

	void ShowUI()
	{
		// UI.SetActive(true);
		UI.enabled = true;
	}

	void HideUI()
	{
		// UI.SetActive(false);
		UI.enabled = false;
	}

	void UpdateFactSheet()
	{
		string playersParentName = player.transform.parent.name;
		SolarSystemObject currentSolarSystemObject = null;
		Moon currentMoon = null;

		foreach (SolarSystemObject solarSystemObject in solarSystemObjects)
		{
			if (playersParentName == solarSystemObject.getName())
			{
				currentSolarSystemObject = solarSystemObject;
				break;
			}

			foreach (Moon moon in solarSystemObject.getMoons())
			{
				if (playersParentName == moon.getName())
				{
					currentMoon = moon;
					break;
				}
			}
		}

		if (currentMoon != null)
		{
			factSheetTitle.text = currentMoon.getName();
			factSheetText.text = currentMoon.getFact();
		}

		if (currentSolarSystemObject != null)
		{
			factSheetTitle.text = currentSolarSystemObject.getName();
			factSheetText.text = currentSolarSystemObject.getFact();
		}
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

			foreach (Moon moon in solarSystemObjects[i].getMoons())
			{
				if (playersParentName == moon.getName())
				{
					index = i;
					break;
				}
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

			foreach (Moon moon in solarSystemObjects[i].getMoons())
			{
				if (playersParentName == moon.getName())
				{
					index = i;
					break;
				}
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
