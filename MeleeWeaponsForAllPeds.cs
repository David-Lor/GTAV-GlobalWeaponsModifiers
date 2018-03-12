using GTA;
using GTA.Native;
using System;

public class MeleeWeaponsForAllPeds : Script
{

    private WeaponHash[] allowedWeapons = {
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
        WeaponHash.Wrench
    };

    private Random r = new Random((int)DateTime.Now.Ticks);

    public MeleeWeaponsForAllPeds()
    {
        Tick += OnTick;
        Interval = 25;
    }

    void OnTick(object sender, EventArgs e)
    {
        foreach (Ped p in World.GetAllPeds()) { //Get all peds in game world
            if (!p.IsPlayer && p.IsHuman && p.IsAlive && p.Exists()) { //Only act on non-player, human, alive, existing peds

                //Avoid ped from fleeing
                //GTA.Native.Function.Call(GTA.Native.Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, p, true);
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_FLEE_ATTRIBUTES, p, 0, 0); //Disable fleeing
                //GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, p, 17, 1); //Allow fight with disadvantage
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, p, 46, 1); //Allow fight with disadvantage
                GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_COMBAT_ATTRIBUTES, p, 5, 1); //Allow fight with disadvantage
                //GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_SEEING_RANGE, p, 0.0f);
                //GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_HEARING_RANGE, p, 0.0f);
                //GTA.Native.Function.Call(GTA.Native.Hash.SET_PED_ALERTNESS, 0);

                if (Array.IndexOf(allowedWeapons, p.Weapons.Current.Hash) < 0) { //If current ped's weapon is not allowed...
                    p.Weapons.RemoveAll(); //Remove all current weapons
                    int allowedWeaponsIndex = r.Next(allowedWeapons.Length); //Get a random weapon from allowed weapons list
                    p.Weapons.Give(allowedWeapons[allowedWeaponsIndex], 0, true, true); //Give that weapon to the ped
                }

            }
        }
    }
}
