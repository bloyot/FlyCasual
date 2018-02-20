﻿using Ship;
using SubPhases;
using System;
using System.Collections.Generic;

namespace Ship
{
    namespace HWK290
    {
        public class KyleKatarn : HWK290
        {
            public KyleKatarn() : base()
            {
                PilotName = "Kyle Katarn";
                PilotSkill = 6;
                Cost = 21;

                IsUnique = true;

                PrintedUpgradeIcons.Add(Upgrade.UpgradeType.Elite);

                faction = Faction.Rebel;

                PilotAbilities.Add(new Abilities.KyleKatarnAbility());
            }
        }
    }
}

namespace Abilities
{
    public class KyleKatarnAbility : GenericAbility
    {
        public override void ActivateAbility()
        {
            Phases.OnCombatPhaseStart += RegisterAbility;
        }

        public override void DeactivateAbility()
        {
            Phases.OnCombatPhaseStart -= RegisterAbility;
        }

        private void RegisterAbility()
        {
            RegisterAbilityTrigger(TriggerTypes.OnCombatPhaseStart, Ability);
        }

        private void Ability(object sender, EventArgs e)
        {
            if (HostShip.Owner.Ships.Count > 1 && HostShip.Tokens.HasToken(typeof(Tokens.FocusToken)))
            {
                Messages.ShowInfoToHuman("Kyle Katarn: Select a ship to receive a Focus token");

                SelectTargetForAbilityNew(
                    SelectAbilityTarget,
                    FilterAbilityTarget,
                    GetAiAbilityPriority,
                    HostShip.Owner.PlayerNo
                );
            }
            else
            {
                Triggers.FinishTrigger();
            }
        }

        private bool FilterAbilityTarget(GenericShip ship)
        {
            return FilterByTargetType(ship, new List<TargetTypes>() { TargetTypes.OtherFriendly }) && FilterTargetsByRange(ship, 1, 3);
        }

        private int GetAiAbilityPriority(GenericShip ship)
        {
            int result = 0;
            int shipFocusTokens = ship.Tokens.CountTokensByType(typeof(Tokens.FocusToken));
            if (shipFocusTokens == 0) result += 100;
            result += (5 - shipFocusTokens);
            return result;
        }

        private void SelectAbilityTarget()
        {
            HostShip.Tokens.RemoveToken(
                typeof(Tokens.FocusToken),
                delegate {
                    TargetShip.Tokens.AssignToken(
                        new Tokens.FocusToken(TargetShip),
                        SelectShipSubPhase.FinishSelection
                    );
                }
            );          
        }
    }
}
