using Godot;
using System;
using Godot.Collections;

public partial class MyGlobal : Node
{
 	public Inventory PlayerInventory { get; private set; } = new();
	
	public int PlayerHealth = 100;
	public int PlayerMana = 100;
	public int PlayerCoins = 0;
	
	public override void _Ready()
	{
		PlayerInventory = new Inventory();
		AddChild(PlayerInventory);
	}
	
}
