﻿using GTANetworkAPI;
using MySql.Data.MySqlClient;
using NemesusWorld.Database;
using NemesusWorld.Models;
using System;
using System.Collections.Generic;
using NemesusWorld.Controllers;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Runtime.ConstrainedExecution;
using System.Reflection.Metadata;
using Google.Protobuf.WellKnownTypes;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Security.Principal;
using GTANetworkMethods;
using Player = GTANetworkAPI.Player;
using Vehicle = GTANetworkAPI.Vehicle;
using Blip = GTANetworkAPI.Blip;
using Ped = GTANetworkAPI.Ped;
using System.Numerics;
using Vector3 = GTANetworkAPI.Vector3;
using System.Drawing;
using Color = GTANetworkAPI.Color;
using System.Threading.Channels;

namespace NemesusWorld.Utils
{
    class Helper : Script
    {
        public static AdminSettings adminSettings;
        public static Random rnd = new Random();
        public static int MeatPrice = 115 + rnd.Next(0, 45);
        public static int FishPrice = 125 + rnd.Next(0, 55);
        public static JObject weatherObj = null;
        public static String weatherstring = "";
        public static int weatherTimestamp = 0;
        public static List<SpedVehicles> spedVehiclesList = new List<SpedVehicles>();
        public static List<ShopItems> shopItemList = new List<ShopItems>();
        public static List<Reports> reportList = new List<Reports>();
        public static List<Animations> animList = new List<Animations>();
        public static List<Whitelist> whitelistList = new List<Whitelist>();
        public static List<BusRoutes> busRoutesList = new List<BusRoutes>();
        public static List<Garbage> garbageList = new List<Garbage>();
        public static List<ATMSpots> atmSpotList = new List<ATMSpots>();
        public static List<Taxi> taxiJobs = new List<Taxi>();
        public static List<Invoices> invoicesList = new List<Invoices>();
        public static int garbageCount = 0;
        public static List<GarbageRoutes> garbageRoutesList = new List<GarbageRoutes>();
        public static List<TaxiRoutes> taxiRoutesList = new List<TaxiRoutes>();
        public static List<TaxiBots> taxiBotList = new List<TaxiBots>();
        public static List<Blitzer> blitzerList = new List<Blitzer>();
        public static List<CCTV> cctvList = new List<CCTV>();
        public static List<SpikeStrip> spikeStripList = new List<SpikeStrip>();
        public static List<PoliceProps> policePropList = new List<PoliceProps>();
        //ToDo: Discord Webhooks setzen
        public static string AdminNotificationWebHook = "TODO";
        public static string ErrorWebhook = "TODO";
        public static string ScreenshotWebhook = "TODO";
        public static int mats = 15;
        public static Vector3[] fuelPositions = new Vector3[62]
                                { 
                                  //Bizz 5
                                  new Vector3(1175.8339, -322.80164, 69.35089+0.5),
                                  new Vector3(1183.2217, -321.4986, 69.351875+0.5),
                                  new Vector3(1184.7297, -329.1253, 69.31523+0.5),
                                  new Vector3(1177.3427, -330.53165, 69.31664+0.5),
                                  new Vector3(1175.5642, -321.74146, 69.35079+0.5),
                                  new Vector3(1182.9696, -320.4142, 69.349754+0.5),
                                  new Vector3(1186.2598, -337.6653, 69.3517+0.5),
                                  new Vector3(1178.8286, -338.972, 69.35641+0.5),
                                  new Vector3(1179.0657, -340.09277, 69.356476+0.5),
                                  new Vector3(1186.4966, -338.79626, 69.357765+0.5),
                                  new Vector3(1184.9692, -330.18472, 69.3179+0.5),
                                  new Vector3(1177.5522, -331.4926, 69.316574+0.5),
                                  //Bizz 6
                                  new Vector3(-63.152065, -1768.0918, 29.261726+0.5),
                                  new Vector3(-60.549255, -1760.9589, 29.261694+0.5),
                                  new Vector3(-61.581593, -1760.6464, 29.261728+0.5),
                                  new Vector3(-64.16449, -1767.736, 29.2611+0.5),
                                  new Vector3(-71.56, -1765.2302, 29.53404+0.5),
                                  new Vector3(-68.99002, -1758.1678, 29.533997+0.5),
                                  new Vector3(-70.00452, -1757.8163, 29.53369+0.5),
                                  new Vector3(-72.522446, -1764.9323, 29.533525+0.5),
                                  new Vector3(-79.62542, -1762.3412, 29.794657+0.5),
                                  new Vector3(-77.05797, -1755.2039, 29.800293+0.5),
                                  new Vector3(-78.06932, -1754.9265, 29.800333+0.5),
                                  new Vector3(-80.6763, -1761.9991, 29.800333+0.5),
                                  //Bizz 7
                                  new Vector3(2573.2622, 364.7006, 108.647804+0.5),
                                  new Vector3(2572.9868, 359.21198, 108.647804+0.5),
                                  new Vector3(2574.033, 359.22083, 108.647804+0.5),
                                  new Vector3(2574.3328, 364.76498, 108.6478+0.5),
                                  new Vector3(2580.5867, 364.37717, 108.88851+0.5),
                                  new Vector3(2580.3574, 358.9027, 108.6478+0.5),
                                  new Vector3(2581.4875, 358.85263, 108.6478+0.5),
                                  new Vector3(2581.7566, 364.34555, 108.62464+0.5),
                                  new Vector3(2588.1624, 364.0315, 108.64774+0.5),
                                  new Vector3(2587.9172, 358.5365, 108.647804+0.5),
                                  new Vector3(2588.99, 358.50677, 108.63903+0.5),
                                  new Vector3(2589.1453, 364.10083, 108.6478+0.5),
                                  //Bizz 12
                                  new Vector3(1705.5573, 6414.1465, 32.76403+0.5),
                                  new Vector3(1705.9689, 6415.112, 32.764038+0.5),
                                  new Vector3(1701.9115, 6416.966, 32.764015+0.5),
                                  new Vector3(1701.5212, 6415.996, 32.764034+0.5),
                                  new Vector3(1697.6099, 6417.883, 32.764034+0.5),
                                  new Vector3(1697.9768, 6418.794, 32.764034+0.5),
                                  //Bizz 14
                                  new Vector3(2001.9058, 3771.6328, 32.403934+0.5),
                                  new Vector3(2004.2795, 3772.9077, 32.40394+0.5),
                                  new Vector3(2006.6052, 3774.4265, 32.40394+0.5),
                                  new Vector3(2009.6277, 3776.1829, 32.403934+0.5),
                                  new Vector3(2008.8662, 3777.3423, 32.403934+0.5),
                                  new Vector3(2005.8527, 3775.523, 32.40394+0.5),
                                  new Vector3(2003.552, 3774.0486, 32.403934+0.5),
                                  new Vector3(2001.1718, 3772.7617, 32.403934+0.5),
                                  //Bizz 15
                                  new Vector3(-714.92346, -932.5354, 19.21382+0.5),
                                  new Vector3(-714.94696, -939.3189, 19.20394+0.5),
                                  new Vector3(-716.02344, -939.34656, 19.20773+0.5),
                                  new Vector3(-715.94995, -932.48334, 19.213755+0.5),
                                  new Vector3(-723.5149, -932.47687, 19.213905+0.5),
                                  new Vector3(-723.4378, -939.3022, 19.20352+0.5),
                                  new Vector3(-724.50696, -939.3707, 19.208748+0.5),
                                  new Vector3(-724.5022, -932.59235, 19.213932+0.5),
                                  new Vector3(-732.05963, -932.5169, 19.21393+0.5),
                                  new Vector3(-732.12146, -939.26245, 19.20386+0.5),
                                  new Vector3(-733.1399, -939.3334, 19.208328+0.5),
                                  new Vector3(-733.175, -932.5181, 19.213482+0.5)
                                };

        //Factionlist
        public static List<FactionsModel> factionList = new List<FactionsModel>();

        public static object RequestConstants { get; private set; }

        //Account/Character Data
        public static string GetCharacterKey()
        {
            return "Character_Data";
        }

        public static string GetAccountKey()
        {
            return "Account_Data";
        }

        public static string GetTempData()
        {
            return "Temp_Data";
        }

        public static Account GetAccountData(Player player)
        {
            if (player == null) return null;
            return player.GetData<Account>(Helper.GetAccountKey());
        }

        public static Character GetCharacterData(Player player)
        {
            if (player == null) return null;
            return player.GetData<Character>(GetCharacterKey());
        }

        public static TempData GetCharacterTempData(Player player)
        {
            if (player == null) return null;
            return player.GetData<TempData>(GetTempData());
        }

        //IP
        public static string GetIP(Player player)
        {
            return player.Address;
        }

        public static Character GetCharacterDataOffline(int number)
        {
            Character character = null;
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                character = db.Single<Character>("SELECT * FROM characters WHERE id = @0 LIMIT 1", number);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetCharacterDataOffline]: " + e.ToString());
            }
            return character;
        }

        //Adminlogs
        public static void CreateAdminLog(string loglabel, string text, string ip = null, ulong miscellaneous = 10)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO adminlogs (loglabel, text, ip, miscellaneous, timestamp) VALUES (@loglabel, @text, @ip, @miscellaneous, @timestamp)";

                command.Parameters.AddWithValue("@loglabel", loglabel);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@ip", ip);
                command.Parameters.AddWithValue("@miscellaneous", Convert.ToUInt64(miscellaneous));
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateAdminLog]: " + e.ToString());
            }
        }

        //CreateFactionLog
        public static void CreateFactionLog(int factionid, string text)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO logs (loglabel, text, timestamp) VALUES (@loglabel, @text, @timestamp)";

                command.Parameters.AddWithValue("@loglabel", "faction-" + factionid);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateFactionLog]: " + e.ToString());
            }
        }

        public static void CreateEvidenceLog(string text)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO logs (loglabel, text, timestamp) VALUES (@loglabel, @text, @timestamp)";

                command.Parameters.AddWithValue("@loglabel", "evidence");
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateEvidenceLog]: " + e.ToString());
            }
        }

        //CreateWeaponLog
        public static void CreateWeaponLog(int factionid, string text)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO logs (loglabel, text, timestamp) VALUES (@loglabel, @text, @timestamp)";

                command.Parameters.AddWithValue("@loglabel", "weapon-" + factionid);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateWeaponLog]: " + e.ToString());
            }
        }

        //CreateGroupLog
        public static void CreateGroupLog(int groupid, string text)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO logs (loglabel, text, timestamp) VALUES (@loglabel, @text, @timestamp)";

                command.Parameters.AddWithValue("@loglabel", "group-" + groupid);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateGroupLog]: " + e.ToString());
            }
        }

        public static void CreateGroupMoneyLog(int groupid, string text)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO logs (loglabel, text, timestamp) VALUES (@loglabel, @text, @timestamp)";

                command.Parameters.AddWithValue("@loglabel", "groupmoney-" + groupid);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateGroupMoneyLog]: " + e.ToString());
            }
        }

        //BankSettings
        public static void BankSettings(string banknumber, string setting, string value, string name)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO banksettings (banknumber, setting, value, name, timestamp) VALUES (@banknumber, @setting, @value, @name, @timestamp)";

                command.Parameters.AddWithValue("@banknumber", banknumber);
                command.Parameters.AddWithValue("@setting", setting);
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[BankSettings]: " + e.ToString());
            }
        }

        //Bankfile
        public static void Bankfile(Bank bank1, Bank bank2, string verwendungszweck, int value, bool reserverOrder = false)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO bankfile (bankid, bankfrom, bankto, banktext, bankvalue, banktime) VALUES (@bankid, @bankfrom, @bankto, @banktext, @bankvalue, @banktime)";

                if (reserverOrder == false)
                {
                    command.Parameters.AddWithValue("@bankid", bank1.id);
                }
                else
                {
                    command.Parameters.AddWithValue("@bankid", bank2.id);
                }
                command.Parameters.AddWithValue("@bankfrom", bank1.banknumber);
                command.Parameters.AddWithValue("@bankto", bank2.banknumber);
                command.Parameters.AddWithValue("@banktext", verwendungszweck);
                command.Parameters.AddWithValue("@bankvalue", value);
                command.Parameters.AddWithValue("@banktime", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[Bankfile]: " + e.ToString());
            }
        }

        public static void SetGovMoney(int value, string setting = "n/A")
        {
            try
            {
                Bank bank = BankController.GetBankByBankNumber("SA3701-100000");
                if (bank != null)
                {
                    bank.bankvalue += value;
                    adminSettings.govvalue = bank.bankvalue;
                    MySqlCommand command = General.Connection.CreateCommand();

                    command.CommandText = "INSERT INTO logs (loglabel, text, timestamp) VALUES (@loglabel, @text, @timestamp)";

                    command.Parameters.AddWithValue("@loglabel", "govmoney");
                    command.Parameters.AddWithValue("@text", $"{value}$ " + setting);
                    command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetGovMoney]: " + e.ToString());
            }
        }

        //Discord Webhooks
        public static void DiscordWebhook(string webhookid, string content, string username = "Gameserver")
        {
            if (webhookid.ToLower() == "todo") return;
            try
            {
                HTTP.Post(webhookid, new System.Collections.Specialized.NameValueCollection()
                {
                    {
                        "username",
                        username
                    },
                    {
                       "content",
                        content
                    }
                });
            }
            catch (Exception) { }
        }

        //Führungszeugnis
        public static int ShowFührungsZeugnis(Player player, String props, bool show = true)
        {
            try
            {
                CenterMenu centerMenu = null;
                List<CenterMenu> centerMenuList = new List<CenterMenu>();

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT * FROM policefile WHERE name = @name AND commentary = 0 ORDER BY timestamp DESC LIMIT 35";
                command.Parameters.AddWithValue("@name", props);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        centerMenu = new CenterMenu();

                        centerMenu.var2 = reader.GetString("text");
                        centerMenu.var3 = Convert.ToString(reader.GetInt32("timestamp"));
                        centerMenuList.Add(centerMenu);
                    }
                    reader.Close();
                }
                if (show == true)
                {
                    String rules = "ID,Verbrechen,Kommentar,Zeitpunkt";
                    ItemsController.OnShowInventory(player, 1);
                    player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenuList), "Führungszeugnis");
                    player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                }
                return centerMenuList.Count;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowFührungsZeugnis]: " + e.ToString());
            }
            return 0;
        }

        //ForumUpdate
        //ToDo: Gruppen IDs und Sonstiges anpassen
        public static void ForumUpdate(Player player, string action, int forumid = -1, string grund = "n/A", int zeit = -1)
        {
            return;
            Character character = Helper.GetCharacterData(player);
            Account account = Helper.GetAccountData(player);
            TempData tempData = Helper.GetCharacterTempData(player);

            if (account == null || character == null || tempData == null || (account.forumaccount == -1 && forumid == -1)) return;

            int oldForumID = forumid;

            if (account.forumaccount > -1 && forumid == -1)
            {
                forumid = account.forumaccount;
            }

            if (action == "ban")
            {
                //ToDo: ForumConnect Link anpassen
                HTTP.Post("HIER/forumConnect.php?id=cu4VUud8EB4TLyfhbSSN589u7&status=ban&userid=" + forumid + "&grund=" + grund + "&zeit=" + zeit, new System.Collections.Specialized.NameValueCollection());
            }
            else if (action == "unban")
            {
                //ToDo: ForumConnect Link anpassen
                HTTP.Post("HIER/forumConnect.php?id=cu4VUud8EB4TLyfhbSSN589u7&status=unban&userid=" + forumid, new System.Collections.Specialized.NameValueCollection());
            }
            else if (action == "groups" || action == "all")
            {
                string groups = "6";
                string removeGroups = "-1";

                //Premium
                if (account.premium > 0 && account.premium > UnixTimestamp())
                {
                    if (account.premium == 1)
                    {
                        groups = groups + ",16";
                        removeGroups = removeGroups + ",14,15";
                    }
                    else if (account.premium == 2)
                    {
                        groups = groups + ",15";
                        removeGroups = removeGroups + ",14,16";
                    }
                    else if (account.premium == 3)
                    {
                        groups = groups + ",14";
                        removeGroups = removeGroups + ",15,16";
                    }
                }
                else
                {
                    groups = groups + ",16";
                    removeGroups = removeGroups + ",14,15,16";
                }
                //Fraktionen
                int faction = -1;
                int leader = 0;
                int member = 0;
                int rang = 0;
                FactionsModel factionsModel = null;

                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT faction,leader,member,rang FROM characters WHERE userid = @userid";
                command.Parameters.AddWithValue("@userid", account.id);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reader.Read();
                        faction = reader.GetInt32("faction");
                        leader = reader.GetInt32("leader");
                        member = reader.GetInt32("member");
                        rang = reader.GetInt32("rang");
                        factionsModel = GetFactionById(faction);
                        if (factionsModel != null)
                        {
                            if (faction == 1)
                            {
                                if (account.id == factionsModel.leader || rang >= 10)
                                {
                                    if (!groups.Contains(",20"))
                                    {
                                        groups = groups + ",20";
                                    }
                                    if (!groups.Contains(",21"))
                                    {
                                        removeGroups = removeGroups + ",21";
                                    }
                                }
                                else
                                {
                                    if (!groups.Contains(",21"))
                                    {
                                        groups = groups + ",21";
                                    }
                                    if (!groups.Contains(",20"))
                                    {
                                        removeGroups = removeGroups + ",20";
                                    }
                                }
                            }
                        }
                    }
                    reader.Close();
                }

                //Police
                if (!groups.Contains(",20") && !groups.Contains(",21") && !removeGroups.Contains(",20") && !removeGroups.Contains(",21"))
                {
                    removeGroups = removeGroups + ",20,21";
                }

                //Post
                //ToDo: ForumConnect Link anpassen
                HTTP.Post("HIER/forumConnect.php?id=cu4VUud8EB4TLyfhbSSN589u7&status=removefromgroups&userid=" + forumid + "&groupids=" + removeGroups, new System.Collections.Specialized.NameValueCollection());
                HTTP.Post("HIER/forumConnect.php?id=cu4VUud8EB4TLyfhbSSN589u7&status=settogroups&userid=" + forumid + "&groupids=" + groups, new System.Collections.Specialized.NameValueCollection());
            }

            if (oldForumID == -1)
            {
                account.forumupdate = Helper.UnixTimestamp() + (60 * 25);
            }
            return;
        }

        //Logs
        public static void CreateUserLog(int userid, string action)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO userlog (userid, action, timestamp) VALUES (@userid, @action, @timestamp)";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@action", action);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateUserLog]: " + e.ToString());
            }
        }

        public static void CreateUserFile(int userid, string admin, string text, string penalty)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO userfile (userid, admin, text, penalty, timestamp) VALUES (@userid, @admin, @text, @penalty, @timestamp)";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@admin", admin);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@penalty", penalty);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateUserFile]: " + e.ToString());
            }
        }

        //Timeline
        public static void CreateUserTimeline(int userid, int charid, string text, int icon = 0)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO timeline (userid, charid, text, icon, timestamp) VALUES (@userid, @charid, @text, @icon, @timestamp)";

                command.Parameters.AddWithValue("@userid", userid);
                command.Parameters.AddWithValue("@charid", charid);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@icon", icon);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateUserTimeline]: " + e.ToString());
            }
        }

        //Other functions
        public static bool IsATrailer(Vehicle vehicle)
        {
            try
            {
                if (vehicle.GetSharedData<String>("Vehicle:Name").ToLower().Contains("trailer") || vehicle.GetSharedData<String>("Vehicle:Name").ToLower().Contains("tanker"))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsATrailer]: " + e.ToString());
            }
            return false;
        }

        public static List<Logs> GetLogEntries(string loglabel)
        {
            List<Logs> logList = new List<Logs>();
            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
            foreach (Logs log in db.Fetch<Logs>("SELECT * FROM logs WHERE loglabel = @0 ORDER BY timestamp DESC LIMIT 25", loglabel))
            {
                logList.Add(log);
            }
            return logList;
        }

        public static void GetCharacterTattoos(Player player, int characterid)
        {
            TempData tempData = Helper.GetCharacterTempData(player);
            if (tempData == null) return;

            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
            foreach (Tattoos tattoo in db.Fetch<Tattoos>("SELECT * FROM tattoos WHERE characterid = @0", characterid))
            {
                tempData.tattoos.Add(tattoo);
                Decoration decoration = new Decoration();
                decoration.Collection = NAPI.Util.GetHashKey(tattoo.dlcname);
                decoration.Overlay = NAPI.Util.GetHashKey(tattoo.name);
                NAPI.Player.SetPlayerDecoration(player, decoration);
            }
        }

        public static string GetCharacterName(int myid)
        {
            string getName = "n/A";
            MySqlCommand command = General.Connection.CreateCommand();
            command.CommandText = "SELECT name from characters WHERE id=@id LIMIT 1";
            command.Parameters.AddWithValue("@id", myid);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    getName = reader.GetString("name");
                    reader.Close();
                }
            }
            return getName;
        }

        public static string GetAccountName(int myid)
        {
            string getName = "Keiner";
            MySqlCommand command = General.Connection.CreateCommand();
            command.CommandText = "SELECT name from users WHERE id=@id LIMIT 1";
            command.Parameters.AddWithValue("@id", myid);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    getName = reader.GetString("name");
                    reader.Close();
                }
            }
            return getName;
        }

        public static Player GetPlayerFromID(int id)
        {
            try
            {
                foreach (Player player in NAPI.Pools.GetAllPlayers())
                {
                    if (player.Id == id)
                    {
                        return player;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetPlayerFromID]: " + e.ToString());
            }
            return null;
        }

        public static int GetAccountID(string name)
        {
            int id = -1;
            MySqlCommand command = General.Connection.CreateCommand();
            command.CommandText = "SELECT id from users WHERE name=@name LIMIT 1";
            command.Parameters.AddWithValue("@name", name);
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    id = reader.GetInt32("id");
                    reader.Close();
                }
            }
            return id;
        }

        [RemoteEvent("Server:CrouchPlayer")]
        public static void OnPlayerCrouch(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;

                tempData.crouching = !tempData.crouching;
                if (tempData.crouching == false)
                {
                    player.ResetSharedData("Player:Crouching");
                    player.SetSharedData("Player:WalkingStyle", character.walkingstyle);
                }
                else
                {
                    player.SetSharedData("Player:Crouching", 1);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerCrouch]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ReportPlayer")]
        public static void OnReportPlayer(Player player, int id)
        {
            try
            {
                if (player.Id == id)
                {
                    SendNotificationWithoutButton(player, "Du kannst dich nicht selber melden!", "error", "top-left", 1500);
                    player.TriggerEvent("Client:PlayerFreeze", false);
                    player.TriggerEvent("Client:HideCursor");
                    return;
                }
                player.SetData<int>("Player:Report", id);
                player.TriggerEvent("Client:CallInput", "Spieler melden", "Warum möchtest du diesen Spieler melden?", "text", "Der Spieler cheatet!", 30, "ReportPlayer");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnReportPlayer]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetGeworben")]
        public static void OnSetGeworben(Player player, string name)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                if (account == null) return;

                if (name == account.name)
                {
                    Helper.SendNotificationWithoutButton2(player, "Du kannst dich nicht selber werben!", "error", "center");
                    return;
                }

                bool found = false;
                int id = -1;
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT id FROM users where name=@name LIMIT 1";
                command.Parameters.AddWithValue("@name", name);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        id = reader.GetInt32("id");
                        found = true;
                    }
                    reader.Close();
                }

                CreateUserLog(id, $"{account.name} geworben!");

                if (found == true)
                {
                    Helper.SendNotificationWithoutButton2(player, "Geworben von erfolgreich eingetragen!", "success", "center");
                    command = General.Connection.CreateCommand();
                    command.CommandText = "UPDATE users SET geworben=@geworben WHERE id=@id";

                    command.Parameters.AddWithValue("@geworben", name);
                    command.Parameters.AddWithValue("@id", account.id);

                    command.ExecuteNonQuery();

                    account.geworben = name;
                }
                else
                {
                    Helper.SendNotificationWithoutButton2(player, "Ungültiger Spieler!", "error", "center");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSetGeworben]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:GetShop")]
        public static void OnGetShop(Player player, int shopitem)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                if (account == null) return;

                if (shopitem == 1)
                {
                    if (account.coins < 50)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    account.namechanges++;
                    account.coins -= 50;
                    Helper.SendNotificationWithoutButton2(player, "Du hast 1x Namechange freigeschaltet!", "success", "center");
                }
                else if (shopitem == 2)
                {
                    if (account.coins < 50)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    account.epboost = Helper.UnixTimestamp() + (3600 * 12);
                    account.coins -= 50;
                    Helper.SendNotificationWithoutButton2(player, "Du hast einen 12h Erfahrungspunkte Boost freigeschaltet!", "success", "center");
                }
                else if (shopitem == 3)
                {
                    if (account.coins < 75)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    account.epboost = Helper.UnixTimestamp() + (3600 * 24);
                    account.coins -= 75;
                    Helper.SendNotificationWithoutButton2(player, "Du hast einen 24h Erfahrungspunkte Boost freigeschaltet!", "success", "center");
                }
                else if (shopitem == 4)
                {
                    if (account.coins < 150)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    if (account.houseslots >= 2)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du kannst max. 2 weitere Hausslots freischalten!", "error", "center");
                        return;
                    }
                    account.houseslots++;
                    account.coins -= 150;
                    Helper.SendNotificationWithoutButton2(player, "Du hast +1 Hausslot freigeschaltet!", "success", "center");
                }
                else if (shopitem == 5)
                {
                    if (account.coins < 150)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    if (account.vehicleslots >= 2)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du kannst max. 3 weitere Fahrzeugslots freischalten!", "error", "center");
                        return;
                    }
                    account.vehicleslots++;
                    account.coins -= 150;
                    Helper.SendNotificationWithoutButton2(player, "Du hast +1 Hausslot freigeschaltet!", "success", "center");
                }
                else if (shopitem == 6)
                {
                    if (account.coins < 100)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    if (account.premium > 0)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast bereits Premium!", "error", "center");
                        return;
                    }
                    account.premium = 1;
                    account.premium_time = Helper.UnixTimestamp() + (30 * 86400);
                    account.coins -= 100;
                    Helper.SendNotificationWithoutButton2(player, "Du hast Premium Bronze für 30 Tage freigeschaltet!", "success", "center");
                }
                else if (shopitem == 7)
                {
                    if (account.coins < 200)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    if (account.premium > 0)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast bereits Premium!", "error", "center");
                        return;
                    }
                    account.premium = 2;
                    account.premium_time = Helper.UnixTimestamp() + (30 * 86400);
                    account.coins -= 200;
                    Helper.SendNotificationWithoutButton2(player, "Du hast Premium Silber für 30 Tage freigeschaltet!", "success", "center");
                }
                else if (shopitem == 8)
                {
                    if (account.coins < 300)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast nicht genügend Coins!", "error", "center");
                        return;
                    }
                    if (account.premium > 0)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Du hast bereits Premium!", "error", "center");
                        return;
                    }
                    account.premium = 3;
                    account.premium_time = Helper.UnixTimestamp() + (30 * 86400);
                    account.coins -= 300;
                    Helper.SendNotificationWithoutButton2(player, "Du hast Premium Gold für 30 Tage freigeschaltet!", "success", "center");
                }
                player.TriggerEvent("Client:ShowCoins", account.coins);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGetShop]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetAFK")]
        public static void OnSetAFK(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                if (character.afk == 0)
                {
                    player.TriggerEvent("SaltyChat_InitToTalkClient", player.Id);
                    character.afk = 1;
                }
                else
                {
                    player.TriggerEvent("SaltyChat_EndTalkClient", player.Id);
                    character.afk = 0;
                }
                player.SetOwnSharedData("Player:Needs", character.hunger + "," + character.thirst + "," + character.afk);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSetAFK]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SaveGov")]
        public static void OnSaveGov(Player player, string csv, int modus)
        {
            try
            {
                string[] govArray = new string[10];
                govArray = csv.Split(',');
                if (modus == 1)
                {
                    adminSettings.lsteuer = Convert.ToInt32(govArray[0]);
                    adminSettings.gsteuer = Convert.ToInt32(govArray[1]);
                    adminSettings.ksteuer = float.Parse(govArray[3]);
                }
                else if (modus == 2)
                {
                    FactionController.factionBudgetsList[0].budget = Convert.ToInt32(govArray[0]);
                    FactionController.factionBudgetsList[1].budget = Convert.ToInt32(govArray[1]);
                    FactionController.factionBudgetsList[2].budget = Convert.ToInt32(govArray[2]);
                    FactionController.factionBudgetsList[3].budget = Convert.ToInt32(govArray[3]);
                    FactionController.factionBudgetsList[4].budget = Convert.ToInt32(govArray[4]);
                }
                else if (modus == 3)
                {
                    adminSettings.grouparray[4] = govArray[0];
                    adminSettings.grouparray[5] = govArray[1];
                    adminSettings.grouparray[6] = govArray[2];
                    adminSettings.grouparray[7] = govArray[3];
                    adminSettings.grouparray[8] = govArray[4];
                    adminSettings.grouparray[9] = govArray[5];
                    adminSettings.grouparray[10] = govArray[6];
                    adminSettings.grouparray[11] = govArray[7];
                    adminSettings.grouparray[12] = govArray[8];
                    adminSettings.grouparray[13] = govArray[9];
                    adminSettings.groupsettings = String.Join(",", adminSettings.grouparray);
                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                    {
                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                        {
                            Helper.SyncThings(p);
                        }
                    }
                }
                SendNotificationWithoutButton(player, "Die Einstellungen wurden gespeichert!", "success", "top-left", 2150);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnReportPlayer]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:RespawnPlayer")]
        public static void OnRespawnPlayer(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                Vector3[] spawnDeath = new Vector3[5]
                   { new Vector3(-668.23755, 315.70834, 88.017-0.025),
                                                     new Vector3(-672.73737, 316.15836, 88.017-0.025),
                                                     new Vector3(-677.48584, 316.79562, 88.017-0.025),
                                                     new Vector3(-679.99023, 321.55426, 88.017006-0.025),
                                                     new Vector3(-679.8668, 326.07382, 88.017-0.025)};

                float[] spawnDeathRot = new float[5]
                                                    { 15.619248f,
                                                     10.032906f,
                                                     15.074937f,
                                                     -83.64569f,
                                                     -80.71787f};

                Random rand = new Random();
                int index = rand.Next(5);
                if (tempData.cuffed > 0)
                {
                    tempData.cuffed = 0;
                    player.TriggerEvent("Client:SetCuff", false);
                }
                player.SetSharedData("Player:Adminsettings", "0,0,0");
                player.TriggerEvent("Client:UnsetDeath");
                player.TriggerEvent("Client:HideCursor");
                character.death = false;
                player.SetOwnSharedData("Player:Death", false);
                Helper.SpawnPlayer(player, spawnDeath[index], spawnDeathRot[index]);
                int cash = rand.Next(375) + 125;
                if (character.faction == 1 || character.faction == 2 || character.faction == 3 || character.faction == 4)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du wurdest behandelt, die Behandlungskosten wurden vom Staat übernommen!", "success", "top-end", 8500);
                    Helper.SetPlayerHealth(player, 100);
                }
                else
                {
                    Helper.SendNotificationWithoutButton(player, $"Du wurdest behandelt, die Behandlung hat {cash}$ gekostet!", "success", "top-end", 8500);
                    CharacterController.SetMoney(player, -cash);
                    Helper.SetGovMoney(cash, "Krankenhausbehandlung");
                    Helper.SetPlayerHealth(player, 85);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnRespawnPlayer]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:Teleport")]
        public static void OnTeleport(Player player, float x, float y, float z, bool check, bool showonly = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (showonly == false)
                {
                    if (character.inhouse > -1)
                    {
                        character.inhouse = -1;
                        player.SetOwnSharedData("Player:InHouse", -1);
                    }
                    SetPlayerPosition(player, new Vector3(x, y, z), 655);
                }
                else
                {
                    if ((character.faction == 1 || character.faction == 2 || character.faction == 3))
                    {
                        FactionsModel faction = FactionController.GetFactionById(character.faction);
                        if (faction.numbername == character.name)
                        {
                            if (FactionController.OperatorBlip == false)
                            {
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    Character character2 = Helper.GetCharacterData(player);
                                    if (character2 != null && character2.faction == character.faction && character2.factionduty == true)
                                    {
                                        p.TriggerEvent("Client:SetMDCBlip", x, y, z);
                                    }
                                }
                                FactionController.OperatorBlip = true;
                                if (character.faction == 1)
                                {
                                    MDCController.SendPoliceMDCMessage(player, $"Neue Leitstellenmarkiert vorhanden!");
                                }
                                else if (character.faction == 2)
                                {
                                    MDCController.SendMedicMDCMessage(player, $"Neue Leitstellenmarkiert vorhanden!");
                                }
                                else if (character.faction == 3)
                                {
                                    MDCController.SendACLSMDCMessage(player, $"Neue Leitstellenmarkiert vorhanden!");
                                }
                            }
                            else
                            {
                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    Character character2 = Helper.GetCharacterData(player);
                                    if (character2 != null && character2.faction == character.faction && character2.factionduty == true)
                                    {
                                        p.TriggerEvent("Client:SetMDCBlip", x, y, z);
                                    }
                                }
                                FactionController.OperatorBlip = false;
                                if (character.faction == 1)
                                {
                                    MDCController.SendPoliceMDCMessage(player, $"Leitstellenmarkierung wurde entfernt!");
                                }
                                else if (character.faction == 2)
                                {
                                    MDCController.SendMedicMDCMessage(player, $"Leitstellenmarkierung wurde entfernt!");
                                }
                                else if (character.faction == 3)
                                {
                                    MDCController.SendACLSMDCMessage(player, $"Leitstellenmarkierung wurde entfernt!");
                                }
                            }
                            return;
                        }
                    }
                    if (player.IsInVehicle)
                    {
                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                        {
                            if (p.IsInVehicle && p.Vehicle == player.Vehicle && player != p)
                            {
                                p.TriggerEvent("Client:CreateWaypoint", x, y);
                                Helper.SendNotificationWithoutButton(p, "Es wurde ein neuer Waypoint geshared!", "success", "top-left", 1850);
                            }
                        }
                    }
                }
                if (check == true)
                {
                    SendNotificationWithoutButton(player, "Erfolgreich teleportiert!", "success", "top-left", 1500);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnTeleport]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetRadioFreq")]
        public static void OnSetRadioFreq(Player player, string freq)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                if (freq == "LS")
                {
                    if (tempData.radiols == false)
                    {
                        SendNotificationWithoutButton(player, $"Lautsprecher angeschaltet!", "success", "top-left", 2750);
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, $"Lautsprecher ausgeschaltet", "success", "top-left", 2750);
                    }
                    tempData.radiols = !tempData.radiols;
                    player.TriggerEvent("Client:Setspeaker", tempData.radiols);
                    return;
                }
                int freqNumeric = Convert.ToInt32(freq);
                if ((!Information.IsNumeric(freq.Trim()) || freqNumeric < -1 || freqNumeric > 999 || freqNumeric < 100) && freqNumeric != -1)
                {
                    SendNotificationWithoutButton(player, "Ungültige Frequenz!", "error", "top-left", 3500);
                    return;
                }
                if (freqNumeric >= 900 && freqNumeric <= 925 && character.faction != 1)
                {
                    SendNotificationWithoutButton(player, "Auf diese Frequenz kann nicht zugegriffen werden!", "error", "top-left", 3500);
                    return;
                }
                if (freqNumeric >= 926 && freqNumeric <= 941 && character.faction != 2)
                {
                    SendNotificationWithoutButton(player, "Auf diese Frequenz kann nicht zugegriffen werden!", "error", "top-left", 3500);
                    return;
                }
                if (freqNumeric >= 942 && freqNumeric <= 945 && character.faction != 3)
                {
                    SendNotificationWithoutButton(player, "Auf diese Frequenz kann nicht zugegriffen werden!", "error", "top-left", 3500);
                    return;
                }
                if (freqNumeric >= 946 && freqNumeric <= 950 && character.faction != 4)
                {
                    SendNotificationWithoutButton(player, "Auf diese Frequenz kann nicht zugegriffen werden!", "error", "top-left", 3500);
                    return;
                }
                if (freqNumeric >= 951 && freqNumeric <= 960 && character.faction <= 0 || character.faction > 3)
                {
                    SendNotificationWithoutButton(player, "Auf diese Frequenz kann nicht zugegriffen werden!", "error", "top-left", 3500);
                    return;
                }
                if (freq == tempData.radio)
                {
                    SendNotificationWithoutButton(player, "Du funkst schon auf dieser Frequenz!", "error", "top-left", 3500);
                    return;
                }
                if (freq == "-1" && tempData.radio == "")
                {
                    SendNotificationWithoutButton(player, "Das Funkgerät ist bereits aus!", "error", "top-left", 3500);
                    return;
                }
                if (freqNumeric != -1)
                {
                    SendNotificationWithoutButton(player, $"Die Frequenz {freqNumeric}mHz wurde erfolgreich eingestellt!", "success", "top-left", 2750);
                    tempData.radio = freq;
                    player.TriggerEvent("Client:Joinradio", tempData.radio);
                }
                else
                {
                    SendNotificationWithoutButton(player, $"Das Funkgerät wurde ausgeschaltet!", "success", "top-left", 2750);
                    player.TriggerEvent("Client:Leaveradio", tempData.radio);
                    tempData.radio = "";
                }
                player.TriggerEvent("Client:ShowRadioSystem", freq);
                return;
            }
            catch (Exception)
            {
                SendNotificationWithoutButton(player, "Ungültige Frequenz!", "error", "top-left", 3500);
            }
        }

        [RemoteEvent("Server:PlayPhoneAnim")]
        public static void PlayPhoneAnim(Player player, bool ignore = false)
        {
            try
            {
                TempData tempData = GetCharacterTempData(player);
                if (tempData == null) return;
                if (tempData.showSmartphone == true)
                {
                    if (!player.IsInVehicle)
                    {
                        if (tempData.inCall2 == true)
                        {
                            if (player.GetData<int>("Player:PhoneAnim") == 1 && ignore == false) return;
                            player.SetData<int>("Player:PhoneAnim", 1);
                            player.SetSharedData("Player:AnimData", $"cellphone@%cellphone_call_listen_base%{50}");
                        }
                        else
                        {
                            if (player.GetData<int>("Player:PhoneAnim") == 2 && ignore == false) return;
                            player.SetData<int>("Player:PhoneAnim", 2);
                            player.SetSharedData("Player:AnimData", $"cellphone@%cellphone_text_in%{50}");
                        }
                    }
                    else
                    {
                        if (tempData.inCall2 == true)
                        {
                            if (player.GetData<int>("Player:PhoneAnim") == 3 && ignore == false) return;
                            player.SetData<int>("Player:PhoneAnim", 3);
                            player.SetSharedData("Player:AnimData", $"anim@cellphone@in_car@ps%cellphone_call_listen_base%{50}");
                        }
                        else
                        {
                            if (player.GetData<int>("Player:PhoneAnim") == 4 && ignore == false) return;
                            player.SetData<int>("Player:PhoneAnim", 4);
                            player.SetSharedData("Player:AnimData", $"anim@cellphone@in_car@ps%cellphone_text_in%{50}");
                        }
                    }
                }
                else
                {
                    player.SetData<int>("Player:PhoneAnim", 0);
                    OnStopAnimation(player);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[PlayPhoneAnim]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:PlayDeathAnim")]
        public static void PlayerDeathAnim(Player player, bool ignore = false)
        {
            try
            {
                TempData tempData = GetCharacterTempData(player);
                if (tempData == null) return;
                if (tempData.follow == false && tempData.followed == false)
                {
                    if (tempData.cuffed == 0)
                    {
                        player.SetSharedData("Player:AnimData", $"dead%dead_a%{2}");
                        player.PlayAnimation("dead", "dead_a", 2);
                    }
                    else
                    {
                        player.SetSharedData("Player:AnimData", $"dead%dead_f%{2}");
                        player.PlayAnimation("dead", "dead_f", 2);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[PlayerDeathAnim]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:PlayShortAnimation")]
        public static void PlayShortAnimation(Player player, string dist, string name, int time = 1000)
        {
            try
            {
                player.SetSharedData("Player:AnimData", $"{dist}%{name}%{(int)(AnimFlags.AnimationFlags.Loop | AnimFlags.AnimationFlags.AllowPlayerControl | AnimFlags.AnimationFlags.OnlyAnimateUpperBody)}");
                NAPI.Task.Run(() =>
                {
                    player.ResetData("Player:PlayCustomAnimation");
                    OnStopAnimation(player);
                }, delayTime: time);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[PlayShortAnimation]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StopAnimationSync")]
        public static void OnStopAnimation(Player player)
        {
            try
            {
                player.PlayAnimation("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 33);
                player.SetSharedData("Player:AnimData", "0");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStopAnimation]: " + e.ToString());
            }
        }


        [RemoteEvent("Server:StopAnimation")]
        public static void OnStopAnimation2(Player player)
        {
            try
            {
                NAPI.Player.StopPlayerAnimation(player);
                player.PlayAnimation("rcmcollect_paperleadinout@", "kneeling_arrest_get_up", 33);
                player.SetSharedData("Player:AnimData", "0");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStopAnimation2]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:PlayScenario")]
        public static void OnPlayScenario(Player player, String scenario)
        {
            try
            {
                player.SetSharedData("Player:AnimData", scenario);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayScenario]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SelectCrosshair")]
        public static void OnSelectCrosshair(Player player, int crosshair)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null) return;

                account.crosshair = crosshair;
                SyncThings(player);
                SendNotificationWithoutButton(player, "Crosshair erfolgreich ausgewählt!", "success", "top-left", 3500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SelectCrosshair]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SelectWalkingStyle")]
        public static void OnSelectWalkingStyle(Player player)
        {
            try
            {
                Helper.ShowPreShop(player, "Laufstilauswahl", 0, 1, 1);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSelectWalkingStyle]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UpdateAnimations")]
        public static void OnUpdateAnimations(Player player, string animations)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                character.animations = animations;
                Helper.SendNotificationWithoutButton2(player, "Animations Hotkeys erfolgreich gespeichert!", "success", "center");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnUpdateAnimations]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:FingerPointSync")]
        public static void OnFingerPoint(Player player, float camPitch, float camHeading)
        {
            try
            {
                NAPI.ClientEvent.TriggerClientEventInRange(player.Position, 100, "Client:FingerPointSync", player.Handle, camPitch, camHeading);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnFingerPoint]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:ShowTabMenu")]
        public static void OnShowTabMenu(Player player)
        {
            List<TabMenu> tabMenuList = new List<TabMenu>();
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p != null)
                    {
                        Account account = Helper.GetAccountData(p);
                        if (account == null) continue;
                        TabMenu tabMenu = new TabMenu();
                        tabMenu.id = p.Id;
                        if (player.GetSharedData<int>("Player:AdminLogin") == 1)
                        {
                            tabMenu.name = account.name;
                        }
                        else
                        {
                            tabMenu.name = "Spieler-" + tabMenu.id;
                        }
                        tabMenu.level = account.level;
                        tabMenu.ping = p.Ping;
                        tabMenu.admin = p.GetSharedData<int>("Player:AdminLogin") == 1 ? true : false;
                        tabMenuList.Add(tabMenu);
                    }
                }
                player.TriggerEvent("Client:StartTabMenu", NAPI.Util.ToJson(tabMenuList.OrderByDescending(o => o.id).ToList()));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnShowTabMenu]: " + e.ToString());
            }
        }

        public static int SelectIDFromName(string name)
        {
            int id = -1;
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "SELECT * FROM users where name=@name LIMIT 1";
                command.Parameters.AddWithValue("@name", name);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        id = reader.GetInt32("id");
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SelectIDFromName]: " + e.ToString());
            }
            return id;
        }

        public static Player GetPlayerByNameOrID(string nameorid)
        {
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    if (p.Handle.ToString() == nameorid || p.Name.ToLower().Contains(nameorid.ToLower()))
                    {
                        return p;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetPlayerByNameOrID]: " + e.ToString());
            }
            return null;
        }

        public static Player GetPlayerByAccountName(string nameorid)
        {
            try
            {
                foreach (Player p in NAPI.Pools.GetAllPlayers())
                {
                    Account account = Helper.GetAccountData(p);
                    if (p.Handle.ToString() == nameorid || account.name.ToLower().Contains(nameorid.ToLower()))
                    {
                        return p;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetPlayerByAccountName]: " + e.ToString());
            }
            return null;
        }

        public static Player GetPlayerByCharacterName(string charactername)
        {
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                Character character = GetCharacterData(p);
                if (character != null && character.name.ToLower() == charactername.ToLower())
                {
                    return p;
                }
            }
            return null;
        }

        public static Player GetPlayerByCharacterId(int id)
        {
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                Character character = GetCharacterData(p);
                if (character != null && character.id == id)
                {
                    return p;
                }
            }
            return null;
        }

        [RemoteEvent("Server:RPQuizFinish")]
        public static void OnRPQuizFinis(Player player, int errors)
        {
            try
            {
                if (errors <= 4)
                {
                    Account account = Helper.GetAccountData(player);
                    if (account == null)
                    {
                        Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                        return;
                    }
                    account.rpquizfinish = 1;
                    CharacterController.GetAvailableCharacters(player, account.id);
                    player.TriggerEvent("Client:HideRPQuestions");
                }
                else
                {
                    Helper.SendNotificationWithoutButton2(player, "Du hast zuviele Fragen falsch beantwortet!", "error", "center");
                    return;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RPQuizFinish]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UploadScreenshot")]
        public static void OnUploadScreenshot(Player player, string screenshot, string screenName)
        {
            try
            {
                if (screenshot.Length <= 5) return;
                Character character = Helper.GetCharacterData(player);
                MySqlCommand command = null;
                Account account = Helper.GetAccountData(player);
                character = Helper.GetCharacterData(player);
                if (character == null || account == null) return;
                if (player.GetData<string>("Player:DiscordUpload").Length >= 3)
                {
                    string text = "[Screenshot Upload - " + player.GetData<string>("Player:DiscordUpload") + "]: " + screenshot;
                    DiscordWebhook(ScreenshotWebhook, text, "nScreens");
                    player.SetData("Player:DiscordUpload", "");
                }
                if (screenName.Contains("Char-"))
                {
                    if (player.GetData<int>("Player:Screenshot") == 0)
                    {
                        character.screen = screenshot;
                    }
                    else
                    {
                        character.screen = screenshot;

                        command = General.Connection.CreateCommand();
                        command.CommandText = "UPDATE characters SET screen=@screen WHERE id=@id";

                        command.Parameters.AddWithValue("@screen", screenshot);
                        command.Parameters.AddWithValue("@id", player.GetData<int>("Player:Screenshot"));

                        command.ExecuteNonQuery();
                    }
                }
                command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO screenshots (userid, charid, screenshot, screenname, timestamp) VALUES (@userid, @charid, @screenshot, @screenname, @timestamp)";

                command.Parameters.AddWithValue("@userid", account.id);
                command.Parameters.AddWithValue("@charid", player.GetData<int>("Player:Screenshot"));
                command.Parameters.AddWithValue("@screenshot", screenshot);
                command.Parameters.AddWithValue("@screenname", screenName);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());

                command.ExecuteNonQuery();

                player.SetData("Player:Screenshot", 0);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnUploadScreenshot]: " + e.ToString());
            }
        }

        public static bool IsInRangeOfPoint(Vector3 playerPos, Vector3 target, float range)
        {
            var direct = new Vector3(target.X - playerPos.X, target.Y - playerPos.Y, target.Z - playerPos.Z);
            var len = direct.X * direct.X + direct.Y * direct.Y + direct.Z * direct.Z;
            return range * range > len;
        }

        public static Int32 UnixTimestamp()
        {

            string timeZoneBerlin = "(GMT+01:00) Deutschland/Berlin Time";
            TimeZoneInfo str_Berlin = TimeZoneInfo.CreateCustomTimeZone("Berlin Time", new TimeSpan(01, 00, 00), timeZoneBerlin, "Berlin Time");
            string data_Berlin = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, str_Berlin).ToString();
            DateTime dt = DateTime.Parse(s: data_Berlin);
            bool winterSummerTime = dt.Equals(TimeZone.CurrentTimeZone.GetDaylightChanges(dt.Year).Start);
            Int32 unixTimestamp;
            if (winterSummerTime == true) //Sommerzeit
            {
                unixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
            else
            {
                unixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - 3600;
            }
            return unixTimestamp;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static void ConsoleLog(string status, string text, bool trace = true)
        {
            try
            {
                if (status == "error")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    NAPI.Util.ConsoleOutput(text, ConsoleColor.Red);
                    if (trace == true)
                    {
                        if (NAPI.Server.GetServerPort() == 22005)
                        {
                            DiscordWebhook(ErrorWebhook, text, "nMonitoring");
                        }
                        DateTime localDate = DateTime.Now;
                        using (StreamWriter file = new StreamWriter(@"./serverdata/logs/errorlog.txt", true))
                        {
                            file.WriteLine("[" + localDate.ToString() + "]\n" + text + "\n");
                        }
                    }
                }
                else if (status == "warning")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    NAPI.Util.ConsoleOutput(text, ConsoleColor.Yellow);
                }
                else if (status == "info")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    NAPI.Util.ConsoleOutput(text, ConsoleColor.Blue);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    NAPI.Util.ConsoleOutput(text, ConsoleColor.Green);
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception) { }
        }

        public static void SendNotificationWithoutButton(Player player, string title, string status = "success", string position = "top-end", int timer = 2500)
        {
            if (position == "center")
            {
                position = "top-left";
            }
            player.TriggerEvent("Client:SendNotificationWithoutButton", title, status, position, timer);
            return;
        }

        public static void SendNotificationWithoutButton2(Player player, string title, string status = "success", string position = "top-end", int timer = 2500)
        {
            player.TriggerEvent("Client:SendNotificationWithoutButton2", title, status, position, timer);
            return;
        }

        public static void SendNotificationWithTimer(Player player, string title, string text, int timer)
        {
            player.TriggerEvent("Client:sendNotificationWithTimer", title, text, timer);
            return;
        }

        public static string GetJobName(int job)
        {
            switch (job)
            {
                case -1: return "Arbeitslos";
                case 1: return "Spediteur";
                case 2: return "Jäger";
                case 3: return "Busfahrer";
                case 4: return "Müllmann";
                case 5: return "Kanalreiniger";
                case 6: return "Taxifahrer";
                case 7: return "Landwirt";
                case 8: return "Geldlieferant";
                default: return "Keinen";
            }
        }

        [RemoteEvent("Server:CreateTaxiPosition")]
        public static void OnCreateTaxiPosition(Player player, string zone)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                TaxiRoutes taxiRoute = null;
                foreach (TaxiRoutes tRoute in Helper.taxiRoutesList)
                {
                    if (tRoute.id == 1)
                    {
                        taxiRoute = tRoute;
                    }
                }
                if (taxiRoute != null)
                {
                    taxiRoute.routes = taxiRoute.routes + $"{player.Position.X.ToString(new CultureInfo("en-US"))},{player.Position.Y.ToString(new CultureInfo("en-US"))},{player.Position.Z.ToString(new CultureInfo("en-US"))},{zone}|";
                    db.Save(taxiRoute);
                    Helper.SendNotificationWithoutButton(player, "Taxiposition wurde erfolgreich erstellt!", "success", "top-end");
                }
                else
                {
                    taxiRoute = new TaxiRoutes();
                    taxiRoute.id = 1;
                    taxiRoute.routes = taxiRoute.routes + $"{player.Position.X.ToString(new CultureInfo("en-US"))},{player.Position.Y.ToString(new CultureInfo("en-US"))},{player.Position.Z.ToString(new CultureInfo("en-US"))},{zone}|";
                    db.Save(taxiRoute);
                    Helper.taxiRoutesList.Add(taxiRoute);
                    Helper.SendNotificationWithoutButton(player, "Taxiposition wurde erfolgreich erstellt!", "success", "top-end");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCreateTaxiPosition]: " + e.ToString());
            }
        }

        public static void CreateTaxiBot()
        {
            try
            {
                string[] advertArray = new string[125];
                foreach (TaxiRoutes taxiRoute in Helper.taxiRoutesList)
                {
                    advertArray = taxiRoute.routes.Split("|");
                    Random random = new Random();
                    int randomArrayIndex1 = random.Next(0, advertArray.Length);
                    int randomArrayIndex2 = random.Next(0, advertArray.Length);
                    if (randomArrayIndex1 == randomArrayIndex2)
                    {
                        randomArrayIndex2 = random.Next(0, advertArray.Length);
                    }
                    string[] positionsArray = new string[4];
                    positionsArray = advertArray[randomArrayIndex1].Split(",");
                    string[] positionsArray2 = new string[4];
                    positionsArray2 = advertArray[randomArrayIndex2].Split(",");

                    TaxiBots taxiBot = new TaxiBots();
                    taxiBot.id = random.Next(1, 9999);
                    taxiBot.from = positionsArray[3];
                    taxiBot.to = positionsArray2[3];
                    taxiBot.v1 = new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")));
                    taxiBot.v2 = new Vector3(float.Parse(positionsArray2[0], new CultureInfo("en-US")), float.Parse(positionsArray2[1], new CultureInfo("en-US")), float.Parse(positionsArray2[2], new CultureInfo("en-US")));
                    taxiBot.money = (int)(taxiBot.v1.DistanceTo(taxiBot.v2)) / 2;
                    Helper.taxiBotList.Add(taxiBot);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateTaxiBot]: " + e.ToString());
            }
        }

        public static void GetAllTaxiRoutes()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (TaxiRoutes taxiRoute in db.Fetch<TaxiRoutes>("SELECT * FROM taxiroutes"))
                {
                    taxiRoutesList.Add(taxiRoute);
                }
                while (Helper.taxiBotList.Count < 6)
                {
                    Helper.CreateTaxiBot();
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllTaxiRoutes]: " + e.ToString());
            }
        }

        public static void GetAllGarbageRoutes()
        {
            try
            {
                string[] advertArray = new string[115];
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (GarbageRoutes garbageRoute in db.Fetch<GarbageRoutes>("SELECT * FROM garbageroutes"))
                {
                    garbageRoutesList.Add(garbageRoute);
                    if (garbageRoute.name == "Garbage")
                    {
                        advertArray = garbageRoute.routes.Split("|");
                        for (int i = 0; i < advertArray.Length; i++)
                        {
                            if (advertArray[i].Length > 0)
                            {
                                string[] positionsArray = new string[3];
                                positionsArray = advertArray[i].Split(",");
                                Garbage garbage = new Garbage();
                                garbage.created = false;
                                garbage.position = new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")));
                                garbage.objectHandle = null;
                                garbageList.Add(garbage);
                            }
                        }
                    }
                }
                CreateNewGarbage();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllGarbageRoutes]: " + e.ToString());
            }
        }

        public static void CreateNewGarbage()
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    while (garbageCount < 150)
                    {
                        Random random = new Random();
                        int index = random.Next(garbageList.Count);
                        if (garbageList[index].created == true) continue;
                        garbageCount++;
                        garbageList[index].created = true;
                        float minus = -0.355f;
                        if (index <= 70)
                        {
                            minus = -0.959f;
                        }
                        if (Helper.GetRandomPercentage(35))
                        {
                            garbageList[index].objectHandle = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_rub_litter_03b"), new Vector3(garbageList[index].position.X, garbageList[index].position.Y, garbageList[index].position.Z + minus), new Vector3(0.0f, 0.0f, 0.0f), 255, 0);
                        }
                        else
                        {
                            minus = minus + 0.015f;
                            garbageList[index].objectHandle = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_rub_litter_03c"), new Vector3(garbageList[index].position.X, garbageList[index].position.Y, garbageList[index].position.Z + minus), new Vector3(0.0f, 0.0f, 0.0f), 255, 0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Helper.ConsoleLog("error", $"[CreateNewGarbage]: " + e.ToString());
                }
            });
        }

        public static void CreateNewATMSpots()
        {
            NAPI.Task.Run(() =>
            {
                try
                {
                    Random rand = new Random();
                    while (atmSpotList.Count < 8)
                    {
                        List<ATMSpots> atmTempList = new List<ATMSpots>();
                        foreach (Blip blip in NAPI.Pools.GetAllBlips())
                        {
                            if (blip.Name.ToLower().Contains("bankautomat"))
                            {
                                ATMSpots atmSpot = new ATMSpots();
                                atmSpot.id = rand.Next(1, 9999);
                                atmSpot.position = blip.Position;
                                atmSpot.value = rand.Next(30000) + 5000;
                                atmTempList.Add(atmSpot);
                            }
                        }
                        int index = rand.Next(atmTempList.Count);
                        var match = atmSpotList.FirstOrDefault(x => x.position == atmTempList[index].position);
                        while (match != null)
                        {
                            index = rand.Next(atmTempList.Count);
                            match = atmSpotList.FirstOrDefault(x => x.position == atmTempList[index].position);
                        }
                        atmSpotList.Add(atmTempList[index]);
                    }
                }
                catch (Exception e)
                {
                    Helper.ConsoleLog("error", $"[CreateNewATMSpots]: " + e.ToString());
                }
            });
        }

        public static void GetAllSpeedCameras()
        {
            try
            {
                string[] positionsArray = new string[3];
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Blitzer blitzer in db.Fetch<Blitzer>("SELECT * FROM speedcameras"))
                {
                    positionsArray = blitzer.position.Split(",");
                    blitzer.colshape = NAPI.ColShape.CreatCircleColShape(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), 7f, 0);
                    blitzer.speedobject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cctv_pole_01a"), new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")) - 3.95), new Vector3(0.0f, 0.0f, blitzer.heading), 255, 0);
                    Helper.blitzerList.Add(blitzer);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllSpeedCameras]: " + e.ToString());
            }
        }

        public static void GetAllCCTVs()
        {
            try
            {
                string[] positionsArray = new string[3];
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (CCTV cctv in db.Fetch<CCTV>("SELECT * FROM cctvs"))
                {
                    positionsArray = cctv.position.Split(",");
                    cctv.cctvobject = NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_cctv_pole_02"), new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")) - 1.15), new Vector3(0.0f, 0.0f, cctv.heading), 255, 0);
                    Helper.cctvList.Add(cctv);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllCCTVs]: " + e.ToString());
            }
        }

        public static void GetAllBusRoutes()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (BusRoutes busRoute in db.Fetch<BusRoutes>("SELECT * FROM busroutes"))
                {
                    string[] advertArray = new string[30];
                    if (busRoute.advert.Length > 0)
                    {
                        advertArray = busRoute.advert.Split("|");

                        for (int i = 0; i < advertArray.Length; i++)
                        {
                            if (advertArray[i].Length > 0)
                            {
                                string[] positionsArray = new string[4];
                                positionsArray = advertArray[i].Split(",");
                                NAPI.TextLabel.CreateTextLabel($"~b~Haltestelle: {positionsArray[3]} [{busRoute.name}]\n~w~Benutze Taste ~b~[F3]~w~ dir den Fahrplan anzusehen!", new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")) + 0.70), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
                                Blip temp = NAPI.Blip.CreateBlip(513, new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US"))), 0.4f, 51);
                                NAPI.Blip.SetBlipShortRange(temp, true);
                                NAPI.Blip.SetBlipScale(temp, 0.4f);
                                NAPI.Blip.SetBlipName(temp, "Bushaltestelle");
                            }
                        }
                    }
                    busRoutesList.Add(busRoute);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllBusRoutes]: " + e.ToString());
            }
        }

        public static String GetBusRoutes()
        {
            string busRoutesString = "";
            try
            {
                foreach (BusRoutes busRoutes in busRoutesList)
                {
                    busRoutesString += $"{busRoutes.name}, ";
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetBusRoutes]: " + e.ToString());
            }
            busRoutesString = busRoutesString.Substring(0, busRoutesString.Length - 2);
            return busRoutesString;
        }

        public static void GetNextBusStation(Player player, string busRoute)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                if (player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "bus" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "coach" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "rentalbus" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                {
                    foreach (BusRoutes busRoutes in busRoutesList)
                    {
                        if (busRoutes.name == busRoute)
                        {
                            string[] routesArray = new string[30];
                            if (busRoutes.routes.Length > 0)
                            {
                                routesArray = busRoutes.routes.Split("|");

                                for (int i = 0; i < routesArray.Length; i++)
                                {
                                    if (routesArray[i].Length > 0)
                                    {
                                        if (i == player.GetData<int>("Player:BusStation"))
                                        {
                                            string[] positionsArray = new string[4];
                                            positionsArray = routesArray[i].Split(",");
                                            player.SetData<int>("Player:BusStation", i + 1);
                                            if (Helper.IsABusDriver(player) == 2)
                                            {
                                                player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Canny Bus Group - {busRoutes.name}\n~y~Nächste Haltestelle: {positionsArray[3]}");
                                            }
                                            else
                                            {
                                                player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)} - {busRoutes.name}\n~y~Nächste Haltestelle: {positionsArray[3]}");
                                            }
                                            if (i < routesArray.Length && i > 0)
                                            {
                                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                                {
                                                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetOwnSharedData<bool>("Player:Death") == false && p.Vehicle == player.Vehicle)
                                                    {
                                                        p.TriggerEvent("Client:TextToSpeech", $"Nächste Haltestelle: {positionsArray[3]}");
                                                    }
                                                }
                                            }
                                            player.TriggerEvent("Client:SetBusDriverCP", float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")));
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllBusRoutes]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:IsAtNextBusStation")]
        public static void IsAtNextBusStation(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "bus" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "coach" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "rentalbus" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                {
                    foreach (BusRoutes busRoutes in busRoutesList)
                    {
                        if (busRoutes.name == player.GetData<String>("Player:BusRoute"))
                        {
                            string[] routesArray = new string[30];
                            if (busRoutes.routes.Length > 0)
                            {
                                routesArray = busRoutes.routes.Split("|");
                                for (int i = 0; i < routesArray.Length; i++)
                                {
                                    if (routesArray.Length == player.GetData<int>("Player:BusStation"))
                                    {
                                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:BusTime"))
                                        {
                                            AntiCheatController.OnCallAntiCheat(player, "Teleport to Checkpoint Cheat", "Busfahrer", false);
                                            return;
                                        }
                                        int skill = character.busskill / 35;
                                        string[] positionsArray = new string[4];
                                        positionsArray = routesArray[0].Split(",");
                                        string[] positionsArray2 = new string[4];
                                        positionsArray2 = routesArray[player.GetData<int>("Player:BusStation") - 1].Split(",");
                                        int salary = ((int)new Vector3(float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US"))).DistanceTo(new Vector3(float.Parse(positionsArray2[0], new CultureInfo("en-US")), float.Parse(positionsArray2[1], new CultureInfo("en-US")), float.Parse(positionsArray2[2], new CultureInfo("en-US")))));
                                        salary = salary / 2 * 2;
                                        salary = salary + (salary / 100 * skill);
                                        if (character.mygroup != -1 && Helper.IsABusDriver(player) == 1)
                                        {
                                            int money = 0;
                                            Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                            Bank bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                            if (bank != null)
                                            {
                                                money = salary;
                                                int prov = 0;
                                                if (mygroup.provision > 0)
                                                {
                                                    prov = money / 100 * mygroup.provision;
                                                }
                                                if (prov > 0 && character.defaultbank != "n/A")
                                                {
                                                    Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                                    bank.bankvalue += money;
                                                    bank.bankvalue -= prov;
                                                    if (bank2 != null)
                                                    {
                                                        bank2.bankvalue += prov;
                                                    }
                                                    Helper.SendNotificationWithoutButton(player, $"Route erfolgreich beendet, {money}$ werden dem Konto deiner Firma gutgeschrieben. Du erhälst {prov}$ Provision!", "success", "top-left", 5500);
                                                    Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat eine Route erfolgreich beendet und {money - prov}$ erwirtschaftet!");
                                                }
                                                else
                                                {
                                                    Helper.SendNotificationWithoutButton(player, $"Route erfolgreich beendet, {money}$ werden dem Konto deiner Firma gutgeschrieben!", "success", "top-left", 5500);
                                                    Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat eine Route erfolgreich beendet und {money}$ erwirtschaftet!");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Helper.SendNotificationWithoutButton(player, $"Route erfolgreich beendet, du erhältst {salary}$ für den nächsten Payday gutgeschrieben!", "success", "top-left", 5500);
                                            character.nextpayday += salary;
                                        }
                                        player.SetData<int>("Player:BusStation", 0);
                                        GetNextBusStation(player, busRoutes.name);
                                        if (character.busskill < 175)
                                        {
                                            character.busskill++;
                                        }
                                        player.SetData<int>("Player:BusTime", Helper.UnixTimestamp() + (60));
                                        return;
                                    }
                                    else
                                    {
                                        GetNextBusStation(player, busRoutes.name);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtNextBusStation]: " + e.ToString());
            }
        }

        //Garbage
        public static void GetNextGarbageStation(Player player, string garbageRoute, int check = 1)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;
                if (character.job == 4)
                {
                    foreach (GarbageRoutes garbageRoutes in garbageRoutesList)
                    {
                        if (garbageRoutes.name != "Garbage" && garbageRoutes.name == garbageRoute)
                        {
                            string[] routesArray = new string[30];
                            if (garbageRoutes.routes.Length > 0)
                            {
                                routesArray = garbageRoutes.routes.Split("|");

                                for (int i = 0; i < routesArray.Length; i++)
                                {
                                    if (routesArray[i].Length > 0)
                                    {
                                        if (i == player.GetData<int>("Player:GarbageStation"))
                                        {
                                            string[] positionsArray = new string[3];
                                            positionsArray = routesArray[i].Split(",");
                                            if (!player.HasData("Player:GarbagePlayer2"))
                                            {
                                                if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                                                {
                                                    Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                                                    garbagePlayer.SetData<int>("Player:GarbageStation", i + 1);
                                                    garbagePlayer.TriggerEvent("Client:SetGarbageDriverCP", 0, float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")));
                                                }
                                                player.SetData<int>("Player:GarbageStation", i + 1);
                                            }
                                            player.TriggerEvent("Client:SetGarbageDriverCP", 1, float.Parse(positionsArray[0], new CultureInfo("en-US")), float.Parse(positionsArray[1], new CultureInfo("en-US")), float.Parse(positionsArray[2], new CultureInfo("en-US")));
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetNextGarbageStation]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:NextGarbageStation")]
        public static void IsNextGarbageStation(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null)
                {
                    if (tempData.jobVehicle != null)
                    {
                        player.SetData<bool>("Player:Garbage", true);
                        player.SetSharedData("Player:AnimData", $"anim@heists@narcotics@trash%walk%{(int)(AnimFlags.AnimationFlags.AllowPlayerControl)}");
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsNextGarbageStation]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:NextGarbageStation2")]
        public static void IsNextGarbageStation2(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (!player.IsInVehicle)
                {
                    foreach (GarbageRoutes garbageRoutes in garbageRoutesList)
                    {
                        if (garbageRoutes.name != "Garbage" && garbageRoutes.name == player.GetData<String>("Player:GarbageRoute"))
                        {
                            string[] routesArray = new string[30];
                            if (garbageRoutes.routes.Length > 0)
                            {
                                routesArray = garbageRoutes.routes.Split("|");
                                for (int i = 0; i < routesArray.Length; i++)
                                {
                                    if (routesArray.Length == player.GetData<int>("Player:GarbageStation"))
                                    {
                                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:GarbageTime"))
                                        {
                                            AntiCheatController.OnCallAntiCheat(player, "Teleport to Checkpoint Cheat", "Müllmann", false);
                                            return;
                                        }
                                        Random rnd = new Random();
                                        int salary = routesArray.Length * (32 + rnd.Next(0, 3));
                                        Helper.SendNotificationWithoutButton(player, $"Müllroute erfolgreich beendet, du erhältst {salary}$ für den nächsten Payday gutgeschrieben!", "success", "top-left", 5500);
                                        player.SetData<int>("Player:GarbageStation", 0);
                                        if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                                        {
                                            salary = salary + (salary / 100 * 3);
                                            Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                                            Character character2 = Helper.GetCharacterData(garbagePlayer);
                                            if (player.Position.DistanceTo(garbagePlayer.Position) <= 10.5)
                                            {
                                                Helper.SendNotificationWithoutButton(garbagePlayer, $"Müllroute erfolgreich beendet, du erhältst {salary}$ für den nächsten Payday gutgeschrieben!", "success", "top-left", 5500);
                                                character2.nextpayday += salary;
                                            }
                                            garbagePlayer.SetData<int>("Player:GarbageStation", 0);
                                        }
                                        character.nextpayday += salary;
                                    }
                                    else
                                    {
                                        if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                                        {
                                            Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                                            garbagePlayer.SetData<int>("Player:GarbageTime", Helper.UnixTimestamp() + (10));
                                        }
                                        player.SetData<int>("Player:GarbageTime", Helper.UnixTimestamp() + (10));
                                        GetNextGarbageStation(player, garbageRoutes.name, 1);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsNextGarbageStation2]: " + e.ToString());
            }
        }

        public static void GetAllFactions(int check = 0)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                if (check == 0)
                {
                    foreach (FactionsModel faction in db.Fetch<FactionsModel>("SELECT * FROM factions"))
                    {
                        factionList.Add(faction);
                    }
                }
                else
                {
                    foreach (FactionsModel faction in db.Fetch<FactionsModel>("SELECT * FROM factions"))
                    {
                        foreach (FactionsModel faction2 in Helper.factionList)
                        {
                            if (faction.id == faction2.id)
                            {
                                if (faction.tag != faction2.tag)
                                {
                                    foreach (Cars car in Cars.carList)
                                    {
                                        if (car.vehicleData.owner == "faction-" + faction.id)
                                        {
                                            car.vehicleData.plate.Replace(faction.tag, faction2.tag);
                                            if (car.vehicleHandle != null)
                                            {
                                                car.vehicleHandle.NumberPlate = car.vehicleData.plate;
                                            }
                                        }
                                    }
                                }
                                faction2.name = faction.name;
                                faction2.tag = faction.tag;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllFactions]: " + e.ToString());
            }
        }

        public static void SaveFactions()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (FactionsModel faction in Helper.factionList)
                {
                    db.Save(faction);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SaveFaction]: " + e.ToString());
            }
        }

        public static void GetWhitelist()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (Whitelist whitelist in db.Fetch<Whitelist>("SELECT * FROM whitelist"))
                {
                    whitelistList.Add(whitelist);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetWhitelist]: " + e.ToString());
            }
        }

        public static FactionsModel GetFactionById(int factionid)
        {
            foreach (FactionsModel value in factionList)
            {
                if (value.id == factionid)
                {
                    return value;
                }
            }
            return null;
        }

        public static string GetFactionName(int factionid)
        {
            foreach (var value in factionList)
            {
                if (value.id == factionid)
                {
                    return value.name;
                }
            }
            return "Keine Fraktion";
        }

        public static int GetFactionCountDuty(int factionid)
        {
            int count = 0;
            try
            {
                foreach (Player player in NAPI.Pools.GetAllPlayers())
                {
                    Character character = Helper.GetCharacterData(player);
                    if (character != null && character.faction == factionid && character.factionduty == true)
                    {
                        count++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionCountDuty]: " + e.ToString());
            }
            return count;
        }


        public static string GetFactionTag(int factionid)
        {
            foreach (var value in factionList)
            {
                if (value.id == factionid)
                {
                    return value.tag;
                }
            }
            return "Keine Fraktion";
        }

        public static string GetFactionRangName(Player player, int factionid, int rangid)
        {
            string rangname = "Kein Rang";
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null || factionid <= 0 || rangid <= 0) return rangname;

                TempData tempData = Helper.GetCharacterTempData(player);
                if (tempData.last_rang_check < Helper.UnixTimestamp())
                {
                    MySqlCommand command = General.Connection.CreateCommand();
                    command.CommandText = "SELECT rang" + rangid + " FROM factionsrangs WHERE id=@id LIMIT 1";
                    command.Parameters.AddWithValue("@id", factionid);

                    tempData.last_rang_check = Helper.UnixTimestamp() + (60 * 15);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            rangname = reader.GetString("rang" + rangid);
                        }
                        reader.Close();
                    }
                }
                else
                {
                    return tempData.rangname;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFactionRangName]: " + e.ToString());
            }
            return rangname;
        }

        public static void SendRadiusMessage(string message, int radius, Player player)
        {
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                if (Account.IsPlayerLoggedIn(p) && IsInRangeOfPoint(p.Position, player.Position, radius))
                {
                    SendChatMessage(p, message);
                }
            }
        }

        public static void SendRadioMessage(string message, string freq)
        {
            foreach (Player p in NAPI.Pools.GetAllPlayers())
            {
                TempData tempData = Helper.GetCharacterTempData(p);
                if (Account.IsPlayerLoggedIn(p) && tempData.radio == freq)
                {
                    SendChatMessage(p, message);
                }
            }
        }

        public static void SendPremiumMessage(string message, int premiumlevel, Player player)
        {
            string nachricht = message;
            Account account = Helper.GetAccountData(player);
            foreach (Player c in NAPI.Pools.GetAllPlayers())
            {
                Account cacc = Helper.GetAccountData(c);
                if (Account.IsPlayerLoggedIn(c) && cacc.premium > 0)
                {
                    string premiums = "";
                    switch (premiumlevel)
                    {
                        case 1: premiums = "!{#663300}Bronze"; break;
                        case 2: premiums = "!{#999999}Silber"; break;
                        case 3: premiums = "!{#FFcc00}Gold"; break;

                    }
                    string text = "!{#008080}[Premium Chat] " + account.name + "[" + c.Id + "]" + "(" + premiums + "!{#008080}) :!{#008080} " + nachricht.Remove(0, 1);
                    SendChatMessage(c, text, false);
                }
            }
        }

        public static void SendHouseMessage(int houseID, string message)
        {
            if (houseID <= 0) return;
            foreach (Player player in NAPI.Pools.GetAllPlayers())
            {
                Character character = Helper.GetCharacterData(player);
                if (Account.IsPlayerLoggedIn(player) && character.inhouse == houseID)
                {
                    SendChatMessage(player, message);
                }
            }
        }

        public static void SendChatMessage(Player player, string message, bool removefirst = false)
        {
            Character character = GetCharacterData(player);
            Account account = GetAccountData(player);
            if (character != null && account != null)
            {
                if (removefirst == true)
                {
                    message = message.Remove(0, 1);
                }
                NAPI.Chat.SendChatMessageToPlayer(player, message);
            }
        }

        //Testmessage
        public static void SendTestMessage(string message, Player player)
        {
            Account account = Helper.GetAccountData(player);
            message = message.Remove(0, 1);
            foreach (Player c in NAPI.Pools.GetAllPlayers())
            {
                Account cacc = Helper.GetAccountData(c);
                if (Account.IsPlayerLoggedIn(c) && (c.GetOwnSharedData<bool>("Player:Testmodus") == true || cacc.adminlevel >= 1))
                {
                    string text = "!{#07C71B}[Testchat] " + account.name + ": " + message;
                    SendChatMessage(c, text, false);
                }
            }
        }

        //Adminsystem
        public static void SendAdminMessage(string message, Player player)
        {
            Account account = Helper.GetAccountData(player);
            message = message.Remove(0, 1);
            foreach (Player c in NAPI.Pools.GetAllPlayers())
            {
                Account cacc = Helper.GetAccountData(c);
                if (Account.IsPlayerLoggedIn(c) && cacc.adminlevel >= 1)
                {
                    string text = "!{#0099ff}[Adminchat] " + account.name + ": " + message;
                    SendChatMessage(c, text, false);
                }
            }
        }

        public static void SendAdminMessage2(string message, int adminlevel, bool todiscord = true)
        {
            foreach (Player c in NAPI.Pools.GetAllPlayers())
            {
                Account cacc = Helper.GetAccountData(c);
                if (c.Exists && c.GetOwnSharedData<bool>("Player:Spawned") == true && cacc.adminlevel >= adminlevel)
                {
                    string text2 = "!{#0099ff}[Benachrichtigung]: " + message;
                    SendChatMessage(c, text2, false);
                }
            }
            if (todiscord == true)
            {
                DiscordWebhook(AdminNotificationWebHook, message, "Gameserver");
            }
        }

        public static void SendAdminMessageToAll(string message, bool removefirst = false)
        {
            foreach (Player c in NAPI.Pools.GetAllPlayers())
            {
                if (c.Exists && c.GetOwnSharedData<bool>("Player:Spawned") == true)
                {
                    SendChatMessage(c, message, removefirst);
                }
            }
        }

        public static void SendAdminMessage3(string message, int time = 14500, bool sendtodiscord = false)
        {

            foreach (Player player in NAPI.Pools.GetAllPlayers())
            {
                if (player != null && player.Exists && player.GetOwnSharedData<bool>("Player:Spawned") == true)
                {
                    player.TriggerEvent("Client:AdminInfoMessage", message, time);
                }
            }
            if (sendtodiscord == true)
            {
                DiscordWebhook(AdminNotificationWebHook, message, "Gameserver");
            }
        }

        public static void GetAdminSettings()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                adminSettings = db.Single<AdminSettings>("WHERE id = 1");
                string[] govArray = new string[10];

                //Govvalue
                Bank bank = BankController.GetBankByBankNumber("SA3701-100000");
                if (bank != null)
                {
                    adminSettings.govvalue = bank.bankvalue;
                }

                //Schatzsucher Limit reseten
                adminSettings.dailyguesslimit = 0;

                //Groupsettings
                govArray = adminSettings.groupsettings.Split(",");

                adminSettings.grouparray = new string[14];

                adminSettings.grouparray[0] = "" + adminSettings.lsteuer;
                adminSettings.grouparray[1] = "" + adminSettings.gsteuer;
                adminSettings.grouparray[2] = "" + adminSettings.ksteuer;
                adminSettings.grouparray[3] = "" + adminSettings.towedcash;
                adminSettings.grouparray[4] = govArray[0];
                adminSettings.grouparray[5] = govArray[1];
                adminSettings.grouparray[6] = govArray[2];
                adminSettings.grouparray[7] = govArray[3];
                adminSettings.grouparray[8] = govArray[4];
                adminSettings.grouparray[9] = govArray[5];
                adminSettings.grouparray[10] = govArray[6];
                adminSettings.grouparray[11] = govArray[7];
                adminSettings.grouparray[12] = govArray[8];
                adminSettings.grouparray[13] = govArray[9];
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAdminSettings]: " + e.ToString());
            }
        }

        public static void SaveAdminSettings()
        {
            try
            {

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Save(Helper.adminSettings);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAdminSettings]: " + e.ToString());
            }
        }

        public static void LoadSpedVehicles()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (SpedVehicles spedVehicle in db.Fetch<SpedVehicles>("SELECT * FROM spedvehicles"))
                {
                    spedVehiclesList.Add(spedVehicle);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[LoadSpedVehicles]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnInput2")]
        public static void OnOnInput2(Player player, string input, string flag)
        {
            try
            {
                int number = -1;
                Account account = Helper.GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                House house = null;
                if (flag.ToLower() != "buyvehicle")
                {
                    player.TriggerEvent("Client:PlayerFreeze", false);
                }
                number = Convert.ToInt32(input);
                switch (flag.ToLower())
                {
                    case "buyvehicle":
                        {
                            string vColor = "";
                            Groups group = null;
                            number = Convert.ToInt32(input);
                            Business bizz = Business.GetBusinessById(player.GetData<int>("Player:LastBizz"));
                            if (bizz != null)
                            {
                                if (number == 1)
                                {
                                    int maxVehicles = DealerShipController.MaxVehicles(player, 1);
                                    if (DealerShipController.CountVehicles("character-" + character.id) > maxVehicles)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, $"Du kannst max. {maxVehicles} Fahrzeuge besitzen!", "error", "center");
                                        return;
                                    }
                                }
                                else
                                {
                                    group = GroupsController.GetGroupById(character.mygroup);
                                    if (group == null)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du hast keine Gruppierung ausgewählt!", "error", "center");
                                        return;
                                    }
                                    GroupsMembers groupMember = GroupsController.GetGroupMemberById(character.id, group.id);
                                    if (groupMember.rang < 10)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Du darfst für deine Gruppierung keine Fahrzeuge kaufen!", "error", "center");
                                        return;
                                    }
                                    int maxVehicles = DealerShipController.MaxVehicles(player, 2);
                                    if (DealerShipController.CountVehicles("character-" + character.id) > maxVehicles)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, $"Deine Gruppierung kann max. {maxVehicles} Fahrzeuge besitzen!", "error", "center");
                                        return;
                                    }
                                }
                                string[] carArray = new string[4];
                                carArray = player.GetData<string>("Player:VehicleBuyData").Split(",");
                                int price = (int)(Convert.ToInt32(carArray[1]) * bizz.multiplier);
                                Bank bank = null;
                                if (bizz.id == 23 && SetAndGetCharacterLicense(player, 1, 1337) != "1")
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Motorradschein!", "error", "top-end", 2250);
                                    return;
                                }
                                if (bizz.id == 29 && SetAndGetCharacterLicense(player, 2, 1337) != "1")
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Truckerschein!", "error", "top-end", 2250);
                                    return;
                                }
                                if (bizz.id == 31 && SetAndGetCharacterLicense(player, 3, 1337) != "1")
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Bootsschein!", "error", "top-end", 2250);
                                    return;
                                }
                                if (bizz.id == 32 && SetAndGetCharacterLicense(player, 4, 1337) != "1")
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Flugschein!", "error", "top-end", 2250);
                                    return;
                                }
                                if (bizz.id == 23 && bizz.id != 29 && bizz.id != 31 && bizz.id != 32 && SetAndGetCharacterLicense(player, 0, 1337) != "1")
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Führerschein!", "error", "top-end", 2250);
                                    return;
                                }
                                if (bizz.products < 200)
                                {
                                    Helper.SendNotificationWithoutButton2(player, $"Wir haben leider aktuell keine Fahrzeuge mehr auf Lager!", "error", "center");
                                    return;
                                }
                                if (carArray[2] == "1")
                                {
                                    if (character.cash < price)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error", "center");
                                        return;
                                    }
                                }
                                else
                                {
                                    bank = BankController.GetDefaultBank(player, character.defaultbank);
                                    if (bank == null)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Es wurde kein Standardkonto gefunden!", "error", "center");
                                        return;
                                    }
                                    if (bank.bankvalue < Convert.ToInt32(price))
                                    {
                                        Helper.SendNotificationWithoutButton2(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "center");
                                        return;
                                    }
                                }
                                if (carArray[0].ToLower() == "benson" || carArray[0].ToLower() == "boxville2" || carArray[0].ToLower().Contains("mule") || carArray[0].ToLower() == "pounder" || carArray[0].ToLower() == "pounder2" || carArray[0].ToLower() == "phantom" || carArray[0].ToLower() == "phantom3" || carArray[0].ToLower() == "packer" || carArray[0].ToLower().Contains("trailer") || carArray[0].ToLower() == "tanker")
                                {
                                    if (number == 1)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug wird nur an Firmen mit einer Speditionslizenz verkauft!", "error", "center");
                                        return;
                                    }
                                    else
                                    {
                                        string[] licArray = new string[9];
                                        licArray = group.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[0]) == 0)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma besitzt keine Speditionslizenz!", "error", "center");
                                            return;
                                        }
                                    }
                                }
                                if (carArray[0].ToLower() == "bus" || carArray[0].ToLower() == "coach" || carArray[0].ToLower() == "rentalbus" || carArray[0].ToLower() == "tourbus" || carArray[0].ToLower() == "taxi")
                                {
                                    if (number == 1)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug wird nur an Firmen mit einer Personenbeförderungslizenz verkauft!", "error", "center");
                                        return;
                                    }
                                    else
                                    {
                                        string[] licArray = new string[9];
                                        licArray = group.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[3]) == 0)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma besitzt keine Personenbeförderungslizenz!", "error", "center");
                                            return;
                                        }
                                    }
                                }
                                if (carArray[0].ToLower() == "stockade")
                                {
                                    if (number == 1)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug wird nur an Firmen mit einer Sicherheitslizenz verkauft!", "error", "center");
                                        return;
                                    }
                                    else
                                    {
                                        string[] licArray = new string[9];
                                        licArray = group.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[4]) == 0)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma besitzt keine Sicherheitslizenz!", "error", "center");
                                            return;
                                        }
                                    }
                                }
                                if (carArray[0].ToLower() == "utillitruck2" || carArray[0].ToLower() == "utillitruck3" || carArray[0].ToLower() == "towtruck" || carArray[0].ToLower() == "towtruck2" || carArray[0].ToLower() == "flatbed")
                                {
                                    if (number == 1)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug wird nur an Firmen mit einer Mechatronikerlizenz verkauft!", "error", "center");
                                        return;
                                    }
                                    else
                                    {
                                        string[] licArray = new string[9];
                                        licArray = group.licenses.Split("|");
                                        if (Convert.ToInt32(licArray[2]) == 0)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Deine Firma besitzt keine Mechatronikerlizenz!", "error", "center");
                                            return;
                                        }
                                    }
                                }
                                if (!ItemsController.CanPlayerHoldItem(player, 55))
                                {
                                    SendNotificationWithoutButton2(player, "Du hast keinen Platz mehr im Inventar für den Fahrzeugschlüssel!", "error", "center");
                                    return;
                                }
                                VehicleData vehicleData = new VehicleData();
                                vehicleData.id = Cars.carList.Count + 1;
                                vehicleData.owner = number == 1 ? "character-" + character.id : "group-" + group.id;
                                vehicleData.vehiclename = carArray[0];
                                vehicleData.plate = "n/A";
                                vehicleData.fuel = -1;
                                vehicleData.engine = 0;
                                vehicleData.status = 1;
                                SetPlayerPosition(player, tempData.furnitureOldPosition);
                                player.Dimension = 0;
                                Vehicle vehicle = null;
                                vColor = $"{Convert.ToInt32(carArray[3])},{Convert.ToInt32(carArray[3])},-1,-1";
                                vehicleData.color = vColor;
                                if (bizz.id == 22)
                                {
                                    vehicleData.position = $"145.23679|-140.08275|54.24724|-20.416449|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(145.23679, -140.08275, 54.24724 + 0.25), -20.416449f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 23)
                                {
                                    vehicleData.position = $"274.08023|-1159.84|28.617239|87.37559|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(274.08023, -1159.84, 28.617239 + 0.25), 87.37559f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 24)
                                {
                                    vehicleData.position = $"-31.80824|-1091.3527|25.65422|-31.949799|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(-31.80824, -1091.3527, 25.65422 + 0.25), -31.949799f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 25)
                                {
                                    vehicleData.position = $"-68.83505|82.71136|71.28684|63.46722|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(-68.83505, 82.71136, 71.28684 + 0.25), 63.46722f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 26)
                                {
                                    vehicleData.position = $"-23.408432|-1678.2253|29.160381|110.107635|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(-23.408432, -1678.2253, 29.160381 + 0.25), 110.107635f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 27)
                                {
                                    vehicleData.position = $"1214.5273|2708.1516|37.477882|156.33295|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(1214.5273, 2708.1516, 37.477882 + 0.25), 156.33295f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 28)
                                {
                                    vehicleData.position = $"-201.7464|6204.731|31.017431|46.196392|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(-201.7464, 6204.731, 31.017431 + 0.25), 46.196392f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 29)
                                {
                                    vehicleData.position = $"663.9333|-2687.8196|6.147993|90.25697|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(663.9333, -2687.8196, 6.147993 + 0.25), 90.25697f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                else if (bizz.id == 30)
                                {
                                    vehicleData.position = $"-1139.5806|-211.71169|37.537098|74.32476|0";
                                    vehicle = Cars.createNewCar(carArray[0].ToLower(), new Vector3(-1139.5806, -211.71169, 37.537098 + 0.25), 74.32476f, Convert.ToInt32(carArray[3]), Convert.ToInt32(carArray[3]), vehicleData.owner, "n/A", true, false, true, 0, vehicleData, true);
                                }
                                if (vehicle != null && vehicle.Class != 13)
                                {
                                    vehicleData.tuev = Helper.UnixTimestamp() + (93 * 86400);
                                }
                                else
                                {
                                    vehicleData.tuev = -50;
                                }
                                vehicleData.health = "1000.0|1000.0|1000.0";
                                Items newitem = ItemsController.CreateNewItem(player, character.id, "Fahrzeugschlüssel", "Player", 1, ItemsController.GetFreeItemID(player), vehicleData.vehiclename + ": " + vehicleData.id);
                                if (newitem != null)
                                {
                                    tempData.itemlist.Add(newitem);
                                }
                                if (carArray[2] == "1")
                                {
                                    CharacterController.SetMoney(player, -price);
                                }
                                else
                                {
                                    bank.bankvalue -= price;
                                    Helper.BankSettings(bank.banknumber, "Autohaus Rechnung bezahlt", price.ToString(), character.name);
                                }
                                int gewinn = 0;
                                if (bizz.id != 28)
                                {
                                    gewinn = 6250 + (price / 100 * 5);
                                    Business.ManageBizzCash(bizz, gewinn, true);
                                    bizz.govcash += (gewinn / 100) * Helper.adminSettings.gsteuer;
                                    bizz.products -= 200;
                                }
                                else
                                {
                                    gewinn = 3150 + (price / 100 * 10);
                                    Business.ManageBizzCash(bizz, gewinn, true);
                                    bizz.govcash += (gewinn / 100) * Helper.adminSettings.gsteuer;
                                    bizz.products -= 83;
                                }
                                player.TriggerEvent("Client:PlayerFreeze", false);
                                player.SetData<int>("Player:LastBizz", 0);
                                player.TriggerEvent("Client:ShowDealerShip", "n/A", "n/A", null, null, -1);
                                Helper.SendNotificationWithoutButton2(player, "Fahrzeug erfolgreich erworben, das Fahrzeug steht direkt hier vorne, alles weitere findest du im F2 Menü!", "success", "center", 3750);
                                player.ResetData("Player:VehicleBuyData");
                                if (number == 2)
                                {
                                    Helper.CreateGroupLog(group.id, $"{character.name} hat ein {carArray[0]} für {price}$, für die Gruppierung erworben!");
                                }
                                if (account.faqarray[9] == "0")
                                {
                                    account.faqarray[9] = "1";
                                }
                            }
                            else
                            {
                                player.ResetData("Player:VehicleBuyData");
                                SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "center");
                            }
                            break;
                        }
                    case "acceptshare":
                        {
                            Player fromplayer = player.GetData<Player>("Player:GroupInvitePlayer");
                            if (fromplayer == null || !player.IsInVehicle)
                            {
                                SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-end");
                                return;
                            }
                            TempData tempData2 = Helper.GetCharacterTempData(fromplayer);
                            SpedOrders spedOrder = tempData2.order;
                            SpedVehicles spedVehicle = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                            if (spedVehicle.capa < spedOrder.capa)
                            {
                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug ist nicht für den Auftrag geeignet!", "error", "top-end", 4500);
                                return;
                            }
                            if (number == 2)
                            {
                                SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Anfrage abgelehnt!", "error", "top-end");
                            }
                            else
                            {
                                SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Anfrage angenommen!", "success", "top-end");
                                SendNotificationWithoutButton(player, "Anfrage angenommen!", "success", "top-end");
                                tempData.jobstatus = 1;
                                tempData.order = spedOrder;
                                player.TriggerEvent("Client:HideSpedition");
                                Helper.SendNotificationWithoutButton(player, $"Auftrag angenommen, bitte hol die Ware jetzt bei/m {spedOrder.from} ab!", "success", "top-left", 4000);
                                player.TriggerEvent("Client:CreateWaypoint", spedOrder.position1.X, spedOrder.position1.Y);
                                player.TriggerEvent("Client:CreateMarker", spedOrder.position1.X, spedOrder.position1.Y, spedOrder.position1.Z, 39);
                                tempData.jobColshape = NAPI.ColShape.CreatCircleColShape(spedOrder.position1.X, spedOrder.position1.Y, 3.5f, player.Dimension);
                            }
                            player.SetData<Player>("Player:GroupInvitePlayer", null);
                            break;
                        }
                    case "acceptshare2":
                        {
                            Player fromplayer = player.GetData<Player>("Player:GroupInvitePlayer");
                            if (fromplayer == null)
                            {
                                SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-end");
                                return;
                            }
                            if (number == 2)
                            {
                                SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Anfrage abgelehnt!", "error", "top-end");
                                player.SetData<Player>("Player:GroupInvitePlayer", null);
                            }
                            else
                            {
                                player.TriggerEvent("Client:PressedEscape");
                                SendNotificationWithoutButton(player, $"Müllrouten Share angenommen, du bist der Fahrer! Du findest die Mülltonnen rot auf der Karte als Müllwagen markiert!", "success", "top-end", 3650);
                                SendNotificationWithoutButton(fromplayer, $"Müllrouten Share wurde angenommen, du bist der Müllabholer! Du findest die Mülltonnen rot auf der Karte als Müllwagen markiert!", "success", "top-end", 3650);
                                player.SetData<String>("Player:GarbageRoute", fromplayer.GetData<String>("Player:GarbageRoute"));
                                player.SetData<int>("Player:GarbageStation", fromplayer.GetData<int>("Player:GarbageStation"));
                                fromplayer.SetData<Player>("Player:GarbageGetPlayer", player);
                                player.SetData<Player>("Player:GarbageGetPlayer", fromplayer);
                                player.SetData<bool>("Player:GarbagePlayer2", true);
                                Helper.GetNextGarbageStation(fromplayer, fromplayer.GetData<String>("Player:GarbageRoute"), 0);
                                player.SetData<int>("Player:GarbageTime", Helper.UnixTimestamp() + (20));
                                player.SetData<Player>("Player:GroupInvitePlayer", null);
                            }
                            break;
                        }
                    case "factioninvite":
                        {
                            if (player.GetData<int>("Player:GroupInvite") <= 0)
                            {
                                SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-end");
                                player.SetData<Player>("Player:GroupInvitePlayer", null);
                                return;
                            }
                            Player fromplayer = player.GetData<Player>("Player:GroupInvitePlayer");
                            if (number == 2)
                            {
                                SendNotificationWithoutButton(player, "Fraktionseinladung abgelehnt!", "success", "top-end");

                                Helper.CreateFactionLog(player.GetData<int>("Player:GroupInvite"), $"{character.name} hat hat die Fraktionseinladung abgelehnt!");

                                if (fromplayer != null)
                                {
                                    SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Fraktionseinladung abgelehnt!", "error", "top-left", 3500);
                                }
                            }
                            else
                            {
                                if (tempData.order != null)
                                {
                                    SendNotificationWithoutButton(player, "Du kannst jetzt keine Fraktionseinladung annehmen!", "error", "top-end");
                                    return;
                                }

                                SendNotificationWithoutButton(player, "Fraktionseinladung angenommen, weitere Infos findest du im F2 Menü!", "success", "top-end");
                                character.faction = player.GetData<int>("Player:GroupInvite");
                                character.rang = 1;
                                character.swat = 0;
                                character.faction_dutytime = 0;
                                character.dutyjson = "{\"clothing\":[-1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"clothingColor\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]}";
                                character.faction_since = Helper.UnixTimestamp();

                                if (character.faction == 1)
                                {
                                    character.armor = 59;
                                    if (NAPI.Player.GetPlayerArmor(player) > 0)
                                    {
                                        NAPI.Player.SetPlayerClothes(player, 9, character.armor, 0);
                                    }
                                }

                                CharacterController.SaveCharacter(player);

                                FactionsModel faction = FactionController.GetFactionById(character.faction);
                                if (faction != null)
                                {
                                    faction.members++;
                                }

                                Helper.CreateFactionLog(player.GetData<int>("Player:GroupInvite"), $"{character.name} hat hat die Fraktionseinladung angenommen!");

                                player.SetOwnSharedData("Player:Faction", character.faction);
                                player.SetOwnSharedData("Player:FactionRang", 1);

                                Helper.CreateUserTimeline(account.id, character.id, $"Fraktion {faction.name} beigetreten", 4);

                                if (fromplayer != null)
                                {
                                    SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Fraktionseinladung angenommen!", "success", "top-left", 3500);
                                    FactionController.UpdateFactionStats(fromplayer);
                                }
                            }
                            player.SetData<Player>("Player:GroupInvitePlayer", null);
                            player.SetData<int>("Player:GroupInvite", 0);
                            player.TriggerEvent("Client:HideMenus");
                            break;
                        }
                    case "groupinvite":
                        {
                            if (player.GetData<int>("Player:GroupInvite") <= 0)
                            {
                                SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-end");
                                return;
                            }
                            Player fromplayer = player.GetData<Player>("Player:GroupInvitePlayer");
                            if (number == 2)
                            {
                                SendNotificationWithoutButton(player, "Gruppierungseinladung abgelehnt!", "success", "top-end");

                                Helper.CreateGroupLog(player.GetData<int>("Player:GroupInvite"), $"{character.name} hat hat die Gruppierungseinladung abgelehnt!");

                                if (fromplayer != null)
                                {
                                    SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Gruppierungseinladung abgelehnt!", "error", "top-end");
                                }
                            }
                            else
                            {
                                if (tempData.order != null)
                                {
                                    SendNotificationWithoutButton(player, "Du kannst jetzt keine Gruppierungseinladung annehmen!", "error", "top-end");
                                    return;
                                }

                                SendNotificationWithoutButton(player, "Gruppierungseinladung angenommen, weitere Infos findest du im F2 Menü!", "success", "top-end");
                                character.mygroup = player.GetData<int>("Player:GroupInvite");

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "INSERT INTO groups_members (groupsid, charid, rang, duty_time, payday, payday_day, since) VALUES (@groupsid, @charid, @rang, @duty_time, @payday, @payday_day, @since)";
                                command.Parameters.AddWithValue("@groupsid", player.GetData<int>("Player:GroupInvite"));
                                command.Parameters.AddWithValue("@charid", character.id);
                                command.Parameters.AddWithValue("@rang", 1);
                                command.Parameters.AddWithValue("@duty_time", 0);
                                command.Parameters.AddWithValue("@payday", 0);
                                command.Parameters.AddWithValue("@payday_day", 0);
                                command.Parameters.AddWithValue("@since", Helper.UnixTimestamp());

                                command.ExecuteNonQuery();

                                Helper.CreateGroupLog(player.GetData<int>("Player:GroupInvite"), $"{character.name} hat hat die Gruppierungseinladung angenommen!");

                                player.SetOwnSharedData("Player:Group", player.GetData<int>("Player:GroupInvite"));

                                Groups newGroup = GroupsController.GetGroupById(character.mygroup);
                                if (newGroup != null)
                                {
                                    newGroup.members++;
                                    GroupsController.SaveGroup(newGroup);
                                }

                                Helper.CreateUserTimeline(account.id, character.id, $"Gruppierung {newGroup.name} beigetreten", 4);

                                if (fromplayer != null)
                                {
                                    SendNotificationWithoutButton(fromplayer, $"{character.name} hat deine Gruppierungseinladung angenommen!", "success", "top-end");
                                    GroupsController.UpdateGroupStats(fromplayer);
                                }

                                player.SetOwnSharedData("Player:GroupRang", 1);
                                CharacterController.SaveCharacter(player);
                            }
                            player.SetData<Player>("Player:GroupInvitePlayer", null);
                            player.SetData<int>("Player:GroupInvite", 0);
                            player.TriggerEvent("Client:HideMenus");
                            break;
                        }
                    case "shootingrange":
                        {
                            if (number == 1)
                            {
                                player.SetData<int>("Player:AmmuQuiz", 4);
                                player.TriggerEvent("Client:StartRange", 50, 0, 0);
                                SetPlayerPosition(player, new Vector3(8.953279, -1097.588, 29.797028));
                                player.Rotation = new Vector3(0.0, 0.0, -110.08769);
                                tempData.jobColshape = Events.ammuCol;
                                SendNotificationWithoutButton(player, "Versuche die Ziele (Mittlerer roter Punkt) so schnell wie möglich zu treffen!", "success", "top-end", 3750);
                            }
                            break;
                        }
                    case "buywaffenschein":
                        {
                            if (number == 1)
                            {
                                if (GetAge(DateTime.ParseExact(character.birth, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)) < 18)
                                {
                                    SendNotificationWithoutButton(player, $"Du bist zu jung für einen Waffenschein!", "error", "top-end");
                                    return;
                                }
                                foreach (Items iteminlist in tempData.itemlist.ToList())
                                {
                                    if (iteminlist != null && iteminlist.type == 5 && !iteminlist.description.ToLower().Contains("schutzweste"))
                                    {
                                        string[] weaponArray = new string[7];
                                        weaponArray = iteminlist.props.Split(",");
                                        if (weaponArray[1] == "1")
                                        {
                                            SendNotificationWithoutButton(player, $"Du musst zuerst alle Waffen weglegen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                }
                                if (character.cash < 12500)
                                {
                                    SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 12500$!", "error", "top-end");
                                    return;
                                }
                                Items item = null;
                                item = ItemsController.GetItemByItemName(player, "F-Zeugnis");
                                if (item != null && item.props == character.name)
                                {
                                    if (ShowFührungsZeugnis(player, character.name, false) >= 3)
                                    {
                                        SendNotificationWithoutButton(player, $"Dein Führungszeugnis lässt leider keine Waffenscheinprüfung zu!", "error", "top-end", 3500);
                                        return;
                                    }
                                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                    CharacterController.SetMoney(player, -12500);
                                    Helper.SetGovMoney(6500, "Waffenscheinprüfung");
                                    Business bizz = Business.GetBusinessById(19);
                                    if (bizz != null)
                                    {
                                        Business.ManageBizzCash(bizz, 3500, true);
                                    }
                                    ItemsController.RemoveItemById(player, item.itemid);
                                    player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                                    player.TriggerEvent("Client:ShowHud");
                                    player.TriggerEvent("Client:ShowAmmuQuiz");
                                    player.SetData<int>("Player:AmmuQuiz", 1);
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, $"Du musst dir zuerst ein Führungszeugnis im Los Santos Police-Department beantragen!", "error", "top-end");
                                }
                            }
                            break;
                        }
                    case "sellvehicle":
                        {
                            number = Convert.ToInt32(input);
                            if (number == 1)
                            {
                                int price = player.GetData<int>("Player:VehiclePrice");
                                foreach (Cars car in Cars.carList)
                                {
                                    if (car.vehicleHandle != null && car.vehicleHandle == player.Vehicle)
                                    {
                                        CharacterController.SetMoney(player, price);
                                        player.WarpOutOfVehicle();
                                        player.ResetData("Player:VehiclePrice");
                                        NAPI.Task.Run(() =>
                                        {
                                            Cars.carList.Remove(car);
                                            if (car.vehicleData.owner.Contains("group"))
                                            {
                                                Helper.CreateGroupLog(character.mygroup, $"{character.name} hat das Fahrzeug {car.vehicleData.vehiclename}[{car.vehicleData.id}] für {price}$ verkauft!");
                                            }
                                            ItemsController.RemoveItemByProp(player, car.vehicleData.vehiclename + ": " + car.vehicleData.id);
                                            PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                            db.Delete(car.vehicleData);
                                            car.vehicleData = null;
                                            car.vehicleHandle.Delete();
                                            car.vehicleHandle = null;
                                            SendNotificationWithoutButton(player, $"Du hast das Fahrzeug erfolgreich für {price}$ verkauft!", "success", "top-end", 3750);
                                            Business bizz = Business.GetClosestBusiness(player, 40.5f);
                                            if (bizz != null)
                                            {
                                                bizz.products += (price / 30);
                                                if (bizz.products > 2000)
                                                {
                                                    bizz.products = 2000;
                                                }
                                            }
                                            player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                        }, delayTime: 95);
                                    }
                                }
                            }
                            break;
                        }
                    case "rentfaggio":
                        {
                            number = Convert.ToInt32(input);
                            if (number == 2)
                            {
                                OnRollerVerleih(player, false);
                            }
                            else
                            {
                                if (account.faqarray[1] == "0")
                                {
                                    account.faqarray[1] = "1";
                                }
                                OnRollerVerleih(player, true);
                            }
                            break;
                        }
                    case "garbagebag":
                        {
                            number = Convert.ToInt32(input);
                            if (number == 1)
                            {
                                player.SetData<bool>("Player:GarbageBag", true);
                                Helper.SendNotificationWithoutButton(player, "Danke, du siehst ja hier liegt überall Müll rum, hier der Müllbeutel", "success", "top-left", 4150);
                                Helper.AddRemoveAttachments(player, "garbageBag", true);
                                player.TriggerEvent("Client:StartBeachGarbage");
                            }
                            break;
                        }
                    case "sellfish":
                        {
                            number = Convert.ToInt32(input);
                            if (number == 1 && tempData.tempValue > 0)
                            {
                                player.TriggerEvent("Client:PressedEscape");
                                CharacterController.SetMoney(player, tempData.tempValue);
                                SendNotificationWithoutButton(player, $"Otto: Du hast mir Fische im Wert von {tempData.tempValue} verkauft!", "success", "top-end", 4500);
                                tempData.tempValue = 0;
                                foreach (Items item in tempData.itemlist.ToList())
                                {
                                    if (item.description == "Dorsch" || item.description == "Makrele" || item.description == "Forelle" || item.description == "Wildkarpfen" || item.description == "Teufelskärpfling")
                                    {
                                        ItemsController.RemoveItem(player, item.itemid);
                                    }
                                }
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                            }
                            else
                            {
                                Helper.ShowPreShop(player, "Angelmenü", 0, 1, 1);
                            }
                            break;
                        }
                    case "sellachmed":
                        {
                            number = Convert.ToInt32(input);
                            if (number == 1)
                            {
                                Vehicle vehicle = player.Vehicle;
                                if (vehicle != null)
                                {
                                    player.WarpOutOfVehicle();
                                    NAPI.Task.Run(() =>
                                    {
                                        vehicle.Health = 175.0f;
                                        Helper.SetVehicleEngine(vehicle, false);
                                        vehicle.SetSharedData("Vehicle:Sync", "0,0,0,1,1,0,0");
                                        vehicle.SetSharedData("Vehicle:Doors", "[true,true,true,true,true,true]");
                                        vehicle.Locked = true;
                                        vehicle.SetSharedData("Vehicle:Fuel", 0);
                                        vehicle.SetSharedData("Vehicle:Oel", 0);
                                        vehicle.SetSharedData("Vehicle:Battery", 0);
                                        CharacterController.SetMoney(player, player.GetData<int>("Player:AchmedPrice"));
                                        SendNotificationWithoutButton(player, "Achmed: Hier dein Geld, wenn du was neues hast komm mal wieder vorbei!", "success", "top-end", 3500);
                                        player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                    }, delayTime: 215);
                                }
                            }
                            player.ResetData("Player:AchmedPrice");
                            break;
                        }
                    case "vehiclesettings":
                        {
                            number = Convert.ToInt32(input);
                            Helper.OnVehicleSettings(player, "show", "" + number);
                            break;
                        }
                    case "renthouse":
                        {
                            number = Convert.ToInt32(input);
                            house = House.GetClosestHouse(player);
                            if (house == null) return;
                            if (number == 1)
                            {
                                if (house.owner == character.name)
                                {
                                    SendNotificationWithoutButton(player, "Du bist der Besitzer dieses Hauses!", "error", "top-end");
                                    return;
                                }
                                Items newitem = ItemsController.CreateNewItem(player, character.id, "Mietschlüssel", "Player", 1, ItemsController.GetFreeItemID(player));
                                if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                {
                                    newitem = null;
                                    SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für den Mietschlüssel!", "error", "top-end");
                                    return;
                                }
                                if (newitem != null)
                                {
                                    newitem.props = "Miethausnummer: " + house.id;
                                    tempData.itemlist.Add(newitem);
                                    house.tenants++;
                                    House.SaveHouse(house);
                                    CharacterController.SaveCharacter(player);
                                    SendNotificationWithoutButton(player, "Du hast dich eingemietet, im F2 Menü kannst du dir alle notwendigen Informationen einsehen!", "success", "top-end", 5500);
                                    player.TriggerEvent("Client:PlaySoundSuccessNormal");
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Du konntest dich in das Haus nicht einmieten!", "error", "top-end");
                                }
                            }
                            else
                            {
                                Helper.PlayShortAnimation(player, "timetable@jimmy@doorknock@", "knockdoor_idle", 1550);
                                SendNotificationWithoutButton(player, "Du klopfst an der Türe!", "success");
                                Helper.SendHouseMessage(house.id, "!{#EE82EE}* Jemand klopft an der Tür!");
                            }
                            break;
                        }
                    case "buybizz":
                        {
                            number = Convert.ToInt32(input);
                            Business bizz = Business.GetClosestBusiness(player);
                            if (bizz != null)
                            {
                                if (number == 1)
                                {
                                    if (character.cash < bizz.price)
                                    {
                                        SendNotificationWithoutButton(player, "Du hast nicht genügend Geld dabei!", "error", "top-end");
                                        return;
                                    }
                                    if (bizz.owner != "n/A")
                                    {
                                        SendNotificationWithoutButton(player, "Dieses Business wurde bereits verkauft!", "error", "top-end");
                                        return;
                                    }
                                    int count = 0;
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "SELECT COUNT(*) as count FROM business where owner=@owner";
                                    command.Parameters.AddWithValue("@owner", character.name);

                                    if (account.premium == 0)
                                    {
                                        if (count >= 1)
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst keine weiteren Businesse mehr kaufen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    else if (account.premium == 3)
                                    {
                                        if (count >= 2)
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst keine weiteren Businesse mehr kaufen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Bizzschlüssel", "Player", 1, ItemsController.GetFreeItemID(player));
                                    if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                    {
                                        newitem = null;
                                        SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für den Bizzschlüssel!", "error", "top-end");
                                        return;
                                    }
                                    if (newitem != null)
                                    {
                                        newitem.props = "Bizzschlüssel: " + bizz.id;
                                        tempData.itemlist.Add(newitem);
                                        CharacterController.SetMoney(player, -bizz.price);
                                        bizz.owner = character.name;
                                        if (Helper.GetRandomPercentage(50))
                                        {
                                            bizz.elec = 50;
                                        }
                                        else
                                        {
                                            bizz.elec = 51;
                                        }
                                        Business.SetBusinessHandle(bizz);
                                        Business.SaveBusiness(bizz);
                                        CharacterController.SaveCharacter(player);
                                        SendNotificationWithoutButton(player, "Du hast dir das Business erworben, im F2 Menü kannst du dir alle notwendigen Informationen einsehen!", "success", "top-end", 5500);
                                        player.TriggerEvent("Client:PlaySoundSuccessNormal");
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, "Das Business konnte nicht erworben werden!", "error", "top-end");
                                    }
                                }
                            }
                            break;
                        }
                    case "buyhouse":
                        {
                            number = Convert.ToInt32(input);
                            house = House.GetClosestHouse(player);
                            if (house != null)
                            {
                                if (number == 1)
                                {
                                    if (character.cash < house.price)
                                    {
                                        SendNotificationWithoutButton(player, "Du hast nicht genügend Geld dabei!", "error", "top-end");
                                        return;
                                    }
                                    if (house.owner != "n/A")
                                    {
                                        SendNotificationWithoutButton(player, "Dieses Haus wurde bereits verkauft!", "error", "top-end");
                                        return;
                                    }
                                    int count = 0;
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "SELECT COUNT(*) as count FROM houses where owner=@owner";
                                    command.Parameters.AddWithValue("@owner", character.name);

                                    if (account.premium == 0)
                                    {
                                        if ((count - account.houseslots) >= 2)
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst keine weiteren Häuser mehr kaufen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    else if (account.premium == 1)
                                    {
                                        if ((count - account.houseslots) >= 3)
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst keine weiteren Häuser mehr kaufen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    else if (account.premium == 2)
                                    {
                                        if ((count - account.houseslots) >= 4)
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst keine weiteren Häuser mehr kaufen!", "error", "top-end");
                                            return;
                                        }
                                    }
                                    else if (account.premium == 3)
                                    {
                                        if ((count - account.houseslots) >= 5)
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst keine weiteren Häuser mehr kaufen!", "error", "top-end");
                                            return;
                                        }
                                    }

                                    using (MySqlDataReader reader = command.ExecuteReader())
                                    {
                                        if (reader.HasRows)
                                        {
                                            reader.Read();
                                            count = reader.GetInt32("count");
                                        }
                                        reader.Close();
                                    }

                                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Hausschlüssel", "Player", 1, ItemsController.GetFreeItemID(player));
                                    if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                                    {
                                        newitem = null;
                                        SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für den Hausschlüssel!", "error", "top-end");
                                        return;
                                    }
                                    if (newitem != null)
                                    {
                                        newitem.props = "Hausnummer: " + house.id;
                                        tempData.itemlist.Add(newitem);
                                        CharacterController.SetMoney(player, -house.price);
                                        house.status = 1;
                                        house.owner = character.name;
                                        house.locked = 0;
                                        house.tenants = 0;
                                        if (Helper.GetRandomPercentage(50))
                                        {
                                            house.elec = 50;
                                        }
                                        else
                                        {
                                            house.elec = 51;
                                        }
                                        House.SetHouseHandle(house);
                                        House.SaveHouse(house);
                                        CharacterController.SaveCharacter(player);
                                        SendNotificationWithoutButton(player, "Du hast dir das Haus erworben, im F2 Menü kannst du dir alle notwendigen Informationen einsehen!", "success", "top-end", 5500);
                                        player.TriggerEvent("Client:PlaySoundSuccessNormal");
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, "Das Haus konnte nicht erworben werden!", "error", "top-end");
                                    }
                                }
                                else
                                {
                                    player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(house.interior));
                                    SetPlayerPosition(player, House.GetHouseExitPoint(house.interior));
                                    player.Dimension = Convert.ToUInt32(house.id);
                                    character.inhouse = house.id;
                                    Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                                    player.SetOwnSharedData("Player:InHouse", house.id);
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Haus!", "error", "top-end");
                            }
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnInput2]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnInput")]
        public static void OnOnInput(Player player, string input, string flag, bool confirmed = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                Account account = Helper.GetAccountData(player);
                if (character == null || tempData == null || account == null) return;
                if (flag.ToLower() != "newpin")
                {
                    player.TriggerEvent("Client:PlayerFreeze", false);
                }
                switch (flag.ToLower())
                {
                    case "reportplayer":
                        {
                            if (confirmed == true)
                            {
                                Player getPlayer = Helper.GetPlayerByNameOrID(Convert.ToString(player.GetData<int>("Player:Report")));
                                if (getPlayer == null)
                                {
                                    SendNotificationWithoutButton(player, "Ungültiger Spieler!", "success", "top-end", 4250);
                                    return;
                                }
                                Account account2 = Helper.GetAccountData(getPlayer);
                                if (account2 == null)
                                {
                                    SendNotificationWithoutButton(player, "Ungültiger Spieler!", "success", "top-end", 4250);
                                    return;
                                }
                                string message = $"{account.name}[{player.Id}] hat {account2.name}[{getPlayer.Id}] gemeldet, Grund: {input}!";
                                int timestamp = Helper.UnixTimestamp();
                                Reports report = new Reports();
                                report.text = message;
                                report.timestamp = Helper.UnixTimeStampToDateTime(timestamp).ToString();
                                reportList.Insert(0, report);
                                if (reportList.Count > 25)
                                {
                                    int count = reportList.Count - 25;
                                    reportList.RemoveRange(reportList.Count - count, reportList.Count);
                                }
                                SendAdminMessage2(message, 1, true);
                                SendNotificationWithoutButton(player, "Du hast den Spieler gemeldet, die Administration wurde benachrichtigt!", "success", "top-end", 4250);
                                player.ResetData("Player:Report");
                            }
                            else
                            {
                                player.ResetData("Player:Report");
                            }
                            break;
                        }
                    case "sellproducts1":
                        {
                            if (confirmed == true)
                            {
                                if (IsASpediteur(player) != 1)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Spediteur!", "error", "top-end");
                                    return;
                                }
                                if (character.mygroup == -1)
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst eine Gruppierung auswählen!", "error", "top-end");
                                    return;
                                }
                                Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                Bank bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                if (bank != null)
                                {
                                    if (!IsASpeditionsVehicle(player.Vehicle))
                                    {
                                        SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte verkaufen!", "error");
                                        return;
                                    }
                                    int number = Convert.ToInt32(input);
                                    Business bizz = Business.GetClosestBusiness(player, 3.75f);
                                    SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                    if (bizz != null)
                                    {
                                        if (number <= 0 || number > player.Vehicle.GetData<int>("Vehicle:Products") || number + bizz.products > 2000)
                                        {
                                            SendNotificationWithoutButton(player, "Ungültige Produktmenge!", "error", "top-end");
                                            return;
                                        }
                                        int price = number * bizz.prodprice;
                                        if (bizz.cash < price)
                                        {
                                            SendNotificationWithoutButton(player, "Das Business hat nicht genügend Geld um dir die Produkte abzukaufen!", "error", "top-end");
                                            return;
                                        }
                                        character.sellprods += number;
                                        if (character.sellprods >= 1200)
                                        {
                                            character.sellprods -= 1200;
                                            if (character.truckerskill < 225)
                                            {
                                                character.truckerskill++;
                                            }
                                        }
                                        player.Vehicle.SetData<int>("Vehicle:Products", player.Vehicle.GetData<int>("Vehicle:Products") - number);
                                        bizz.products += number;
                                        bizz.cash -= price;
                                        Business.SaveBusiness(bizz);
                                        int bonus = 0;
                                        bonus = (int)((character.truckerskill / 45) * 0.04);
                                        int payday = (int)(number * bizz.prodprice);
                                        bank.bankvalue += payday + bonus;
                                        if (mygroup.provision <= 0)
                                        {
                                            SendNotificationWithoutButton(player, $"Du hast {number} Produkte verkauft und erhältst {payday + bonus}$ auf das Konto deiner Firma!", "success", "top-end", 4500);
                                            Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat {number} Produkte verkauft und {payday + bonus}$ erwirtschaftet!");
                                        }
                                        else
                                        {
                                            int prov = payday / 100 * mygroup.provision;
                                            SendNotificationWithoutButton(player, $"Du hast {number} Produkte verkauft und erhältst {payday + bonus}$ auf das Konto deiner Firma. Du erhälst {prov}$ Provision!", "success", "top-end", 4500);
                                            Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat {number} Produkte verkauft und {(payday + bonus) - prov}$ erwirtschaftet!");
                                            Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                            if (bank2 != null)
                                            {
                                                bank2.bankvalue += prov;
                                                bank.bankvalue -= prov;
                                            }
                                        }
                                        bank.bankvalue += payday + bonus;
                                    }
                                    else
                                    {
                                        House house = House.GetClosestHouse(player, 3.75f);
                                        if (house != null)
                                        {
                                            Groups houseGroup = GroupsController.GetGroupById(house.housegroup);
                                            Bank bank3 = BankController.GetBankByBankNumber(houseGroup.banknumber);
                                            if (bank3 == null || houseGroup == null)
                                            {
                                                SendNotificationWithoutButton(player, "Kein Gruppierungskonto gefunden!", "error", "top-end", 4500);
                                                return;
                                            }
                                            if (number <= 0 || number > player.Vehicle.GetData<int>("Vehicle:Products") || number + house.stock > 3500)
                                            {
                                                SendNotificationWithoutButton(player, "Ungültige Produktmenge!", "error", "top-end");
                                                return;
                                            }
                                            int price = number * house.stockprice;
                                            if (bank3.bankvalue < price)
                                            {
                                                SendNotificationWithoutButton(player, "Diese Gruppierung hat nicht genügend Geld um dir die Produkte abzukaufen!", "error", "top-end");
                                                return;
                                            }
                                            character.sellprods += number;
                                            if (character.sellprods >= 1200)
                                            {
                                                character.sellprods -= 1200;
                                                if (character.truckerskill < 225)
                                                {
                                                    character.truckerskill++;
                                                }
                                            }
                                            player.Vehicle.SetData<int>("Vehicle:Products", player.Vehicle.GetData<int>("Vehicle:Products") - number);
                                            house.stock += number;
                                            bank3.bankvalue -= price;
                                            Helper.BankSettings(bank3.banknumber, $"{number} Produkte erworben", price.ToString(), "Haus: " + house.id);
                                            Helper.CreateGroupMoneyLog(houseGroup.id, $"{number} Produkte für {price}$ erworben, Haus: {house.id}!");
                                            House.SaveHouse(house);
                                            int bonus = 0;
                                            bonus = (int)((character.truckerskill / 45) * 0.04);
                                            int payday = (int)(number * house.stockprice);
                                            bank.bankvalue += payday + bonus;
                                            if (mygroup.provision <= 0)
                                            {
                                                SendNotificationWithoutButton(player, $"Du hast {number} Produkte verkauft und erhältst {payday + bonus}$ auf das Konto deiner Firma!", "success", "top-end", 4500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat {number} Produkte verkauft und {payday + bonus}$ erwirtschaftet!");
                                            }
                                            else
                                            {
                                                int prov = payday / 100 * mygroup.provision;
                                                SendNotificationWithoutButton(player, $"Du hast {number} Produkte verkauft und erhältst {payday + bonus}$ auf das Konto deiner Firma. Du erhälst {prov}$ Provision!", "success", "top-end", 4500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat {number} Produkte verkauft und {(payday + bonus) - prov}$ erwirtschaftet!");
                                                Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                                if (bank2 != null)
                                                {
                                                    bank2.bankvalue += prov;
                                                    bank.bankvalue -= prov;
                                                }
                                            }
                                            bank.bankvalue += payday + bonus;
                                        }
                                    }
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Kein Firmenkonto gefunden!", "error", "top-end", 4500);
                                }
                            }
                            break;
                        }
                    case "sellproducts2":
                        {
                            if (confirmed == true)
                            {
                                if (IsASpediteur(player) != 2)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Spediteur!", "error", "top-end");
                                    return;
                                }
                                if (!IsASpeditionsVehicle(player.Vehicle))
                                {
                                    SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte verkaufen!", "error");
                                    return;
                                }
                                int number = Convert.ToInt32(input);
                                Business bizz = Business.GetClosestBusiness(player, 3.75f);
                                SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                if (bizz != null)
                                {
                                    if (number <= 0 || number > player.Vehicle.GetData<int>("Vehicle:Products") || number + bizz.products > 2000)
                                    {
                                        SendNotificationWithoutButton(player, "Ungültige Produktmenge!", "error", "top-end");
                                        return;
                                    }
                                    int price = number * bizz.prodprice;
                                    if (bizz.cash < price)
                                    {
                                        SendNotificationWithoutButton(player, "Das Business hat nicht genügend Geld um dir die Produkte abzukaufen!", "error", "top-end");
                                        return;
                                    }
                                    character.sellprods += number;
                                    if (character.sellprods >= 1200)
                                    {
                                        character.sellprods -= 1200;
                                        if (character.truckerskill < 225)
                                        {
                                            character.truckerskill++;
                                        }
                                    }
                                    player.Vehicle.SetData<int>("Vehicle:Products", player.Vehicle.GetData<int>("Vehicle:Products") - number);
                                    bizz.products += number;
                                    bizz.cash -= price;
                                    Business.SaveBusiness(bizz);
                                    int bonus = 0;
                                    bonus = (int)((character.truckerskill / 45) * 0.04);
                                    int payday = (int)(number * 2.1);
                                    character.nextpayday = payday;
                                    CharacterController.SaveCharacter(player);
                                    SendNotificationWithoutButton(player, $"Du hast {number} Produkte verkauft und erhältst {payday}$ auf deinen nächsten Gehaltscheck gutgeschrieben!", "success", "top-end", 4500);
                                }
                                else
                                {
                                    House house = House.GetClosestHouse(player, 3.75f);
                                    if (house != null)
                                    {
                                        Groups houseGroup = GroupsController.GetGroupById(house.housegroup);
                                        Bank bank3 = null;
                                        if (house.id == 2)
                                        {
                                            BankController.GetBankByBankNumber("SA3701-100000");
                                        }
                                        else
                                        {
                                            BankController.GetBankByBankNumber(houseGroup.banknumber);
                                        }
                                        if (bank3 == null)
                                        {
                                            SendNotificationWithoutButton(player, "Kein Gruppierungskonto/Fraktionskonto gefunden!", "error", "top-end", 4500);
                                            return;
                                        }
                                        if (number <= 0 || number > player.Vehicle.GetData<int>("Vehicle:Products") || number + house.stock > 2000)
                                        {
                                            SendNotificationWithoutButton(player, "Ungültige Produktmenge!", "error", "top-end");
                                            return;
                                        }
                                        int price = number * house.stockprice;
                                        if (bank3.bankvalue < price)
                                        {
                                            SendNotificationWithoutButton(player, "Die Gruppierung/Fraktion hat nicht genügend Geld um dir die Produkte abzukaufen!", "error", "top-end");
                                            return;
                                        }
                                        character.sellprods += number;
                                        if (character.sellprods >= 1200)
                                        {
                                            character.sellprods -= 1200;
                                            if (character.truckerskill < 225)
                                            {
                                                character.truckerskill++;
                                            }
                                        }
                                        player.Vehicle.SetData<int>("Vehicle:Products", player.Vehicle.GetData<int>("Vehicle:Products") - number);
                                        house.stock += number;
                                        bank3.bankvalue -= price;
                                        Helper.BankSettings(bank3.banknumber, $"{number} Produkte erworben", price.ToString(), "Haus: " + house.id);
                                        if (house.id == 2)
                                        {
                                            Helper.CreateFactionLog(houseGroup.id, $"{number} Produkte für {price}$ erworben, Haus: {house.id}!");
                                        }
                                        else
                                        {
                                            Helper.CreateGroupMoneyLog(houseGroup.id, $"{number} Produkte für {price}$ erworben, Haus: {house.id}!");
                                        }
                                        House.SaveHouse(house);
                                        int bonus = 0;
                                        bonus = (int)((character.truckerskill / 45) * 0.04);
                                        int payday = (int)(number * 2.1);
                                        character.nextpayday = payday;
                                        CharacterController.SaveCharacter(player);
                                        SendNotificationWithoutButton(player, $"Du hast {number} Produkte verkauft und erhältst {payday}$ auf deinen nächsten Gehaltscheck gutgeschrieben!", "success", "top-end", 4500);
                                    }
                                }
                            }
                            break;
                        }
                    case "servicefirma":
                        {
                            if (confirmed == true)
                            {
                                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                                Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                if (mygroup != null)
                                {
                                    CharacterController.SetMoney(player, -Convert.ToInt32(Helper.adminSettings.grouparray[8]));
                                    Helper.SetGovMoney(Convert.ToInt32(Helper.adminSettings.grouparray[8]), "Firmenservice");
                                    mygroup.service = 1;
                                    Helper.SendNotificationWithoutButton2(player, "Firma erfolgreich als Service eingetragen!", "success", "center", 3500);
                                    Services service = new Services();
                                    service.id = GroupsController.servicesList.Count + 1;
                                    service.groupid = mygroup.id;
                                    service.text = input;
                                    db.Insert(service);
                                    GroupsController.servicesList.Add(service);
                                }
                                else
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                            }
                            break;
                        }
                    case "setdienstpreis":
                        {
                            if (confirmed == true)
                            {
                                int number = Convert.ToInt32(input);
                                if (number == 0) return;
                                if (number <= 0 || number > 9999)
                                {
                                    SendNotificationWithoutButton(player, $"Ungültiger Dienstpreis!", "success", "top-end");
                                    return;
                                }
                                SendNotificationWithoutButton(player, $"Dienstfahrt begonnen und Dienstpreis auf {number}$ pro Kilometer gesetzt!", "success", "top-end");
                                if (Helper.IsATaxiDriver(player) == 2)
                                {
                                    if (player.HasData("Player:TaxameterOn"))
                                    {
                                        player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: {number}$ | ~y~Taxameter: 0$");
                                    }
                                    else
                                    {
                                        player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: {number}$");
                                    }
                                }
                                else
                                {
                                    Helper.CreateGroupLog(character.mygroup, $"{character.name} hat seine Dienstfahrt begonnen, zu einem Dienstpreis von {number}$!");
                                    if (player.HasData("Player:TaxameterOn"))
                                    {
                                        player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: {number}$ | ~y~Taxameter: 0$");
                                    }
                                    else
                                    {
                                        player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: {number}$");
                                    }
                                }
                                player.SetData<int>("Player:Taxameter", 0);
                                player.SetData<int>("Player:Fare", number);
                                player.TriggerEvent("Client:UpdateKilometreTaxi", 1);
                            }
                            break;
                        }
                    case "buyproducts1":
                        {
                            if (confirmed == true)
                            {
                                if (IsASpediteur(player) != 1)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Spediteur!", "error", "top-end");
                                    return;
                                }
                                if (character.mygroup == -1)
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst eine Gruppierung auswählen!", "error", "top-end");
                                    return;
                                }
                                if (!IsASpeditionsVehicle(player.Vehicle))
                                {
                                    SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                    return;
                                }
                                int number = Convert.ToInt32(input);
                                if (number == 0) return;
                                int capanow = player.Vehicle.GetData<int>("Vehicle:Products");
                                SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                if (number <= 0 || number > spedVehicles.capa || (number + capanow) > spedVehicles.capa)
                                {
                                    SendNotificationWithoutButton(player, "Ungültige Produktmenge!", "error", "top-end");
                                    return;
                                }
                                int money = 18 * number;
                                Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                Bank bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                if (bank != null)
                                {
                                    if (bank.bankvalue < money)
                                    {
                                        SendNotificationWithoutButton(player, "Auf dem Firmenkonto ist nicht genügend Geld vorhanden!", "error", "top-end");
                                        return;
                                    }
                                    bank.bankvalue -= money;
                                    BankController.SaveBank(bank);
                                    Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat {number} Produkte im Wert von {money}$ erworben!");
                                    player.Vehicle.SetData<int>("Vehicle:Products", (capanow + number));
                                    SendNotificationWithoutButton(player, $"Du hast {number} Produkte für {money} erworben, diese kannst du jetzt bei einem Business/einer Gruppierung verkaufen - (Taste: F3)!", "success", "top-end", 3250);
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Kein Firmenkonto vorhanden!", "error", "top-end");
                                }
                            }
                            break;
                        }
                    case "buyproducts2":
                        {
                            if (confirmed == true)
                            {
                                if (IsASpediteur(player) != 2)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Spediteur!", "error", "top-end");
                                    return;
                                }
                                if (tempData.jobVehicle == null || player.Vehicle != tempData.jobVehicle)
                                {
                                    SendNotificationWithoutButton(player, "Du sitzt in keinem Jobfahrzeug!", "error", "top-end");
                                    return;
                                }
                                if (!IsASpeditionsVehicle(player.Vehicle))
                                {
                                    SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                    return;
                                }
                                int number = Convert.ToInt32(input);
                                if (number == 0) return;
                                int capanow = player.Vehicle.GetData<int>("Vehicle:Products");
                                SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                if (number <= 0 || number > spedVehicles.capa || (number + capanow) > spedVehicles.capa)
                                {
                                    SendNotificationWithoutButton(player, "Ungültige Produktmenge!", "error", "top-end");
                                    return;
                                }
                                player.Vehicle.SetData<int>("Vehicle:Products", (capanow + number));
                                SendNotificationWithoutButton(player, $"Du hast {number} Produkte eingeladen, diese kannst du jetzt bei einem Business/einer Gruppierung verkaufen - (Taste: F3)!", "success", "top-end", 3250);
                            }
                            break;
                        }
                    case "bizzinsert":
                        {
                            if (confirmed == true)
                            {
                                int number = Convert.ToInt32(input);
                                if (number > character.cash)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Soviel Geld hast du nicht dabei!", "error", "center");
                                    player.TriggerEvent("Client:ShowCursor");
                                    return;
                                }
                                Business bizz = Business.GetClosestBusiness(player);
                                if (bizz != null)
                                {
                                    CharacterController.SetMoney(player, -number);
                                    bizz.cash += number;
                                    Helper.SendNotificationWithoutButton2(player, $"Du hast {number}$ in die Kasse gelegt!", "success", "center");
                                    Business.SaveBusiness(bizz);
                                    CharacterController.SaveCharacter(player);
                                    Business.OnStartBizz(player, true);
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "bizzcashout":
                        {
                            if (confirmed == true)
                            {
                                int number = Convert.ToInt32(input);
                                Business bizz = Business.GetClosestBusiness(player);
                                if (bizz != null)
                                {
                                    if (number > bizz.cash)
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Soviel Geld liegt nicht in der Kasse!", "error", "center");
                                        player.TriggerEvent("Client:ShowCursor");
                                        return;
                                    }
                                    CharacterController.SetMoney(player, number);
                                    bizz.cash -= number;
                                    Helper.SendNotificationWithoutButton2(player, $"Du hast {number}$ aus der Kasse genommen!", "success", "center");
                                    Business.SaveBusiness(bizz);
                                    CharacterController.SaveCharacter(player);
                                    Business.OnStartBizz(player, true);
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "invitefaction":
                        {
                            if (confirmed == true)
                            {
                                FactionsModel faction = FactionController.GetFactionById(character.faction);
                                if (faction != null)
                                {
                                    if (character.rang >= 10 || character.id == faction.leader)
                                    {
                                        if (input == "n/A" || input.Length <= 3)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültiger Name!", "error", "center");
                                            player.TriggerEvent("Client:ShowCursor");
                                            return;
                                        }
                                        Player newPlayer = Helper.GetPlayerByCharacterName(input);
                                        if (newPlayer == null || newPlayer == player)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültiger Name!", "error", "center");
                                            player.TriggerEvent("Client:ShowCursor");
                                            return;
                                        }
                                        Character character2 = Helper.GetCharacterData(newPlayer);
                                        if (character2.faction == faction.id)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Der Spieler befindet sich bereits in der Fraktion!", "success", "center");
                                            return;
                                        }
                                        newPlayer.SetData<int>("Player:GroupInvite", faction.id);
                                        newPlayer.SetData<Player>("Player:GroupInvitePlayer", player);
                                        Helper.SendNotificationWithoutButton2(player, "Einladung wurde verschickt!", "success", "center");
                                        newPlayer.TriggerEvent("Client:CallInput2", "Fraktionseinladung", $"Einladung zur Fraktion {faction.name} von {character.name}, möchtest du dieser beitreten?", "factioninvite", "Ja", "Nein");
                                        Helper.CreateFactionLog(faction.id, $"{character.name} hat {input} in die Fraktion eingeladen!");
                                        player.TriggerEvent("Client:ShowCursor");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                        player.TriggerEvent("Client:ShowCursor");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "moneygroup":
                        {
                            if (confirmed == true)
                            {
                                try
                                {
                                    string[] money = new string[2];
                                    money = input.Trim().Split(",");

                                    Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                    GroupsMembers groupMember1 = GroupsController.GetGroupMemberById(character.id, mygroup.id);
                                    GroupsMembers groupMember2 = GroupsController.GetGroupMemberById(player.GetData<int>("Player:Tempid"), mygroup.id);
                                    int number = player.GetData<int>("Player:Tempid");
                                    if (groupMember1 != null && groupMember2 != null)
                                    {
                                        groupMember2.payday = Convert.ToInt32(money[0]);
                                        groupMember2.payday_day = Convert.ToInt32(money[1]);

                                        if (groupMember2.payday < 0 || groupMember2.payday_day < 0)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültige Eingabe", "error", "center");
                                            player.TriggerEvent("Client:ShowCursor");
                                            return;
                                        }

                                        Helper.SendNotificationWithoutButton2(player, "Der Lohn für den Spieler wurde erfolgreich eingestellt!", "success", "center");
                                        GroupsController.SaveGroupMember(groupMember2);

                                        string charactername = Helper.GetCharacterName(number);
                                        Helper.CreateGroupLog(mygroup.id, $"{character.name} hat den Lohn von {charactername} auf {groupMember2.payday}$ für jeden {groupMember2.payday_day}ten Payday gesetzt!");

                                        GroupsController.UpdateGroupStats(player);

                                        player.TriggerEvent("Client:ShowCursor");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    }
                                    break;
                                }
                                catch (Exception)
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Fehler bei der Eingabe!", "error", "center");
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "invitegroup":
                        {
                            if (confirmed == true)
                            {
                                Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                GroupsMembers groupMember1 = GroupsController.GetGroupMemberById(character.id, mygroup.id);
                                if (groupMember1 != null)
                                {
                                    if (groupMember1.rang >= 10 || character.id == mygroup.leader)
                                    {
                                        if (input == "n/A" || input.Length <= 3)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültiger Name!", "error", "center");
                                            player.TriggerEvent("Client:ShowCursor");
                                            return;
                                        }
                                        Player newPlayer = Helper.GetPlayerByCharacterName(input);
                                        if (newPlayer == null || newPlayer == player)
                                        {
                                            Helper.SendNotificationWithoutButton2(player, "Ungültiger Name!", "error", "center");
                                            player.TriggerEvent("Client:ShowCursor");
                                            return;
                                        }
                                        List<GroupsMembers> groupMembers = GroupsController.GetGroupMembers(mygroup.id);
                                        foreach (GroupsMembers gm in groupMembers)
                                        {
                                            if (gm != null && gm.name.ToLower() == input.ToLower())
                                            {
                                                Helper.SendNotificationWithoutButton2(player, "Dieser Spieler befindet sich bereits in der Gruppierung!", "error", "center");
                                                player.TriggerEvent("Client:ShowCursor");
                                                return;
                                            }
                                        }
                                        newPlayer.SetData<int>("Player:GroupInvite", mygroup.id);
                                        newPlayer.SetData<Player>("Player:GroupInvitePlayer", player);
                                        Helper.SendNotificationWithoutButton2(player, "Einladung wurde verschickt!", "success", "center");
                                        newPlayer.TriggerEvent("Client:CallInput2", "Gruppierungseinladung", $"Einladung zur Gruppierung {mygroup.name} von {character.name}, möchtest du dieser beitreten?", "groupinvite", "Ja", "Nein");
                                        Helper.CreateGroupLog(mygroup.id, $"{character.name} hat {input} in die Gruppierung eingeladen!");
                                        player.TriggerEvent("Client:ShowCursor");
                                    }
                                    else
                                    {
                                        Helper.SendNotificationWithoutButton2(player, "Keine Berechtigung!", "error", "center");
                                        player.TriggerEvent("Client:ShowCursor");
                                    }
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                    player.TriggerEvent("Client:ShowCursor");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "creategroup":
                        {
                            if (confirmed == true)
                            {
                                GroupsController.OnGroupSettings(player, "creategroup", input);
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                            }
                            break;
                        }
                    case "namegroup":
                        {
                            if (confirmed == true)
                            {
                                GroupsController.OnGroupSettings(player, "setgroupname", input);
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                            }
                            break;
                        }
                    case "registervehicle2":
                        {
                            if (confirmed == true)
                            {
                                OnVehicleSettings(player, "register3", input);
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "changevehicle":
                        {
                            if (confirmed == true)
                            {
                                OnVehicleSettings(player, "changevehicle2", input);
                            }
                            else
                            {
                                player.TriggerEvent("Client:ShowCursor");
                            }
                            break;
                        }
                    case "sellmeat":
                        {
                            if (confirmed == true)
                            {
                                int number = Convert.ToInt32(input);
                                Items item = ItemsController.GetItemByItemName(player, "Fleisch");
                                if (number > item.amount)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Soviel Fleisch hast du nicht dabei!", "error", "top-left");
                                    return;
                                }
                                int price = number * Helper.MeatPrice;
                                if (character.job == 2 && tempData.jobduty == true)
                                {
                                    price += (price / 100 * 10);
                                }
                                item.amount -= number;
                                if (item.amount <= 0)
                                {
                                    ItemsController.RemoveItem(player, item.itemid);
                                }
                                if (character.job != 2 || tempData.jobduty == false)
                                {
                                    CharacterController.SetMoney(player, price);
                                    Helper.SendNotificationWithoutButton(player, $"Du hast {price}$ für {number}/Stcke Fleisch bekommen!", "success", "top-left", 3500);
                                }
                                else
                                {
                                    character.nextpayday += price;
                                    Helper.SendNotificationWithoutButton(player, $"Du hast {price}$ für {number}/Stcke Fleisch für den nächsten Payday gutgeschrieben bekommen!", "success", "top-left", 3500);
                                }
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                            }
                            break;
                        }
                    case "newpin":
                        {
                            if (confirmed == true)
                            {
                                int pin = Convert.ToInt32(input);
                                Bank bank = player.GetData<Bank>("Player:Bank");
                                if (bank == null || bank.ownercharid != character.id)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du bist nicht der Inhaber von diesem Konto!", "error");
                                    player.TriggerEvent("Client:ShowCursor");
                                    return;
                                }
                                if (input.Length < 4)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Ungültiger Pin!", "error");
                                    player.TriggerEvent("Client:ShowCursor");
                                    return;
                                }
                                bank.pincode = "" + pin;
                                player.ResetData("Player:Bank");
                                if (pin != -1)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Der Pin wurde auf {bank.pincode} geändert!", "success", "top-left", 6500);
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Pin resetet, neuer Pin: {bank.pincode}!", "success", "top-left", 6500);
                                }
                            }
                            player.TriggerEvent("Client:ShowCursor");
                            break;
                        }
                    case "exitarrest":
                        {
                            Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                            if (getPlayer != null && getPlayer != player)
                            {
                                Character character2 = Helper.GetCharacterData(getPlayer);
                                if (character2 != null && character2.faction != 1)
                                {
                                    character2.arrested = 0;
                                    character2.cell = 0;
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                                    command.Parameters.AddWithValue("@name", character2.name);
                                    command.Parameters.AddWithValue("@police", character.name);
                                    command.Parameters.AddWithValue("@text", $"Aus Inhaftierung freigelassen, Grund: {input}");
                                    command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                                    command.Parameters.AddWithValue("@commentary", 0);
                                    command.ExecuteNonQuery();
                                    Helper.SendNotificationWithoutButton(player, $"Du hast die Person aus der Inhaftierung freigelassen!", "success", "top-left", 3500);
                                    Helper.SendNotificationWithoutButton(getPlayer, $"Du wurdest aus der Inhaftierung freigelassen!", "success", "top-left", 7500);
                                    Helper.CreateFactionLog(character.faction, $"{character.name} hat {character2.name} aus der Inhaftierung freigelassen, Grund: {input}");
                                    getPlayer.TriggerEvent("Client:SetArrested", false);
                                    return;
                                }
                            }
                            break;
                        }
                    case "adminlogin":
                        {
                            if (confirmed == true)
                            {
                                if (input.Length <= 0) return;
                                string adminpw = input;
                                if (account != null && account.adminlevel > 0 && adminpw.Length >= 8 && adminpw.Length <= 15)
                                {
                                    string inputpw = adminpw + "(8wgwWoRld136=";
                                    if (BCrypt.Net.BCrypt.Verify(inputpw, adminSettings.adminpassword))
                                    {
                                        player.TriggerEvent("Client:ResetTabCD");
                                        SendNotificationWithoutButton(player, "Der Adminlogin war erfolgreich!", "success", "top-end");
                                        tempData.adminduty = true;
                                        NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 1);
                                        player.SetData("Client:WrongPW", 0);
                                        player.SetData("Player:AdminDuty", true);
                                        player.SetData<int>("Player:OldHealth", NAPI.Player.GetPlayerHealth(player));
                                        Helper.SetPlayerHealth(player, 999);
                                        player.SetSharedData("Player:Adminsettings", "1,0,0");
                                        JObject obj = JObject.Parse(character.json);
                                        CharacterController.SetCharacterCloths(player, obj, character.clothing);
                                        NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, "Ungültiges Adminpasswort!", "error", "top-end");
                                        tempData.adminduty = false;
                                        NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 0);
                                        player.SetData("Client:WrongPW", player.GetData<int>("Client:WrongPW") + 1);
                                    }
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Ungültiges Adminpasswort!", "error", "top-end");
                                    tempData.adminduty = false;
                                    NAPI.Data.SetEntitySharedData(player, "Player:AdminLogin", 0);
                                    player.SetData("Client:WrongPW", player.GetData<int>("Client:WrongPW") + 1);
                                }
                                if (player.GetData<int>("Client:WrongPW") >= 3)
                                {
                                    Helper.CreateAdminLog("adminlog", $"{account.name} hat das Adminpasswort 3x falsch eingegeben und wurde gekickt!", Helper.GetIP(player), account.identifier);
                                    SendNotificationWithoutButton(player, "Du hast das Adminpasswort zu oft falsch eingegeben!", "error", "top-end", 5500);
                                    AntiCheatController.SetKick(player, "Adminpasswort 3x falsch eingegeben", "Server", false, false);
                                    return;
                                }
                            }
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                if (flag.Contains("bizz"))
                {
                    Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                }
                else
                {
                    SendNotificationWithoutButton(player, "Ungültige Operation!", "error", "top-end");
                }
                Helper.ConsoleLog("error", $"[OnOnInput]: " + e.ToString());
            }
        }

        //Jobfunktionen
        //DJ
        public static int IsADJ(Player player)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                //Firma
                if (character.mygroup != -1)
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        if (Convert.ToInt32(group.licenses.Split("|")[2]) == 1)
                        {
                            return 1;
                        }
                    }
                }
                //Job
                if (character.job == 2 && tempData.jobduty == true)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsADJ]: " + e.ToString());
            }
            return -1;
        }
        //Spediteur
        public static bool IsASpeditionsVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null) return false;
                int vclass = vehicle.Class;
                if (vclass == 12 || vclass == 20)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsASpeditionsVehicle]: " + e.ToString());
            }
            return false;
        }

        public static int IsASpediteur(Player player)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                //Firma
                if (character.mygroup != -1)
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        if (Convert.ToInt32(group.licenses.Split("|")[0]) == 1)
                        {
                            return 1;
                        }
                    }
                }
                //Job
                if (character.job == 1 && tempData.jobduty == true)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsASpediteur]: " + e.ToString());
            }
            return -1;
        }

        public static int IsABusDriver(Player player)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                //Firma
                if (character.mygroup != -1)
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        if (Convert.ToInt32(group.licenses.Split("|")[3]) == 1)
                        {
                            return 1;
                        }
                    }
                }
                //Job
                if (character.job == 3 && tempData.jobduty == true)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsABusDriver]: " + e.ToString());
            }
            return -1;
        }

        public static int IsATaxiDriver(Player player)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                //Firma
                if (character.mygroup != -1)
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        if (Convert.ToInt32(group.licenses.Split("|")[3]) == 1)
                        {
                            return 1;
                        }
                    }
                }
                //Job
                if (character.job == 6 && tempData.jobduty == true)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsATaxiDriver]: " + e.ToString());
            }
            return -1;
        }

        public static int IsASecruity(Player player)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                //Firma
                if (character.mygroup != -1)
                {
                    Groups group = GroupsController.GetGroupById(character.mygroup);
                    if (group != null)
                    {
                        if (Convert.ToInt32(group.licenses.Split("|")[4]) == 1)
                        {
                            return 1;
                        }
                    }
                }
                //Job
                if (character.job == 8 && tempData.jobduty == true)
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsASecruity]: " + e.ToString());
            }
            return -1;
        }

        public static SpedVehicles GetSpedVehicleById(int id)
        {
            try
            {
                foreach (SpedVehicles spedvehicle in spedVehiclesList)
                {
                    if (spedvehicle != null && spedvehicle.id == id)
                    {
                        return spedvehicle;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetSpedVehicleById]: " + e.ToString());
            }
            return null;
        }

        public static SpedVehicles GetSpedVehicleByModel(string model)
        {
            try
            {
                foreach (SpedVehicles spedvehicle in spedVehiclesList)
                {
                    if (spedvehicle != null)
                    {
                        if (spedvehicle.name.ToLower() == model.ToLower())
                        {
                            return spedvehicle;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetSpedVehicleById]: " + e.ToString());
            }
            return null;
        }

        //Allgemein
        [RemoteEvent("Server:JobSettings")]
        public static void OnJobSettings(Player player, string setting, string value)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                Account account = GetAccountData(player);
                switch (setting.ToLower())
                {
                    case "getjob":
                        {
                            int number = Convert.ToInt32(value);

                            if (number > 0)
                            {
                                if (character.jobless == 1)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst das Arbeitslosengeld kündigen!", "error", "top-end");
                                    return;
                                }
                                if (number == character.job)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du hast diesen Job bereits!", "error", "top-end");
                                    return;
                                }
                                if (character.job > 0)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du musst zuerst deinen alten Job kündigen!", "error", "top-end");
                                    return;
                                }
                                if (number == 1 || number == 3)
                                {
                                    if (SetAndGetCharacterLicense(player, 2, 1337) != "1")
                                    {
                                        SendNotificationWithoutButton(player, "Du besitzt keinen Truckerschein!", "error", "top-end", 2250);
                                        return;
                                    }
                                }
                                if (number == 2)
                                {
                                    if (SetAndGetCharacterLicense(player, 5, 1337) != "1")
                                    {
                                        SendNotificationWithoutButton(player, "Du besitzt keinen Waffenschein!", "error", "top-end", 2250);
                                        return;
                                    }
                                }
                                if (account.faqarray[7] == "0")
                                {
                                    account.faqarray[7] = "1";
                                }
                                player.SetOwnSharedData("Player:Job", number);
                                character.job = number;
                                player.TriggerEvent("Client:HideStadthalle");
                                Helper.SendNotificationWithoutButton2(player, $"Du hast den Job als {Helper.GetJobName(number)} angenommen, alle wichtigen Infos findest du im F2 Menü!", "success", "center", 3550);
                            }
                            else
                            {
                                if (number == 0)
                                {
                                    if (character.job == 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du hast keinen Job!", "error", "top-end");
                                        return;
                                    }
                                    player.SetOwnSharedData("Player:Job", 0);
                                    character.job = 0;
                                    player.TriggerEvent("Client:HideStadthalle");
                                    Helper.SendNotificationWithoutButton2(player, "Du hast deinen aktuellen Job gekündigt!", "success", "center", 3550);
                                }
                                else if (number == -1)
                                {
                                    if (character.job > 0 && character.faction > 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du kannst kein Arbeitslosengeld beantragen!", "error", "top-end");
                                        return;
                                    }
                                    if (character.jobless == 1)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du beziehst schon Arbeitslosengeld!", "error", "top-end");
                                        return;
                                    }
                                    character.jobless = 1;
                                    player.TriggerEvent("Client:HideStadthalle");
                                    Helper.SendNotificationWithoutButton2(player, "Du beziehst absofort Arbeitslosengeld!", "success", "center", 3550);
                                }
                                else if (number == -2)
                                {
                                    if (character.jobless == 0)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Du beziehst kein Arbeitslosengeld!", "error", "top-end");
                                        return;
                                    }
                                    character.jobless = 0;
                                    player.TriggerEvent("Client:HideStadthalle");
                                    Helper.SendNotificationWithoutButton2(player, "Du beziehst absofort kein Arbeitslosengeld mehr!", "success", "center", 3550);
                                }
                            }
                            break;
                        }
                    case "acceptorder":
                        {
                            if (Helper.IsASpediteur(player) == -1)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du bist kein Spediteur!", "error", "center");
                                return;
                            }
                            SpedVehicles spedVehicle = GetSpedVehicleById(Convert.ToInt32(value));
                            if (spedVehicle == null)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                return;
                            }
                            SpedOrders spedOrder = SpedOrders.GetSpedOrderById(Convert.ToInt32(value));
                            if (spedVehicle.capa < spedOrder.capa)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug ist nicht für den Auftrag geeignet!", "error", "center");
                                return;
                            }
                            tempData.jobstatus = 1;
                            tempData.order = spedOrder;
                            player.TriggerEvent("Client:HideSpedition");
                            SpedOrders.spedOrderList.Remove(spedOrder);
                            Helper.SendNotificationWithoutButton(player, $"Auftrag angenommen, bitte hol die Ware jetzt bei/m {spedOrder.from} ab!", "success", "top-left", 4000);
                            player.TriggerEvent("Client:CreateWaypoint", spedOrder.position1.X, spedOrder.position1.Y);
                            player.TriggerEvent("Client:CreateMarker", spedOrder.position1.X, spedOrder.position1.Y, spedOrder.position1.Z, 39);
                            tempData.jobColshape = NAPI.ColShape.CreatCircleColShape(spedOrder.position1.X, spedOrder.position1.Y, 3.5f, player.Dimension);
                            break;
                        }
                    case "accepttaxi":
                        {
                            if (Helper.IsATaxiDriver(player) == -1)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du bist kein Taxifahrer!", "error", "center");
                                return;
                            }
                            if (player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                            {
                                foreach (TaxiBots taxiBot in Helper.taxiBotList)
                                {
                                    if (taxiBot.id == Convert.ToInt32(value))
                                    {
                                        tempData.order2 = taxiBot;
                                        break;
                                    }
                                }
                                player.SetData<int>("Player:BusTime", Helper.UnixTimestamp() + (6));
                                tempData.jobstatus = 1;
                                player.TriggerEvent("Client:HideSpedition");
                                Helper.taxiBotList.Remove(tempData.order2);
                                Helper.SendNotificationWithoutButton(player, $"Taxiauftrag angenommen, bitte hol deinen Fahrgast jetzt bei/m {tempData.order2.from} ab!", "success", "top-left", 4000);
                                player.TriggerEvent("Client:CreatePed", tempData.order2.v1.X, tempData.order2.v1.Y, tempData.order2.v1.Z, 1);
                                tempData.jobColshape = NAPI.ColShape.CreatCircleColShape(tempData.order2.v1.X, tempData.order2.v1.Y, 3.85f, player.Dimension);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug ist nicht für den Taxiauftrag geeignet!", "error", "center");
                            }
                            break;
                        }
                    case "acceptatm":
                        {
                            if (Helper.IsASecruity(player) == -1)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du bist kein Sicherheitsmitarbeiter!", "error", "center");
                                return;
                            }
                            if (player.IsInVehicle && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade")
                            {
                                foreach (ATMSpots atmSpot in Helper.atmSpotList)
                                {
                                    if (atmSpot.id == Convert.ToInt32(value))
                                    {
                                        tempData.furniturePosition = atmSpot.position;
                                        tempData.tempValue = atmSpot.id;
                                        break;
                                    }
                                }
                                tempData.jobstatus = 1;
                                player.TriggerEvent("Client:HideSpedition");
                                Helper.SendNotificationWithoutButton(player, $"Der Bankautomat wurde markiert!", "success", "top-left", 4000);
                                player.TriggerEvent("Client:CreateWaypoint", tempData.furniturePosition.X, tempData.furniturePosition.Y);
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug ist nicht für diesen Auftrag geeignet!", "error", "center");
                            }
                            break;
                        }
                    case "acceptsecruity":
                        {
                            if (Helper.IsASecruity(player) == -1)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du bist kein Sicherheitsmitarbeiter!", "error", "center");
                                return;
                            }
                            if (player.IsInVehicle && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade")
                            {
                                Business bizz = Business.GetBusinessById(Convert.ToInt32(value));
                                if (bizz != null)
                                {
                                    player.TriggerEvent("Client:HideSpedition");
                                    Helper.SendNotificationWithoutButton(player, $"Das Business/Unternehmen wurde markiert!", "success", "top-left", 4000);
                                    player.TriggerEvent("Client:CreateWaypoint", bizz.position.X, bizz.position.Y);
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton2(player, "Dieses Fahrzeug ist nicht für diesen Auftrag geeignet!", "error", "center");
                            }
                            break;
                        }
                    case "selectvehiclespediteur":
                        {
                            if (character.job != 1)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du bist kein Spediteur!", "error", "center");
                                return;
                            }
                            SpedVehicles spedVehicle = GetSpedVehicleById(Convert.ToInt32(value));
                            if (spedVehicle == null)
                            {
                                Helper.SendNotificationWithoutButton2(player, "Ungültige Interaktion!", "error", "center");
                                return;
                            }
                            if (character.truckerskill < (spedVehicle.skill * 45))
                            {
                                Helper.SendNotificationWithoutButton2(player, "Du hast noch nicht den benötigten Skill erreicht!", "error", "center");
                                return;
                            }
                            Vector3[] spawnTruck = new Vector3[3]
                                                 { new Vector3(-1091.5062, -2050.7183, 13.2992935-0.1),
                                                   new Vector3(-1102.0541, -2039.6836, 13.300303-0.1),
                                                   new Vector3(-1069.9792, -2072.0244, 13.310075-0.1)};
                            Random rand = new Random();
                            int index = rand.Next(3);
                            Random rand2 = new Random();
                            tempData.jobVehicle = Cars.createNewCar(spedVehicle.name, spawnTruck[index], -46.03f, rand2.Next(0, 159), rand2.Next(0, 159), "LS-S-155" + player.Id, "Spedition", true, true, false);
                            tempData.jobVehicle.Dimension = 0;
                            player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            Helper.SendNotificationWithoutButton(player, "Hier dein Fahrzeug, fahr es bitte noch von der Ladefläche runter!", "success", "top-left", 3000);
                            player.TriggerEvent("Client:HideSpedition");
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnJobSettings]: " + e.ToString());
            }
        }

        //Handsup
        [RemoteEvent("Server:SetHandsUp")]
        public static void OnPlayerHandsUp(Player player)
        {
            try
            {
                TempData tempData = GetCharacterTempData(player);
                if (tempData != null)
                {
                    if (player.HasData("Player:Use") && player.GetData<bool>("Player:Use") == true) return;
                    if (tempData.handsUp == false)
                    {
                        tempData.handsUp = true;
                        player.SetSharedData("Player:AnimData", $"missminuteman_1ig_2%handsup_base%{50}");
                    }
                    else
                    {
                        tempData.handsUp = false;
                        OnStopAnimation2(player);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerHandsUp]: " + e.ToString());
            }
        }

        //Keys
        [RemoteEvent("Server:OnPlayerPressSTRG")]
        public static void OnPlayerPressSTRG(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || tempData == null || account == null) return;
                //Abkoppeln
                if (player.IsInVehicle && Helper.IsATruck(player.Vehicle) && Helper.IsTrailerAttached(player))
                {
                    SendNotificationWithoutButton(player, "Anhänger abgekoppelt!", "success");
                    Vehicle veh = Helper.GetClosestVehicleFromVehicle(player, 9.85f);
                    if (veh != null && IsATrailer(veh))
                    {
                        veh.Spawn(new Vector3(veh.Position.X, veh.Position.Y, veh.Position.Z - 0.5));
                        NAPI.Task.Run(() =>
                        {
                            veh.Rotation = new Vector3(0.0, 0.0, player.Vehicle.Rotation.Z);
                            veh.EngineStatus = false;
                        }, delayTime: 215);
                        return;
                    }
                }
                //Taxi Taxameter
                if (IsATaxiDriver(player) > -1 && player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                {
                    if (player.HasData("Player:Fare") && player.GetData<int>("Player:Taxameter") > 0)
                    {
                        player.SetData<int>("Player:Taxameter", 0);
                        if (Helper.IsATaxiDriver(player) == 2)
                        {
                            player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$ | ~y~Taxameter: {player.GetData<int>("Player:Taxameter")}$");
                        }
                        else
                        {
                            player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$ | ~y~Taxameter: {player.GetData<int>("Player:Taxameter")}$");
                        }
                        player.SetData<float>("Player:Taxakilometer", player.Vehicle.GetSharedData<float>("Vehicle:Kilometre"));
                        SendNotificationWithoutButton(player, $"Taxameter resetet!", "success", "top-left", 2550);
                        return;
                    }
                }
                //Straße reinigen
                if (player.IsInVehicle && character.job == 4 && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "sweeper" && Helper.garbageCount > 0)
                {
                    if (player.HasData("Player:CleanCooldown"))
                    {
                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:CleanCooldown"))
                        {
                            return;
                        }
                        player.ResetData("Player:CleanCooldown");
                    }
                    foreach (Garbage garbage in Helper.garbageList)
                    {
                        if (garbage.created == true && garbage.position.DistanceTo(player.Position) < 2.35)
                        {
                            garbage.created = false;
                            Helper.garbageCount--;
                            garbage.objectHandle.Delete();
                            garbage.objectHandle = null;
                            int salary = 25 + rnd.Next(0, 11);
                            SendNotificationWithoutButton(player, $"Boden gereinigt und Müll entfernt, du erhältst {salary}$ für den nächsten Payday gutgeschrieben!", "success", "top-left", 4250);
                            character.nextpayday += salary;
                            player.TriggerEvent("Client:RemoveWaypoint2");
                            player.TriggerEvent("Client:PlaySoundPeep2");
                            player.SetData<int>("Player:CleanCooldown", Helper.UnixTimestamp() + (2));
                        }
                    }
                    return;
                }
                //Grabbing
                if (!player.IsInVehicle)
                {
                    Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                    if (getPlayer != null && getPlayer != player && !getPlayer.IsInVehicle)
                    {
                        Account account2 = Helper.GetAccountData(getPlayer);
                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                        Character character2 = Helper.GetCharacterData(getPlayer);
                        if (character2.death == true || tempData.adminduty == true)
                        {
                            if (tempData2.follow == false && tempData2.followed == false)
                            {
                                if (tempData.follow == true)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du wirst schon getragen!", "error");
                                    return;
                                }
                                getPlayer.TriggerEvent("Client:HideMenus");
                                NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                NAPI.Player.SetPlayerCurrentWeapon(getPlayer, WeaponHash.Unarmed);
                                Helper.SendNotificationWithoutButton(player, "Du trägst jemanden!", "success");
                                Helper.SendNotificationWithoutButton(getPlayer, "Du wirst getragen!", "success");
                                getPlayer.TriggerEvent("Client:PlayerFreeze", true);
                                player.SetSharedData("Player:Follow", getPlayer.Id);
                                player.SetSharedData("Player:AnimData", "missfinale_c2mcs_1%fin_c2_mcs_1_camman%49");
                                NAPI.Task.Run(() =>
                                {
                                    getPlayer.SetSharedData("Player:AnimData", "nm%firemans_carry%33");
                                }, delayTime: 215);
                                NAPI.Task.Run(() =>
                                {
                                    getPlayer.Heading = player.Heading + 90;
                                    tempData.followed = true;
                                    player.SetSharedData("Player:FollowStatus", 2);
                                    tempData2.follow = true;
                                }, delayTime: 115);
                            }
                            else
                            {
                                if (tempData.follow == true)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Du wirst schon getragen!", "error");
                                    return;
                                }
                                Helper.SendNotificationWithoutButton(player, "Du trägst keinen mehr!", "success");
                                Helper.SendNotificationWithoutButton(getPlayer, "Du wirst nicht mehr getragen!", "success");
                                player.SetSharedData("Player:FollowStatus", 0);
                                tempData.followed = false;
                                NAPI.Task.Run(() =>
                                {
                                    player.SetSharedData("Player:Follow", "n/A");
                                    player.ResetSharedData("Player:Follow");
                                    player.ResetSharedData("Player:FollowStatus");
                                    Vector3 newPosition = Helper.GetPositionInFrontOfPlayer(player, 1.15f);
                                    if (character2.death == false)
                                    {
                                        SetPlayerPosition(getPlayer, newPosition);
                                    }
                                    else
                                    {
                                        SetPlayerPosition(getPlayer, new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 1.0f));
                                    }
                                    getPlayer.Heading = player.Heading + 90;
                                    tempData2.follow = false;
                                    Helper.OnStopAnimation(getPlayer);
                                    Helper.OnStopAnimation(player);
                                    getPlayer.TriggerEvent("Client:PlayerFreeze", false);
                                    if (tempData2.cuffed > 0)
                                    {
                                        getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                    }
                                    else if (character2.death == true)
                                    {
                                        PlayerDeathAnim(getPlayer);
                                    }
                                    else
                                    {
                                        Helper.OnStopAnimation(getPlayer);
                                    }
                                }, delayTime: 115);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressSTRG]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF5")]
        public static void OnPlayerPressF5(Player player, int hide = 0)
        {
            try
            {
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (player == null || !player.Exists || account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false || player.HasData("Player:PlayCustomAnimation")) return;
                SmartphoneController.ShowSmartphone(player, null, "0", hide);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF5]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF6")]
        public static void OnPlayerPressF6(Player player, bool abort = false)
        {
            try
            {
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;
                //Helikoper Kamera
                if (player.IsInVehicle && player.VehicleSeat == 1 && player.Vehicle.EngineStatus == true && (player.Vehicle.GetSharedData<String>("Vehicle:Name").ToLower() == "polmav" || player.Vehicle.GetSharedData<String>("Vehicle:Name").ToLower() == "buzzard2" || player.Vehicle.GetSharedData<String>("Vehicle:Name").ToLower() == "swift"))
                {
                    player.TriggerEvent("Client:StartHeliCam");
                    player.TriggerEvent("Client:StartHeliCamSettings");
                    return;
                }
                //Nokeys
                if (abort == true) return;
                //Gangzone
                if (tempData.ingangzone != -1 && character.factionduty == false)
                {
                    player.TriggerEvent("Client:ShowGangzone");
                    return;
                }
                //Crafting + Kleiderschrank
                House house = null;
                FurnitureSetHouse furniture = null;
                if (character.inhouse == -1)
                {
                    house = House.GetClosestHouse(player, 25.5f);
                }
                else
                {
                    house = House.GetHouseById(character.inhouse);
                }
                if (house != null)
                {
                    furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
                    if (furniture != null)
                    {
                        if (furniture.name.Contains("Werkbank"))
                        {
                            Items mats = ItemsController.GetItemByItemName(player, "Materialien");
                            int amount = 0;
                            if (mats != null)
                            {
                                amount = mats.amount;
                            }
                            player.TriggerEvent("Client:ShowCraft", amount);
                            return;
                        }
                        else if (furniture.name.Contains("Kleiderschrank"))
                        {
                            if (tempData.adminduty == true || character.factionduty == true)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du kannst den Kleiderschrank jetzt nicht benutzen!", "error");
                                return;
                            }
                            ShowWardrobe(player, furniture);
                            return;
                        }
                    }
                }
                //Strafzettel
                if (!player.GetOwnSharedData<bool>("Player:DevModus"))
                {
                    if (character.faction == 3 && character.factionduty == true)
                    {
                        if (player.IsInVehicle)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                            return;
                        }
                        Vehicle vehicle = Helper.GetClosestVehicle(player, 2.55f);
                        if (vehicle == null)
                        {
                            Helper.SendNotificationWithoutButton(player, "Es befindet sich kein Fahrzeug in der Nähe!", "error");
                            return;
                        }
                        VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                        if (vehicleData != null && (vehicleData.owner.Contains("group-") || vehicleData.owner.Contains("character-")))
                        {
                            int charid = -1;
                            string charname = "n/A";
                            tempData.tempValue = -1;
                            if (vehicleData.owner.Contains("character-"))
                            {
                                charid = Convert.ToInt32(vehicleData.owner.Split("-")[1]);
                            }
                            else if (vehicleData.owner.Contains("group-"))
                            {
                                Groups group = GroupsController.GetGroupById(Convert.ToInt32(vehicleData.owner.Split("-")[1]));
                                if (group != null)
                                {
                                    charid = group.leader;
                                    tempData.tempValue = group.id;
                                }
                                else
                                {
                                    charid = -1;
                                }
                            }
                            if (charid != -1)
                            {
                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "SELECT name FROM characters where id=@id LIMIT 1";
                                command.Parameters.AddWithValue("@id", charid);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        reader.Read();
                                        charname = reader.GetString("name");
                                    }
                                    reader.Close();
                                }

                                if (charname != "n/A")
                                {
                                    tempData.undercover = charname;

                                    player.TriggerEvent("Client:ShowTicket", charname);
                                }
                                return;
                            }
                        }
                        Helper.SendNotificationWithoutButton(player, "Du kannst kein Strafzettel an das Fahrzeug hängen!", "error");
                        return;
                    }
                }
                //Feuerlöscher auffüllen
                if (!player.IsInVehicle && character.faction == 2)
                {
                    Vehicle vehicleTemp = GetClosestVehicleFromVehicle(player, 4.75f);
                    if (vehicleTemp != null && vehicleTemp.GetSharedData<string>("Vehicle:Name").ToLower().Contains("firetruk") && NAPI.Player.GetPlayerCurrentWeapon(player) == WeaponHash.Fireextinguisher)
                    {
                        if (NAPI.Player.GetPlayerWeaponAmmo(player, WeaponHash.Fireextinguisher) >= 2000)
                        {
                            SendNotificationWithoutButton(player, $"Der Feuerlöscher ist schon voll!", "error");
                            return;
                        }
                        NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Fireextinguisher, 2000);
                        SendNotificationWithoutButton(player, $"Der Feuerlöscher wurde aufgefüllt!", "success");
                        return;

                    }
                }
                //Ghettoblaster
                if (!player.IsInVehicle && tempData.ghettoblaster != null && tempData.ghettoblaster.Position.DistanceTo(player.Position) <= 2.75)
                {
                    player.TriggerEvent("Client:SetSoundRange", 15.5f);
                    player.TriggerEvent("Client:ShowMusicStation", 2);
                }
                //Waffenkammer Lieferung
                if (player.IsInVehicle && player.Vehicle != null && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade" && (character.faction == 1 || character.faction == 2) && character.factionduty == true && IsInRangeOfPoint(player.Position, new Vector3(163.66289, -3290.7888, 5.928894), 3.75f))
                {
                    if (MDCController.weaponOrderStatus == "RDY" && MDCController.weaponOrder != "n/A")
                    {
                        if (MDCController.weaponOrderFaction != character.faction)
                        {
                            SendNotificationWithoutButton(player, "Die Lieferung ist nicht für euch!", "success");
                            return;
                        }
                        if (character.rang <= 3)
                        {
                            SendNotificationWithoutButton(player, "Du hast leider keine Berechtigung die Lieferung abzuholen!", "success");
                            return;
                        }
                        if (character.factionduty == false)
                        {
                            SendNotificationWithoutButton(player, "Bist du überhaupt im Dienst?", "error", "top-end", 2250);
                            return;
                        }
                        if (player.Vehicle.HasData("Vehicle:WaffenTransport"))
                        {
                            SendNotificationWithoutButton(player, "Dein Laderaum ist voll!", "success");
                            return;
                        }
                        if (player.Vehicle.HasData("Vehicle:AsservatenTransport"))
                        {
                            SendNotificationWithoutButton(player, "Dein Laderaum ist voll!", "success");
                            return;
                        }
                        if (character.faction == 1)
                        {
                            player.TriggerEvent("Client:CreateWaypoint", 597.40106, -33.440994);
                        }
                        else if (character.faction == 2)
                        {
                            player.TriggerEvent("Client:CreateWaypoint", -687.32886, 338.13617);
                        }
                        else if (character.faction == 3)
                        {
                            player.TriggerEvent("Client:CreateWaypoint", -355.77936, -160.7771);
                        }
                        SendNotificationWithoutButton(player, "Hier eure Bestellung wir haben diese schon mal vorbereitet, bringe diese jetzt zum Auslieferungspunkt!", "success");
                        if (character.faction == 1)
                        {
                            MDCController.SendPoliceMDCMessage(player, $"{character.name} hat die Waffenbestellung abgeholt!");
                        }
                        else
                        {
                            MDCController.SendMedicMDCMessage(player, $"{character.name} hat die Waffenbestellung abgeholt!");
                        }
                        Helper.CreateFactionLog(character.faction, $"{character.name} hat die Waffenlieferung abgeholt!");
                        MDCController.weaponOrderStatus = "DLY";
                        player.Vehicle.SetData<bool>("Vehicle:WaffenTransport", true);
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Aktuell haben wir keine offene Bestellung für euch!", "error");
                    }
                }
                if (player.IsInVehicle && player.Vehicle != null && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade" && (character.faction == 1 || character.faction == 2 || character.faction == 3) && character.factionduty == true && (IsInRangeOfPoint(player.Position, new Vector3(597.40106, -33.440994, 70.628044), 5.75f) || IsInRangeOfPoint(player.Position, new Vector3(-687.32886, 338.13617, 78.118355), 5.75f) || IsInRangeOfPoint(player.Position, new Vector3(-355.77936, -160.7771, 38.7285), 5.75f)))
                {
                    if (MDCController.weaponOrderStatus == "DLY" && MDCController.weaponOrder != "n/A" && player.Vehicle.HasData("Vehicle:WaffenTransport"))
                    {
                        int count = 0;
                        string[] deliverArray = new string[15];
                        deliverArray = MDCController.weaponOrder.Split(",");

                        foreach (ShopItems shopItem in shopItemList)
                        {
                            if (shopItem != null && shopItem.shoplabel == "Waffenkammer-" + character.faction)
                            {
                                if (Convert.ToInt32(deliverArray[count]) > 0)
                                {
                                    shopItem.itemprice += Convert.ToInt32(deliverArray[count]);
                                }
                                count++;
                            }
                        }
                        if (character.faction == 1)
                        {
                            MDCController.SendPoliceMDCMessage(player, $"{character.name} hat die Waffenbestellung abgeliefert!");
                        }
                        else if (character.faction == 2)
                        {
                            MDCController.SendMedicMDCMessage(player, $"{character.name} hat die Waffenbestellung abgeliefert!");
                        }
                        else if (character.faction == 3)
                        {
                            MDCController.SendACLSMDCMessage(player, $"{character.name} hat die Waffenbestellung abgeliefert!");
                        }
                        Helper.CreateFactionLog(character.faction, $"{character.name} hat die Waffenlieferung abgeliefert!");
                        MDCController.weaponOrderStatus = "DNE";
                        MDCController.weaponOrderFaction = 0;
                        MDCController.weaponOrder = "n/A";
                        player.Vehicle.SetData<bool>("Vehicle:WaffenTransport", false);
                        player.Vehicle.ResetData("Vehicle:WaffenTransport");
                        player.TriggerEvent("Client:PlaySoundPeep2");
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Du hast keine Ware dabei!", "error");
                    }
                }
                //Asservatenkammer Transport
                if (player.IsInVehicle && player.Vehicle != null && !player.Vehicle.HasData("Vehicle:WaffenTransport") && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade" && character.faction == 1 && character.factionduty == true && IsInRangeOfPoint(player.Position, new Vector3(597.40106, -33.440994, 70.628044), 5.75f))
                {
                    if (ItemsController.CountEvidenceItems() <= 0)
                    {
                        SendNotificationWithoutButton(player, "Die Asservatenkammer ist leer!", "error");
                        return;
                    }
                    foreach (ItemsGlobal globalitem in ItemsController.itemListGlobal.ToList())
                    {
                        if (globalitem.owneridentifier == "evidence")
                        {
                            ItemsController.itemListGlobal.Remove(globalitem);
                        }
                    }
                    player.TriggerEvent("Client:CreateWaypoint2", -293.65198, -2209.025, 9.896334, "Zerstörungsanlage");
                    MDCController.SendPoliceMDCMessage(player, $"{character.name} hat einen Asservatenkammer Transport gestartet!");
                    Helper.CreateFactionLog(character.faction, $"{character.name} hat einen Asservatenkammer Transport gestartet!");
                    SendNotificationWithoutButton(player, "Asservatenkammer geleert und Transporter befüllt, bring den Transport jetzt zur Zerstörungsanlage!", "success", "top-left", 4750);
                    player.Vehicle.SetData<bool>("Vehicle:AsservatenTransport", true);
                }
                //Asservatenkammer Lieferung
                if (player.IsInVehicle && player.Vehicle != null && !player.Vehicle.HasData("Vehicle:WaffenTransport") && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade" && character.faction == 1 && character.factionduty == true && IsInRangeOfPoint(player.Position, new Vector3(-293.65198, -2209.025, 9.896334), 5.75f))
                {
                    if (!player.Vehicle.HasData("Vehicle:AsservatenTransport"))
                    {
                        SendNotificationWithoutButton(player, "Dein Laderaum ist leer!", "error");
                        return;
                    }
                    player.TriggerEvent("Client:RemoveWaypoint2");
                    MDCController.SendPoliceMDCMessage(player, $"{character.name} hat den Asservatenkammer Transport erfolgreich beendet!");
                    Helper.CreateFactionLog(character.faction, $"{character.name} hat den Asservatenkammer Transport erfolgreich beendet!");
                    SendNotificationWithoutButton(player, "Asservatenkammer Transport beendet und die Ware wurde abgeliefert!", "success", "top-left", 4750);
                    player.Vehicle.SetData<bool>("Vehicle:AsservatenTransport", false);
                    player.Vehicle.ResetData("Vehicle:AsservatenTransport");
                    player.TriggerEvent("Client:PlaySoundPeep2");
                }
                //Taxifahrer - Taxameter
                if (IsATaxiDriver(player) > -1 && player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus") && player.HasData("Player:Fare"))
                {
                    if (player.HasData("Player:TaxameterOn"))
                    {
                        player.ResetData("Player:TaxameterOn");
                        SendNotificationWithoutButton(player, "Taxameter ausgeschaltet!", "success");
                        NAPI.Task.Run(() =>
                        {
                            if (Helper.IsATaxiDriver(player) == 2)
                            {
                                player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$");
                            }
                            else
                            {
                                player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$");
                            }
                        }, delayTime: 225);
                    }
                    else
                    {
                        player.SetData<bool>("Player:TaxameterOn", true);
                        SendNotificationWithoutButton(player, "Taxameter angeschaltet!", "success");
                        NAPI.Task.Run(() =>
                        {
                            if (Helper.IsATaxiDriver(player) == 2)
                            {
                                player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$ | ~y~Taxameter: {player.GetData<int>("Player:Taxameter")}$");
                            }
                            else
                            {
                                player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: {player.GetData<int>("Player:Fare")}$ | ~y~Taxameter: {player.GetData<int>("Player:Taxameter")}$");
                            }
                        }, delayTime: 225);
                    }
                    return;
                }
                //Landwirt
                if (character.job == 7 && tempData.jobduty == true && !player.IsInVehicle && IsInRangeOfPoint(player.Position, new Vector3(1935.6422, 5007.799, 42.914547), 525f))
                {
                    if (!player.HasData("Player:FarmerRoute"))
                    {
                        player.SetData<int>("Player:FarmerRoute", 3);
                        player.TriggerEvent("Client:StartFarmerTomato");
                    }
                    else
                    {
                        if (player.GetData<int>("Player:FarmerRoute") == 3)
                        {
                            player.ResetData("Player:FarmerRoute");
                            player.TriggerEvent("Client:StopFarmer");
                            SendNotificationWithoutButton(player, "Landwirtschafts Aktion beendet!", "success");
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deine Landwirtschaft Aktion beenden!", "error");
                        }
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF6]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF7")]
        public static void OnPlayerPressF7(Player player, bool update = false)
        {
            try
            {
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;
                //Animationen
                if (player.GetOwnSharedData<bool>("Player:Death") == false && !player.HasData("Player:PlayCustomAnimation"))
                {
                    if (player.HasData("Player:Mikrofon") || player.HasData("Player:Filmkamera"))
                    {
                        if (player.HasData("Player:Mikrofon"))
                        {
                            player.SetSharedData("Player:AnimData", $"missheistdocksprep1hold_cellphone%hold_cellphone%{50}");
                        }
                        else if (player.HasData("Player:Filmkamera"))
                        {
                            player.SetSharedData("Player:AnimData", $"missfinale_c2mcs_1%fin_c2_mcs_1_camman%{50}");
                        }
                        return;
                    }
                    string text1 = "";
                    string text2 = "";
                    var DistinctItems = Helper.animList.GroupBy(x => x.category).Select(y => y.First()).OrderBy(n => n.category);
                    foreach (var item in DistinctItems)
                    {
                        text1 += $"{item.category[0].ToString().ToUpper() + item.category.Substring(1)},";
                        text2 += $"0,";
                    }
                    text1 += "Abbrechen";
                    text2 += "0";
                    tempData.lastShop = "Animationsmenü";
                    if (update == false)
                    {
                        player.TriggerEvent("Client:ShowShop2", text1, text2, "Animationsmenü", 0, 1, 1, false);
                    }
                    else
                    {
                        player.TriggerEvent("Client:ShowShop2", text1, text2, "Animationsmenü", 1, 0, 0, true);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF7]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF8")]
        public static void OnPlayerPressF8(Player player, bool update = false)
        {
            try
            {
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;

                //Strafzettel
                if (character.faction == 1 && character.factionduty == true)
                {
                    FactionController.OnFactionSettings(player, "wheel", "9");
                }

                //Rezept ausstellen
                if (character.faction == 2 && character.factionduty == true)
                {
                    FactionController.OnFactionSettings(player, "wheel", "9");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF8]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF4")]
        public static void OnPlayerPressF4(Player player)
        {
            try
            {
                //Accountdata + Character Data
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;
                //Garagensystem
                //Shovel
                if (ItemsController.PlayerHasItem(player, "Kleine-Schaufel") && player.Position.DistanceTo(new Vector3(1406.4316, 3730.3584, 33.2187)) <= 35)
                {
                    Helper.StartShovel(player);
                    return;
                }
                //Verwahrplatz
                if (IsInRangeOfPoint(player.Position, new Vector3(408.9756, -1622.715, 29.291948), 6.75f))
                {
                    List<CenterMenu> centerMenu = new List<CenterMenu>();
                    foreach (Cars car in Cars.carList)
                    {
                        if (car.vehicleData.garage == "towed-1")
                        {
                            CenterMenu cMenu = new CenterMenu();
                            cMenu.var1 = "" + car.vehicleData.towed + "$";
                            if (car.vehicleData.ownname != "n/A")
                            {
                                cMenu.var2 = car.vehicleData.ownname;
                            }
                            else
                            {
                                cMenu.var2 = car.vehicleData.vehiclename;
                            }
                            cMenu.var3 = car.vehicleData.plate;
                            cMenu.var4 = "" + car.vehicleData.id;
                            centerMenu.Add(cMenu);
                        }
                    }
                    String rules = "Kosten,Fahrzeugname,Nummernschild,Aktion";
                    player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu.OrderBy(x => x.var3.Length).ThenBy(x => x.var3).ToList()), "Verwahrplatz");
                    return;
                }
                //Fraktionsgaragen
                //LSPD
                if ((IsInRangeOfPoint(player.Position, new Vector3(463.60544, -982.2816, 43.692), 37.75f) && player.Position.Z >= 38) || IsInRangeOfPoint(player.Position, new Vector3(445.048, -972.2439, 25.788462), 6.75f) || (IsInRangeOfPoint(player.Position, new Vector3(569.6448, 10.3835535, 103.22986), 37.75f) && player.Position.Z >= 102) || IsInRangeOfPoint(player.Position, new Vector3(608.4999, -2.2341008, 70.62814), 7.75f))
                {
                    if (character.faction == 1)
                    {
                        List<CenterMenu> centerMenu = new List<CenterMenu>();
                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleData != null && car.vehicleData.garage == "faction-" + character.faction)
                            {
                                CenterMenu cMenu = new CenterMenu();
                                cMenu.var1 = "" + car.vehicleData.id;
                                if (car.vehicleData.ownname != "n/A")
                                {
                                    cMenu.var2 = car.vehicleData.ownname;
                                }
                                else
                                {
                                    cMenu.var2 = car.vehicleData.vehiclename;
                                }
                                cMenu.var3 = car.vehicleData.plate;
                                cMenu.var4 = "" + car.vehicleData.id;
                                centerMenu.Add(cMenu);
                            }
                        }
                        String rules = "ID,Fahrzeugname,Nummernschild,Aktion";
                        player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu.OrderBy(x => x.var3.Length).ThenBy(x => x.var3).ToList()), "Fraktionsgarage");
                        return;
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Zeig mir zuerst deinen Dienstausweis!", "error", "top-end");
                    }
                }
                //LSRC
                if ((IsInRangeOfPoint(player.Position, new Vector3(-651.331, 328.43048, 140.14816), 37.75f) && player.Position.Z >= 139) || IsInRangeOfPoint(player.Position, new Vector3(-676.8967, 336.86328, 78.11836), 6.75f))
                {
                    if (character.faction == 2)
                    {
                        List<CenterMenu> centerMenu = new List<CenterMenu>();
                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleData != null && car.vehicleData.garage == "faction-" + character.faction)
                            {
                                CenterMenu cMenu = new CenterMenu();
                                cMenu.var1 = "" + car.vehicleData.id;
                                if (car.vehicleData.ownname != "n/A")
                                {
                                    cMenu.var2 = car.vehicleData.ownname;
                                }
                                else
                                {
                                    cMenu.var2 = car.vehicleData.vehiclename;
                                }
                                cMenu.var3 = car.vehicleData.plate;
                                cMenu.var4 = "" + car.vehicleData.id;
                                centerMenu.Add(cMenu);
                            }
                        }
                        String rules = "ID,Fahrzeugname,Nummernschild,Aktion";
                        player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu.OrderBy(x => x.var3.Length).ThenBy(x => x.var3).ToList()), "Fraktionsgarage");
                        return;
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Zeig mir zuerst deinen Dienstausweis!", "error", "top-end");
                    }
                }
                //LSC
                if ((IsInRangeOfPoint(player.Position, new Vector3(-333.11682, -146.52608, 60.445873), 37.75f) && player.Position.Z >= 59) || IsInRangeOfPoint(player.Position, new Vector3(-354.90842, -166.38257, 39.015373), 6.75f))
                {
                    if (character.faction == 3)
                    {
                        List<CenterMenu> centerMenu = new List<CenterMenu>();
                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleData != null && car.vehicleData.garage == "faction-" + character.faction)
                            {
                                CenterMenu cMenu = new CenterMenu();
                                cMenu.var1 = "" + car.vehicleData.id;
                                if (car.vehicleData.ownname != "n/A")
                                {
                                    cMenu.var2 = car.vehicleData.ownname;
                                }
                                else
                                {
                                    cMenu.var2 = car.vehicleData.vehiclename;
                                }
                                cMenu.var3 = car.vehicleData.plate;
                                cMenu.var4 = "" + car.vehicleData.id;
                                centerMenu.Add(cMenu);
                            }
                        }
                        String rules = "ID,Fahrzeugname,Nummernschild,Aktion";
                        player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu.OrderBy(x => x.var3.Length).ThenBy(x => x.var3).ToList()), "Fraktionsgarage");
                        return;
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Zeig mir zuerst deinen Dienstausweis!", "error", "top-end");
                    }
                }
                //GOV
                if (IsInRangeOfPoint(player.Position, new Vector3(-506.12866, -199.77043, 34.215195), 18.65f))
                {
                    if (character.faction == 4)
                    {
                        List<CenterMenu> centerMenu = new List<CenterMenu>();
                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleData != null && car.vehicleData.garage == "faction-" + character.faction)
                            {
                                CenterMenu cMenu = new CenterMenu();
                                cMenu.var1 = "" + car.vehicleData.id;
                                if (car.vehicleData.ownname != "n/A")
                                {
                                    cMenu.var2 = car.vehicleData.ownname;
                                }
                                else
                                {
                                    cMenu.var2 = car.vehicleData.vehiclename;
                                }
                                cMenu.var3 = car.vehicleData.plate;
                                cMenu.var4 = "" + car.vehicleData.id;
                                centerMenu.Add(cMenu);
                            }
                        }
                        String rules = "ID,Fahrzeugname,Nummernschild,Aktion";
                        player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu.OrderBy(x => x.var3.Length).ThenBy(x => x.var3).ToList()), "Fraktionsgarage");
                        return;
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, "Zeig mir zuerst deinen Dienstausweis!", "error", "top-end");
                    }
                }
                //Haus + Business Garagen
                House house = House.GetClosestHouse(player, 10.15f);
                Business bizz = Business.GetClosestBusiness(player, IsAtGarage(player));
                if (house != null)
                {
                    if (house.locked == 0)
                    {
                        List<CenterMenu> centerMenu = new List<CenterMenu>();
                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleData.garage == "house-" + house.id)
                            {
                                CenterMenu cMenu = new CenterMenu();
                                cMenu.var1 = "" + car.vehicleData.id;
                                if (car.vehicleData.ownname != "n/A")
                                {
                                    cMenu.var2 = car.vehicleData.ownname;
                                }
                                else
                                {
                                    cMenu.var2 = car.vehicleData.vehiclename;
                                }
                                cMenu.var3 = car.vehicleData.plate;
                                cMenu.var4 = "" + car.vehicleData.id;
                                centerMenu.Add(cMenu);
                            }
                        }
                        String rules = "ID,Fahrzeugname,Nummernschild,Aktion";
                        player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu), "Hausgarage");
                        return;
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Die Garage ist verschlossen!", "error");
                        return;
                    }
                }
                if (bizz != null)
                {
                    List<CenterMenu> centerMenu = new List<CenterMenu>();
                    foreach (Cars car in Cars.carList)
                    {
                        if (car.vehicleData.garage == "bizz-" + bizz.id)
                        {
                            foreach (Items iteminlist in tempData.itemlist)
                            {
                                if (iteminlist != null && iteminlist.props == $"{car.vehicleData.vehiclename}: {car.vehicleData.id}")
                                {
                                    CenterMenu cMenu = new CenterMenu();
                                    cMenu.var1 = "" + car.vehicleData.id;
                                    if (car.vehicleData.ownname != "n/A")
                                    {
                                        cMenu.var2 = car.vehicleData.ownname;
                                    }
                                    else
                                    {
                                        cMenu.var2 = car.vehicleData.vehiclename;
                                    }
                                    cMenu.var3 = car.vehicleData.plate;
                                    cMenu.var4 = "" + car.vehicleData.id;
                                    centerMenu.Add(cMenu);
                                    break;
                                }
                            }
                        }
                    }
                    String rules = "ID,Fahrzeugname,Nummernschild,Aktion";
                    string name = $"{bizz.name} - {Convert.ToInt32(315 * bizz.multiplier)}$ pro Payday Garagenkosten!";
                    player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenu), bizz.name);
                    return;
                }
                //Secruity
                if (IsASecruity(player) > -1 && player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade")
                {
                    if (!player.Vehicle.HasData("Vehicle:Money") && !player.HasData("Player:Money"))
                    {
                        if (tempData.furniturePosition != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen ATM Auftrag abbrechen!", "error");
                            return;
                        }
                        List<BizzSecruityList> bizzList = new List<BizzSecruityList>();
                        int count = 0;
                        foreach (Business bizzInList in Business.businessList)
                        {
                            if (bizzInList.getmoney > 0 && bizzInList.cash >= bizzInList.getmoney)
                            {
                                BizzSecruityList bizsz = new BizzSecruityList();
                                bizsz.id = bizzInList.id;
                                bizsz.name = bizzInList.name;
                                bizsz.bizz = bizzInList.id;
                                bizsz.money = bizzInList.cash;
                                bizsz.isbizz = true;
                                bizzList.Add(bizsz);
                                count++;
                                if (count >= 12) break;
                            }
                        }
                        if (count > 0)
                        {
                            player.TriggerEvent("Client:ShowSpedition", 6, NAPI.Util.ToJson(bizzList));
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Keine Aufträge vorhanden!", "error");
                        }
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst das Geld wieder ausladen/den Geldkoffer zurückbringen!", "error");
                    }
                    return;
                }
                //Kanalreiniger
                if (character.job == 5 && player.IsInVehicle && player.Vehicle == tempData.jobVehicle)
                {
                    if (player.HasData("Player:WireCooldown"))
                    {
                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:WireCooldown"))
                        {
                            SendNotificationWithoutButton(player, "Du kannst nur alle 45 Sekunden dir eine zu säuberende Kanalisation anzeigen lassen!", "error");
                            return;
                        }
                        player.ResetData("Player:Wirecooldown");
                    }
                    player.SetData<int>("Player:WireCooldown", Helper.UnixTimestamp() + (45));
                    Vector3[] kanalPosi = new Vector3[6]
                    {
                       new Vector3(227.957, -1242.5942, 29.283136-0.025),
                       new Vector3(-263.94232, -1144.8015, 23.049805-0.025),
                       new Vector3(-342.4834, -1819.7576, 23.450586-0.025),
                       new Vector3(932.3689, -1763.4984, 31.214033-0.025),
                       new Vector3(779.2169, -594.0568, 29.78341-0.025),
                       new Vector3(-89.61843, 63.91574, 71.52714-0.025)
                    };

                    if (tempData.jobVehicle2 == null)
                    {
                        Random rand = new Random();
                        int index = rand.Next(6);
                        player.TriggerEvent("Client:ShowCleaner", kanalPosi[index].X, kanalPosi[index].Y, kanalPosi[index].Z);
                    }
                    return;
                }
                //Landwirt
                if (character.job == 7 && tempData.jobduty == true && player.IsInVehicle && player.Vehicle == tempData.jobVehicle)
                {
                    if (!player.HasData("Player:FarmerRoute"))
                    {
                        if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                        {
                            SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                            return;
                        }
                        player.SetData<int>("Player:FarmerRoute", 1);
                        player.TriggerEvent("Client:StartFarmerWheat");
                    }
                    else
                    {
                        if (player.GetData<int>("Player:FarmerRoute") == 1)
                        {
                            player.ResetData("Player:FarmerRoute");
                            player.TriggerEvent("Client:StopFarmer");
                            SendNotificationWithoutButton(player, "Landwirtschafts Aktion beendet!", "success");
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deine Landwirtschaft Aktion beenden!", "error");
                        }
                    }
                    return;
                }
                //Taxidriver
                if (IsATaxiDriver(player) > -1 && player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                {
                    if (player.HasData("Player:Fare"))
                    {
                        player.ResetData("Player:Fare");
                        player.ResetData("Player:Taxameter");
                        player.ResetData("Player:Taxakilometer");
                        player.ResetData("Player:TaxameterOn");
                        SendNotificationWithoutButton(player, "Dienstfahrt beendet!", "success");
                        NAPI.Task.Run(() =>
                        {
                            if (Helper.IsATaxiDriver(player) == 2)
                            {
                                if (player.HasData("Player:TaxameterOn"))
                                {
                                    player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: 0$ | ~y~Taxameter: 0$");
                                }
                                else
                                {
                                    player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~Yellow Cab Co.\n~y~Dienstpreis: 0$");
                                }
                            }
                            else
                            {
                                if (player.HasData("Player:TaxameterOn"))
                                {
                                    player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: 0$ | ~y~Taxameter: 0$");
                                }
                                else
                                {
                                    player.Vehicle.SetSharedData("Vehicle:Text3D", $"~w~{GroupsController.GetGroupNameById(character.mygroup)}\n~y~Dienstpreis: 0$");
                                }
                            }
                            if (player.Vehicle.HasSharedData("Vehicle:Text3D"))
                            {
                                player.Vehicle.ResetSharedData("Vehicle:Text3D");
                            }
                        }, delayTime: 225);
                    }
                    else
                    {
                        player.TriggerEvent("Client:CallInput", "Dienstpreis einstellen", $"Wie hoch soll dein Dienstpreis pro Kilometer sein?", "text", "4", 4, "SetDienstpreis");
                    }
                    return;
                }
                //Sweeper
                if (character.job == 4 && player.IsInVehicle && player.Vehicle == tempData.jobVehicle && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "sweeper")
                {
                    if (player.HasData("Player:WireCooldown"))
                    {
                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:WireCooldown"))
                        {
                            SendNotificationWithoutButton(player, "Du kannst nur alle 10 Sekunden dir eine Müllposition anzeigen lassen!", "error");
                            return;
                        }
                        player.ResetData("Player:Wirecooldown");
                    }
                    player.SetData<int>("Player:WireCooldown", Helper.UnixTimestamp() + (10));
                    double dist = 125f;
                    double newDist = 125f;
                    Vector3 position = null;
                    foreach (Garbage garbage in Helper.garbageList)
                    {
                        if (garbage.created == true)
                        {
                            if (garbage.position.DistanceTo(player.Position) < dist)
                            {
                                newDist = garbage.position.DistanceTo(player.Position);
                                position = garbage.position;
                            }
                        }
                    }
                    if (position != null)
                    {
                        player.TriggerEvent("Client:CreateWaypoint2", position.X, position.Y, position.Z, "Müllposition");
                        SendNotificationWithoutButton(player, "Müllposition auf der Karte markiert!", "success");
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Kein Müll in der Nähe vorhanden!", "error");
                    }
                    return;
                }
                //Müllmann
                if (character.job == 4 && player.IsInVehicle && player.Vehicle == tempData.jobVehicle && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "trash")
                {
                    Helper.ShowPreShop(player, "Müllroutenauswahl", 0, 1, 1);
                    return;
                }
                //Busfahrer
                if (IsABusDriver(player) != -1 && player.IsInVehicle && (player.Vehicle == tempData.jobVehicle || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "bus" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "coach" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "rentalbus" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                {
                    Helper.ShowPreShop(player, "Routenauswahl", 0, 1, 1);
                    return;
                }
                //Fishing
                if (!player.IsInVehicle && player.HasData("Player:FishingRod"))
                {
                    Items bait = ItemsController.GetItemByItemName(player, "Köder");
                    if (bait == null)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du hast keine Köder dabei!", "error");
                        return;
                    }
                    if (!ItemsController.CanPlayerHoldItem(player, 275))
                    {
                        Helper.SendNotificationWithoutButton(player, "Dein Inventar ist (fast) voll!", "error", "top-end");
                        return;
                    }
                    Random rand = new Random();
                    int waitTime = rand.Next(8, 15);
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    player.TriggerEvent("Client:StartLockpicking", waitTime, "fishing", "Angel wird ausgeworfen...");
                    return;
                }
                //Spediteur Auftragsliste
                int checkspedi = IsASpediteur(player);
                if (checkspedi > -1 && player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    if (player.Vehicle.GetSharedData<String>("Vehicle:Name").Contains("rcbandito")) return;
                    if (checkspedi == 2)
                    {
                        if (player.IsInVehicle && tempData.jobVehicle != null && player.Vehicle == tempData.jobVehicle)
                        {
                            if (tempData.order != null)
                            {
                                SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen Auftrag beenden oder /abort benutzen!", "error");
                                return;
                            }
                            if (SpedOrders.spedOrderList == null || SpedOrders.spedOrderList.Count <= 0)
                            {
                                SendNotificationWithoutButton(player, "Aktuell sind keine Aufträge verfügbar!", "error");
                                return;
                            }
                            if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                            {
                                SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                                return;
                            }
                            player.TriggerEvent("Client:ShowSpedition", 3, NAPI.Util.ToJson(SpedOrders.spedOrderList));
                        }
                    }
                    else
                    {
                        if (tempData.order != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen Auftrag beenden oder /abort benutzen!", "error");
                            return;
                        }
                        if (!IsASpeditionsVehicle(player.Vehicle))
                        {
                            SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Aufträge erledigen!", "error");
                            return;
                        }
                        SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                        if (spedVehicles == null)
                        {
                            SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Aufträge erledigen!", "error");
                            return;
                        }
                        if (SpedOrders.spedOrderList == null || SpedOrders.spedOrderList.Count <= 0)
                        {
                            SendNotificationWithoutButton(player, "Aktuell sind keine Aufträge zu erledigen!", "error");
                            return;
                        }
                        if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                        {
                            SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                            return;
                        }
                        player.TriggerEvent("Client:ShowSpedition", 3, NAPI.Util.ToJson(SpedOrders.spedOrderList));
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF4]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF3")]
        public static void OnPlayerPressF3(Player player)
        {
            try
            {
                //Accountdata + Character Data
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;
                //Landwirt
                if (character.job == 7 && tempData.jobduty == true && !player.IsInVehicle && IsInRangeOfPoint(player.Position, new Vector3(2196.4106, 4968.656, 41.37355), 525f))
                {
                    int skill = character.farmingskill / 25;
                    if (skill < 2)
                    {
                        SendNotificationWithoutButton(player, "Du benötigst mind. Landwirtskill 2!", "error");
                        return;
                    }
                    if (!player.HasData("Player:FarmerRoute"))
                    {
                        player.SetData<int>("Player:FarmerRoute", 2);
                        player.TriggerEvent("Client:StartFarmerCow");
                    }
                    else
                    {
                        if (player.GetData<int>("Player:FarmerRoute") == 2)
                        {
                            player.ResetData("Player:FarmerRoute");
                            player.TriggerEvent("Client:StopFarmer");
                            SendNotificationWithoutButton(player, "Landwirtschafts Aktion beendet!", "success");
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deine Landwirtschaft Aktion beenden!", "error");
                        }
                    }
                    return;
                }
                //Secruity
                if (IsASecruity(player) > -1 && player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver && player.Vehicle.GetSharedData<String>("Vehicle:Name") == "stockade")
                {
                    if (player.Vehicle.HasData("Vehicle:Money"))
                    {
                        if (tempData.furniturePosition != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen ATM Auftrag abbrechen!", "error");
                            return;
                        }
                        List<ATMSpots> atmSpotTempList = new List<ATMSpots>();
                        foreach (ATMSpots atmSpots in Helper.atmSpotList.ToList())
                        {
                            if (atmSpots.value < 50000)
                            {
                                atmSpots.dist = (int)player.Position.DistanceTo(atmSpots.position);
                                atmSpotTempList.Add(atmSpots);
                            }
                        }
                        if (atmSpotTempList.Count > 0)
                        {
                            player.TriggerEvent("Client:ShowSpedition", 5, NAPI.Util.ToJson(atmSpotTempList));
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Keine Aufträge vorhanden!", "error");
                        }
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst Geld einladen, der Transporter ist leer!", "error");
                    }
                    return;
                }
                //Taxidriver
                if (IsATaxiDriver(player) > -1 && player.IsInVehicle && (player.Vehicle.GetSharedData<String>("Vehicle:Name") == "taxi" || player.Vehicle.GetSharedData<String>("Vehicle:Name") == "tourbus"))
                {
                    if (player.HasData("Player:Fare"))
                    {
                        if (tempData.order2 != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen Taxiauftrag beenden oder /abort benutzen!", "error");
                            return;
                        }
                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                        {
                            if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetOwnSharedData<bool>("Player:Death") == false && p == player.Vehicle && p.VehicleSeat == 1)
                            {
                                SendNotificationWithoutButton(player, "Der Beifahrersitz muss frei sein!", "error");
                                return;
                            }
                        }
                        List<TaxiBots> taxiBotListTemp = new List<TaxiBots>();
                        foreach (TaxiBots taxiBot in Helper.taxiBotList)
                        {
                            TaxiBots nTaxiBot = new TaxiBots();
                            nTaxiBot.id = taxiBot.id;
                            nTaxiBot.from = taxiBot.from;
                            nTaxiBot.to = taxiBot.to;
                            nTaxiBot.money = taxiBot.money;
                            nTaxiBot.v1 = null;
                            nTaxiBot.v2 = null;
                            taxiBotListTemp.Add(nTaxiBot);
                        }
                        player.TriggerEvent("Client:ShowSpedition", 4, NAPI.Util.ToJson(taxiBotListTemp));
                    }
                    return;
                }
                //Spediteur
                int checkspedi = IsASpediteur(player);
                if (checkspedi > -1 && player.IsInVehicle)
                {
                    //Produkte kaufen
                    if (IsInRangeOfPoint(player.Position, new Vector3(-1154.1656, -2173.3665, 12.749134), 3.55f))
                    {
                        if (tempData.order != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen Auftrag beenden oder /abort benutzen!", "error");
                            return;
                        }
                        if (!IsASpeditionsVehicle(player.Vehicle))
                        {
                            SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                            return;
                        }
                        if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                        {
                            SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                            return;
                        }
                        if (checkspedi == 1)
                        {
                            if (!player.Vehicle.HasData("Vehicle:Products"))
                            {
                                player.Vehicle.SetData<int>("Vehicle:Products", 0);
                            }
                            SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                            if (spedVehicles == null)
                            {
                                SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                return;
                            }
                            player.TriggerEvent("Client:CallInput", "Produkte kaufen", $"Wieviele Produkte (18$ pro Produkt/e) möchtest du kaufen? Aktuelle Kapazität: {player.Vehicle.GetData<int>("Vehicle:Products")}/{spedVehicles.capa}", "text", "250", 4, "BuyProducts1");
                        }
                        else
                        {
                            if (tempData != null && tempData.jobVehicle != null && tempData.jobVehicle == player.Vehicle)
                            {
                                SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                if (spedVehicles == null)
                                {
                                    SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                    return;
                                }
                                player.TriggerEvent("Client:CallInput", "Produkte einladen", $"Wieviele Produkte möchtest du einladen? Aktuelle Kapazität: {player.Vehicle.GetData<int>("Vehicle:Products")}/{spedVehicles.capa}", "text", "250", 4, "BuyProducts2");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Du sitzt in keinem Jobfahrzeug!", "error");
                            }
                        }
                        return;
                    }
                    //Produkte verkaufen
                    if (!IsInRangeOfPoint(player.Position, new Vector3(-1154.1656, -2173.3665, 12.749134), 4.55f) && player.Vehicle.HasData("Vehicle:Products"))
                    {
                        if (tempData.order != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen Auftrag beenden oder /abort benutzen!", "error");
                            return;
                        }
                        foreach (Business bizz in Business.businessList)
                        {
                            if (bizz == null) continue;
                            //Produkte
                            if (player.Position.DistanceTo(bizz.position) <= 3.75f && player.Dimension == 0)
                            {
                                if (bizz.buyproducts == 0)
                                {
                                    if (checkspedi == 2 && bizz.owner != "n/A" && Business.HasPlayerBusinessKey(player, bizz.id))
                                    {
                                        SendNotificationWithoutButton(player, "Du kannst dieses Business nur als Firma beliefern!", "error");
                                        return;
                                    }
                                    if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                                    {
                                        SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                                        return;
                                    }
                                    SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                    if (spedVehicles == null)
                                    {
                                        SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                        return;
                                    }
                                    if (checkspedi == 1)
                                    {
                                        player.TriggerEvent("Client:CallInput", "Produkte verkaufen", $"Wieviele Produkte möchtest du verkaufen? Benötigte/Vorhandene Produkte: {2000 - bizz.products}/{player.Vehicle.GetData<int>("Vehicle:Products")}", "text", "250", 4, "SellProducts1");
                                    }
                                    else
                                    {
                                        player.TriggerEvent("Client:CallInput", "Produkte verkaufen", $"Wieviele Produkte (Kaufpreis: {bizz.prodprice}$/Produkt) möchtest du verkaufen? Benötigte/Vorhandene Produkte: {2000 - bizz.products}/{player.Vehicle.GetData<int>("Vehicle:Products")}", "text", "250", 4, "SellProducts2");
                                    }
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Dieses Business kauft aktuell keine Produkte!", "error");
                                }
                                return;
                            }
                        }
                        //Tuningteile
                        foreach (House house in House.houseList)
                        {
                            if (house == null) continue;
                            if (player.Position.DistanceTo(house.position) <= 3.75f && player.Dimension == 0 && house.housegroup != -1)
                            {
                                if (house.stockprice > 0)
                                {
                                    if (checkspedi == 2 && house.owner != "n/A" && House.HasPlayerHouseKey(player, house.id))
                                    {
                                        SendNotificationWithoutButton(player, "Du kannst diese Gruppierung/Fraktion nur als Firma beliefern!", "error");
                                        return;
                                    }
                                    if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                                    {
                                        SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                                        return;
                                    }
                                    SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                                    if (spedVehicles == null)
                                    {
                                        SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                        return;
                                    }
                                    if (checkspedi == 1)
                                    {
                                        player.TriggerEvent("Client:CallInput", "Produkte verkaufen", $"Wieviele Produkte möchtest du verkaufen? Benötigte/Vorhandene Produkte: {3500 - house.stock}/{player.Vehicle.GetData<int>("Vehicle:Products")}", "text", "250", 4, "SellProducts1");
                                    }
                                    else
                                    {
                                        player.TriggerEvent("Client:CallInput", "Produkte verkaufen", $"Wieviele Produkte (Kaufpreis: {house.stockprice}$/Produkt) möchtest du verkaufen? Benötigte/Vorhandene Produkte: {3500 - house.stock}/{player.Vehicle.GetData<int>("Vehicle:Products")}", "text", "250", 4, "SellProducts2");
                                    }
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Diese Gruppierung kauft aktuell keine Produkte!", "error");
                                }
                                return;
                            }
                        }
                    }
                    //Business Belieferungsübersicht
                    if (!IsInRangeOfPoint(player.Position, new Vector3(-1154.1656, -2173.3665, 12.749134), 3.55f))
                    {
                        if (tempData.order != null)
                        {
                            SendNotificationWithoutButton(player, "Du musst zuerst deinen aktuellen Auftrag beenden oder /abort benutzen!", "error");
                            return;
                        }
                        List<BusinessLoad> tempBizzList = Business.GetBusinessWithNeeds();
                        if (checkspedi == 2)
                        {
                            if (player.IsInVehicle && tempData.jobVehicle != null && player.Vehicle == tempData.jobVehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                            {
                                if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                                {
                                    SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                                    return;
                                }
                                if (tempBizzList == null || tempBizzList.Count <= 0)
                                {
                                    SendNotificationWithoutButton(player, "Aktuell müssen keine Businesse beliefert werden!", "error");
                                    return;
                                }
                                player.TriggerEvent("Client:ShowSpedition", 2, NAPI.Util.ToJson(Business.GetBusinessWithNeeds()));
                                return;
                            }
                        }
                        else if (checkspedi == 1 && player.VehicleSeat == (int)VehicleSeat.Driver)
                        {
                            if (!IsASpeditionsVehicle(player.Vehicle))
                            {
                                SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte verkaufen!", "error");
                                return;
                            }
                            SpedVehicles spedVehicles = GetSpedVehicleById(player.Vehicle.GetData<int>("Vehicle:Jobid"));
                            if (spedVehicles == null)
                            {
                                SendNotificationWithoutButton(player, "Mit diesem Fahrzeug kannst du keine Produkte kaufen/einladen!", "error");
                                return;
                            }
                            if (tempBizzList == null || tempBizzList.Count <= 0)
                            {
                                SendNotificationWithoutButton(player, "Aktuell müssen keine Businesse beliefert werden!", "error");
                                return;
                            }
                            if (IsATruck(player.Vehicle) && !IsTrailerAttached(player))
                            {
                                SendNotificationWithoutButton(player, "Du benötigst einen Anhänger!", "error");
                                return;
                            }
                            player.TriggerEvent("Client:ShowSpedition", 2, NAPI.Util.ToJson(Business.GetBusinessWithNeeds()));
                        }
                    }
                }
                //Busplan
                if (!player.IsInVehicle && player.Dimension == 0)
                {
                    string routeName = "";
                    string stationName = "";
                    string allStations = "";
                    string[] advertArray = new string[30];
                    string[] posArray = new string[4];
                    foreach (BusRoutes busRoutes in busRoutesList)
                    {
                        if (busRoutes.name.Length > 0)
                        {
                            if (busRoutes.routes.Length > 0)
                            {
                                advertArray = busRoutes.advert.Split("|");
                                for (int i = 0; i < advertArray.Length; i++)
                                {
                                    posArray = advertArray[i].Split(",");
                                    if (player.Position.DistanceTo(new Vector3(float.Parse(posArray[0], new CultureInfo("en-US")), float.Parse(posArray[1], new CultureInfo("en-US")), float.Parse(posArray[2], new CultureInfo("en-US")))) <= 2.25)
                                    {
                                        stationName = posArray[3];
                                        routeName = busRoutes.name;
                                        break;
                                    }
                                }
                                if (routeName != "")
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if (routeName != "")
                    {
                        foreach (BusRoutes busRoutes in busRoutesList)
                        {
                            if (busRoutes.name == routeName)
                            {
                                advertArray = busRoutes.advert.Split("|");
                                for (int i = 0; i < advertArray.Length; i++)
                                {
                                    posArray = advertArray[i].Split(",");
                                    allStations += $"{posArray[3]},";
                                }
                            }
                        }
                    }
                    if (allStations.Length > 0)
                    {
                        allStations = allStations.Substring(0, allStations.Length - 1);
                        int busDriver = 0;
                        foreach (Player p in NAPI.Pools.GetAllPlayers())
                        {
                            if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetOwnSharedData<bool>("Player:Death") == false && p.GetData<String>("Player:BusRoute") == routeName)
                            {
                                busDriver++;
                            }
                        }
                        player.TriggerEvent("Player:ShowBusPlan", routeName, stationName, allStations, busDriver);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF3]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressE")]
        public static void OnPlayerPressE(Player player)
        {
            try
            {
                //Accountdata + Character Data
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;
                //Filmkamera
                if (player.HasData("Player:Filmkamera"))
                {
                    player.TriggerEvent("Client:ToggleFilmCamera", player.Id, 1);
                    return;
                }
                //LSPD + LSRC
                if ((IsInRangeOfPoint(player.Position, new Vector3(441.58102, -985.00275, 30.68931), 1.75f) || IsInRangeOfPoint(player.Position, new Vector3(631.1063, 7.9442596, 82.62809), 1.75f) || IsInRangeOfPoint(player.Position, new Vector3(-676.01, 325.56766, 83.08313), 1.75f)) && player.Dimension == 0)
                {
                    if (MDCController.klingelCooldown > 0)
                    {
                        if (Helper.UnixTimestamp() < MDCController.klingelCooldown)
                        {
                            Helper.SendNotificationWithoutButton(player, "Die Klingel kann nur alle 5 Minuten benutzt werden!", "error", "top-end");
                            return;
                        }
                    }
                    MDCController.klingelCooldown = Helper.UnixTimestamp() + (300);
                    if (IsInRangeOfPoint(player.Position, new Vector3(441.58102, -985.00275, 30.68931), 1.75f))
                    {
                        MDCController.SendPoliceMDCMessage(player, $"Jemand hat die Klingel im LSPD Eingangsbereich (Zweigstelle) betätigt!");
                    }
                    else if (IsInRangeOfPoint(player.Position, new Vector3(631.1063, 7.9442596, 82.62809), 1.75f))
                    {
                        MDCController.SendPoliceMDCMessage(player, $"Jemand hat die Klingel im LSPD Eingangsbereich betätigt!");
                    }
                    else if (IsInRangeOfPoint(player.Position, new Vector3(-676.01, 325.56766, 83.08313), 1.75f))
                    {
                        MDCController.SendMedicMDCMessage(player, $"Jemand hat die Klingel im LSRC Eingangsbereich betätigt!");
                    }
                    Commands.cmd_animation(player, "give", true);
                    Helper.SendNotificationWithoutButton(player, $"Du hast die Klingel betätigt!", "success", "top-left", 1250);
                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                    {
                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetOwnSharedData<bool>("Player:Death") == false && p.Position.DistanceTo(player.Position) <= 25.5 && !p.IsInVehicle)
                        {
                            p.TriggerEvent("Client:PlaySound", "klingel.wav", 0);
                        }
                    }
                }
                //Big. D
                if (GangController.dealerPosition2 != null && Helper.IsInRangeOfPoint(player.Position, GangController.dealerPosition2, 2.75f) && player.Dimension == 0)
                {
                    if (character.factionduty == true)
                    {
                        SendNotificationWithoutButton(player, "Ich chille hier nur!", "error", "top-end", 2250);
                        return;
                    }
                    ShowDealerShop2(player, 0);
                    return;
                }
                //A. der Waffendealer
                if (GangController.dealerPosition != null && Helper.IsInRangeOfPoint(player.Position, GangController.dealerPosition, 2.75f) && player.Dimension == 0)
                {
                    if (character.factionduty == true)
                    {
                        SendNotificationWithoutButton(player, "Ich verkaufe nichts!", "error", "top-end", 2250);
                        return;
                    }
                    ShowDealerShop(player, 0);
                    return;
                }
                //Garbage
                if (character.job == 4 && player.HasData("Player:Garbage"))
                {
                    Vector3 behindPlayer = Helper.GetPositionBehindOfVehicle(tempData.jobVehicle, 6.25f);
                    if (player.Position.DistanceTo(behindPlayer) <= 1.75)
                    {
                        Helper.PlayShortAnimation(player, "anim@heists@narcotics@trash", "throw_b", 1650);
                        NAPI.Task.Run(() =>
                        {
                            Helper.SendNotificationWithoutButton(player, $"Müllbeutel entsorgt!", "success", "top-left", 1250);
                            if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                            {
                                Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                                Helper.SendNotificationWithoutButton(garbagePlayer, $"Müllbeutel wurde entsorgt!", "success", "top-left", 1250);
                            }
                            Helper.IsNextGarbageStation2(player);
                            player.ResetData("Player:Garbage");
                            Helper.AddRemoveAttachments(player, "garbageBag", false);
                        }, delayTime: 1033);
                    }
                    return;
                }
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(243.1547, 224.7177, 106.2868), 3.5f) && Helper.IsASecruity(player) != -1 && player.Vehicle == null)
                {
                    if (player.HasData("Player:MoneyInfos"))
                    {
                        Helper.PlayShortAnimation(player, "mp_common", "givetake1_a", 1850);
                        NAPI.Task.Run(() =>
                        {
                            string[] moneyArray = new string[3];
                            moneyArray = player.GetData<string>("Player:MoneyInfos").Split(",");
                            if (moneyArray[0].Length > 0 && moneyArray[1].Length > 3)
                            {
                                Bank bank = BankController.GetBankByBankNumber(moneyArray[1]);
                                if (bank != null)
                                {
                                    Random random = new Random();
                                    int percent = 515 + random.Next(0, 85) + (player.GetData<int>("Player:Money") / 100 * 2);
                                    SmartphoneController.OnSmartphoneMessage(player, Helper.UnixTimestamp(), moneyArray[0], "01897337", $"{character.name} hat den Geldtransport in Höhe von {player.GetData<int>("Player:Money")}$ - {percent}$ Provision abgeschlossen!");
                                    Helper.SendNotificationWithoutButton(player, $"Danke, wir zahlen das Geld ({player.GetData<int>("Player:Money")}$) jetzt ein!", "success", "top-left", 4350);
                                    bank.bankvalue += player.GetData<int>("Player:Money") - percent;
                                    Helper.BankSettings(moneyArray[1], "Geldtransport", "" + (player.GetData<int>("Player:Money") - percent), character.name);
                                    player.TriggerEvent("Client:PlaySoundPeep2");
                                    if (character.mygroup != -1)
                                    {
                                        Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                        bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                        if (bank != null)
                                        {
                                            int prov = 0;
                                            if (mygroup.provision > 0)
                                            {
                                                prov = percent / 100 * mygroup.provision;
                                            }
                                            if (prov > 0 && character.defaultbank != "n/A")
                                            {
                                                Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                                bank.bankvalue += percent;
                                                bank.bankvalue -= prov;
                                                if (bank2 != null)
                                                {
                                                    bank2.bankvalue += prov;
                                                }
                                                Helper.SendNotificationWithoutButton(player, $"{percent}$ werden dem Konto deiner Firma gutgeschrieben. Du erhälst {prov}$ Provision!", "success", "top-left", 5500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Bankautomaten aufgefüllt und {percent - prov}$ erwirtschaftet!");
                                            }
                                            else
                                            {
                                                bank.bankvalue += percent;
                                                Helper.SendNotificationWithoutButton(player, $"{percent}$ werden dem Konto deiner Firma gutgeschrieben!", "success", "top-left", 5500);
                                                Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Bankautomaten aufgefüllt und {percent}$ erwirtschaftet!");
                                            }
                                        }
                                        else
                                        {
                                            character.nextpayday += percent;
                                            Helper.SendNotificationWithoutButton(player, $"Du bekommst für deinen nächsten Gehaltscheck {percent}$ gutgeschrieben!", "success", "top-left", 5500);
                                        }
                                    }
                                    else
                                    {
                                        character.nextpayday += percent;
                                        Helper.SendNotificationWithoutButton(player, $"Du bekommst für deinen nächsten Gehaltscheck {percent}$ gutgeschrieben!", "success", "top-left", 5500);
                                    }

                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Das Geld konnte nicht eingezahlt werden!", "error", "top-left", 4500);
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Das Geld konnte nicht eingezahlt werden!", "error", "top-left", 4500);
                            }
                            player.ResetData("Player:Money");
                            player.ResetData("Player:MoneyInfos");
                            Helper.AddRemoveAttachments(player, "moneyBag", false);
                        }, delayTime: 1550);
                        return;
                    }
                    else
                    {
                        if (player.HasData("Player:Money"))
                        {
                            Helper.PlayShortAnimation(player, "mp_common", "givetake1_a", 1850);
                            NAPI.Task.Run(() =>
                            {
                                Helper.SendNotificationWithoutButton(player, $"Geldkoffer zurückgebracht!", "success", "top-left", 1250);
                                player.ResetData("Player:Money");
                                Helper.AddRemoveAttachments(player, "moneyBag", false);
                            }, delayTime: 1550);
                        }
                        else
                        {
                            player.SetData<int>("Player:Money", 200000);
                            Helper.PlayShortAnimation(player, "mp_common", "givetake1_a", 1850);
                            NAPI.Task.Run(() =>
                            {
                                Helper.AddRemoveAttachments(player, "moneyBag", true);
                                Helper.SendNotificationWithoutButton(player, $"Geldkoffer (200.000$) entgegen genommen, mit der Taste [E] kannst du diesen in deinen Transporter legen!", "success", "top-left", 3550);
                                NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                            }, delayTime: 1550);
                        }
                    }
                }
                if (Helper.IsASecruity(player) != -1 && !player.IsInVehicle)
                {
                    Vehicle veh = Helper.GetClosestVehicle(player, 3.95f);
                    if (veh != null && veh.GetSharedData<string>("Vehicle:Name").ToLower() == "stockade")
                    {
                        if (veh.HasData("Vehicle:Money") && player.HasData("Player:Money"))
                        {
                            Helper.SendNotificationWithoutButton(player, $"Im Transporter liegt schon ein Geldkoffer!", "error", "top-left", 1250);
                            return;
                        }
                        if (player.HasData("Player:Money"))
                        {
                            Helper.PlayShortAnimation(player, "mp_common", "givetake1_a", 1850);
                            NAPI.Task.Run(() =>
                            {
                                if (player.HasData("Player:MoneyInfos"))
                                {
                                    veh.SetData<string>("Vehicle:MoneyInfos", player.GetData<string>("Player:MoneyInfos"));
                                    player.ResetData("Player:MoneyInfos");
                                }
                                veh.SetData<int>("Vehicle:Money", player.GetData<int>("Player:Money"));
                                player.ResetData("Player:Money");
                                Helper.AddRemoveAttachments(player, "moneyBag", false);
                                Helper.SendNotificationWithoutButton(player, $"Geldkoffer in den Geldtransporter gelegt!", "success", "top-left", 1250);
                            }, delayTime: 1550);
                        }
                        else
                        {
                            if (veh.HasData("Vehicle:Money"))
                            {
                                Helper.PlayShortAnimation(player, "mp_common", "givetake1_a", 1850);
                                NAPI.Task.Run(() =>
                                {
                                    if (veh.HasData("Vehicle:MoneyInfos"))
                                    {
                                        player.SetData<string>("Player:MoneyInfos", veh.GetData<string>("Vehicle:MoneyInfos"));
                                        veh.ResetData("Vehicle:MoneyInfos");
                                    }
                                    player.SetData<int>("Player:Money", veh.GetData<int>("Vehicle:Money"));
                                    veh.ResetData("Vehicle:Money");
                                    Helper.AddRemoveAttachments(player, "moneyBag", true);
                                    Helper.SendNotificationWithoutButton(player, $"Geldkoffer aus dem Geldtransporter genommen!", "success", "top-left", 1250);
                                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                                }, delayTime: 1550);
                            }
                        }
                    }
                }
                if (Helper.IsASecruity(player) != -1 && player.Vehicle == null && player.HasData("Player:Money") && tempData.furniturePosition != null && Helper.IsInRangeOfPoint(player.Position, tempData.furniturePosition, 4.15f))
                {
                    if (Helper.IsInRangeOfPoint(player.Position, tempData.furniturePosition, 0.45f))
                    {
                        Helper.SendNotificationWithoutButton(player, $"Du stehst zu nah am Bankautomaten!", "error", "top-left", 1250);
                        return;
                    }
                    if (player.GetData<int>("Player:Money") <= 0)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Der Geldkoffer ist leer!", "error", "top-left", 1250);
                        return;
                    }
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    player.TriggerEvent("Client:StartLockpicking", 7, "fillatm", "Bankautomat wird aufgefüllt...");
                }
                //Secruity
                if (Helper.IsASecruity(player) != -1 && player.Vehicle == null && !player.HasData("Player:Money") && !player.HasData("Player:MoneyInfos"))
                {
                    Business bizz = Business.GetClosestBusiness(player);
                    if (bizz != null)
                    {
                        if (bizz.getmoney > 0 && bizz.cash > 0 && bizz.cash >= bizz.getmoney)
                        {
                            Helper.PlayShortAnimation(player, "mp_common", "givetake1_a", 1850);
                            NAPI.Task.Run(() =>
                            {
                                string phone = "";
                                string bank = "";

                                MySqlCommand command = General.Connection.CreateCommand();
                                command.CommandText = "SELECT lastsmartphone, defaultbank FROM characters WHERE name = @name LIMIT 1";
                                command.Parameters.AddWithValue("@name", bizz.owner);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        reader.Read();
                                        phone = reader.GetString("lastsmartphone");
                                        bank = reader.GetString("defaultbank");
                                    }
                                    reader.Close();
                                }

                                if (phone.Length > 0 && bank.Length > 3)
                                {
                                    SmartphoneController.OnSmartphoneMessage(player, Helper.UnixTimestamp(), phone, "01897337", $"{character.name} hat mit dem Geldtransport von {bizz.cash}$ des Businesses {bizz.name} begonnen!");
                                    player.SetData<int>("Player:Money", bizz.cash);
                                    player.SetData<string>("Player:MoneyInfos", $"{phone},{bank},{bizz.id}");
                                    Helper.AddRemoveAttachments(player, "moneyBag", true);
                                    Business.SaveBusiness(bizz);
                                    player.TriggerEvent("Client:CreateWaypoint", 234.8648, 217.0689);
                                    Helper.SendNotificationWithoutButton(player, $"Du hast {bizz.cash}$ aus dem Business {bizz.name} genommen, bringe dieses jetzt zum Maze Bank Schalter!", "success", "top-left", 4500);
                                    bizz.cash = 0;
                                }
                                else
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Du kannst aus dem Unternehmen/Geschäft kein Geld rausnehmen!", "error", "top-left", 4500);
                                }
                            }, delayTime: 1250);
                            return;
                        }
                        else
                        {
                            Helper.SendNotificationWithoutButton(player, $"Du kannst aus dem Unternehmen/Geschäft kein Geld rausnehmen!", "error", "top-left", 4500);
                        }
                    }
                }
                //Achmed
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(801.72723, -2924.453, 5.9188385), 3.5f))
                {
                    if (!player.IsInVehicle)
                    {
                        SendNotificationWithoutButton(player, "Du sitzt in keinem Fahrzeug!", "error");
                        return;
                    }
                    if (player.Vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[4] == "1")
                    {
                        SendNotificationWithoutButton(player, "Achmed: Haha, dieses Fahrzeug hab ich schon ausgeschlachtet!", "error");
                        return;
                    }
                    VehicleShop vehicleShop = DealerShipController.GetVehicleShopByVehicleName(player.Vehicle.GetSharedData<String>("Vehicle:Name"));
                    if (vehicleShop != null)
                    {
                        VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(player.Vehicle);
                        if (vehicleData != null)
                        {
                            int skill = character.thiefskill / 25;
                            int price = vehicleShop.price / 100 * (8 + skill);
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            player.SetData<int>("Player:AchmedPrice", price);
                            player.TriggerEvent("Client:CallInput2", "Fahrzeugexporteur Achmed", $"Schickes Fahrzeug, ja das Fahrzeug kann ich ausschlachten! Für die Teile gebe ich dir {price}$!", "SellAchmed", "Ja", "Nein");
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Achmed: Mit diesem Fahrzeug kann ich nichts anfangen!", "error");
                        }
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Achmed: Mit diesem Fahrzeug kann ich nichts anfangen!", "error");
                    }
                }
                //Drugplants
                if (!player.IsInVehicle)
                {
                    DrugsPlants drugPlant = DrugController.GetClosestDrugPlant(player, 1.55f);
                    if (drugPlant != null)
                    {
                        if (drugPlant.value > 0)
                        {
                            Items newitem = null;

                            if (!ItemsController.CanPlayerHoldItem(player, 5 * drugPlant.value))
                            {
                                Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für die Drogen!", "success", "top-left");
                                return;
                            }

                            if (drugPlant.drugname == "Marihuana")
                            {
                                newitem = ItemsController.CreateNewItem(player, character.id, "Marihuana", "Player", drugPlant.value, ItemsController.GetFreeItemID(player));
                            }
                            else if (drugPlant.drugname == "Kokain")
                            {
                                newitem = ItemsController.CreateNewItem(player, character.id, "Kokablatt", "Player", drugPlant.value, ItemsController.GetFreeItemID(player));
                            }

                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }

                            drugPlant.textLabel.Text = $"~b~{drugPlant.drugname}pflanze\n~b~{drugPlant.value}g - Wasserzustand: {drugPlant.water}%\n\n~b~[E]~w~ zum ernten\n~b~[G]~w~ zum giessen\n~b~[P]~w~ zum zerstören";
                            drugPlant.value = 0;
                            Helper.PlayShortAnimation(player, "amb@world_human_gardener_plant@male@idle_a", "idle_b", 2250);
                            if (drugPlant.drugname == "Marihuana")
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast {drugPlant.value}g Marihuana gepflückt!", "success", "top-left");
                            }
                            else if (drugPlant.drugname == "Kokain")
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast {drugPlant.value}g Kokablätter gepflückt!", "success", "top-left");
                            }
                        }
                        else
                        {
                            Helper.SendNotificationWithoutButton(player, "Du kannst hier noch nichts ernten!", "error", "top-left");
                        }
                    }
                }
                //Häuser
                if (player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    foreach (House house in House.houseList)
                    {
                        if (house == null || house.interior == 0 || House.GetInteriorClassify(house.interior) < 4) continue;
                        if (player.Vehicle.Position.DistanceTo(house.position) <= 4.5f && player.Vehicle.Dimension == house.dimension && player.Vehicle.EngineStatus == true)
                        {
                            if (house.locked == 1)
                            {
                                SendNotificationWithoutButton(player, "Die Türe ist verschlossen!", "error");
                            }
                            else
                            {
                                if (house.status != 0)
                                {
                                    foreach (Cars car in Cars.carList)
                                    {
                                        if (car.vehicleHandle == player.Vehicle)
                                        {
                                            DealerShipController.SaveOneVehicleData(car);
                                            break;
                                        }
                                    }
                                    player.Vehicle.SetData("Vehicle:Rotation", player.Vehicle.Rotation);
                                    player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(house.interior));
                                    NAPI.Task.Run(() =>
                                    {
                                        player.Vehicle.Position = House.GetHouseExitPoint(house.interior);
                                        if (house.interior == 52)
                                        {
                                            Vector3 rotation = player.Vehicle.Rotation;
                                            player.Vehicle.Rotation = new Vector3(rotation.X, rotation.Y, 132.20403f);
                                        }
                                    }, delayTime: 155);
                                    player.Dimension = Convert.ToUInt32(house.id);
                                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                                    {
                                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetOwnSharedData<bool>("Player:Death") == false && p.Vehicle == player.Vehicle && p != player)
                                        {
                                            p.Dimension = Convert.ToUInt32(house.id);
                                            Character character2 = GetCharacterData(p);
                                            if (character2 != null)
                                            {
                                                character2.inhouse = house.id;
                                                p.SetOwnSharedData("Player:InHouse", house.id);
                                            }
                                        }
                                    }
                                    player.Vehicle.Dimension = Convert.ToUInt32(house.id);
                                    character.inhouse = house.id;
                                    player.SetOwnSharedData("Player:InHouse", house.id);
                                    Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                                    return;
                                }
                            }
                            return;
                        }
                        else if (character.inhouse == house.id)
                        {
                            Vector3 exitPosition;
                            exitPosition = House.GetHouseExitPoint(house.interior);
                            if (player.Vehicle.Position.DistanceTo(exitPosition) < 4.5f)
                            {
                                if (house.locked == 1)
                                {
                                    SendNotificationWithoutButton(player, "Die Türe ist verschlossen!", "error");
                                    return;
                                }
                                else
                                {
                                    player.Vehicle.Position = house.position;
                                    if (player.Vehicle.HasData("Vehicle:Rotation"))
                                    {
                                        Vector3 rotation = player.Vehicle.GetData<Vector3>("Vehicle:Rotation");
                                        player.Vehicle.Rotation = new Vector3(rotation.X, rotation.Y, rotation.Z + 180);
                                    }
                                    player.Vehicle.Dimension = (uint)house.dimension;
                                    character.inhouse = -1;
                                    player.SetOwnSharedData("Player:InHouse", -1);
                                    player.Dimension = (uint)house.dimension;
                                    foreach (Player p in NAPI.Pools.GetAllPlayers())
                                    {
                                        if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true && p.GetOwnSharedData<bool>("Player:Death") == false && p.Vehicle == player.Vehicle && p != player)
                                        {
                                            p.Dimension = (uint)house.dimension;
                                            Character character2 = GetCharacterData(p);
                                            if (character2 != null)
                                            {
                                                character2.inhouse = -1;
                                                p.SetOwnSharedData("Player:InHouse", -1);
                                            }
                                        }
                                    }
                                    Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressE]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:OnPlayerPressF")]
        public static void OnPlayerPressF(Player player, int atATM = -1)
        {
            try
            {
                //Accountdata + Character Data
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (account == null || character == null || tempData.freezed == true || player.GetOwnSharedData<bool>("Player:Spawned") == false) return;
                //Rollerverleih
                if (IsInRangeOfPoint(player.Position, new Vector3(-523.0017, -256.7837, 35.6499), 4.55f) || IsInRangeOfPoint(player.Position, new Vector3(479.0879, -1861.0743, 27.460703), 4.55f) || IsInRangeOfPoint(player.Position, new Vector3(-682.462, 319.9259, 83.083145), 1.85f))
                {
                    if (player.IsInVehicle) return;
                    if (tempData.rentVehicle == null)
                    {
                        player.TriggerEvent("Client:PlayerFreeze", true);
                        if (IsInRangeOfPoint(player.Position, new Vector3(-682.462, 319.9259, 83.083145), 1.85f))
                        {
                            player.TriggerEvent("Client:CallInput2", "Rollstuhl Rollerverleih", "Möchtest du einen Rollstuhl für 150$ leihen?", "RentFaggio", "Ja", "Nein");
                        }
                        else
                        {
                            player.TriggerEvent("Client:CallInput2", "Rathaus Rollerverleih", "Möchtest du einen Roller (Faggio) für 150$ leihen?", "RentFaggio", "Ja", "Nein");
                        }
                        return;
                    }
                    else
                    {
                        if (tempData.rentVehicle.Class != 8 && tempData.rentVehicle.Class != 0)
                        {
                            SendNotificationWithoutButton(player, "Du hast kein Fahrzeug gemietet!", "error");
                            return;
                        }
                        if (!IsInRangeOfPoint(player.Position, tempData.rentVehicle.Position, 5.0f))
                        {
                            SendNotificationWithoutButton(player, "Des gemietete Fahrzeug ist nicht in der Nähe!", "error");
                            return;
                        }
                        SendNotificationWithoutButton(player, "Du hast das Fahrzeug wieder zurück gegeben und erhältst 50$ zurück!", "success");
                        CharacterController.SetMoney(player, 50);
                        tempData.rentVehicle.Delete();
                        tempData.rentVehicle = null;
                        player.TriggerEvent("Client:PlayerFreeze", false);
                        return;
                    }
                }
                if (player.IsInVehicle) return;
                //Rathaus
                if (IsInRangeOfPoint(player.Position, new Vector3(-555.7711, -185.85564, 38.22111), 3.75f) && player.Dimension == 0)
                {
                    player.TriggerEvent("Client:ShowStadthalle");
                    return;
                }
                //HOF
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-509.94046, -208.40746, 39.37905), 3.75f))
                {
                    Commands.cmd_hof(player);
                }
                //Bank
                int atbank = Helper.IsAtBank(player, atATM);
                if (atbank > -1)
                {
                    if (atATM > -1)
                    {
                        Commands.cmd_animation(player, "give", true);
                        player.TriggerEvent("Client:PlaySoundPeep");
                    }
                    BankController.ShowBankMenu(player, atbank);
                    return;
                }
                //Waffenkammer LSPD
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(484.85513, -1003.6393, 25.734646), 2.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(609.0513, -14.120579, 76.62814), 2.75f)) && player.Dimension == 0)
                {
                    if (character.faction != 1)
                    {
                        SendNotificationWithoutButton(player, "Wer bist du?", "error", "top-end", 2250);
                        return;
                    }
                    if (character.factionduty == false)
                    {
                        SendNotificationWithoutButton(player, "Beginn erstmal deinen Dienst!", "error", "top-end", 2250);
                        return;
                    }
                    ShowPreShop(player, "Waffenkammer LSPD", 0, 1, 1);
                }
                //Waffenkammer LSRC
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-679.80237, 329.18146, 88.017006), 2.75f) && player.Dimension == 0)
                {
                    if (character.faction != 2)
                    {
                        SendNotificationWithoutButton(player, "Wer bist du?", "error", "top-end", 2250);
                        return;
                    }
                    if (character.factionduty == false)
                    {
                        SendNotificationWithoutButton(player, "Beginn erstmal deinen Dienst!", "error", "top-end", 2250);
                        return;
                    }
                    ShowPreShop(player, "Lager LSRC", 0, 1, 1);
                }
                //Aufzug LSRC
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(-664.159, 328.23718, 83.08322), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-664.0749, 328.1962, 88.01673), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-664.0938, 328.36017, 92.7444), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-664.0335, 328.23676, 78.12267), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-664.07245, 328.3244, 140.12306), 1.75f)) && player.Dimension == 0)
                {
                    string text1 = "Keller,Ebene 1,Ebene 2,Ebene 3,Dach,Abbrechen";
                    string text2 = "0,0,0,0,0,0";
                    tempData.lastShop = "LSRC Dach";
                    player.TriggerEvent("Client:ShowShop2", text1, text2, "LSRC Aufzug", 0, 1, 1, false);
                    return;
                }
                //Waffenkammer ACLS
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-350.75766, -155.40205, 39.013554), 2.75f) && player.Dimension == 0)
                {
                    if (character.faction != 3)
                    {
                        SendNotificationWithoutButton(player, "Wer bist du?", "error", "top-end", 2250);
                        return;
                    }
                    if (character.factionduty == false)
                    {
                        SendNotificationWithoutButton(player, "Beginn erstmal deinen Dienst!", "error", "top-end", 2250);
                        return;
                    }
                    ShowPreShop(player, "Lager ACLS", 0, 1, 1);
                }
                //Allgemeine Verwaltung GOV
                if (Helper.IsInRangeOfPoint(player.Position, new Vector3(-585.1373, -211.43108, 42.836597), 2.75f) && player.Dimension == 0)
                {
                    if (character.faction != 4 || character.rang <= 10)
                    {
                        SendNotificationWithoutButton(player, "Du hast keine Berechtigung um auf die allgemeine Verwaltung zuzugreifen!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.factionduty == false)
                    {
                        SendNotificationWithoutButton(player, "Beginn erstmal deinen Dienst!", "error", "top-end", 2250);
                        return;
                    }
                    string prices0 = $"{adminSettings.lsteuer},{adminSettings.gsteuer},{adminSettings.ksteuer.ToString(new CultureInfo("en-US"))},{adminSettings.towedcash}";
                    string prices1 = $"{FactionController.factionBudgetsList[0].budget},{FactionController.factionBudgetsList[1].budget},{FactionController.factionBudgetsList[2].budget},{FactionController.factionBudgetsList[3].budget},{FactionController.factionBudgetsList[4].budget}";
                    string prices2 = "";
                    try
                    {
                        prices2 = $"{adminSettings.grouparray[4]},{adminSettings.grouparray[5]},{adminSettings.grouparray[6]},{adminSettings.grouparray[7]},{adminSettings.grouparray[8]},{adminSettings.grouparray[9]},{adminSettings.grouparray[10]},{adminSettings.grouparray[11]},{adminSettings.grouparray[12]},{adminSettings.grouparray[13]}";
                    }
                    catch (Exception)
                    {
                        prices2 = "0,0,0,0,0,0,0,0,0,0";
                    }
                    player.TriggerEvent("Client:ShowGovMenu", prices0, prices1, prices2);
                }
                //LSPD Gebäude
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(565.7387, 4.9065294, 103.233215), 2.75f)) && player.Dimension == 0)
                {
                    if (character.faction == 1 || tempData.adminduty == true)
                    {
                        SetPlayerPosition(player, new Vector3(604.84576, 5.519227, 97.87246 + 0.01));
                        player.Heading = -11.951478f;
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Du hast nicht den passenden Schlüssel dabei!", "error", "top-end", 2250);
                    }
                    return;
                }
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(604.84576, 5.519227, 97.87246), 2.75f)) && player.Dimension == 0 && character.faction == 1)
                {
                    if (character.faction == 1 || tempData.adminduty == true)
                    {
                        SetPlayerPosition(player, new Vector3(565.7387, 4.9065294, 103.233215 + 0.01));
                        player.Heading = -96.49713f;
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Du hast nicht den passenden Schlüssel dabei!", "error", "top-end", 2250);
                    }
                    return;
                }
                //Apotheke
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(-665.72156, 322.29745, 83.08319), 3.15f)))
                {
                    ShowApotheke(player);
                }
                //Materialversteck
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(-2070.9304, -1020.88715, 5.884131), 2.15f)))
                {
                    if (mats <= 0)
                    {
                        SendNotificationWithoutButton(player, "Im Versteck befinden sich keine Materialien mehr!", "error", "top-left", 2250);
                        return;
                    }
                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Materialien", "Player", mats, ItemsController.GetFreeItemID(player));
                    if (!ItemsController.CanPlayerHoldItem(player, newitem.weight))
                    {
                        newitem = null;
                        Helper.SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für die Materialien!", "success", "top-left");
                        return;
                    }
                    if (newitem != null)
                    {
                        tempData.itemlist.Add(newitem);
                        mats = 0;
                        SendNotificationWithoutButton(player, $"Du hast {mats} Materialien aus dem Versteck genommen!", "error", "top-left", 2250);
                    }
                }
                //Bar
                if (IsAtBar(player))
                {
                    ShowBar(player);
                }
                //Music Stations
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(1987.5565, 3051.1028, 47.215157), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(121.39574, -1279.8123, 29.6533), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(840.6783, -118.2715, 79.77466), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-456.60736, 274.12994, 84.22368), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-561.8999, 281.58398, 85.67638), 1.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-1381.4324, -616.371, 31.497929), 1.75f)) && player.Dimension == 0)
                {
                    Business bizz = Business.GetClosestBusiness(player, 85.5f);
                    if (bizz.musicPlayer == null || bizz.musicPlayer == player)
                    {
                        if (player.HasData("Player:MusicBizz"))
                        {
                            if (bizz.id != player.GetData<int>("Player:MusicBizz"))
                            {
                                SendNotificationWithoutButton(player, "Du spielst schon woanders Musik ab!", "error", "top-end", 2250);
                                return;
                            }
                        }
                        player.SetData<int>("Player:MusicBizz", bizz.id);
                        bizz.musicPlayer = player;
                        if (bizz.id == 45)
                        {
                            player.TriggerEvent("Client:SetSoundRange", 125.5f);
                        }
                        else if (bizz.id == 46)
                        {
                            player.TriggerEvent("Client:SetSoundRange", 27.5f);
                        }
                        else if (bizz.id == 47)
                        {
                            player.TriggerEvent("Client:SetSoundRange", 119.5f);
                        }
                        else if (bizz.id == 48)
                        {
                            player.TriggerEvent("Client:SetSoundRange", 17.5f);
                        }
                        else if (bizz.id == 49)
                        {
                            player.TriggerEvent("Client:SetSoundRange", 155.5f);
                        }
                        player.TriggerEvent("Client:ShowMusicStation", 1);
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Es hat schon jemand anders Kontrolle über die Musikstation!", "error", "top-end", 2250);
                    }
                }
                //Snackpoint
                if ((Helper.IsInRangeOfPoint(player.Position, new Vector3(606.85767, -4.1742187, 82.62812), 2.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-691.39923, 322.45074, 83.08313), 2.75f) || Helper.IsInRangeOfPoint(player.Position, new Vector3(-667.6538, 342.01093, 83.08318), 2.75f)) && player.Dimension == 0)
                {
                    ShowSnackpoint(player);
                }
                //24/7
                if (IsAt247(player))
                {
                    Show247Menu(player);
                }
                //Barber Shop
                if (IsAtBarberShop(player))
                {
                    if (tempData.adminduty == true || character.factionduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du kannst diese Funktion jetzt nicht benutzen!", "error", "top-end", 2250);
                        return;
                    }
                    if (account.faqarray[5] == "0")
                    {
                        account.faqarray[5] = "1";
                    }
                    ShowBarberShop(player);
                }
                //Tattoo Shop
                if (IsAtTattooShop(player))
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du kannst diese Funktion jetzt nicht benutzen!", "error", "top-end", 2250);
                        return;
                    }
                    ShowTattooShop(player);
                }
                //Ammunation
                if (IsAtAmmunation(player))
                {
                    if (SetAndGetCharacterLicense(player, 5, 1337) != "1")
                    {
                        SendNotificationWithoutButton(player, "Du besitzt keinen Waffenschein!", "error", "top-end", 2250);
                        return;
                    }
                    ShowPreShop(player, "Ammunation", 0, 1, 1);
                }
                //Cardealer
                if (IsAtDealerShip(player) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    DealerShipController.ShowDealerShip(player);
                }
                //Shootingrange
                if (IsInRangeOfPoint(player.Position, new Vector3(6.5058265, -1100.1274, 29.797022), 3.15f) && player.Dimension == 0)
                {
                    if (player.HasData("Player:AmmuQuiz"))
                    {
                        if (player.GetData<int>("Player:AmmuQuiz") == 1)
                        {
                            return;
                        }
                    }
                    if (SetAndGetCharacterLicense(player, 5, 1337) != "1")
                    {
                        SendNotificationWithoutButton(player, "Du besitzt keinen Waffenschein!", "error", "top-end", 2250);
                        return;
                    }
                    if (NAPI.Player.GetPlayerCurrentWeapon(player) == WeaponHash.Unarmed)
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst eine Waffe ausrüsten!", "error", "top-end", 2250);
                        return;
                    }
                    if (NAPI.Player.GetPlayerCurrentWeaponAmmo(player) < 50)
                    {
                        SendNotificationWithoutButton(player, "Du benötigst mind. 50 Schuss für die ausgerüstete Waffe!", "error", "top-end", 2250);
                        return;
                    }
                    Items item = ItemsController.GetItemFromWeaponHash(player, NAPI.Player.GetPlayerCurrentWeapon(player));
                    if (item != null)
                    {
                        int weaponclass = Convert.ToInt32(WeaponController.GetWeaponClass(item.description.ToLower()));
                        if (weaponclass == -1 || weaponclass == 2 || weaponclass == 9 || weaponclass == 10)
                        {
                            SendNotificationWithoutButton(player, "Ungültige Waffe!", "error", "top-end", 2250);
                            return;
                        }
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Ungültige Waffe!", "error", "top-end", 2250);
                        return;
                    }
                    player.TriggerEvent("Client:CallInput2", "Shootingrange", "Möchtest du versuchen 50 Ziele so schnell wie möglich zu treffen (Mittlerer roter Punkt)? (( Im UCP findest du eine Statistik! ))", "Shootingrange", "Ja", "Nein");
                }
                //Waffenschein
                if (IsInRangeOfPoint(player.Position, new Vector3(12.569119, -1105.6268, 29.797026), 3.15f) && player.Dimension == 0)
                {
                    if (SetAndGetCharacterLicense(player, 5, 1337) == "1")
                    {
                        SendNotificationWithoutButton(player, "Du hast bereits einen Waffenschein!", "error", "top-end", 2250);
                        return;
                    }
                    if (Convert.ToInt32(SetAndGetCharacterLicense(player, 5, 1337)) > 1 && Helper.UnixTimestamp() < Convert.ToInt32(SetAndGetCharacterLicense(player, 5, 1337)))
                    {
                        SendNotificationWithoutButton(player, $"Du hast noch eine Waffenscheinsperre bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(SetAndGetCharacterLicense(player, 6, 1337)))} Uhr!", "error", "top-end", 3550);
                        return;
                    }
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    player.TriggerEvent("Client:CallInput2", "Waffenschein beantragen", "Möchtest du einen Waffenschein für 12500$ beantragen?", "BuyWaffenschein", "Ja", "Nein");
                }
                //Kleidungsladen
                if (Helper.IsAtClothShop(player))
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du kannst diese Funktion jetzt nicht benutzen!", "error", "top-end", 2250);
                        return;
                    }
                    if (account.faqarray[4] == "0")
                    {
                        account.faqarray[4] = "1";
                    }
                    Business.ShowClothMenu(player);
                    return;
                }
                //Maskenhändler
                if (IsInRangeOfPoint(player.Position, new Vector3(-1579.3323, -951.5237, 13.017388), 3.25f) && player.Dimension == 0)
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du kannst diese Funktion jetzt nicht benutzen!", "error", "top-end", 2250);
                        return;
                    }
                    if (NAPI.Player.GetPlayerAccessoryDrawable(player, 0) != 255)
                    {
                        SendNotificationWithoutButton(player, "Zieh zuerst bitte deine Kopfbedeckung ab!", "error", "top-end", 2250);
                        return;
                    }
                    Business.ShowMaskMenu(player);
                    return;
                }
                //Juwelier
                if (IsInRangeOfPoint(player.Position, new Vector3(-622.2842, -229.88474, 38.057053), 3.25f) && player.Dimension == 0)
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du kannst diese Funktion jetzt nicht benutzen!", "error", "top-end", 2250);
                        return;
                    }
                    Business.ShowJuweMenu(player);
                    return;
                }
                //Fraktionen
                //LSPD
                if ((IsInRangeOfPoint(player.Position, new Vector3(443.5972, -984.0872, 30.689312), 4.25f) || IsInRangeOfPoint(player.Position, new Vector3(633.46796, 8.740156, 82.628075), 4.25f)) && player.Dimension == 0)
                {
                    tempData.lastShop = "Police-Department";
                    player.TriggerEvent("Client:ShowShop2", "Führungszeugnis beantragen,Abbrechen", "525,0", "Police-Department", 0, 1, 1, false);
                }
                if ((IsInRangeOfPoint(player.Position, new Vector3(471.16223, -988.9328, 25.734646), 4.25f) || IsInRangeOfPoint(player.Position, new Vector3(629.6149, 3.6072264, 76.628044), 4.25f) || IsInRangeOfPoint(player.Position, new Vector3(624.4518, -3.4003117, 76.628136), 4.25f)) && player.Dimension == 0)
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst deinen Admindienst beenden!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.faction != 1)
                    {
                        SendNotificationWithoutButton(player, "Du besitzt keinen Schlüssel für einen Spint!", "error", "top-end", 2250);
                        return;
                    }
                    player.SetData<bool>("Player:InShop", true);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                    }
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    List<Outfits> outfitList = new List<Outfits>();
                    foreach (Outfits outfit in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 3", "faction-" + character.id))
                    {
                        outfitList.Add(outfit);
                    }
                    if (IsInRangeOfPoint(player.Position, new Vector3(471.16223, -988.9328, 25.734646), 4.25f))
                    {
                        SetPlayerPosition(player, new Vector3(471.24893, -989.1332, 25.734667 + 0.01));
                        player.Heading = 2.1481764f;
                    }
                    if (IsInRangeOfPoint(player.Position, new Vector3(629.6149, 3.6072264, 76.628044), 4.25f))
                    {
                        SetPlayerPosition(player, new Vector3(629.4588, 3.3549075, 76.62808 + 0.01));
                        player.Heading = 166.66809f;
                    }
                    if (IsInRangeOfPoint(player.Position, new Vector3(624.4518, -3.4003117, 76.628136), 4.25f))
                    {
                        SetPlayerPosition(player, new Vector3(624.51294, -3.63025, 76.62802));
                        player.Heading = 72.135796f;
                    }
                    player.Dimension = (uint)(player.Id + 5);
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:CharacterCameraOn");
                    }, delayTime: 500);
                    player.SetData<bool>("Player:InShop", true);
                    NAPI.Player.ClearPlayerAccessory(player, 2);
                    NAPI.Player.ClearPlayerAccessory(player, 6);
                    NAPI.Player.ClearPlayerAccessory(player, 7);
                    NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    NAPI.Player.ClearPlayerAccessory(player, 1);
                    NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    player.TriggerEvent("Client:ShowFactionClothing", NAPI.Util.ToJson(obj["clothing"]), NAPI.Util.ToJson(obj["clothingColor"]), character.gender, character.faction, NAPI.Util.ToJson(outfitList));
                    return;
                }
                //LSRC
                if ((IsInRangeOfPoint(player.Position, new Vector3(-663.262, 321.58627, 92.74433), 4.25f)) && player.Dimension == 0)
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst deinen Admindienst beenden!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.faction != 2)
                    {
                        SendNotificationWithoutButton(player, "Du besitzt keinen Schlüssel für einen Spint!", "error", "top-end", 2250);
                        return;
                    }
                    player.SetData<bool>("Player:InShop", true);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                    }
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    List<Outfits> outfitList = new List<Outfits>();
                    foreach (Outfits outfit in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 3", "faction-" + character.id))
                    {
                        outfitList.Add(outfit);
                    }
                    if (IsInRangeOfPoint(player.Position, new Vector3(-663.262, 321.58627, 92.74433), 4.25f))
                    {
                        SetPlayerPosition(player, new Vector3(-664.10297, 322.28616, 92.74441));
                        player.Heading = 82.002144f;
                    }
                    player.Dimension = (uint)(player.Id + 5);
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:CharacterCameraOn");
                    }, delayTime: 500);
                    player.SetData<bool>("Player:InShop", true);
                    NAPI.Player.ClearPlayerAccessory(player, 2);
                    NAPI.Player.ClearPlayerAccessory(player, 6);
                    NAPI.Player.ClearPlayerAccessory(player, 7);
                    NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    NAPI.Player.ClearPlayerAccessory(player, 1);
                    NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    player.TriggerEvent("Client:ShowFactionClothing", NAPI.Util.ToJson(obj["clothing"]), NAPI.Util.ToJson(obj["clothingColor"]), character.gender, character.faction, NAPI.Util.ToJson(outfitList));
                    return;
                }
                //ACLS
                if ((IsInRangeOfPoint(player.Position, new Vector3(-340.87454, -161.04536, 44.58743), 4.25f)) && player.Dimension == 0)
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst deinen Admindienst beenden!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.faction != 3)
                    {
                        SendNotificationWithoutButton(player, "Du besitzt keinen Schlüssel für einen Spint!", "error", "top-end", 2250);
                        return;
                    }
                    player.SetData<bool>("Player:InShop", true);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                    }
                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    List<Outfits> outfitList = new List<Outfits>();
                    foreach (Outfits outfit in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 3", "faction-" + character.id))
                    {
                        outfitList.Add(outfit);
                    }
                    if (IsInRangeOfPoint(player.Position, new Vector3(-340.87454, -161.04536, 44.58743), 4.25f))
                    {
                        SetPlayerPosition(player, new Vector3(-339.501, -161.42168, 44.5875));
                        player.Heading = -99.88331f;
                    }
                    player.Dimension = (uint)(player.Id + 5);
                    NAPI.Task.Run(() =>
                    {
                        player.TriggerEvent("Client:CharacterCameraOn");
                    }, delayTime: 500);
                    player.SetData<bool>("Player:InShop", true);
                    NAPI.Player.ClearPlayerAccessory(player, 2);
                    NAPI.Player.ClearPlayerAccessory(player, 6);
                    NAPI.Player.ClearPlayerAccessory(player, 7);
                    NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                    NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                    NAPI.Player.ClearPlayerAccessory(player, 1);
                    NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                    player.TriggerEvent("Client:ShowFactionClothing", NAPI.Util.ToJson(obj["clothing"]), NAPI.Util.ToJson(obj["clothingColor"]), character.gender, character.faction, NAPI.Util.ToJson(outfitList));
                    return;
                }
                //GOV
                if ((IsInRangeOfPoint(player.Position, new Vector3(-553.10834, -182.35042, 38.43551), 3.25f)) && player.Dimension == 0)
                {
                    if (tempData.adminduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du musst zuerst deinen Admindienst beenden!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.faction != 4)
                    {
                        SendNotificationWithoutButton(player, "Wer bist du?", "error", "top-end", 2250);
                        return;
                    }
                    if (character.factionduty == true)
                    {
                        character.factionduty = false;
                        Helper.SendNotificationWithoutButton(player, $"Dienst beendet!", "success", "top-left", 2500);

                        Items radio = ItemsController.GetItemByItemName(player, "Funkgerät");
                        if (radio != null)
                        {
                            ItemsController.RemoveItem(player, radio.itemid);
                        }
                    }
                    else
                    {
                        Helper.SendNotificationWithoutButton(player, $"Dienst begonnen!", "success");
                        character.factionduty = true;
                        Items radio = ItemsController.GetItemByItemName(player, "Funkgerät");
                        if (radio == null)
                        {
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "Funkgerät", "Player", 1, ItemsController.GetFreeItemID(player));
                            if (newitem != null && ItemsController.CanPlayerHoldItem(player, newitem.weight))
                            {
                                tempData.itemlist.Add(newitem);
                            }
                        }
                    }
                    return;
                }
                //Jobs
                //Spediteur
                if (IsInRangeOfPoint(player.Position, new Vector3(-1107.5642, -2040.5759, 13.291501), 3.25f) && player.Dimension == 0)
                {
                    if (character.job == 1)
                    {
                        if (Helper.IsASpediteur(player) == 1)
                        {
                            SendNotificationWithoutButton(player, "Du bist doch in einer Speditionsfirma, arbeite doch für die!", "error", "top-end", 2250);
                            return;
                        }
                        if (tempData.jobduty == false)
                        {
                            SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                            tempData.jobduty = true;
                        }
                        else
                        {
                            if (tempData.jobVehicle != null)
                            {
                                SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                return;
                            }
                            player.TriggerEvent("Client:RemoveWaypoint");
                            SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                            tempData.jobduty = false;
                        }
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                    }
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-1077.2032, -2079.2852, 13.291501), 3.5f) && player.Dimension == 0)
                {
                    if (character.job != 1)
                    {
                        SendNotificationWithoutButton(player, "Du arbeitest doch garnicht bei uns?", "error", "top-end", 2500);
                        return;
                    }
                    if (Helper.IsASpediteur(player) == 1)
                    {
                        SendNotificationWithoutButton(player, "Du bist doch in einer Speditionsfirma, arbeite doch für die!", "error", "top-end", 2250);
                        return;
                    }
                    if (tempData.jobduty == false)
                    {
                        SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                        return;
                    }
                    if (tempData.jobVehicle != null)
                    {
                        if (IsInRangeOfPoint(player.Position, tempData.jobVehicle.Position, 8.25f))
                        {
                            if (tempData.jobVehicle != null && Helper.IsTrailerAttached2(tempData.jobVehicle))
                            {
                                SendNotificationWithoutButton(player, "Bring zuerst den Anhänger weg!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                            {
                                SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                return;
                            }
                            SendNotificationWithoutButton(player, "Danke fürs zurückbringen!", "success", "top-end", 2500);
                            if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                            {
                                tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                            }
                            tempData.jobVehicle.Delete();
                            tempData.jobVehicle = null;
                            NAPI.Task.Run(() =>
                            {
                                Vehicle veh = Helper.GetClosestVehicle(player, 8.95f);
                                if (veh != null && IsATrailer(veh))
                                {
                                    veh.Spawn(veh.GetData<Vector3>("Vehicle:Position"));
                                    NAPI.Task.Run(() =>
                                    {
                                        veh.Rotation = new Vector3(0.0, 0.0, player.Vehicle.Rotation.Z);
                                        veh.EngineStatus = false;
                                    }, delayTime: 215);
                                }
                            }, delayTime: 155);
                            return;
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Dein Jobfahrzeug befindet sich nicht in der Nähe!", "error", "top-end", 2500);
                        }
                        return;
                    }
                    else
                    {
                        player.TriggerEvent("Client:ShowSpedition", 1, NAPI.Util.ToJson(Helper.spedVehiclesList));
                    }
                }
                //Jäger
                if (IsInRangeOfPoint(player.Position, new Vector3(1094.8888, -265.38666, 69.314156), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    Helper.ShowPreShop(player, "Jägermenü", 0, 1, 1);
                }
                //Müllmann
                if (IsInRangeOfPoint(player.Position, new Vector3(-350.10602, -1569.9631, 25.221148), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (character.job != 4)
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                        return;
                    }
                    Helper.ShowPreShop(player, "Müllmannmenü", 0, 1, 1);
                }
                //Kanalreiniger
                if (IsInRangeOfPoint(player.Position, new Vector3(415.26788, -2072.553, 21.47752), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (character.job != 5)
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                        return;
                    }
                    Helper.ShowPreShop(player, "Kanalreinigermenü", 0, 1, 1);
                }
                //Landwirt
                if (IsInRangeOfPoint(player.Position, new Vector3(2243.3335, 5154.16, 57.88714), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (character.job != 7)
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                        return;
                    }
                    Helper.ShowPreShop(player, "Landwirtmenü", 0, 1, 1);
                }
                //Geldlieferant
                if (IsInRangeOfPoint(player.Position, new Vector3(-1402.6819, -451.97513, 34.482605), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (character.job != 8)
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                        return;
                    }
                    Helper.ShowPreShop(player, "Geldlieferantmenü", 0, 1, 1);
                }
                //Busfahrer
                if (IsInRangeOfPoint(player.Position, new Vector3(442.69806, -628.07556, 28.520735), 3.55f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (Helper.IsABusDriver(player) == 1)
                    {
                        SendNotificationWithoutButton(player, "Du bist doch in einer Personenbeförderungsfirma, arbeite doch für die!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.job != 3)
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                        return;
                    }
                    Helper.ShowPreShop(player, "Busfahrermenü", 0, 1, 1);
                }
                //Taxifahrer
                if (IsInRangeOfPoint(player.Position, new Vector3(895.20514, -179.2199, 74.700356), 3.55f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (Helper.IsATaxiDriver(player) == 1)
                    {
                        SendNotificationWithoutButton(player, "Du bist doch in einer Personenbeförderungsfirma, arbeite doch für die!", "error", "top-end", 2250);
                        return;
                    }
                    if (character.job != 6)
                    {
                        SendNotificationWithoutButton(player, "Suchst du nach einem Job? Schau mal im Rathaus vorbei!", "success", "top-end", 2500);
                        return;
                    }
                    Helper.ShowPreShop(player, "Taxifahrermenü", 0, 1, 1);
                }
                //Ticketverkauf
                if (IsInRangeOfPoint(player.Position, new Vector3(440.02197, -646.4714, 28.520739), 3.55f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    Helper.ShowPreShop(player, "Ticketverkauf", 0, 1, 1);
                }
                //Fahrschule
                if (IsInRangeOfPoint(player.Position, new Vector3(-711.8821, -1307.4515, 5.113356), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    Helper.ShowPreShop(player, "Fahrschule", 0, 1, 1);
                }
                //Müll sammeln am Strand
                if (IsInRangeOfPoint(player.Position, new Vector3(-1376.8951, -1424.4462, 3.5720022), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    if (tempData.jobduty == true)
                    {
                        SendNotificationWithoutButton(player, "Du arbeitest doch gerade schon in deinem Job!", "error", "top-end", 2500);
                        return;
                    }
                    if (player.HasData("Player:GarbageBag"))
                    {
                        Helper.SendNotificationWithoutButton(player, "Schade, wenn du mir wieder helfen willst, sag einfach bescheid!", "success", "top-left", 4150);
                        Helper.AddRemoveAttachments(player, "garbageBag", false);
                        player.ResetData("Player:GarbageBag");
                        player.TriggerEvent("Client:StopBeachGarbage");
                    }
                    else
                    {
                        player.TriggerEvent("Client:CallInput2", "Müll aufsammeln", "Möchtest du mir helfen, den Strand vom Müll zu befreien?", "GarbageBag", "Ja", "Nein");
                    }
                }
                //Angelmenü
                if (IsInRangeOfPoint(player.Position, new Vector3(-2195.372, -418.9289, 13.095015), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    Helper.ShowPreShop(player, "Angelmenü", 0, 1, 1);
                }
                //Schatzsuchermenü
                if (IsInRangeOfPoint(player.Position, new Vector3(1441.0588, 3749.4375, 32.193043), 3.25f) && player.Dimension == 0 && !player.IsInVehicle)
                {
                    Helper.ShowPreShop(player, "Schatzsuchermenü", 0, 1, 1);
                }
                //Business
                foreach (Business bizz in Business.businessList)
                {
                    if (bizz == null) continue;
                    if (player.Position.DistanceTo(bizz.position) <= 2.5f && player.Dimension == 0)
                    {
                        if (bizz.owner == "n/A")
                        {
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            player.TriggerEvent("Client:CallInput2", "Businesskauf", $"Möchtest du dieses Business für {bizz.price}$ kaufen?", "BuyBizz", "Ja", "Nein");
                            return;
                        }
                    }
                }
                //Häuser
                foreach (House house in House.houseList)
                {
                    if (house == null || house.interior == 0) continue;
                    if (player.Position.DistanceTo(house.position) <= 1.65f && player.Dimension == house.dimension)
                    {
                        if (house.locked == 1)
                        {
                            SendNotificationWithoutButton(player, "Die Türe ist verschlossen!", "error");
                        }
                        else
                        {
                            if (house.status == 0)
                            {
                                player.TriggerEvent("Client:PlayerFreeze", true);
                                player.TriggerEvent("Client:CallInput2", "Hauskauf", $"Möchtest du dieses Haus für {house.price}$ kaufen?", "BuyHouse", "Kaufen", "Besichtigen");
                                return;
                            }
                            else
                            {
                                player.TriggerEvent("Client:LoadIPL", House.GetInteriorIPL(house.interior));
                                SetPlayerPosition(player, House.GetHouseExitPoint(house.interior));
                                player.Dimension = Convert.ToUInt32(house.id);
                                character.inhouse = house.id;
                                player.SetOwnSharedData("Player:InHouse", house.id);
                                Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                                return;
                            }
                        }
                        return;
                    }
                    else if (character.inhouse == house.id)
                    {
                        Vector3 exitPosition;
                        exitPosition = House.GetHouseExitPoint(house.interior);
                        if (player.Position.DistanceTo(exitPosition) < 3.5f)
                        {
                            if (house.locked == 1)
                            {
                                SendNotificationWithoutButton(player, "Die Türe ist verschlossen!", "error");
                                return;
                            }
                            else
                            {
                                SetPlayerPosition(player, house.position);
                                player.Dimension = (uint)house.dimension;
                                character.inhouse = -1;
                                player.SetOwnSharedData("Player:InHouse", -1);
                                Furniture.UpdateMöbelList(player, House.GetFurnitureForHouse(house.id));
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartCleaningDrone")]
        public static void OnStartCleaningDrone(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null && character.job == 5)
                {

                    Vector3[] kanalPosi = new Vector3[3]
                    {
                       new Vector3(16.251493, -12.196378, -146.64925),
                       new Vector3(67.03948, -0.7744771, -146.63298),
                       new Vector3(15.490382, 31.995388, -146.64862)
                    };

                    float[] kanalPosiRot = new float[3]
                    {
                       -89.88654f,
                       89.073235f,
                       -90.32696f
                    };

                    Random rand = new Random();
                    int index = rand.Next(3);

                    player.Dimension = (uint)(player.Id + 250);
                    tempData.jobVehicle2 = Cars.createNewCar("rcbandito", new Vector3(kanalPosi[index].X, kanalPosi[index].Y, kanalPosi[index].Z + 1.25), kanalPosiRot[index], 62, 62, "Drohne", "n/A", false, false, true, player.Dimension, null, true);
                    tempData.furnitureOldPosition = player.Position;
                    SetPlayerPosition(player, new Vector3(kanalPosi[index].X, kanalPosi[index].Y, kanalPosi[index].Z + 1.25));
                    NAPI.Task.Run(() =>
                    {
                        NAPI.Player.SetPlayerIntoVehicle(player, tempData.jobVehicle2, 0);
                        NAPI.Task.Run(() =>
                        {
                            player.TriggerEvent("Client:StartCleaner", player.Dimension);
                        }, delayTime: 85);
                    }, delayTime: 515);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartCleaningDrone]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:BeachGarbageFinish")]
        public static void OnBeachGarbageFinish(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character != null)
                {
                    int salary = 6 * (86 + rnd.Next(0, 9));
                    Helper.SendNotificationWithoutButton(player, $"WOW soviel Müll, hier ein neuer Müllbeutel und das ist für dich: {salary}$", "success", "top-left", 4150);
                    CharacterController.SetMoney(player, salary);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnBeachGarbageFinish]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CleanerFinish")]
        public static void OnCleanerFinish(Player player, int check = 1)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null && character.job == 5)
                {
                    if (check == 1)
                    {
                        int salary = 8 * (102 + rnd.Next(0, 5));
                        SendNotificationWithoutButton(player, $"Kanal erfolgreich gesäubert, du erhälst für den nächsten Payday {salary}$ gutgeschrieben!", "success", "top-left", 5200);
                        character.nextpayday += salary;
                    }
                    if (player.IsInVehicle)
                    {
                        player.WarpOutOfVehicle();
                    }
                    if (tempData.jobVehicle2 != null)
                    {
                        if (tempData.jobVehicle2.HasSharedData("Vehicle:Text3D"))
                        {
                            tempData.jobVehicle2.ResetSharedData("Vehicle:Text3D");
                        }
                        tempData.jobVehicle2.Delete();
                        tempData.jobVehicle2 = null;
                    }
                    player.Dimension = 0;
                    SetPlayerPosition(player, tempData.furnitureOldPosition);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCleanerFinish]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:FarmerFinish")]
        public static void OnFarmerFinish(Player player, int check = 1)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character != null && character.job == 7)
                {
                    int skill = character.farmingskill / 25;
                    int bonus = skill * 44;
                    if (check == 1)
                    {
                        player.TriggerEvent("Client:PlaySoundPeep2");
                        int salary = 6 * (122 + rnd.Next(0, 5));
                        SendNotificationWithoutButton(player, $"Alle Kühe wurden gemolken, du erhälst für den nächsten Payday {salary + bonus}$ gutgeschrieben!", "success", "top-left", 5200);
                        character.nextpayday += salary + bonus;
                    }
                    else if (check == 2)
                    {
                        player.TriggerEvent("Client:PlaySoundPeep2");
                        int salary = 38 * (28 + rnd.Next(0, 5));
                        SendNotificationWithoutButton(player, $"Das Weizen wurde erfolgreich geerntet, du erhälst für den nächsten Payday {salary + bonus}$ gutgeschrieben!", "success", "top-left", 5200);
                        character.nextpayday += salary + bonus;
                    }
                    else
                    {
                        player.TriggerEvent("Client:PlaySoundPeep2");
                        int salary = 8 * (127 + rnd.Next(0, 6));
                        SendNotificationWithoutButton(player, $"Die Tomaten wurden erfolgreich geerntet, du erhälst für den nächsten Payday {salary + bonus}$ gutgeschrieben!", "success", "top-left", 5200);
                        character.nextpayday += salary + bonus;
                    }
                    if (player.HasData("Player:FarmerRoute"))
                    {
                        player.ResetData("Player:FarmerRoute");
                    }
                    if (character.farmingskill < 150)
                    {
                        character.farmingskill++;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[FarmerFinish]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CowMilking")]
        public static void OnCowMilking(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character != null && character.job == 7)
                {
                    player.SetSharedData("Player:AnimData", "PROP_HUMAN_BUM_BIN");
                    player.TriggerEvent("Client:StartLockpicking", 5, "milking", "Kuh wird gemolken...");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCowMilking]: " + e.ToString());
            }
        }

        //Driving school
        [RemoteEvent("Server:StopAmmu")]
        public static void OnStopAmmu(Player player)
        {
            try
            {
                Helper.OnStopAnimation2(player);
                player.TriggerEvent("Client:HideCursor");
                player.TriggerEvent("Client:PlayerFreeze", false);
                player.TriggerEvent("Client:ShowHud");
                player.SetData<int>("Player:AmmuQuiz", 0);
                player.ResetData("Player:AmmuQuiz");
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStopAmmu]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartCar")]
        public static void OnStartCar(Player player, int id)
        {
            try
            {
                Vector3[] spawnVehicle;
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (character != null)
                {
                    Helper.OnStopAnimation2(player);
                    player.TriggerEvent("Client:HideCursor");
                    player.TriggerEvent("Client:PlayerFreeze", false);
                    player.TriggerEvent("Client:ShowHud");
                    if (id > -1)
                    {
                        player.SetData<int>("Player:CarQuiz", 1);
                    }
                    if (id == 1)
                    {
                        spawnVehicle = new Vector3[4]
                                     { new Vector3(-713.0309, -1272.7114, 4.369912-0.025),
                                       new Vector3(-710.5208, -1274.7017, 4.3699875-0.025),
                                       new Vector3(-708.1022, -1276.8584, 4.369979-0.025),
                                       new Vector3(-705.5301, -1278.9525, 4.369596-0.025)};

                        Random rand = new Random();
                        int index = rand.Next(4);
                        tempData.jobVehicle = Cars.createNewCar("sentinel", spawnVehicle[index], 140.46222f, 28, 28, "LS-S-155" + player.Id, "Fahrschule", true, true, false);
                        tempData.jobVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~r~Fahrschule");
                        SendNotificationWithTimer(player, "Praktische Führerscheinprüfung", "Willkommen bei der praktischen Führerscheinprüfung, unsere heutige Route habe ich im Navigerät eingegeben. Bitte achte auf deine Geschwindigkeit (/limitspeed) und vorallem auf unsere Sicherheit! Viel Erfolg!", 5250);
                    }
                    else if (id == 2)
                    {
                        spawnVehicle = new Vector3[4]
                                     { new Vector3(-717.02435, -1292.2317, 4.5131335-0.025),
                                       new Vector3(-719.2209, -1294.804, 4.513582-0.025),
                                       new Vector3(-721.3435, -1297.4855, 4.5180326-0.025),
                                       new Vector3(-723.39905, -1299.7823, 4.51139-0.025)};

                        Random rand = new Random();
                        int index = rand.Next(4);
                        tempData.jobVehicle = Cars.createNewCar("akuma", spawnVehicle[index], 52.38735f, 28, 28, "LS-S-155" + player.Id, "Fahrschule", true, true, false);
                        tempData.jobVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~r~Fahrschule");
                        SendNotificationWithTimer(player, "Praktische Führerscheinprüfung", "Willkommen bei der praktischen Motorradscheinprüfung, unsere heutige Route habe ich im Navigerät eingegeben. Bitte achte auf deine Geschwindigkeit (/limitspeed) und vorallem auf unsere Sicherheit! Viel Erfolg!", 5250);
                    }
                    else if (id == 3)
                    {
                        tempData.jobVehicle = Cars.createNewCar("pounder", new Vector3(-774.3099, -1308.4923, 5.073449), -40.524742f, 28, 28, "LS-S-155" + player.Id, "Fahrschule", true, true, false);
                        tempData.jobVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~r~Fahrschule");
                        SendNotificationWithTimer(player, "Praktische Truckerscheinprüfung", "Willkommen bei der praktischen Truckerscheinprüfung, unsere heutige Route habe ich im Navigerät eingegeben. Bitte achte auf deine Geschwindigkeit (/limitspeed) und vorallem auf unsere Sicherheit! Viel Erfolg!", 5250);
                    }
                    else if (id == 4)
                    {
                        tempData.jobVehicle = Cars.createNewCar("dinghy", new Vector3(-742.4566, -1346.8132, 0.12037361), -132.06824f, 28, 28, "LS-S-155" + player.Id, "Fahrschule", true, true, false);
                        tempData.jobVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~r~Fahrschule");
                        SendNotificationWithTimer(player, "Praktische Bootsscheinprüfung", "Willkommen bei der praktischen Bootsscheinprüfung, unsere heutige Route habe ich im Navigerät eingegeben. Bitte achte auf deine Geschwindigkeit (/limitspeed) und vorallem auf unsere Sicherheit! Viel Erfolg!", 5250);
                    }
                    else if (id == 5)
                    {
                        SetPlayerPosition(player, new Vector3(-1700.757, -2857.4375, 13.944447 + 0.15));
                        NAPI.Task.Run(() =>
                        {
                            tempData.jobVehicle = Cars.createNewCar("havok", new Vector3(-1704.8922, -2851.095, 14.265111), -31.0317f, 28, 28, "LS-S-155" + player.Id, "Fahrschule", true, true, false);
                            tempData.jobVehicle.Dimension = 0;
                            player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~r~Fahrschule");
                        }, delayTime: 375);
                        SendNotificationWithTimer(player, "Praktische Flugscheinprüfung", "Willkommen bei der praktischen Flugscheinprüfung, unsere heutige Route habe ich im Navigerät eingegeben. Der erste Punkt befindet sich auf. ca 300m Höhe (( Lila Markierungen )), bitte achte auf deine Flughöhe vorallem auf unsere Sicherheit! Viel Erfolg!", 5550);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartCar]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:FinishCar")]
        public static void OnFinishCar(Player player, int id)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character != null)
                {
                    if (player.IsInVehicle)
                    {
                        player.WarpOutOfVehicle();
                    }
                    NAPI.Task.Run(() =>
                    {
                        SetPlayerPosition(player, new Vector3(-710.9736, -1304.975, 5.1126294));
                        player.Heading = 159.13942f;
                        player.ResetData("Player:CarQuiz");
                        if (id == -1)
                        {
                            SendNotificationWithTimer(player, "Nicht bestanden!", "Du hast unerlaubte Hilfsmittel benutzt, du hast leider nicht bestanden!", 3250);
                        }
                        else if (id == 1)
                        {
                            Helper.CreateUserTimeline(account.id, character.id, $"Führerscheinprüfung bestanden!", 4);
                            SetAndGetCharacterLicense(player, 0, 1);
                            SendNotificationWithTimer(player, "Bestanden!", "Herzlichen Glückwunsch, du hast die Prüfung bestanden! Hier dein Führerschein!", 3250);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                            if (account.faqarray[6] == "0")
                            {
                                account.faqarray[6] = "1";
                            }
                        }
                        else if (id == 2)
                        {
                            Helper.CreateUserTimeline(account.id, character.id, $"Motorradscheinprüfung bestanden!", 4);
                            SetAndGetCharacterLicense(player, 1, 1);
                            SendNotificationWithTimer(player, "Bestanden!", "Herzlichen Glückwunsch, du hast die Prüfung bestanden! Hier dein Motorradschein!", 3250);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                        }
                        else if (id == 3)
                        {
                            Helper.CreateUserTimeline(account.id, character.id, $"Truckerscheinprüfung bestanden!", 4);
                            SetAndGetCharacterLicense(player, 2, 1);
                            SendNotificationWithTimer(player, "Bestanden!", "Herzlichen Glückwunsch, du hast die Prüfung bestanden! Hier dein Truckerschein!", 3250);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                        }
                        else if (id == 4)
                        {
                            Helper.CreateUserTimeline(account.id, character.id, $"Bootsscheinprüfung bestanden!", 4);
                            SetAndGetCharacterLicense(player, 3, 1);
                            SendNotificationWithTimer(player, "Bestanden!", "Herzlichen Glückwunsch, du hast die Prüfung bestanden! Hier dein Bootsschein!", 3250);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                        }
                        else if (id == 5)
                        {
                            Helper.CreateUserTimeline(account.id, character.id, $"Flugscheinprüfung bestanden!", 4);
                            SetAndGetCharacterLicense(player, 4, 1);
                            SendNotificationWithTimer(player, "Bestanden!", "Herzlichen Glückwunsch, du hast die Prüfung bestanden! Hier dein Flugschein!", 3250);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                        }
                        if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                        {
                            tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                        }
                        tempData.jobVehicle.Delete();
                        tempData.jobVehicle = null;
                    }, delayTime: 95);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnFinishCar]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:AbortSound")]
        public static void OnAbortSound(Player player)
        {
            try
            {
                if (player.HasData("Player:MusicBizz"))
                {
                    Business bizz = Business.GetBusinessById(player.GetData<int>("Player:MusicBizz"));
                    if (bizz != null)
                    {
                        bizz.musicPlayer = null;
                    }
                    player.ResetData("Player:MusicBizz");
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnAbortSound]: " + e.ToString());
            }
        }

        //Outfits
        [RemoteEvent("Server:Deleteoutfit")]
        public static void OnDeleteOutfit(Player player, int id)
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "DELETE FROM outfits WHERE id = @id LIMIT 1";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnDeleteOutfit]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:CreateOutfit")]
        public static void OnCreateOutfit(Player player, string name, string json1, string json2)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null) return;

                Outfits outfit = new Outfits();
                outfit.name = name;
                outfit.owner = "faction-" + character.id;
                outfit.json1 = json1;
                outfit.json2 = json2;

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                db.Insert(outfit);

                List<Outfits> outfitList = new List<Outfits>();
                foreach (Outfits outfit2 in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 3", "faction-" + character.id))
                {
                    outfitList.Add(outfit2);
                }
                player.TriggerEvent("Client:UpdateOutfits", NAPI.Util.ToJson(outfitList));

                Helper.SendNotificationWithoutButton(player, "Das neue Outfit wurde erstellt!", "success", "top-left", 2500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnCreateOutfit]: " + e.ToString());
            }
        }

        //Speedcamera
        [RemoteEvent("Server:SpeedCheck")]
        public static void OnSpeedCheck(Player player, int speed)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (player.IsInVehicle && speed > 0 && player.Vehicle.NumberPlate.Length > 3)
                {
                    character.sapoints++;
                    MySqlCommand command = General.Connection.CreateCommand();
                    command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                    command.Parameters.AddWithValue("@name", character.name);
                    command.Parameters.AddWithValue("@police", "Blitzer");
                    command.Parameters.AddWithValue("@text", $"+1 Punkt in SA, Grund: Geblitzt mit {speed} KM/H");
                    command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                    command.Parameters.AddWithValue("@commentary", 0);
                    command.ExecuteNonQuery();

                    if (character.sapoints >= 10)
                    {
                        command.CommandText = "INSERT INTO policefile (name, police, text, timestamp, commentary) VALUES (@name, @police, @text, @timestamp, @commentary)";
                        command.Parameters.AddWithValue("@name", character.name);
                        command.Parameters.AddWithValue("@police", "Blitzer");
                        command.Parameters.AddWithValue("@text", $"Führer/Motorradsch/Truckereinsperre (14 Tage), wegen zu vieler Punkte in SA");
                        command.Parameters.AddWithValue("@timestamp", Helper.UnixTimestamp());
                        command.Parameters.AddWithValue("@commentary", 0);
                        command.ExecuteNonQuery();

                        Helper.SetAndGetCharacterLicense(player, 0, Helper.UnixTimestamp() + (14 * 86400), character);
                        Helper.SetAndGetCharacterLicense(player, 1, Helper.UnixTimestamp() + (14 * 86400), character);
                        Helper.SetAndGetCharacterLicense(player, 2, Helper.UnixTimestamp() + (14 * 86400), character);

                        character.sapoints = 0;
                    }

                    Invoices invoice = new Invoices();
                    invoice.value = speed * 4;
                    invoice.empfänger = character.name;
                    invoice.modus = "Privatrechnung";
                    invoice.text = $"Strafgebühr {invoice.value}$, geblitzt mit {speed} KM/H";
                    invoice.banknumber = "SA3701-100000";
                    invoice.timestamp = Helper.UnixTimestamp();

                    PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                    db.Insert(invoice);

                    Helper.invoicesList.Add(invoice);

                    Smartphone smartphone = SmartphoneController.GetSmartPhoneByNumber(character.lastsmartphone);
                    if (smartphone != null && smartphone.akku > 0)
                    {
                        BankController.OnLoadInvoices(player, false);
                        Helper.SendNotificationWithoutButton(player, $"Du wurdest geblitzt und hast eine neue Rechnung[{invoice.id}] in Höhe von {invoice.value}$ erhalten!", "info", "top-left", 6500);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSpeedCheck]: " + e.ToString());
            }
        }

        //Weapon license/Shooting Range
        [RemoteEvent("Server:StartRange")]
        public static void OnStartRange(Player player, int set = 0, int seconds = 0)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (tempData == null || character == null) return;
                Helper.OnStopAnimation2(player);
                if (set == 0)
                {
                    player.TriggerEvent("Client:HideCursor");
                    player.TriggerEvent("Client:PlayerFreeze", false);
                    player.TriggerEvent("Client:ShowHud");
                    SetPlayerPosition(player, new Vector3(8.953279, -1097.588, 29.797028));
                    player.Rotation = new Vector3(0.0, 0.0, -110.08769);
                    SendNotificationWithTimer(player, "Praktische Waffenscheinprüfung", "Du erhältst 30 Patronen und musst mind. 25 Ziele treffen (Mittlerer roter Punkt), erfüllst du diese Aufgabe hast du die Waffenscheinprüfung bestanden! Wenn du fertig bist, kannst du die Shootingrange einfach verlassen!", 4500);
                    tempData.jobColshape = Events.ammuCol;
                    player.SetData<int>("Player:AmmuQuiz", 2);
                    NAPI.Player.GivePlayerWeapon(player, WeaponHash.Pistol, 30);
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Pistol);
                }
                else
                {
                    if (set == 1)
                    {
                        if (player.GetData<int>("Player:AmmuQuiz") == 1)
                        {
                            NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Pistol, 0);
                            NAPI.Player.RemovePlayerWeapon(player, WeaponHash.Pistol);
                            if (tempData.jobColshape != null)
                            {
                                tempData.jobColshape = null;
                            }
                            player.TriggerEvent("Client:HideCursor");
                            player.TriggerEvent("Client:PlayerFreeze", false);
                            player.SetData<int>("Player:AmmuQuiz", 0);
                            player.ResetData("Player:AmmuQuiz");
                            player.TriggerEvent("Client:ShowHud");
                            player.TriggerEvent("Client:StopRange");
                            Helper.SendNotificationWithoutButton(player, "Waffenscheinprüfung nicht bestanden!", "error", "top-left", 4000);
                        }
                    }
                    else if (set == 2)
                    {
                        if (player.GetData<int>("Player:AmmuQuiz") == 2)
                        {
                            NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Pistol, 0);
                            NAPI.Player.RemovePlayerWeapon(player, WeaponHash.Pistol);
                            if (tempData.jobColshape != null)
                            {
                                tempData.jobColshape = null;
                            }
                            player.SetData<int>("Player:AmmuQuiz", 0);
                            player.ResetData("Player:AmmuQuiz");
                            player.TriggerEvent("Client:StopRange");
                            player.TriggerEvent("Client:PlaySoundSuccessNormal");
                            SendNotificationWithTimer(player, "Bestanden!", "Du hast die Waffenscheinprüfung bestanden und erhältst deinen Waffenschein, viel Erfolg damit!", 4500);
                            SetAndGetCharacterLicense(player, 5, 1);
                        }
                    }
                    else
                    {
                        player.SetData<int>("Player:AmmuQuiz", 0);
                        player.ResetData("Player:AmmuQuiz");
                        player.TriggerEvent("Client:StopRange");
                        player.TriggerEvent("Client:PlaySoundSuccessNormal");
                        SendNotificationWithTimer(player, "Alles getoffen!", $"Du hast alle Ziele in {seconds} Sekunden getroffen!", 4500);
                        account.shootingrange = seconds;
                        NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnPlayerPressF]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:SetCloth")]
        public static void OnSetCloth(Player player, int index)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character != null)
                {
                    JObject obj = null;
                    if (character.factionduty == false)
                    {
                        obj = JObject.Parse(character.json);
                    }
                    else
                    {
                        obj = JObject.Parse(character.dutyjson);
                    }
                    string[] clothingArray = new string[8];
                    clothingArray = character.clothing.Split(",");
                    string dict = "";
                    string anim = "";
                    int time = 0;
                    if (index == 1)
                    {
                        if (NAPI.Player.GetPlayerAccessoryDrawable(player, 0) != 255)
                        {
                            clothingArray[0] = "0";
                            NAPI.Player.ClearPlayerAccessory(player, 0);
                            SendNotificationWithoutButton(player, "Kopfbedeckung ausgezogen!", "success", "top-left", 1150);
                            dict = "missheist_agency2ahelmet";
                            anim = "take_off_helmet_stand";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            clothingArray[0] = "1";
                            if ((int)obj["clothing"][7] == -1 || (int)obj["clothing"][7] == 255)
                            {
                                SendNotificationWithoutButton(player, "Keine Kopfbedeckung vorhanden!", "error", "top-left", 1150);
                                return;
                            }
                            NAPI.Player.SetPlayerAccessory(player, 0, (int)obj["clothing"][7], (int)obj["clothingColor"][7]);
                            SendNotificationWithoutButton(player, "Kopfbedeckung angezogen!", "success", "top-left", 1150);
                            dict = "mp_masks@standard_car@ds@";
                            anim = "put_on_mask";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 2)
                    {
                        if (NAPI.Player.GetPlayerAccessoryDrawable(player, 1) != 255)
                        {
                            clothingArray[1] = "0";
                            NAPI.Player.ClearPlayerAccessory(player, 1);
                            SendNotificationWithoutButton(player, "Sonnenbrille ausgezogen!", "success", "top-left", 1150);
                            dict = "clothingspecs";
                            anim = "take_off";
                            time = 1400;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            if ((int)obj["clothing"][6] == -1 || (int)obj["clothing"][6] == 255)
                            {
                                SendNotificationWithoutButton(player, "Keine Sonnenbrille vorhanden!", "error", "top-left", 1150);
                                return;
                            }
                            clothingArray[1] = "1";
                            NAPI.Player.SetPlayerAccessory(player, 1, (int)obj["clothing"][6], (int)obj["clothingColor"][6]);
                            SendNotificationWithoutButton(player, "Sonnenbrille angezogen!", "success", "top-left", 1150);
                            dict = "clothingspecs";
                            anim = "take_off";
                            time = 1400;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 3)
                    {
                        if (NAPI.Player.GetPlayerClothesDrawable(player, 11) != 15)
                        {
                            clothingArray[2] = "0";
                            NAPI.Player.SetPlayerClothes(player, 11, 15, 0);
                            SendNotificationWithoutButton(player, "Oberbekleidung ausgezogen!", "success", "top-left", 1150);
                            dict = "missmic4";
                            anim = "michael_tux_fidget";
                            time = 1500;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            clothingArray[2] = "1";
                            NAPI.Player.SetPlayerClothes(player, 11, (int)obj["clothing"][0], (int)obj["clothingColor"][0]);
                            SendNotificationWithoutButton(player, "Oberbekleidung angezogen!", "success", "top-left", 1150);
                            dict = "missmic4";
                            anim = "michael_tux_fidget";
                            time = 1500;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 4)
                    {
                        if (NAPI.Player.GetPlayerClothesDrawable(player, 8) != 15)
                        {
                            clothingArray[3] = "0";
                            NAPI.Player.SetPlayerClothes(player, 8, 15, 0);
                            SendNotificationWithoutButton(player, "T-Shirt ausgezogen!", "success", "top-left", 1150);
                            dict = "clothingtie";
                            anim = "try_tie_negative_a";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            clothingArray[3] = "1";
                            NAPI.Player.SetPlayerClothes(player, 8, (int)obj["clothing"][4], (int)obj["clothingColor"][4]);
                            SendNotificationWithoutButton(player, "T-Shirt angezogen!", "success", "top-left", 1150);
                            dict = "clothingtie";
                            anim = "try_tie_negative_a";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 5)
                    {
                        if (NAPI.Player.GetPlayerClothesDrawable(player, 6) != 34 && NAPI.Player.GetPlayerClothesDrawable(player, 6) != 35)
                        {
                            clothingArray[4] = "0";
                            if (character.gender == 1)
                            {
                                NAPI.Player.SetPlayerClothes(player, 6, 34, 0);
                            }
                            else
                            {
                                NAPI.Player.SetPlayerClothes(player, 6, 35, 0);
                            }
                            SendNotificationWithoutButton(player, "Schuhe ausgezogen!", "success", "top-left", 1150);
                            dict = "random@domestic";
                            anim = "pickup_low";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            clothingArray[4] = "1";
                            NAPI.Player.SetPlayerClothes(player, 6, (int)obj["clothing"][3], (int)obj["clothingColor"][3]);
                            SendNotificationWithoutButton(player, "Schuhe angezogen!", "success", "top-left", 1150);
                            dict = "random@domestic";
                            anim = "pickup_low";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 6)
                    {
                        if (NAPI.Player.GetPlayerClothesDrawable(player, 4) != 14)
                        {
                            clothingArray[5] = "0";
                            NAPI.Player.SetPlayerClothes(player, 4, 14, 0);
                            SendNotificationWithoutButton(player, "Hose ausgezogen!", "success", "top-left", 1150);
                            dict = "re@construction";
                            anim = "out_of_breath";
                            time = 1300;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            clothingArray[5] = "1";
                            NAPI.Player.SetPlayerClothes(player, 4, (int)obj["clothing"][2], (int)obj["clothingColor"][2]);
                            SendNotificationWithoutButton(player, "Hose angezogen!", "success", "top-left", 1150);
                            dict = "re@construction";
                            anim = "out_of_breath";
                            time = 1300;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 7)
                    {
                        if (NAPI.Player.GetPlayerAccessoryDrawable(player, 2) != 255 || NAPI.Player.GetPlayerAccessoryDrawable(player, 6) != 255 || NAPI.Player.GetPlayerAccessoryDrawable(player, 7) != 255 || NAPI.Player.GetPlayerClothesDrawable(player, 5) != 0 || NAPI.Player.GetPlayerClothesDrawable(player, 7) != 0)
                        {
                            clothingArray[6] = "0";
                            NAPI.Player.ClearPlayerAccessory(player, 2);
                            NAPI.Player.ClearPlayerAccessory(player, 6);
                            NAPI.Player.ClearPlayerAccessory(player, 7);
                            NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                            NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                            SendNotificationWithoutButton(player, "Accessoires ausgezogen!", "success", "top-left", 1150);
                            dict = "nmt_3_rcm-10";
                            anim = "cs_nigel_dual-10";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            clothingArray[6] = "1";
                            NAPI.Player.SetPlayerAccessory(player, 2, (int)obj["clothing"][9], (int)obj["clothingColor"][9]);
                            NAPI.Player.SetPlayerAccessory(player, 6, (int)obj["clothing"][10], (int)obj["clothingColor"][10]);
                            NAPI.Player.SetPlayerAccessory(player, 7, (int)obj["clothing"][11], (int)obj["clothingColor"][11]);
                            NAPI.Player.SetPlayerClothes(player, 5, (int)obj["clothing"][5], (int)obj["clothingColor"][5]);
                            NAPI.Player.SetPlayerClothes(player, 7, (int)obj["clothing"][12], (int)obj["clothingColor"][12]);
                            SendNotificationWithoutButton(player, "Accessoires angezogen!", "success", "top-left", 1150);
                            dict = "nmt_3_rcm-10";
                            anim = "cs_nigel_dual-10";
                            time = 1200;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    else if (index == 8)
                    {
                        if (NAPI.Player.GetPlayerClothesDrawable(player, 1) != 0)
                        {
                            clothingArray[7] = "0";
                            NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                            SendNotificationWithoutButton(player, "Maske ausgezogen!", "success", "top-left", 1150);
                            dict = "mp_masks@standard_car@ds@";
                            anim = "put_on_mask";
                            time = 850;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                        else
                        {
                            if (character.faction != 2 || character.factionduty == false)
                            {
                                if ((int)obj["clothing"][8] == -1 || (int)obj["clothing"][8] == 0 || (int)obj["clothing"][8] == 255)
                                {
                                    SendNotificationWithoutButton(player, "Keine Maske vorhanden!", "error", "top-left", 1150);
                                    return;
                                }
                            }
                            clothingArray[7] = "1";
                            if (character.faction == 2 && character.factionduty == true)
                            {
                                if (character.gender == 1)
                                {
                                    NAPI.Player.SetPlayerClothes(player, 1, 209, 0);
                                }
                                else
                                {
                                    NAPI.Player.SetPlayerClothes(player, 1, 224, 0);
                                }
                            }
                            else
                            {
                                NAPI.Player.SetPlayerClothes(player, 1, (int)obj["clothing"][8], (int)obj["clothingColor"][8]);
                            }
                            SendNotificationWithoutButton(player, "Maske angezogen!", "success", "top-left", 1150);
                            dict = "mp_masks@standard_car@ds@";
                            anim = "put_on_mask";
                            time = 850;
                            Helper.PlayShortAnimation(player, dict, anim, time);
                        }
                    }
                    character.clothing = String.Join(",", clothingArray);
                }
                else
                {
                    SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error", "top-left", 1150);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnSetCloth]: " + e.ToString());
            }
        }

        public static string SetAndGetCharacterLicense(Player player, int getfield, int setting, Character getCharacter = null)
        {
            try
            {
                Character character = null;
                if (getCharacter == null)
                {
                    character = GetCharacterData(player);
                }
                else
                {
                    character = getCharacter;
                }
                if (character == null) return "0";
                string[] licArray = new string[6];
                licArray = character.licenses.Split("|");
                if (setting != 1337)
                {
                    licArray[getfield] = "" + setting;
                    character.licenses = $"{licArray[0]}|{licArray[1]}|{licArray[2]}|{licArray[3]}|{licArray[4]}|{licArray[5]}";
                    return "1";
                }
                else
                {
                    return "" + licArray[getfield];
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SetAndGetCharacterLicense]: " + e.ToString());
            }
            return "0";
        }

        [RemoteEvent("Server:GetTrash")]
        public static void OnGetTrash(Player player, bool check)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || (player.HasData("Player:LookingForTrash") && player.GetData<bool>("Player:LookingForTrash") == true)) return;

                player.SetData<bool>("Player:LookingForTrash", true);
                player.SetSharedData("Player:AnimData", "PROP_HUMAN_BUM_BIN");

                NAPI.Task.Run(() =>
                {
                    player.SetData<bool>("Player:LookingForTrash", false);
                    NAPI.Player.StopPlayerAnimation(player);
                    if (check == true)
                    {
                        SendNotificationWithoutButton(player, "Die Mülltonne war leer!", "error", "top-left", 3250);
                    }
                    else
                    {
                        Random rand = new Random();
                        int index = rand.Next(2);
                        int index2 = rand.Next(4) + 1;
                        int index3 = rand.Next(4);
                        if (index == 1)
                        {
                            if (!ItemsController.CanPlayerHoldItem(player, 150 * index2))
                            {
                                SendNotificationWithoutButton(player, "Du hast keinen Platz mehr bei dir im Inventar!", "error", "top-end");
                                return;
                            }
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "Pfandflasche", "Player", index2, ItemsController.GetFreeItemID(player));
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            SendNotificationWithoutButton(player, $"Du hast {index2} Pfandflaschen gefunden!", "success", "top-left", 3250);
                        }
                        else
                        {
                            if (index3 == 2)
                            {
                                if (Helper.GetRandomPercentage(50))
                                {
                                    SendNotificationWithoutButton(player, "Du hast eine Dirty Dancing Blue Ray gefunden und direkt weggeworfen!", "error", "top-left", 3250);
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Du hast das 9€ Ticket gefunden und direkt weggeworfen!", "error", "top-left", 3250);
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Die Mülltonne war leer!", "error", "top-left", 3250);
                            }
                        }
                    }
                }, delayTime: 4500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGetTrash]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:GetSoda")]
        public static void OnGetSoda(Player player, int soda)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null) return;
                Business bizz = Business.GetBusinessById(16);
                if (bizz == null)
                {
                    SendNotificationWithoutButton(player, "Der Getränkeautomat ist leer!", "error");
                    return;
                }
                int price = Convert.ToInt32(30 * bizz.multiplier);
                if (bizz.products <= 0)
                {
                    SendNotificationWithoutButton(player, "Der Getränkeautomat ist leer!", "error");
                    return;
                }
                if (character.cash < price)
                {
                    SendNotificationWithoutButton(player, $"Du hast nicht genügenjd Geld dabei - {price}$!", "error");
                    return;
                }
                if (soda == 1)
                {
                    if (!ItemsController.CanPlayerHoldItem(player, 150))
                    {
                        SendNotificationWithoutButton(player, "Du hast keinen Platz mehr bei dir im Inventar!", "error", "top-end");
                        return;
                    }
                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Cola", "Player", 1, ItemsController.GetFreeItemID(player));
                    if (newitem != null)
                    {
                        tempData.itemlist.Add(newitem);
                    }
                }
                else
                {
                    if (!ItemsController.CanPlayerHoldItem(player, 150))
                    {
                        SendNotificationWithoutButton(player, "Du hast keinen Platz mehr bei dir im Inventar!", "error", "top-end");
                        return;
                    }
                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Sprunk", "Player", 1, ItemsController.GetFreeItemID(player));
                    if (newitem != null)
                    {
                        tempData.itemlist.Add(newitem);
                    }
                }
                Helper.PlayShortAnimation(player, "mini@sprunk", "PLYR_BUY_DRINK_PT1", 1850);
                CharacterController.SetMoney(player, -price);
                Business.ManageBizzCash(bizz, price / 2, true);
                bizz.selled++;
                if (bizz.selled >= 3)
                {
                    bizz.selled = 0;
                    bizz.products--;
                }
                SendNotificationWithoutButton(player, $"Es ist ein Getränk für {price}$ aus dem Automat geploppt!", "success", "top-left", 3250);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnGetSoda]: " + e.ToString());
            }
        }

        //Rollerverleih
        [RemoteEvent("Server:RollerVerleih")]
        public static void OnRollerVerleih(Player player, bool check)
        {
            try
            {
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);

                if (account == null || character == null)
                {
                    SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                    return;
                }

                if (check)
                {
                    if (character.cash < 150)
                    {
                        SendNotificationWithoutButton(player, "Du hast nicht genügend Geld dabei - 150$!", "error");
                        return;
                    }
                    if (IsInRangeOfPoint(player.Position, new Vector3(-522.10754, -258.09152, 35.10415), 10.0f))
                    {
                        player.TriggerEvent("Client:PlaySoundSuccessNormal");
                        CharacterController.SetMoney(player, -150);
                        SendNotificationWithoutButton(player, "Du hast dir einen Roller für 150$ gemietet!", "success", "top-end", 3500);
                        Vector3[] spawnFaggioArray = new Vector3[4]
                                                   { new Vector3(-521.5794, -262.30298, 34.97372-0.1),
                                                     new Vector3(-518.625, -261.41068, 34.987602-0.1),
                                                     new Vector3(-515.1545, -259.99585, 35.01732-0.1),
                                                     new Vector3(-510.93433, -258.55273, 35.061962-0.1) };
                        Random rand = new Random();
                        int index = rand.Next(4);
                        Random rand2 = new Random();
                        tempData.rentVehicle = Cars.createNewCar("faggio", spawnFaggioArray[index], -133.2003f, rand2.Next(0, 159), rand2.Next(0, 159), "LS-S-100" + player.Id, "Rollerverleih", true, true, false);
                        tempData.rentVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.rentVehicle, (int)VehicleSeat.Driver);
                    }
                    else if (IsInRangeOfPoint(player.Position, new Vector3(479.0879, -1861.0743, 27.460703), 10.0f))
                    {
                        player.TriggerEvent("Client:PlaySoundSuccessNormal");
                        CharacterController.SetMoney(player, -150);
                        SendNotificationWithoutButton(player, "Du hast dir einen Roller für 150$ gemietet!", "success", "top-end", 3500);
                        Random rand2 = new Random();
                        tempData.rentVehicle = Cars.createNewCar("faggio", new Vector3(476.3351, -1853.3799, 26.867142), -18.604107f, rand2.Next(0, 159), rand2.Next(0, 159), "LS-S-100" + player.Id, "Rollerverleih", true, true, false);
                        tempData.rentVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.rentVehicle, (int)VehicleSeat.Driver);
                    }
                    else if (IsInRangeOfPoint(player.Position, new Vector3(-682.462, 319.9259, 83.083145), 1.85f))
                    {
                        player.TriggerEvent("Client:PlaySoundSuccessNormal");
                        CharacterController.SetMoney(player, -150);
                        SendNotificationWithoutButton(player, "Du hast dir einen Rollstuhl für 150$ gemietet!", "success", "top-end", 3500);
                        Random rand2 = new Random();
                        tempData.rentVehicle = Cars.createNewCar("iak_wheelchair", new Vector3(-681.25146, 319.29184, 82.57396), -161.80653f, rand2.Next(0, 159), rand2.Next(0, 159), "LS-S-100" + player.Id, "Rollstuhlverleih", false, true, false);
                        tempData.rentVehicle.Dimension = 0;
                        player.SetIntoVehicle(tempData.rentVehicle, (int)VehicleSeat.Driver);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[RollerVerleih]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:Interact")]
        public static void OnInteract(Player player)
        {
            player.TriggerEvent("Client:HideWheel2");
            //Häuser
            House house = House.GetClosestHouse(player);
            if (house != null)
            {
                if ((house.status == 3 || house.status == 4) && !House.HasPlayerHouseKey(player, house.id))
                {
                    TempData tempData = Helper.GetCharacterTempData(player);
                    player.TriggerEvent("Client:PlayerFreeze", true);
                    player.TriggerEvent("Client:CallInput2", "Hausmiete", $"Möchtest du dich für {house.rental}$ in das Haus einmieten?", "RentHouse", "Einmieten", "Anklopfen");
                    return;
                }
                else
                {
                    Helper.PlayShortAnimation(player, "timetable@jimmy@doorknock@", "knockdoor_idle", 1550);
                    SendNotificationWithoutButton(player, "Du klopfst an der Türe!", "success");
                    Helper.SendHouseMessage(house.id, "!{#EE82EE}* Jemand klopft an der Tür!");
                }
                return;
            }
            //Sonstiges
            SendNotificationWithoutButton(player, "Du kannst hier mit nichts interagieren!", "error");
        }

        //SelectionWheel
        //OnVehicleEngine
        [RemoteEvent("Server:SelectWheel")]
        public static void OnSelectWheel(Player player, string use, int amount)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                player.TriggerEvent("Client:HideWheel2");
                if (use.Length > 3)
                {
                    switch (use)
                    {
                        case "lock":
                            {
                                OnEntityLock(player);
                                break;
                            }
                        case "money":
                            {
                                Player nearestplayer = GetClosestPlayer(player, 2.5f);
                                if (nearestplayer != null)
                                {
                                    Character character = GetCharacterData(player);
                                    Character character2 = GetCharacterData(nearestplayer);
                                    if (amount <= 0 || amount > character.cash)
                                    {
                                        SendNotificationWithoutButton(player, "Ungültige Summe!", "error");
                                        return;
                                    }
                                    Commands.cmd_animation(player, "give", true);
                                    CharacterController.SetMoney(nearestplayer, amount);
                                    CharacterController.SetMoney(player, -amount);
                                    SendNotificationWithoutButton(player, $"Du hast dem Spieler {amount}$ gegeben!", "success");
                                    SendNotificationWithoutButton(nearestplayer, $"Du hast {amount}$ bekommen!", "success");
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Es befindet sich kein Spieler in der Nähe!", "error");
                                }
                                break;
                            }
                        case "perso":
                            {
                                Player getPlayer = GetClosestPlayer(player, 2.5f);
                                Personalausweis perso = new Personalausweis();
                                Commands.cmd_animation(player, "give", true);
                                if (getPlayer == null)
                                {
                                    getPlayer = player;
                                }
                                Character getCharacter = Helper.GetCharacterData(player);
                                Character getCharacter2 = Helper.GetCharacterData(getPlayer);
                                if (getCharacter.death == true || getCharacter2.death == true)
                                {
                                    SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                                    return;
                                }
                                perso.id = getCharacter.id;
                                if (tempData.undercover == "")
                                {
                                    perso.name = getCharacter.name;
                                }
                                else
                                {
                                    perso.name = tempData.undercover;
                                }
                                perso.birthday = getCharacter.birth;
                                perso.size = getCharacter.size;
                                if (tempData.undercover == "")
                                {
                                    perso.from = getCharacter.origin;
                                }
                                else
                                {
                                    perso.from = "Los Santos";
                                }
                                perso.eyecolor = getCharacter.eyecolor;
                                if (getPlayer != player)
                                {
                                    SendNotificationWithoutButton(player, "Du zeigst jemanden deinen Personalausweis!", "success");
                                }
                                getPlayer.TriggerEvent("Client:ShowPerso", NAPI.Util.ToJson(perso));
                                break;
                            }
                        case "lics":
                            {
                                Player getPlayer = GetClosestPlayer(player, 2.5f);
                                Lics lics = new Lics();
                                Commands.cmd_animation(player, "give", true);
                                if (getPlayer == null)
                                {
                                    getPlayer = player;
                                }
                                Character getCharacter = Helper.GetCharacterData(player);
                                Character getCharacter2 = Helper.GetCharacterData(getPlayer);
                                if (getCharacter.death == true || getCharacter2.death == true)
                                {
                                    SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                                    return;
                                }
                                if (getCharacter.death == true)
                                {
                                    SendNotificationWithoutButton(player, "Ungültiger Spieler!", "error");
                                    return;
                                }
                                lics.id = getCharacter.id;
                                if (tempData.undercover == "")
                                {
                                    lics.name = getCharacter.name;
                                }
                                else
                                {
                                    lics.name = tempData.undercover;
                                }
                                lics.birthday = getCharacter.birth;
                                if (getPlayer != player)
                                {
                                    SendNotificationWithoutButton(player, "Du zeigst jemanden deine Lizenzen!", "success");
                                }
                                getPlayer.TriggerEvent("Client:ShowLics", getCharacter.licenses, NAPI.Util.ToJson(lics));
                                break;
                            }
                        case "open":
                            {
                                if (player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                                {
                                    if (player.Vehicle.GetSharedData<float>("Vehicle:MaxFuel") == 0) return;
                                    if (player.Vehicle.EngineStatus == true)
                                    {
                                        SendNotificationWithoutButton(player, "Der Motor läuft bereits!", "error");
                                        return;
                                    }
                                    if (player.HasData("Player:WireCooldown"))
                                    {
                                        if (Helper.UnixTimestamp() < player.GetData<int>("Player:WireCooldown"))
                                        {
                                            SendNotificationWithoutButton(player, "Du kannst nur alle 25 Minuten ein Fahrzeug kurzschliessen!", "error");
                                            return;
                                        }
                                        player.ResetData("Player:Wirecooldown");
                                    }
                                    player.TriggerEvent("Client:ShowSpeedometer");
                                    player.TriggerEvent("Client:StartLockpicking", 14, "vehicle2", "Fahrzeug wird kurzgeschlossen...");
                                    player.TriggerEvent("Client:PlayerFreeze", true);
                                }
                                break;
                            }
                        case "invehicle":
                            {
                                if (!player.IsInVehicle)
                                {
                                    Player getPlayer = Helper.GetClosestPlayer(player, 2.15f);
                                    if (getPlayer != null)
                                    {
                                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                        Character character2 = Helper.GetCharacterData(getPlayer);
                                        if (character2 != null && character2.death && tempData2.followed == false && tempData2.follow == false && tempData.follow == false && tempData.followed == false && !getPlayer.IsInVehicle)
                                        {
                                            Commands.cmd_animation(player, "give", true);
                                            ItemsController.OnShowInventory(player, 2);
                                            return;
                                        }
                                        if ((tempData2.cuffed > 0 || character2.death == true || tempData.adminduty == true) && getPlayer.IsInVehicle)
                                        {
                                            Vector3 behindPlayer = Helper.GetPositionBehindOfPlayer(player, 0.75f);
                                            SendNotificationWithoutButton(player, "Du hast jemanden aus dem Fahrzeug gezogen!", "success");
                                            SendNotificationWithoutButton(getPlayer, "Du wurdest aus dem Fahrzeug gezogen!", "success");
                                            getPlayer.WarpOutOfVehicle();
                                            NAPI.Task.Run(() =>
                                            {
                                                SetPlayerPosition(getPlayer, behindPlayer);
                                                if (tempData2.cuffed > 0)
                                                {
                                                    getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                                }
                                                else if (character2.death == true)
                                                {
                                                    PlayerDeathAnim(getPlayer);
                                                }
                                                else
                                                {
                                                    Helper.OnStopAnimation(getPlayer);
                                                }
                                            }, delayTime: 1015);
                                            return;
                                        }
                                        if (tempData.followed == true)
                                        {
                                            player.TriggerEvent("Client:GetNearestSeat", "follow");
                                        }
                                        else
                                        {
                                            SendNotificationWithoutButton(player, "Du ziehst keinen mehr vor dir her/trägst keinen mehr!", "error");
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            catch (Exception e)
            {
                player.TriggerEvent("Client:HideWheel2");
                SendNotificationWithoutButton(player, "Ungültige Eingabe!", "error");
                Helper.ConsoleLog("error", $"[OnSelectWheel]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:NearestSeat")]
        public void OnNearestSeat(Player player, string flag, int seat)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                if (flag == "follow")
                {
                    if (!player.IsInVehicle)
                    {
                        if (seat != -1)
                        {
                            Player getPlayer = Helper.GetClosestPlayer(player, 3.75f);
                            if (getPlayer != null)
                            {
                                if (player.HasSharedData("Player:Follow") && tempData.followed == true)
                                {
                                    TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                    Vehicle veh = Helper.GetClosestVehicle(player, 6.5f);
                                    if (veh != null && veh.Locked == false)
                                    {
                                        tempData.followed = false;
                                        player.SetSharedData("Player:FollowStatus", 0);
                                        Helper.OnStopAnimation(player);
                                        Helper.OnStopAnimation(getPlayer);
                                        NAPI.Task.Run(() =>
                                        {
                                            player.SetSharedData("Player:Follow", "n/A");
                                            player.ResetSharedData("Player:Follow");
                                            player.ResetSharedData("Player:FollowStatus");
                                            tempData2.follow = false;
                                            if (tempData2.cuffed > 0)
                                            {
                                                getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                            }
                                            getPlayer.TriggerEvent("Client:PlayerFreeze", false);
                                            SendNotificationWithoutButton(player, "Du hast jemanden ins Fahrzeug geworfen!", "success");
                                            SendNotificationWithoutButton(getPlayer, "Jemand hat dich ins Fahrzeug geworfen", "success");
                                            getPlayer.SetIntoVehicle(veh, seat + 1);
                                        }, delayTime: 115);
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Fahrzeug/oder das Fahrzeug ist verschlossen!", "error");
                                    }
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Du ziehst keinen mehr vor dir her/trägst keinen mehr!", "error");
                                }
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Fahrzeug/keinen freien Sitzplatz gefunden!", "error");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnNearestSeat]: " + e.ToString());
            }
        }

        //Fahrzeugsystem
        public static void SetVehicleEngine(Vehicle vehicle, bool engine)
        {
            if (vehicle != null)
            {
                vehicle.EngineStatus = engine;
                vehicle.SetData<bool>("Vehicle:EngineStatus", engine);
            }
        }

        public static bool GetVehicleRights(Player player, Vehicle vehicle)
        {
            try
            {
                Account account = GetAccountData(player);
                Character character = GetCharacterData(player);
                int groupid = -1;
                int factionid = -1;
                if (account == null || character == null) return false;
                if (vehicle != null)
                {
                    TempData tempData = Helper.GetCharacterTempData(player);
                    if (tempData == null) return false;
                    //Ab Administrator alles öffnen
                    if (tempData.adminduty == true && account.adminlevel >= (int)Account.AdminRanks.Administrator) return true;
                    //Adminfahrzeug
                    if (tempData.adminvehicle != null && tempData.adminvehicle == vehicle) return true;
                    //Mietfahrzeuge
                    if (tempData.rentVehicle != null && tempData.rentVehicle == vehicle) return true;
                    //Jobfahrzeuge
                    if (tempData.jobVehicle != null && tempData.jobVehicle == vehicle) return true;
                    //Probefahrt
                    if (tempData.dealerShip != null && tempData.dealerShip == vehicle) return true;
                    //Andere
                    foreach (Cars car in Cars.carList)
                    {
                        if (car.vehicleHandle == vehicle)
                        {
                            if (car.vehicleData.owner.Contains("group"))
                            {
                                groupid = Convert.ToInt32(car.vehicleData.owner.Split("-")[1]);
                                if (character.mygroup == groupid)
                                {
                                    GroupsMembers groupMember = GroupsController.GetGroupMemberById(character.id, groupid);
                                    if (groupMember.rang >= car.vehicleData.rang) return true;
                                }
                            }
                            if (car.vehicleData.owner.Contains("faction"))
                            {
                                factionid = Convert.ToInt32(car.vehicleData.owner.Split("-")[1]);
                                if (character.faction == factionid)
                                {
                                    if (character.rang >= car.vehicleData.rang) return true;
                                }
                            }
                            foreach (Items item in tempData.itemlist)
                            {
                                if (item.description == "Fahrzeugschlüssel")
                                {
                                    string propString = $"{car.vehicleData.vehiclename}: {car.vehicleData.id}";
                                    if (item.props.ToLower() == propString.ToLower())
                                    {
                                        return true;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleRights]: " + e.ToString());
            }
            return false;
        }

        public static Vehicle GetVehicleById(int vehicleid)
        {
            try
            {
                foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
                {
                    if (vehicle != null && vehicle.Id == vehicleid)
                    {
                        return vehicle;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetVehicleById]: " + e.ToString());
            }
            return null;
        }

        //OnEntityLock
        [RemoteEvent("Server:OnEntityLock")]
        public static void OnEntityLock(Player player)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || tempData == null) return;
                //Fraktionstüren
                if (character.faction > 0 || tempData.adminduty == true)
                {
                    Doors door = DoorsController.GetClosestDoor(player, 1.55f);
                    if (door != null)
                    {
                        if (door.props.Length > 3 && (door.props == "faction-" + character.faction || tempData.adminduty == true))
                        {
                            if (door.toogle == false)
                            {
                                door.toogle = true;
                            }
                            else
                            {
                                door.toogle = false;
                            }
                            DoorsController.UpdateDoor(player, door);
                            if (door.toogle == true)
                            {
                                SendNotificationWithoutButton(player, $"Abgeschlossen!", "success");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Aufgeschlossen!", "success");
                            }
                            return;
                        }
                    }
                }
                //Fahrzeuge
                Vehicle vehicle = null;
                int vclass = 0;
                if (player.IsInVehicle)
                {
                    vehicle = player.Vehicle;
                    vclass = vehicle.Class;
                }
                else
                {
                    vehicle = GetClosestVehicle(player, 6.75f);
                    if (vehicle != null)
                    {
                        vclass = vehicle.Class;
                        if (vclass == 13 || vclass == 8)
                        {
                            if (!IsInRangeOfPoint(player.Position, vehicle.Position, 2.75f))
                            {
                                vehicle = null;
                            }
                        }
                    }
                }
                if (vehicle != null)
                {
                    if (vehicle.GetSharedData<String>("Vehicle:Name") == "rcbandito" || vehicle.GetSharedData<String>("Vehicle:Name") == "iak_wheelchair") return;
                    if (GetVehicleRights(player, vehicle))
                    {
                        if (!player.IsInVehicle && vclass != 13 && vclass != 8 && !vehicle.GetSharedData<String>("Vehicle:Name").Contains("nrg500"))
                        {
                            Helper.PlayShortAnimation(player, "anim@mp_player_intmenu@key_fob@", "fob_click_fp", 550);
                            player.TriggerEvent("Client:PlaySound", "carlock.mp3", 0);
                        }
                        if (vehicle.Locked == true)
                        {
                            SendNotificationWithoutButton(player, "Fahrzeug geöffnet!", "success");
                            vehicle.Locked = false;
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Fahrzeug verschlossen!", "success");
                            vehicle.Locked = true;
                        }
                        return;
                    }
                }
                //Business
                Business bizz = Business.GetClosestBusiness(player, 5.75f);
                if (bizz != null)
                {
                    Doors door = DoorsController.GetClosestDoor(player, 1.55f);
                    if (door != null)
                    {
                        if (door.props.Length > 3 && door.props == "bizz-" + bizz.id && (Business.HasPlayerBusinessKey(player, bizz.id) || tempData.adminduty == true))
                        {
                            if (door.toogle == false)
                            {
                                door.toogle = true;
                            }
                            else
                            {
                                door.toogle = false;
                            }
                            DoorsController.UpdateDoor(player, door);
                            if (door.toogle == true)
                            {
                                SendNotificationWithoutButton(player, $"Abgeschlossen!", "success");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Aufgeschlossen!", "success");
                            }
                            return;
                        }
                    }
                }
                //Haustüren
                House house2 = House.GetClosestHouse(player, 5.75f);
                if (house2 != null)
                {
                    Doors door = DoorsController.GetClosestDoor(player, 1.55f);
                    if (door != null)
                    {
                        if (door.props.Length > 3 && door.props == "house-" + house2.id && (House.HasPlayerHouseKey(player, house2.id) || tempData.adminduty == true))
                        {
                            if (door.toogle == false)
                            {
                                door.toogle = true;
                            }
                            else
                            {
                                door.toogle = false;
                            }
                            DoorsController.UpdateDoor(player, door);
                            if (door.toogle == true)
                            {
                                SendNotificationWithoutButton(player, $"Abgeschlossen!", "success");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Aufgeschlossen!", "success");
                            }
                            return;
                        }
                    }
                }
                House house = null;
                //Möbel
                if (character.inhouse == -1)
                {
                    house = House.GetClosestHouse(player, 12.5f);
                }
                else
                {
                    house = House.GetHouseById(character.inhouse);
                }
                if (house != null && (House.HasPlayerHouseKey(player, house.id) || tempData.adminduty == true))
                {
                    FurnitureSetHouse furniture = Furniture.GetClosestFurniture(player, house.id, 2.65f);
                    if (furniture != null)
                    {
                        if (furniture.extra == 5)
                        {
                            Doors door = DoorsController.GetDoorByPosition(furniture.position);
                            if (door != null)
                            {
                                if (door.toogle == false)
                                {
                                    door.toogle = true;
                                }
                                else
                                {
                                    door.toogle = false;
                                }
                                DoorsController.UpdateDoor(player, door);
                                furniture.props = "" + door.toogle;
                                if (door.toogle == true)
                                {
                                    SendNotificationWithoutButton(player, $"{furniture.name} abgeschlossen!", "success");
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, $"{furniture.name} aufgeschlossen!", "success");
                                }
                                House.SaveFurniture(furniture);
                                return;
                            }
                        }
                    }
                }
                //Häuser
                if (character.inhouse == -1)
                {
                    house = House.GetClosestHouse(player);
                    if (house != null)
                    {
                        if (House.HasPlayerHouseKey(player, house.id))
                        {
                            player.TriggerEvent("Client:PlaySound", "doorlock.mp3", 0);
                            if (house.locked == 0)
                            {
                                house.locked = 1;
                                SendNotificationWithoutButton(player, "Haus verschlossen!", "success");
                            }
                            else
                            {
                                house.locked = 0;
                                SendNotificationWithoutButton(player, "Haus geöffnet!", "success");
                            }
                            return;
                        }
                    }
                }
                else
                {
                    house = House.GetHouseById(character.inhouse);
                    if (house == null) return;
                    Vector3 houseExit = House.GetHouseExitPoint(house.interior);
                    if (player.Position.DistanceTo(houseExit) <= 2.5f && player.Dimension == house.id)
                    {
                        if (House.HasPlayerHouseKey(player, house.id))
                        {
                            player.TriggerEvent("Client:PlaySound", "doorlock.mp3", 0);
                            if (house.locked == 0)
                            {
                                house.locked = 1;
                                SendNotificationWithoutButton(player, "Haus verschlossen!", "success");
                            }
                            else
                            {
                                house.locked = 0;
                                SendNotificationWithoutButton(player, "Haus geöffnet!", "success");
                            }
                            return;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnEntityLock]: " + e.ToString());
            }
        }

        //IsAtBank
        public static int IsAtBank(Player player, int atATM = -1)
        {
            try
            {
                if (atATM != -1)
                {
                    return 2;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(243.13979, 224.8297, 106.28682), 3.25f) && player.Dimension == 0)
                {
                    return 0;
                }
                foreach (Blip blip in NAPI.Pools.GetAllBlips())
                {
                    if (blip != null && player.Position.DistanceTo(blip.Position) <= 2.25f && blip.Name != "Bankautomat" && blip.Name.Contains("Bank"))
                    {
                        if (blip.Name.Contains("Fleeca"))
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtBank]: " + e.ToString());
            }
            return -1;
        }

        //IsAt247
        public static bool IsAt247(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(1163.6031, -323.91104, 69.20506), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-48.444313, -1758.1068, 29.420996), 1.5f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(374.14062, 327.8349, 103.56637), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-1222.4231, -906.9754, 12.326356), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-3243.9048, 1001.35065, 12.830711), 1.5f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(548.03674, 2669.369, 42.156494), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-1222.3374, -906.8022, 12.326356), 1.5f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(1729.7112, 6416.285, 35.037224), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(1135.6917, -982.8833, 46.415848), 1.5f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(1960.2924, 3742.1748, 32.343742), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-707.3151, -914.574, 19.215591), 1.5f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAt247]: " + e.ToString());
            }
            return false;
        }

        //IsAtBar
        public static bool IsAtBar(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(-561.7678, 287.1561, 82.17647), 2.75f) || IsInRangeOfPoint(player.Position, new Vector3(129.46414, -1283.6378, 29.272814), 2.75f))
                {
                    return true;
                }
                else if (IsInRangeOfPoint(player.Position, new Vector3(1984.1924, 3054.6648, 47.215168), 2.75f) || IsInRangeOfPoint(player.Position, new Vector3(836.1573, -115.343544, 79.77466), 2.75f))
                {
                    return true;
                }
                else if (IsInRangeOfPoint(player.Position, new Vector3(-434.2632, 273.92822, 83.42211), 2.75f) || IsInRangeOfPoint(player.Position, new Vector3(-1391.7916, -605.65985, 30.319567), 2.75f))
                {
                    return true;
                }
                else if (IsInRangeOfPoint(player.Position, new Vector3(-1377.5298, -629.2274, 30.819584), 4.15f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtBar]: " + e.ToString());
            }
            return false;
        }

        //IsAtBarberShop
        public static bool IsAtBarberShop(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(134.57858, -1707.9354, 29.291605), 2.15f) || IsInRangeOfPoint(player.Position, new Vector3(-821.6776, -184.55536, 37.5689), 2.15f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtBarberShop]: " + e.ToString());
            }
            return false;
        }

        //IsAtTattooShop
        public static bool IsAtTattooShop(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(1323.5483, -1651.1605, 52.275097), 2.15f) || IsInRangeOfPoint(player.Position, new Vector3(321.03555, 180.49698, 103.58651), 2.15f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtTattooShop]: " + e.ToString());
            }
            return false;
        }

        //IsAtGarage
        public static int IsAtGarage(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(214.0354, -808.4536, 31.014893), 8.75f))
                {
                    return 33;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-918.12463, -1364.8055, 1.59539), 8.75f))
                {
                    return 34;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-950.207, -3056.4653, 13.945073), 10.75f))
                {
                    return 35;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(67.724884, 12.313736, 69.21442), 8.75f))
                {
                    return 36;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtGarage]: " + e.ToString());
            }
            return -1;
        }

        //IsAtDealerShip
        public static bool IsAtDealerShip(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(117.880554, -139.82031, 54.85013), 2.55f) || IsInRangeOfPoint(player.Position, new Vector3(280.9859, -1163.3105, 29.27299), 2.55f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-29.988438, -1105.174, 26.422417), 2.55f) || IsInRangeOfPoint(player.Position, new Vector3(-55.34697, 68.470764, 71.94996), 2.55f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(1232.2146, 2712.3074, 38.005795), 2.55f) || IsInRangeOfPoint(player.Position, new Vector3(-17.720493, -1651.1118, 29.52027), 2.55f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-211.62364, 6218.96, 31.491283), 2.55f) || IsInRangeOfPoint(player.Position, new Vector3(671.6452, -2672.725, 6.287953), 2.55f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-1136.9968, -199.24275, 37.96), 2.55f) || IsInRangeOfPoint(player.Position, new Vector3(-764.9401, -1316.4093, 5.150273), 2.55f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-941.12964, -2954.1677, 13.945079), 2.55f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtAmmunation]: " + e.ToString());
            }
            return false;
        }

        //IsAtAmmunation
        public static bool IsAtAmmunation(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(810.1653, -2157.738, 29.618994), 3.15f) || IsInRangeOfPoint(player.Position, new Vector3(842.4312, -1033.9905, 28.194862), 3.15f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(22.616953, -1105.5131, 29.797026), 3.15f) || IsInRangeOfPoint(player.Position, new Vector3(-1305.3777, -394.34583, 36.69577), 3.15f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-330.72876, 6084.083, 31.45477), 3.15f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtAmmunation]: " + e.ToString());
            }
            return false;
        }

        //IsAtClothShop
        public static bool IsAtClothShop(Player player)
        {
            try
            {
                if (IsInRangeOfPoint(player.Position, new Vector3(77.555916, -1389.3691, 29.376125), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-1193.231, -768.11237, 17.318638), 1.5f))
                {
                    return true;
                }
                if (IsInRangeOfPoint(player.Position, new Vector3(-710.3996, -152.13515, 37.41514), 1.5f) || IsInRangeOfPoint(player.Position, new Vector3(-3170.51, 1043.7423, 20.863214), 1.5f))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAtClothShop]: " + e.ToString());
            }
            return false;
        }

        public static void CheckLevelUp(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);

                if (account == null || character == null) return;

                var level_exp = ((account.level + 1) * 4);

                if (account.play_points > level_exp)
                {
                    account.play_points -= level_exp;
                    account.level++;
                    account.coins += 5;
                    Helper.SendNotificationWithoutButton(player, $"Levelaufstieg, du bist jetzt Level {account.level}!", "success", "top-end", 3500);
                    Helper.CreateUserTimeline(account.id, character.id, $"Level {account.level} erreicht", 2);

                    if (account.level == 3 && account.geworben != "Keiner")
                    {
                        Player getPlayer = Helper.GetPlayerByAccountName(account.geworben);
                        if (getPlayer != null)
                        {
                            Account account2 = Helper.GetAccountData(getPlayer);
                            if (account2 != null)
                            {
                                account2.coins += 25;
                                CreateUserLog(account2.id, "+25 Coins durch geworbenen Spieler: " + account.name);
                                return;
                            }
                        }
                        MySqlCommand command = General.Connection.CreateCommand();
                        command.CommandText = "SELECT id FROM users WHERE name=@name LIMIT 1";
                        command.Parameters.AddWithValue("@name", account.geworben);

                        int accid = -1;

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                accid = reader.GetInt32("id");
                            }
                            reader.Close();
                        }

                        if (accid != -1)
                        {
                            command = General.Connection.CreateCommand();
                            command.CommandText = "UPDATE users SET coins=coins+25 WHERE id=@id";

                            command.Parameters.AddWithValue("@id", accid);

                            command.ExecuteNonQuery();
                            CreateUserLog(accid, "+25 Coins durch geworbenen Spieler: " + account.name);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckLevelUp]: " + e.ToString());
            }
        }

        //Payday
        public static void CheckPayday(Player player)
        {
            try
            {
                Account account = Helper.GetAccountData(player);

                Character character = Helper.GetCharacterData(player);

                character.payday_points = 0;

                account.play_time++;
                account.play_points++;
                if (account.epboost > 0)
                {
                    account.play_points++;
                    if (UnixTimestamp() > account.epboost)
                    {
                        account.epboost = 0;
                    }
                }
                if (account.premium > 0)
                {
                    account.play_points++;
                }

                CheckLevelUp(player);

                if (account.play_time == 5 || account.play_time == 15 || account.play_time == 25 || account.play_time == 50 || account.play_time == 100 || account.play_time == 250 || account.play_time == 500 || account.play_time == 1000 || account.play_time == 2500 || account.play_time == 5000 || account.play_time == 10000)
                {
                    Helper.CreateUserTimeline(account.id, character.id, $"{account.play_time} Spielstunden erreicht", 1);
                }

                CreatePayday(player);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckPayday]: " + e.ToString());
            }

        }

        public static void CreatePayday(Player player)
        {
            try
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        Character character = Helper.GetCharacterData(player);
                        TempData tempData = Helper.GetCharacterTempData(player);
                        Account account = Helper.GetAccountData(player);
                        if (character == null || tempData == null || account == null) return;

                        Bank charbank = BankController.GetBankByBankNumber(character.defaultbank);

                        player.TriggerEvent("Client:PlaySoundPeep2");

                        if (charbank == null)
                        {
                            SendNotificationWithoutButton(player, "Es konnte kein Payday ausgezahlt werden, da kein Standardkonto vorhanden ist!", "error", "top-left", 3500);
                            return;
                        }

                        if (account.faqarray[8] == "0")
                        {
                            account.faqarray[8] = "1";
                        }

                        List<Payday> paydayList = new List<Payday>();

                        int value = 0;
                        int cash = 0;
                        int total = 0;
                        if (character.faction > 0)
                        {
                            FactionSalary factionSalary = FactionController.GetFactionSalarysById(character.faction);
                            value = FactionController.GetFactionSalaryByRang(factionSalary, character.faction, character.rang);
                            if (value > 0)
                            {
                                if (Helper.adminSettings.govvalue > 0)
                                {
                                    Payday fraklohn = new Payday();
                                    fraklohn.modus = "Privat";
                                    fraklohn.setting = "Fraktionslohn";
                                    fraklohn.value = value;
                                    Helper.SetGovMoney(-value, "Fraktionslohn Auszahlung");
                                    charbank.bankvalue += value;
                                    cash += value;
                                    total += value;
                                    value = 0;
                                    paydayList.Add(fraklohn);
                                }
                                else
                                {
                                    Payday fraklohn = new Payday();
                                    fraklohn.modus = "Privat";
                                    fraklohn.setting = "Fraktionslohn - (Staatskasse ist leer)";
                                    fraklohn.value = 0;
                                    value = 0;
                                    paydayList.Add(fraklohn);
                                }
                            }
                        }

                        PetaPoco.Database db = new PetaPoco.Database(General.Connection);

                        List<GroupsMembers> groups = new List<GroupsMembers>();
                        foreach (GroupsMembers group in db.Fetch<GroupsMembers>("SELECT * FROM groups_members WHERE charid = @0 ORDER BY id", character.id))
                        {
                            groups.Add(group);
                        }

                        foreach (GroupsMembers groupmember in groups)
                        {
                            if (groupmember != null && groupmember.payday > 0)
                            {
                                if (groupmember.payday_day == groupmember.payday_since)
                                {
                                    Groups groups1 = GroupsController.GetGroupById(groupmember.groupsid);
                                    if (groups1 != null)
                                    {
                                        Bank groupbank = BankController.GetBankByBankNumber(groups1.banknumber);
                                        if (groupbank != null)
                                        {
                                            if (groupbank.bankvalue >= groupmember.payday)
                                            {
                                                groupmember.payday_since = 0;
                                                Payday gruppierungslohn = new Payday();
                                                gruppierungslohn.modus = "Privat";
                                                gruppierungslohn.setting = "Gruppierungslohn - " + groups1.name;
                                                gruppierungslohn.value = groupmember.payday;
                                                groupbank.bankvalue -= groupmember.payday;
                                                charbank.bankvalue += groupmember.payday;
                                                cash += groupmember.payday;
                                                total += gruppierungslohn.value;
                                                Bankfile(groupbank, charbank, "Gruppierungslohn - " + character.name, groupmember.payday);
                                                paydayList.Add(gruppierungslohn);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    groupmember.payday_since = groupmember.payday_since + 1;
                                }
                            }
                            db.Save(groupmember);
                        }

                        if (character.nextpayday > 0)
                        {
                            Payday joblohn = new Payday();
                            joblohn.modus = "Privat";
                            joblohn.setting = "Joblohn";
                            joblohn.value = character.nextpayday;
                            charbank.bankvalue += character.nextpayday;
                            cash += character.nextpayday;
                            total += character.nextpayday;
                            character.nextpayday = 0;
                            paydayList.Add(joblohn);
                        }

                        if (total > 0 && character.jobless == 1)
                        {
                            character.jobless = 0;
                        }

                        if (character.jobless == 1 && Helper.adminSettings.govvalue >= Convert.ToInt32(Helper.adminSettings.groupsettings.Split(",")[4]))
                        {
                            Payday ageld = new Payday();
                            ageld.modus = "Privat";
                            ageld.setting = "Arbeitslosengeld";
                            ageld.value = Convert.ToInt32(Helper.adminSettings.groupsettings.Split(",")[4]);
                            charbank.bankvalue += Convert.ToInt32(Helper.adminSettings.groupsettings.Split(",")[4]);
                            cash += character.nextpayday;
                            total += character.nextpayday;
                            paydayList.Add(ageld);
                        }

                        foreach (Bank getbank in BankController.bankList)
                        {
                            if (getbank.ownercharid == character.id && getbank.banknumber != "SA3701-100000")
                            {
                                if (getbank.banknumber == character.defaultbank)
                                {
                                    Payday zinsen = new Payday();
                                    if (getbank.bankvalue > 0)
                                    {
                                        zinsen.modus = "Privat";
                                        if (account.premium == 3)
                                        {
                                            zinsen.setting = "Kontozinsen 0.65%";
                                            zinsen.value = (int)(getbank.bankvalue / 100 * 0.65);
                                        }
                                        else
                                        {
                                            if (getbank.banktype == 0)
                                            {
                                                zinsen.setting = "Kontozinsen 0.25%";
                                                zinsen.value = (int)(getbank.bankvalue / 100 * 0.25);
                                            }
                                            else if (getbank.banktype == 1)
                                            {
                                                zinsen.setting = "Kontozinsen 0.45%";
                                                zinsen.value = (int)(getbank.bankvalue / 100 * 0.45);
                                            }
                                        }
                                    }
                                    if (zinsen.value > 0)
                                    {
                                        charbank.bankvalue += zinsen.value;
                                        BankSettings(charbank.banknumber, "Zinsen Auszahlung", "" + zinsen.value, "Bank");
                                        paydayList.Add(zinsen);
                                        total += zinsen.value;
                                    }
                                }
                                else
                                {
                                    int zinsen = 0;
                                    if (getbank.bankvalue > 0)
                                    {
                                        if (account.premium == 3)
                                        {
                                            zinsen = (int)(getbank.bankvalue / 100 * 0.65);
                                        }
                                        else
                                        {
                                            if (getbank.banktype == 0)
                                            {
                                                zinsen = (int)(getbank.bankvalue / 100 * 0.25);
                                            }
                                            else if (getbank.banktype == 1)
                                            {
                                                zinsen = (int)(getbank.bankvalue / 100 * 0.45);
                                            }
                                        }
                                    }
                                    if (zinsen > 0)
                                    {
                                        getbank.bankvalue += zinsen;
                                        BankSettings(getbank.banknumber, "Zinsen Auszahlung", "" + zinsen, "Bank");
                                    }
                                }
                            }
                        }

                        if (cash > 0)
                        {
                            Payday lohnsteuer = new Payday();
                            lohnsteuer.modus = "Steuern";
                            lohnsteuer.setting = "Lohnsteuer " + adminSettings.lsteuer + "%";
                            lohnsteuer.value = (int)(cash / 100 * adminSettings.lsteuer);
                            if (lohnsteuer.value > 0)
                            {
                                charbank.bankvalue -= lohnsteuer.value;
                                total -= lohnsteuer.value;
                                cash = 0;
                                paydayList.Add(lohnsteuer);
                                Helper.SetGovMoney(lohnsteuer.value, "Lohnsteuer Einzahlung");
                            }
                        }

                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleData != null && car.vehicleData.plate.Length > 0 && car.vehicleData.owner == "character-" + character.id)
                            {
                                VehicleShop vehicleShop = DealerShipController.GetVehicleShopByVehicleName(car.vehicleData.vehiclename);
                                Payday kfzsteuer = new Payday();
                                kfzsteuer.modus = "Steuern";
                                string vehiclename = car.vehicleData.vehiclename;
                                if (car.vehicleData.ownname != "n/A")
                                {
                                    vehiclename = car.vehicleData.ownname;
                                }
                                kfzsteuer.setting = "KFZ-Steuer " + adminSettings.ksteuer + "%" + " für " + vehiclename;
                                kfzsteuer.value = -(int)(vehicleShop.price / 100 * adminSettings.ksteuer);
                                charbank.bankvalue -= kfzsteuer.value;
                                paydayList.Add(kfzsteuer);
                                Helper.SetGovMoney((int)(vehicleShop.price / 100 * adminSettings.ksteuer), "KFZ-Steuer Einzahlung");
                                total += kfzsteuer.value;
                            }
                        }

                        foreach (House house in House.houseList)
                        {
                            if (house.owner != "n/A")
                            {
                                if (house.owner == character.name)
                                {
                                    Business bizz = Business.GetBusinessById(house.elec);
                                    Payday housemoney = new Payday();
                                    housemoney.modus = "Haus";
                                    housemoney.setting = "Stromkosten für Haus " + house.id;
                                    housemoney.value = -((int)(125 * bizz.multiplier));
                                    charbank.bankvalue -= (int)(125 * bizz.multiplier);
                                    Business.ManageBizzCash(bizz, (int)(125 * bizz.multiplier), true);
                                    paydayList.Add(housemoney);
                                    Helper.SetGovMoney(((int)(125 * bizz.multiplier)), "Stromkosten Haus");
                                    total += housemoney.value;
                                }
                            }
                        }

                        foreach (Business business in Business.businessList)
                        {
                            if (business.owner != "n/A" && business.owner == character.name)
                            {
                                Business bizz = Business.GetBusinessById(business.elec);
                                Payday bizzmoney = new Payday();
                                bizzmoney.modus = "Business";
                                bizzmoney.setting = "Stromkosten für " + business.name;
                                bizzmoney.value = -((int)(125 * bizz.multiplier));
                                charbank.bankvalue -= (int)(125 * bizz.multiplier);
                                Business.ManageBizzCash(bizz, (int)(125 * bizz.multiplier), true);
                                paydayList.Add(bizzmoney);
                                Helper.SetGovMoney((int)(125 * bizz.multiplier), "Stromkosten " + business.name);
                                total += bizzmoney.value;

                                if (business.govcash > 0)
                                {
                                    Payday bizzmoney2 = new Payday();
                                    bizzmoney2.modus = "Steuern";
                                    bizzmoney2.setting = "Gewerbesteuern " + adminSettings.gsteuer + "%" + " für " + business.name;
                                    bizzmoney2.value = -business.govcash;
                                    paydayList.Add(bizzmoney2);
                                    Helper.SetGovMoney(business.govcash, "Gewerbesteuern " + business.name);
                                    business.govcash = 0;
                                }
                            }
                        }

                        if (paydayList.Count > 0)
                        {
                            SendNotificationWithoutButton(player, "Neue Payday Informationen verfügbar!", "info", "top-left", 2250);

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "INSERT INTO paydays (characterid, text, timestamp, total) VALUES (@characterid, @text, @timestamp, @total)";

                            command.Parameters.AddWithValue("@characterid", character.id);
                            command.Parameters.AddWithValue("@text", NAPI.Util.ToJson(paydayList));
                            command.Parameters.AddWithValue("@timestamp", UnixTimestamp());
                            command.Parameters.AddWithValue("@total", total);

                            command.ExecuteNonQuery();

                            CreateAdminLog("payday", $"{account.name}({character.name}) hat {total}$ am Payday({command.LastInsertedId}) verdient!");
                        }
                    });
                });
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreatePayday]: " + e.ToString());
            }
        }


        //OnVehicleEngine
        [RemoteEvent("Server:VehicleEngine")]
        public static void OnVehicleEngine(Player player)
        {
            try
            {
                Vehicle vehicle = player.Vehicle;
                if (vehicle != null)
                {
                    if (player.VehicleSeat != (int)VehicleSeat.Driver) return;
                    if (vehicle.GetSharedData<float>("Vehicle:MaxFuel") == 0) return;
                    if (vehicle.GetSharedData<String>("Vehicle:Name") == "iak_wheelchair") return;
                    if (GetVehicleRights(player, vehicle))
                    {
                        if (vehicle.EngineStatus == false)
                        {
                            if (vehicle.GetSharedData<float>("Vehicle:Fuel") <= 0)
                            {
                                SendNotificationWithoutButton(player, "Der Tank des Fahrzeuges ist leer!", "error");
                                return;
                            }
                            if (vehicle.GetSharedData<int>("Vehicle:Oel") <= 0 || vehicle.GetSharedData<int>("Vehicle:Battery") <= 0 || NAPI.Vehicle.GetVehicleHealth(vehicle) <= 0 || NAPI.Vehicle.GetVehicleEngineHealth(vehicle) <= 50)
                            {
                                SendNotificationWithoutButton(player, "Der Motor springt nichtmehr an!", "error");
                                return;
                            }
                            if (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[6] == "1")
                            {
                                SendNotificationWithoutButton(player, "Du kannst nicht losfahren, es befindet sich eine Parkkralle an deinem Fahrzeug!", "error");
                                return;
                            }
                            SendNotificationWithoutButton(player, "Motor gestartet!", "success");
                            SetVehicleEngine(vehicle, true);
                            player.SetOwnSharedData("Player:VehicleEngine", true);
                            player.TriggerEvent("Client:RadioOff");
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Motor gestoppt!", "success");
                            SetVehicleEngine(vehicle, false);
                            player.SetOwnSharedData("Player:VehicleEngine", false);
                        }
                    }
                    else
                    {
                        SendNotificationWithoutButton(player, "Du hast nicht den passenden Schlüssel für dieses Fahrzeug!", "error");
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnVehicleEngine]: " + e.ToString());
            }
        }

        //OnVehicleEngine
        [RemoteEvent("Server:UpdateKilometre")]
        public static void OnUpdateKilometre(Player player, float kilometre, int vehicleID)
        {
            try
            {
                Vehicle vehicle = GetVehicleById(vehicleID);
                if (vehicle != null)
                {
                    vehicle.SetSharedData("Vehicle:Kilometre", Math.Round(kilometre, 2));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnUpdateKilometre]: " + e.ToString());
            }
        }

        //Fuelsystem
        [RemoteEvent("Server:ShowFuelStation")]
        public static void OnShowFuelStation(Player player)
        {
            try
            {
                Vehicle vehicle = GetClosestVehicle(player, 1.95f);
                if (player.IsInVehicle)
                {
                    SendNotificationWithoutButton(player, "Du musst zuerst aus deinem Fahrzeug aussteigen!", "error");
                    return;
                }
                if (vehicle == null)
                {
                    SendNotificationWithoutButton(player, "Es befindet sich kein Fahrzeug in der Nähe!", "error");
                    return;
                }
                if (vehicle.EngineStatus == true)
                {
                    SendNotificationWithoutButton(player, "Du musst zuerst den Motor vom Fahrzeug ausschalten!", "error");
                    return;
                }
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (bizz == null)
                {
                    SendNotificationWithoutButton(player, "Du kannst hier nicht tanken!", "error");
                    return;
                }
                if (bizz.products <= 0)
                {
                    SendNotificationWithoutButton(player, "Die Tankstelle hat keinen Treibstoff mehr!", "error");
                    return;
                }
                if (player.GetData<int>("Player:FuelPrice") > 0)
                {
                    SendNotificationWithoutButton(player, "Du musst zuerst deine aktuelle Tankrechnung begleichen!", "error");
                    return;
                }
                //player.PlayAnimation("timetable@gardener@filling_can", "gar_ig_5_filling_can", 50);
                player.SetSharedData("Player:AnimData", $"timetable@gardener@filling_can%gar_ig_5_filling_can%{50}");
                player.TriggerEvent("Client:PlayerFreeze", true);
                player.TriggerEvent("Client:ShowFuelStation", (bizz.prodprice / 4) * bizz.multiplier, vehicle.GetSharedData<float>("Vehicle:Fuel"), vehicle.GetSharedData<float>("Vehicle:MaxFuel"), bizz.products);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnShowFuelStation]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:GetFuel")]
        public static void GetFuel(Player player, int price, float fuel, float newfuel)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Vehicle vehicle = GetClosestVehicle(player, 3.5f);
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                player.TriggerEvent("Client:PlayerFreeze", false);
                if (bizz == null)
                {
                    SendNotificationWithoutButton(player, "Ungültige Interaktion!", "error");
                    return;
                }
                if (newfuel > 0)
                {
                    vehicle.SetSharedData("Vehicle:Fuel", fuel);
                    bizz.products -= (int)newfuel;
                    if (!IsAGovCar(vehicle))
                    {
                        player.SetData<int>("Player:FuelPrice", price);
                        player.SetData<int>("Player:FuelCooldown", Helper.UnixTimestamp() + (60 * 6));
                        player.SetData<int>("Player:FuelBizz", bizz.id);
                        SendNotificationWithoutButton(player, $"Du hast erfolgreich {newfuel.ToString("0")}l getankt, der Preis beträgt {price}$. Begebe dich jetzt in den Laden und bezahle deine Tankrechnung!", "success", "top-left", 5750);
                    }
                    else
                    {
                        Helper.SetGovMoney(-price, "Tankrechnung bezahlt");
                        player.SetData<int>("Player:FuelPrice", 0);
                        player.SetData<int>("Player:FuelCooldown", 0);
                        player.SetData<int>("Player:FuelBizz", 0);
                        Business.ManageBizzCash(bizz, price, true);
                        SendNotificationWithoutButton(player, $"Du hast erfolgreich {newfuel.ToString("0")}l getankt, der Preis beträgt {price}$. Die Tankrechnung wird vom Staat übernommen!", "success", "top-left", 5750);
                    }
                }
                OnStopAnimation(player);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetFuel]: " + e.ToString());
            }
        }

        //IsAGovCar
        public static bool IsAGovCar(Vehicle vehicle)
        {
            try
            {
                VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                if (vehicleData != null && (vehicleData.owner == "faction-1" || vehicleData.owner == "faction-2" || vehicleData.owner == "faction-3" || vehicleData.owner == "faction-4"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsAGovCar]: " + e.ToString());
            }
            return false;
        }

        //IsATruck
        public static bool IsATruck(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null && vehicle.HasSharedData("Vehicle:Name"))
                {
                    if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower() == "phantom" || vehicle.GetSharedData<string>("Vehicle:Name").ToLower() == "phantom3")
                    {
                        return true;
                    }
                    if (vehicle.GetSharedData<string>("Vehicle:Name").ToLower() == "tractor2")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsATruck]: " + e.ToString());
            }
            return false;
        }

        public static bool IsTrailerAttached(Player player)
        {
            try
            {
                if (player != null && player.IsInVehicle && player.Vehicle != null)
                {
                    Vehicle vehicleTemp = GetClosestVehicleFromVehicle(player, 9.85f);
                    if (vehicleTemp != null && vehicleTemp.HasSharedData("Vehicle:Name") && (vehicleTemp.GetSharedData<string>("Vehicle:Name").ToLower().Contains("trailer") || vehicleTemp.GetSharedData<string>("Vehicle:Name").ToLower().Contains("tanker")))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsTrailerAttached]: " + e.ToString());
            }
            return false;
        }

        public static bool IsTrailerAttached2(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    Vehicle vehicleTemp = GetClosestVehicleFromVehicle2(vehicle, 8.95f);
                    if (vehicleTemp != null && vehicleTemp.HasSharedData("Vehicle:Name") && (vehicleTemp.GetSharedData<string>("Vehicle:Name").Contains("trailer") || vehicleTemp.GetSharedData<string>("Vehicle:Name").Contains("tanker")))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsTrailerAttached2]: " + e.ToString());
            }
            return false;
        }


        public static bool IsTraileredBy(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    Vehicle vehicleTemp = GetClosestVehicleFromVehicle2(vehicle, 8.95f);
                    if (vehicleTemp != null && IsATruck(vehicleTemp))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[IsTraileredBy]: " + e.ToString());
            }
            return false;
        }

        [RemoteEvent("SaltyChat_MicStateChanged")]
        public static void SaltyChat_MicStateChanged(Player player, bool status)
        {
            if (status == true)
            {
                player.TriggerEvent("Client:SetTalkstate", 2);
                player.SetOwnSharedData("Player:Voice", 2);
            }
            else
            {
                if (player.GetData<bool>("Player:SoundEnabled") == true)
                {
                    player.SetData<bool>("Player:MicEnabled", status);
                    return;
                }
                player.SetOwnSharedData("Player:Voice", 0);
                player.TriggerEvent("Client:SetTalkstate", 0);
            }
            player.SetData<bool>("Player:MicEnabled", status);
        }

        [RemoteEvent("SaltyChat_SoundStateChanged")]
        public static void SaltyChat_SoundStateChanged(Player player, bool status)
        {
            if (status == true)
            {
                player.SetOwnSharedData("Player:Voice", 2);
                player.TriggerEvent("Client:SetTalkstate", 2);
            }
            else
            {
                if (player.GetData<bool>("Player:MicEnabled") == true)
                {
                    player.SetData<bool>("Player:SoundEnabled", status);
                    return;
                }
                player.SetOwnSharedData("Player:Voice", 0);
                player.TriggerEvent("Client:SetTalkstate", 0);
            }
            player.SetData<bool>("Player:SoundEnabled", status);
        }

        //Prison
        [RemoteEvent("Server:EndPrison")]
        public static void OnEndPrison(Player player, int check)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (character == null || account == null) return;
                account.prison = 0;
                if (check == 0)
                {
                    Helper.DiscordWebhook(Helper.AdminNotificationWebHook, $"{account.name} hat alle Checkpoints abgelaufen!", "Gameserver");
                    Helper.CreateAdminLog("prisonlog", $"{account.name} hat alle Checkpoints abgelaufen!");
                }
                string[] spawnCharAfterReconnect = new string[5];
                spawnCharAfterReconnect = character.lastpos.Split("|");
                player.Dimension = Convert.ToUInt32(spawnCharAfterReconnect[4]);
                SetPlayerPosition(player, new Vector3(float.Parse(spawnCharAfterReconnect[0]), float.Parse(spawnCharAfterReconnect[1]), float.Parse(spawnCharAfterReconnect[2])));
                player.Heading = float.Parse(spawnCharAfterReconnect[3]);
                player.Rotation = new Vector3(0.0, 0.0, float.Parse(spawnCharAfterReconnect[3]));
                if (check == 0)
                {
                    Helper.SendNotificationWithoutButton(player, $"Du bist erfolgreich alle Checkpoints abgelaufen!", "success", "top-end", 3500);
                    Helper.SendAdminMessage2($"{account.name} hat alle Checkpoints abgelaufen!", 0);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnEndPrison]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:UpdatePrison")]
        public static void OnUpdatePrison(Player player, int count)
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                if (account == null) return;
                account.prison = count;
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnEndPrison]: " + e.ToString());
            }
        }

        //Shopsystem
        public static void GetAllShopItems()
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                foreach (ShopItems shopItem in db.Fetch<ShopItems>("SELECT * FROM shopitems"))
                {
                    shopItemList.Add(shopItem);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetAllShopItems]: " + e.ToString());
            }
        }

        public static void ShowTattooShop(Player player, bool hide = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character == null || tempData == null || player.GetOwnSharedData<bool>("Player:Death") == true) return;
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (bizz != null)
                {
                    if (bizz.nobuy == true)
                    {
                        Helper.SendNotificationWithoutButton(player, "Du kannst jetzt hier keine Dienstleistung in Anspruch nehmen!", "error");
                        return;
                    }
                    if (bizz.products <= 0)
                    {
                        Helper.SendNotificationWithoutButton(player, $"Zurzeit können wir unsere Dienstleistungen leider nicht anbieten!", "error", "top-end", 3500);
                        return;
                    }
                    player.SetData<bool>("Player:InShop", true);
                    player.Dimension = (uint)(player.Id + 5);
                    if (bizz.id == 39)
                    {
                        SetPlayerPosition(player, new Vector3(1325.6906, -1652.1112, 52.275673));
                        player.Heading = 87.08427f;
                    }
                    else if (bizz.id == 40)
                    {
                        SetPlayerPosition(player, new Vector3(320.9111, 182.86111, 103.58651));
                        player.Heading = -152.20581f;
                    }
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    if (NAPI.Player.GetPlayerAccessoryDrawable(player, 0) != 255)
                    {
                        NAPI.Player.ClearPlayerAccessory(player, 0);
                    }
                    NAPI.Player.SetPlayerClothes(player, 2, 0, 0);
                    NAPI.Player.SetPlayerHairColor(player, 0, 0);
                    player.TriggerEvent("hairOverlay::update", player, 0);

                    player.ClearDecorations();

                    player.TriggerEvent("Client:ShowTattoShop", NAPI.Util.ToJson(tempData.tattoos), character.gender, bizz.multiplier);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowTattooShop]: " + e.ToString());
            }
        }

        public static void ShowBarberShop(Player player, bool hide = false)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character == null || player.GetOwnSharedData<bool>("Player:Death") == true) return;
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if (bizz != null)
                {
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    JObject obj = JObject.Parse(character.json);
                    if (bizz.id == 37)
                    {
                        if (hide == false)
                        {
                            if (bizz.nobuy == true)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du kannst jetzt hier keine Dienstleistung in Anspruch nehmen!", "error");
                                return;
                            }
                            if (bizz.products <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Zurzeit können wir unsere Dienstleistungen leider nicht anbieten!", "error", "top-end", 3500);
                                return;
                            }
                            player.SetData<bool>("Player:InShop", true);
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            player.Dimension = (uint)(player.Id + 5);
                            SetPlayerPosition(player, new Vector3(139.16472, -1707.9677, 29.0013));
                            player.Heading = -127.958244f;
                            player.SetSharedData("Player:AnimData", $"amb@prop_human_seat_chair_mp@male@generic@base%base%1");
                            if (NAPI.Player.GetPlayerAccessoryDrawable(player, 0) != 255)
                            {
                                NAPI.Player.ClearPlayerAccessory(player, 0);
                            }
                        }
                        else
                        {
                            player.SetData<bool>("Player:InShop", false);
                            player.TriggerEvent("Client:PlayerFreeze", false);
                            Helper.OnStopAnimation2(player);
                            SetPlayerPosition(player, new Vector3(139.99487, -1707.3672, 29.30162));
                            player.Heading = 21.715519f;
                            player.Dimension = 0;
                            CharacterController.SetCharacterCloths(player, obj, character.clothing);
                            NAPI.Task.Run(() =>
                            {
                                Helper.GetCharacterTattoos(player, character.id);
                            }, delayTime: 550);
                        }
                        player.TriggerEvent("Client:ShowBarberShop",
                        obj["hair"][0], obj["hair"][1], obj["beard"][0], obj["beard"][1], obj["hair"][2], character.gender, bizz.multiplier, bizz.id, obj["headOverlays"][2], obj["headOverlaysColors"][2], obj["headOverlays"][4], obj["headOverlaysColors"][4], obj["headOverlays"][8], obj["headOverlaysColors"][8]);
                    }
                    else if (bizz.id == 38)
                    {
                        if (hide == false)
                        {
                            if (bizz.products <= 0)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Zurzeit können wir unsere Dienstleistungen leider nicht anbieten!", "error", "top-end", 3500);
                                return;
                            }
                            player.SetData<bool>("Player:InShop", true);
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            player.Dimension = (uint)(player.Id + 5);
                            SetPlayerPosition(player, new Vector3(-814.4572, -182.05435, 37.068924));
                            player.Heading = 21.560816f;
                            player.SetSharedData("Player:AnimData", $"amb@prop_human_seat_chair_mp@male@generic@base%base%1");
                            if (NAPI.Player.GetPlayerAccessoryDrawable(player, 0) != 255)
                            {
                                NAPI.Player.ClearPlayerAccessory(player, 0);
                            }
                        }
                        else
                        {
                            player.SetData<bool>("Player:InShop", false);
                            player.TriggerEvent("Client:PlayerFreeze", false);
                            Helper.OnStopAnimation2(player);
                            SetPlayerPosition(player, new Vector3(-814.9795, -183.07498, 37.56893));
                            player.Heading = 149.0038f;
                            player.Dimension = 0;
                            CharacterController.SetCharacterCloths(player, obj, character.clothing);
                        }
                        player.TriggerEvent("Client:ShowBarberShop",
                        obj["hair"][0], obj["hair"][1], obj["beard"][0], obj["beard"][1], obj["hair"][2], character.gender, bizz.multiplier, bizz.id, obj["headOverlays"][2], obj["headOverlaysColors"][2], obj["headOverlays"][4], obj["headOverlaysColors"][4], obj["headOverlays"][8], obj["headOverlaysColors"][8]);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowBarberShop]: " + e.ToString());
            }
        }

        public static void ShowWaffenkammer(Player player, int faction, int update = 0, int check = 1)
        {
            try
            {

                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;

                List<ShopItems> waffenkammer = new List<ShopItems>();
                foreach (ShopItems shopItem in shopItemList)
                {
                    if (shopItem.shoplabel == "Waffenkammer-" + faction)
                    {
                        ShopItems shopItemTemp = new ShopItems();
                        shopItemTemp.id = shopItem.id;
                        shopItemTemp.itemname = shopItem.itemname;
                        shopItemTemp.itemprice = shopItem.itemprice;
                        waffenkammer.Add(shopItemTemp);
                    }
                }
                if (faction == 1)
                {
                    player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(waffenkammer), update, "Waffenkammer LSPD", check);
                }
                else if (faction == 2)
                {
                    player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(waffenkammer), update, "Lager LSRC", check);
                }
                else if (faction == 3)
                {
                    player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(waffenkammer), update, "Lager ACLS", check);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowWaffenkammer]: " + e.ToString());
            }
        }

        public static void Show247Menu(Player player, int update = 0)
        {
            try
            {
                if (IsAt247(player))
                {
                    TempData tempData = Helper.GetCharacterTempData(player);
                    Character character = Helper.GetCharacterData(player);
                    if (tempData == null || character == null) return;

                    Business bizz = Business.GetClosestBusiness(player, 40.5f);
                    if (bizz != null)
                    {
                        if (bizz.nobuy == true)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du kannst jetzt hier nichts kaufen!", "error");
                            return;
                        }
                        List<ShopItems> shop247 = new List<ShopItems>();
                        if (player.HasData("Player:FuelPrice") && player.GetData<int>("Player:FuelPrice") > 0)
                        {
                            ShopItems shopItems = new ShopItems();
                            shopItems.id = 9999;
                            shopItems.shoplabel = "24/7";
                            shopItems.itemname = "Tankrechnung";
                            shopItems.itemprice = player.GetData<int>("Player:FuelPrice");
                            shop247.Add(shopItems);
                        }
                        int count = 0;
                        if (tempData.itemlist != null && tempData.itemlist.Count > 0)
                        {
                            foreach (Items item in tempData.itemlist)
                            {
                                if (item != null && item.description.Contains("Pfandflasche"))
                                {
                                    count = item.amount;
                                    break;
                                }
                            }
                        }
                        if (count > 0)
                        {
                            ShopItems shopItems = new ShopItems();
                            shopItems.id = 9998;
                            shopItems.shoplabel = "24/7";
                            shopItems.itemname = "Pfandflasche";
                            shopItems.itemprice = 25 * count;
                            shop247.Add(shopItems);
                        }
                        foreach (ShopItems shopItem in shopItemList)
                        {
                            if (shopItem.shoplabel == "24/7")
                            {
                                ShopItems shopItemTemp = new ShopItems();
                                shopItemTemp.id = shopItem.id;
                                shopItemTemp.itemname = shopItem.itemname;
                                shopItemTemp.shoplabel = "24/7";
                                shopItemTemp.itemprice = Convert.ToInt32(shopItem.itemprice * bizz.multiplier);
                                shop247.Add(shopItemTemp);
                            }
                        }
                        player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(shop247), update, "24/7 Laden", 1);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[Show247Menu]: " + e.ToString());
            }
        }

        public static void ShowSnackpoint(Player player, int update = 0)
        {
            try
            {

                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;

                List<ShopItems> snackpoint = new List<ShopItems>();
                foreach (ShopItems shopItem in shopItemList)
                {
                    if (shopItem.shoplabel == "Snackpoint")
                    {
                        ShopItems shopItemTemp = new ShopItems();
                        shopItemTemp.id = shopItem.id;
                        shopItemTemp.itemname = shopItem.itemname;
                        shopItemTemp.shoplabel = "Snackpoint";
                        shopItemTemp.itemprice = shopItem.itemprice;
                        snackpoint.Add(shopItemTemp);
                    }
                }
                player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(snackpoint), update, "Snackpoint", 1);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowSnackpoint]: " + e.ToString());
            }
        }

        public static void ShowBar(Player player, int update = 0)
        {
            try
            {

                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;

                Business bizz = Business.GetClosestBusiness(player, 65.5f);
                if (bizz != null)
                {
                    List<ShopItems> bar = new List<ShopItems>();
                    foreach (ShopItems shopItem in shopItemList)
                    {
                        if (shopItem.shoplabel == "Bar")
                        {
                            ShopItems shopItemTemp = new ShopItems();
                            shopItemTemp.id = shopItem.id;
                            shopItemTemp.itemname = shopItem.itemname;
                            shopItemTemp.shoplabel = "Bar";
                            shopItemTemp.itemprice = Convert.ToInt32(shopItem.itemprice * bizz.multiplier);
                            bar.Add(shopItemTemp);
                        }
                    }
                    player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(bar), update, "Bar", 1);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowBar]: " + e.ToString());
            }
        }

        public static void ShowApotheke(Player player, int update = 0)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;


                List<ShopItems> apotheke = new List<ShopItems>();
                foreach (ShopItems shopItem in shopItemList)
                {
                    if (shopItem.shoplabel == "Apotheke")
                    {
                        ShopItems shopItemTemp = new ShopItems();
                        shopItemTemp.id = shopItem.id;
                        shopItemTemp.itemname = shopItem.itemname;
                        shopItemTemp.shoplabel = "Apotheke";
                        shopItemTemp.itemprice = shopItem.itemprice;
                        apotheke.Add(shopItemTemp);
                    }
                }
                player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(apotheke), update, "Apotheke", 1);

            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowApotheke]: " + e.ToString());
            }
        }

        public static void ShowDealerShop(Player player, int update)
        {
            int count = 0;
            List<ShopItems> waffendealer = new List<ShopItems>();
            foreach (ShopItems shopItem in shopItemList)
            {
                if (shopItem.shoplabel == "Waffenkammer-A")
                {
                    ShopItems shopItemTemp = new ShopItems();
                    shopItemTemp.id = shopItem.id;
                    shopItemTemp.itemname = shopItem.itemname;
                    shopItemTemp.itemprice = shopItem.itemprice;
                    shopItemTemp.shoplabel = "Waffendealer";
                    shopItemTemp.itemamount = GangController.dealerAmount[count];
                    waffendealer.Add(shopItemTemp);
                    count++;
                }
            }
            player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(waffendealer), update, "Waffendealer", 1);
        }

        public static void ShowDealerShop2(Player player, int update)
        {
            int count = 0;
            List<ShopItems> bigd = new List<ShopItems>();
            Random random = new Random();
            foreach (ShopItems shopItem in shopItemList)
            {
                if (shopItem.shoplabel == "Waffenkammer-D")
                {
                    ShopItems shopItemTemp = new ShopItems();
                    shopItemTemp.id = shopItem.id;
                    shopItemTemp.itemname = shopItem.itemname;
                    shopItemTemp.itemprice = shopItem.itemprice;
                    shopItemTemp.shoplabel = "Big. D";
                    shopItemTemp.itemamount = GangController.drugAmount;
                    bigd.Add(shopItemTemp);
                    count++;
                }
            }
            player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(bigd), update, "Big. D", 1);
        }

        [RemoteEvent("Server:ShowPreShop")]
        public static void ShowPreShop(Player player, string shopname, int update, int show, int hud)
        {
            try
            {
                if (shopname == "n/A") return;
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (tempData == null || character == null) return;
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                if ((bizz != null && shopname != "n/A") || shopname.ToLower() == "mechatronikermenü" || shopname.ToLower() == "jägermenü" || shopname.ToLower() == "angelmenü" || shopname.ToLower() == "schatzsuchermenü" || shopname.ToLower() == "busfahrermenü" || shopname.ToLower() == "müllmannmenü" || shopname.ToLower() == "routenauswahl" || shopname.ToLower() == "müllroutenauswahl" || shopname.ToLower() == "ticketverkauf" || shopname.ToLower() == "taxifahrermenü" || shopname.ToLower() == "kanalreinigermenü"
                || shopname.ToLower() == "landwirtmenü" || shopname.ToLower() == "animationsmenü" || shopname.ToLower() == "animationsauswahl" || shopname.ToLower() == "laufstilauswahl" || shopname.ToLower() == "geldlieferantmenü" || shopname.ToLower() == "fahrschule" || shopname.ToLower() == "police-department" || shopname.ToLower() == "waffenkammer lspd" || shopname.ToLower() == "lager lsrc" || shopname.ToLower() == "lsrc dach" || shopname.ToLower() == "lager acls" || shopname.ToLower() == "gutschein erstellen")
                {
                    string text1 = "n/A";
                    string text2 = "n/A";
                    tempData.lastShop = shopname;
                    if (shopname.ToLower() == "waffenkammer lspd")
                    {
                        if (Helper.IsInRangeOfPoint(player.Position, new Vector3(484.85513, -1003.6393, 25.734646), 2.75f))
                        {
                            text1 = "Waffenkammer,Waffenkomponenten,Schiessstand benutzen,Abbrechen";
                            text2 = "0,0,0,0";
                        }
                        else
                        {
                            text1 = "Waffenkammer,Waffenkomponenten,Abbrechen";
                            text2 = "0,0,0";
                        }
                    }
                    if (shopname.ToLower() == "lager lsrc")
                    {
                        text1 = "Lager,Feuerlöscher auffüllen,Abbrechen";
                        text2 = "0,0,0";
                    }
                    if (shopname.ToLower() == "lager acls")
                    {
                        text1 = "Lager,Abbrechen";
                        text2 = "0,0";
                    }
                    if (shopname.ToLower() == "ammunation")
                    {
                        if (player.HasData("Player:Ammunation") && player.GetData<bool>("Player:Ammunation"))
                        {
                            player.SetData<bool>("Player:Ammunation", false);
                            player.Heading = player.Heading - 180;
                        }
                        text1 = "Waffen kaufen,Munition kaufen,Waffenkomponenten kaufen,Abbrechen";
                        text2 = "0,0,1250/1950,0";
                    }
                    else if (shopname.ToLower() == "jägermenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Fahrzeug ausleihen,Fleisch verkaufen,Abbrechen";
                        text2 = "0,0,0,0";
                    }
                    else if (shopname.ToLower() == "müllmannmenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Müllwagen aus/einparken,Sweeper aus/einparken,Abbrechen";
                        text2 = "0,0,0,0,0";
                    }
                    else if (shopname.ToLower() == "müllroutenauswahl")
                    {
                        text1 = "";
                        text2 = "";
                        foreach (GarbageRoutes garbageRoutes in Helper.garbageRoutesList)
                        {
                            if (garbageRoutes.routes.Length > 0 && garbageRoutes.name.Length > 0 && garbageRoutes.name != "Garbage")
                            {
                                text1 = text1 + garbageRoutes.name + ",";
                                text2 = text2 + $"0,";
                            }
                        }
                        text1 = text1.Substring(0, text1.Length - 1);
                        text2 = text2.Substring(0, text2.Length - 1);
                        text1 = text1 + ",Route beenden,Abbrechen";
                        text2 = text2 + ",0,0";
                    }
                    else if (shopname.ToLower() == "landwirtmenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Traktor aus/einparken,Anhänger aus/einparken,Fahrrad aus/einparken,Abbrechen";
                        text2 = "0,0,0,0,0";
                    }
                    else if (shopname.ToLower() == "fahrschule")
                    {
                        text1 = "Führerscheinprüfung,Motorradscheinprüfung,Truckerscheinprüfung,Bootsscheinprüfung,Flugscheinprüfung,Abbrechen";
                        text2 = "1500,2500,4750,7500,10500,0";
                    }
                    else if (shopname.ToLower() == "geldlieferantmenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Stockade aus/einparken,Abbrechen";
                        text2 = "0,0,0";
                    }
                    else if (shopname.ToLower() == "busfahrermenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Normalen Bus aus/einparken,Reisebus aus/einparken,Rentalbus aus/einparken,Abbrechen";
                        text2 = "0,0,0,0,0";
                    }
                    else if (shopname.ToLower() == "laufstilauswahl")
                    {
                        text1 = "Alien,Armored,Arrogant,Brave,Casual,Casual2,Casual3,Casual4,Casual5,Casual6,Chichi,Confident,Cop,Cop2,Cop3,Default Female,Default Male,Drunk,Drunk2,Drunk3,Drunk4,Femme,Fire,Fire2,Fire3,Flee,Franklin,Gangster,Gangster2,Gangster3,Gangster4,Gangster5,Grooving,Guard,Handcuffs,Heels,Heels2,Hiking,Hipster,Hobo,Hurry,Janitor,Janitor2,Jog,Lemar,Lester,Lester2,Maneater,Michael,Money,Muscle,Posh,Posh2,Quick,Runner,Sad,Sassy,Sassy2,Scared,Sexy,Shady,Slow,Swagger,Tough,Tough2,Trash,Trash2,Trevor,Wide,Abbrechen";
                        text2 = "move_m@alien,anim_group_move_ballistic,move_f@arrogant@a,move_m@brave,move_m@casual@a,move_m@casual@b,move_m@casual@c,move_m@casual@d,move_m@casual@e,move_m@casual@f,move_f@chichi,move_m@confident,move_m@business@a,move_m@business@b,move_m@business@c,move_f@multiplayer,move_m@multiplayer,move_m@drunk@a,move_m@drunk@slightlydrunk,move_m@buzzed,move_m@drunk@verydrunk,move_f@femme@,move_characters@franklin@fire,move_characters@michael@fire,move_m@fire,move_f@flee@a,move_p_m_one,move_m@gangster@generic,move_m@gangster@ng,move_m@gangster@var_e,move_m@gangster@var_f,move_m@gangster@var_i,anim@move_m@grooving@,move_m@prison_gaurd,move_m@prisoner_cuffed,move_f@heels@c,move_f@heels@d,move_m@hiking,move_m@hipster@a,move_m@hobo@a,move_f@hurry@a,move_p_m_zero_janitor,move_p_m_zero_slow,move_m@jog@,anim_group_move_lemar_alley,move_heist_lester,move_lester_caneup,move_f@maneater,move_ped_bucket,move_m@money,move_m@muscle@a,move_m@posh@,move_f@posh@,move_m@quick,female_fast_runner,move_m@sad@a,move_m@sassy,move_f@sassy,move_f@scared,move_f@sexy@a,move_m@shadyped@a,move_characters@jimmy@slow@,move_m@swagger,move_m@tough_guy@,move_f@tough_guy@,clipset@move@trash_fast_turn,missfbi4prepp1_garbageman,move_p_m_two,move_m@bag,0";
                    }
                    else if (shopname.ToLower() == "ticketverkauf")
                    {
                        text1 = "Tagesticket kaufen,Wochenticket kaufen,Monatsticket kaufen,Abbrechen";
                        text2 = "125,575,1150,0";
                    }
                    else if (shopname.ToLower() == "routenauswahl")
                    {
                        text1 = "";
                        text2 = "";
                        foreach (BusRoutes busRoutes in Helper.busRoutesList)
                        {
                            if (busRoutes.routes.Length > 0 && busRoutes.name.Length > 0)
                            {
                                text1 = text1 + busRoutes.name + ",";
                                text2 = text2 + $"{busRoutes.skill},";
                            }
                        }
                        text1 = text1.Substring(0, text1.Length - 1);
                        text2 = text2.Substring(0, text2.Length - 1);
                        text1 = text1 + ",Route beenden,Abbrechen";
                        text2 = text2 + ",0,0";
                    }
                    else if (shopname.ToLower() == "landwirtmenu")
                    {
                        text1 = "Jobdienst beginnen/beenden,Traktor aus/einparken,Anhänger aus/einparken,Abbrechen";
                        text2 = "0,0,0,0,0";
                    }
                    else if (shopname.ToLower() == "taxifahrermenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Taxi aus/einparken,Tourbus aus/einparken,Abbrechen";
                        text2 = "0,0,0,0,0";
                    }
                    else if (shopname.ToLower() == "kanalreinigermenü")
                    {
                        text1 = "Jobdienst beginnen/beenden,Dienstfahrzeug aus/einparken,Abbrechen";
                        text2 = "0,0,0";
                    }
                    else if (shopname.ToLower() == "angelmenü")
                    {
                        text1 = "Angel kaufen,15x Köder kaufen,Boot ausleihen,Fisch verkaufen,Abbrechen";
                        text2 = "625,275,535,0,0";
                    }
                    else if (shopname.ToLower() == "schatzsuchermenü")
                    {
                        text1 = "Kleine Schaufel kaufen,Schätze verkaufen,Abbrechen";
                        text2 = $"815,{character.guessvalue},0";
                    }
                    else if (shopname.ToLower() == "mechatronikermenü")
                    {
                        Vehicle vehicle = Helper.GetClosestVehicle(player, 6.55f);
                        if (vehicle == null)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du bist nicht in der Nähe von einem Fahrzeug!", "error", "top-left", 2500);
                            return;
                        }
                        int health = (int)(1000 - NAPI.Vehicle.GetVehicleHealth(vehicle));
                        text1 = "Fahrzeug auf/abbocken,Fahrzeugdiagnose,Fahrzeug reparieren,Reifen reparieren,Tank auspumpen,Oel nachfüllen,Batterie austauschen,Schloss verbessern,Schloss austauschen,TÜV Plakette aufkleben,Abbrechen";
                        int maxspeed = vehicle.GetSharedData<int>("Vehicle:MaxSpeed");
                        maxspeed = maxspeed / 100;
                        if (maxspeed < 1)
                        {
                            maxspeed = 1;
                        }
                        int price = (health <= 0 ? 0 : (health * 2));
                        if (price > 0)
                        {
                            if (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[5] == "1")
                            {
                                price = 1000 * 5;
                            }
                            price = price / 30;
                            if (price <= 1)
                            {
                                price = 1;
                            }
                        }

                        text2 = $"0,0,{price * maxspeed},25,15,25,30,45,40,35,0";
                    }
                    player.TriggerEvent("Client:ShowShop2", text1, text2, shopname, update, show, hud, false);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowPreShop]: " + e.ToString());
            }
        }

        public static void ShowAmmunationMenu(Player player, int update = 0, string shoplabel = "Ammunation1")
        {
            try
            {
                if (IsAtAmmunation(player))
                {
                    TempData tempData = Helper.GetCharacterTempData(player);
                    Character character = Helper.GetCharacterData(player);
                    if (tempData == null || character == null) return;

                    Business bizz = Business.GetClosestBusiness(player, 40.5f);
                    if (bizz != null)
                    {
                        if (bizz.nobuy == true)
                        {
                            Helper.SendNotificationWithoutButton(player, "Du kannst jetzt hier nichts kaufen!", "error");
                            return;
                        }
                        List<ShopItems> shop = new List<ShopItems>();
                        foreach (ShopItems shopItem in shopItemList)
                        {
                            if (shopItem.shoplabel == shoplabel)
                            {
                                ShopItems shopItemTemp = new ShopItems();
                                shopItemTemp.id = shopItem.id;
                                shopItemTemp.itemname = shopItem.itemname;
                                shopItemTemp.shoplabel = "Ammunation";
                                shopItemTemp.itemprice = Convert.ToInt32(shopItem.itemprice * bizz.multiplier);
                                shop.Add(shopItemTemp);
                            }
                        }
                        player.TriggerEvent("Client:ShowShop", NAPI.Util.ToJson(shop), update, "Ammunation", 0);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowAmmunationMenu]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:VehicleSettings")]
        public static void OnVehicleSettings(Player player, string input, string numberinput = "0")
        {
            try
            {
                Account account = Helper.GetAccountData(player);
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                int number = 0;
                if (account == null || character == null || tempData == null) return;
                switch (input.ToLower())
                {
                    case "changevehicle":
                        {
                            number = Convert.ToInt32(numberinput);
                            player.SetData<int>("Player:ChangeVehicleID", number);
                            player.TriggerEvent("Client:CallInput", "Fahrzeug überschreiben", "Bitte gebe einen Spieler oder Gruppierungsnamen an!", "text", "Boss", 65, "ChangeVehicle");
                            break;
                        }
                    case "changevehicle2":
                        {
                            int found = 0;
                            Player ntarget = Helper.GetPlayerByNameOrID(numberinput);
                            Character nCharacter = null;
                            Groups group = null;
                            number = Convert.ToInt32(player.GetData<int>("Player:ChangeVehicleID"));
                            player.ResetData("Player:ChangeVehicleID");
                            if (ntarget != null)
                            {
                                nCharacter = Helper.GetCharacterData(ntarget);
                                if (nCharacter != null)
                                {
                                    found = 1;
                                }
                            }
                            else
                            {
                                group = GroupsController.GetGroupByName(numberinput);
                                if (group != null)
                                {
                                    found = 2;
                                }
                            }
                            if (found > 0)
                            {
                                if (found == 1)
                                {
                                    int maxVehicles = DealerShipController.MaxVehicles(player, 1);
                                    if (DealerShipController.CountVehicles("character-" + nCharacter.id) > maxVehicles)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Max. Anzahl an Fahrzeugen erreicht!", "error", "top-left", 2500);
                                        break;
                                    }
                                }
                                else
                                {
                                    int maxVehicles = DealerShipController.MaxVehicles(player, 2);
                                    if (DealerShipController.CountVehicles("group-" + group.id) > maxVehicles)
                                    {
                                        Helper.SendNotificationWithoutButton(player, "Max. Anzahl an Fahrzeugen erreicht!", "error", "top-left", 2500);
                                        break;
                                    }
                                }
                                foreach (Cars car in Cars.carList)
                                {
                                    if (car.vehicleData.id == number)
                                    {
                                        if (car.vehicleData.owner.Contains("group"))
                                        {
                                            Helper.CreateGroupLog(character.mygroup, $"{character.name} hat das Fahrzeug {car.vehicleData.vehiclename}[{car.vehicleData.id}] auf {nCharacter.name} umgeschrieben!");
                                        }
                                        if (found == 1)
                                        {
                                            if (car.vehicleData.vehiclename.ToLower() == "benson" || car.vehicleData.vehiclename.ToLower() == "boxville2" || car.vehicleData.vehiclename.ToLower().Contains("mule") || car.vehicleData.vehiclename.ToLower() == "pounder" || car.vehicleData.vehiclename.ToLower() == "pounder2" || car.vehicleData.vehiclename.ToLower() == "phantom" || car.vehicleData.vehiclename.ToLower() == "phantom3" || car.vehicleData.vehiclename.ToLower() == "packer" || car.vehicleData.vehiclename.ToLower().Contains("trailer") || car.vehicleData.vehiclename.ToLower() == "tanker" || car.vehicleData.vehiclename.ToLower() == "utillitruck2" || car.vehicleData.vehiclename.ToLower() == "utillitruck3" || car.vehicleData.vehiclename.ToLower() == "towtruck" || car.vehicleData.vehiclename.ToLower() == "towtruck2" || car.vehicleData.vehiclename.ToLower() == "flatbed" || car.vehicleData.vehiclename.ToLower() == "stockade")
                                            {
                                                Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nur an eine Firma überschrieben werden!", "error", "top-left", 2500);
                                                return;
                                            }
                                            if (car.vehicleData.vehiclename.ToLower() == "utillitruck2" || car.vehicleData.vehiclename.ToLower() == "utillitruck3" || car.vehicleData.vehiclename.ToLower() == "towtruck" || car.vehicleData.vehiclename.ToLower() == "towtruck2" || car.vehicleData.vehiclename.ToLower() == "flatbed" || car.vehicleData.vehiclename.ToLower() == "stockade")
                                            {
                                                string[] licArray = new string[9];
                                                licArray = group.licenses.Split("|");
                                                if (Convert.ToInt32(licArray[2]) == 0)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nur an eine Firma überschrieben werden!", "error", "top-left", 2500);
                                                    return;
                                                }
                                            }
                                            if (car.vehicleData.vehiclename.ToLower() == "bus" || car.vehicleData.vehiclename.ToLower() == "coach" || car.vehicleData.vehiclename.ToLower() == "rentalbus" || car.vehicleData.vehiclename.ToLower() == "tourbus" || car.vehicleData.vehiclename.ToLower() == "taxi")
                                            {
                                                string[] licArray = new string[9];
                                                licArray = group.licenses.Split("|");
                                                if (Convert.ToInt32(licArray[3]) == 0)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Dieses Fahrzeug kann nur an eine Firma überschrieben werden!", "error", "top-left", 2500);
                                                    return;
                                                }
                                            }
                                            car.vehicleData.owner = "character-" + nCharacter.id;
                                        }
                                        else
                                        {
                                            if (car.vehicleData.vehiclename.ToLower() == "benson" || car.vehicleData.vehiclename.ToLower() == "boxville2" || car.vehicleData.vehiclename.ToLower().Contains("mule") || car.vehicleData.vehiclename.ToLower() == "pounder" || car.vehicleData.vehiclename.ToLower() == "pounder2" || car.vehicleData.vehiclename.ToLower() == "phantom" || car.vehicleData.vehiclename.ToLower() == "phantom3" || car.vehicleData.vehiclename.ToLower() == "packer" || car.vehicleData.vehiclename.ToLower().Contains("trailer") || car.vehicleData.vehiclename.ToLower() == "tanker")
                                            {
                                                string[] licArray = new string[9];
                                                licArray = group.licenses.Split("|");
                                                if (Convert.ToInt32(licArray[0]) == 0)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Die Firma besitzt keine Speditionslizenz!", "error", "top-left", 2500);
                                                    return;
                                                }
                                            }
                                            if (car.vehicleData.vehiclename.ToLower() == "utillitruck2" || car.vehicleData.vehiclename.ToLower() == "utillitruck3" || car.vehicleData.vehiclename.ToLower() == "towtruck" || car.vehicleData.vehiclename.ToLower() == "towtruck2" || car.vehicleData.vehiclename.ToLower() == "flatbed")
                                            {
                                                string[] licArray = new string[9];
                                                licArray = group.licenses.Split("|");
                                                if (Convert.ToInt32(licArray[2]) == 0)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Die Firma besitzt keine Mechatronikerlizenz!", "error", "top-left", 2500);
                                                    return;
                                                }
                                            }
                                            if (car.vehicleData.vehiclename.ToLower() == "bus" || car.vehicleData.vehiclename.ToLower() == "coach" || car.vehicleData.vehiclename.ToLower() == "rentalbus" || car.vehicleData.vehiclename.ToLower() == "tourbus" || car.vehicleData.vehiclename.ToLower() == "taxi")
                                            {
                                                string[] licArray = new string[9];
                                                licArray = group.licenses.Split("|");
                                                if (Convert.ToInt32(licArray[3]) == 0)
                                                {
                                                    Helper.SendNotificationWithoutButton(player, "Die Firma besitzt keine Personenbeförderungslizenz!", "error", "top-left", 2500);
                                                    return;
                                                }
                                            }
                                            Helper.CreateGroupLog(character.mygroup, $"{character.name} hat das Fahrzeug {car.vehicleData.vehiclename}[{car.vehicleData.id}] auf die Gruppierung umgeschrieben!");
                                            car.vehicleData.owner = "group-" + group.id;
                                        }
                                        Helper.SendNotificationWithoutButton2(player, $"Das Fahrzeug wurde erfolgreich umgeschrieben, den Schlüssel musst du manuell übergeben!", "success", "center", 3500);
                                        player.TriggerEvent("Client:VehicleSettings", "hide", 0, account.premium);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Helper.SendNotificationWithoutButton(player, $"Ungültige Eingabe!", "error", "top-left", 2500);
                            }
                            break;
                        }
                    case "unregister":
                        {
                            if (character.cash < 350)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 350$!", "error", "top-left", 2500);
                                return;
                            }

                            number = Convert.ToInt32(numberinput);

                            foreach (Cars car in Cars.carList)
                            {
                                if (car.vehicleData.id == number)
                                {
                                    car.vehicleData.plate = "n/A";
                                    if (car.vehicleHandle != null)
                                    {
                                        car.vehicleHandle.NumberPlate = "";
                                    }
                                    if (car.vehicleData.owner.Contains("group"))
                                    {
                                        Helper.CreateGroupLog(character.mygroup, $"{character.name} hat das Fahrzeug {car.vehicleData.vehiclename}[{car.vehicleData.id}] abgemeldet!");
                                    }
                                    break;
                                }
                            }

                            CharacterController.SetMoney(player, -500);
                            Helper.SetGovMoney(500, "Fahrzeug Abmeldung");
                            Helper.SendNotificationWithoutButton2(player, $"Das Fahrzeug wurde erfolgreich abgemeldet!", "success", "center", 3500);
                            player.TriggerEvent("Client:VehicleSettings", "hide", 0, account.premium);
                            break;
                        }
                    case "keyvehicle":
                        {
                            if (character.cash < 175)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 175$!", "error", "top-left", 2500);
                                return;
                            }

                            if (!ItemsController.CanPlayerHoldItem(player, 55))
                            {
                                SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für den Fahrzeugschlüssel!", "error", "center");
                                return;
                            }

                            number = Convert.ToInt32(numberinput);

                            foreach (Cars car in Cars.carList)
                            {
                                if (car.vehicleData.id == number)
                                {
                                    Items newitem = ItemsController.CreateNewItem(player, character.id, "Fahrzeugschlüssel", "Player", 1, ItemsController.GetFreeItemID(player), car.vehicleData.vehiclename + ": " + car.vehicleData.id);
                                    if (newitem != null)
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                    break;
                                }
                            }

                            CharacterController.SetMoney(player, -175);
                            Helper.SetGovMoney(175, "Fahrzeugschlüssel Service");
                            Helper.SendNotificationWithoutButton2(player, $"Hier der neue Schlüssel für Ihr Fahrzeug!", "success", "center", 3500);
                            player.TriggerEvent("Client:VehicleSettings", "hide", 0, account.premium);
                            break;
                        }
                    case "register1":
                        {
                            bool found = true;
                            string plate = "";

                            number = Convert.ToInt32(numberinput);

                            if (character.cash < 500)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 500$!", "error", "top-left", 2500);
                                return;
                            }

                            MySqlCommand command = General.Connection.CreateCommand();
                            while (found == true)
                            {
                                plate = $"LS-{character.name[0].ToString().ToUpper()}-{GeneratePin(3)}";
                                command.CommandText = "SELECT plate FROM vehicles WHERE plate = @plate";
                                command.Parameters.AddWithValue("@plate", plate);

                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    if (!reader.HasRows)
                                    {
                                        found = false;
                                        break;
                                    }
                                }
                            }
                            foreach (Cars car in Cars.carList)
                            {
                                if (car.vehicleData.id == number)
                                {
                                    if (car.vehicleHandle.Class == 13)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Fahrräder können nicht angemeldet werden!", "error", "top-left", 2500);
                                        player.TriggerEvent("Client:ShowCursor");
                                        return;
                                    }
                                    car.vehicleData.plate = plate;
                                    if (car.vehicleHandle != null)
                                    {
                                        car.vehicleHandle.NumberPlate = plate;
                                    }
                                    if (car.vehicleData.owner.Contains("group"))
                                    {
                                        Helper.CreateGroupLog(character.mygroup, $"{character.name} hat das Fahrzeug {car.vehicleData.vehiclename}[{car.vehicleData.id}] angemeldet - {plate}!");
                                    }
                                    break;
                                }
                            }
                            CharacterController.SetMoney(player, -500);
                            Helper.SetGovMoney(500, "Fahrzeug Anmeldung");
                            Helper.SendNotificationWithoutButton2(player, $"Das Fahrzeug wurde erfolgreich angemeldet - {plate}!", "success", "center", 3500);
                            player.TriggerEvent("Client:VehicleSettings", "hide", 0, account.premium);
                            break;
                        }
                    case "register3":
                        {
                            string plate = numberinput;

                            MySqlCommand command = General.Connection.CreateCommand();

                            command.CommandText = "SELECT plate FROM vehicles WHERE plate = @plate";
                            command.Parameters.AddWithValue("@plate", plate);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    Helper.SendNotificationWithoutButton(player, $"Dieses Kennzeichen wird schon verwendet!", "error", "top-left", 2500);
                                    player.TriggerEvent("Client:ShowCursor");
                                    return;
                                }
                            }
                            foreach (Cars car in Cars.carList)
                            {
                                if (car.vehicleData.id == number)
                                {
                                    if (car.vehicleHandle.Class == 13)
                                    {
                                        Helper.SendNotificationWithoutButton(player, $"Fahrräder können nicht angemeldet werden!", "error", "top-left", 2500);
                                        player.TriggerEvent("Client:ShowCursor");
                                        return;
                                    }
                                    car.vehicleData.plate = plate;
                                    if (car.vehicleHandle != null)
                                    {
                                        car.vehicleHandle.NumberPlate = plate;
                                    }
                                    if (car.vehicleData.owner.Contains("group"))
                                    {
                                        Helper.CreateGroupLog(character.mygroup, $"{character.name} hat das Fahrzeug {car.vehicleData.vehiclename}[{car.vehicleData.id}] angemeldet - {plate}!");
                                    }
                                    break;
                                }
                            }
                            CharacterController.SetMoney(player, -750);
                            Helper.SetGovMoney(750, "Fahrzeug Anmeldung");
                            Helper.SendNotificationWithoutButton2(player, $"Das Fahrzeug wurde erfolgreich angemeldet - {plate}!", "success", "center", 3500);
                            player.TriggerEvent("Client:VehicleSettings", "hide", 0, account.premium);
                            break;
                        }
                    case "register2":
                        {
                            if (account.premium == 0)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast kein Premium!", "error", "top-left", 2500);
                                return;
                            }
                            if (character.cash < 750)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 750$!", "error", "top-left", 2500);
                                return;
                            }
                            player.TriggerEvent("Client:CallInput", "Individuelles Kennzeichen", "Wie soll dein individuelles Kennzeichen aussehen?", "text", "Boss", 8, "RegisterVehicle2");
                            break;
                        }
                    case "preshow":
                        {
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            player.TriggerEvent("Client:CallInput2", "Fahrzeugverwaltung", "Möchtest du deine privaten oder Gruppierungsfahrzeuge verwalten?", "VehicleSettings", "Privat", "Gruppierung");
                            break;
                        }
                    case "show":
                        {
                            number = Convert.ToInt32(numberinput);
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (number == 2)
                            {
                                if (group == null)
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton(player, "Du hast keine Gruppierung ausgewählt!", "error", "center");
                                    return;
                                }
                                GroupsMembers groupMember = GroupsController.GetGroupMemberById(character.id, group.id);
                                if (groupMember == null || groupMember.rang < 10)
                                {
                                    player.TriggerEvent("Client:ShowStadthalle");
                                    Helper.SendNotificationWithoutButton(player, "Keine Berechtigung!", "error", "center");
                                    return;
                                }
                            }
                            List<VehicleData> vehicleDataList = new List<VehicleData>();
                            if (number == 1)
                            {
                                foreach (Cars car in Cars.carList)
                                {
                                    if (car.vehicleData != null && car.vehicleData.owner == "character-" + character.id)
                                    {
                                        vehicleDataList.Add(car.vehicleData);
                                    }
                                }
                            }
                            else if (number == 2)
                            {
                                foreach (Cars car in Cars.carList)
                                {
                                    if (car.vehicleData.owner == "group-" + character.mygroup)
                                    {
                                        vehicleDataList.Add(car.vehicleData);
                                    }
                                }
                            }
                            if (vehicleDataList.Count <= 0)
                            {
                                player.TriggerEvent("Client:ShowStadthalle");
                                Helper.SendNotificationWithoutButton(player, "Keine Fahrzeuge vorhanden!", "error", "center");
                                return;
                            }
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            player.TriggerEvent("Client:VehicleSettings", "showui", NAPI.Util.ToJson(vehicleDataList), account.premium);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowAmmunationMenu]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:BuyShopItem")]
        public static void OnBuyShopItem(Player player, string itemname, int price, int choose, string shopname, int size)
        {
            try
            {
                Character character = GetCharacterData(player);
                TempData tempData = GetCharacterTempData(player);
                Account account = GetAccountData(player);
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                Bank bank = BankController.GetDefaultBank(player, character.defaultbank);
                if (itemname.Length > 0)
                {
                    if (bizz == null && shopname != "Waffenkammer LSPD" && shopname != "Lager LSRC" && shopname != "Lager ACLS" && shopname != "Snackpoint" && shopname != "Apotheke" && shopname != "Waffendealer" && shopname != "Big. D")
                    {
                        SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                        return;
                    }
                    if (shopname == "Lager ACLS")
                    {
                        if (choose == 3)
                        {
                            bool found = false;
                            string foundString = "";
                            string[] weaponArray = new string[7];
                            if (tempData.itemlist.Count <= 0)
                            {
                                SendNotificationWithoutButton(player, "Dein Inventar ist leer!", "error", "top-left", 1750);
                                return;
                            }
                            foreach (ShopItems shopItems in Helper.shopItemList)
                            {
                                if (shopItems.shoplabel == "Waffenkammer-" + character.faction)
                                {
                                    if (shopItems != null)
                                    {
                                        Items weaponItem = ItemsController.GetItemByItemName(player, shopItems.itemname);
                                        if (weaponItem != null)
                                        {
                                            weaponArray = weaponItem.props.Split(",");
                                            if (weaponItem.type != 5 || (weaponItem.type == 5 && weaponArray[1] == "0" && weaponArray[4] == "LSC-Waffenkammer"))
                                            {
                                                found = true;
                                                foundString += $"{weaponItem.amount}x {weaponItem.description}, ";
                                                shopItems.itemprice += weaponItem.amount;
                                                ItemsController.RemoveItem(player, weaponItem.itemid);
                                            }
                                        }
                                    }
                                }
                            }
                            if (found == true)
                            {
                                if (foundString.Length > 2)
                                {
                                    foundString = foundString.Substring(0, foundString.Length - 2);
                                }
                                Helper.CreateWeaponLog(character.faction, $"{character.name} hat {foundString} in die Waffenkammer zurückgelegt!");
                                SendNotificationWithoutButton(player, $"Du hast {foundString} zurückgelegt!", "success", "top-left", 2750);
                                ShowWaffenkammer(player, character.faction, 1, 0);
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Du kannst nichts zurücklegen!", "error", "top-left", 1750);
                            }
                        }
                        else
                        {
                            string props = "n/A";
                            Items item = new Items();
                            ItemsController.GetItemByName(item, itemname);
                            if (item != null)
                            {
                                ShopItems shopItemTemp = null;
                                if (choose != 3)
                                {
                                    foreach (ShopItems shopItems in Helper.shopItemList)
                                    {
                                        if (shopItems.shoplabel == "Waffenkammer-" + character.faction)
                                        {
                                            if (shopItems.itemname == itemname)
                                            {
                                                shopItemTemp = shopItems;
                                            }
                                        }
                                    }
                                }
                                if (choose == 1)
                                {
                                    if (shopItemTemp == null || shopItemTemp.itemprice < size)
                                    {
                                        SendNotificationWithoutButton(player, "Soviel ist nichtmehr in der Waffenkammer vorhanden!", "error", "top-left", 1750);
                                        return;
                                    }
                                    if (!ItemsController.CanPlayerHoldItem(player, item.weight * size))
                                    {
                                        SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                        return;
                                    }
                                    Items newitem = null;
                                    newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props, "LSC-Waffenkammer", character.name);
                                    if (newitem != null)
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                    SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} aus der Waffenkammer genommen!", "success", "top-left", 1750);
                                    Helper.CreateWeaponLog(character.faction, $"{character.name} hat {size}x {itemname} aus der Waffenkammer genommen!");
                                    shopItemTemp.itemprice -= size;
                                }
                                else
                                {
                                    string[] weaponArray = new string[7];
                                    Items weaponItem = ItemsController.GetItemByItemName(player, itemname);
                                    if (weaponItem != null)
                                    {
                                        weaponArray = weaponItem.props.Split(",");
                                        if (weaponItem.type != 5 || (weaponItem.type == 5 && weaponArray[1] == "0" && weaponArray[4] == "LSC-Waffenkammer"))
                                        {
                                            shopItemTemp.itemprice += weaponItem.amount;
                                            SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} in die Waffenkammer zurückgelegt!", "success", "top-left", 1750);
                                            Helper.CreateWeaponLog(character.faction, $"{character.name} hat {size}x {itemname} in die Waffenkammer zurückgelegt!");
                                            ItemsController.RemoveItem(player, weaponItem.itemid);
                                        }
                                        else
                                        {
                                            SendNotificationWithoutButton(player, $"Das Item ist ausgerüstet oder kann nicht zurückgelegt werden!", "error", "top-left", 1750);
                                        }
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, $"Du kannst nichts zurücklegen!", "error", "top-left", 1750);
                                    }
                                }
                            }
                            ShowWaffenkammer(player, character.faction, 1, 0);
                        }
                        return;
                    }
                    if (shopname == "Lager LSRC")
                    {
                        if (choose == 3)
                        {
                            bool found = false;
                            string foundString = "";
                            string[] weaponArray = new string[7];
                            if (tempData.itemlist.Count <= 0)
                            {
                                SendNotificationWithoutButton(player, "Dein Inventar ist leer!", "error", "top-left", 1750);
                                return;
                            }
                            foreach (ShopItems shopItems in Helper.shopItemList)
                            {
                                if (shopItems.shoplabel == "Waffenkammer-" + character.faction)
                                {
                                    if (shopItems != null)
                                    {
                                        Items weaponItem = ItemsController.GetItemByItemName(player, shopItems.itemname);
                                        if (weaponItem != null)
                                        {
                                            weaponArray = weaponItem.props.Split(",");
                                            if (weaponItem.type != 5 || (weaponItem.type == 5 && weaponArray[1] == "0" && weaponArray[4] == "LSRC-Waffenkammer"))
                                            {
                                                found = true;
                                                foundString += $"{weaponItem.amount}x {weaponItem.description}, ";
                                                shopItems.itemprice += weaponItem.amount;
                                                ItemsController.RemoveItem(player, weaponItem.itemid);
                                            }
                                        }
                                    }
                                }
                            }
                            if (found == true)
                            {
                                if (foundString.Length > 2)
                                {
                                    foundString = foundString.Substring(0, foundString.Length - 2);
                                }
                                Helper.CreateWeaponLog(character.faction, $"{character.name} hat {foundString} in die Waffenkammer zurückgelegt!");
                                SendNotificationWithoutButton(player, $"Du hast {foundString} zurückgelegt!", "success", "top-left", 2750);
                                ShowWaffenkammer(player, character.faction, 1, 0);
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Du kannst nichts zurücklegen!", "error", "top-left", 1750);
                            }
                        }
                        else
                        {
                            string props = "n/A";
                            Items item = new Items();
                            ItemsController.GetItemByName(item, itemname);
                            if (item != null)
                            {
                                ShopItems shopItemTemp = null;
                                if (choose != 3)
                                {
                                    foreach (ShopItems shopItems in Helper.shopItemList)
                                    {
                                        if (shopItems.shoplabel == "Waffenkammer-" + character.faction)
                                        {
                                            if (shopItems.itemname == itemname)
                                            {
                                                shopItemTemp = shopItems;
                                            }
                                        }
                                    }
                                }
                                if (choose == 1)
                                {
                                    if (shopItemTemp == null || shopItemTemp.itemprice < size)
                                    {
                                        SendNotificationWithoutButton(player, "Soviel ist nichtmehr in der Waffenkammer vorhanden!", "error", "top-left", 1750);
                                        return;
                                    }
                                    if (!ItemsController.CanPlayerHoldItem(player, item.weight * size))
                                    {
                                        SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                        return;
                                    }
                                    Items newitem = null;
                                    newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props, "LSRC-Waffenkammer", character.name);
                                    if (newitem != null)
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                    SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} aus der Waffenkammer genommen!", "success", "top-left", 1750);
                                    Helper.CreateWeaponLog(character.faction, $"{character.name} hat {size}x {itemname} aus der Waffenkammer genommen!");
                                    shopItemTemp.itemprice -= size;
                                }
                                else
                                {
                                    string[] weaponArray = new string[7];
                                    Items weaponItem = ItemsController.GetItemByItemName(player, itemname);
                                    if (weaponItem != null)
                                    {
                                        weaponArray = weaponItem.props.Split(",");
                                        if (weaponItem.type != 5 || (weaponItem.type == 5 && weaponArray[1] == "0" && weaponArray[4] == "LSRC-Waffenkammer"))
                                        {
                                            shopItemTemp.itemprice += weaponItem.amount;
                                            SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} in die Waffenkammer zurückgelegt!", "success", "top-left", 1750);
                                            Helper.CreateWeaponLog(character.faction, $"{character.name} hat {size}x {itemname} in die Waffenkammer zurückgelegt!");
                                            ItemsController.RemoveItem(player, weaponItem.itemid);
                                        }
                                        else
                                        {
                                            SendNotificationWithoutButton(player, $"Das Item ist ausgerüstet oder kann nicht zurückgelegt werden!", "error", "top-left", 1750);
                                        }
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, $"Du kannst nichts zurücklegen!", "error", "top-left", 1750);
                                    }
                                }
                            }
                            ShowWaffenkammer(player, character.faction, 1, 0);
                        }
                        return;
                    }
                    if (shopname == "Waffenkammer LSPD")
                    {
                        if (choose == 3)
                        {
                            bool found = false;
                            string foundString = "";
                            string[] weaponArray = new string[7];
                            if (tempData.itemlist.Count <= 0)
                            {
                                SendNotificationWithoutButton(player, "Dein Inventar ist leer!", "error", "top-left", 1750);
                                return;
                            }
                            foreach (ShopItems shopItems in Helper.shopItemList)
                            {
                                if (shopItems.shoplabel == "Waffenkammer-" + character.faction)
                                {
                                    if (shopItems != null)
                                    {
                                        Items weaponItem = ItemsController.GetItemByItemName(player, shopItems.itemname);
                                        if (weaponItem != null)
                                        {
                                            weaponArray = weaponItem.props.Split(",");
                                            if (weaponItem.type != 5 || (weaponItem.type == 5 && weaponArray[1] == "0" && weaponArray[4] == "LSPD-Waffenkammer"))
                                            {
                                                found = true;
                                                foundString += $"{weaponItem.amount}x {weaponItem.description}, ";
                                                shopItems.itemprice += weaponItem.amount;
                                                ItemsController.RemoveItem(player, weaponItem.itemid);
                                            }
                                        }
                                    }
                                }
                            }
                            if (found == true)
                            {
                                if (foundString.Length > 2)
                                {
                                    foundString = foundString.Substring(0, foundString.Length - 2);
                                }
                                Helper.CreateWeaponLog(character.faction, $"{character.name} hat {foundString} in die Waffenkammer zurückgelegt!");
                                SendNotificationWithoutButton(player, $"Du hast {foundString} zurückgelegt!", "success", "top-left", 2750);
                                ShowWaffenkammer(player, character.faction, 1, 0);
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Du kannst nichts zurücklegen!", "error", "top-left", 1750);
                            }
                        }
                        else
                        {
                            string props = "n/A";
                            Items item = new Items();
                            ItemsController.GetItemByName(item, itemname);
                            if (item != null)
                            {
                                ShopItems shopItemTemp = null;
                                if (choose != 3)
                                {
                                    foreach (ShopItems shopItems in Helper.shopItemList)
                                    {
                                        if (shopItems.shoplabel == "Waffenkammer-" + character.faction)
                                        {
                                            if (shopItems.itemname == itemname)
                                            {
                                                shopItemTemp = shopItems;
                                            }
                                        }
                                    }
                                }
                                if (choose == 1)
                                {
                                    if (shopItemTemp == null || shopItemTemp.itemprice < size)
                                    {
                                        SendNotificationWithoutButton(player, "Soviel ist nichtmehr in der Waffenkammer vorhanden!", "error", "top-left", 1750);
                                        return;
                                    }
                                    if (!ItemsController.CanPlayerHoldItem(player, item.weight * size))
                                    {
                                        SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                        return;
                                    }
                                    if (character.swat == 0 && (item.description == "Sniper" || item.description == "BZGas" || item.description == "Karabiner-Gewehr-MK2"))
                                    {
                                        SendNotificationWithoutButton(player, "Keine Berechtigung!", "error", "top-end");
                                        return;
                                    }
                                    Items newitem = null;
                                    newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props, "LSPD-Waffenkammer", character.name);
                                    if (newitem != null)
                                    {
                                        tempData.itemlist.Add(newitem);
                                    }
                                    SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} aus der Waffenkammer genommen!", "success", "top-left", 1750);
                                    Helper.CreateWeaponLog(character.faction, $"{character.name} hat {size}x {itemname} aus der Waffenkammer genommen!");
                                    shopItemTemp.itemprice -= size;
                                }
                                else
                                {
                                    string[] weaponArray = new string[7];
                                    Items weaponItem = ItemsController.GetItemByItemName(player, itemname);
                                    if (weaponItem != null)
                                    {
                                        weaponArray = weaponItem.props.Split(",");
                                        if (weaponItem.type != 5 || (weaponItem.type == 5 && weaponArray[1] == "0" && weaponArray[4] == "LSPD-Waffenkammer"))
                                        {
                                            shopItemTemp.itemprice += weaponItem.amount;
                                            SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} in die Waffenkammer zurückgelegt!", "success", "top-left", 1750);
                                            Helper.CreateWeaponLog(character.faction, $"{character.name} hat {size}x {itemname} in die Waffenkammer zurückgelegt!");
                                            ItemsController.RemoveItem(player, weaponItem.itemid);
                                        }
                                        else
                                        {
                                            SendNotificationWithoutButton(player, $"Das Item ist ausgerüstet oder kann nicht zurückgelegt werden!", "error", "top-left", 1750);
                                        }
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, $"Du kannst nichts zurücklegen!", "error", "top-left", 1750);
                                    }
                                }
                            }
                            ShowWaffenkammer(player, character.faction, 1, 0);
                        }
                        return;
                    }
                    else if (shopname == "24/7 Laden")
                    {
                        if (itemname != "Tankrechnung" && itemname != "Pfandflasche")
                        {
                            Items item = new Items();
                            ItemsController.GetItemByName(item, itemname);
                            if (item != null)
                            {
                                if (bizz.products < (price / 25) / 4)
                                {
                                    Helper.SendNotificationWithoutButton(player, "Soviel haben wir leider nichtmehr auf Lager!", "error");
                                    return;
                                }
                                if (choose == 1)
                                {
                                    if (character.cash < price)
                                    {
                                        SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (bank == null)
                                    {
                                        SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                        return;
                                    }
                                    if (bank.bankvalue < price)
                                    {
                                        SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                        return;
                                    }
                                }
                                if (item.description == "L-Schein")
                                {
                                    Items lItem = ItemsController.GetItemByItemName(player, "L-Schein");
                                    if (lItem != null)
                                    {
                                        SendNotificationWithoutButton(player, $"Du hast schon einen Lottoschein!", "error", "top-left");
                                        return;
                                    }
                                    if (character.defaultbank == "n/A")
                                    {
                                        SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                        return;
                                    }
                                    if (size > 1)
                                    {
                                        size = 1;
                                    }
                                }
                                if (!ItemsController.CanPlayerHoldItem(player, item.weight * size) && item.description != "Zigaretten")
                                {
                                    SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                    return;
                                }
                                if (!ItemsController.CanPlayerHoldItem(player, (item.weight * 20) * size) && item.description == "Zigaretten")
                                {
                                    SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                    return;
                                }
                                string props = "n/A";
                                if (itemname == "Benzinkanister")
                                {
                                    props = "25 Liter";
                                }
                                if (itemname == "Feuerzeug")
                                {
                                    props = "20";
                                }
                                if (itemname == "L-Schein")
                                {
                                    Random random = new Random();
                                    props = "";
                                    for (int i = 0; i < 5; i++)
                                    {
                                        props = props + $"{random.Next(1, 49)},";
                                    }
                                    props = props.Remove(props.Length - 1);
                                }
                                if (itemname == "Smartphone")
                                {
                                    Random RndNum = new Random();
                                    MySqlCommand command = General.Connection.CreateCommand();
                                    int RnNum = 0;
                                    bool found = true;
                                    while (found == true)
                                    {
                                        RnNum = RndNum.Next(100000, 999999);

                                        command.CommandText = "SELECT phonenumber FROM smartphones WHERE phonenumber = @phonenumber";
                                        command.Parameters.AddWithValue("@phonenumber", RnNum);

                                        using (MySqlDataReader reader = command.ExecuteReader())
                                        {
                                            if (reader.HasRows)
                                            {
                                                found = true;
                                            }
                                            else
                                            {
                                                found = false;
                                            }
                                            reader.Close();
                                        }
                                    }

                                    SmartphoneProps phoneProps = new SmartphoneProps();
                                    phoneProps.phonenumber = "0189" + RnNum;

                                    Smartphone phone = new Smartphone();
                                    phone.phonenumber = "" + RnNum;

                                    phone.phoneprops = NAPI.Util.ToJson(phoneProps);

                                    command.CommandText = "INSERT INTO smartphones (phonenumber, phoneprops) VALUES (@insertnumber, @phoneprops)";
                                    command.Parameters.AddWithValue("@insertnumber", RnNum);
                                    command.Parameters.AddWithValue("@phoneprops", phone.phoneprops);
                                    command.ExecuteNonQuery();

                                    phone.id = (int)command.LastInsertedId;

                                    SmartphoneController.smartphoneList.Add(phone);
                                    props = "0189" + RnNum;

                                    if (account.faqarray[3] == "0")
                                    {
                                        account.faqarray[3] = "1";
                                    }
                                }
                                Items newitem = null;
                                if (itemname.ToLower() != "zigaretten")
                                {
                                    newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props);
                                }
                                else
                                {
                                    newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", 20 * size, ItemsController.GetFreeItemID(player), props);
                                }
                                if (newitem != null)
                                {
                                    tempData.itemlist.Add(newitem);
                                }
                                if (choose == 1)
                                {
                                    CharacterController.SetMoney(player, -price);
                                }
                                else
                                {
                                    bank.bankvalue -= price;
                                }
                                Business.ManageBizzCash(bizz, price);
                                if (itemname != "Smartphone" && itemname != "L-Schein")
                                {
                                    SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} für {price}$ erworben!", "success", "top-left", 1750);
                                }
                                else if (itemname == "L-Schein")
                                {
                                    SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} für {price}$ erworben, die Lottoausschüttzung ist immer am Mittwoch um 18 Uhr!", "success", "top-left", 4750);
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} (Prepaid) für {price}$ erworben!", "success", "top-left", 3350);
                                }
                                if (choose == 2)
                                {
                                    Helper.BankSettings(bank.banknumber, "24/7 Rechnung bezahlt", price.ToString(), character.name);
                                }
                            }
                        }
                        else
                        {
                            if (itemname == "Tankrechnung")
                            {
                                if (player.HasData("Player:FuelPrice") && player.GetData<int>("Player:FuelPrice") > 0)
                                {
                                    if (player.GetData<int>("Player:FuelBizz") == bizz.id)
                                    {
                                        if (choose == 1)
                                        {
                                            if (character.cash < price)
                                            {
                                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (bank == null)
                                            {
                                                SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                                return;
                                            }
                                            if (bank.bankvalue < price)
                                            {
                                                SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                                return;
                                            }
                                        }
                                        SendNotificationWithoutButton(player, $"Du hast die Tankrechnung in Höhe von {price}$ bezahlt!", "success", "top-end", 2500);
                                        if (choose == 1)
                                        {
                                            CharacterController.SetMoney(player, -price);
                                        }
                                        else
                                        {
                                            bank.bankvalue -= price;
                                        }
                                        player.SetData<int>("Player:FuelPrice", 0);
                                        player.SetData<int>("Player:FuelCooldown", 0);
                                        player.SetData<int>("Player:FuelBizz", 0);
                                        Business.ManageBizzCash(bizz, price, true);
                                        Show247Menu(player, 1);
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, "Du hast keine offene Tankrechnung bei uns!", "error", "top-end");
                                    }
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Du hast keine offene Tankrechnung!", "error", "top-end");
                                }
                            }
                            else
                            {
                                if (bizz.cash < price)
                                {
                                    SendNotificationWithoutButton(player, "Das Business hat nicht genügend Geld dir die Pfandflaschen abzukaufen!", "error", "top-end");
                                    return;
                                }
                                if (bizz.products < (int)(price / 25))
                                {
                                    bizz.products += (int)(price / 25);
                                }
                                Business.ManageBizzCash(bizz, -price, true);
                                CharacterController.SetMoney(player, price);
                                ItemsController.RemoveItemByName(player, "Pfandflasche");
                                SendNotificationWithoutButton(player, $"Du hast deine Pfandflaschen verkauft, und hast {price}$ bekommen!", "success", "top-end", 2500);
                                Show247Menu(player, 1);
                            }
                        }
                    }
                    else if (shopname == "Snackpoint")
                    {
                        Items item = new Items();
                        ItemsController.GetItemByName(item, itemname);
                        if (item != null)
                        {
                            if (choose == 1)
                            {
                                if (character.cash < price)
                                {
                                    SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                                    return;
                                }
                            }
                            else
                            {
                                if (bank == null)
                                {
                                    SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                    return;
                                }
                                if (bank.bankvalue < price)
                                {
                                    SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                    return;
                                }
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, item.weight * size))
                            {
                                SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            string props = "n/A";
                            Items newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props);
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            if (choose == 1)
                            {
                                CharacterController.SetMoney(player, -price);
                            }
                            else
                            {
                                bank.bankvalue -= price;
                            }
                            SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} für {price}$ erworben!", "success", "top-left", 1750);
                            if (choose == 2)
                            {
                                Helper.BankSettings(bank.banknumber, "Snackpoint Rechnung bezahlt", price.ToString(), character.name);
                            }
                        }
                    }
                    else if (shopname == "Bar")
                    {
                        Items item = new Items();
                        ItemsController.GetItemByName(item, itemname);
                        if (item != null)
                        {
                            if (choose == 1)
                            {
                                if (character.cash < price)
                                {
                                    SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                                    return;
                                }
                            }
                            else
                            {
                                if (bank == null)
                                {
                                    SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                    return;
                                }
                                if (bank.bankvalue < price)
                                {
                                    SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                    return;
                                }
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, item.weight * size))
                            {
                                SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            string props = "n/A";
                            Items newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props);
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            if (choose == 1)
                            {
                                CharacterController.SetMoney(player, -price);
                            }
                            else
                            {
                                bank.bankvalue -= price;
                            }
                            Business.ManageBizzCash(bizz, price, true);
                            SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} für {price}$ erworben!", "success", "top-left", 1750);
                            if (choose == 2)
                            {
                                Helper.BankSettings(bank.banknumber, "Bar Rechnung bezahlt", price.ToString(), character.name);
                            }
                        }
                    }
                    else if (shopname == "Apotheke")
                    {
                        Items item = new Items();
                        Items checkItem = new Items();
                        ItemsController.GetItemByName(item, itemname);
                        if (item != null)
                        {
                            if (itemname.ToLower() == "ibuprofee-800mg" || itemname.ToLower() == "antibiotika" || itemname.ToLower() == "morphin-10mg")
                            {
                                checkItem = ItemsController.GetItemFromProp(player, itemname);
                                if (checkItem == null)
                                {
                                    SendNotificationWithoutButton(player, $"Du benötigst ein Rezept für dieses Medikament!", "error");
                                    return;
                                }
                            }
                            if (choose == 1)
                            {
                                if (character.cash < price)
                                {
                                    SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price}$!", "error");
                                    return;
                                }
                            }
                            else
                            {
                                if (bank == null)
                                {
                                    SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                    return;
                                }
                                if (bank.bankvalue < price)
                                {
                                    SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price}$!", "error", "top-left");
                                    return;
                                }
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, item.weight * size))
                            {
                                SendNotificationWithoutButton(player, "Du kannst das Item/die Items nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            string props = "n/A";
                            Items newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props);
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            if (choose == 1)
                            {
                                CharacterController.SetMoney(player, -price);
                            }
                            else
                            {
                                bank.bankvalue -= price;
                            }
                            if (checkItem != null)
                            {
                                if (checkItem.amount > 0)
                                {
                                    checkItem.amount--;
                                }
                                if (checkItem.amount <= 0)
                                {
                                    ItemsController.RemoveItem(player, checkItem.itemid);
                                }
                            }
                            SendNotificationWithoutButton(player, $"Du hast {size}x {itemname} für {price}$ erworben!", "success", "top-left", 1750);
                            if (choose == 2)
                            {
                                Helper.BankSettings(bank.banknumber, "Bar Rechnung bezahlt", price.ToString(), character.name);
                            }
                            Helper.SetGovMoney(price, "Apotheken Verkauf");
                        }
                    }
                    else if (shopname == "Ammunation")
                    {
                        Items item = new Items();
                        ItemsController.GetItemByName(item, itemname);
                        if (item != null)
                        {
                            if (bizz.products < ((price * size) / 25) / 4)
                            {
                                Helper.SendNotificationWithoutButton(player, "Unsere Lager sind leider leer!", "error");
                                return;
                            }
                            if (choose == 1)
                            {
                                if (character.cash < price)
                                {
                                    SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price * size}$!", "error");
                                    return;
                                }
                            }
                            else
                            {
                                if (bank == null)
                                {
                                    SendNotificationWithoutButton(player, "Es wurde kein Standardkonto gefunden!", "error", "top-left");
                                    return;
                                }
                                if (bank.bankvalue < price)
                                {
                                    SendNotificationWithoutButton(player, $"Soviel Geld liegt nicht auf dem Konto - {price * size}$!", "error", "top-left");
                                    return;
                                }
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, item.weight))
                            {
                                SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            string props = "n/A";
                            int ammo = size;
                            if (item.description.ToLower().Contains("munition"))
                            {
                                ammo = size * 25;
                            }
                            Items newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", ammo, ItemsController.GetFreeItemID(player), props, "Ammunation", character.name);
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            if (choose == 1)
                            {
                                CharacterController.SetMoney(player, -price);
                            }
                            else
                            {
                                bank.bankvalue -= price;
                            }
                            Business.ManageBizzCash(bizz, price);
                            SendNotificationWithoutButton(player, $"Du hast ein/e/en {itemname} für {price * size}$ erworben!", "success", "top-left", 1750);
                            if (choose != 1)
                            {
                                Helper.BankSettings(bank.banknumber, "Ammunation Rechnung bezahlt", (price * size).ToString(), character.name);
                            }
                        }
                    }
                    else if (shopname == "Waffendealer")
                    {
                        int count = 0;
                        Items item = new Items();
                        ItemsController.GetItemByName(item, itemname);
                        if (item != null)
                        {
                            if (GangController.dealerPosition == null)
                            {
                                SendNotificationWithoutButton(player, $"Sorry, ich muss jetzt schnell weg!", "error");
                                return;
                            }
                            if (character.cash < (price * size))
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price * size}$!", "error");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, item.weight))
                            {
                                SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            foreach (ShopItems shopItem in shopItemList)
                            {
                                if (shopItem.shoplabel == "Waffenkammer-A")
                                {
                                    if (shopItem.itemname == itemname)
                                    {
                                        if (GangController.dealerAmount[count] < size)
                                        {
                                            SendNotificationWithoutButton(player, "Soviel ist nichtmehr von diesem Gegenstand auf Lager!", "error", "top-end");
                                            return;
                                        }
                                        break;
                                    }
                                    count++;
                                }
                            }
                            string props = "n/A";
                            int ammo = size;
                            if (item.description.ToLower().Contains("munition"))
                            {
                                ammo = size * 25;
                            }
                            Items newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", ammo, ItemsController.GetFreeItemID(player), props, "n/A");
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            GangController.dealerAmount[count] -= size;
                            CharacterController.SetMoney(player, -price);
                            SendNotificationWithoutButton(player, $"Du hast ein/e/en {itemname} für {price * size}$ erworben!", "success", "top-left", 1750);
                            player.TriggerEvent("Client:UpdateHud3");
                            Helper.ShowDealerShop(player, 1);
                        }
                    }
                    else if (shopname == "Big. D")
                    {
                        int count = 0;
                        Items item = new Items();
                        ItemsController.GetItemByName(item, itemname);
                        if (item != null)
                        {
                            if (GangController.dealerPosition2 == null)
                            {
                                SendNotificationWithoutButton(player, $"Sorry, ich muss jetzt schnell weg!", "error");
                                return;
                            }
                            if (character.cash < (price * size))
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {price * size}$!", "error");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, item.weight))
                            {
                                SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            foreach (ShopItems shopItem in shopItemList)
                            {
                                if (shopItem.shoplabel == "Waffenkammer-D")
                                {
                                    if (shopItem.itemname == itemname)
                                    {
                                        if (GangController.drugAmount < size)
                                        {
                                            SendNotificationWithoutButton(player, "Soviel ist nichtmehr von diesem Gegenstand auf Lager!", "error", "top-end");
                                            return;
                                        }
                                        break;
                                    }
                                    count++;
                                }
                            }
                            string props = "n/A";
                            Items newitem = ItemsController.CreateNewItem(player, character.id, itemname, "Player", size, ItemsController.GetFreeItemID(player), props, "n/A");
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            GangController.drugAmount--;
                            CharacterController.SetMoney(player, -price);
                            SendNotificationWithoutButton(player, $"Du hast ein/e/en {itemname} für {price * size}$ erworben!", "success", "top-left", 1750);
                            player.TriggerEvent("Client:UpdateHud3");
                            Helper.ShowDealerShop2(player, 1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnBuyShopItem]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:VehicleJack")]
        public static void OnVehicleJack(Player player)
        {
            try
            {
                TempData tempData = GetCharacterTempData(player);
                Vehicle vehicle = Helper.GetClosestVehicle(player, 6.55f);
                if (vehicle.HasData("Vehicle:Jacked") && vehicle.GetData<bool>("Vehicle:Jacked") == true)
                {
                    NAPI.Task.Run(() =>
                    {
                        vehicle.SetData<bool>("Vehicle:Jacked", false);
                        vehicle.ResetData("Vehicle:Jacked");
                        if (vehicle.HasData("Vehicle:JackedObject"))
                        {
                            vehicle.GetData<GTANetworkAPI.Object>("Vehicle:JackedObject").Delete();
                            vehicle.ResetData("Vehicle:JackedObject");
                        }
                        string[] vehicleArray = new string[7];
                        vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                        vehicle.SetSharedData("Vehicle:Sync", $"0,0,{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},0,{vehicleArray[6]}");
                        Helper.SetVehicleEngine(vehicle, false);
                    }, delayTime: 10000);
                }
                else
                {
                    string[] vehicleArray = new string[7];
                    vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                    vehicle.SetSharedData("Vehicle:Sync", $"0,0,{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},1|{vehicle.Position.Z.ToString(new CultureInfo("en-US"))},{vehicleArray[6]}");
                    vehicle.SetData<bool>("Vehicle:Jacked", true);
                    Vector3 newPosition = GetPositionInFrontOfPlayer(player, 1.75f);
                    Vector3 newPosition2 = GetPositionInFrontOfPlayer(player, 1.255f);
                    vehicle.Position = new Vector3(newPosition.X, newPosition.Y, vehicle.Position.Z);
                    Helper.SetVehicleEngine(vehicle, false);
                    vehicle.SetData<GTANetworkAPI.Object>("Vehicle:JackedObject", NAPI.Object.CreateObject(NAPI.Util.GetHashKey("prop_carjack"), new Vector3(newPosition2.X, newPosition2.Y, vehicle.Position.Z - 0.5), new Vector3(vehicle.Rotation.X, vehicle.Rotation.Y, vehicle.Rotation.Z + 90), 255, vehicle.Dimension));
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnVehicleJack]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:BuyShopItem2")]
        public static void OnBuyShopItem2(Player player, string text1, string text2)
        {
            try
            {
                Character character = GetCharacterData(player);
                Account account = GetAccountData(player);
                TempData tempData = GetCharacterTempData(player);
                Business bizz = Business.GetClosestBusiness(player, 40.5f);
                Bank bank = BankController.GetDefaultBank(player, character.defaultbank);
                if (text1.Length > 0)
                {
                    if ((bizz == null || tempData.lastShop == "n/A") && tempData.lastShop.ToLower() != "mechatronikermenü" && tempData.lastShop.ToLower() != "jägermenü" && tempData.lastShop.ToLower() != "angelmenü" && tempData.lastShop.ToLower() != "schatzsuchermenü" && tempData.lastShop.ToLower() != "müllmannmenü" && tempData.lastShop.ToLower() != "busfahrermenü" && tempData.lastShop.ToLower() != "routenauswahl" && tempData.lastShop.ToLower() != "müllroutenauswahl" && tempData.lastShop.ToLower() != "ticketverkauf" && tempData.lastShop.ToLower() != "taxifahrermenü" && tempData.lastShop.ToLower() != "kanalreinigermenü" && tempData.lastShop.ToLower() != "landwirtmenü"
                    && tempData.lastShop.ToLower() != "animationsmenü" && tempData.lastShop.ToLower() != "animationsauswahl" && tempData.lastShop.ToLower() != "laufstilauswahl" && tempData.lastShop.ToLower() != "geldlieferantmenü" && tempData.lastShop.ToLower() != "fahrschule" && tempData.lastShop.ToLower() != "police-department" && tempData.lastShop.ToLower() != "waffenkammer lspd" && tempData.lastShop.ToLower() != "lager lsrc" && tempData.lastShop.ToLower() != "lsrc dach" && tempData.lastShop.ToLower() != "lager acls" && tempData.lastShop.ToLower() != "gutschein erstellen")
                    {
                        SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                        return;
                    }
                    if (tempData.lastShop == "Schatzsuchermenü")
                    {
                        if (text1 == "Kleine Schaufel kaufen")
                        {
                            if (character.cash < Convert.ToInt32(text2))
                            {
                                SendNotificationWithoutButton(player, $"Billy: Du hast nicht genügend Geld dabei - {text2}$!", "error", "top-end");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, 375))
                            {
                                SendNotificationWithoutButton(player, "Billy: Du hast keinen Platz mehr bei dir im Inventar!", "error", "top-end");
                                return;
                            }
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "Kleine-Schaufel", "Player", 1, ItemsController.GetFreeItemID(player));
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            CharacterController.SetMoney(player, -Convert.ToInt32(text2));
                            SendNotificationWithoutButton(player, $"Billy: Du hast dir erfolgreich eine Kleine-Schaufel für {text2}$ erworben, du kannst hier in der Nähe mit der Taste [F4] nach Schätzen suchen!", "success", "top-end", 6500);
                            player.TriggerEvent("Client:PressedEscape");
                            player.TriggerEvent("Client:HideStadthalle");
                        }
                        else if (text1 == "Schätze verkaufen")
                        {
                            if (character.guessvalue <= 0)
                            {
                                SendNotificationWithoutButton(player, "Billy: Du hast noch keine Schätze gefunden!", "error", "top-end");
                                return;
                            }
                            if (Helper.adminSettings.dailyguesslimit >= 65000)
                            {
                                SendNotificationWithoutButton(player, "Billy: Ich habe aktuell kein Geld mehr, komm später nochmal vorbei!", "error", "top-end");
                                return;
                            }
                            Helper.adminSettings.dailyguesslimit += character.guessvalue;
                            CharacterController.SetMoney(player, character.guessvalue);
                            SendNotificationWithoutButton(player, $"Billy: Du hast Schätze im Wert von {character.guessvalue}$ verkauft!", "success", "top-end", 5500);
                            character.guessvalue = 0;
                            player.TriggerEvent("Client:PressedEscape");
                            player.TriggerEvent("Client:HideStadthalle");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                            NAPI.Task.Run(() =>
                            {
                                player.TriggerEvent("Client:HideStadthalle");
                            }, delayTime: 15);
                        }
                    }
                    if (tempData.lastShop == "Angelmenü")
                    {
                        if (text1 == "Angel kaufen")
                        {
                            if (character.cash < Convert.ToInt32(text2))
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {text2}$!", "error", "top-end");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, 500))
                            {
                                SendNotificationWithoutButton(player, "Du hast keinen Platz mehr bei dir im Inventar!", "error", "top-end");
                                return;
                            }
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "Angel", "Player", 1, ItemsController.GetFreeItemID(player));
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            CharacterController.SetMoney(player, -Convert.ToInt32(text2));
                            SendNotificationWithoutButton(player, $"Du hast dir erfolgreich eine Angel für {text2}$ erworben!", "success", "top-end", 2500);
                        }
                        else if (text1 == "15x Köder kaufen")
                        {
                            if (character.cash < Convert.ToInt32(text2))
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {text2}$!", "error", "top-end");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, (15 * 25)))
                            {
                                SendNotificationWithoutButton(player, "Du hast keinen Platz mehr bei dir im Inventar!", "error", "top-end");
                                return;
                            }
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "Köder", "Player", 15, ItemsController.GetFreeItemID(player));
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            CharacterController.SetMoney(player, -Convert.ToInt32(text2));
                            SendNotificationWithoutButton(player, $"Du hast dir erfolgreich 15x Köder für {text2}$ erworben!", "success", "top-end", 2500);
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Boot ausleihen")
                        {
                            if (player.IsInVehicle)
                            {
                                SendNotificationWithoutButton(player, "Bitte steige erst aus deinem aktuellen Fahrzeug aus!", "error");
                                return;
                            }
                            if (character.cash < Convert.ToInt32(text2))
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {text2}$!", "error", "top-end");
                                return;
                            }
                            if (SetAndGetCharacterLicense(player, 0, 3) != "1")
                            {
                                SendNotificationWithoutButton(player, "Du besitzt keinen Bootsschein!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.rentVehicle == null)
                            {
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:PlaySoundSuccessNormal");
                                CharacterController.SetMoney(player, -Convert.ToInt32(text2));
                                SendNotificationWithoutButton(player, $"Du hast dir ein Boot für {text2}$ gemietet!", "success", "top-end", 3500);
                                Random rand2 = new Random();
                                tempData.rentVehicle = Cars.createNewCar("dinghy", new Vector3(-2195.1804, -478.90747, 0.5353528), -172.48631f, rand2.Next(0, 159), rand2.Next(0, 159), "LS-S-100" + player.Id, "Bootsverleih", true, true, false);
                                tempData.rentVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.rentVehicle, (int)VehicleSeat.Driver);
                                return;
                            }
                            else
                            {
                                if (tempData.rentVehicle.Class != 14)
                                {
                                    SendNotificationWithoutButton(player, "Du hast hier kein Boot gemietet!", "error");
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Du hast das Boot wieder zurück gegeben und erhältst 50$ zurück!", "success");
                                CharacterController.SetMoney(player, 50);
                                tempData.rentVehicle.Delete();
                                tempData.rentVehicle = null;
                                return;
                            }
                        }
                        else if (text1 == "Fisch verkaufen")
                        {
                            int price = 0;
                            player.TriggerEvent("Client:PressedEscape");
                            foreach (Items item in tempData.itemlist)
                            {
                                if (item.description == "Dorsch")
                                {
                                    price += (Helper.FishPrice) * item.amount;
                                }
                                else if (item.description == "Makrele")
                                {
                                    price += (Helper.FishPrice + 10) * item.amount;
                                }
                                else if (item.description == "Forelle")
                                {
                                    price += (Helper.FishPrice + 20) * item.amount;
                                }
                                else if (item.description == "Wildkarpfen")
                                {
                                    price += (Helper.FishPrice + 30) * item.amount;
                                }
                                else if (item.description == "Teufelskärpfling")
                                {
                                    price += (Helper.FishPrice + 45) * item.amount;
                                }
                            }
                            if (price <= 0)
                            {
                                SendNotificationWithoutButton(player, "Du hast keine Fische dabei!", "error");
                                return;
                            }
                            int skill = character.fishingskill / 35;
                            price = price + (price / 100 * (skill + 1));
                            tempData.tempValue = price;
                            NAPI.Task.Run(() =>
                            {
                                player.TriggerEvent("Client:CallInput2", "Fischverkauf", $"Ich biete dir für deine gefangenen Fische {price}$ an!", "SellFish", "Ja", "Nein");
                            }, delayTime: 600);
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Jägermenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 2)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Jäger!", "error", "top-end", 2250);
                                    return;
                                }
                                if (SetAndGetCharacterLicense(player, 5, 1337) != "1")
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Waffenschein!", "error", "top-end", 2250);
                                    return;
                                }
                                Items item = new Items();
                                item = ItemsController.GetItemByItemName(player, "Musket");
                                if (item == null)
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt kein Jagtgewehr - (Musket), im Ammunation kannst du dir eins kaufen!", "error", "top-end", 2250);
                                    return;
                                }
                                item = ItemsController.GetItemByItemName(player, "Messer");
                                if (item == null)
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt kein Messer, im Ammunation kannst du dir eins kaufen!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Fahrzeug ausleihen")
                        {
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                Random rand = new Random();
                                tempData.jobVehicle = Cars.createNewCar("bison", new Vector3(1101.1064, -262.4775, 68.767105), -31.50282f, rand.Next(0, 159), rand.Next(0, 159), "LS-S-155" + player.Id, "Jäger", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                    {
                                        tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Fleisch verkaufen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                            player.TriggerEvent("Client:PlayerFreeze", true);
                            int price = Helper.MeatPrice;
                            if (character.job == 2)
                            {
                                price += (price / 100 * 10);
                            }
                            NAPI.Task.Run(() =>
                            {
                                player.TriggerEvent("Client:ShowCursor");
                                player.TriggerEvent("Client:CallInput", "Fleisch verkaufen", $"Wieviel Stücke Fleisch möchtest du mir verkaufen? Ich gebe dir pro Stück: {price}$", "text", "1", 4, "SellMeat");
                            }, delayTime: 415);
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Routenauswahl")
                    {
                        if (text1 != "Abbrechen" && text1 != "Route beenden")
                        {
                            foreach (BusRoutes busRoutes in Helper.busRoutesList)
                            {
                                if (text1 == busRoutes.name)
                                {
                                    int skill = character.busskill / 35;
                                    if (skill < Convert.ToInt32(text2))
                                    {
                                        SendNotificationWithoutButton(player, "Du hast nicht den passenden Busskill!", "success", "top-end", 2500);
                                        return;
                                    }
                                    Events.busCount++;
                                    Events.busLabel.Text = $"~b~Busexperte JinJong\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren\nZurzeit sind {Events.busCount} Busfahrer auf einer Route unterwegs!";
                                    player.TriggerEvent("Client:PressedEscape");
                                    SendNotificationWithoutButton(player, $"Route {busRoutes.name} begonnen, fahre jetzt alle Haltestellen ab!", "success", "top-end", 3650);
                                    player.SetData<String>("Player:BusRoute", busRoutes.name);
                                    player.SetData<int>("Player:BusStation", 0);
                                    Helper.GetNextBusStation(player, busRoutes.name);
                                    player.SetData<int>("Player:BusTime", Helper.UnixTimestamp() + (60));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (text1 == "Route beenden")
                            {
                                if (!player.HasData("Player:BusRoute"))
                                {
                                    SendNotificationWithoutButton(player, $"Du fährst aktuell keine Route!", "error", "top-end", 2500);
                                    return;
                                }
                                if (player.GetData<int>("Player:BusStation") > 1)
                                {
                                    SendNotificationWithoutButton(player, $"Du kannst die Route jetzt nicht mehr beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (Events.busCount > 0)
                                {
                                    Events.busCount--;
                                }
                                Events.busLabel.Text = $"~b~Busexperte JinJong\n~w~Benutze Taste ~b~[F]~w~ um mit Ihm zu kummunizieren\nZurzeit sind {Events.busCount} Busfahrer auf einer Route unterwegs!";
                                player.ResetData("Player:BusRoute");
                                player.ResetData("Player:BusStation");
                                player.ResetData("Player:BusTime");
                                if (player.IsInVehicle)
                                {
                                    if (Helper.IsABusDriver(player) == 2)
                                    {
                                        player.Vehicle.SetSharedData("Vehicle:Text3D", "~w~Canny Bus Group - Dienstfahrt");
                                    }
                                    else
                                    {
                                        player.Vehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                }
                                SendNotificationWithoutButton(player, $"Route beendet!", "success", "top-end", 3250);
                                player.TriggerEvent("Client:RemoveBusDriverCP");
                            }
                            else
                            {
                                player.TriggerEvent("Client:PressedEscape");
                            }
                        }
                    }
                    else if (tempData.lastShop == "Müllroutenauswahl")
                    {
                        if (text1 != "Abbrechen" && text1 != "Route beenden")
                        {
                            foreach (GarbageRoutes garbageRoutes in Helper.garbageRoutesList)
                            {
                                if (garbageRoutes.name != "Garbage" && text1 == garbageRoutes.name)
                                {
                                    player.TriggerEvent("Client:PressedEscape");
                                    SendNotificationWithoutButton(player, $"Müllroute {garbageRoutes.name} begonnen, du findest die Mülltonnen rot auf der Karte als Müllwagen markiert!", "success", "top-end", 3650);
                                    player.SetData<String>("Player:GarbageRoute", garbageRoutes.name);
                                    player.SetData<int>("Player:GarbageStation", 0);
                                    Helper.GetNextGarbageStation(player, garbageRoutes.name, 1);
                                    player.SetData<int>("Player:GarbageTime", Helper.UnixTimestamp() + (20));
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (text1 == "Route beenden")
                            {
                                if (!player.HasData("Player:GarbageRoute"))
                                {
                                    SendNotificationWithoutButton(player, $"Du fährst aktuell keine Müllroute!", "error", "top-end", 2500);
                                    return;
                                }
                                player.ResetData("Player:GarbageRoute");
                                player.ResetData("Player:GarbageStation");
                                player.ResetData("Player:GarbageTime");
                                if (player.HasData("Player:GarbagePlayer2"))
                                {
                                    player.ResetData("Player:GarbagePlayer2");
                                }
                                SendNotificationWithoutButton(player, $"Müllroute beendet!", "success", "top-end", 3250);
                                player.TriggerEvent("Client:RemoveGarbageDriverCP");
                                if (player.HasData("Player:GarbageGetPlayer") && player.GetData<Player>("Player:GarbageGetPlayer") != null)
                                {
                                    Player garbagePlayer = player.GetData<Player>("Player:GarbageGetPlayer");
                                    garbagePlayer.ResetData("Player:GarbageRoute");
                                    if (garbagePlayer.HasData("Player:GarbagePlayer2"))
                                    {
                                        garbagePlayer.ResetData("Player:GarbagePlayer2");
                                    }
                                    garbagePlayer.ResetData("Player:GarbageStation");
                                    garbagePlayer.ResetData("Player:GarbageTime");
                                    SendNotificationWithoutButton(garbagePlayer, $"Müllroute beendet!", "success", "top-end", 3250);
                                    garbagePlayer.TriggerEvent("Client:RemoveGarbageDriverCP");
                                    player.ResetData("Player:GarbageGetPlayer");
                                    garbagePlayer.ResetData("Player:GarbageGetPlayer");
                                }
                            }
                            else
                            {
                                player.TriggerEvent("Client:PressedEscape");
                            }
                        }
                    }
                    else if (tempData.lastShop == "Ticketverkauf")
                    {
                        if (text1 != "Abbrechen")
                        {
                            int busPrice = Convert.ToInt32(text2);
                            if (character.cash < busPrice)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - {text2}$!", "error", "top-end");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, 50))
                            {
                                Helper.SendNotificationWithoutButton(player, "Du kannst das Busticket nichtmehr tragen!", "error", "top-end");
                                return;
                            }
                            Items newitem = null;
                            if (text1 == "Tagesticket kaufen")
                            {
                                newitem = ItemsController.CreateNewItem(player, character.id, "Busticket", "Player", 1, ItemsController.GetFreeItemID(player), "Gültig bis: " + Helper.UnixTimeStampToDateTime(Helper.UnixTimestamp() + (1 * 86400)).ToString());
                            }
                            else if (text1 == "Wochenticket kaufen")
                            {
                                newitem = ItemsController.CreateNewItem(player, character.id, "Busticket", "Player", 1, ItemsController.GetFreeItemID(player), "Gültig bis: " + Helper.UnixTimeStampToDateTime(Helper.UnixTimestamp() + (7 * 86400)).ToString());
                            }
                            else
                            {
                                newitem = ItemsController.CreateNewItem(player, character.id, "Busticket", "Player", 1, ItemsController.GetFreeItemID(player), "Gültig bis: " + Helper.UnixTimeStampToDateTime(Helper.UnixTimestamp() + (31 * 86400)).ToString());
                            }
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            Helper.SetGovMoney(busPrice, "Fahrkarten Verkauf");
                            CharacterController.SetMoney(player, -busPrice);
                            Helper.SendNotificationWithoutButton(player, $"Du hast dir erfolgreich ein Busticket für {busPrice}$ erworben!", "success", "top-left", 2750);
                            player.TriggerEvent("Client:PressedEscape");
                            if (player.IsInVehicle)
                            {
                                Player driver = (Player)NAPI.Vehicle.GetVehicleDriver(player.Vehicle);
                                if (driver != null)
                                {
                                    if (Helper.IsABusDriver(driver) == 1)
                                    {
                                        CharacterController.SetMoney(driver, busPrice);
                                        Helper.SendNotificationWithoutButton(driver, $"Ein Fahrgast hat ein Fahrticket erworben, du erhältst: {busPrice}$!", "success", "top-left", 2750);
                                    }
                                    else
                                    {
                                        CharacterController.SetMoney(driver, busPrice / 3);
                                        Helper.SendNotificationWithoutButton(driver, $"Ein Fahrgast hat ein Fahrticket erworben, du erhältst: {busPrice / 3}$!", "success", "top-left", 2750);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (player.IsInVehicle)
                            {
                                Helper.SendNotificationWithoutButton(player, "Wenn du kein Busticket hast, fährst du schwarz mit!", "info", "top-end");
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Taxifahrermenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 6)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Taxifahrer!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Taxi aus/einparken")
                        {
                            if (character.job != 6)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Taxifahrer!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnTaxi = new Vector3[4]
                                                   { new Vector3(919.3982, -163.7372, 74.39542-0.025),
                                                     new Vector3(915.14795, -170.98164, 73.98672-0.025),
                                                     new Vector3(911.51886, -163.59286, 73.96947-0.025),
                                                     new Vector3(913.99194, -159.90617, 74.38578-0.025)};

                                float[] spawnTaxiRot = new float[4]
                                                   { 98.437096f,
                                                     102.08468f,
                                                     -164.32187f,
                                                     -163.57915f};

                                Random rand = new Random();
                                int index = rand.Next(4);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("taxi", spawnTaxi[index], spawnTaxiRot[index], 42, 42, "LS-S-155" + player.Id, "Yellow Cab", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 6)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Taxifahrer!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:Fare"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Dienstfahrt beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Tourbus aus/einparken")
                        {
                            if (character.job != 6)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Taxifahrer!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("tourbus", new Vector3(901.55695, -187.04422, 73.6024), -31.475304f, 42, 42, "LS-S-155" + player.Id, "Yellow Cab", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 6)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Taxifahrer!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:Fare"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Dienstfahrt beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Kanalreinigermenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 5)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Kanalreiniger!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Dienstfahrzeug aus/einparken")
                        {
                            if (character.job != 5)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Kanalreiniger!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnUV = new Vector3[3]
                                                   { new Vector3(408.89767, -2065.556, 21.09941-0.025),
                                                     new Vector3(411.40387, -2067.3237, 21.122131-0.025),
                                                     new Vector3(414.18137, -2068.739, 21.159105-0.025)};

                                Random rand = new Random();
                                int index = rand.Next(3);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("utillitruck3", spawnUV[index], 140.16808f, 62, 62, "LS-S-155" + player.Id, "Kanalreiniger", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 5)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Kanalreiniger!", "error", "top-end", 2250);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Laufstilauswahl")
                    {
                        if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else
                        {
                            character.walkingstyle = text2;
                            player.TriggerEvent("Client:PressedEscape");
                            player.SetSharedData("Player:WalkingStyle", text2);
                            SendNotificationWithoutButton(player, "Neuer Laufstil wurde erfolgreich gesetzt!", "success", "top-end", 2500);
                        }
                    }
                    else if (tempData.lastShop == "Geldlieferantmenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 8)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Geldlieferant!", "error", "top-end", 2250);
                                    return;
                                }
                                Items item = new Items();
                                item = ItemsController.GetItemByItemName(player, "Schlagstock");
                                if (item == null)
                                {
                                    SendNotificationWithoutButton(player, "Du besitzt keinen Schlagstock, im Ammunation kannst du dir einen kaufen!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Stockade aus/einparken")
                        {
                            if (character.job != 8)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Geldlieferant!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnStockade = new Vector3[3]
                                                   { new Vector3(-1393.6237, -449.1239, 34.087337-0.05),
                                                     new Vector3(-1389.1426, -460.6535, 34.085274-0.05),
                                                     new Vector3(-1407.0458, -463.14377, 34.08722-0.05)};

                                float[] spawnStockadeRot = new float[3]
                                                   { -173.64134f,
                                                     98.12571f,
                                                     98.0261f};

                                Random rand = new Random();
                                int index = rand.Next(3);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("stockade", spawnStockade[index], spawnStockadeRot[index], 94, 94, "LS-S-155" + player.Id, "gruppe6", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 8)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Geldlieferant!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:Money") || tempData.furniturePosition != null)
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Geldlieferanten Aktion beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 15.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Landwirtmenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 7)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Busfahrer!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle2 != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch den Jobanhänger wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Traktor aus/einparken")
                        {
                            if (character.job != 7)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Landwirt!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnTractor = new Vector3[4]
                                                   { new Vector3(2241.0867, 5168.7407, 59.457405-0.05),
                                                     new Vector3(2235.522, 5162.2783, 58.389393-0.05),
                                                     new Vector3(2230.9385, 5179.8066, 60.316803-0.05),
                                                     new Vector3(2222.35, 5169.3154, 58.616646-0.05)};

                                float[] spawnTractorRot = new float[4]
                                                   { 99.44831f,
                                                     71.25042f,
                                                     157.50307f,
                                                     155.97096f};

                                Random rand = new Random();
                                int index = rand.Next(5);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("tractor2", spawnTractor[index], spawnTractorRot[index], 53, 53, "LS-S-155" + player.Id, "Landwirt", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 7)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Landwirt!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:FarmerRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Landwirtschafts Aktion beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle != null && Helper.IsTrailerAttached2(tempData.jobVehicle))
                                {
                                    SendNotificationWithoutButton(player, "Bring zuerst den Anhänger weg!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Anhänger aus/einparken")
                        {
                            if (character.job != 7)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Landwirt!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle2 == null)
                            {
                                SendNotificationWithoutButton(player, "Hier der Anhänger, bring mir den aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle2 = Cars.createNewCar("raketrailer", new Vector3(2237.197, 5135.213, 55.639217), 115.81946f, 53, 53, "LS-S-155" + player.Id, "Landwirt", true, true, false);
                                tempData.jobVehicle2.Dimension = 0;
                            }
                            else
                            {
                                if (character.job != 7)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Landwirt!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:FarmerRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Landwirtschafts Aktion beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle2.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Der Anhänger befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle2.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle2.ResetSharedData("Vehicle:Text3D");
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle2.Delete();
                                    tempData.jobVehicle2 = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Fahrrad aus/einparken")
                        {
                            if (character.job != 7)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Landwirt!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnTractor = new Vector3[4]
                                                   { new Vector3(2241.0867, 5168.7407, 59.457405-0.05),
                                                     new Vector3(2235.522, 5162.2783, 58.389393-0.05),
                                                     new Vector3(2230.9385, 5179.8066, 60.316803-0.05),
                                                     new Vector3(2222.35, 5169.3154, 58.616646-0.05)};

                                float[] spawnTractorRot = new float[4]
                                                   { 99.44831f,
                                                     71.25042f,
                                                     157.50307f,
                                                     155.97096f};

                                Random rand = new Random();
                                int index = rand.Next(5);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("cruiser", spawnTractor[index], spawnTractorRot[index], 53, 53, "LS-S-155" + player.Id, "Landwirt", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 7)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Landwirt!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:FarmerRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Landwirtschafts Aktion beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Busfahrermenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 3)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Busfahrer!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Normalen Bus aus/einparken")
                        {
                            if (character.job != 3)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Busfahrer!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnBus = new Vector3[5]
                                                   { new Vector3(439.16663, -573.94745, 28.49585-0.05),
                                                     new Vector3(443.6427, -576.5612, 28.49608-0.05),
                                                     new Vector3(449.52643, -581.38403, 28.497211-0.05),
                                                     new Vector3(453.4599, -586.8975, 28.496298-0.05),
                                                     new Vector3(449.85956, -593.60004, 28.500366-0.05)};

                                float[] spawnBusRot = new float[5]
                                                   { 148.26027f,
                                                     145.65208f,
                                                     126.441605f,
                                                     106.71059f,
                                                     84.46441f };

                                Random rand = new Random();
                                int index = rand.Next(5);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("bus", spawnBus[index], spawnBusRot[index], 41, 41, "LS-S-155" + player.Id, "Canny Bus", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                NAPI.Task.Run(() =>
                                {
                                    if (Helper.IsABusDriver(player) == 2)
                                    {
                                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~w~Canny Bus Group - Dienstfahrt");
                                    }
                                    else
                                    {
                                        tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                    player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                                }, delayTime: 95);
                                player.TriggerEvent("Client:HideMenus");
                            }
                            else
                            {
                                if (character.job != 3)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Busfahrer!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:BusRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Route beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 45.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Reisebus aus/einparken")
                        {
                            if (character.job != 3)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Busfahrer!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            int skill = character.busskill / 35;
                            if (skill < 3)
                            {
                                SendNotificationWithoutButton(player, "Du benötigst mind. Skill Profi um den Reisebus ausparken zu können!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnBus = new Vector3[5]
                                                   { new Vector3(439.16663, -573.94745, 28.49585-0.05),
                                                     new Vector3(443.6427, -576.5612, 28.49608-0.05),
                                                     new Vector3(449.52643, -581.38403, 28.497211-0.05),
                                                     new Vector3(453.4599, -586.8975, 28.496298-0.05),
                                                     new Vector3(449.85956, -593.60004, 28.500366-0.05)};

                                float[] spawnBusRot = new float[5]
                                                   { 148.26027f,
                                                     145.65208f,
                                                     126.441605f,
                                                     106.71059f,
                                                     84.46441f };

                                Random rand = new Random();
                                int index = rand.Next(5);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("coach", spawnBus[index], spawnBusRot[index], 41, 41, "LS-S-155" + player.Id, "Canny Bus", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                NAPI.Task.Run(() =>
                                {
                                    if (Helper.IsABusDriver(player) == 2)
                                    {
                                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~w~Canny Bus Group - Dienstfahrt");
                                    }
                                    else
                                    {
                                        tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                    player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                                }, delayTime: 95);
                                player.TriggerEvent("Client:HideMenus");
                            }
                            else
                            {
                                if (player.HasData("Player:BusRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Route beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 45.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Rentalbus aus/einparken")
                        {
                            if (character.job != 3)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Busfahrer!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            int skill = character.busskill / 35;
                            if (skill < 5)
                            {
                                SendNotificationWithoutButton(player, "Du benötigst mind. Skill Experte um den Reisebus ausparken zu können!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnBus = new Vector3[5]
                                                   { new Vector3(439.16663, -573.94745, 28.49585-0.05),
                                                     new Vector3(443.6427, -576.5612, 28.49608-0.05),
                                                     new Vector3(449.52643, -581.38403, 28.497211-0.05),
                                                     new Vector3(453.4599, -586.8975, 28.496298-0.05),
                                                     new Vector3(449.85956, -593.60004, 28.500366-0.05)};

                                float[] spawnBusRot = new float[5]
                                                   { 148.26027f,
                                                     145.65208f,
                                                     126.441605f,
                                                     106.71059f,
                                                     84.46441f };

                                Random rand = new Random();
                                int index = rand.Next(5);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("rentalbus", spawnBus[index], spawnBusRot[index], 41, 41, "LS-S-155" + player.Id, "Canny Bus", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                                NAPI.Task.Run(() =>
                                {
                                    if (Helper.IsABusDriver(player) == 2)
                                    {
                                        tempData.jobVehicle.SetSharedData("Vehicle:Text3D", "~w~Canny Bus Group - Dienstfahrt");
                                    }
                                    else
                                    {
                                        tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                    player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                                }, delayTime: 95);
                                player.TriggerEvent("Client:HideMenus");
                            }
                            else
                            {
                                if (player.HasData("Player:BusRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Route beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 45.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                {
                                    tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Müllmannmenü")
                    {
                        if (text1 == "Jobdienst beginnen/beenden")
                        {
                            if (tempData.jobduty == false)
                            {
                                if (character.job != 4)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Müllmann!", "error", "top-end", 2250);
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Dienst begonnen, mit der F2 Taste kannst du die Jobhilfe öffnen!", "success", "top-end", 2250);
                                tempData.jobduty = true;
                            }
                            else
                            {
                                if (tempData.jobVehicle != null)
                                {
                                    SendNotificationWithoutButton(player, "Bring uns zuerst noch das Jobfahrzeug wieder zurück!", "error", "top-end", 2500);
                                    return;
                                }
                                player.TriggerEvent("Client:RemoveWaypoint");
                                player.TriggerEvent("Client:RemoveWaypoint2");
                                SendNotificationWithoutButton(player, "Dienst beendet!", "success", "top-end", 1500);
                                tempData.jobduty = false;
                            }
                        }
                        else if (text1 == "Müllwagen aus/einparken")
                        {
                            if (character.job != 4)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Müllmann!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnGarbage = new Vector3[3]
                                                   { new Vector3(-330.6473, -1527.8435, 27.248093-0.05),
                                                     new Vector3(-324.38956, -1528.0874, 27.261396-0.05),
                                                     new Vector3(-325.9845, -1518.731, 27.25384-0.05)};

                                float[] spawnGarbageRot = new float[3]
                                                   { -0.0058504213f,
                                                     0.25526208f,
                                                     -179.68666f };

                                Random rand = new Random();
                                int index = rand.Next(3);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("trash", spawnGarbage[index], spawnGarbageRot[index], 0, 0, "LS-S-155" + player.Id, "Müllwagen", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                tempData.jobVehicle.Locked = false;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 4)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Müllmann!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:GarbageRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Route beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 75.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                    {
                                        tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen, wir leeren noch schnell den Müllwagen/Sweeper!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Sweeper aus/einparken")
                        {
                            if (character.job != 4)
                            {
                                SendNotificationWithoutButton(player, "Du bist kein Müllmann!", "error", "top-end", 2250);
                                return;
                            }
                            if (tempData.jobduty == false)
                            {
                                SendNotificationWithoutButton(player, "Stempel erstmal vorne ein, du willst doch nicht für lau arbeiten!", "error", "top-end", 2500);
                                return;
                            }
                            if (tempData.jobVehicle == null)
                            {
                                Vector3[] spawnGarbage = new Vector3[3]
                                                   { new Vector3(-330.6473, -1527.8435, 27.248093-0.05),
                                                     new Vector3(-324.38956, -1528.0874, 27.261396-0.05),
                                                     new Vector3(-325.9845, -1518.731, 27.25384-0.05)};

                                float[] spawnGarbageRot = new float[3]
                                                   { -0.0058504213f,
                                                     0.25526208f,
                                                     -179.68666f };

                                Random rand = new Random();
                                int index = rand.Next(3);
                                SendNotificationWithoutButton(player, "Hier das Fahrzeug, bring mir das aber wieder im Ganzen zurück!", "success", "top-end", 2500);
                                tempData.jobVehicle = Cars.createNewCar("sweeper", spawnGarbage[index], spawnGarbageRot[index], 0, 0, "LS-S-155" + player.Id, "Müllwagen", true, true, false);
                                tempData.jobVehicle.Dimension = 0;
                                tempData.jobVehicle.Locked = false;
                                player.SetIntoVehicle(tempData.jobVehicle, (int)VehicleSeat.Driver);
                            }
                            else
                            {
                                if (character.job != 4)
                                {
                                    SendNotificationWithoutButton(player, "Du bist kein Müllmann!", "error", "top-end", 2250);
                                    return;
                                }
                                if (player.HasData("Player:GarbageRoute"))
                                {
                                    SendNotificationWithoutButton(player, "Du musst zuerst deine Route beenden!", "error", "top-end", 2500);
                                    return;
                                }
                                if (tempData.jobVehicle.Position.DistanceTo(player.Position) > 12.5)
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug befindet sich nicht in deiner Nähe!", "error", "top-end", 2500);
                                    return;
                                }
                                if (player.IsInVehicle)
                                {
                                    player.WarpOutOfVehicle();
                                }
                                NAPI.Task.Run(() =>
                                {
                                    if (tempData.jobVehicle.HasSharedData("Vehicle:Text3D"))
                                    {
                                        tempData.jobVehicle.ResetSharedData("Vehicle:Text3D");
                                    }
                                    tempData.jobVehicle.Delete();
                                    tempData.jobVehicle = null;
                                }, delayTime: 215);
                                SendNotificationWithoutButton(player, "Danke fürs zurück bringen, wir leeren noch schnell den Müllwagen/Sweeper!", "success", "top-end", 2500);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Mechatronikermenü")
                    {
                        Vehicle vehicle = Helper.GetClosestVehicle(player, 6.55f);
                        int vclass = vehicle.Class;
                        if (vehicle == null && text1 != "Abbrechen") return;
                        if (text1 == "Fahrzeug auf/abbocken")
                        {
                            if (vehicle.HasData("Vehicle:Jacked") && vehicle.GetData<bool>("Vehicle:Jacked") == true)
                            {
                                player.TriggerEvent("Client:PressedEscape");
                                NAPI.Task.Run(() =>
                                {
                                    player.TriggerEvent("Client:StartLockpicking", 10, "mecha2", "Fahrzeug wird abgebockt...");
                                }, delayTime: 415);
                            }
                            else
                            {
                                if (vclass == 8 || vclass == 9 || vclass == 13 || vclass == 10 || vclass == 11 || vclass == 12 || vclass == 14 || vclass == 15 || vclass == 16 || vclass == 17 || vclass == 18 || vclass == 19 || vclass == 20 || vclass == 21 || vehicle.GetSharedData<String>("Vehicle:Name").Contains("nrg500"))
                                {
                                    SendNotificationWithoutButton(player, $"Dieses Fahrzeug kann nicht aufgebockt werden!", "error");
                                    return;
                                }
                                player.TriggerEvent("Client:PressedEscape");
                                NAPI.Task.Run(() =>
                                {
                                    player.TriggerEvent("Client:StartLockpicking", 14, "mecha1", "Fahrzeug wird aufgebockt...");
                                }, delayTime: 415);
                            }
                        }
                        else if (text1 == "Fahrzeugdiagnose")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            player.TriggerEvent("Client:StartLockpicking", 8, "mecha3", "Fahrzeugdiagnose wird durchgeführt...");
                        }
                        else if (text1 == "Fahrzeug reparieren")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (text2 == "0")
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss nicht repariert werden!", "error");
                                return;
                            }
                            int time = (int)NAPI.Vehicle.GetVehicleHealth(vehicle) / 50;
                            if (time < 5)
                            {
                                time = 5;
                            }
                            if (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[5] == "1")
                            {
                                time = 20;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", time, "mecha4", "Fahrzeug wird repariert...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Reifen reparieren")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (player.Vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[3] == "0")
                            {
                                SendNotificationWithoutButton(player, $"Die Reifen müssen nicht repariert werden!", "error");
                                return;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 9, "mecha6", "Reifen werden repariert...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Tank auspumpen")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (vehicle.GetSharedData<float>("Vehicle:Fuel") <= 0)
                            {
                                SendNotificationWithoutButton(player, $"Der Tank ist leer!", "error");
                                return;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 8, "mecha5", "Tank wird ausgepumpt...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Oel nachfüllen")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (vehicle.GetSharedData<int>("Vehicle:Oel") >= 75)
                            {
                                SendNotificationWithoutButton(player, "Das ÖL muss noch nicht nachgefüllt werden!", "error");
                                return;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 11, "mecha7", "ÖL wird nachgefüllt...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Batterie austauschen")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (vehicle.GetSharedData<int>("Vehicle:Battery") >= 75)
                            {
                                SendNotificationWithoutButton(player, $"Die Batterie muss noch nicht ausgetauscht werden!", "error");
                                return;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 11, "mecha8", "Batterie wird ausgetauscht...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Schloss verbessern")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (vehicle.HasSharedData("Vehicle:VLock") && vehicle.GetSharedData<int>("Vehicle:VLock") > 0)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug hat schon ein verbessertes Schloss!", "error");
                                return;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 11, "mecha10", "Schloss wird verbessert...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Schloss austauschen")
                        {
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 12, "mecha11", "Schloss wird ausgetauscht...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "TÜV Plakette aufkleben")
                        {
                            if (vclass == 13)
                            {
                                Helper.SendNotificationWithoutButton(player, $"Fahrräder brauchen kein TÜV!", "error", "top-left", 2500);
                                player.TriggerEvent("Client:ShowCursor");
                                return;
                            }
                            if (!vehicle.HasData("Vehicle:Jacked") && vclass != 8 && vclass != 9 && vclass != 13 && vclass != 10 && vclass != 11 && vclass != 12 && vclass != 14 && vclass != 15 && vclass != 16 && vclass != 17 && vclass != 18 && vclass != 19 && vclass != 20 && vclass != 21)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug muss erst aufgebockt werden!", "error");
                                return;
                            }
                            if (vehicle.NumberPlate.Length <= 3)
                            {
                                SendNotificationWithoutButton(player, $"Das Fahrzeug wurde noch nicht angemeldet!", "error");
                                return;
                            }

                            House house = null;
                            Groups group = GroupsController.GetGroupById(character.mygroup);
                            if (group != null || character.faction == 3)
                            {
                                if (character.faction == 3 && character.factionduty == true)
                                {
                                    house = House.GetHouseById(2);
                                }
                                else
                                {
                                    house = House.GetHouseByGroupId(group.id);
                                }
                                if (house == null)
                                {
                                    SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                                    return;
                                }
                                if (house.stock < Convert.ToInt32(text2))
                                {
                                    SendNotificationWithoutButton(player, $"Soviele Utensilien sind nichtmehr verfügbar!", "error");
                                    return;
                                }
                                tempData.tempValue = Convert.ToInt32(text2);
                                player.TriggerEvent("Client:PressedEscape");
                                player.TriggerEvent("Client:StartLockpicking", 7, "mecha9", "TÜV Plakette wird angeklebt...");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Ungültige Interaktion!", "error");
                            }
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Gutschein erstellen")
                    {
                        if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else
                        {
                            string coupon = Helper.CreateCoupon(text1, tempData.tempValue);
                            if (coupon.Length != 8)
                            {
                                Helper.SendNotificationWithoutButton(player, "Der Gutschein konnte nicht erstellt werden!", "error", "top-end");
                                return;
                            }
                            CreateAdminLog("gutscheinlog", $"{account.name} hat einen {text1} Gutschein ({coupon}) mit {tempData.tempValue} Anwendungen erstellt!");
                            SendNotificationWithoutButton(player, $"Gutschein erstellt: {coupon} - (Zwischenablage)!", "info", "top-left", 6250);
                            player.TriggerEvent("Client:CopyToClipboard", coupon);
                        }
                    }
                    else if (tempData.lastShop == "LSRC Dach")
                    {
                        if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else
                        {
                            if (text1 == "Keller")
                            {
                                SetPlayerPosition(player, new Vector3(-664.0335, 328.23676, 78.12267));
                                player.Heading = -9.229935f;
                            }
                            else if (text1 == "Ebene 1")
                            {
                                SetPlayerPosition(player, new Vector3(-664.159, 328.23718, 83.08322));
                                player.Heading = -8.3361225f;
                            }
                            else if (text1 == "Ebene 2")
                            {
                                SetPlayerPosition(player, new Vector3(-664.0749, 328.1962, 88.01673));
                                player.Heading = -11.017559f;
                            }
                            else if (text1 == "Ebene 3")
                            {
                                SetPlayerPosition(player, new Vector3(-664.0938, 328.36017, 92.7444));
                                player.Heading = -8.972109f;
                            }
                            else if (text1 == "Dach")
                            {
                                SetPlayerPosition(player, new Vector3(-664.07245, 328.3244, 140.12306));
                                player.Heading = -7.3391743f;
                            }
                            player.TriggerEvent("Client:PlaySoundPeep");
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Animationsmenü")
                    {
                        if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                        else
                        {
                            var anims = String.Join(",", Helper.animList.Where(x => x.category.ToLower() == text1.ToLower()).OrderBy(x => x.name).Select(x => x.name).ToArray());
                            text1 = "";
                            text2 = "";
                            text1 += $"{anims},Zurück";
                            text2 = "";
                            NAPI.Task.Run(() =>
                            {
                                tempData.lastShop = "Animationsauswahl";
                                player.TriggerEvent("Client:ShowShop2", text1, text2, "Animationsauswahl", 1, 0, 0, true);
                            }, delayTime: 355);
                        }
                    }
                    else if (tempData.lastShop == "Animationsauswahl")
                    {
                        if (text1 == "Zurück")
                        {
                            NAPI.Task.Run(() =>
                            {
                                OnPlayerPressF7(player, true);
                            }, delayTime: 115);
                        }
                        else
                        {
                            player.TriggerEvent("Client:PressedEscape");
                            Commands.cmd_animation(player, text1);
                        }
                    }
                    else if (tempData.lastShop == "Police-Department")
                    {
                        if (text1 == "Führungszeugnis beantragen")
                        {
                            if (character.cash <= 525)
                            {
                                Helper.SendNotificationWithoutButton(player, "Du hast nicht genügend Geld dabei - 525$!", "error", "top-end");
                                player.TriggerEvent("Client:ShowCursor");
                                return;
                            }
                            if (!ItemsController.CanPlayerHoldItem(player, (ItemsController.GetItemWeight("F-Zeugnis") * 1)))
                            {
                                Helper.SendNotificationWithoutButton(player, "Du kannst das Item nichtmehr tragen!", "error", "top-end");
                                player.TriggerEvent("Client:ShowCursor");
                                return;
                            }
                            CharacterController.SetMoney(player, -525);
                            Items newitem = ItemsController.CreateNewItem(player, character.id, "F-Zeugnis", "Player", 1, ItemsController.GetFreeItemID(player), character.name);
                            if (newitem != null)
                            {
                                tempData.itemlist.Add(newitem);
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            Helper.SetGovMoney(525, "Führungszeugnis ausgestellt");
                            Helper.SendNotificationWithoutButton(player, $"Hier Ihr Führungszeugnis!", "success", "center", 4250);
                            return;
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Fahrschule")
                    {
                        if (text1 == "Führerscheinprüfung")
                        {
                            if (GetAge(DateTime.ParseExact(character.birth, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)) < 18)
                            {
                                SendNotificationWithoutButton(player, $"Du bist zu jung für einen Führerschein!", "error", "top-end");
                                return;
                            }
                            if (tempData.jobVehicle != null)
                            {
                                SendNotificationWithoutButton(player, "Du musst zuerst deinen Jobbdienst beenden!", "error", "top-end", 2250);
                                return;
                            }
                            if (SetAndGetCharacterLicense(player, 0, 1337) == "1")
                            {
                                SendNotificationWithoutButton(player, "Du besitzt bereits einen Führerschein!", "error", "top-end", 2250);
                                return;
                            }
                            if (Convert.ToInt32(SetAndGetCharacterLicense(player, 0, 1337)) > 1 && Helper.UnixTimestamp() < Convert.ToInt32(SetAndGetCharacterLicense(player, 0, 1337)))
                            {
                                SendNotificationWithoutButton(player, $"Du hast noch eine Führerscheinsperre, bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(SetAndGetCharacterLicense(player, 1, 1337)))} Uhr!", "error", "top-end", 2250);
                                return;
                            }
                            if (character.cash < 1500)
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 1500$!", "error", "top-end", 2250);
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            NAPI.Task.Run(() =>
                            {
                                CharacterController.SetMoney(player, -1500);
                                bizz = Business.GetBusinessById(43);
                                if (bizz != null)
                                {
                                    Business.ManageBizzCash(bizz, 1500 / 3, true);
                                }
                                player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                                player.TriggerEvent("Client:ShowHud");
                                player.TriggerEvent("Client:ShowCarQuiz", 1);
                            }, delayTime: 355);
                        }
                        else if (text1 == "Motorradscheinprüfung")
                        {
                            if (GetAge(DateTime.ParseExact(character.birth, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)) < 18)
                            {
                                SendNotificationWithoutButton(player, $"Du bist zu jung für einen Motorradschein!", "error", "top-end");
                                return;
                            }
                            if (tempData.jobVehicle != null)
                            {
                                SendNotificationWithoutButton(player, "Du musst zuerst deinen Jobbdienst beenden!", "error", "top-end", 2250);
                                return;
                            }
                            if (SetAndGetCharacterLicense(player, 1, 1337) == "1")
                            {
                                SendNotificationWithoutButton(player, "Du besitzt bereits einen Motorradschein!", "error", "top-end", 2250);
                                return;
                            }
                            if (Convert.ToInt32(SetAndGetCharacterLicense(player, 1, 1337)) > 1 && Helper.UnixTimestamp() < Convert.ToInt32(SetAndGetCharacterLicense(player, 1, 1337)))
                            {
                                SendNotificationWithoutButton(player, $"Du hast noch eine Motorradscheinsperre, bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(SetAndGetCharacterLicense(player, 2, 1337)))} Uhr!", "error", "top-end", 2250);
                                return;
                            }
                            if (character.cash < 2250)
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 2250$!", "error", "top-end", 2250);
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            NAPI.Task.Run(() =>
                            {
                                CharacterController.SetMoney(player, -2500);
                                bizz = Business.GetBusinessById(43);
                                if (bizz != null)
                                {
                                    Business.ManageBizzCash(bizz, 2500 / 3, true);
                                }
                                player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                                player.TriggerEvent("Client:ShowHud");
                                player.TriggerEvent("Client:ShowCarQuiz", 2);
                            }, delayTime: 355);
                        }
                        else if (text1 == "Truckerscheinprüfung")
                        {
                            if (GetAge(DateTime.ParseExact(character.birth, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)) < 18)
                            {
                                SendNotificationWithoutButton(player, $"Du bist zu jung für einen Truckerschein!", "error", "top-end");
                                return;
                            }
                            if (tempData.jobVehicle != null)
                            {
                                SendNotificationWithoutButton(player, "Du musst zuerst deinen Jobbdienst beenden!", "error", "top-end", 2250);
                                return;
                            }
                            if (SetAndGetCharacterLicense(player, 2, 1337) == "1")
                            {
                                SendNotificationWithoutButton(player, "Du besitzt bereits einen Truckerschein!", "error", "top-end", 2250);
                                return;
                            }
                            if (Convert.ToInt32(SetAndGetCharacterLicense(player, 2, 1337)) > 1 && Helper.UnixTimestamp() < Convert.ToInt32(SetAndGetCharacterLicense(player, 2, 1337)))
                            {
                                SendNotificationWithoutButton(player, $"Du hast noch eine Truckerscheinsperre, bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(SetAndGetCharacterLicense(player, 3, 1337)))} Uhr!", "error", "top-end", 2250);
                                return;
                            }
                            if (character.cash < 3500)
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 3500$!", "error", "top-end", 2250);
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            NAPI.Task.Run(() =>
                            {
                                CharacterController.SetMoney(player, -4750);
                                bizz = Business.GetBusinessById(43);
                                if (bizz != null)
                                {
                                    Business.ManageBizzCash(bizz, 4750 / 3, true);
                                }
                                player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                                player.TriggerEvent("Client:ShowHud");
                                player.TriggerEvent("Client:ShowCarQuiz", 3);
                            }, delayTime: 355);
                        }
                        else if (text1 == "Bootsscheinprüfung")
                        {
                            if (GetAge(DateTime.ParseExact(character.birth, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)) < 18)
                            {
                                SendNotificationWithoutButton(player, $"Du bist zu jung für einen Bootsschein!", "error", "top-end");
                                return;
                            }
                            if (tempData.jobVehicle != null)
                            {
                                SendNotificationWithoutButton(player, "Du musst zuerst deinen Jobbdienst beenden!", "error", "top-end", 2250);
                                return;
                            }
                            if (SetAndGetCharacterLicense(player, 3, 1337) == "1")
                            {
                                SendNotificationWithoutButton(player, "Du besitzt bereits einen Bootsschein!", "error", "top-end", 2250);
                                return;
                            }
                            if (Convert.ToInt32(SetAndGetCharacterLicense(player, 3, 1337)) > 1 && Helper.UnixTimestamp() < Convert.ToInt32(SetAndGetCharacterLicense(player, 3, 1337)))
                            {
                                SendNotificationWithoutButton(player, $"Du hast noch eine Bootsscheinsperre, bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(SetAndGetCharacterLicense(player, 4, 1337)))} Uhr!", "error", "top-end", 2250);
                                return;
                            }
                            if (character.cash < 5000)
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 5000$!", "error", "top-end", 2250);
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            NAPI.Task.Run(() =>
                            {
                                CharacterController.SetMoney(player, -7500);
                                bizz = Business.GetBusinessById(43);
                                if (bizz != null)
                                {
                                    Business.ManageBizzCash(bizz, 7500 / 3, true);
                                }
                                player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                                player.TriggerEvent("Client:ShowHud");
                                player.TriggerEvent("Client:ShowCarQuiz", 4);
                            }, delayTime: 355);
                        }
                        else if (text1 == "Flugscheinprüfung")
                        {
                            if (GetAge(DateTime.ParseExact(character.birth, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)) < 18)
                            {
                                SendNotificationWithoutButton(player, $"Du bist zu jung für einen Flugschein!", "error", "top-end");
                                return;
                            }
                            if (tempData.jobVehicle != null)
                            {
                                SendNotificationWithoutButton(player, "Du musst zuerst deinen Jobbdienst beenden!", "error", "top-end", 2250);
                                return;
                            }
                            if (SetAndGetCharacterLicense(player, 4, 1337) == "1")
                            {
                                SendNotificationWithoutButton(player, "Du besitzt bereits einen Flugschein!", "error", "top-end", 2250);
                                return;
                            }
                            if (Convert.ToInt32(SetAndGetCharacterLicense(player, 4, 1337)) > 1 && Helper.UnixTimestamp() < Convert.ToInt32(SetAndGetCharacterLicense(player, 4, 1337)))
                            {
                                SendNotificationWithoutButton(player, $"Du hast noch eine Flugscheinsperre, bis zum {Helper.UnixTimeStampToDateTime(Convert.ToInt32(SetAndGetCharacterLicense(player, 4, 1337)))} Uhr!", "error", "top-end", 2250);
                                return;
                            }
                            if (character.cash < 7500)
                            {
                                SendNotificationWithoutButton(player, $"Du hast nicht genügend Geld dabei - 7500$!", "error", "top-end", 2250);
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            NAPI.Task.Run(() =>
                            {
                                CharacterController.SetMoney(player, -10500);
                                bizz = Business.GetBusinessById(43);
                                if (bizz != null)
                                {
                                    Business.ManageBizzCash(bizz, 10500 / 3, true);
                                }
                                player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                                player.TriggerEvent("Client:ShowHud");
                                player.TriggerEvent("Client:ShowCarQuiz", 5);
                            }, delayTime: 355);
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Waffenkammer LSPD")
                    {
                        if (text1 == "Waffenkammer")
                        {
                            ShowWaffenkammer(player, character.faction, 0, 0);
                        }
                        else if (text1 == "Waffenkomponenten")
                        {
                            if (NAPI.Player.GetPlayerCurrentWeapon(player) == (WeaponHash)WeaponController.GetWeaponHashFromName("faust"))
                            {
                                SendNotificationWithoutButton(player, $"Du musst zuerst eine Waffe ausrüsten!", "error");
                                return;
                            }
                            Items item = ItemsController.GetItemFromWeaponHash(player, NAPI.Player.GetPlayerCurrentWeapon(player));
                            if (item == null)
                            {
                                SendNotificationWithoutButton(player, $"Du musst zuerst eine Waffe ausrüsten!", "error");
                                return;
                            }
                            if (WeaponController.GetWeaponClass(item.description) == "2" && NAPI.Player.GetPlayerCurrentWeapon(player) != (WeaponHash)WeaponController.GetWeaponHashFromName("taschenlampe"))
                            {
                                SendNotificationWithoutButton(player, $"Für diese Waffe führen wir keine zusätzlichen Komponenten!", "error");
                                return;
                            }
                            player.Heading = player.Heading + 180;
                            player.TriggerEvent("Client:ShowAmmunationMenu", NAPI.Util.ToJson(item), 1250, 1950, Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)), 1);
                        }
                        else if (text1 == "Schiessstand benutzen")
                        {
                            if (player.GetData<int>("Player:AmmuQuiz") == 4)
                            {
                                player.TriggerEvent("Client:StopRange");
                                player.SetData<int>("Player:AmmuQuiz", 0);
                                player.ResetData("Player:AmmuQuiz");
                                SendNotificationWithoutButton(player, $"Schussübung beendet!", "succhess");
                                return;
                            }
                            if (NAPI.Player.GetPlayerCurrentWeapon(player) == (WeaponHash)WeaponController.GetWeaponHashFromName("faust"))
                            {
                                SendNotificationWithoutButton(player, $"Du musst zuerst eine Waffe ausrüsten!", "error");
                                return;
                            }
                            Items item = ItemsController.GetItemFromWeaponHash(player, NAPI.Player.GetPlayerCurrentWeapon(player));
                            if (item == null)
                            {
                                SendNotificationWithoutButton(player, $"Du musst zuerst eine Waffe ausrüsten!", "error");
                                return;
                            }
                            if (NAPI.Player.GetPlayerCurrentWeaponAmmo(player) < 45)
                            {
                                SendNotificationWithoutButton(player, "Du benötigst mind. 45 Schuss für die ausgerüstete Waffe!", "error", "top-end", 2250);
                                return;
                            }
                            player.TriggerEvent("Client:PressedEscape");
                            player.SetData<int>("Player:AmmuQuiz", 4);
                            player.TriggerEvent("Client:StartRange", 45, 3, 0);
                            SendNotificationWithoutButton(player, "Versuche die Ziele (Mittlerer roter Punkt) so schnell wie möglich zu treffen, benutze /abort zum abbrechen!", "success", "top-end", 4250);
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Lager LSRC")
                    {
                        if (text1 == "Lager")
                        {
                            ShowWaffenkammer(player, character.faction, 0, 0);
                        }
                        if (text1 == "Feuerlöscher auffüllen")
                        {
                            if (NAPI.Player.GetPlayerCurrentWeapon(player) == WeaponHash.Fireextinguisher)
                            {
                                if (NAPI.Player.GetPlayerWeaponAmmo(player, WeaponHash.Fireextinguisher) >= 2000)
                                {
                                    SendNotificationWithoutButton(player, $"Der Feuerlöscher ist schon voll!", "error");
                                    return;
                                }
                                NAPI.Player.SetPlayerWeaponAmmo(player, WeaponHash.Fireextinguisher, 2000);
                                SendNotificationWithoutButton(player, $"Der Feuerlöscher wurde aufgefüllt!", "success");
                                return;
                            }
                            SendNotificationWithoutButton(player, $"Du hast keinen Feuerlöscher in der Hand!", "error");
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Lager ACLS")
                    {
                        if (text1 == "Lager")
                        {
                            ShowWaffenkammer(player, character.faction, 0, 0);
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else if (tempData.lastShop == "Ammunation")
                    {
                        if (text1 == "Waffen kaufen")
                        {
                            ShowAmmunationMenu(player, 0, "Ammunation1");
                        }
                        else if (text1 == "Munition kaufen")
                        {
                            player.Heading = player.Heading - 180;
                            ShowAmmunationMenu(player, 0, "Ammunation2");
                        }
                        else if (text1 == "Waffenkomponenten kaufen")
                        {
                            if (NAPI.Player.GetPlayerCurrentWeapon(player) == (WeaponHash)WeaponController.GetWeaponHashFromName("faust"))
                            {
                                SendNotificationWithoutButton(player, $"Du musst zuerst eine Waffe ausrüsten!", "error");
                                return;
                            }
                            Items item = ItemsController.GetItemFromWeaponHash(player, NAPI.Player.GetPlayerCurrentWeapon(player));
                            if (item == null)
                            {
                                SendNotificationWithoutButton(player, $"Du musst zuerst eine Waffe ausrüsten!", "error");
                                return;
                            }
                            if (WeaponController.GetWeaponClass(item.description) == "2" && NAPI.Player.GetPlayerCurrentWeapon(player) != (WeaponHash)WeaponController.GetWeaponHashFromName("taschenlampe"))
                            {
                                SendNotificationWithoutButton(player, $"Für diese Waffe führen wir keine zusätzlichen Komponenten!", "error");
                                return;
                            }
                            foreach (ShopItems shopItem in shopItemList.ToList().OrderBy(x => x.itemprice))
                            {
                                if (shopItem.shoplabel == "Ammunation1")
                                {
                                    if (shopItem.itemname.ToLower() == item.description.ToLower() || account.adminlevel >= (int)Account.AdminRanks.Manager)
                                    {
                                        player.SetData<bool>("Player:Ammunation", true);
                                        player.Heading = player.Heading + 180;
                                        player.TriggerEvent("Client:ShowAmmunationMenu", NAPI.Util.ToJson(item), Convert.ToInt32(1250 * bizz.multiplier), Convert.ToInt32(1950 * bizz.multiplier), Convert.ToString(NAPI.Player.GetPlayerCurrentWeapon(player)), 0);
                                        return;
                                    }
                                }
                            }
                            SendNotificationWithoutButton(player, $"Für diese Waffe führen wir keine zusätzlichen Komponenten!", "error");
                            return;
                        }
                        else if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:PressedEscape");
                        }
                    }
                    else
                    {
                        if (text1 == "Abbrechen")
                        {
                            player.TriggerEvent("Client:ShowStadthalle");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnBuyShopItem2]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:StartLockpicking")]
        public static void OnStartLockpicking(Player player, String action)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                if (character != null)
                {
                    if (action == "vehicle")
                    {
                        Items item = ItemsController.GetItemByItemName(player, "Dietrich");
                        if (item != null)
                        {
                            item.amount--;
                            if (item.amount <= 0)
                            {
                                ItemsController.RemoveItem(player, item.itemid);
                            }
                        }
                    }
                    if (player.Vehicle == null && action != "milking" && action != "shoveling" && action != "cleaning" && action != "welding" && action != "reanim" && action != "check" && action != "crafting")
                    {
                        if (action == "fishing")
                        {
                            Helper.AddRemoveAttachments(player, "fishingRod", true);
                            player.SetSharedData("Player:AnimData", $"amb@world_human_stand_fishing@base%base%{1}");
                        }
                        else
                        {
                            player.SetSharedData("Player:AnimData", $"anim@amb@clubhouse@tutorial@bkr_tut_ig3@%machinic_loop_mechandplayer%{1}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnStartLockpicking]: " + e.ToString());
            }
        }


        [RemoteEvent("Server:FinishProgress")]
        public static void OnFinishProgress(Player player, string action)
        {
            try
            {
                Character character = Helper.GetCharacterData(player);
                TempData tempData = Helper.GetCharacterTempData(player);
                if (character != null && tempData != null)
                {
                    string[] vehicleArray = new string[7];
                    if (!player.IsInVehicle)
                    {
                        OnStopAnimation2(player);
                    }
                    else
                    {
                        player.TriggerEvent("Client:PlayerFreeze", false);
                    }
                    if (player.IsInVehicle && player.VehicleSeat == (int)VehicleSeat.Driver && player.Health > 0)
                    {
                        player.TriggerEvent("Client:ShowSpeedometer");
                    }

                    Vehicle vehicle = null;
                    vehicle = Helper.GetClosestVehicle(player, 6.55f);

                    if (action.Contains("mecha") && vehicle != null)
                    {
                        vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                    }

                    if (action == "failed" && !action.Contains("mecha"))
                    {
                        SendNotificationWithoutButton(player, "Interaktion abgebrochen!", "error", "top-left", 3500);
                        if (action.Contains("shoveling"))
                        {
                            tempData.inrob = false;
                        }
                    }
                    else if (action == "searchdeath")
                    {
                        Player getPlayer = player.GetData<Player>("Player:NearestPlayer");
                        player.ResetData("Player:NearestPlayer");
                        if (getPlayer != null && getPlayer.Exists)
                        {
                            TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                            if (tempData2 != null)
                            {
                                if (tempData2.killed != "")
                                {
                                    if (tempData2.killed.Length == 12)
                                    {
                                        if (GetRandomPercentage(60))
                                        {
                                            SendNotificationWithoutButton(player, $"Du hast eine Patronenhülse mit der Waffenidentifikationsnummer: {tempData2.killed} gefunden - (Zwischenablage)!", "info", "top-left", 6250);
                                            player.TriggerEvent("Client:CopyToClipboard", tempData2.killed);
                                            tempData2.killed = "";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (GetRandomPercentage(15))
                                        {
                                            SendNotificationWithoutButton(player, $"Es wurden DNA Spuren gefunden, der Datenbankabgleich hat eine Ähnlichkeit zur Person: {tempData2.killed} ergeben - (Zwischenablage)!", "info", "top-left", 6250);
                                            player.TriggerEvent("Client:CopyToClipboard", tempData2.killed);
                                            tempData2.killed = "";
                                            return;
                                        }
                                    }
                                    tempData2.killed = "";
                                }
                            }
                        }
                        SendNotificationWithoutButton(player, $"Du hast am Tatort keine Hinweise gefunden!", "error", "top-left", 7500);
                    }
                    else if (action == "reanim")
                    {
                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                        if (getPlayer != null && getPlayer != player)
                        {
                            Character character2 = Helper.GetCharacterData(getPlayer);
                            if (character2 != null && character2.death == true)
                            {
                                NAPI.Task.Run(() =>
                                {
                                    SetPlayerPosition(getPlayer, new Vector3(getPlayer.Position.X,
                                                                     getPlayer.Position.Y,
                                                                     getPlayer.Position.Z + 0.15f));
                                }, delayTime: 55);
                                getPlayer.SetSharedData("Player:Adminsettings", "0,0,0");
                                getPlayer.TriggerEvent("Client:UnsetDeath");
                                character2.death = false;
                                getPlayer.SetOwnSharedData("Player:Death", false);
                                Helper.SendNotificationWithoutButton(getPlayer, $"Du wurdest reanimiert!", "success", "top-end", 3500);
                                Helper.SendNotificationWithoutButton(player, $"Du hast die Person erfolgreich reanimiert!", "success", "top-end", 3500);
                                SpawnPlayer(getPlayer, getPlayer.Position, getPlayer.Heading);
                                player.TriggerEvent("Client:PlaySoundPeep2");
                                getPlayer.TriggerEvent("Client:PlaySoundPeep2");
                                TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                                if (tempData2.cuffed > 0)
                                {
                                    getPlayer.SetSharedData("Player:AnimData", $"mp_arresting%idle%{49}");
                                }
                                Helper.SetPlayerHealth(getPlayer, 25);
                                return;
                            }
                        }
                        Helper.SendNotificationWithoutButton(player, $"Reanimation abgebrochen!", "error", "top-end", 3500);
                    }
                    else if (action == "check")
                    {
                        Player getPlayer = Helper.GetClosestPlayer(player, 2.5f);
                        if (getPlayer == null)
                        {
                            Helper.SendNotificationWithoutButton(player, $"Untersuchung abgebrochen!", "error", "top-end", 3500);
                            return;
                        }
                        Character character2 = Helper.GetCharacterData(getPlayer);
                        TempData tempData2 = Helper.GetCharacterTempData(getPlayer);
                        if (character2 != null && character2.death == false)
                        {
                            SendNotificationWithoutButton(player, "Untersuchung erfolgreich durchgeführt!", "success", "top-left", 3500);
                            player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                            List<CenterMenu> centerMenuList = new List<CenterMenu>();
                            CenterMenu centerMenu = new CenterMenu();
                            if (character2.disease == 0)
                            {
                                centerMenu.var1 = "Keine";
                            }
                            else if (character2.disease == 1)
                            {
                                centerMenu.var1 = "Erkältung";
                            }
                            else if (character2.disease == 1)
                            {
                                centerMenu.var1 = "Lebensmittelvergiftung";
                            }
                            if (NAPI.Player.GetPlayerHealth(getPlayer) >= 100)
                            {
                                centerMenu.var1 = "Keine Schmerzen";
                            }
                            else if (NAPI.Player.GetPlayerHealth(getPlayer) < 100 && NAPI.Player.GetPlayerHealth(getPlayer) >= 75)
                            {
                                centerMenu.var2 = "Leichte Schmerzen";
                            }
                            else if (NAPI.Player.GetPlayerHealth(getPlayer) < 75 && NAPI.Player.GetPlayerHealth(getPlayer) >= 45)
                            {
                                centerMenu.var2 = "Mittlere Schmerzen";
                            }
                            else if (NAPI.Player.GetPlayerHealth(getPlayer) < 45)
                            {
                                centerMenu.var2 = "Starke Schmerzen";
                            }
                            centerMenu.var3 = $"{NAPI.Player.GetPlayerHealth(getPlayer)}%";
                            centerMenu.var4 = "" + tempData2.tempValue + "/Stck";
                            String rules = "Krankheit,Schmerzzustand,Allgemeiner Zustand,Schussverletzungen";
                            centerMenuList.Add(centerMenu);
                            player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenuList), "Untersuchung");
                            return;
                        }
                        Helper.SendNotificationWithoutButton(player, $"Untersuchung abgebrochen!", "error", "top-end", 3500);
                    }
                    else if (action == "crafting")
                    {
                        string craftsettings = player.GetData<string>("Player:Craftsettings");
                        player.ResetData("Player:Craftsettings");
                        string[] craftArray = new string[4];
                        craftArray = craftsettings.Split(",");

                        int amount = 1;
                        if (craftArray[0].Contains("Munition"))
                        {
                            amount = 35;
                        }
                        else if (craftArray[0] == "Kokain")
                        {
                            amount = 10;
                        }
                        else if (craftArray[0] == "Crystal-Meth")
                        {
                            amount = 5;
                        }
                        else if (craftArray[0] == "Space-Cookies")
                        {
                            amount = 10;
                        }

                        Items newitem = null;
                        newitem = ItemsController.CreateNewItem(player, character.id, craftArray[0], "Player", amount, ItemsController.GetFreeItemID(player), "n/A", "n/A");
                        if (newitem != null)
                        {
                            tempData.itemlist.Add(newitem);
                        }
                        player.TriggerEvent("Client:PlaySoundPeep2");
                        SendNotificationWithoutButton(player, "Der Craftvorgang war erfolgreich!", "success", "top-left", 2500);
                        if (character.craftingskill < 75)
                        {
                            character.craftingskill++;
                        }
                    }
                    else if (action == "shoveling")
                    {
                        Helper.OnStopAnimation2(player);
                        if (Helper.GetRandomPercentage(90))
                        {
                            if (character.guessvalue >= 8500)
                            {
                                SendNotificationWithoutButton(player, "Du kannst keine weiteren Schätze mehr tragen!", "error", "top-left", 2500);
                                return;
                            }
                            List<String> schatzListe = new List<String>();
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("5,Meddlstein");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("255,Kupfer");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("95,Philipps Glasauge");
                            schatzListe.Add("215,Brieftasche");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("2050,Goldzahn");
                            schatzListe.Add("95,Philipps Glasauge");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("5,Meddlstein");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("375,Goldzahn von MC sHoOTi");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("400,Schweizer Taschenmesser");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("2750,Ametyst");
                            schatzListe.Add("3150,Rubin");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("1350,Nugget");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("400,Stückchen vom kleinem Nugget");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("0,Altes T-Shirt");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("0,Benutztes Kondom");
                            schatzListe.Add("400,Stückchen vom kleinem Nugget");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("950,Kleines Nugget");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("95,Philipps Glasauge");
                            schatzListe.Add("5,Meddlstein");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("0,Baumrinde");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("250,Antike Lampe");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("750,Perlen");
                            schatzListe.Add("5,Meddlstein");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("175,Wertvolles Pergament");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("450,Alte Münzen");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("250,Handy");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("75,Fernbedienung");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("450,Opium");
                            schatzListe.Add("105,Stück Messing");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("500,Goldene Kette");
                            schatzListe.Add("300,Ring");
                            schatzListe.Add("350,Kette");
                            schatzListe.Add("2750,Ametyst");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("315,Schmuck");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("900,Goldring");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("3750,Amethyst");
                            schatzListe.Add("215,Brieftasche");
                            schatzListe.Add("135,Ohrring");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("255,Kupfer");
                            schatzListe.Add("100,Alte Geldscheine");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("2350,Kleiner Diamant");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("0,Benutztes Kondom");
                            schatzListe.Add("95,Kaputte Vase");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("150,Wertvolles Geschirr");
                            schatzListe.Add("30,Spaex's HM 3 Prüfung");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("450,Opium");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("5,Metallüberreste");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("0,Benutztes Kondom");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("1750,Rohdiamant");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("650,Altes Testament");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("950,Kleines Nugget");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("150,Wertvolles Geschirr");
                            schatzListe.Add("2750,Kleiner Diamant");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("95,Kaputte Vase");
                            schatzListe.Add("0,Stück Autoreifen");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("25,Pfandflasche");
                            schatzListe.Add("85,San Andreas Flagge - 1 Auflage");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("3150,Rubin");
                            schatzListe.Add("35,Lappen");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("600,Antike Vase");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("900,Goldring");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("0,Benutztes Kondom");
                            schatzListe.Add("850,Tütchen Kokain");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("0,Stück Holz");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("50,Platine");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("1025,Goldener Zahn");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("0,Vollgerotztes Taschentuch");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("2050,Goldzahn");
                            schatzListe.Add("2750,Ametyst");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("65,Bauschutt aus Bayside");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("650,Ehering");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("10,Überreste");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("400,Stückchen vom kleinem Nugget");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("500,Goldene Kette");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("1150,Tütchen Kokain");
                            schatzListe.Add("0,Schrott");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("3250,Amethyst");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("650,Altes Testament");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("0,Schrott");
                            schatzListe.Add("150,Wertvolles Geschirr");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("750,Perlen");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("3000,Smaragd");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("400,Stückchen vom kleinem Nugget");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("150,Wertvolles Geschirr");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("175,Wertvolles Pergament");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("3150,Rubin");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("65,Bauschutt aus Bayside");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("215,Brieftasche");
                            schatzListe.Add("150,Wertvolles Geschirr");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("0,Erde");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("2050,Goldzahn");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("0,Benutztes Kondom");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("95,Kaputte Vase");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("95,Philipps Glasauge");
                            schatzListe.Add("50,Duplone");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("400,Sü�ckchen vom kleinem Nugget");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("1350,Nugget");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("5,Meddlstein");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("55,Besonderer Stein");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("400,Schweizer Taschenmesser");
                            schatzListe.Add("3000,Smaragd");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("25,Klappmesser");
                            schatzListe.Add("0,Dreck");
                            schatzListe.Add("25,Metallüberreste");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("650,Topas");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("2000,Nemesus-Roleplay Gamemode");
                            schatzListe.Add("65,Alten Zahn");
                            schatzListe.Add("2750,Ametyst");
                            schatzListe.Add("75,Kräuter");
                            schatzListe.Add("1,Sand");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("5,Alten Schuh");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("30,Schrott");
                            schatzListe.Add("750,Perlen");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("15,Sägeblatt");
                            schatzListe.Add("10,Steine");
                            schatzListe.Add("5,Seltene Blätter");
                            schatzListe.Add("0,Müll");
                            schatzListe.Add("5,Laub");
                            schatzListe.Add("500,Goldene Kette");
                            schatzListe.Add("5,Meddlstein");
                            schatzListe.Add("35,Armband Uhr");
                            schatzListe.Add("750,Goldring");

                            Random random = new Random();
                            int rand = random.Next(0, schatzListe.Count);

                            player.TriggerEvent("Client:PlaySoundPeep2");
                            SendNotificationWithoutButton(player, $"Du hast {schatzListe[rand].Split(",")[1]} im Wert von {schatzListe[rand].Split(",")[0]}$ gefunden!", "success", "top-left", 4500);
                            character.guessvalue += Convert.ToInt32(schatzListe[rand].Split(",")[0]);
                            if (Helper.GetRandomPercentage(1))
                            {
                                Helper.SendNotificationWithoutButton(player, "Deine Kleine-Schaufel ist gebrochen!", "error");
                                ItemsController.RemoveItemByName(player, "Kleine-Schaufel");
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Du hast keinen Schatz gefunden!", "error", "top-left", 2500);
                        }
                        tempData.inrob = false;
                    }
                    else if (action == "welding")
                    {
                        vehicle = GetClosestVehicle(player, 4.25f);
                        VehicleData vehicleData = DealerShipController.GetVehicleDataByVehicle(vehicle);
                        if (vehicle != null && vehicleData != null && (vehicle.HasData("Vehicle:WaffenTransport") || vehicle.HasData("Vehicle:AsservatenTransport")))
                        {
                            string from = "LSPD-Waffenkammer";
                            if (vehicle.HasData("Vehicle:AsservatenTransport"))
                            {
                                from = "LSPD Asservatenkammer";
                            }
                            if (MDCController.weaponOrderFaction == 2)
                            {
                                from = "LSRC-Waffenkammer";
                            }
                            if (MDCController.weaponOrderFaction == 3)
                            {
                                from = "ACLS-Waffenkammer";
                            }
                            Random random = new Random();
                            MDCController.weaponOrderStatus = "DNE";
                            MDCController.weaponOrder = "n/A";
                            vehicle.ResetData("Vehicle:WaffenTransport");
                            vehicle.ResetData("Vehicle:AsservatenTransport");
                            Helper.SetVehicleEngine(vehicle, false);
                            vehicle.Locked = false;
                            vehicle.Health = 175.0f;
                            NAPI.Vehicle.SetVehicleBodyHealth(vehicle, 175.0f);
                            NAPI.Vehicle.SetVehicleEngineHealth(vehicle, 175.0f);
                            vehicle.SetSharedData("Vehicle:Sync", $"1,{vehicleArray[1]},1,{vehicleArray[3]},1,{vehicleArray[5]},{vehicleArray[6]}");
                            vehicle.SetSharedData("Vehicle:Doors", "[false,false,false,false,true,false]");

                            int randomCount = random.Next(5, 10);

                            Items newitem = null;

                            if (MDCController.weaponOrderFaction == 1 || vehicle.HasData("Vehicle:AsservatenTransport"))
                            {
                                for (int i = 0; i < randomCount; i++)
                                {
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Pistole", "Trunk1-" + vehicleData.id, 1, ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                }

                                newitem = ItemsController.CreateNewItem(player, vehicleData.id, "9mm-Munition", "Trunk1-" + vehicleData.id, random.Next(200, 515), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Grosse-Schutzweste", "Trunk1-" + vehicleData.id, random.Next(0, 2), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                if (newitem.amount > 0)
                                {
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                }
                                if (GetRandomPercentage(35))
                                {
                                    randomCount = random.Next(1, 3);
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "MP5", "Trunk1-" + vehicleData.id, randomCount, ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    if (newitem.amount > 0)
                                    {
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                }
                                if (GetRandomPercentage(15))
                                {
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Drohne", "Trunk1-" + vehicleData.id, 1, ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    if (newitem.amount > 0)
                                    {
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                }
                                newitem = ItemsController.CreateNewItem(player, vehicleData.id, "5.56-Munition", "Trunk1-" + vehicleData.id, random.Next(85, 235), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                if (newitem.amount > 0)
                                {
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                }
                                newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Dietrich", "Trunk1-" + vehicleData.id, random.Next(1, 10), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                if (newitem.amount > 0)
                                {
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                }
                                if (vehicle.HasData("Vehicle:AsservatenTransport"))
                                {
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Materialien", "Trunk1-" + vehicleData.id, random.Next(125, 315), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    if (newitem.amount > 0)
                                    {
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                }
                            }
                            else
                            {
                                if (MDCController.weaponOrderFaction == 2)
                                {
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Bandage", "Trunk1-" + vehicleData.id, random.Next(5, 12), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Ibuprofee-400mg", "Trunk1-" + vehicleData.id, random.Next(4, 10), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    if (GetRandomPercentage(35))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Ibuprofee-800mg", "Trunk1-" + vehicleData.id, random.Next(2, 5), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                    if (GetRandomPercentage(25))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Morphin-10mg", "Trunk1-" + vehicleData.id, random.Next(1, 2), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                    if (GetRandomPercentage(15))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Drohne", "Trunk1-" + vehicleData.id, 1, ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                        if (newitem.amount > 0)
                                        {
                                            ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                        }
                                    }
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Dietrich", "Trunk1-" + vehicleData.id, random.Next(3, 10), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    if (newitem.amount > 0)
                                    {
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                }
                                else
                                {
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Reparaturwerkzeug", "Trunk1-" + vehicleData.id, random.Next(2, 6), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Benzinkanister", "Trunk1-" + vehicleData.id, random.Next(1, 3), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    if (GetRandomPercentage(65))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Taschenlampe", "Trunk1-" + vehicleData.id, random.Next(2, 4), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                    if (GetRandomPercentage(35))
                                    {
                                        newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Brechstange", "Trunk1-" + vehicleData.id, random.Next(1, 2), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                    newitem = ItemsController.CreateNewItem(player, vehicleData.id, "Dietrich", "Trunk1-" + vehicleData.id, random.Next(5, 15), ItemsController.GetFreeItemIDGlobal(), "n/A", from);
                                    if (newitem.amount > 0)
                                    {
                                        ItemsController.itemListGlobal.Add(ItemsController.CreateGlobalItemFromItem(newitem));
                                    }
                                }
                            }
                            SendNotificationWithoutButton(player, "Transporter aufgeknackt, greif mit der Taste [I] auf den Laderaum zu!", "success", "top-left", 4500);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Interaktion abgebrochen!", "error", "top-left", 3500);
                        }
                        return;
                    }
                    else if (action == "vehicle")
                    {
                        vehicle = GetClosestVehicle(player, 3.55f);
                        if (vehicle != null)
                        {
                            if (vehicle.GetData<int>("Vehicle:VLock") == 2)
                            {
                                SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                                return;
                            }
                            int skill = character.thiefskill / 25;
                            skill = skill * 3;
                            int percantage = 10 + skill;
                            if (vehicle.GetData<int>("Vehicle:VLock") == 1)
                            {
                                percantage -= 5;
                            }
                            if (GetRandomPercentage(20) && vehicle.GetSharedData<int>("Vehicle:Battery") > 0)
                            {
                                vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                                if (vehicleArray[2] == "0")
                                {
                                    vehicle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},1,{vehicleArray[3]},{vehicleArray[4]},{vehicleArray[5]},{vehicleArray[6]}");
                                    NAPI.Task.Run(() =>
                                    {
                                        if (vehicle != null)
                                        {
                                            vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                                            vehicle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},0,{vehicleArray[3]},{vehicleArray[4]},{vehicleArray[5]},{vehicleArray[6]}");
                                        }
                                    }, delayTime: 32500);
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                                }
                            }
                            if (GetRandomPercentage(percantage))
                            {
                                vehicle.Locked = false;
                                SendNotificationWithoutButton(player, "Fahrzeug erfolgreich aufgeknackt!", "success", "top-left", 3500);
                                if (character.thiefskill < 150)
                                {
                                    character.thiefskill++;
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                        }
                    }
                    else if (action == "vehicle2")
                    {
                        player.SetData<int>("Player:WireCooldown", Helper.UnixTimestamp() + (25));
                        vehicle = player.Vehicle;
                        if (vehicle != null || player.VehicleSeat != (int)VehicleSeat.Driver)
                        {
                            if (vehicle.GetData<int>("Vehicle:VLock") == 2)
                            {
                                SendNotificationWithoutButton(player, "Kurzschliessen fehlgeschlagen!", "error", "top-left", 3500);
                                return;
                            }
                            int skill = character.thiefskill / 25;
                            skill = skill * 3;
                            int percantage = 10 + skill;
                            if (vehicle.GetData<int>("Vehicle:VLock") == 1)
                            {
                                percantage -= 5;
                            }
                            if (GetRandomPercentage(percantage))
                            {
                                if (player.Vehicle.GetSharedData<float>("Vehicle:Fuel") <= 0)
                                {
                                    SendNotificationWithoutButton(player, "Der Tank des Fahrzeuges ist leer!", "error");
                                    return;
                                }
                                if (player.Vehicle.GetSharedData<int>("Vehicle:Oel") <= 0 || player.Vehicle.GetSharedData<int>("Vehicle:Battery") <= 0 || NAPI.Vehicle.GetVehicleHealth(player.Vehicle) <= 0 || NAPI.Vehicle.GetVehicleEngineHealth(player.Vehicle) <= 50)
                                {
                                    SendNotificationWithoutButton(player, "Der Motor springt nicht an!", "error");
                                    return;
                                }
                                if (player.Vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[4] == "1")
                                {
                                    SendNotificationWithoutButton(player, "Das Fahrzeug wurde ausgeschlachtet, der Motor springt nichtmehr an!", "error");
                                    return;
                                }
                                SendNotificationWithoutButton(player, "Fahrzeug erfolgreich kurzgeschlossen!", "success");
                                SetVehicleEngine(player.Vehicle, true);
                                player.SetOwnSharedData("Player:VehicleEngine", true);
                                player.TriggerEvent("Client:RadioOff");
                                if (character.thiefskill < 150)
                                {
                                    character.thiefskill++;
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Kurzschliessen fehlgeschlagen!", "error", "top-left", 3500);
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Kurzschliessen fehlgeschlagen!", "error", "top-left", 3500);
                        }
                    }
                    else if (action == "fillatm")
                    {
                        int minus = 0;
                        foreach (ATMSpots atmSpot in Helper.atmSpotList.ToList())
                        {
                            if (tempData.tempValue == atmSpot.id)
                            {
                                minus = 50000 - atmSpot.value;
                                if (minus > player.GetData<int>("Player:Money"))
                                {
                                    minus = player.GetData<int>("Player:Money");
                                }
                                atmSpot.value += minus;
                                if (atmSpot.value > 37500)
                                {
                                    Helper.atmSpotList.Remove(atmSpot);
                                }
                                break;
                            }
                        }
                        tempData.furniturePosition = null;
                        tempData.tempValue = 0;
                        tempData.jobstatus = 0;
                        if (minus > 0)
                        {
                            player.SetData<int>("Player:Money", player.GetData<int>("Player:Money") - minus);
                            SendNotificationWithoutButton(player, $"Der Bankautomat wurde erfolgreich mit {minus}$ befüllt, es befinden sich noch {player.GetData<int>("Player:Money")}$ im Geldkoffer!", "success", "top-left", 4500);
                            player.TriggerEvent("Client:PlaySoundPeep2");
                            Random random = new Random();
                            int money = 375 + random.Next(0, 131);
                            if (character.mygroup != -1)
                            {
                                Groups mygroup = GroupsController.GetGroupById(character.mygroup);
                                Bank bank = BankController.GetBankByBankNumber(mygroup.banknumber);
                                if (bank != null)
                                {
                                    int prov = 0;
                                    if (mygroup.provision > 0)
                                    {
                                        prov = money / 100 * mygroup.provision;
                                    }
                                    if (prov > 0 && character.defaultbank != "n/A")
                                    {
                                        Bank bank2 = BankController.GetDefaultBank(player, character.defaultbank);
                                        bank.bankvalue += money;
                                        bank.bankvalue -= prov;
                                        if (bank2 != null)
                                        {
                                            bank2.bankvalue += prov;
                                        }
                                        Helper.SendNotificationWithoutButton(player, $"{money}$ werden dem Konto deiner Firma gutgeschrieben. Du erhälst {prov}$ Provision!", "success", "top-left", 5500);
                                        Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Bankautomaten aufgefüllt und {money - prov}$ erwirtschaftet!");
                                    }
                                    else
                                    {
                                        bank.bankvalue += money;
                                        Helper.SendNotificationWithoutButton(player, $"{money}$ werden dem Konto deiner Firma gutgeschrieben!", "success", "top-left", 5500);
                                        Helper.CreateGroupMoneyLog(mygroup.id, $"{character.name} hat einen Bankautomaten aufgefüllt und {money}$ erwirtschaftet!");
                                    }
                                }
                                else
                                {
                                    character.nextpayday += money;
                                    Helper.SendNotificationWithoutButton(player, $"Du bekommst für deinen nächsten Gehaltscheck {money}$ gutgeschrieben!", "success", "top-left", 5500);
                                }
                            }
                            else
                            {
                                character.nextpayday += money;
                                Helper.SendNotificationWithoutButton(player, $"Du bekommst für deinen nächsten Gehaltscheck {money}$ gutgeschrieben!", "success", "top-left", 5500);
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, $"Der Bankautomat konnte nicht befüllt werden, es liegt ein technischer Defekt vor!", "error", "top-left", 4500);
                        }
                    }
                    else if (action == "house")
                    {
                        House house = House.GetClosestHouse(player, 2.75f);
                        if (house != null)
                        {
                            int skill = character.thiefskill / 25;
                            skill = skill * 3;
                            int percantage = 10 + skill;
                            if (GetRandomPercentage(percantage))
                            {
                                house.locked = 0;
                                SendNotificationWithoutButton(player, "Haustür erfolgreich aufgeknackt!", "success", "top-left", 3500);
                                if (character.thiefskill < 150)
                                {
                                    character.thiefskill++;
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                        }
                    }
                    else if (action == "door")
                    {
                        Doors door = DoorsController.GetClosestDoor(player, 2.15f);
                        if (door != null)
                        {
                            int skill = character.thiefskill / 25;
                            skill = skill * 3;
                            int percantage = 10 + skill;
                            if (GetRandomPercentage(percantage))
                            {
                                door.toogle = false;
                                SendNotificationWithoutButton(player, "Türe erfolgreich aufgeknackt!", "success", "top-left", 3500);
                                if (character.thiefskill < 150)
                                {
                                    character.thiefskill++;
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                        }
                    }
                    else if (action == "player")
                    {
                        Player getPlayer = GetClosestPlayer(player, 2.5f);
                        TempData tempData2 = GetCharacterTempData(getPlayer);
                        if (getPlayer != null && tempData2.cuffed == 1)
                        {
                            int skill = character.thiefskill / 25;
                            skill = skill * 3;
                            int percantage = 35 + skill;
                            if (GetRandomPercentage(percantage))
                            {
                                Helper.OnStopAnimation(getPlayer);
                                tempData2.cuffed = 0;
                                getPlayer.TriggerEvent("Client:SetCuff", false);
                                SendNotificationWithoutButton(player, "Handschellen erfolgreich aufgeknackt!", "success", "top-left", 3500);
                                if (character.thiefskill < 150)
                                {
                                    character.thiefskill++;
                                }
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                            }
                        }
                        else
                        {
                            SendNotificationWithoutButton(player, "Aufknacken fehlgeschlagen!", "error", "top-left", 3500);
                        }
                    }
                    else if (action == "animal1")
                    {
                        Random rnd = new Random();
                        Ped ped = player.GetData<Ped>("Player:TempPed");
                        if (ped == null || ped.GetSharedData<int>("Ped:Death") != 4)
                        {
                            SendNotificationWithoutButton(player, "Kein Fleisch gefunden!", "error", "top-end");
                            return;
                        }
                        int meat = rnd.Next(2, 5);
                        if (!ItemsController.CanPlayerHoldItem(player, meat * 250))
                        {
                            SendNotificationWithoutButton(player, "Du hast keinen Platz mehr im Inventar für das Fleisch!", "error", "top-end");
                            return;
                        }
                        Items newitem = ItemsController.CreateNewItem(player, character.id, "Fleisch", "Player", meat, ItemsController.GetFreeItemID(player));
                        if (newitem != null)
                        {
                            tempData.itemlist.Add(newitem);
                        }
                        SendNotificationWithoutButton(player, $"Du hast {meat}/Stcke Fleisch aus dem Tier gewonnen!", "success", "top-left", 3500);
                        ped.SetSharedData("Ped:Death", 2);
                        NAPI.Task.Run(() =>
                        {
                            HuntingController.CreateNewAnimal(ped);
                        }, delayTime: 7500);
                    }
                    else if (action == "fishing")
                    {
                        Helper.AddRemoveAttachments(player, "fishingRod", false);
                        Items bait = ItemsController.GetItemByItemName(player, "Köder");
                        if (bait != null)
                        {
                            bait.amount--;
                            if (bait.amount <= 0)
                            {
                                ItemsController.RemoveItem(player, bait.itemid);
                            }
                        }
                        Helper.OnStopAnimation(player);
                        int chance = 68;
                        int skill = character.fishingskill / 35;
                        Items newitem = null;
                        if (Helper.GetRandomPercentage(chance + (skill * 2)))
                        {
                            if (Helper.GetRandomPercentage(85 - (skill * 3)))
                            {
                                if (Helper.GetRandomPercentage(75 - (skill * 3)))
                                {
                                    SendNotificationWithoutButton(player, $"Du hast einen Dorsch geangelt!", "success", "top-left", 3500);
                                    newitem = ItemsController.CreateNewItem(player, character.id, "Dorsch", "Player", 1, ItemsController.GetFreeItemID(player));
                                    player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                }
                                else
                                {
                                    if (Helper.GetRandomPercentage(60 - (skill * 3)))
                                    {
                                        SendNotificationWithoutButton(player, $"Du hast eine Makrele geangelt!", "success", "top-left", 3500);
                                        newitem = ItemsController.CreateNewItem(player, character.id, "Makrele", "Player", 1, ItemsController.GetFreeItemID(player));
                                        player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                    }
                                    else
                                    {
                                        SendNotificationWithoutButton(player, $"Du hast eine Forelle geangelt!", "success", "top-left", 3500);
                                        newitem = ItemsController.CreateNewItem(player, character.id, "Forelle", "Player", 1, ItemsController.GetFreeItemID(player));
                                        player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                    }
                                }
                            }
                            else
                            {
                                if (Helper.GetRandomPercentage(90 - (skill * 3)))
                                {
                                    SendNotificationWithoutButton(player, $"Du hast einen Wildkarpfen geangelt!", "success", "top-left", 3500);
                                    newitem = ItemsController.CreateNewItem(player, character.id, "Wildkarpfen", "Player", 1, ItemsController.GetFreeItemID(player));
                                    player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                }
                                else
                                {
                                    SendNotificationWithoutButton(player, $"Du hast einen Teufelskärpfling geangelt!", "success", "top-left", 3500);
                                    newitem = ItemsController.CreateNewItem(player, character.id, "Teufelskärpfling", "Player", 1, ItemsController.GetFreeItemID(player));
                                    player.TriggerEvent("Client:PlaySoundSuccessExtra");
                                }
                            }
                        }
                        else
                        {
                            if (Helper.GetRandomPercentage(15))
                            {
                                SendNotificationWithoutButton(player, $"Du hast eine Pfandflasche geangelt!", "success", "top-left", 3500);
                                newitem = ItemsController.CreateNewItem(player, character.id, "Pfandflasche", "Player", 1, ItemsController.GetFreeItemID(player));
                                player.TriggerEvent("Client:PlaySoundSuccessExtra");
                            }
                            else
                            {
                                SendNotificationWithoutButton(player, $"Kein Fisch hat angebissen!", "success", "top-left", 3500);
                            }
                        }
                        if (newitem != null)
                        {
                            tempData.itemlist.Add(newitem);
                        }
                        if (character.fishingskill < 175)
                        {
                            character.fishingskill++;
                        }
                    }
                    else if (action == "mecha1")
                    {
                        if (vehicle == null) return;
                        SendNotificationWithoutButton(player, "Fahrzeug erfolgreich aufgebockt!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:Sync", $"0,0,{vehicleArray[2]},{vehicleArray[3]},{vehicleArray[4]},1|{vehicle.Position.Z.ToString(new CultureInfo("en-US"))},{vehicleArray[6]}");
                    }
                    else if (action == "mecha2")
                    {
                        if (vehicle == null) return;
                        SendNotificationWithoutButton(player, "Fahrzeug erfolgreich abgebockt!", "success", "top-left", 3500);
                    }
                    else if (action == "mecha3")
                    {
                        if (vehicle == null) return;
                        SendNotificationWithoutButton(player, "Fahrzeugdiagnose erfolgreich durchgeführt!", "success", "top-left", 3500);
                        player.SetSharedData("Player:AnimData", "WORLD_HUMAN_CLIPBOARD");
                        List<CenterMenu> centerMenuList = new List<CenterMenu>();
                        CenterMenu centerMenu = new CenterMenu();
                        centerMenu.var1 = "" + (NAPI.Vehicle.GetVehicleHealth(vehicle) > 0 ? NAPI.Vehicle.GetVehicleHealth(vehicle) / 1000 * 100 + "%" : "0%");
                        if (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[5] == "1")
                        {
                            centerMenu.var1 = "0%";
                        }
                        centerMenu.var2 = "" + (vehicle.GetSharedData<int>("Vehicle:Battery") > 0 ? vehicle.GetSharedData<int>("Vehicle:Battery") + "%" : 0 + "%");
                        centerMenu.var3 = "" + (vehicle.GetSharedData<int>("Vehicle:Oel") > 0 ? vehicle.GetSharedData<int>("Vehicle:Oel") + "%" : 0 + "%");
                        centerMenu.var4 = "" + vehicle.GetSharedData<float>("Vehicle:Fuel") + "l";
                        centerMenu.var5 = "" + (vehicle.GetSharedData<string>("Vehicle:Sync").Split(",")[3] == "1" ? "Kaputt" : "Funktiontüchtig");
                        if (vehicle.HasSharedData("Vehicle:Tuev"))
                        {
                            centerMenu.var6 = "" + Cars.GetTuev(vehicle.GetSharedData<int>("Vehicle:Tuev"));
                        }
                        else
                        {
                            centerMenu.var6 = "Nicht vorhanden";
                        }
                        String rules = "Allgemeiner Zustand,Autobatterie,Ölstand,Tankstand,Reifenstatus,TÜV";
                        centerMenuList.Add(centerMenu);
                        player.TriggerEvent("Client:ShowCenterMenu", rules, NAPI.Util.ToJson(centerMenuList), "Fahrzeugdiagnose");
                    }
                    else if (action == "mecha4")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat ein Fahrzeug für {tempData.tempValue} Utensilien repariert!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat ein Fahrzeug für {tempData.tempValue} Utensilien repariert!");
                        }
                        NAPI.Vehicle.RepairVehicle(vehicle);
                        SendNotificationWithoutButton(player, $"Fahrzeug erfolgreich mit {tempData.tempValue} Utensilien repariert!", "success", "top-left", 3500);
                        vehicleArray = vehicle.GetSharedData<string>("Vehicle:Sync").Split(",");
                        vehicle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},0,0,0,{vehicleArray[5]},{vehicleArray[6]}");
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha5")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat den Tank von einem Fahrzeug für {tempData.tempValue} Utensilien repariert!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat den Tank von einem Fahrzeug für {tempData.tempValue} Utensilien repariert!");
                        }
                        SendNotificationWithoutButton(player, $"Tank erfolgreich mit {tempData.tempValue} Utensilien ausgepumpt!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:Sync", $"{vehicleArray[0]},{vehicleArray[1]},{vehicleArray[2]},0,{vehicleArray[4]},{vehicleArray[5]},{vehicleArray[6]}");
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha6")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat die Reifen von einem Fahrzeug für {tempData.tempValue} Utensilien repariert!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat die Reifen von einem Fahrzeug für {tempData.tempValue} Utensilien repariert!");
                        }
                        SendNotificationWithoutButton(player, $"Reifen erfolgreich mit {tempData.tempValue} Utensilien repariert!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:Fuel", 0.0f);
                        Helper.SetVehicleEngine(vehicle, false);
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha7")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat das ÖL von einem Fahrzeug für {tempData.tempValue} Utensilien nachgefüllt!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat das ÖL von einem Fahrzeug für {tempData.tempValue} Utensilien nachgefüllt!");
                        }
                        SendNotificationWithoutButton(player, $"ÖL erfolgreich mit {tempData.tempValue} Utensilien nachgefüllt!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:Oel", 100);
                        Helper.SetVehicleEngine(vehicle, false);
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha8")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat die Batterie von einem Fahrzeug für {tempData.tempValue} Utensilien getauscht!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat die Batterie von einem Fahrzeug für {tempData.tempValue} Utensilien getauscht!");
                        }
                        SendNotificationWithoutButton(player, $"Batterie erfolgreich mit {tempData.tempValue} Utensilien getauscht!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:Battery", 100);
                        Helper.SetVehicleEngine(vehicle, false);
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha9")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat eine TÜV Plakette an einem Fahrzeug für {tempData.tempValue} angebracht!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat eine TÜV Plakette an einem Fahrzeug für {tempData.tempValue} angebracht!");
                        }
                        SendNotificationWithoutButton(player, $"TÜV Plakette erfolgreich mit {tempData.tempValue} Utensilien angebracht!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:Tuev", Helper.UnixTimestamp() + (93 * 86400));
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha10")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat das Schloss von einem Fahrzeug für {tempData.tempValue} Utensilien verbessert!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat das Schloss von einem Fahrzeug für {tempData.tempValue} Utensilien verbessert!");
                        }
                        SendNotificationWithoutButton(player, $"Schloss erfolgreich mit {tempData.tempValue} Utensilien verbessert!", "success", "top-left", 3500);
                        vehicle.SetSharedData("Vehicle:VLock", 1);
                        Helper.SetVehicleEngine(vehicle, false);
                        tempData.tempValue = 0;
                    }
                    else if (action == "mecha11")
                    {
                        House house = null;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            house = House.GetHouseById(2);
                        }
                        else
                        {
                            house = House.GetHouseByGroupId(character.mygroup);
                        }
                        if (house == null)
                        {
                            SendNotificationWithoutButton(player, $"Es wurde kein Firmenhaus/Fraktionshaus gefunden!", "error");
                            return;
                        }
                        house.stock -= tempData.tempValue;
                        if (character.faction == 3 && character.factionduty == true)
                        {
                            Helper.CreateFactionLog(character.faction, $"{character.name} hat das Schloss von einem Fahrzeug für {tempData.tempValue} Utensilien ausgetauscht, der Schlüssel kann im Rathaus nachgemacht werden!");
                        }
                        else
                        {
                            Helper.CreateGroupMoneyLog(character.mygroup, $"{character.name} hat das Schloss von einem Fahrzeug für {tempData.tempValue} Utensilien ausgetauscht, der Schlüssel kann im Rathaus nachgemacht werden!");
                        }
                        SendNotificationWithoutButton(player, $"Schloss erfolgreich mit {tempData.tempValue} Utensilien ausgetauscht!", "success", "top-left", 3500);
                        Helper.SetVehicleEngine(vehicle, false);
                        tempData.tempValue = 0;
                        foreach (Cars car in Cars.carList)
                        {
                            if (car.vehicleHandle != null && car.vehicleHandle == vehicle)
                            {
                                MySqlCommand command = General.Connection.CreateCommand();

                                string vehicleString = $"{car.vehicleData.vehiclename}: {car.vehicleData.id}";

                                command.CommandText = $"UPDATE characters SET items = REPLACE(items, '{vehicleString}', 'n/A') WHERE items LIKE @vehicle";
                                command.Parameters.AddWithValue("@id", car.vehicleData.owner[car.vehicleData.owner.Length - 1]);
                                command.Parameters.AddWithValue("@vehicle", $"%{vehicleString}%");

                                command.ExecuteNonQuery();

                                foreach (Player p in NAPI.Pools.GetAllPlayers())
                                {
                                    if (p != null && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                                    {
                                        Character character2 = Helper.GetCharacterData(p);
                                        if (character2 != null)
                                        {
                                            ItemsController.DeleteItemWithProp(p, vehicleString);
                                            ItemsController.UpdateInventory(p);
                                        }
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[OnFinishProgress]: " + e.ToString());
            }
        }

        public static string CreateCoupon(string coupontext, int usages = 1)
        {
            String coupon = "n/A";
            try
            {
                coupon = GenerateRandomString(8);

                MySqlCommand command;
                command = General.Connection.CreateCommand();
                command.CommandText = "INSERT INTO coupons (coupon, coupontext, usages, timestamp) VALUES (@coupon, @coupontext, @usages, @timestamp)";
                command.Parameters.AddWithValue("@coupon", coupon);
                command.Parameters.AddWithValue("@coupontext", coupontext);
                command.Parameters.AddWithValue("@usages", usages);
                command.Parameters.AddWithValue("@timestamp", UnixTimestamp());
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CreateCoupon]: " + e.ToString());
            }
            return coupon;
        }

        [RemoteEvent("Server:GetNearestEntity")]
        public static void GetNearestEntity(Player player, Player getplayer, Vehicle getvehicle)
        {
            try
            {
                if (getplayer != null || getvehicle != null)
                {
                    if (getplayer != null)
                    {
                        player.SetData<Player>("Player:NearestPlayer", getplayer);
                        player.SetData<Vehicle>("Player:NearestVehicle", null);
                    }
                    if (getvehicle != null)
                    {
                        player.SetData<Player>("Player:NearestPlayer", null);
                        player.SetData<Vehicle>("Player:NearestVehicle", getvehicle);
                    }
                }
                else
                {
                    player.SetData<Player>("Player:NearestPlayer", null);
                    player.SetData<Vehicle>("Player:NearestVehicle", null);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[GetNearestPlayer]: " + e.ToString());
            }
        }

        //Wardrobe
        public static void ShowWardrobe(Player player, FurnitureSetHouse furniture)
        {
            try
            {
                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                List<Outfits> outfitList = new List<Outfits>();
                foreach (Outfits outfit in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 10", "furniture-" + furniture.id))
                {
                    outfitList.Add(outfit);
                }
                player.SetData<int>("Player:WardrobeID", furniture.id);
                player.TriggerEvent("Client:ShowWardrobe", NAPI.Util.ToJson(outfitList));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[ShowWardrobe]: " + e.ToString());
            }
        }

        public static void UpdateWardrobe(Player player)
        {
            try
            {
                int wardrobeID = player.GetData<int>("Player:WardrobeID");

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                List<Outfits> outfitList = new List<Outfits>();
                foreach (Outfits outfit in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 10", "furniture-" + wardrobeID))
                {
                    outfitList.Add(outfit);
                }
                player.TriggerEvent("Client:UpdateWardrobe", NAPI.Util.ToJson(outfitList));
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[UpdateWardrobe]: " + e.ToString());
            }
        }

        [RemoteEvent("Server:WardrobeAktion")]
        public static void WardrobeAktion(Player player, String aktion, String name = "n/A")
        {
            try
            {
                Character character = GetCharacterData(player);

                if (character == null) return;

                int wardrobeID = player.GetData<int>("Player:WardrobeID");

                PetaPoco.Database db = new PetaPoco.Database(General.Connection);
                List<Outfits> outfitList = new List<Outfits>();
                foreach (Outfits outfit in db.Fetch<Outfits>("SELECT * FROM outfits WHERE owner = @0 LIMIT 10", "furniture-" + wardrobeID))
                {
                    outfitList.Add(outfit);
                }

                switch (aktion.ToLower())
                {
                    case "selectoutfit":
                        {
                            Outfits outfits = outfitList.Find(o => o.name == name);
                            if (outfits == null)
                            {
                                SendNotificationWithoutButton(player, "Ungültiges Outfit!", "error");
                                return;
                            }

                            String json1 = outfits.json1;
                            String json2 = outfits.json2;
                            string[] json1Array = new string[15];
                            string[] json2Array = new string[15];
                            JObject obj = null;

                            obj = JObject.Parse(character.json);
                            json1 = json1.Substring(1, json1.Length - 2);
                            json2 = json2.Substring(1, json2.Length - 2);

                            json1Array = json1.Split(",");
                            json2Array = json2.Split(",");

                            NAPI.Util.ConsoleOutput(json1);

                            for (int i = 0; i < json1Array.Length; i++)
                            {
                                obj["clothing"][i] = Convert.ToInt32(json1Array[i]);
                                obj["clothingColor"][i] = Convert.ToInt32(json2Array[i]);
                            }

                            CharacterController.SetCharacterCloths(player, obj, character.clothing);

                            character.json = NAPI.Util.ToJson(obj);

                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "DELETE FROM outfits WHERE name = @name and owner = @owner LIMIT 1";
                            command.Parameters.AddWithValue("name", name);
                            command.Parameters.AddWithValue("owner", "furniture-" + wardrobeID);
                            command.ExecuteNonQuery();

                            SendNotificationWithoutButton(player, "Outfit ausgewählt!", "success");

                            Helper.PlayShortAnimation(player, "missmic4", "michael_tux_fidget", 1500);

                            UpdateWardrobe(player);
                            break;
                        }
                    case "deleteoutfit":
                        {
                            Outfits outfits = outfitList.Find(o => o.name == name);
                            if (outfits == null)
                            {
                                SendNotificationWithoutButton(player, "Ungültiges Outfit!", "error");
                                return;
                            }
                            MySqlCommand command = General.Connection.CreateCommand();
                            command.CommandText = "DELETE FROM outfits WHERE name = @name and owner = @owner LIMIT 1";
                            command.Parameters.AddWithValue("name", name);
                            command.Parameters.AddWithValue("owner", "furniture-" + wardrobeID);
                            command.ExecuteNonQuery();
                            SendNotificationWithoutButton(player, "Outfit gelöscht!", "success");
                            UpdateWardrobe(player);
                            break;
                        }
                    case "newoutfit":
                        {
                            if (outfitList.Count >= 10)
                            {
                                SendNotificationWithoutButton(player, "Es sind schon alle Plätze belegt!", "error");
                                return;
                            }

                            Outfits outfits = outfitList.Find(o => o.name == name);
                            if (outfits != null || name.ToLower() == "leer")
                            {
                                SendNotificationWithoutButton(player, "Dieser Outfitname ist schon belegt!", "error");
                                return;
                            }

                            JObject obj = null;
                            obj = JObject.Parse(character.json);

                            if (character.gender == 1)
                            {
                                NAPI.Player.SetPlayerClothes(player, 3, 15, 0);
                                obj["clothing"][1] = 15;
                                obj["clothingColor"][1] = 0;
                            }
                            else
                            {
                                NAPI.Player.SetPlayerClothes(player, 3, 0, 0);
                                obj["clothing"][1] = 0;
                                obj["clothingColor"][1] = 0;
                            }

                            NAPI.Player.SetPlayerClothes(player, 11, 15, 0);
                            obj["clothing"][0] = 15;
                            obj["clothingColor"][0] = 0;

                            NAPI.Player.SetPlayerClothes(player, 4, 14, 0);
                            obj["clothing"][2] = 14;
                            obj["clothingColor"][2] = 0;

                            if (character.gender == 1)
                            {
                                NAPI.Player.SetPlayerClothes(player, 8, 15, 0);
                                obj["clothing"][4] = 15;
                                obj["clothingColor"][4] = 0;
                            }
                            else
                            {
                                NAPI.Player.SetPlayerClothes(player, 8, 0, 0);
                                obj["clothing"][4] = 0;
                                obj["clothingColor"][4] = 0;
                            }

                            if (character.gender == 1)
                            {
                                NAPI.Player.SetPlayerClothes(player, 6, 34, 0);
                                obj["clothing"][3] = 34;
                                obj["clothingColor"][3] = 0;
                            }
                            else
                            {
                                NAPI.Player.SetPlayerClothes(player, 6, 35, 0);
                                obj["clothing"][3] = 35;
                                obj["clothingColor"][3] = 0;
                            }

                            NAPI.Player.ClearPlayerAccessory(player, 1);
                            obj["clothing"][6] = 255;
                            obj["clothingColor"][6] = 0;

                            NAPI.Player.ClearPlayerAccessory(player, 0);
                            obj["clothing"][7] = 255;
                            obj["clothingColor"][7] = 0;

                            NAPI.Player.ClearPlayerAccessory(player, 2);
                            obj["clothing"][9] = 255;
                            obj["clothingColor"][9] = 0;
                            NAPI.Player.ClearPlayerAccessory(player, 6);
                            obj["clothing"][10] = 255;
                            obj["clothingColor"][10] = 0;
                            NAPI.Player.ClearPlayerAccessory(player, 7);
                            obj["clothing"][11] = 255;
                            obj["clothingColor"][11] = 0;
                            NAPI.Player.SetPlayerClothes(player, 5, 0, 0);
                            obj["clothing"][5] = 0;
                            obj["clothingColor"][5] = 0;
                            NAPI.Player.SetPlayerClothes(player, 7, 0, 0);
                            obj["clothing"][12] = 0;
                            obj["clothingColor"][12] = 0;

                            NAPI.Player.SetPlayerClothes(player, 1, 0, 0);
                            obj["clothing"][8] = 0;
                            obj["clothingColor"][8] = 0;

                            character.json = NAPI.Util.ToJson(obj);

                            Outfits outfit = new Outfits();
                            outfit.name = name;
                            outfit.owner = "furniture-" + wardrobeID;
                            outfit.json1 = NAPI.Util.ToJson(obj["clothing"]);
                            outfit.json2 = NAPI.Util.ToJson(obj["clothingColor"]);

                            db.Insert(outfit);

                            SendNotificationWithoutButton(player, "Aktuelles Outfit erfolgreich in den Kleiderschrank gelegt!", "success");

                            Helper.PlayShortAnimation(player, "missmic4", "michael_tux_fidget", 1500);

                            UpdateWardrobe(player);
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[WardrobeAktion]: " + e.ToString());
            }
        }

        //GetPositionInFrontOfPlayer
        public static Vector3 GetPositionInFrontOfPlayer(Player player, float dist = 1.1f)
        {
            double radians = -player.Heading * Math.PI / 180;
            double nx = player.Position.X + (dist * Math.Sin(radians));
            double ny = player.Position.Y + (dist * Math.Cos(radians));
            return new Vector3(nx, ny, player.Position.Z);
        }

        public static Vector3 GetPositionInFrontOfPosition(Vector3 position, float angle, float dist = 1.1f)
        {
            double radians = -angle * Math.PI / 180;
            double nx = position.X + (dist * Math.Sin(radians));
            double ny = position.Y + (dist * Math.Cos(radians));
            return new Vector3(nx, ny, position.Z);
        }

        public static Vector3 GetPositionBehindOfVehicle(Vehicle vehicle, float dist = 1.35f)
        {
            double radians = -vehicle.Heading * Math.PI / 180;
            double nx = vehicle.Position.X - (dist * Math.Sin(radians));
            double ny = vehicle.Position.Y - (dist * Math.Cos(radians));
            return new Vector3(nx, ny, vehicle.Position.Z);
        }

        public static Vector3 GetPositionBehindOfPlayer(Player player, float dist = 1.55f)
        {
            double radians = -player.Heading * Math.PI / 180;
            double nx = player.Position.X - (dist * Math.Sin(radians));
            double ny = player.Position.Y - (dist * Math.Cos(radians));
            return new Vector3(nx, ny, player.Position.Z);
        }

        public static Vector3 GetPositionBehindOfPosition(Vector3 position, float angle, float dist = 1.1f)
        {
            double radians = -angle * Math.PI / 180;
            double nx = position.X + (dist * Math.Sin(radians));
            double ny = position.Y + (dist * Math.Cos(radians));
            return new Vector3(nx, ny, position.Z);
        }

        public static void SetAndGetWeather()
        {
            try
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        if (weatherTimestamp != 0 && weatherTimestamp > UnixTimestamp()) return;
                        var request = (HttpWebRequest)WebRequest.Create("http://api.openweathermap.org/data/2.5/onecall?lat=40.416775&lon=-3.703790&exclude=minutely,hourly,alerts&appid=2a1286a10ecda09b311ca793ecc0e0b8");
                        request.Method = "GET";
                        request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                        var content = string.Empty;
                        using (var response = (HttpWebResponse)request.GetResponse())
                        {
                            using (var stream = response.GetResponseStream())
                            {
                                using (var sr = new StreamReader(stream))
                                {
                                    content = sr.ReadToEnd();
                                    weatherObj = JObject.Parse(content);
                                    Weather weather = new Weather();
                                    string tempstring2;
                                    tempstring2 = weatherObj["current"].ToString();
                                    JObject weatherObjTemp2 = JObject.Parse(tempstring2);
                                    string tempstring;
                                    tempstring = weatherObjTemp2["weather"].ToString();
                                    tempstring = tempstring.Substring(2);
                                    tempstring = tempstring.Substring(0, tempstring.Length - 1);
                                    if (tempstring.Length > 10)
                                    {
                                        try
                                        {
                                            JObject weatherObjTemp = JObject.Parse(tempstring);
                                            weatherstring = weatherObjTemp["description"].ToString().ToLower();
                                        }
                                        catch (Exception e)
                                        {
                                            Helper.ConsoleLog("error", $"[SetAndGetWeather]: " + e.ToString());
                                            weatherstring = "clear sky";
                                        }
                                    }
                                    else
                                    {
                                        weatherstring = "clear sky";
                                    }
                                    SetWeather();
                                }
                            }
                        }
                    });
                });
            }
            catch (Exception)
            {
                weatherstring = "clear sky";
                SetWeather();
            }
        }

        public static void SetWeather()
        {
            try
            {
                switch (weatherstring)
                {
                    case "clear sky":
                        {
                            NAPI.World.SetWeather("CLEAR");
                            break;
                        }
                    case "few clouds":
                        {
                            NAPI.World.SetWeather("CLOUDS");
                            break;
                        }
                    case "scattered clouds":
                        {
                            NAPI.World.SetWeather("OVERCAST");
                            break;
                        }
                    case "broken clouds":
                        {
                            NAPI.World.SetWeather("OVERCAST");
                            break;
                        }
                    case "overcast clouds":
                        {
                            NAPI.World.SetWeather("OVERCAST");
                            break;
                        }
                    case "shower rain":
                        {
                            NAPI.World.SetWeather("RAIN");
                            break;
                        }
                    case "light rain":
                        {
                            NAPI.World.SetWeather("RAIN");
                            break;
                        }
                    case "rain":
                        {
                            NAPI.World.SetWeather("RAIN");
                            break;
                        }
                    case "thunderstorm":
                        {
                            NAPI.World.SetWeather("THUNDER");
                            break;
                        }
                    case "snow":
                        {
                            NAPI.World.SetWeather("SNOW");
                            break;
                        }
                    case "mist":
                        {
                            NAPI.World.SetWeather("FOGGY");
                            break;
                        }
                    default:
                        {
                            NAPI.World.SetWeather("CLEAR");
                            break;
                        }
                }
            }
            catch (Exception)
            {
                weatherstring = "clear sky";
                NAPI.World.SetWeather("CLEAR");
            }
        }

        public static bool CheckForAttachment(Player player, string propname)
        {
            try
            {
                TempData tempData = GetCharacterTempData(player);
                if (tempData != null)
                {
                    if (tempData.attachments.Contains(propname))
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckForAttachment]: " + e.ToString());
            }
            return false;
        }

        [RemoteEvent("Server:AddRemoveAttachments")]
        //Attachments
        public static void AddRemoveAttachments(Player player, String propname, bool check)
        {
            try
            {
                bool changes = false;
                TempData tempData = GetCharacterTempData(player);
                if (tempData != null)
                {
                    if (check == true)
                    {
                        if (!tempData.attachments.Contains(propname))
                        {
                            tempData.attachments.Add(propname);
                            changes = true;
                        }
                    }
                    else
                    {
                        if (tempData.attachments.Contains(propname))
                        {
                            tempData.attachments.Remove(propname);
                            changes = true;
                        }
                    }
                    if (changes == true)
                    {
                        String csv = "";
                        if (tempData.attachments.Count > 1)
                        {
                            csv = String.Join(",", tempData.attachments.Select(x => x.ToString()).ToArray());
                        }
                        else
                        {
                            if (tempData.attachments.Count > 0)
                            {
                                csv = $"{tempData.attachments[0]},n/A";
                            }
                            else
                            {
                                csv = "0";
                            }
                        }
                        player.SetSharedData("Player:Attachments", csv);
                    }
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[AddRemoveAttachments]: " + e.ToString());
            }
        }

        //Shovel
        public static void StartShovel(Player player)
        {
            try
            {
                TempData tempData = Helper.GetCharacterTempData(player);
                Character character = Helper.GetCharacterData(player);
                if (character != null && tempData != null)
                {
                    if (character.afk == 0 && tempData.inrob == true) return;
                    player.SetSharedData("Player:AnimData", "WORLD_HUMAN_GARDENER_PLANT");
                    NAPI.Player.SetPlayerCurrentWeapon(player, WeaponHash.Unarmed);
                    player.TriggerEvent("Client:StartLockpicking", 12, "shoveling", "Nach Schätzen suchen...");

                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[StartShovel]: " + e.ToString());
            }
        }

        //Saltychat
        public static void CheckSaltyChat(Player player)
        {
            try
            {
                NAPI.Task.Run(() =>
                {
                    if (player.HasOwnSharedData("Player:Voice") && player.GetOwnSharedData<int>("Player:Voice") == -2)
                    {
                        player.SetOwnSharedData("Player:Voice", -1);
                    }
                }, delayTime: 8500);
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[CheckSaltyChat]: " + e.ToString());
            }
        }

        public static string GenerateRandomString(int length)
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        //GeneratePin
        public static string GeneratePin(int length)
        {
            const string valid = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        //Round
        public static int Round(int iNumber)
        {
            int iReturn;
            double dNumber;

            dNumber = (double)iNumber / 100;

            iReturn = (int)Math.Ceiling(dNumber);

            iReturn = iReturn * 100;

            return iReturn;
        }

        //SyncThings
        public static void SyncThings(Player player)
        {
            try
            {
                Business sprunk = Business.GetBusinessById(16);
                Character character = Helper.GetCharacterData(player);
                Account account = Helper.GetAccountData(player);
                if (sprunk != null && character != null && account != null)
                {
                    string prices = $"{Convert.ToInt32(30 * sprunk.multiplier)},";
                    player.TriggerEvent("Client:SyncThings", prices, character.animations, account.crosshair, adminSettings.groupsettings, account.level, character.name, Helper.adminSettings.voicerp, Helper.adminSettings.nametag);
                }
            }
            catch (Exception e)
            {
                Helper.ConsoleLog("error", $"[SyncThings]: " + e.ToString());
            }
        }

        public static void DeleteOldLogs()
        {
            try
            {
                MySqlCommand command = General.Connection.CreateCommand();
                command.CommandText = "DELETE FROM adminlogs WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 31 DAY))";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM logs WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 93 DAY))";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM bankfile WHERE banktime <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 186 DAY))";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM banksettings WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 186 DAY))";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM paydays WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 31 DAY))";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM coupons WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 93 DAY)) OR usages = 0";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM lifeinvaderads WHERE timestamp <= UNIX_TIMESTAMP(DATE(NOW() - INTERVAL 1 DAY))";
                command.ExecuteNonQuery();

                //Nur jeden Montag
                System.DateTime moment = new System.DateTime(Helper.UnixTimestamp());
                if (moment.DayOfWeek == DayOfWeek.Monday)
                {
                    command.CommandText = "UPDATE adminsettings SET adcount = 0";
                    command.ExecuteNonQuery();

                    command.CommandText = "UPDATE characters SET adcount = 0";
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ConsoleLog("error", $"[DeleteOldLogs]: " + e.ToString());
            }
        }

        public static bool GetRandomPercentage(int percentage)
        {
            try
            {
                Random rand = new Random();
                int randPercentage = rand.Next(0, 101);
                if (percentage >= randPercentage)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                ConsoleLog("error", $"[GetRandomPercentage]: " + e.ToString());
            }
            return false;
        }

        //SetPlayerArmor
        public static void SetPlayerArmor(Player player, int armor)
        {
            if (armor >= 100)
            {
                armor = 99;
            }
            player.SetOwnSharedData("Player:Armor", armor);
            NAPI.Player.SetPlayerArmor(player, armor);
        }

        public static void SetPlayerPosition(Player player, Vector3 position, int waittime = 485)
        {
            player.TriggerEvent("Client:UpdatePosition", position.X, position.Y, position.Z);
            NAPI.Task.Run(() =>
            {
                player.Position = position;
            }, delayTime: waittime);
        }

        public static void SpawnPlayer(Player player, Vector3 position, float rotation, int waittime = 405)
        {
            player.TriggerEvent("Client:UpdatePosition");
            NAPI.Task.Run(() =>
            {
                NAPI.Player.SpawnPlayer(player, position, rotation);
            }, delayTime: waittime);
        }

        public static void SetPlayerHealth(Player player, int health)
        {
            player.SetOwnSharedData("Player:Health", health);
            NAPI.Player.SetPlayerHealth(player, health);
        }

        //GetClosestVehicleFromVehicle
        public static Vehicle GetClosestVehicleFromVehicle2(Vehicle vehicle, float distance)
            => NAPI.Pools.GetAllVehicles()
                    .Where(v => vehicle.Position.DistanceTo(v.Position) <= distance && v != vehicle && vehicle.Dimension == v.Dimension)
                    .OrderBy(v => vehicle.Position.DistanceTo(v.Position))
                    .FirstOrDefault();

        public static Vehicle GetClosestVehicleFromVehicle(Player player, float distance)
            => NAPI.Pools.GetAllVehicles()
                    .Where(v => player.Position.DistanceTo(v.Position) <= distance && v != player.Vehicle && player.Dimension == v.Dimension)
                    .OrderBy(v => player.Position.DistanceTo(v.Position))
                    .FirstOrDefault();

        //GetClosestVehicle
        public static Vehicle GetClosestVehicle(Player player, float distance)
            => NAPI.Pools.GetAllVehicles()
                    .Where(v => player.Position.DistanceTo(v.Position) <= distance && player.Dimension == v.Dimension)
                    .OrderBy(v => player.Position.DistanceTo(v.Position))
                    .FirstOrDefault();

        //GetClosestVehicleByName
        public static Vehicle GetClosestVehicleByName(Player player, float distance, string name)
            => NAPI.Pools.GetAllVehicles()
            .Where(v => player.Position.DistanceTo(v.Position) <= distance && v.GetSharedData<string>("Vehicle:Name").ToLower().Contains(name.ToLower()) && player.Dimension == v.Dimension)
            .OrderBy(v => player.Position.DistanceTo(v.Position))
            .FirstOrDefault();

        //GetClosestPlayer
        public static Player GetClosestPlayer(Player player, float distance)
            => NAPI.Pools.GetAllPlayers()
                    .Where(p => player.Position.DistanceTo(p.Position) <= distance && p != player && player.Dimension == p.Dimension && p.GetOwnSharedData<bool>("Player:Spawned") == true)
                    .OrderBy(p => player.Position.DistanceTo(p.Position))
                    .FirstOrDefault();

        //GetClosestPed
        public static Ped GetClosestPed(Player player, float distance)
            => NAPI.Pools.GetAllPeds()
                    .Where(p => player.Position.DistanceTo(p.Position) <= distance)
                    .OrderBy(p => player.Position.DistanceTo(p.Position))
                    .FirstOrDefault();


        public static Int32 GetAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
