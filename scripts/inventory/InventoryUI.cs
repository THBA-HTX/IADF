using Godot;
using System.Collections.Generic;

// InventoryUI viser indholdet af ét Inventory i et grid.
// Den kan vise ALLE Inventory-komponenter: den får bare at vide, hvilken den skal vise.
public partial class InventoryUI : MarginContainer
{
	// Slot-scenen, som skal oprettes én gang pr. slot. Sættes i Inspector.
	// [Export] public PackedScene SlotScene { get; set; }
	private PackedScene SlotScene = GD.Load<PackedScene>(
    	"res://scenes/inventory/slot_ui.tscn"
	);

	private GridContainer _grid;
	private Inventory _inventory;

	// En liste over de slot-grafikker, vi har oprettet.
	private readonly List<SlotUI> _slotUIs = new List<SlotUI>();

	public override void _Ready()
	{
		_grid = GetNode<GridContainer>("Background/Content/Grid");
	}

	// Fortæller UI'et, hvilket inventory det skal vise, og bygger slots.
	// Fortæller UI'et, hvilket inventory det skal vise, og bygger slots.
	public void SetInventory(Inventory inventory)
	{
		// Afmeld et evt. tidligere inventory.
		DetachInventory();

		_inventory = inventory;

		if (_inventory == null)
			return;

		_inventory.InventoryChanged += Refresh;

		if (SlotScene == null)
		{
			GD.PushError("InventoryUI: SlotScene er ikke sat i Inspector.");
			return;
		}

		// Fjern eventuelle gamle slots.
		foreach (SlotUI oldSlot in _slotUIs)
		{
			oldSlot.QueueFree();
		}
		_slotUIs.Clear();

		// Opret én SlotUI pr. slot i inventoryet.
		for (int i = 0; i < _inventory.GetSize(); i++)
		{
			SlotUI slotUi = SlotScene.Instantiate<SlotUI>();
			_grid.AddChild(slotUi);
			_slotUIs.Add(slotUi);
		}

		// Vis det aktuelle indhold.
		Refresh();
	}

	private void DetachInventory()
	{
		if (_inventory != null &&
			GodotObject.IsInstanceValid(_inventory))
		{
			_inventory.InventoryChanged -= Refresh;
		}

		_inventory = null;
	}
	// Opdaterer alle slots, så de matcher inventoryets indhold.
	// Skal kaldes, hver gang inventoryets indhold er ændret.
	public void Refresh()
	{
		for (int i = 0; i < _slotUIs.Count; i++)
		{
			_slotUIs[i].SetItem(_inventory.GetItem(i));
		}
	}
}
