using Godot;
using System;

public partial class EyeSpawner : Node2D
{
	[Export] public PackedScene EyeScene; // Træk din AnimalEyes scene ind her i Editoren
	[Export] public Rect2 SpawnArea = new Rect2(0, 0, 1152, 648); // Definer hvor på skærmen de må dukke op
	[Export] public float MinSpawnDelay = 3.0f;
	[Export] public float MaxSpawnDelay = 8.0f;

	private Timer _spawnTimer;

	public override void _Ready()
	{
		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = GetRandomDelay();
		_spawnTimer.OneShot = true;
		_spawnTimer.Timeout += OnSpawnTimerTimeout;
		AddChild(_spawnTimer);
		_spawnTimer.Start();
	}

	private void OnSpawnTimerTimeout()
	{
		SpawnEyes();
		// Nulstil timeren med et nyt tilfældigt interval
		_spawnTimer.WaitTime = GetRandomDelay();
		_spawnTimer.Start();
	}

	private void SpawnEyes()
	{
		// Lav en instans af øjnene
		var eyes = EyeScene.Instantiate<Node2D>();
		
		// Find en tilfældig position inden for SpawnArea
		float randomX = (float)GD.RandRange(SpawnArea.Position.X, SpawnArea.Position.X + SpawnArea.Size.X);
		float randomY = (float)GD.RandRange(SpawnArea.Position.Y, SpawnArea.Position.Y + SpawnArea.Size.Y);
		
		eyes.GlobalPosition = new Vector2(randomX, randomY);
		AddChild(eyes);

		// Slet øjnene automatisk efter nogle sekunder, så de ikke hober sig op
		GetTree().CreateTimer(GD.RandRange(2.0f, 5.0f)).Timeout += () => eyes.QueueFree();
	}

	private float GetRandomDelay()
	{
		return (float)GD.RandRange(MinSpawnDelay, MaxSpawnDelay);
	}
}
