using UnityEngine;
using System.Collections;

public class Tiler : MonoBehaviour
{

	public float TileX = 1;
	public float TileY = 1;
	public float OffsetX = 0;
	public float OffsetY = 0;
	Renderer rend;
	Material mat;

	void Start ()
	{
		rend = gameObject.GetComponent<Renderer> ();
		mat = rend.material;
		mat.mainTextureScale = new Vector2 (TileX, TileY);
		mat.mainTextureOffset = new Vector2 (OffsetX, OffsetY);
	}
}
