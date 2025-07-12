# Discord Join Notifier Plugin

A CounterStrikeSharp plugin that sends Discord notifications when players join your CS2 server.

## Features

- 🔔 Sends a Discord webhook message when a player joins the server
- ⚙️ Fully configurable message format
- 📁 Automatic config file generation
- 🛠️ Easy setup and installation

## Requirements

- **CS2 Server** with CounterStrikeSharp (CSS) installed
- **Discord Webhook URL** (see setup instructions below)

## Installation

### Option 1: Download Pre-built Plugin

1. Download the latest release from the [Releases](https://github.com/AndiiCodes/cs2-DiscordJoinNotifierPlugin/releases) page
2. Extract the files to your server

### Option 2: Build from Source

1. **Clone the repository**:
   ```bash
   git clone https://github.com/AndiiCodes/cs2-DiscordJoinNotifierPlugin.git
   cd cs2-DiscordJoinNotifierPlugin
   ```

2. **Build the plugin**:
   ```bash
   dotnet restore
   dotnet build --configuration Release
   ```

3. **Locate the built files**:
   - `bin/Release/net8.0/DiscordJoinNotifier.dll`
   - `bin/Release/net8.0/DiscordJoinNotifier.deps.json`
   - `bin/Release/net8.0/DiscordJoinNotifier.pdb`

## Server Installation

1. **Create plugin directory**:
   ```
   csgo/addons/counterstrikesharp/plugins/DiscordJoinNotifier/
   ```

2. **Copy all files** to the plugin directory:
   ```
   csgo/addons/counterstrikesharp/plugins/DiscordJoinNotifier/
   ├── DiscordJoinNotifier.dll
   ├── DiscordJoinNotifier.deps.json
   ├── DiscordJoinNotifier.pdb
   └── .csplugin.json
   ```

3. **Restart your CS2 server**

## Configuration

### Step 1: Create Discord Webhook

1. Go to your Discord server
2. Right-click on the channel where you want notifications
3. Select **"Edit Channel"**
4. Go to **"Integrations"** → **"Webhooks"**
5. Click **"Create Webhook"**
6. Copy the webhook URL (it should look like: `https://discord.com/api/webhooks/123456789/abcdef...`)

### Step 2: Configure the Plugin

1. **Start your server** - if installed correctly, the plugin will automatically create a config file 
2. **Navigate to the config file**:
   ```
   csgo/addons/counterstrikesharp/configs/DiscordJoinNotifier/join_notifier_config.json
   ```

3. **Edit the config file**:
   ```json
   {
     "WebhookUrl": "https://discord.com/api/webhooks/YOUR_WEBHOOK_URL_HERE",
     "MessageFormat": "🔔 {player} (SteamID: {steamid}) joined the CS2 server."
   }
   ```

4. **Replace** `YOUR_WEBHOOK_URL_HERE` with your actual Discord webhook URL
5. **Customize** the message format if desired (use `{player}` and `{steamid}` placeholders)
6. **Save** the file

### Step 3: Test the Plugin

1. **Restart your server** or reload the plugin
2. **Join your server** to test the notification
3. **Check your Discord channel** for the join message

## Message Customization

You can customize the message format using these placeholders:

- `{player}` - Player's name
- `{steamid}` - Player's SteamID

Example message formats:
```json
"🎮 {player} connected to the server!"
"Welcome {player} (ID: {steamid}) to our CS2 server!"
"New player joined: {player}"
```



## Development

### Building

```bash
# Restore dependencies
dotnet restore

# Build debug version
dotnet build

# Build release version
dotnet build --configuration Release
```

### Project Structure

```
DiscordJoinNotifier/
├── DiscordJoinNotifier.cs      # Main plugin code
├── DiscordJoinNotifier.csproj  # Project file
├── .csplugin.json             # Plugin metadata
├── DiscordJoinNotifier.sln    # Solution file
└── README.md                  # This file
```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

- **Issues**: [GitHub Issues](https://github.com/AndiiCodes/cs2-DiscordJoinNotifierPlugin/issues)
- **Discord**: [Your Discord Server](https://discord.gg/hsFB2V32PK)

## Changelog

### v1.0.0
- Initial release
- Basic join notification functionality
- Automatic config generation
- Configurable message format

---

## to-do

- Notification when players leave the lobby (optional) 

---

 
**Repository**: [DiscordJoinNotifier](https://github.com/AndiiCodes/DiscordJoinNotifier)

