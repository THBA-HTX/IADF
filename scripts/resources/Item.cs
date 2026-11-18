using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
	[Export]
	public string Name = "";
	
	[Export(PropertyHint.MultilineText)]
	public string Description { get; set; } = "";
	
	[Export]
	public Texture2D Icon;
}
