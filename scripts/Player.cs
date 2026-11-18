using Godot;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _sprite;
	public bool canMove = true;

	[Export]
	public float Speed { get; set; } = 130.0f;
	
	public Inventory Inv;

	// Gemmer spillerens seneste bevægelsesretning
	private Vector2 _lastDirection = Vector2.Down;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
				// Set inventory
		Inv = (GetNode<MyGlobal>("/root/MyGlobal")).PlayerInventory;
		InventoryUI InvUI = GetNode<InventoryUI>("%InventoryUI");
		InvUI.SetInventory(Inv);
		GD.Print("Iventory:" + Inv);

		UpdateAnimation(Vector2.Zero);
	}

	public override void _PhysicsProcess(double delta)
	{
		// Hent input fra tastaturet
		Vector2 direction = Input.GetVector(
			"ui_left",
			"ui_right",
			"ui_up",
            "ui_down"
		);

		// Beregn bevægelsen
		Velocity = direction * Speed;

		// Flyt spilleren
		if ( canMove == true )
			MoveAndSlide();

		// Opdater animation separat
		UpdateAnimation(direction);
	}

	private void UpdateAnimation(Vector2 direction)
	{
		if (direction == Vector2.Zero)
		{
			PlayIdleAnimation();
			return;
		}

		// Gem den seneste bevægelsesretning
		_lastDirection = direction;

		// Vandret bevægelse
		if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
		{
			_sprite.FlipH = direction.X < 0;
			_sprite.Play("walk_right");
		}
		// Bevægelse opad
		else if (direction.Y < 0)
		{
			_sprite.FlipH = false;
			_sprite.Play("walk_up");
		}
		// Bevægelse nedad
		else
		{
			_sprite.FlipH = false;
			_sprite.Play("walk_down");
		}
	}

	private void PlayIdleAnimation()
	{
		// Sidste retning var vandret
		if (Mathf.Abs(_lastDirection.X) >
			Mathf.Abs(_lastDirection.Y))
		{
			_sprite.FlipH = _lastDirection.X < 0;
			_sprite.Play("idle_right");
		}
		// Sidste retning var opad
		else if (_lastDirection.Y < 0)
		{
			_sprite.FlipH = false;
			_sprite.Play("idle_up");
		}
		// Sidste retning var nedad
		else
		{
			_sprite.FlipH = false;
			_sprite.Play("idle_down");
		}
	}
	
	public void PickupItem(Item data){
		
		GD.Print("Player picked up: "+ data.Name);
		
	}
	
	
}
