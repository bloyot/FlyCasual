﻿using Upgrade;

namespace Ship
{
    namespace SecondEdition.TIEAgAggressor
    {
        public class OnyxSquadronScout : TIEAgAggressor
        {
            public OnyxSquadronScout() : base()
            {
                PilotInfo = new PilotCardInfo(
                    "Onyx Squadron Scout",
                    3,
                    28,
                    extraUpgradeIcon: UpgradeType.Talent,
                    seImageNumber: 129
                );
            }
        }
    }
}
