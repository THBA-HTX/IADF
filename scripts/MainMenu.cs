using Godot;
using System;

public partial class MainMenu : Control
{

	// Vi erklærer at vi vil bruge disse referencer..
	VBoxContainer mainMenuButtons;
	Panel optionButtons;
	
	public override void _Ready(){
		// Vi sætte referencer til at pege på MainMenu knapper og Options Buttons.
		mainMenuButtons = GetNode<VBoxContainer>("%VBoxContainer");
		optionButtons = GetNode<Panel>("%OptionButtons");
		//GD.Print("OPTIONS :" +optionButtons );
		mainMenuButtons.Visible = true;
   		optionButtons.Visible = false;
	}


	public void OnStartPressed(){
		GetTree().ChangeSceneToFile("res://scenes/World.tscn");
	}

	public void OnOptionsPressed(){
		mainMenuButtons.Visible = false;
   		optionButtons.Visible = true;
	}

	public void OnExitPressed(){
		GetTree().Quit();
	}


}
