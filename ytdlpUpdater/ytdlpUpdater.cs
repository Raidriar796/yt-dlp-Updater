using ResoniteModLoader;
using System.Diagnostics;

namespace ytdlpUpdater;

public class ytdlpUpdater : ResoniteMod
{
    public override string Name => "yt-dlp Updater";
    public override string Author => "Raidriar796";
    public override string Version => "1.1.0";
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

        //Runs update on startup
        Update(Config.GetValue(UpdateBranch));

        //Runs update on config change
        UpdateBranch.OnChanged += (value) => { Update(Config.GetValue(UpdateBranch)); };
    }

    public enum Branch
    {
        Stable,
        Nightly,
        Master
    }

    private static void Update(Branch SelectedBranch)
    {
        switch (SelectedBranch)
        {
            case Branch.Stable:
            Process process1 = new Process();
            process1.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
            process1.StartInfo.Arguments = "--update-to stable@latest";
            process1.Start();
            break;

            case Branch.Nightly:
            Process process2 = new Process();
            process2.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
            process2.StartInfo.Arguments = "--update-to nightly@latest";
            process2.Start();
            break;

            case Branch.Master:
            Process process3 = new Process();
            process3.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
            process3.StartInfo.Arguments = "--update-to master@latest";
            process3.Start();
            break;

            default:
            Process process4 = new Process();
            process4.StartInfo.FileName = "RuntimeData/yt-dlp.exe";
            process4.StartInfo.Arguments = "--update-to stable@latest";
            process4.Start();
            break;
        }
    }
}
