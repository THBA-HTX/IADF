using Godot;
using Godot.Collections;

// Inventory er en genbrugelig komponent, der KUN håndterer data:
// hvilke items ligger i hvilke slots. Den ved intet om grafik.
// Den kan sættes som barn under en spiller, en kiste, en fjende osv.
[GlobalClass]
public partial class Inventory : Node
{
	// KAN GIVE BESKED NÅR INVENTORY ÆNDRES
	[Signal]
	public delegate void InventoryChangedEventHandler();
	
	// Antal slots. Ændres i Inspector.
	[Export] 
	public int SlotCount { get; set; } = 9;

	// Items, inventoryet starter med. Praktisk til test.
	[Export] 
	public Array<Item> StartItems { get; set; } = new Array<Item>();

	private InventorySlot[] _slots;

	public override void _Ready()
	{
		// Opret en tom slot for hvert felt i inventoryet.
		_slots = new InventorySlot[SlotCount];
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i] = new InventorySlot();
		}
		 GD.Print($"Inventory READY: {GetPath()}, slots: {_slots.Length}");
		//GD.Print( _slots );
		// Læg start-items i inventoryet.
		foreach (Item item in StartItems)
		{
			AddItem(item);
		}
	}

	// Hvor mange slots har inventoryet?
	public int GetSize()
	{
		return _slots.Length;
	}

	// Giver itemet i en slot, eller null hvis slotten er tom eller ikke findes.
	public Item GetItem(int index)
	{
		if (!IsValidIndex(index))
		{
			return null;
		}
		return _slots[index].HeldItem;
	}

	// Lægger et item i den første tomme slot.
	// Returnerer true, hvis det lykkedes, og false, hvis inventoryet er fuldt.
	public bool AddItem(Item item)
	{
		
		
		if (item == null)
		{
			return false;
		}
	GD.Print("TEST" + item );
		// Her kan stacking senere tilføjes: led først efter en slot med samme item,
		// som ikke er fyldt op, og læg kun i en tom slot, hvis der ikke er nogen.
		for (int i = 0; i < _slots.Length; i++)
		{
			
			if (_slots[i].IsEmpty())
			{
				_slots[i].HeldItem = item;
				_slots[i].Amount = 1;
				GD.Print("ITEM WAS ADDED:" + item );
				EmitSignal(SignalName.InventoryChanged);
				return true;
			}
		}

		return false; // Ingen tomme slots
	}

	// Fjerner itemet i en slot og giver det tilbage.
	// Returnerer null, hvis slotten var tom eller ikke findes.
	public Item RemoveItem(int index)
	{
		if (!IsValidIndex(index) || _slots[index].IsEmpty())
		{
			return null;
		}

		Item removed = _slots[index].HeldItem;
		_slots[index].Clear();
		EmitSignal(SignalName.InventoryChanged);
		return removed;
	}

	// Skriver indholdet i Output-panelet. Bruges til at teste uden UI.
	public void PrintContents()
	{
		GD.Print($"--- Inventory i '{GetParent().Name}' ({_slots.Length} slots) ---");
		for (int i = 0; i < _slots.Length; i++)
		{
			string text = _slots[i].IsEmpty() ? "(tom)" : _slots[i].HeldItem.Name;
			GD.Print($"Slot {i}: {text}");
		}
	}

	// Tjekker, at et slot-nummer er inden for grænserne.
	private bool IsValidIndex(int index)
	{
		return (index >= 0) && (index < _slots.Length);
	}
}
