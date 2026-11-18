using Godot;

public partial class ItemPickup : Area2D
{
	[Export]
	public Item item { get; set; }

	private Sprite2D _sprite;
	
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");

		if (item != null)
		{
			_sprite.Texture = item.Icon;
		}
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D other)
	{
		if (other is Player)
		{
			Inventory Inv = (GetNode<MyGlobal>("/root/MyGlobal")).PlayerInventory;
			
 			GD.Print("Inventory: "+ Inv);
			Inv.AddItem(item);
			//MyGlobal.PlayerInventory.PrintContents();
			QueueFree();
		}
	}
}
