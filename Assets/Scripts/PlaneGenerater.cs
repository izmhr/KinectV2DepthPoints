using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Unity] スクリプトからメッシュ（ポリゴン）を生成する http://ftvoid.com/blog/post/716

public class PlaneGenerater : MonoBehaviour
{
	[SerializeField]
	private int w = 512;
	[SerializeField]
	private int h = 424;
	[SerializeField]
	private float pitch = 0.01f;

	public enum UVShift
	{
		NORMAL,
		SHIFT
	}
	[SerializeField]
	private UVShift uvShift = UVShift.SHIFT;


	// 起動時にインスペクターの設定に従ってメッシュを生成する
	void Awake()
	{
		GetComponent<MeshFilter>().mesh = MeshGenerate();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private Mesh MeshGenerateTest()
	{
		Mesh mesh = new Mesh();
		Rect rect = new Rect(0, 0, 2, 2);

		// 頂点の指定
		mesh.vertices = new Vector3[] {
			new Vector3(rect.xMin, rect.yMin, 0),
			new Vector3(rect.xMax, rect.yMin, 0),
			new Vector3(rect.xMax, rect.yMax, 0),
			new Vector3(rect.xMin, rect.yMax, 0),
		};
		// UV座標の指定
		mesh.uv = new Vector2[] {
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(1, 1),
			new Vector2(0, 1),
		};
		// 頂点インデックスの指定
		mesh.triangles = new int[] {
			0, 1, 2,
			0, 2, 3,
		};

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	Mesh MeshGenerateTest2()
	{
		Mesh mesh = new Mesh();

		Vector3[] vertices = new Vector3[3 * 3];
		for (var i = 0; i < 3; i++)
		{
			for (var j = 0; j < 3; j++)
			{
				vertices[i * 3 + j] = new Vector3((float)j, (float)i, 0f);
				//vertices[i * 3 + j].x = j;
				//mesh.vertices[i * 3 + j].y = i;
			}
		}
		mesh.vertices = vertices;

		Vector2[] uv = new Vector2[3 * 3];
		for (var i = 0; i < 3; i++)
		{
			for (var j = 0; j < 3; j++)
			{
				uv[i * 3 + j] = new Vector2((float)j / 2.0f, (float)i / 2.0f);
			}
		}
		mesh.uv = uv;

		mesh.triangles = new int[]
		{
			0,1,4,
			1,2,5,
			0,4,3,
			1,5,4,
			3,4,7,
			4,5,8,
			3,7,6,
			4,8,7
		};
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	Mesh MeshGenerateTest3(int w, int h)
	{
		Mesh mesh = new Mesh();

		Vector3[] vertices = new Vector3[(w + 1) * (h + 1)];
		for (var y = 0; y < (h + 1); y++)
		{
			for (var x = 0; x < (w + 1); x++)
			{
				vertices[y * (w + 1) + x] = new Vector3((float)x, (float)y, 0f);
				//vertices[i * 3 + j].x = j;
				//mesh.vertices[i * 3 + j].y = i;
			}
		}
		mesh.vertices = vertices;

		Vector2[] uv = new Vector2[(w + 1) * (h + 1)];
		for (var y = 0; y < (h + 1); y++)
		{
			for (var x = 0; x < (w + 1); x++)
			{
				uv[y * (w + 1) + x] = new Vector2((float)x / (float)w, (float)y / (float)h);
			}
		}
		mesh.uv = uv;

		int[] triangles = new int[w * h * 2 * 3];
		for (var y = 0; y < h; y++)
		{
			for (var x = 0; x < w; x++)
			{
				triangles[(y * w + x) * 2 * 3 + 0] = (y * (w + 1) + x);
				triangles[(y * w + x) * 2 * 3 + 1] = (y * (w + 1) + x + 1);
				triangles[(y * w + x) * 2 * 3 + 2] = ((y + 1) * (w + 1) + x + 1);
				triangles[(y * w + x) * 2 * 3 + 3] = (y * (w + 1) + x);
				triangles[(y * w + x) * 2 * 3 + 4] = ((y + 1) * (w + 1) + x + 1);
				triangles[(y * w + x) * 2 * 3 + 5] = ((y + 1) * (w + 1) + x);
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	// w と h はメッシュの分割数 (w=2 h=2 なら、 2x2で4マス、とか)
	Mesh MeshGenerateNormal(int w, int h, float pitch)
	{
		Mesh mesh = new Mesh();
		mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

		Vector3[] vertices = new Vector3[(w + 1) * (h + 1)];
		for (var y = 0; y < (h + 1); y++)
		{
			for (var x = 0; x < (w + 1); x++)
			{
				vertices[y * (w + 1) + x] = new Vector3((float)(x - w / 2) * pitch, (float)(y - h / 2) * pitch, 0f);
			}
		}
		mesh.vertices = vertices;

		Vector2[] uv = new Vector2[(w + 1) * (h + 1)];
		for (var y = 0; y < (h + 1); y++)
		{
			for (var x = 0; x < (w + 1); x++)
			{
				uv[y * (w + 1) + x] = new Vector2((float)x / (float)w, (float)y / (float)h);
			}
		}
		mesh.uv = uv;

		int[] triangles = new int[w * h * 2 * 3];
		for (var y = 0; y < h; y++)
		{
			for (var x = 0; x < w; x++)
			{
				triangles[(y * w + x) * 2 * 3 + 0] = (y * (w + 1) + x);
				triangles[(y * w + x) * 2 * 3 + 1] = (y * (w + 1) + x + 1);
				triangles[(y * w + x) * 2 * 3 + 2] = ((y + 1) * (w + 1) + x + 1);
				triangles[(y * w + x) * 2 * 3 + 3] = (y * (w + 1) + x);
				triangles[(y * w + x) * 2 * 3 + 4] = ((y + 1) * (w + 1) + x + 1);
				triangles[(y * w + x) * 2 * 3 + 5] = ((y + 1) * (w + 1) + x);
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	// w と h は頂点数。 w=2 h=2 なら、 1マス。
	Mesh MeshGenerateShift(int w, int h, float pitch)
	{
		Mesh mesh = new Mesh();
		mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

		Vector3[] vertices = new Vector3[w * h];
		for (var y = 0; y < h; y++)
		{
			for (var x = 0; x < w; x++)
			{
				vertices[y * w + x] = new Vector3((float)(x - w / 2) * pitch, (float)(y - h / 2) * pitch, 0f);
			}
		}
		mesh.vertices = vertices;

		Vector2[] uv = new Vector2[w * h];
		for (var y = 0; y < h; y++)
		{
			for (var x = 0; x < w; x++)
			{
				uv[y * w + x] = new Vector2(((float)x + 0.5f) / (float)w, ((float)y + 0.5f) / (float)h);
			}
		}
		mesh.uv = uv;

		int[] triangles = new int[(w - 1) * (h - 1) * 2 * 3];
		for (var y = 0; y < h - 1; y++)
		{
			for (var x = 0; x < w - 1; x++)
			{
				triangles[(y * (w - 1) + x) * 2 * 3 + 0] = (y * w + x);
				triangles[(y * (w - 1) + x) * 2 * 3 + 1] = (y * w + x + 1);
				triangles[(y * (w - 1) + x) * 2 * 3 + 2] = ((y + 1) * w + x + 1);
				triangles[(y * (w - 1) + x) * 2 * 3 + 3] = (y * w + x);
				triangles[(y * (w - 1) + x) * 2 * 3 + 4] = ((y + 1) * w + x + 1);
				triangles[(y * (w - 1) + x) * 2 * 3 + 5] = ((y + 1) * w + x);
			}
		}
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	Mesh MeshGenerate()
	{
		if (uvShift == UVShift.NORMAL)
		{
			return MeshGenerateNormal(w, h, pitch);
		}
		else
		{
			return MeshGenerateShift(w, h, pitch);
		}
	}
}
