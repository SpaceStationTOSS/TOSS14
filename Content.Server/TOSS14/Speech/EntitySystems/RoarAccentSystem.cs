using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Content.Shared.Speech;

namespace Content.Server.Speech.EntitySystems;

public sealed class RoarAccentSystem : EntitySystem
{
    private static readonly Regex RegexLowerR = new("r+");
    private static readonly Regex RegexUpperR = new("R+");
    private static readonly Regex RegexRUSLowerR = new("р+");
    private static readonly Regex RegexRUSUpperR = new("Р+");

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<RoarAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, RoarAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // rawrrrr
        message = RegexLowerR.Replace(message, "rr");
        // RawRRRR
        message = RegexUpperR.Replace(message, "RR");
        // rawrrrr
        message = RegexRUSLowerR.Replace(message, "рр");
        // RawRRRR
        message = RegexRUSUpperR.Replace(message, "РР");

        args.Message = message;
    }
}