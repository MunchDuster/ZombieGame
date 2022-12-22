using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClassController : MonoBehaviour
{

	public PlayerClass chosenClass;
	public MeshFilter playerDisplayHatMeshFilter;
	public MeshRenderer playerDisplayHatMeshRenderer;
	public SkinnedMeshRenderer playerDisplayClothesMeshRenderer;

	public PlayerClassDisplay[] displays;

	public void PickClass(int classIndex)
	{
		this.chosenClass = (PlayerClass)classIndex;
		UpdateClassDisplay();
	}

	public void PickClass(PlayerClass newPlayerClass)
	{
		this.chosenClass = newPlayerClass;
		UpdateClassDisplay();
	}

	void UpdateClassDisplay()
	{
		PlayerClassDisplay classDisplay = FindMatchingClassDisplay();
		if (classDisplay == null) throw new System.ArgumentException("Could not find classDisplay for class: " + chosenClass);

		playerDisplayHatMeshFilter.mesh = classDisplay.hatMesh;
		playerDisplayHatMeshRenderer.materials = classDisplay.hatMaterials;
		playerDisplayClothesMeshRenderer.materials[0].mainTexture = classDisplay.clothesTexture;
	}

	PlayerClassDisplay FindMatchingClassDisplay()
	{
		for (int i = 0; i < displays.Length; i++)
		{
			if (displays[i].playerClass == chosenClass)
			{
				return displays[i];
			}
		}

		return null;
	}
}
