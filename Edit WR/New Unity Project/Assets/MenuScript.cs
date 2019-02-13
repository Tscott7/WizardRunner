using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas selector;
    public GameObject wiz1;
    public GameObject wiz2;
	public Canvas quitMenu;
	public Button startText;
	public Button exitText;


	// Use this for initialization
	void Start () {

		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
		selector = selector.GetComponent<Canvas> ();
		selector.enabled = false;
	}

	public void InitSelect()
	{
		selector.enabled = true;
	}

	public void ExitPress()
	{

		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;

	}

	public void NoPress()
	{

		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;

	}

	public void StartLevel()
	{
		Application.LoadLevel (1);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}


	
	// Update is called once per frame
	public void Update () {
		
	
	}
}
