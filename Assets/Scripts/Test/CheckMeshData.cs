using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMeshData : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		CheckMesh();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void CheckMesh()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Debug.Log(mesh.ToString());
	}
}
