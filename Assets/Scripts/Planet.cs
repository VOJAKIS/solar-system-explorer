using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	private GameObject player;
	public Behaviour halo;

	void Start()
	{
		if (halo != null)
		{
			halo.enabled = false;
		}
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

	}

	public void setPlayer(GameObject player)
	{
		this.player = player;
	}
}
