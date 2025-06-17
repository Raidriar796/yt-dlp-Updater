# yt-dlp Updater
 
A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) mod for [Resonite](https://resonite.com/) that runs the built in update function for yt-dlp on startup, includes option to change the update branch.

This mod is now deprecated as of Resonite version `2025.6.17.623` 

**Only the Windows graphical cilent is supported, including Wine/Proton.**

## Requirements
- [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) 2.5.0 or later
- [ResoniteModSettings](https://github.com/badhaloninja/ResoniteModSettings) for changing update branch in Resonite

## Installation
1. Install [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader).
2. Place [ytdlpUpdater.dll](https://github.com/Raidriar796/yt-dlp-Updater/releases/latest/download/ytdlpUpdater.dll) into your `rml_mods` folder. This folder should be at `C:\Program Files (x86)\Steam\steamapps\common\Resonite\rml_mods` for a default install. You can create it if it's missing, or if you launch the game once with ResoniteModLoader installed it will create the folder for you.
3. Start the game. If you want to verify that the mod is working you can check your Resonite logs.

## How does it work?

Resonite comes bundled with yt-dlp in the `RuntimeTools` folder, this .exe is instantiated everytime a video link is pasted into Resonite so the video can be streamed through Resonite.

yt-dlp has a built in update function and because it's installed as a standalone .exe, it can be called upon with the update arguments. In the case of this mod, the update function is ran when the engine starts and when the config changes.

Since the .exe is instantiated, it can be updated during runtime and you can start using the updated version immediately.

## Why does a terminal appear everytime Resonite starts?

yt-dlp has an embedded python runtime which always opens a terminal when called upon through this mod. A modified version of yt-dlp would be needed to not have a terminal open.

## How do I go back to the yt-dlp version released with Resonite?

The mod currently has no mechanism to rollback to the version released with Resonite, as it's periodically updated and I'd rather avoid hard coding a specific version of yt-dlp into the mod. There's 2 ways you can manually downgrade:
1. Change the "Custom update version" to the specific version that's released with Resonite at the time
2. Delete the yt-dlp.exe and validate files with Steam.
