using FrooxEngine;
using ResoniteModLoader;
using System.Diagnostics;

namespace ytdlpUpdater;

public class ytdlpUpdater : ResoniteMod
{
    public override string Name => "yt-dlp Updater";
    public override string Author => "Raidriar796";
    public override string Version => "1.3.0";
    public override string Link => "https://github.com/Raidriar796/yt-dlp-Updater";

    public static ModConfiguration Config;

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<bool> EnableUpdates =
            new ModConfigurationKey<bool>(
                "EnableUpdates",
                "Enable updates",
                () => true);

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<Branch> UpdateBranch =
            new ModConfigurationKey<Branch>(
                "UpdateBranch",
                "Update branch",
                () => Branch.Stable);

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<string> UpdateVersion =
            new ModConfigurationKey<string>(
                "UpdateVersion",
                "Custom update version",
                () => "stable@latest");

    public override void OnEngineInit()
    {
        Config = GetConfiguration();
        Config.Save(true);

        if (Engine.Current.Platform == Platform.Windows && !ModLoader.IsHeadless)
        {
            //Runs update on startup
            Update(Config.GetValue(UpdateBranch));

            //Runs update when updates are enabled
            EnableUpdates.OnChanged += (value) => { Update(Config.GetValue(UpdateBranch)); };

            //Runs update on branch change
            UpdateBranch.OnChanged += (value) => { Update(Config.GetValue(UpdateBranch)); };
        }
        else
        {
            Warn("This mod is only supported on Windows or Wine/Proton");
        }
    }

    public enum Branch
    {
        Stable,
        Nightly,
        Master,
        Custom
    }

    private static void Update(Branch SelectedBranch)
    {
        if (Config.GetValue(EnableUpdates))
        {
            if (File.Exists("RuntimeData/yt-dlp.exe"))
            {
                string versionArgs;

                switch (SelectedBranch)
                {
                    case Branch.Stable:
                    versionArgs = "--update-to stable@latest";
                    break;

                    case Branch.Nightly:
                    versionArgs = "--update-to nightly@latest";
                    break;

                    case Branch.Master:
                    versionArgs = "--update-to master@latest";
                    break;

                    case Branch.Custom:
                    versionArgs = "--update-to " + Config.GetValue(UpdateVersion);
                    break;

                    default:
                    versionArgs = "--update-to stable@latest";
                    break;
                }

                Process updateProcess = new Process();
                updateProcess.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
                updateProcess.StartInfo.Arguments = versionArgs;
                updateProcess.Start();
            }
            else
            {
                Error("yt-dlp.exe not found in RuntimeData folder, you may want to validate files or manually redownload yt-dlp");
            }
        }
    }
}
