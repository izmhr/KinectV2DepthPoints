using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//【Unityシェーダ入門】ポリゴンをポイント（点）で表現する - おもちゃラボ http://nn-hokuson.hatenablog.com/entry/2017/06/06/202953

public class PointController : MonoBehaviour
{
	void Start()
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh.SetIndices(meshFilter.mesh.GetIndices(0), MeshTopology.Points, 0);
	}
}