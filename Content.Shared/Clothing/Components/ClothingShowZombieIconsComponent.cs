using Robust.Shared.GameStates;
using Content.Shared.Clothing.EntitySystems;

namespace Content.Shared.Clothing;

[RegisterComponent]
[NetworkedComponent]
[Access(typeof(ClothingShowZombieIconsSystem))]
public sealed partial class ClothingShowZombieIconsComponent : Component
{

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public string Slot = "eyes";

}
