/* Grand Theft Auto V - Melee Weapons for all Peds Script - By David Lor @ https://github.com/David-Lor/GTAV-GlobalWeaponsModifiers

IMPORTANT: to avoid peds fleeing from combat, you should apply this mod: https://www.gta5-mods.com/misc/peds-without-fear

*/

using System;
using GTA;
using GTA.Native;

// ReSharper disable once UnusedMember.Global, CheckNamespace, InvertIf
public class MeleeWeaponsForAllPeds : Script
{

    private readonly WeaponHash unarmed = WeaponHash.Unarmed;
    
    // TODO: analyze all weapons, only give melee weapon if any of ped's weapons is out of this array.
    private readonly WeaponHash[] allowedWeapons = {
        WeaponHash.Unarmed,
        WeaponHash.Bat,
        WeaponHash.BattleAxe,
        WeaponHash.Bottle,
        WeaponHash.Crowbar,
        WeaponHash.Dagger,
        WeaponHash.GolfClub,
        WeaponHash.Hammer,
        WeaponHash.Hatchet,
        WeaponHash.Knife,
        WeaponHash.Machete,
        WeaponHash.PoolCue,
        WeaponHash.SwitchBlade,
        WeaponHash.Wrench,
        WeaponHash.Flashlight
    };

    private readonly Random random;
    
    public MeleeWeaponsForAllPeds()
    {
        Tick += Loop;
        Interval = 500;
        random = new Random((int)DateTime.Now.Ticks);
        UI.Notify("Global Weapons for All Peds LOADED");
    }
    
    private void Loop(object sender, EventArgs e)
    {
        foreach (Ped ped in World.GetAllPeds())
        {
            WeaponHash currentWeapon = ped.Weapons.Current.Hash;
            if (!ped.IsPlayer && ped.IsHuman && ped.IsAlive && ped.Exists() && (Array.IndexOf(allowedWeapons, currentWeapon) < 0 || currentWeapon == unarmed))
            {
                int allowedWeaponsIndex = random.Next(allowedWeapons.Length);
                WeaponHash newWeapon = allowedWeapons[allowedWeaponsIndex];
                
                ped.Weapons.RemoveAll();
                bool equip = (currentWeapon != unarmed);
                ped.Weapons.Give(newWeapon, 0, equip, true);
            }
        }

    }
    
}
