using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicObjectHandler : LogController {

    public enum Categories
    {
        SCENERY,
        TRAINCARS,
        LOOT,
        PROJECTILES,
        MISC
    }

    //  These are already set in the prefab.
    [SerializeField] private static GameObject sceneryObj;
    [SerializeField] private static GameObject traincarsObj;
    [SerializeField] private static GameObject lootObj;
    [SerializeField] private static GameObject projectilesObj;
    [SerializeField] private static GameObject miscObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void InstantiateDynamic(Object o, Categories c)
    {

        Transform parent = null;
        switch(c)
        {
            case (Categories.SCENERY):
                parent = sceneryObj.transform;
                break;
            case (Categories.TRAINCARS):
                parent = traincarsObj.transform;
                break;
            case (Categories.LOOT):
                parent = lootObj.transform;
                break;
            case (Categories.PROJECTILES):
                parent = projectilesObj.transform;
                break;
            case (Categories.MISC):
                parent = miscObj.transform;
                break;
            
        }
        if (parent) Instantiate(o, parent);
        else
        {
            Debug.LogError("Instantiating dynamic object with invalid parent.");
        }
    }
}
