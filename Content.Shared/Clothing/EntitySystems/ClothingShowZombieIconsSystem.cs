using Content.Shared.Inventory.Events;
using Content.Shared.Movement.Systems;
using Content.Shared.Damage.Systems;
using Content.Shared.Movement.Components;
using Robust.Shared.Serialization.Manager;
using Content.Shared.Zombies;
namespace Content.Shared.Clothing;


public sealed class ClothingShowZombieIconsSystem : EntitySystem
{
    [Dependency] private readonly IEntityManager _entities = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ClothingShowZombieIconsComponent, GotEquippedEvent>(OnGotEquipped);
        SubscribeLocalEvent<ClothingShowZombieIconsComponent, GotUnequippedEvent>(OnGotUnequipped);
    }


    public void OnGotUnequipped(EntityUid uid, ClothingShowZombieIconsComponent component, GotUnequippedEvent args)
    {
        if (TryComp(args.Equipee, out ZombieComponent? zombieComponent))
            return;

        if (args.Slot == component.Slot)
        {
            if(TryComp(args.Equipee, out ShowZombieIconsComponent? showzombiesComp))
            {
                _entities.RemoveComponent<ShowZombieIconsComponent>(args.Equipee);
            }
        }
    }

    private void OnGotEquipped(EntityUid uid, ClothingShowZombieIconsComponent component, GotEquippedEvent args)
    {
        if (TryComp(args.Equipee, out ZombieComponent? zombieComponent))
            return;

        if (args.Slot == component.Slot)
        { 
            if(!TryComp(args.Equipee, out ShowZombieIconsComponent? showzombiesComp))
            {
                showzombiesComp = _entities.AddComponent<ShowZombieIconsComponent>(args.Equipee);
            }
        }
    }
}
