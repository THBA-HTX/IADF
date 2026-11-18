using Godot;

// SlotUI viser én slot. Den er "dum": den ved ikke, hvilket inventory den tilhører,
// og får bare at vide, hvilket item den skal vise.
public partial class SlotUI : PanelContainer
{
	private TextureRect _icon;

	public override void _Ready()
	{
		_icon = GetNode<TextureRect>("Icon");
	}

	public void SetItem(Item item)
	{
		if (item == null)
		{
			_icon.Texture = null;
			//TooltipText = "";
		}
		else
		{
			_icon.Texture = item.Icon;
			//TooltipText = item.Name;
		}
	}
}
