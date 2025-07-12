using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;

namespace DiscordJoinNotifierPlugin;

public class DiscordJoinNotifier : BasePlugin
{
    public override string ModuleName => "DiscordJoinNotifier";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "By AndiiCodes - https://github.com/AndiiCodes";

    private JoinNotifierConfig _config = null!;
    private static readonly HttpClient client = new();

    public override void Load(bool hotReload)
    {
        Server.PrintToConsole($"[DiscordJoinNotifier] Load called. Working dir: {Directory.GetCurrentDirectory()}");

        try
        {
            _config = LoadConfig();
            Server.PrintToConsole($"[DiscordJoinNotifier] Config loaded successfully.");
        }
        catch (Exception ex)
        {
            Server.PrintToConsole($"[DiscordJoinNotifier] Failed to load config: {ex.Message}");
            _config = new JoinNotifierConfig(); 
        }

        RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
    }

    private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;

        var name = player.PlayerName ?? "Unknown";
        var steamId = player.SteamID.ToString() ?? "Unknown";

        var message = _config.MessageFormat
            .Replace("{player}", name)
            .Replace("{steamid}", steamId);

        var payload = new { content = message };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _ = Task.Run(async () =>
        {
            try
            {
                await client.PostAsync(_config.WebhookUrl, content);
            }
            catch (Exception ex)
            {
                Server.PrintToConsole($"[DiscordJoinNotifier] Failed to send webhook: {ex.Message}");
            }
        });

        return HookResult.Continue;
    }

    private JoinNotifierConfig LoadConfig()
    {
       
        string pluginDirectory = ModuleDirectory;
        Server.PrintToConsole($"[DiscordJoinNotifier] Plugin directory: {pluginDirectory}");
        
       
        string configsDirectory = Path.Combine(Path.GetDirectoryName(pluginDirectory)!, "..", "configs");
        string configDirectory = Path.Combine(configsDirectory, ModuleName);
        string configPath = Path.Combine(configDirectory, "join_notifier_config.json");

        Server.PrintToConsole($"[DiscordJoinNotifier] Attempting to use config path: {configPath}");

        try
        {
           
            if (!Directory.Exists(configDirectory))
            {
                Server.PrintToConsole($"[DiscordJoinNotifier] Creating config directory: {configDirectory}");
                Directory.CreateDirectory(configDirectory);
            }

            if (!File.Exists(configPath))
            {
                Server.PrintToConsole($"[DiscordJoinNotifier] Config file does not exist, creating default config...");
                
                var defaultConfig = new JoinNotifierConfig();
                var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                
                File.WriteAllText(configPath, json);
                Server.PrintToConsole($"[DiscordJoinNotifier] Created new config at {configPath}");
                return defaultConfig;
            }

            var jsonText = File.ReadAllText(configPath);
            Server.PrintToConsole($"[DiscordJoinNotifier] Loaded existing config from {configPath}");
            
            var config = JsonSerializer.Deserialize<JoinNotifierConfig>(jsonText);
            return config ?? new JoinNotifierConfig();
        }
        catch (Exception ex)
        {
            Server.PrintToConsole($"[DiscordJoinNotifier] LoadConfig Exception: {ex}");
            Server.PrintToConsole($"[DiscordJoinNotifier] Stack trace: {ex.StackTrace}");
            
           
            try
            {
                string fallbackConfigPath = Path.Combine(pluginDirectory, "join_notifier_config.json");
                Server.PrintToConsole($"[DiscordJoinNotifier] Attempting fallback config path: {fallbackConfigPath}");
                
                var defaultConfig = new JoinNotifierConfig();
                var json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
                
                File.WriteAllText(fallbackConfigPath, json);
                Server.PrintToConsole($"[DiscordJoinNotifier] Created fallback config at {fallbackConfigPath}");
                return defaultConfig;
            }
            catch (Exception fallbackEx)
            {
                Server.PrintToConsole($"[DiscordJoinNotifier] Fallback config creation failed: {fallbackEx.Message}");
                return new JoinNotifierConfig();
            }
        }
    }
}

public class JoinNotifierConfig
{
    public string WebhookUrl { get; set; } = "https://discord.com/api/webhooks/YOUR_WEBHOOK_HERE";
    public string MessageFormat { get; set; } = "🔔 {player} (SteamID: {steamid}) joined the CS2 server.";
}