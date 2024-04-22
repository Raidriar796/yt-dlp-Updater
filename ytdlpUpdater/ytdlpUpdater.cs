using FrooxEngine;
using ResoniteModLoader;
using System.Diagnostics;

namespace ytdlpUpdater;

public class ytdlpUpdater : ResoniteMod
{
    public override string Name => "yt-dlp Updater";
    public override string Author => "Raidriar796";
    public override string Version => "1.2.0";
    public override string Link => "https://github.com/Raidriar796/yt-dlp-Updater";

    public static ModConfiguration Config;

    [AutoRegisterConfigKey] public static readonly ModConfigurationKey<Branch> UpdateBranch =
            new ModConfigurationKey<Branch>(
                "UpdateBranch",
                "Update branch.",
                () => Branch.Stable);

    public override void OnEngineInit()
    {
        Config = GetConfiguration();
        Config.Save(true);

        if (Engine.Current.Platform == Platform.Windows)
        {
            //Runs update on startup
            Update(Config.GetValue(UpdateBranch));

            //Runs update on config change
            UpdateBranch.OnChanged += (value) => { Update(Config.GetValue(UpdateBranch)); };
        }
        else
        {
            Msg("This mod is only supported on Windows/Proton");
        }
    }

    private static String branchArgs = "";

    public enum Branch
    {
        Stable,
        Nightly,
        Master
    }

    private static void Update(Branch SelectedBranch)
    {
        if (File.Exists("RuntimeData/yt-dlp.exe"))
        {
            switch (SelectedBranch)
            {
                case Branch.Stable:
                branchArgs = "--update-to stable@latest";
                break;

                case Branch.Nightly:
                branchArgs = "--update-to nightly@latest";
                break;

                case Branch.Master:
                branchArgs = "--update-to master@latest";
                break;

                default:
                branchArgs = "--update-to stable@latest";
                break;
            }

            Process updateProcess = new Process();
            updateProcess.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
            updateProcess.StartInfo.Arguments = branchArgs;
            updateProcess.Start();
        }
        else
        {
            Msg("yt-dlp.exe not found in RuntimeData folder, you may want to validate files or manually redownload yt-dlp");
        }
    }
}
