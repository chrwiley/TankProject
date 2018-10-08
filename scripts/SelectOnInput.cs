using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem; // lets us see if the keyboard is being used to select menu items
    public GameObject selectedObject; //which button was pressed

    private bool buttonSelected; //turns the button on and off 
    
    // Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
