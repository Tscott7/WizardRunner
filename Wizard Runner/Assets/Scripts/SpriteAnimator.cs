using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{

	public int framerate = 24;

	public List<Sprite> sprites = new List<Sprite> ();
	public List<Sprite> spritesL = new List<Sprite> ();
	public List<Sprite> spritesR = new List<Sprite> ();
	public List<Texture> textures = new List<Texture> ();

	public bool useDirectory = false;

	public string directoryNameL;
	public string directoryNameR;
	public int spriteNumber;
	public bool StartAt1 = true;
	public bool RandomStart = true;
	int i = 0;
	public bool startAt;
	public int startIndex;
	public int PaddingNumber = 4;
	public bool noPadding;
	public bool DestroyAtEnd = false;

	SpriteRenderer sr;
	Image im;

	public bool useImageNotSprite = true;
	public bool useMaterial = false;
	public bool useSpriteSheet = false;
	public bool goThenStop = false;
	public bool controlStart = false;
	//public bool go = false;

	public bool notEnabledAtStart = false;
	public bool centerAndScaleToScreen = false;

	public bool run = true;

	Material materialS;

	void Start ()
	{
		if (useMaterial) {
			materialS = GetComponent<MeshRenderer> ().material;
		} else if (useImageNotSprite)
			im = GetComponent<Image> ();
		else
			sr = GetComponent<SpriteRenderer> ();

		LoadSprites ();
		if (!controlStart) {
			StartCoroutine (FrameSpitter ());
		}
		if (notEnabledAtStart) {
			gameObject.SetActive (false);	
		}
		if (centerAndScaleToScreen) {
			Vector3 Temp = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
			RectTransform rt = GetComponent<RectTransform> ();
			rt.position = new Vector3 (0, 0, rt.position.z);
			rt.sizeDelta = new Vector2 (Temp.x * 2.2f, Temp.y * 2f);

			//rt.anchorMax = new Vector2 (Temp.x, Temp.y);
			//rt.anchorMin = new Vector2 (-Temp.x, -Temp.y);
		}
	}

	public void stopIE ()
	{
		run = false;
	}

	public void startIE ()
	{
		run = true;
		StartCoroutine (FrameSpitter ());
	}

	public void LoadSprites ()
	{
		

		if (useDirectory && StartAt1 && !startAt && !useSpriteSheet) {

			for (int i = 1; i <= spriteNumber; i++) {
				//print (directoryName + i.ToString ().PadLeft (4, '0'));
				if (useMaterial) {
					if (noPadding) {
						textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ()));
					} else {
						textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ().PadLeft (PaddingNumber, '0')));
					}
				} else {
					if (noPadding) {
						sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ()));
					} else {
						sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ().PadLeft (PaddingNumber, '0')));
					}
				}
			}
			
		}
		if (useDirectory && !StartAt1 && !startAt && !useSpriteSheet) {

			for (int i = 0; i < spriteNumber; i++) {
				//print (directoryName + i.ToString ().PadLeft (4, '0'));
				if (useMaterial) {
					if (noPadding) {
						textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ()));
					} else {
						textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ().PadLeft (PaddingNumber, '0')));
					}
				} else {
					if (noPadding) {
						sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ()));
					} else {
						sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ().PadLeft (PaddingNumber, '0')));
					}
				}
			}

		}
		if (useDirectory && !StartAt1 && startAt && !useSpriteSheet) {

			for (int i = startIndex; i < spriteNumber; i++) {

				//print (directoryName + i.ToString ().PadLeft (4, '0'));
				if (useMaterial) {
					if (noPadding) {
						textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ()));
					} else {
						textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ().PadLeft (PaddingNumber, '0')));
					}
				} else {
					if (noPadding) {
						sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ()));
					} else {
						sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ().PadLeft (PaddingNumber, '0')));
					}
				}
			}
			
		}
		if (useDirectory && useSpriteSheet) {
			
			Sprite[] temp = Resources.LoadAll<Sprite> (directoryNameL);
			for (int i = 0; i < temp.Length; i++) {
				sprites.Add (temp [i]);
				//print ("thingd");
			}
			
		}
		print ("sprites loaded");
	}

	public void LoadSprites (bool useD)
	{
		sprites.Clear ();
		spritesL.Clear ();
		spritesR.Clear ();
		textures.Clear ();

		if (useD && StartAt1 && !startAt && !useSpriteSheet) {

			for (int i = 1; i <= spriteNumber; i++) {
				//print (directoryName + i.ToString ().PadLeft (4, '0'));
				if (useMaterial)
					textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ().PadLeft (4, '0')));
				else
					sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ().PadLeft (4, '0')));
			}
			
		}
		if (useD && !StartAt1 && !startAt && !useSpriteSheet) {

			for (int i = 0; i < spriteNumber; i++) {
				//print (directoryName + i.ToString ().PadLeft (4, '0'));
				if (useMaterial)
					textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ().PadLeft (4, '0')));
				else
					sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ().PadLeft (4, '0')));
			}
			
		}
		if (useD && !StartAt1 && startAt && !useSpriteSheet) {

			for (int i = startIndex; i < spriteNumber; i++) {

				//print (directoryName + i.ToString ().PadLeft (4, '0'));
				if (useMaterial)
					textures.Add (Resources.Load<Texture> (directoryNameL + i.ToString ().PadLeft (4, '0')));
				else
					sprites.Add (Resources.Load<Sprite> (directoryNameL + i.ToString ().PadLeft (4, '0')));
			}
			
		}
		if (useD && useSpriteSheet) {
			
			Sprite[] temp = Resources.LoadAll<Sprite> (directoryNameL);
			for (int i = 0; i < temp.Length; i++) {
				sprites.Add (temp [i]);
				//print ("thingd");
			}
			
		}
		print ("sprites loaded 2");
	}

	IEnumerator FrameSpitter ()
	{
		if (RandomStart) {
			i = Random.Range (0, spriteNumber);
		}
		while (run) {
			if (useMaterial) {
				materialS.mainTexture = textures [i];
				print ("used texture named " + textures [i].name);
			} else if (useImageNotSprite) {
				try {
					im.sprite = sprites [i];
				} catch {
					//print ("sprite Error");
				}
			} else
				sr.sprite = sprites [i];
			i++;
			if (useMaterial) {
				if (i >= textures.Count) {
					i = 0;
					if (DestroyAtEnd) {
						Destroy (gameObject);
					}
					if (goThenStop)
						run = false;
				}
			} else {
				if (i >= sprites.Count) {
					i = 0;
					if (DestroyAtEnd) {
						Destroy (gameObject);
					}
					if (goThenStop)
						run = false;
				}
			}
			yield return new WaitForSeconds ((float)((/*sprites.Count / framerate*/1f) / framerate));
		}
	}
}
