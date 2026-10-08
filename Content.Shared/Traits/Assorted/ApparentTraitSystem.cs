using Content.Shared.Examine;
using Content.Shared.IdentityManagement;
using Robust.Shared.Network;

namespace Content.Shared.Traits.Assorted;

/// <summary>
/// This handles traits that come with an examine blurb
/// </summary>
public sealed partial class ApparentTraitSystem: EntitySystem
{
    [Dependency] private INetManager _net = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        SubscribeLocalEvent<ApparentTraitComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<ApparentTraitComponent, ComponentShutdown>(OnShutdown);
        SubscribeLocalEvent<ApparentTraitComponent, ExaminedEvent>(OnExamined);
    }

    private void OnExamined(Entity<ApparentTraitComponent> blindness, ref ExaminedEvent args)
    {
        if (args.IsInDetailsRange && !_net.IsClient && blindness.Comp.Blindness == 0)
        {
            args.PushMarkup(Loc.GetString("permanent-blindness-trait-examined", ("target", Identity.Entity(blindness, EntityManager))));
        }
    }
