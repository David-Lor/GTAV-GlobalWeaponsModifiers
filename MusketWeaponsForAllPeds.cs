using GTA;
using GTA.Native;
using System;

public class MusketsForAllPeds : Script
{

    private WeaponHash[] allowedWeapons = { //Peds carrying these weapons won't be given the musket
        WeaponHash.Musket,
        WeaponHash.Unarmed
    };

    public MusketsForAllPeds()
    {
        Tick += OnTick;
        Interval = 25;
    }

    void OnTick(object sender, EventArgs e)
    {
        foreach (Ped p in World.GetAllPeds()) { //Get all peds in game world
            if (!p.IsPlayer && p.IsHuman && p.IsAlive && p.Exists()) { //Only act on non-player, human, alive, existing peds
                if (Array.IndexOf(allowedWeapons, p.Weapons.Current.Hash) < 0) { //If current ped's weapon is not allowed...
                    p.Weapons.RemoveAll(); //Remove all current weapons
                    p.Weapons.Give(WeaponHash.Musket, 500, true, true); //Give musket to ped
                }
            }
        }
    }
}
