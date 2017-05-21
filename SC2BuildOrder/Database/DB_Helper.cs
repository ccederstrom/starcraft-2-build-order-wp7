using System;
using System.Net;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SC2BuildOrder
{
	public class DB_Helper
	{
		public const int UNIT = 0;
		public const int BUILDING = 1;
		public const int UPGRADE = 2;
		public const int ABILITY = 3;

		// Data context for the local database
		//private static Row.GPSDataContext DB;
		private static Index.DataContext_Index DB_Index;
		private static Build_Order.DataContext_Build_Order DB_Build_Order;
		private static Object_SC.DataContext_Object_SC DB_Object_SC;

		// Connect to the database and instantiate data context.
		public static void connect()
		{
			//DB = new Row.GPSDataContext(Row.GPSDataContext.DBConnectionString);
			DB_Index = new Index.DataContext_Index(Index.DataContext_Index.DBConnectionString);
			DB_Build_Order = new Build_Order.DataContext_Build_Order(Build_Order.DataContext_Build_Order.DBConnectionString);
			DB_Object_SC = new Object_SC.DataContext_Object_SC(Object_SC.DataContext_Object_SC.DBConnectionString);
		}

		//public static ObservableCollection<Row> getAllRows()
		//{
		//    var InDB = from Row todo in DB.Rows select todo;
		//    return (new ObservableCollection<Row>(InDB));
		//}
		//public static ObservableCollection<Row> getRowsbyTitle(String title)
		//{
		//    var edit_query = from Row todo in DB.Rows where todo.Title == title select todo;
		//    return (new ObservableCollection<Row>(edit_query));
		//}
		//public static void deleteRow(Row temp)
		//{
		//    DB.Rows.DeleteOnSubmit(temp);
		//    DB.SubmitChanges();
		//}
		//public static void insertRow(Row temp)
		//{
		//    DB.Rows.InsertOnSubmit(temp);
		//    DB.SubmitChanges();
		//}

		/////////////OBJECT_SC TABLE//////////////////////
		public static void insertObject(string name, string race, int type, int food, int mineral, int gas, int time, String path, String strong1, String strong2, String strong3, String weak1, String weak2, String weak3)
		{
			Object_SC obj = new Object_SC(name, race, type, food, mineral, gas, time, path, strong1, strong2, strong3,  weak1, weak2, weak3);
			DB_Object_SC.Object_SC_Table.InsertOnSubmit(obj);
			DB_Object_SC.SubmitChanges();
		}
		public static ObservableCollection<Object_SC> get_Object_SC_by_Type_and_Race(String race, int type)
		{
			var InDB = from Object_SC obj in DB_Object_SC.Object_SC_Table where obj.Type == type && obj.Race == race select obj;
			return (new ObservableCollection<Object_SC>(InDB));
		}
		public static ObservableCollection<Object_SC> get_Object_SC_by_Race(String race)
		{
			var InDB = from Object_SC obj in DB_Object_SC.Object_SC_Table where obj.Race == race select obj;
			return (new ObservableCollection<Object_SC>(InDB));
		}
		public static ObservableCollection<Object_SC> get_Object_SC_by_Obj_Id(int obj_id)
		{
			var InDB = from Object_SC obj in DB_Object_SC.Object_SC_Table where obj.Obj_Id == obj_id select obj;
			return (new ObservableCollection<Object_SC>(InDB));
		}

		public static ObservableCollection<Object_SC> get_Object_SC_by_Name(String Objname)
		{
			var InDB = from Object_SC obj in DB_Object_SC.Object_SC_Table where obj.Name == Objname select obj;
			return (new ObservableCollection<Object_SC>(InDB));
		}
		////////////BUILD_ORDERS TABLE//////////////////////
		public static void insertBuild(int index_id, int order, int obj_id, string when, string note)
		{
			Build_Order obj = new Build_Order(index_id, order, obj_id, when, note);
			DB_Build_Order.Build_Order_Table.InsertOnSubmit(obj);
			DB_Build_Order.SubmitChanges();
		}
		////////////INDEX TABLE//////////////////////////////////
		public static void createIndex(String name, String des,String race, String author, String note, String notefooter)
		{
			Index ind = new Index(name, des, race, author, note, notefooter);
			DB_Index.Index_Table.InsertOnSubmit(ind);
			DB_Index.SubmitChanges();
		}

		public static ObservableCollection<Index> get_Index_By_Name(String name)
		{
			var InDB = from Index ind in DB_Index.Index_Table where ind.Name == name select ind;
			return (new ObservableCollection<Index>(InDB));
		}

		public static ObservableCollection<Index> get_Index_By_Author(String author, String race)
		{
			var InDB = from Index ind in DB_Index.Index_Table where ind.Author == author && ind.Race==race select ind;
			return (new ObservableCollection<Index>(InDB));
		}

		public static ObservableCollection<Index> get_Index_Built_In(String race)
		{
			var InDB = from Index ind in DB_Index.Index_Table where ind.Author != "Custom" && ind.Race==race select ind;
			return (new ObservableCollection<Index>(InDB));
		}
		//////////////BUILD ORDER TABLE/////////////////////////////
		public static ObservableCollection<Build_Order> get_Build_Order_By_Index_Id(int index_id)
		{
			var InDB = from Build_Order ind in DB_Build_Order.Build_Order_Table where ind.Index_Id == index_id orderby ind.Order select ind;
			return (new ObservableCollection<Build_Order>(InDB));
		}
		//////////////////////////////////////////////////
		//Enter all Units, Buildings, and Upgrades to Database
		public static void enterObjectSC_Information()
		{
			DB_Helper.connect();


			string zerg_unit_path = "/SC2BuildOrder;component/Images/Zerg/Units/";
			string zerg_structure_path = "/SC2BuildOrder;component/Images/Zerg/Structures/";
			string zerg_upgrade_path = "/SC2BuildOrder;component/Images/Zerg/Upgrades/";
			string zerg_ability_path = "/SC2BuildOrder;component/Images/Zerg/Ability/";


			string protoss_units_path = "/SC2BuildOrder;component/Images/Protoss/Units/";
			string protoss_structure_path = "/SC2BuildOrder;component/Images/Protoss/Structures/";
			string protoss_upgrade_path = "/SC2BuildOrder;component/Images/Protoss/Upgrades/";
			string protoss_ability_path = "/SC2BuildOrder;component/Images/Protoss/Ability/";

			string terran_unit_path = "/SC2BuildOrder;component/Images/Terran/Units/";
			string terran_structure_path = "/SC2BuildOrder;component/Images/Terran/Structures/";
			string terran_upgrade_path = "/SC2BuildOrder;component/Images/Terran/Upgrades/";


			#region Zerg
			string ZERG = "Zerg";
			#region Zerg Units
			// ZERG Units
			insertObject("Drone", "Zerg", DB_Helper.UNIT, 1, 50, 0, 17, zerg_unit_path + "Unofficial/drone.jpg", terran_unit_path + "", protoss_units_path +"", zerg_unit_path + "", terran_unit_path+"reaper.png", protoss_units_path+"dark_templar.png", zerg_unit_path+"baneling.png");
			insertObject("Queen", "Zerg", DB_Helper.UNIT, 2, 150, 0, 50, zerg_unit_path + "Unofficial/queen.jpg", terran_unit_path + "hellion.png", protoss_units_path + "void_ray.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "marine.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png");
			insertObject("Zergling", "Zerg", DB_Helper.UNIT, 1, 50, 0, 24, zerg_unit_path + "Unofficial/zergling.jpg", terran_unit_path + "marauder.png", protoss_units_path + "stalker.png", zerg_unit_path + "hydralisk.png", terran_unit_path + "hellion.png", protoss_units_path + "colossus.png", zerg_unit_path + "baneling.png");
			insertObject("Baneling", "Zerg", DB_Helper.UNIT, 0, 25, 25, 20, zerg_unit_path + "Unofficial/baneling.jpg", terran_unit_path + "marine.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png", terran_unit_path + "marauder.png", protoss_units_path + "stalker.png", zerg_unit_path + "roach.png");
			insertObject("Roach", "Zerg", DB_Helper.UNIT, 2, 75, 25, 27, zerg_unit_path + "Unofficial/roach.jpg", terran_unit_path + "hellion.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png", terran_unit_path + "marauder.png", protoss_units_path + "immortal.png", zerg_unit_path + "ultralisk.png");
			insertObject("Hydralisk", "Zerg", DB_Helper.UNIT, 2, 100, 50, 33, zerg_unit_path + "Unofficial/hydralisk.jpg", terran_unit_path + "banshee.png", protoss_units_path + "void_ray.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "siegetank.png", protoss_units_path + "colossus.png", zerg_unit_path + "zergling.png");
			insertObject("Mutalisk", "Zerg", DB_Helper.UNIT, 2, 100, 100, 33, zerg_unit_path + "Unofficial/mutalisk.jpg", terran_unit_path + "viking.png", protoss_units_path + "void_ray.png", zerg_unit_path + "broodlord.png", terran_unit_path + "thor.png", protoss_units_path + "phoenix.png", zerg_unit_path + "corruptor.png");
			insertObject("Corruptor", "Zerg", DB_Helper.UNIT, 2, 150, 100, 40, zerg_unit_path + "Unofficial/corruptor.jpg", terran_unit_path + "battlecruiser.png", protoss_units_path + "phoenix.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "viking.png", protoss_units_path + "void_ray.png", zerg_unit_path + "hydralisk.png");
			insertObject("Infestor", "Zerg", DB_Helper.UNIT, 2, 100, 150, 50, zerg_unit_path + "Unofficial/infestor.jpg", terran_unit_path + "marine.png", protoss_units_path + "immortal.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "ghost.png", protoss_units_path + "high_templar.png", zerg_unit_path + "ultralisk.png");
			insertObject("Ultralisk", "Zerg", DB_Helper.UNIT, 6, 300, 200, 70, zerg_unit_path + "Unofficial/ultralisk.jpg", terran_unit_path + "marauder.png", protoss_units_path + "stalker.png", zerg_unit_path + "roach.png", terran_unit_path + "banshee.png", protoss_units_path + "void_ray.png", zerg_unit_path + "mutalisk.png");
			insertObject("Brood Lord", "Zerg", DB_Helper.UNIT, 2, 150, 150, 34, zerg_unit_path + "Unofficial/brood_lord.jpg", terran_unit_path + "marine.png", protoss_units_path + "stalker.png", zerg_unit_path + "hydralisk.png", terran_unit_path + "viking.png", protoss_units_path + "void_ray.png", zerg_unit_path + "corruptor.png");
			insertObject("Overlord", "Zerg", DB_Helper.UNIT, 0, 100, 0, 25, zerg_unit_path + "Unofficial/overlord.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "viking.png", protoss_units_path + "phoenix.png", zerg_unit_path + "corruptor.png");
			insertObject("Overseer", "Zerg", DB_Helper.UNIT, 0, 50, 100, 17, zerg_unit_path + "Unofficial/overseer.jpg", terran_unit_path + "banshee.png", protoss_units_path + "dark_templar.png", zerg_unit_path + "roach.png", terran_unit_path + "viking.png", protoss_units_path + "stalker.png", zerg_unit_path + "mutalisk.png");
			#endregion

			#region Zerg Buildings
			//ZERG Builings
			
			insertObject("Hatchery", ZERG, DB_Helper.BUILDING, 0, 300, 0, 100, zerg_structure_path + "hatchery.jpg","","","","","","");
			insertObject("Lair", ZERG, DB_Helper.BUILDING, 0, 150, 150, 80, zerg_structure_path + "lair.jpg", "", "", "", "", "", "");
			insertObject("Hive", ZERG, DB_Helper.BUILDING, 0, 200, 150, 100, zerg_structure_path + "hive.jpg", "", "", "", "", "", "");
			insertObject("Extractor", ZERG, DB_Helper.BUILDING, 1, 25, 0, 30, zerg_structure_path + "extractor.jpg", "", "", "", "", "", "");
			insertObject("Spawning Pool", ZERG, DB_Helper.BUILDING, 1, 200, 0, 65, zerg_structure_path + "spawning_pool.jpg", "", "", "", "", "", "");
			insertObject("Nydus Network", ZERG, DB_Helper.BUILDING, 1, 150, 200, 50, zerg_structure_path + "nydus_network.jpg", "", "", "", "", "", "");
			insertObject("Spine Crawler", ZERG, DB_Helper.BUILDING, 1, 100, 0, 50, zerg_structure_path + "spine_crawler.jpg", "", "", "", "", "", "");
			insertObject("Spore Crawler", ZERG, DB_Helper.BUILDING, 1, 75, 0, 30, zerg_structure_path + "spore_crawler.jpg", "", "", "", "", "", "");
			insertObject("Evolution Chamber", ZERG, DB_Helper.BUILDING, 1, 75, 0, 35, zerg_structure_path + "evolution_chamber.jpg", "", "", "", "", "", "");
			insertObject("Hydralisk Den", ZERG, DB_Helper.BUILDING, 1, 100, 100, 40, zerg_structure_path + "hydralisk_den.jpg", "", "", "", "", "", "");
			insertObject("Infestation Pit", ZERG, DB_Helper.BUILDING, 1, 100, 100, 50, zerg_structure_path + "infestation_pit.jpg", "", "", "", "", "", "");
			insertObject("Ultralisk Cavern", ZERG, DB_Helper.BUILDING, 1, 150, 200, 65, zerg_structure_path + "ultralisk_cavern.jpg", "", "", "", "", "", "");
			//insertObject("Creep Tumor", ZERG, DB_Helper.BUILDING, 1, 0, 0, 15, zerg_structure_path + "creep_tumor.jpg", "", "", "", "", "", "");
			insertObject("Nydus Worm", "Zerg", DB_Helper.BUILDING, 0, 100, 100, 20, zerg_structure_path + "nydus_worm.jpg", "", "", "", "", "", "");
			insertObject("Baneling Nest", ZERG, DB_Helper.BUILDING, 1, 100, 50, 60, zerg_structure_path + "baneling_nest.jpg", "", "", "", "", "", "");
			insertObject("Roach Warren", ZERG, DB_Helper.BUILDING, 1, 150, 0, 55, zerg_structure_path + "roach_warren.jpg", "", "", "", "", "", "");
			insertObject("Spire", ZERG, DB_Helper.BUILDING, 1, 200, 150, 120, zerg_structure_path + "spire.jpg", "", "", "", "", "", "");
			insertObject("Greater Spire", ZERG, DB_Helper.BUILDING, 0, 100, 150, 100, zerg_structure_path + "greater_spire.jpg", "", "", "", "", "", ""); // depends on spire
			#endregion

			#region Zerg Upgrades
			//Zerg Upgrades
			insertObject("Adrenal Glands", "Zerg", DB_Helper.UPGRADE, 0, 200, 200, 130, zerg_upgrade_path + "adrenalglands.png", "", "", "", "", "", "");
			insertObject("Chitinous Plating", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 110, zerg_upgrade_path + "chitinousplating.png", "", "", "", "", "", "");
			insertObject("Ground Carapace Level 1", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 160, zerg_upgrade_path + "groundcarapace-level1.png", "", "", "", "", "", "");
			insertObject("Ground Carapace Level 2", "Zerg", DB_Helper.UPGRADE, 0, 225, 225, 190, zerg_upgrade_path + "groundcarapace-level2.png", "", "", "", "", "", "");
			insertObject("Ground Carapace Level 3", "Zerg", DB_Helper.UPGRADE, 0, 300, 300, 220, zerg_upgrade_path + "groundcarapace-level3.png", "", "", "", "", "", "");
			insertObject("Melee Attacks Level 0", "Zerg", DB_Helper.UPGRADE, 0, 0, 0, 0, zerg_upgrade_path + "meleeattacks-level0.png", "", "", "", "", "", "");
			insertObject("Melee Attacks Level 1", "Zerg", DB_Helper.UPGRADE, 0, 100, 100, 160, zerg_upgrade_path + "meleeattacks-level1.png", "", "", "", "", "", "");
			insertObject("Melee Attacks Level 2", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 190, zerg_upgrade_path + "meleeattacks-level2.png", "", "", "", "", "", "");
			insertObject("Melee Attacks Level 3", "Zerg", DB_Helper.UPGRADE, 0, 200, 200, 220, zerg_upgrade_path + "meleeattacks-level3.png", "", "", "", "", "", "");
			insertObject("Missile Attacks Level 0", "Zerg", DB_Helper.UPGRADE, 0, 0, 0, 0, zerg_upgrade_path + "missileattacks-level0.png", "", "", "", "", "", "");
			insertObject("Missile Attacks Level 1", "Zerg", DB_Helper.UPGRADE, 0, 100, 100, 160, zerg_upgrade_path + "missileattacks-level1.png", "", "", "", "", "", "");
			insertObject("Missile Attacks Level 2", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 190, zerg_upgrade_path + "missileattacks-level2.png", "", "", "", "", "", "");
			insertObject("Missile Attacks Level 3", "Zerg", DB_Helper.UPGRADE, 0, 200, 200, 220, zerg_upgrade_path + "missileattacks-level3.png", "", "", "", "", "", "");
			insertObject("Pathogen Glands", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 80, zerg_upgrade_path + "pathogenglands.png", "", "", "", "", "", "");
            insertObject("Pneumatized Carapace", "Zerg", DB_Helper.UPGRADE, 0, 100, 100, 60, zerg_upgrade_path + "pneumatizedcarapace.png", "", "", "", "", "", "");
            insertObject("Tunneling Claws", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 110, zerg_upgrade_path + "tunnelingclaws.png", "", "", "", "", "", "");
            insertObject("Ventral Sacs", "Zerg", DB_Helper.UPGRADE, 0, 200, 200, 130, zerg_upgrade_path + "ventralsacs.png", "", "", "", "", "", "");

			insertObject("Metabolic Boost", "Zerg", DB_Helper.UPGRADE, 0, 100, 100, 110, zerg_upgrade_path + "metabolicboost.png", "", "", "", "", "", "");
			insertObject("Centrifugal Hooks", ZERG, DB_Helper.UPGRADE, 0, 150, 150, 110, zerg_upgrade_path + "centrifugalhooks.png", "", "", "", "", "", "");
			insertObject("Glial Reconstitution", ZERG, DB_Helper.UPGRADE, 0, 100, 100, 110, zerg_upgrade_path + "glialreconstitution.png", "", "", "", "", "", ""); // roach warren
			insertObject("Tunneling Claws", ZERG, DB_Helper.UPGRADE, 0, 150, 150, 110, zerg_upgrade_path + "tunnelingclaws.png", "", "", "", "", "", ""); // roach warren
		  //  insertObject("Flyer Attacks Level 1", "Zerg", DB_Helper.UPGRADE, 0, 100, 100, 160, zerg_upgrade_path + "airattacks-level0.png", "", "", "", "", "", ""); // Spire
			insertObject("Flyer Attacks Level 1", "Zerg", DB_Helper.UPGRADE, 0, 100, 100, 160, zerg_upgrade_path + "airattacks-level1.png", "", "", "", "", "", ""); // Spire
			insertObject("Flyer Attacks Level 2", "Zerg", DB_Helper.UPGRADE, 0, 175, 175, 190, zerg_upgrade_path + "airattacks-level2.png", "", "", "", "", "", ""); // Spire
			insertObject("Flyer Attacks Level 3", "Zerg", DB_Helper.UPGRADE, 0, 250, 250, 220, zerg_upgrade_path + "airattacks-level3.png", "", "", "", "", "", ""); // Spire
			insertObject("Flyer Carapace Level 1", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 160, zerg_upgrade_path + "flyercarapace-level1.png", "", "", "", "", "", ""); // Spire
			insertObject("Flyer Carapace Level 2", "Zerg", DB_Helper.UPGRADE, 0, 225, 225, 190, zerg_upgrade_path + "flyercarapace-level2.png", "", "", "", "", "", ""); // Spire
			insertObject("Flyer Carapace Level 3", "Zerg", DB_Helper.UPGRADE, 0, 300, 300, 220, zerg_upgrade_path + "flyercarapace-level3.png", "", "", "", "", "", ""); // Spire
			insertObject("Grooved Spines", "Zerg", DB_Helper.UPGRADE, 0, 150, 150, 80, zerg_upgrade_path + "groovedspines.png", "", "", "", "", "", "");
			#endregion

			#region Zerg Ability
			insertObject("Burrow", "Zerg", DB_Helper.ABILITY, 0, 0, 0, 0, zerg_ability_path + "burrow.png", "", "", "", "", "", "");
			#endregion
			#endregion

			#region Protoss
			string PROTOSS = "Protoss";

			#region Protoss Ability


			insertObject("Blink", PROTOSS, DB_Helper.ABILITY, 0, 0, 0, 0, protoss_ability_path + "blink.png", "", "", "", "", "", "");
			insertObject("Chrono Boost", PROTOSS, DB_Helper.ABILITY, 0, 0, 0, 0, protoss_ability_path + "chrono_boost.png", "", "", "", "", "", "");
			#endregion

			#region Protoss Units
			// PROTOSS Units
			
			//http://wiki.teamliquid.net/starcraft2/Units

			insertObject("Probe", PROTOSS, DB_Helper.UNIT, 1, 50, 0, 17, protoss_units_path + "Unofficial/probe.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "reaper.png", protoss_units_path + "dark_templar.png", zerg_unit_path + "baneling.png");
			insertObject("Zealot", PROTOSS, DB_Helper.UNIT, 2, 100, 0, 38, protoss_units_path + "Unofficial/zealot.jpg", terran_unit_path + "marauder.png", protoss_units_path + "immortal.png", zerg_unit_path + "zergling.png", terran_unit_path + "hellion.png", protoss_units_path + "colossus.png", zerg_unit_path + "roach.png");
			insertObject("Stalker", PROTOSS, DB_Helper.UNIT, 2, 125, 50, 42, protoss_units_path + "Unofficial/stalker.jpg", terran_unit_path + "reaper.png", protoss_units_path + "void_ray.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "marauder.png", protoss_units_path + "immortal.png", zerg_unit_path + "zergling.png");
			insertObject("Sentry", PROTOSS, DB_Helper.UNIT, 2, 50, 100, 37, protoss_units_path + "Unofficial/sentry.jpg", terran_unit_path + "", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png", terran_unit_path + "reaper.png", protoss_units_path + "stalker.png", zerg_unit_path + "hydralisk.png");
			insertObject("High Templar", PROTOSS, DB_Helper.UNIT, 2, 50, 150, 55, protoss_units_path + "Unofficial/high_templar.jpg", terran_unit_path + "marine.png", protoss_units_path + "sentry.png", zerg_unit_path + "hydralisk.png", terran_unit_path + "ghost.png", protoss_units_path + "colossus.png", zerg_unit_path + "roach.png");
			insertObject("Dark Templar", PROTOSS, DB_Helper.UNIT, 2, 125, 125, 55, protoss_units_path + "Unofficial/dark_templar.jpg", terran_unit_path + "scv.png", protoss_units_path + "probe.png", zerg_unit_path + "drone.png", terran_unit_path + "raven.png", protoss_units_path + "observer.png", zerg_unit_path + "overseer.png");
			insertObject("Archon", PROTOSS, DB_Helper.UNIT, 0, 0, 0, 12, protoss_units_path + "Unofficial/archon.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "mutalisk.png", terran_unit_path + "thor.png", protoss_units_path + "immortal.png", zerg_unit_path + "ultralisk.png"); // depends on two high templars, enable if two hight templars in queue
            insertObject("Immortal", PROTOSS, DB_Helper.UNIT, 4, 250, 100, 55, protoss_units_path + "Unofficial/immortal.jpg", terran_unit_path + "siegetank.png", protoss_units_path + "stalker.png", zerg_unit_path + "roach.png", terran_unit_path + "marine.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png");
			insertObject("Colossus", PROTOSS, DB_Helper.UNIT, 6, 300, 200, 75, protoss_units_path + "Unofficial/colossus.jpg", terran_unit_path + "marine.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png", terran_unit_path + "viking.png", protoss_units_path + "immortal.png", zerg_unit_path + "corruptor.png");
			insertObject("Observer", PROTOSS, DB_Helper.UNIT, 1, 25, 75, 40, protoss_units_path + "observer.png", terran_unit_path + "banshee.png", protoss_units_path + "dark_templar.png", zerg_unit_path + "roach.png", terran_unit_path + "raven.png", protoss_units_path + "observer.png", zerg_unit_path + "overseer.png");
			insertObject("Warp Prism", PROTOSS, DB_Helper.UNIT, 2, 200, 0, 50, protoss_units_path + "Unofficial/warp_prism.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "viking.png", protoss_units_path + "phoenix.png", zerg_unit_path + "corruptor.png");
			insertObject("Phoenix", PROTOSS, DB_Helper.UNIT, 2, 150, 100, 35, protoss_units_path + "Unofficial/phoenix.jpg", terran_unit_path + "banshee.png", protoss_units_path + "void_ray.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "battlecruiser.png", protoss_units_path + "carrier.png", zerg_unit_path + "corruptor.png");
			insertObject("Void Ray", PROTOSS, DB_Helper.UNIT, 3, 250, 150, 60, protoss_units_path + "Unofficial/void_ray.jpg", terran_unit_path + "battlecruiser.png", protoss_units_path + "carrier.png", zerg_unit_path + "corruptor.png", terran_unit_path + "viking.png", protoss_units_path + "phoenix.png", zerg_unit_path + "mutalisk.png");
			insertObject("Carrier", PROTOSS, DB_Helper.UNIT, 6, 350, 250, 120, protoss_units_path + "Unofficial/carrier.jpg", terran_unit_path + "thor.png", protoss_units_path + "phoenix.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "viking.png", protoss_units_path + "void_ray.png", zerg_unit_path + "corruptor.png");
			insertObject("Mothership", PROTOSS, DB_Helper.UNIT, 8, 400, 400, 160, protoss_units_path + "Unofficial/mothership.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "viking.png", protoss_units_path + "void_ray.png", zerg_unit_path + "corruptor.png");
			#endregion

			#region Protoss Buildings
			// Protoss building

			insertObject("Assimilator", PROTOSS, DB_Helper.BUILDING, 0, 75, 0, 30, protoss_structure_path + "assimilator.jpg", "", "", "", "", "", "");
			insertObject("Cybernetics Core", PROTOSS, DB_Helper.BUILDING, 0, 150, 0, 50, protoss_structure_path + "cybernetics_core.jpg", "", "", "", "", "", "");
			insertObject("Dark Shrine", PROTOSS, DB_Helper.BUILDING, 0, 100, 250, 100, protoss_structure_path + "dark_shrine.jpg", "", "", "", "", "", "");
			insertObject("Fleet Beacon", PROTOSS, DB_Helper.BUILDING, 0, 300, 200, 60, protoss_structure_path + "fleet_beacon.jpg", "", "", "", "", "", "");
			insertObject("Forge", PROTOSS, DB_Helper.BUILDING, 0, 150, 0, 45, protoss_structure_path + "forge.jpg", "", "", "", "", "", "");
			insertObject("Gateway", PROTOSS, DB_Helper.BUILDING, 0, 150, 0, 65, protoss_structure_path + "gateway.jpg", "", "", "", "", "", "");
			insertObject("Nexus", PROTOSS, DB_Helper.BUILDING, 0, 400, 0, 100, protoss_structure_path + "nexus.jpg", "", "", "", "", "", "");
			insertObject("Photon Cannon", PROTOSS, DB_Helper.BUILDING, 0, 150, 0, 40, protoss_structure_path + "photon_cannon.jpg", "", "", "", "", "", "");
			insertObject("Pylon", PROTOSS, DB_Helper.BUILDING, 0, 100, 0, 25, protoss_structure_path + "pylon.jpg", "", "", "", "", "", "");
			insertObject("Robotics Bay", PROTOSS, DB_Helper.BUILDING, 0, 200, 200, 65, protoss_structure_path + "robotics_bay.jpg", "", "", "", "", "", "");
			insertObject("Robotics Facility", PROTOSS, DB_Helper.BUILDING, 0, 200, 100, 65, protoss_structure_path + "robotics_facility.jpg", "", "", "", "", "", "");
			insertObject("Stargate", PROTOSS, DB_Helper.BUILDING, 0, 150, 150, 60, protoss_structure_path + "stargate.jpg", "", "", "", "", "", "");
			insertObject("Templar Archives", PROTOSS, DB_Helper.BUILDING, 0, 150, 200, 50, protoss_structure_path + "templar_archives.jpg", "", "", "", "", "", "");
			insertObject("Twilight Council", PROTOSS, DB_Helper.BUILDING, 0, 150, 100, 50, protoss_structure_path + "twilight_council.jpg", "", "", "", "", "", "");
			insertObject("Warp Gate", PROTOSS, DB_Helper.BUILDING, 0, 0, 0, 10, protoss_structure_path + "warpgate.jpg", "", "", "", "", "", "");
			#endregion

			#region Protoss Upgrades
			// Protoss upgrades
			insertObject("airarmorlevel1.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airarmorlevel1.png", "", "", "", "", "", "");
			insertObject("airarmorlevel2.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airarmorlevel2.png", "", "", "", "", "", "");
			insertObject("airarmorlevel3.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airarmorlevel3.png", "", "", "", "", "", "");
			insertObject("airweaponslevel0.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airweaponslevel0.png", "", "", "", "", "", "");
			insertObject("airweaponslevel1.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airweaponslevel1.png", "", "", "", "", "", "");
			insertObject("airweaponslevel2.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airweaponslevel2.png", "", "", "", "", "", "");
			insertObject("airweaponslevel3.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "airweaponslevel3.png", "", "", "", "", "", "");
			insertObject("extendedthermallance.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "extendedthermallance.png", "", "", "", "", "", "");
			insertObject("graviticbooster.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "graviticbooster.png", "", "", "", "", "", "");
			insertObject("graviticdrive.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "graviticdrive.png", "", "", "", "", "", "");
			insertObject("gravitoncatapult.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "gravitoncatapult.png", "", "", "", "", "", "");
			insertObject("groundarmorlevel1.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundarmorlevel1.png", "", "", "", "", "", "");
			insertObject("groundarmorlevel2.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundarmorlevel2.png", "", "", "", "", "", "");
			insertObject("groundarmorlevel3.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundarmorlevel3.png", "", "", "", "", "", "");
			insertObject("groundweaponslevel0.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundweaponslevel0.png", "", "", "", "", "", "");
			insertObject("groundweaponslevel1.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundweaponslevel1.png", "", "", "", "", "", "");
			insertObject("groundweaponslevel2.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundweaponslevel2.png", "", "", "", "", "", "");
			insertObject("groundweaponslevel3.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "groundweaponslevel3.png", "", "", "", "", "", "");
			insertObject("shieldslevel1.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "shieldslevel1.png", "", "", "", "", "", "");
			insertObject("shieldslevel2.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "shieldslevel2.png", "", "", "", "", "", "");
			insertObject("shieldslevel3.png 1", PROTOSS, DB_Helper.UPGRADE, 0, 0, 0, 0, protoss_upgrade_path + "shieldslevel3.png", "", "", "", "", "", "");

			#endregion
			#endregion

			#region Terran
			// Terran Units
			string TERRAN = "Terran";
			#region Terran Units
			insertObject("SCV", TERRAN, DB_Helper.UNIT, 1, 50, 0, 17, terran_unit_path + "Unofficial/scv.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "reaper.png", protoss_units_path + "dark_templar.png", zerg_unit_path + "baneling.png");
	  //      insertObject("Mule", TERRAN, DB_Helper.UNIT, 0, 0, 0, 0, terran_unit_path + "mule.png", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "");
			insertObject("Marine", TERRAN, DB_Helper.UNIT, 1, 50, 0, 25, terran_unit_path + "Unofficial/marine.jpg", terran_unit_path + "marauder.png", protoss_units_path + "immortal.png", zerg_unit_path + "hydralisk.png", terran_unit_path + "siegetank.png", protoss_units_path + "colossus.png", zerg_unit_path + "baneling.png");
			insertObject("Marauder", TERRAN, DB_Helper.UNIT, 2, 100, 25, 30, terran_unit_path + "Unofficial/marauder.jpg", terran_unit_path + "thor.png", protoss_units_path + "stalker.png", zerg_unit_path + "roach.png", terran_unit_path + "marine.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png");
			insertObject("Reaper", TERRAN, DB_Helper.UNIT, 1, 50, 50, 45, terran_unit_path + "Unofficial/reaper.jpg", terran_unit_path + "scv.png", protoss_units_path + "probe.png", zerg_unit_path + "drone.png", terran_unit_path + "marauder.png", protoss_units_path + "stalker.png", zerg_unit_path + "roach.png");
			insertObject("Ghost", TERRAN, DB_Helper.UNIT, 2, 200, 100, 40, terran_unit_path + "Unofficial/ghost.jpg", terran_unit_path + "raven.png", protoss_units_path + "high_templar.png", zerg_unit_path + "infestor.png", terran_unit_path + "marauder.png", protoss_units_path + "stalker.png", zerg_unit_path + "zergling.png");
			insertObject("Hellion", TERRAN, DB_Helper.UNIT, 2, 100, 0, 30, terran_unit_path + "Unofficial/hellion.jpg", terran_unit_path + "marine.png", protoss_units_path + "zealot.png", zerg_unit_path + "zergling.png", terran_unit_path + "marauder.png", protoss_units_path + "stalker.png", zerg_unit_path + "roach.png");
			insertObject("Siege Tank", TERRAN, DB_Helper.UNIT, 3, 150, 125, 45, terran_unit_path + "Unofficial/siege_tank.jpg", terran_unit_path + "marine.png", protoss_units_path + "stalker.png", zerg_unit_path + "hydralisk.png", terran_unit_path + "banshee.png", protoss_units_path + "immortal.png", zerg_unit_path + "mutalisk.png");
			insertObject("Thor", TERRAN, DB_Helper.UNIT, 6, 300, 200, 60, terran_unit_path + "Unofficial/thor.jpg", terran_unit_path + "marine.png", protoss_units_path + "stalker.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "marauder.png", protoss_units_path + "immortal.png", zerg_unit_path + "zergling.png");
			insertObject("Viking", TERRAN, DB_Helper.UNIT, 2, 150, 75, 42, terran_unit_path + "Unofficial/viking.jpg", terran_unit_path + "battlecruiser.png", protoss_units_path + "void_ray.png", zerg_unit_path + "corruptor.png", terran_unit_path + "marine.png", protoss_units_path + "stalker.png", zerg_unit_path + "mutalisk.png");
			insertObject("Medivac", TERRAN, DB_Helper.UNIT, 2, 100, 100, 42, terran_unit_path + "Unofficial/medivac.jpg", terran_unit_path + "", protoss_units_path + "", zerg_unit_path + "", terran_unit_path + "viking.png", protoss_units_path + "phoenix.png", zerg_unit_path + "corruptor.png");
			insertObject("Raven", TERRAN, DB_Helper.UNIT, 2, 100, 200, 60, terran_unit_path + "Unofficial/raven.jpg", terran_unit_path + "banshee.png", protoss_units_path + "dark_templar.png", zerg_unit_path + "roach.png", terran_unit_path + "ghost.png", protoss_units_path + "phoenix.png", zerg_unit_path + "corruptor.png");
			insertObject("Banshee", TERRAN, DB_Helper.UNIT, 3, 150, 100, 60, terran_unit_path + "Unofficial/banshee.jpg", terran_unit_path + "siegetank.png", protoss_units_path + "colossus.png", zerg_unit_path + "ultralisk.png", terran_unit_path + "viking.png", protoss_units_path + "phoenix.png", zerg_unit_path + "hydralisk.png");
			insertObject("Battlecruiser", TERRAN, DB_Helper.UNIT, 6, 400, 300, 90, terran_unit_path + "Unofficial/battlecruiser.jpg", terran_unit_path + "thor.png", protoss_units_path + "carrier.png", zerg_unit_path + "mutalisk.png", terran_unit_path + "viking.png", protoss_units_path + "void_ray.png", zerg_unit_path + "corruptor.png");
			#endregion

			#region Terran Buildings
			// Terran buildings
			// need data
			// http://sc2armory.com/game/terran/units
			insertObject("Supply Depot", TERRAN, DB_Helper.BUILDING, 0, 100, 0, 30, terran_structure_path + "supply_depot.jpg", "", "", "", "", "", "");
			insertObject("Barracks", TERRAN, DB_Helper.BUILDING, 0, 150, 0, 65, terran_structure_path + "barracks.jpg", "", "", "", "", "", "");
			insertObject("Bunker", TERRAN, DB_Helper.BUILDING, 0, 100, 0, 40, terran_structure_path + "bunker.jpg", "", "", "", "", "", "");
			insertObject("Engineering Bay", TERRAN, DB_Helper.BUILDING, 0, 125, 0, 35, terran_structure_path + "engineering_bay.jpg", "", "", "", "", "", "");
			insertObject("Ghost Academy", TERRAN, DB_Helper.BUILDING, 0, 150, 50, 40, terran_structure_path + "ghost_academy.jpg", "", "", "", "", "", "");
			insertObject("Starport", TERRAN, DB_Helper.BUILDING, 0, 150, 100, 50, terran_structure_path + "starport.jpg", "", "", "", "", "", "");
			insertObject("Reactor", TERRAN, DB_Helper.BUILDING, 0, 50, 50, 50, terran_structure_path + "reactor.jpg", "", "", "", "", "", "");
			insertObject("Tech Lab", TERRAN, DB_Helper.BUILDING, 0, 50, 25, 25, terran_structure_path + "tech_lab.jpg", "", "", "", "", "", "");
			insertObject("Refinery", TERRAN, DB_Helper.BUILDING, 0, 75, 0, 30, terran_structure_path + "refinery.jpg", "", "", "", "", "", "");
			insertObject("Factory", TERRAN, DB_Helper.BUILDING, 0, 150, 100, 60, terran_structure_path + "factory.jpg", "", "", "", "", "", "");
			insertObject("Armory", TERRAN, DB_Helper.BUILDING, 0, 150, 100, 65, terran_structure_path + "armory.jpg", "", "", "", "", "", "");
			insertObject("Auto-turret", TERRAN, DB_Helper.BUILDING, 0, 0, 0, 0, terran_structure_path + "auto_turret.jpg", "", "", "", "", "", "");
			insertObject("Command Center", TERRAN, DB_Helper.BUILDING, 0, 400, 0, 100, terran_structure_path + "command_center.jpg", "", "", "", "", "", "");
			insertObject("Fusion Core", TERRAN, DB_Helper.BUILDING, 0, 150, 150, 65, terran_structure_path + "fusion_core.jpg", "", "", "", "", "", "");
			insertObject("Missile Turret", TERRAN, DB_Helper.BUILDING, 0, 100, 0, 25, terran_structure_path + "missile_turret.jpg", "", "", "", "", "", "");
			insertObject("Orbital Command", TERRAN, DB_Helper.BUILDING, 0, 150, 0, 35, terran_structure_path + "orbital_command.jpg", "", "", "", "", "", "");
			insertObject("Planetary Fortress", TERRAN, DB_Helper.BUILDING, 0, 150, 150, 50, terran_structure_path + "planetary_fortress.jpg", "", "", "", "", "", "");
			//insertObject("Point Defense Drone", TERRAN, DB_Helper.BUILDING, 0, 0, 0, 0, terran_structure_path + "point_defense_drone.jpg", "", "", "", "", "", "");
			insertObject("Sensor Tower", TERRAN, DB_Helper.BUILDING, 0, 125, 100, 25, terran_structure_path + "sensor_tower.jpg", "", "", "", "", "", "");
			#endregion

			#region Terran Upgrades
			// Terran upgrades
			#region Engineering Bay

			#endregion

			#region Armory

			insertObject("behemothreactor.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "behemothreactor.png", "", "", "", "", "", "");
			insertObject("caduceusreactor.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "caduceusreactor.png", "", "", "", "", "", "");
			insertObject("combatshield-color.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "combatshield-color.png", "", "", "", "", "", "");
			insertObject("corvidreactor.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "corvidreactor.png", "", "", "", "", "", "");
			insertObject("durablematerials.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "durablematerials.png", "", "", "", "", "", "");
			insertObject("infantryarmorlevel1.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryarmorlevel1.png", "", "", "", "", "", "");
			insertObject("infantryarmorlevel2.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryarmorlevel2.png", "", "", "", "", "", "");
			insertObject("infantryarmorlevel3.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryarmorlevel3.png", "", "", "", "", "", "");
			insertObject("infantryweaponslevel0.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryweaponslevel0.png", "", "", "", "", "", "");
			insertObject("infantryweaponslevel1.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryweaponslevel1.png", "", "", "", "", "", "");
			insertObject("infantryweaponslevel2.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryweaponslevel2.png", "", "", "", "", "", "");
			insertObject("infantryweaponslevel3.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infantryweaponslevel3.png", "", "", "", "", "", "");
			insertObject("Infernal Pre-Igniter", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "infernalpreigniter.png", "", "", "", "", "", "");
			insertObject("moebiusreactor.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "moebiusreactor.png", "", "", "", "", "", "");
			insertObject("reapernitropacks.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "reapernitropacks.png", "", "", "", "", "", "");
			insertObject("shipplatinglevel1.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipplatinglevel1.png", "", "", "", "", "", "");
			insertObject("shipplatinglevel2.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipplatinglevel2.png", "", "", "", "", "", "");
			insertObject("shipplatinglevel3.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipplatinglevel3.png", "", "", "", "", "", "");
			insertObject("shipweaponslevel0.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipweaponslevel0.png", "", "", "", "", "", "");
			insertObject("shipweaponslevel1.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipweaponslevel1.png", "", "", "", "", "", "");
			insertObject("shipweaponslevel2.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipweaponslevel2.png", "", "", "", "", "", "");
			insertObject("shipweaponslevel3.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "shipweaponslevel3.png", "", "", "", "", "", "");
			insertObject("vehicleplatinglevel1.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleplatinglevel1.png", "", "", "", "", "", "");
			insertObject("vehicleplatinglevel2.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleplatinglevel2.png", "", "", "", "", "", "");
			insertObject("vehicleplatinglevel3.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleplatinglevel3.png", "", "", "", "", "", "");
			insertObject("vehicleweaponslevel0.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleweaponslevel0.png", "", "", "", "", "", "");
			insertObject(" vehicleweaponslevel1.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleweaponslevel1.png", "", "", "", "", "", "");
			insertObject("vehicleweaponslevel2.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleweaponslevel2.png", "", "", "", "", "", "");
			insertObject("vehicleweaponslevel3.png 1", TERRAN, DB_Helper.UPGRADE, 0, 0, 0, 0, terran_upgrade_path + "vehicleweaponslevel3.png", "", "", "", "", "", "");
	 


			insertObject("Vehicle Weapons Level 1", TERRAN, DB_Helper.UPGRADE, 0, 100, 100, 160, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Vehicle Weapons Level 2", TERRAN, DB_Helper.UPGRADE, 0, 175, 175, 190, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Vehicle Weapons Level 3", TERRAN, DB_Helper.UPGRADE, 0, 250, 250, 220, terran_upgrade_path + "", "", "", "", "", "", "");

			insertObject("Vehicle Plating Level 1", TERRAN, DB_Helper.UPGRADE, 0, 100, 100, 160, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Vehicle Plating Level 2", TERRAN, DB_Helper.UPGRADE, 0, 175, 175, 190, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Vehicle Plating Level 3", TERRAN, DB_Helper.UPGRADE, 0, 250, 250, 220, terran_upgrade_path + "", "", "", "", "", "", "");

			insertObject("Ship Weapons Level 1", TERRAN, DB_Helper.UPGRADE, 0, 100, 100, 160, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Ship Weapons Level 2", TERRAN, DB_Helper.UPGRADE, 0, 175, 175, 190, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Ship Weapons Level 3", TERRAN, DB_Helper.UPGRADE, 0, 250, 250, 220, terran_upgrade_path + "", "", "", "", "", "", "");

			insertObject("Ship Plating Level 1", TERRAN, DB_Helper.UPGRADE, 0, 150, 150, 160, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Ship Plating Level 2", TERRAN, DB_Helper.UPGRADE, 0, 225, 225, 190, terran_upgrade_path + "", "", "", "", "", "", "");
			insertObject("Ship Plating Level 3", TERRAN, DB_Helper.UPGRADE, 0, 300, 300, 220, terran_upgrade_path + "", "", "", "", "", "", "");
			#endregion
			#endregion
			#endregion
			
		}
		public static void enterBuilt_In_BuildOrder()
		{
			DB_Helper.connect();
			String author = "TeamLiquid";
			String race = "";
			String name = "";
			String des = "";
			String note = "";
			String notefooter = "";
			ObservableCollection<Index> result = new ObservableCollection<Index>();
			int index_id = 0;

			
			#region Terran Builds
			race = "Terran";
			// http://wiki.teamliquid.net/starcraft2/Reactor_Hellion_Expand_(vs._Zerg)
			name = "Reactor Hellion Expand";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Barracks")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Factory")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Reactor")[0].Obj_Id, "17 supply, Reactor on Barracks > Switch to Factory", "All SCVs off Refinery");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Command Center")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hellion")[0].Obj_Id, "20 supply", "");


			// http://wiki.teamliquid.net/starcraft2/Reactor_Biomech
			name = "Reactor Biomech";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Barracks")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Marine")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Factory")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Command Center")[0].Obj_Id, "22 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Reactor")[0].Obj_Id, "22 supply, Reactor on Barracks", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Reactor")[0].Obj_Id, "22 supply, Reactor on Factory", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "22 supply", "");

			// http://wiki.teamliquid.net/starcraft2/Griffith_4OC_(vs._Zerg)
			name = "Griffith 4OC";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Barracks")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Barracks")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Command Center")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "2x supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "2x supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Command Center")[0].Obj_Id, "27 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Command Center")[0].Obj_Id, "31 supply", "timing is important!");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "33 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "33 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "", "Transform CC in OC");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "", "Transform CC in OC");


			// http://wiki.teamliquid.net/starcraft2/1_Rax_FE_(TvP)
			name = "1 Rax FE";
			des = " vs Protoss";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("BarrackS")[0].Obj_Id, "11 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Marine")[0].Obj_Id, "16 supply", "1 only");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Marine")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("BarrackS")[0].Obj_Id, "20 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "24 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "28 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Tech Lab")[0].Obj_Id, "29 supply", "Apply to Barrack");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Tech Lab")[0].Obj_Id, "29 supply", "Apply to Barrack");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Reactor")[0].Obj_Id, "29 supply", "Apply to Barrack");



			// http://wiki.teamliquid.net/starcraft2/IEchoic's_2fac_2port_TvT
			name = "iEchoic's 2fact 2port";
			des = " vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("BarrackS")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Orbital Command")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Marine")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Marine")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Supply Depot")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Refinery")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Factory")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Tech Lab")[0].Obj_Id, "19 supply", "on Barracks > switch");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Factory")[0].Obj_Id, "next 100 gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Infernal Pre-Igniter")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hellion")[0].Obj_Id, "23 supply", "Hellions complete > clear Xel'naga towers, wait for Medivac");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Starport")[0].Obj_Id, "next 100 gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Starport")[0].Obj_Id, "next 100 gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Medivac")[0].Obj_Id, "", "Medivac completes, rally to opponents base and move out with all Hellions. Drop all Hellions near/on opposing mineral line");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Tech Lab")[0].Obj_Id, "Starport", "swap Starport with Barracks Tech Lab");
			#endregion

			#region Protoss Builds

			race = "Protoss";
			// http://wiki.teamliquid.net/starcraft2/Korean_4_Warpgate_All_In_(vs._Protoss)
			name = "Korean 4 Warpgate All In";
			des = " vs Protoss";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "10 supply", "Scout");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "10/18", "Apply to Nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "13/18 (@ 60/65 Gateway)", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "15/18 (@ 100% Gateway)", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "17 supply", "Chrono Boost continuously until finished");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "", "Build proxy Pylons in opponent's main once Warpgate Research is at ~75%");



			// http://wiki.teamliquid.net/starcraft2/Adel's_Late_Gas_PvP
			name = "Adel's Late Gas";
			des = " vs Protoss";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "1:14", "Apply to nexus");

			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "13 supply", "Apply to nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "1:58", "Apply to nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "16 supply, 2:26", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "17 supply, 2:45", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "", "Apply to nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "20 supply, 2:58", "@100% three probes should mine gas");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "21 supply, 3:10", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "21 supply, 3:23", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "24 supply, 3:41", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "26 supply, 3:57", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "28 supply, 4:04", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "31 supply, 4:17", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "31 supply, 4:35", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "", "Apply to Gateway");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "33 supply, 4:42", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "36 supply, 4:54", "");




			// http://wiki.teamliquid.net/starcraft2/Defensive_3_Gate_(vs._Protoss)
			name = "Defensive 3 Gate";
			des = " vs Protoss";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "10 supply", "Apply to Nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "12 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "", "Apply to Nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "", "Chrono Boost gateway");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "", "Apply to Nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "22 supply", "Stop Probe production");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "24 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "24 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "24 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "24 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "26 supply", "Resume Probe production");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "27 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "32 supply", "");


			// http://wiki.teamliquid.net/starcraft2/Nexus_first_(vs._Terran)
			name = "Nexus first";
			des = " vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "95% Cybernetics Core", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "@100 Cybernetics Core", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "100% Cybernetics Core", "");


			// http://wiki.teamliquid.net/starcraft2/1_Gate_FE_into_3_Gate_Robo
			name = "1 Gate FE into 3 Gate Robo";
			des = " vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "20 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "21 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "26 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "29 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Robotics Facility")[0].Obj_Id, "31 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "32 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "32 supply", "");


			// http://wiki.teamliquid.net/starcraft2/2_Gateway_Pressure_into_Expansion
			name = "2 Gateway Pressure into Expansion";
			des = " vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "21 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "21 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "22 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "24 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "27 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "29 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "32 supply", "Proxy Pylon near opponent's base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "32 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "", "as soon as your proxy Pylon is done building and your Nexus starts building");


			// http://wiki.teamliquid.net/starcraft2/3_Gate_Void_Ray_All_In
			name = "3 Gate Void Ray All In";
			des = " vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "Chrono Boost Probes a total of 3 times after the Pylon is done ");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "12 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "", "Chrono Boost the gateway @100% Cybernetics Core");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "24 supply", "Proxy Pylon near opponent's base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stargate")[0].Obj_Id, "27 supply, 4:18", "Proxy");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "Warp Gate research will finish as these finish");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "Cut Probe production (You should have 16 Probes on minerals, three on each gas and one Probe making Proxy buildings)");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Void Ray")[0].Obj_Id, "", "(Constantly Chrono Boost till done, start a 2nd Void Ray ASAP)");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "33 supply", "First Warpin should be two Stalkers and one Zealot ");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "33 supply", "Apply to Gateways");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "33 supply", "2nd Warp in should be pure Stalkers");


			// http://wiki.teamliquid.net/starcraft2/3_Gate_Void_Ray_All_In
			name = "3 Gate Void Ray All In (Alt)";
			des = " vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "13 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stargate")[0].Obj_Id, "25 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "26 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "28 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "28 supply", "Stop Probe production");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Void Ray")[0].Obj_Id, "30 supply", "Chrono Boost");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "33, 6:20 supply", "before 1st warp which consisted of sentries) (resume probe production)");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Void Ray")[0].Obj_Id, "", "Continue Void Ray production and transition into a mid game");



			// http://wiki.teamliquid.net/starcraft2/3-Gate_Sentry_Expand
			name = "3-Gate Sentry Expand";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "20 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "100% Assimilator", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "100% Assimilator", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "27 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "28 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "32 supply", "Proxy Pylon below ramp (in order to help make a wall later) ");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "34 supply", "");


			// http://wiki.teamliquid.net/starcraft2/Forge_Fast_Expansion_(vs._Zerg)
			name = "Forge Fast Expansion";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Forge")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Photon Cannon")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "", "to finish off wall-in");


			// http://wiki.teamliquid.net/starcraft2/Forge_Fast_Expansion_(vs._Zerg)
			name = "Forge Fast Expansion (Alt)";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Forge")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Probe")[0].Obj_Id, "15 supply", "resume Probe production");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Photon Cannon")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "18 supply", "at your Main base");


			// http://wiki.teamliquid.net/starcraft2/Blink_Stalker_Push
			name = "Blink Stalker Push";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "13 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "21 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "21 supply", "");

			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "27 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "28 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "29 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Twilight Council")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "Stop Probe production");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Blink")[0].Obj_Id, "30 supply", "Chrono Boost until upgrade completes");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "30 supply", "warp in");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Stalker")[0].Obj_Id, "44 Blink is done", "Continue builing Stalkers till midgame");



			// http://wiki.teamliquid.net/starcraft2/Dark_Templar_Expand
			name = "Dark Templar Expand";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "Scout enemy base");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Chrono Boost")[0].Obj_Id, "10 supply", "Apply to Nexus");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "12 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "22 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "", "Chrono Boost");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "26 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Twilight Council")[0].Obj_Id, "26 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "28 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "28 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Dark Shrine")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "", "Proxy Pylon at bottom of ramp");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "31 supply", "Warp at proxy Pylon");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nexus")[0].Obj_Id, "34 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Dark Templar")[0].Obj_Id, "", "ASAP Warp in two Dark Templar to Proxy Pylon, move one in to Main and one in to Natural");


			// http://wiki.teamliquid.net/starcraft2/3_Gate_Sentry_into_5_Gate_Zealot_all-in
			name = "3 Gate Sentry into 5 Gate Zealot all-in";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Assimilator")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Cybernetics Core")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zealot")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "22 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Warp Gate")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "23 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Gateway")[0].Obj_Id, "30 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Sentry")[0].Obj_Id, "31 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "33 supply", "Proxy Pylon at bottom of ramp");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "34 supply", "");

			// http://wiki.teamliquid.net/starcraft2/Photon_Cannon_rush
			name = "Photon Cannon Rush";
			des = " vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Probe")[0].Obj_Id, "7 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Probe")[0].Obj_Id, "8 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Probe")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Forge")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pylon")[0].Obj_Id, "11~13 supply", "Proxy Pylon");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Photon Cannon")[0].Obj_Id, "12~14 supply", "");

			#endregion

			#region Zerg Builds
			race = "Zerg";

            //http://wiki.teamliquid.net/starcraft2/3_Hatch_1_Gas
			name = "3 Hatch 1 Gas";
			des = "Vs. Protoss";
            note = "Any scouting Zergling units must be replaced straight away. If the initial defense of a push fails make a Baneling Nest and as many Zergling/Baneling units as needed to quickly fend off the opposing army.";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "9 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "15 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "14 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "16 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "16 supply", "up to 4");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "19 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "20 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Baneling Nest")[0].Obj_Id, "21 supply", "or Roach Warren. All Drones off Extractor");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "20 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "20 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "29 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "29 supply", "In-base if opposing player does not expand");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "29 supply", "Drones back in Extractors");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "100 Gas", "");

            //http://wiki.teamliquid.net/starcraft2/2_Base_Infestor/Zergling_(vs._Protoss)
			name = "2 Base Infestor/Zergling";
			des = "Vs. Protoss";
			note = "Any opening that gets you to 2 base economy will work, however it's creator, Destiny, favors the 11 Overpool 18 Hatch into Ice Fisher opening.\nThe build starts once you have 2 Hatcheries and 2 Queens";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Evolution Chamber")[0].Obj_Id, "", "Build 2 Evolution Chambers");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Melee Attacks Level 1")[0].Obj_Id, "100% Evolution Chambers", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Ground Carapace Level 1")[0].Obj_Id, "100% Evolution Chambers", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Infestation Pit")[0].Obj_Id, "100% Lair", "(optional Overseer if you don't know what your opponent is doing by this point)");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Burrow")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pathogen Glands")[0].Obj_Id, "100% Infestation Pit", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Infestor")[0].Obj_Id, "100% Infestor Energy", "make as many Infestors as your gas will allow (around 4-6)");


            //http://wiki.teamliquid.net/starcraft2/2_Base_Offensive_Nydus
			name = "2 Base Offensive Nydus";
			des = "Vs. Protoss";
			note = "Any opening other than Speedling Expand will work here, since you are delaying Zergling Speed until after getting Lair (or until after expanding on larger maps).";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "100 Gas", "Build 2 Evolution Chambers");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "100 Gas", "Build the 3rd Evolution Chambers and Evolution Chamber");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hydralisk Den")[0].Obj_Id, "100% Lair", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Nydus Network")[0].Obj_Id, "200 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Grooved Spines")[0].Obj_Id, "100% Hydralisk Den", "Upgrade Missile Attack +1");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overseer")[0].Obj_Id, "100% Nydus Network", "");

            //http://wiki.teamliquid.net/starcraft2/Roach/Zergling_Pressure
            name = "Roach/Ling Pressure";
            des = "Vs. Protoss";
            note = "The build starts when you have 2 Hatcheries and 2 Queens, and have just started Zergling Speed. Keep Drones on gas";
            notefooter = "";
            createIndex(name, des, race, author, note, notefooter);
            result = DB_Helper.get_Index_By_Name(name);
            index_id = result[0].Id;
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "100 Gas", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach Warren")[0].Obj_Id, " 2 Hatcheries, 2 Queens", "Stop making Drones when you have more than 1-base saturation");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "100% Roach Warren", " make as many Roaches as your gas will allow, and Take Drones off of gas");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "100% Roach Warren", "");

            //http://wiki.teamliquid.net/starcraft2/Roach_Expand
            name = "Roach Expand";
            des = "Vs. Protoss";
            note = "This build works best against a Forge Fast Expansion, especially on maps where the entrance into the opponent's Natural is large. This build is only recommended against a Forge Fast Expansion build.";
            notefooter = "";
            createIndex(name, des, race, author, note, notefooter);
            result = DB_Helper.get_Index_By_Name(name);
            index_id = result[0].Id;
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "9 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "14 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "14 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "16 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "100% Spawning Pool", "1 pair");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "100% Spawning Pool", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "20 supply", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach Warren")[0].Obj_Id, "35/50 Queen", "@ 175 Gas, take Drones off of gas");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "100% Roach Warren", "7 Roaches");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "300 Minerals", "Expand");

            //http://wiki.teamliquid.net/starcraft2/Ling/Bane_(Vs._Protoss)
            name = "Ling/Bane";
            des = "Vs. Protoss";
            note = "Any opening that gets you to a 2-base economy will work.";
            notefooter = "";
            createIndex(name, des, race, author, note, notefooter);
            result = DB_Helper.get_Index_By_Name(name);
            index_id = result[0].Id;
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, " ", "up to around 40 supply");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "150 Minerals", "Build 3 Queens");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "25 Minerals", "Build 3 Extractors");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Evolution Chamber")[0].Obj_Id, "75 Minerals", "Build 2 Evolution Chamber");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spine Crawler")[0].Obj_Id, "100 Minerals", "Build 2 optional Spine Crawlers");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Melee Attacks Level 1")[0].Obj_Id, "100% Evolution Chambers", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Ground Carapace Level 1")[0].Obj_Id, "100% Evolution Chambers", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "100 Gas", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Baneling Nest")[0].Obj_Id, "50 Gas", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "300 Minerals", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Ventral Sacs")[0].Obj_Id, "100% Lair", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pneumatized Carapace")[0].Obj_Id, "100% Lair", "");
            DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Centrifugal Hooks")[0].Obj_Id, "150 Gas", "");

			// http://wiki.teamliquid.net/starcraft2/Muta/Ling/Bane
			name = "Muta/Ling/Bane  ";
			des = "vs Terran";
			note = "any fast 2nd Hatchery opening into Lair > Spire is fine, but Hatchery First is ideal.";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Baneling Nest")[0].Obj_Id, "50 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spire")[0].Obj_Id, "100% Lair", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Centrifugal Hooks")[0].Obj_Id, "100% Lair", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Mutalisk")[0].Obj_Id, "100% Spire", "Make as many Mutalisks as your gas will allow");


			// http://wiki.teamliquid.net/starcraft2/Ling/Roach/Infestor_(vs._Terran)
			name = "Ling/Roach/Infestor";
			des = "vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "300 Minerals", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "100 gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "Next 100 gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Evolution Chamber")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Ground Carapace Level 1")[0].Obj_Id, "100 Evolution Chamber", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Infestation Pit")[0].Obj_Id, "100% Lair", "");


			// http://wiki.teamliquid.net/starcraft2/Expand_Roach_(vs._Terran)
			name = "Expand Roach";
			des = "vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "100% Spawning Pool", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach Warren")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "22 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "100% Roach Warren", "7 Roaches");


			// http://wiki.teamliquid.net/starcraft2/Expand_Roach_(vs._Terran)
			name = "Expand Roach (Alt)";
			des = "vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach Warren")[0].Obj_Id, "18 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "19 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "100% Roach Warren", "5 Roaches");


			// http://wiki.teamliquid.net/starcraft2/Infestor/Corruptor/Brood_Lord
			name = "Infestor/Corruptor/Brood Lord";
			des = "vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hive")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Greater Spire")[0].Obj_Id, "100% Hive", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pathogen Glands")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Corruptor")[0].Obj_Id, "50% Greater Spire", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Brood Lord")[0].Obj_Id, "100% Greater Spire", "");


			// http://wiki.teamliquid.net/starcraft2/2_Base_Ling/Bane_Bust
			name = "2 Base Ling/Bane Bust";
			des = "vs Terran";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "14 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "17 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "100% Spawning Pool", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "100% Hatchery", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Baneling Nest")[0].Obj_Id, "50 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "", "Stop Drone production at 38 supply");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Baneling")[0].Obj_Id, "", "");


			// http://wiki.teamliquid.net/starcraft2/1_Base_Tech_with_Roaches_(vs._Zerg)
			name = "1 Base Tech with Roaches";
			des = "vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "200 mineral", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "100 Spawning Pool", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach Warren")[0].Obj_Id, "70% Queen", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "75% Roach Warren", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Lair")[0].Obj_Id, "100 Gas", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "22 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "100% Roach Warren", "");


			// http://wiki.teamliquid.net/starcraft2/Destiny's_ZvZ_(vs._Zerg)
			name = "Destiny's";
			des = "vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "13 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "15 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "16 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spine Crawler")[0].Obj_Id, "100% Spawning Pool", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach Warren")[0].Obj_Id, "75% Queen", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "100% Roach Warren", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "~32 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "~34 supply", "Expand");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Metabolic Boost")[0].Obj_Id, "36 supply", "Remove Drones from gas at this point");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Roach")[0].Obj_Id, "36 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "44 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "46 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "~50 supply", "Mass Zergling behind Roaches");



			// http://wiki.teamliquid.net/starcraft2/Roach/Infestor_(vs._Zerg)
			name = "Roach/Infestor";
			des = "vs Zerg";
			note = "";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Glial Reconstitution")[0].Obj_Id, "100% Lair", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Evolution Chamber")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Missile Attacks Level 1")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Infestation Pit")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Pathogen Glands")[0].Obj_Id, "100% Infestation Pit", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Infestor")[0].Obj_Id, "31/80 Pathogen Glands upgrade", "");


			// http://wiki.teamliquid.net/starcraft2/Aggressive_Pool_First
			name = "6 Pool";
			des = "vs Zerg";
			note = "6 Pool finishes Spawning Pool at ~1:49 game time";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "6 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "5 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "6 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "7 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "8 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "Next 150 mineral", "");

			// http://wiki.teamliquid.net/starcraft2/Aggressive_Pool_First
			name = "7 Pool";
			des = "vs Zerg";
			note = "7 Pool finishes Spawning Pool at ~1:57 game time";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "7 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "6 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "7 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "8 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "8 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "Next 150 mineral", "");


			// http://wiki.teamliquid.net/starcraft2/Aggressive_Pool_First
			name = "8 Pool";
			des = "vs Zerg";
			note = "8 Pool finishes Spawning Pool at ~2:04 game time";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "8 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "7 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "8 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "11 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "Next 150 mineral", "");


			// http://wiki.teamliquid.net/starcraft2/Aggressive_Pool_First
			name = "9 Pool AKA Fail-Pool";
			des = "vs Zerg";
			note = "9 Pool finishes Spawning Pool at ~2:10 game time";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "11 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "14 supply", "Stop Zergling production and hard Drone");


			// http://wiki.teamliquid.net/starcraft2/Aggressive_Pool_First
			name = "10 Over-Pool";
			des = "vs Zerg";
			note = "10 Pool finishes Spawning Pool at ~2:18 game time";
			notefooter = "";
			createIndex(name, des, race, author, note, notefooter);
			result = DB_Helper.get_Index_By_Name(name);
			index_id = result[0].Id;
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Extractor")[0].Obj_Id, "10 supply, Extractor trick", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Drone")[0].Obj_Id, "9 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Spawning Pool")[0].Obj_Id, "11/10 supply", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Overlord")[0].Obj_Id, "Next 100 mineral", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Queen")[0].Obj_Id, "100% Spawning Pool", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Zergling")[0].Obj_Id, "", "");
			DB_Helper.insertBuild(index_id, 0, get_Object_SC_by_Name("Hatchery")[0].Obj_Id, "18 supply", "Optional");
			#endregion
		}
	}
}
