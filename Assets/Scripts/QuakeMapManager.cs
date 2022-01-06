using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Atlas.MappingComponents.Sandbox;
using FistVR;
using System;

namespace FoffoMods
{
	public class QuakeMapManager : MonoBehaviour 
	{
		public Text trooper_sosig_text;
		public Text dog_sosig_text;

		private int current_trooper_index = 0;
		private int current_dog_index = 0;

		private Array enum_array = Enum.GetValues(typeof(SosigEnemyID));

		public SosigEnemyID current_trooper_sosig = SosigEnemyID.Misc_Dummy;
		public SosigEnemyID current_dog_sosig = SosigEnemyID.Misc_Dummy;

		public bool timed_run = true;
		public Button timed_on_button;
		public Button timed_off_button;

		public bool random_spawns = true;
		public Button random_on_button;
		public Button random_off_button;

//		public bool override_secrets = false;
//		public Button override_on_button;

		public GameObject dog_spawners_parent;
		private SosigSpawnPoint[] dog_spawners;

		public GameObject grunt_spawners_parent;
		private SosigSpawnPoint[] grunt_spawners;

		public bool timer_started;

		public float timer_seconds;

		public List<Text> timer_texts;

		static private System.Random random = new System.Random();

		public Dictionary<SosigEnemyID, String> enemy_name_parser = new Dictionary<SosigEnemyID, String>()
		{
			{SosigEnemyID.Misc_Dummy, "Dummy"},
		    {SosigEnemyID.Misc_Elf, "Elf"},
		    {SosigEnemyID.M_Swat_Scout, "Swat Scout"},
		    {SosigEnemyID.M_Swat_Ranger, "Swat Ranger"},
		    {SosigEnemyID.M_Swat_Sniper, "Swat Sniper"},
		    {SosigEnemyID.M_Swat_Riflewiener, "Swat Riflewiener"},
		    {SosigEnemyID.M_Swat_Officer, "Swat Officer"},
		    {SosigEnemyID.M_Swat_SpecOps, "Swat SpecOps"},
		    {SosigEnemyID.M_Swat_Markswiener, "Swat Markswiener"},
		    {SosigEnemyID.M_Swat_Shield, "Swat Shield"},
		    {SosigEnemyID.M_Swat_Heavy, "Swat Heavy"},
		    {SosigEnemyID.M_Swat_Breacher, "Swat Breacher"},
		    {SosigEnemyID.M_Swat_Guard, "Swat Guard"},
		    {SosigEnemyID.M_MercWiener_Scout, "Mercenary Scout"},
		    {SosigEnemyID.M_MercWiener_Ranger, "Mercenary Ranger"},
		    {SosigEnemyID.M_MercWiener_Sniper, "Mercenary Sniper"},
		    {SosigEnemyID.M_MercWiener_Riflewiener, "Mercenary Riflewiener"},
		    {SosigEnemyID.M_MercWiener_Officer, "Mercenary Officer"},
		    {SosigEnemyID.M_MercWiener_SpecOps, "Mercenary SpecOps"},
		    {SosigEnemyID.M_MercWiener_Markswiener, "Mercenary Markswiener"},
		    {SosigEnemyID.M_MercWiener_Shield, "Mercenary Shield"},
		    {SosigEnemyID.M_MercWiener_Heavy, "Mercenary Heavy"},
		    {SosigEnemyID.M_MercWiener_Breacher, "Mercenary Breacher"},
		    {SosigEnemyID.M_MercWiener_Guard, "Mercenary Guard"},
		    {SosigEnemyID.M_GreaseGremlins_Scout, "Grease Gremlins Scout"},
		    {SosigEnemyID.M_GreaseGremlins_Ranger, "Grease Gremlins Ranger"},
		    {SosigEnemyID.M_GreaseGremlins_Sniper, "Grease Gremlins Sniper"},
		    {SosigEnemyID.M_GreaseGremlins_Riflewiener, "Grease Gremlins Riflewiener"},
		    {SosigEnemyID.M_GreaseGremlins_Officer, "Grease Gremlins Officer"},
		    {SosigEnemyID.M_GreaseGremlins_SpecOps, "Grease Gremlins SpecOps"},
		    {SosigEnemyID.M_GreaseGremlins_Markswiener, "Grease Gremlins Markswiener"},
		    {SosigEnemyID.M_GreaseGremlins_Shield, "Grease Gremlins Shield"},
		    {SosigEnemyID.M_GreaseGremlins_Heavy, "Grease Gremlins Heavy"},
		    {SosigEnemyID.M_GreaseGremlins_Breacher, "Grease Gremlins Breacher"},
		    {SosigEnemyID.M_GreaseGremlins_Guard, "Grease Gremlins Guard"},
		    {SosigEnemyID.M_Popsicles_Scout, "Popsicles Scout"},
		    {SosigEnemyID.M_Popsicles_Ranger, "Popsicles Ranger"},
		    {SosigEnemyID.M_Popsicles_Sniper, "Popsicles Sniper"},
		    {SosigEnemyID.M_Popsicles_Riflewiener, "Popsicles Riflewiener"},
		    {SosigEnemyID.M_Popsicles_Officer, "Popsicles Officer"},
		    {SosigEnemyID.M_Popsicles_SpecOps, "Popsicles SpecOps"},
		    {SosigEnemyID.M_Popsicles_Markswiener, "Popsicles Markswiener"},
		    {SosigEnemyID.M_Popsicles_Shield, "Popsicles Shield"},
		    {SosigEnemyID.M_Popsicles_Heavy, "Popsicles Heavy"},
		    {SosigEnemyID.M_Popsicles_Breacher, "Popsicles Breacher"},
		    {SosigEnemyID.M_Popsicles_Guard, "Popsicles Guard"},
		    {SosigEnemyID.M_VeggieDawgs_Scout, "Veggie Dawgs Scout"},
		    {SosigEnemyID.M_VeggieDawgs_Ranger, "Veggie Dawgs Ranger"},
		    {SosigEnemyID.M_VeggieDawgs_Sniper, "Veggie Dawgs Sniper"},
		    {SosigEnemyID.M_VeggieDawgs_Riflewiener, "Veggie Dawgs Riflewiener"},
		    {SosigEnemyID.M_VeggieDawgs_Officer, "Veggie Dawgs Officer"},
		    {SosigEnemyID.M_VeggieDawgs_SpecOps, "Veggie Dawgs SpecOps"},
		    {SosigEnemyID.M_VeggieDawgs_Markswiener, "Veggie Dawgs Markswiener"},
		    {SosigEnemyID.M_VeggieDawgs_Shield, "Veggie Dawgs Shield"},
		    {SosigEnemyID.M_VeggieDawgs_Heavy, "Veggie Dawgs Heavy"},
		    {SosigEnemyID.M_VeggieDawgs_Breacher, "Veggie Dawgs Breacher"},
		    {SosigEnemyID.M_VeggieDawgs_Guard, "Veggie Dawgs Guard"},
		    {SosigEnemyID.W_Green_Guard, "Green Guard"},
		    {SosigEnemyID.W_Green_Patrol, "Green Patrol"},
		    {SosigEnemyID.W_Green_Officer, "Green Officer"},
		    {SosigEnemyID.W_Green_Riflewiener, "Green Riflewiener"},
		    {SosigEnemyID.W_Green_Grenadier, "Green Grenadier"},
		    {SosigEnemyID.W_Green_HeavyRiflewiener, "Green Heavy Riflewiener"},
		    {SosigEnemyID.W_Green_Machinegunner, "Green Machinegunner"},
		    {SosigEnemyID.W_Green_Flamewiener, "Green Flamewiener"},
		    {SosigEnemyID.W_Green_Antitank, "Green Antitank"},
		    {SosigEnemyID.W_Tan_Guard, "Tan Guard"},
		    {SosigEnemyID.W_Tan_Patrol, "Tan Patrol"},
		    {SosigEnemyID.W_Tan_Officer, "Tan Officer"},
		    {SosigEnemyID.W_Tan_Riflewiener, "Tan Riflewiener"},
		    {SosigEnemyID.W_Tan_Grenadier, "Tan Grenadier"},
		    {SosigEnemyID.W_Tan_HeavyRiflewiener, "Tan Heavy Riflewiener"},
		    {SosigEnemyID.W_Tan_Machinegunner, "Tan Machinegunner"},
		    {SosigEnemyID.W_Tan_Flamewiener, "Tan Flamewiener"},
		    {SosigEnemyID.W_Tan_Antitank, "Tan Antitank"},
		    {SosigEnemyID.W_Brown_Guard, "Brown Guard"},
		    {SosigEnemyID.W_Brown_Patrol, "Brown Patrol"},
		    {SosigEnemyID.W_Brown_Officer, "Brown Officer"},
		    {SosigEnemyID.W_Brown_Riflewiener, "Brown Riflewiener"},
		    {SosigEnemyID.W_Brown_Grenadier, "Brown Grenadier"},
		    {SosigEnemyID.W_Brown_HeavyRiflewiener, "Brown Heavy Riflewiener"},
		    {SosigEnemyID.W_Brown_Machinegunner, "Brown Machinegunner"},
		    {SosigEnemyID.W_Brown_Flamewiener, "Brown Flamewiener"},
		    {SosigEnemyID.W_Brown_Antitank, "Brown Antitank"},
		    {SosigEnemyID.W_Grey_Guard, "Grey Guard"},
		    {SosigEnemyID.W_Grey_Patrol, "Grey Patrol"},
		    {SosigEnemyID.W_Grey_Officer, "Grey Officer"},
		    {SosigEnemyID.W_Grey_Riflewiener, "Grey Riflewiener"},
		    {SosigEnemyID.W_Grey_Grenadier, "Grey Grenadier"},
		    {SosigEnemyID.W_Grey_HeavyRiflewiener, "Grey Heavy Riflewiener"},
		    {SosigEnemyID.W_Grey_Machinegunner, "Grey Machinegunner"},
		    {SosigEnemyID.W_Grey_Flamewiener, "Grey Flamewiener"},
		    {SosigEnemyID.W_Grey_Antitank, "Grey Antitank"},
		    {SosigEnemyID.D_Gambler, "Gambler"},
		    {SosigEnemyID.D_Bandito, "Bandito"},
		    {SosigEnemyID.D_Gunfighter, "Gunfighter"},
		    {SosigEnemyID.D_BountyHunter, "Bounty Hunter"},
		    {SosigEnemyID.D_Sheriff, "Sheriff"},
		    {SosigEnemyID.D_Boss, "Boss"},
		    {SosigEnemyID.D_Sniper, "Sniper"},
		    {SosigEnemyID.D_BountyHunterBoss, "Bounty Hunter Boss"},
		    {SosigEnemyID.J_Guard, "J Guard"},
		    {SosigEnemyID.J_Patrol, "J Patrol"},
		    {SosigEnemyID.J_Grenadier, "J Grenadier"},
		    {SosigEnemyID.J_Officer, "J Officer"},
		    {SosigEnemyID.J_Commando, "J Commando"},
		    {SosigEnemyID.J_Riflewiener, "J Riflewiener"},
		    {SosigEnemyID.J_Flamewiener, "J Flamewiener"},
		    {SosigEnemyID.J_Machinegunner, "J Machinegunner"},
		    {SosigEnemyID.J_Sniper, "J Sniper"},
		    {SosigEnemyID.H_BreadCrabZombie_Fast, "BreadCrab Zombie Fast"},
		    {SosigEnemyID.H_BreadCrabZombie_HEV, "BreadCrab Zombie HEV"},
		    {SosigEnemyID.H_BreadCrabZombie_Poison, "BreadCrab Zombie Poison"},
		    {SosigEnemyID.H_BreadCrabZombie_Standard, "BreadCrab Zombie Standard"},
		    {SosigEnemyID.H_BreadCrabZombie_Zombie, "BreadCrab Zombie Zombie"},
		    {SosigEnemyID.H_CivicErection_Meathack, "Civic Erection Meathack"},
		    {SosigEnemyID.H_CivicErection_Melee, "Civic Erection Melee"},
		    {SosigEnemyID.H_CivicErection_Pistol, "Civic Erection Pistol"},
		    {SosigEnemyID.H_CivicErection_SMG, "Civic Erection SMG"},
		    {SosigEnemyID.H_OberwurstElite_AR2, "Oberwurst Elite AR2"},
		    {SosigEnemyID.H_OberwurstSoldier_Shotgun, "Oberwurst Soldier Shotgun"},
		    {SosigEnemyID.H_OberwurstSoldier_SMG, "Oberwurst Soldier SMG"},
		    {SosigEnemyID.H_OberwurstSoldier_SMGNade, "Oberwurst Soldier SMG+Nade"},
		    {SosigEnemyID.H_OberwurstSoldier_Sniper, "Oberwurst Soldier Sniper"},
		    {SosigEnemyID.MF_RedHots_Demo, "MF RedHots Demoman"},
		    {SosigEnemyID.MF_RedHots_Engineer, "MF RedHots Engineer"},
		    {SosigEnemyID.MF_RedHots_Heavy, "MF RedHots Heavy"},
		    {SosigEnemyID.MF_RedHots_Medic, "MF RedHots Medic"},
		    {SosigEnemyID.MF_RedHots_Pyro, "MF RedHots Pyro"},
		    {SosigEnemyID.MF_RedHots_Scout, "MF RedHots Scout"},
		    {SosigEnemyID.MF_RedHots_Sniper, "MF RedHots Sniper"},
		    {SosigEnemyID.MF_RedHots_Soldier, "MF RedHots Soldier"},
		    {SosigEnemyID.MF_RedHots_Spy, "MF RedHots Spy"},    
		    {SosigEnemyID.MF_BlueFranks_Demo, "MF BlueFranks Demoman"},
		    {SosigEnemyID.MF_BlueFranks_Engineer, "MF BlueFranks Engineer"},
		    {SosigEnemyID.MF_BlueFranks_Heavy, "MF BlueFranks Heavy"},
		    {SosigEnemyID.MF_BlueFranks_Medic, "MF BlueFranks Medic"},
		    {SosigEnemyID.MF_BlueFranks_Pyro, "MF BlueFranks Pyro"},
		    {SosigEnemyID.MF_BlueFranks_Scout, "MF BlueFranks Scout"},
		    {SosigEnemyID.MF_BlueFranks_Sniper, "MF BlueFranks Sniper"},	    
		    {SosigEnemyID.MF_BlueFranks_Soldier, "MF BlueFranks Soldier"},
		    {SosigEnemyID.MF_BlueFranks_Spy, "MF BlueFranks Spy"},
		    {SosigEnemyID.Gladiator_Hoplite, "Gladiator Hoplite"},
		    {SosigEnemyID.Gladiator_Maximus, "Gladiator Maximus"},
		    {SosigEnemyID.Gladiator_Murmillo, "Gladiator Murmillo"},
		    {SosigEnemyID.Gladiator_Porcus, "Gladiator Porcus"},
		    {SosigEnemyID.Gladiator_Secutor, "Gladiator Secutor"},
		    {SosigEnemyID.Gladiator_Thraex, "Gladiator Thraex"},
		    {SosigEnemyID.MountainMeat_Melee, "Mountain Meat Melee"},
		    {SosigEnemyID.MountainMeat_Pistol, "Mountain Meat Pistol"},
		    {SosigEnemyID.MountainMeat_Rifle, "Mountain Meat Rifle"},
		    {SosigEnemyID.MountainMeat_Shotgun, "Mountain Meat Shotgun"},
		    {SosigEnemyID.MountainMeat_SMG, "Mountain Meat SMG"},	    
		    {SosigEnemyID.RW_Beefkicker, "RW Beefkicker"},
		    {SosigEnemyID.RW_Boner, "RW Boner"},
		    {SosigEnemyID.RW_Driller, "RW Driller"},
		    {SosigEnemyID.RW_Floater, "RW Floater"},
		    {SosigEnemyID.RW_FunGuy, "RW Fun Guy"},
		    {SosigEnemyID.RW_HamFister, "RW Ham Fister"},
		    {SosigEnemyID.RW_Lemonhead, "RW Lemonhead"},
		    {SosigEnemyID.RW_OldSmokey, "RW Old Smokey"},
		    {SosigEnemyID.RW_Pig, "RW Pig"},
		    {SosigEnemyID.RW_Prick, "RW Prick"},
		    {SosigEnemyID.RW_RedLumberjack, "RW Red Lumberjack"},		    
		    {SosigEnemyID.RW_Rot, "RW Rot"},    
		    {SosigEnemyID.RW_Spurter, "RW Spurter"},	    
		    {SosigEnemyID.RW_TheHung, "RW The Hung"},	    
		    {SosigEnemyID.RWP_Cultist, "RW Cultist"},
		    {SosigEnemyID.RWP_PacSquad_Commander, "RW PacSquad Commander"},
		    {SosigEnemyID.RWP_PacSquad_Flanker, "RW PacSquad Flanker"},
		    {SosigEnemyID.RWP_PacSquad_Sniper, "RW PacSquad Sniper"},
		    {SosigEnemyID.RWP_PacSquad_Trooper, "RW PacSquad Trooper"},
		    {SosigEnemyID.RWP_Prospector_Bar, "RW Prospector Bar"},
		    {SosigEnemyID.RWP_Prospector_Pistol, "RW Prospector Pistol"},
		    {SosigEnemyID.RWP_Prospector_Rifle, "RW Prospector Rifle"},
		    {SosigEnemyID.RWP_Prospector_Shotgun, "RW Prospector Shotgun"},
		    {SosigEnemyID.RWP_Skulker_Pistol, "RW Skulker Pistol"},
		    {SosigEnemyID.RWP_Skulker_Rifler, "RW Skulker Rifler"},
		    {SosigEnemyID.RWP_Skulker_Shotgun, "RW Skulker Shotgun"},
		    {SosigEnemyID.RWNPC_00, "RW NPC 00"},
		    {SosigEnemyID.RWNPC_01, "RW NPC 01"},
		    {SosigEnemyID.RWNPC_02, "RW NPC 02"},
		    {SosigEnemyID.RWNPC_03, "RW NPC 03"},
		    {SosigEnemyID.RWNPC_04, "RW NPC 04"},
		    {SosigEnemyID.RWNPC_05, "RW NPC 05"},
		    {SosigEnemyID.RWNPC_06, "RW NPC 06"},
		    {SosigEnemyID.RWNPC_07, "RW NPC 07"},
		    {SosigEnemyID.RWNPC_08, "RW NPC 08"},
		    {SosigEnemyID.RWNPC_09, "RW NPC 09"},
		    {SosigEnemyID.RWNPC_10, "RW NPC 10"},
		    {SosigEnemyID.RWNPC_11, "RW NPC 11"},
		    {SosigEnemyID.RWNPC_12, "RW NPC 12"},
		    {SosigEnemyID.RWNPC_13, "RW NPC 13"},
		    {SosigEnemyID.RWNPC_14, "RW NPC 14"},
		    {SosigEnemyID.RWNPC_15, "RW NPC 15"},
		    {SosigEnemyID.RWNPC_16, "RW NPC 16"},
		    {SosigEnemyID.RWNPC_17, "RW NPC 17"},
		    {SosigEnemyID.RWNPC_18, "RW NPC 18"},
		    {SosigEnemyID.RWNPC_19, "RW NPC 19"},   
		    {SosigEnemyID.RWNPC_20, "RW NPC 20"},
		    {SosigEnemyID.RWNPC_21, "RW NPC 21"},
		    {SosigEnemyID.Junkbot_Broken, "Junkbot Broken"},
		    {SosigEnemyID.Junkbot_Patrol, "Junkbot Patrol"},
		    {SosigEnemyID.Junkbot_Sniper, "Junkbot Sniper"},
		    {SosigEnemyID.Junkbot_Rocket, "Junkbot Rocket"},
		    {SosigEnemyID.Junkbot_Pistoler, "Junkbot Pistoler"},
		    {SosigEnemyID.Junkbot_Soldier, "Junkbot Soldier"},
		    {SosigEnemyID.Junkbot_Flamer, "Junkbot Flamer"},
		    {SosigEnemyID.Junkbot_Heavy, "Junkbot Heavy"},
		    {SosigEnemyID.Junkbot_ElfBroken, "Junkbot Elf Broken"},
		    {SosigEnemyID.MG_Soldier_Artic_Rifle, "MG Soldier Artic Rifle"},
		    {SosigEnemyID.MG_Soldier_Chemwar_Rifle, "MG Soldier Chemwar Rifle"},
		    {SosigEnemyID.MG_Soldier_Heavy_Rifle, "MG Soldier Heavy Rifle"},
		    {SosigEnemyID.MG_Soldier_Heavy_Shield, "MG Soldier Heavy Shield"},
		    {SosigEnemyID.MG_Soldier_LInfantry_Rifle, "MG Soldier Infantry Rifle"},
		    {SosigEnemyID.MG_Soldier_LInfantry_Shotgun, "MG Soldier Infantry Shotgun"},
		    {SosigEnemyID.MG_Special_Boss, "MG Special Boss"},
		    {SosigEnemyID.MG_Special_Duelist, "MG Special Duelist"},
		    {SosigEnemyID.MG_Special_Ninja, "MG Special Ninja"},
		    {SosigEnemyID.MG_Special_Sniper, "MG Special Sniper"},
		    {SosigEnemyID.MG_Special_Support, "MG Special Support"},
		    {SosigEnemyID.MG_Special_Telekine, "MG Special Telekine"},
		    {SosigEnemyID.MG_Special_Steak, "MG Special Steak"}
		};

		// Use this for initialization
		public void Start () 
		{
			dog_spawners = dog_spawners_parent.GetComponentsInChildren<SosigSpawnPoint>();
			grunt_spawners = grunt_spawners_parent.GetComponentsInChildren<SosigSpawnPoint>();

			UpdateText();	
		}

		public void Spawn()
		{
			if(random_spawns)
			{
				foreach(SosigSpawnPoint spawn_point in dog_spawners)
				{
					spawn_point.SosigType = (SosigEnemyID)enum_array.GetValue(random.Next(enum_array.Length));
					spawn_point.Spawn();
				}

				foreach(SosigSpawnPoint spawn_point in grunt_spawners)
				{
					spawn_point.SosigType = (SosigEnemyID)enum_array.GetValue(random.Next(enum_array.Length));
					spawn_point.Spawn();
				}
			}
			else
			{
				foreach(SosigSpawnPoint spawn_point in dog_spawners)
				{
					spawn_point.SosigType = current_dog_sosig;
					spawn_point.Spawn();
				}

				foreach(SosigSpawnPoint spawn_point in grunt_spawners)
				{
					spawn_point.SosigType = current_trooper_sosig;
					spawn_point.Spawn();
				}
			}
		}

		
		public void UpdateText()
		{
			trooper_sosig_text.text = enemy_name_parser[current_trooper_sosig];
			dog_sosig_text.text = enemy_name_parser[current_dog_sosig];
		}

		public void TrooperPlusPlus()
		{
			current_trooper_index += 10;

			if(current_trooper_index >= enum_array.Length)
			{
				current_trooper_index -= enum_array.Length;
			}

			current_trooper_sosig = (SosigEnemyID)enum_array.GetValue(current_trooper_index);

			UpdateText();
		}

		public void TrooperPlus()
		{
			current_trooper_index ++;

			if(current_trooper_index >= enum_array.Length)
			{
				current_trooper_index -= enum_array.Length;
			}

			current_trooper_sosig = (SosigEnemyID)enum_array.GetValue(current_trooper_index);

			UpdateText();
		}

		public void TrooperMinus()
		{
			current_trooper_index --;

			if(current_trooper_index < 0)
			{
				current_trooper_index += enum_array.Length;
			}

			current_trooper_sosig = (SosigEnemyID)enum_array.GetValue(current_trooper_index);
			UpdateText();
		}

		public void TrooperMinusMinus()
		{
			current_trooper_index -= 10;

			if(current_trooper_index < 0)
			{
				current_trooper_index += enum_array.Length;
			}

			current_trooper_sosig = (SosigEnemyID)enum_array.GetValue(current_trooper_index);
			UpdateText();
		}
	
		public void DogPlusPlus()
		{
			current_dog_index += 10;

			if(current_dog_index >= enum_array.Length)
			{
				current_dog_index -= enum_array.Length;
			}

			current_dog_sosig = (SosigEnemyID)enum_array.GetValue(current_dog_index);
			UpdateText();
		}

		public void DogPlus()
		{
			current_dog_index ++;

			if(current_dog_index >= enum_array.Length)
			{
				current_dog_index -= enum_array.Length;
			}

			current_dog_sosig = (SosigEnemyID)enum_array.GetValue(current_dog_index);
			UpdateText();
		}

		public void DogMinus()
		{
			current_dog_index --;

			if(current_dog_index < 0)
			{
				current_dog_index += enum_array.Length;
			}

			current_dog_sosig = (SosigEnemyID)enum_array.GetValue(current_dog_index);
			UpdateText();
		}

		public void DogMinusMinus()
		{
			current_dog_index -= 10;

			if(current_dog_index < 0)
			{
				current_dog_index += enum_array.Length;
			}

			current_dog_sosig = (SosigEnemyID)enum_array.GetValue(current_dog_index);
			UpdateText();
		}

		public void TimedOn()
		{
			timed_run = true;
			timed_on_button.interactable = false;
			timed_off_button.interactable = true;
		}

		public void TimedOff()
		{
			timed_run = false;
			timed_on_button.interactable = true;
			timed_off_button.interactable = false;
		}

		public void RandomOn()
		{
			random_spawns = true;
			random_on_button.interactable = false;
			random_off_button.interactable = true;
		}

		public void RandomOff()
		{
			random_spawns = false;
			random_on_button.interactable = true;
			random_off_button.interactable = false;	
		}
/*
		public void SecretOverrideOn()
		{
			override_secrets = true;
			random_on_button.interactable = false;

			
		}*/

		public void StartTimer()
		{
			if(timed_run)
			{
				timer_started = true;
			}
		}

		public void StopTimer()
		{
			if(timed_run)
			{
				timer_started = false;
			}
		}

		public void Update()
		{
			if(timer_started)
			{
				timer_seconds += Time.deltaTime;

				TimeSpan duration = TimeSpan.FromSeconds(timer_seconds);

				foreach(Text timer_text in timer_texts)
				{
					String timer_text_str = String.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", duration.Hours, duration.Minutes, duration.Seconds, duration.Milliseconds);
					
					timer_text.text = timer_text_str;
				}
			}
		}
	}
}