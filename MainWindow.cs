using System;
using F = System.IO.File;
using D = System.IO.Directory;
using P = System.IO.Path;
using System.Windows.Forms;
using IPSLib;

namespace SuperMetroidRandomizer;

public partial class MainWindow : Form
{
    private const int RomSize = 0x300000;

    private const string InvalidFile = "INVALID FILE", InvalidPath = "INVALID PATH", Ext = ".sfc", FileName = "SMMIR_";

    private static readonly string[] Paths =
    {
        "LandingSitePowerDoorFix.ips",
        "PirateShaftDropBlocksFix.ips",
        "TorizoBombBlocksFix.ips",
        "TourianShort.ips",
    };

    public MainWindow()
    {
        InitializeComponent();

        string invalidPaths = "";
        foreach (string path in Paths)
            if (IPS.TryRead(out IPS ips, path)) patches.Add(ips, MergeMode.None);
            else invalidPaths += path + '\n';

        if (invalidPaths.Length > 0)
        {
            outputFolderTextBox.Enabled = false;
            outputFolderButton.Enabled = false;
            seedTextBox.Enabled = false;
            torizoCheckBox.Enabled = false;
            generateButton.Enabled = false;
            seedTextBox.Text = outputFolderTextBox.Text = "INVALID PATCHES";//temp
            MessageBox.Show("These patches could not be loaded they may be missing or corrupt:\n" + invalidPaths, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private readonly IPS patches = new();

    private void generateButton_Click(object sender, EventArgs e) => GenerateButton();

    private void outputFolderButton_Click(object sender, EventArgs e) => OutputFolderButton();

    private void romPathButton_Click(object sender, EventArgs e) => RomPathButton();
    
    private void GenerateButton()
    {
        IPS patch = new();
        patch.Add(patches, MergeMode.None);

        bool torizoNoSpeedBooster = torizoCheckBox.Checked;
        string folderPath = outputFolderTextBox.Text, seedText = seedTextBox.Text;

        if (!D.Exists(folderPath))
        {
            outputFolderTextBox.Text = InvalidPath;
            return;
        }

        generateButton.Enabled = false;

        int seed = int.TryParse(seedText, out int i) ? i : (!string.IsNullOrEmpty(seedText) ? seedText.GetHashCode() : 0);
        
        SM.Generate(ref patch, ref seed, out string spoiler, torizoNoSpeedBooster);

        string outPath = P.Combine(folderPath, FileName + seed);
        F.WriteAllText(outPath + "_Spoiler.txt", spoiler);
        patch.WritePatch(outPath);

        seedTextBox.Text = seed.ToString();
        generateButton.Enabled = true;
    }

    private void OutputFolderButton()
    {
        folderBrowserDialog.ShowDialog();
        outputFolderTextBox.Text = folderBrowserDialog.SelectedPath;
    }

    private void RomPathButton()
    {
        openFileDialog.ShowDialog();
        romPathTextBox.Text = openFileDialog.FileName;
    }
}