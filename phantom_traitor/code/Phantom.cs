using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TerrorTown;

namespace Sandbox
{
    public partial class Phantom : BaseTeam
    {
        public override TeamAlignment TeamAlignment => TeamAlignment.Traitor;
        public override string TeamName => "Phantom";
        public override Color TeamColour => Color.Orange;
        public override TeamMemberVisibility TeamMemberVisibility => TeamMemberVisibility.Alignment | TeamMemberVisibility.PublicWhenConfirmedDead;
        public override string VictimKillMessage => "You were killed by {0}. They were a Traitor.";

        public override string RoleDescription => @"You are a Phantom Traitor! You do not have a shop!

If an innocent inspects your body, they will die!";

        public override string IdentifyString => "{0} found the body of {1}. They were a Phantom Traitor.";

        public override bool DisplayComrades => true;

        public override bool CanSeeMIA => true;

        public override string OverheadIcon => "ui/world/traitor.png";

        [ConVar.Server(
    "nobodyll_players_per_phantom",
    Help = "Assign one phantom per specified player count. For example, setting this to 7 will mean that there will be one phantom per 7 players. The default is 10.",
    Saved = true
)]
        public static int PlayersPerPhantom { get; set; } = 10;

        public override float TeamPlayerPercentage => 1f / PlayersPerPhantom;

        private static Dictionary<TerrorTown.Player, WorldIndicatorPanel> Phantoms = new();

        [Event("Game.CorpseIdentified")]
        public static void CorpseIdentified(TerrorTown.Player ply, Corpse crp)
        {
            if (crp.TeamName != "Phantom") return;

            if (ply.Team.TeamAlignment == TeamAlignment.Innocent || ply.Team.TeamAlignment == TeamAlignment.NoEnemies)ply.TakeDamage((new DamageInfo { Damage = ply.Health * 99 }).WithTag("suicide"));

        }
        public override bool ShouldWin()
        {
            return false;
        }
    }
}