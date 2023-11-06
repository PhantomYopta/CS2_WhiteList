using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;

namespace WhiteList;

public class WhiteList : BasePlugin
{
    public override string ModuleName { get; }
    public override string ModuleVersion { get; }

    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnClientConnected>((slot =>
        {
            var player = Utilities.GetPlayerFromSlot(slot);
            if(player.IsBot) return;
            var steamId = player.SteamID;
            
            var path = Path.Combine(ModuleDirectory, "whitelist.txt");
            
            if(!File.Exists(path)) File.WriteAllLines(path, new []{"Steamid1", "Steamid2"});
            
            var whiteList = File.ReadAllLines(path);

            if (!whiteList.Contains(steamId.ToString()))
            {
                Server.ExecuteCommand($"kickid {player.UserId}");
            }
        }));
    }
}