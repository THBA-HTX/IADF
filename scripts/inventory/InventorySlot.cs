// En slot er ren data: den ved, hvilket item den holder, og hvor mange.
// Klassen arver ikke fra Godot-typer, så den er en helt almindelig C#-klasse.
public class InventorySlot
{
	// Itemet i slotten. null betyder, at slotten er tom.
	public Item HeldItem { get; set; }

	// Antal items i slotten. Er altid 1 indtil videre (non-stackable),
	// men feltet er klar til, at stacking kan tilføjes senere.
	public int Amount { get; set; }

	public bool IsEmpty()
	{
		return HeldItem == null;
	}

	public void Clear()
	{
		HeldItem = null;
		Amount = 0;
	}
}
