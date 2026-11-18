using Godot;
using System;

[GlobalClass]
public partial class WeaponItem : Item
{
	[Export]
	public int damage = 10;
	
	[Export]
	public int range = 30;
}
