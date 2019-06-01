/* Grand Theft Auto V - All Peds Unarmed Script - By David Lor @ https://github.com/David-Lor/GTAV-GlobalWeaponsModifiers

IMPORTANT: to avoid peds fleeing from combat, you should apply this mod: https://www.gta5-mods.com/misc/peds-without-fear

*/

using System;
using GTA;
using GTA.Native;

// ReSharper disable once UnusedMember.Global, CheckNamespace, InvertIf
public class AllPedsUnarmed : Script
{
    private readonly WeaponHash unarmed = WeaponHash.Unarmed;
    
    public AllPedsUnarmed()
    {
        Tick += Loop;
        Interval = 500;
        UI.Notify("All Peds Unarmed LOADED");
    }
    
    private void Loop(object sender, EventArgs e)
    {
        foreach (Ped ped in World.GetAllPeds())
        {
            if (!ped.IsPlayer && ped.IsHuman && ped.IsAlive && ped.Exists() && ped.Weapons.Current.Hash != unarmed) {
            	ped.Weapons.RemoveAll();
            }
        }

    }
    
}
