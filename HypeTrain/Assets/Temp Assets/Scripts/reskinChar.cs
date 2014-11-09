using UnityEngine;
using System;
using System.Collections;

public class reskinChar : MonoBehaviour {

	/*public string spriteSheetName;

	// Update is called once per frame
	void LateUpdate () {
	
		Sprite[] subSprites = Resources.LoadAll<Sprite>("Sprites/characters/" + spriteSheetName);

		foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
		{
			Debug.Log("in the foreach loop");
			string spriteName = renderer.sprite.name;
			Sprite newSprite = Array.Find (subSprites, item => item.name == spriteName);
			if(newSprite) {
				Debug.Log ("should be swapping");
				renderer.sprite = newSprite;
			}
		}
	}*/


	//GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("NAMEOFCONTROLLER");

}
