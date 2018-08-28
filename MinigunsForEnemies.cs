using GTA;
using GTA.Native;
using System;

public class MinigunsForEnemies : Script
{

    private WeaponHash[] allowedWeapons = { //Peds carrying these weapons won't be given the minigun
        WeaponHash.Minigun,
        WeaponHash.Unarmed
    };

    private Relationship[] allyRel = {
        Relationship.Companion,
        Relationship.Like,
        Relationship.Respect,
        Relationship.Pedestrians
    };

    public MinigunsForEnemies()
    {
        Tick += OnTick;
        Interval = 100;
    }

    void OnTick(object sender, EventArgs e)
    {
        foreach (Ped p in World.GetAllPeds()) { //Get all peds in game world
            Relationship rel = p.GetRelationshipWithPed(Game.Player.Character);
            if (!p.IsPlayer && p.IsHuman && p.IsAlive && p.Exists() && Array.IndexOf(allyRel, rel) < 0) { //Only act on non-player, human, alive, existing, non-ally, non-pedestrian peds

                if (Array.IndexOf(allowedWeapons, p.Weapons.Current.Hash) < 0) { //If current ped's weapon is not allowed...
                    p.Weapons.RemoveAll(); //Remove all current weapons
                    p.Weapons.Give(WeaponHash.Minigun, 9999, true, true); //Give the minigun
                    p.FiringPattern = FiringPattern.FullAuto; //Make the ped shoot constantly (not in short bursts)
                    Function.Call(Hash.SET_PED_COMBAT_ABILITY, p, 100); //http://www.dev-c.com/nativedb/func/info/c7622c0d36b2fda8
                    Function.Call(Hash.SET_PED_SHOOT_RATE, p, 1000); //Increase shooting rate?
                    /*Function.Call(Hash.SET_PED_COMBAT_RANGE, p, 2);
                    Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, p, 1);*/
                }
            }
        }
    }
}
