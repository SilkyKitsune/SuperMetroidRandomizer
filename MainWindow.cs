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

    private const string InvalidFile = "INVALID FILE", InvalidPath = "INVALID PATH", Ext = ".sfc", FileName = "SMMIR_",
        BombBlockFix = "IPS\\BombBlockFix.ips", DropBlockFix = "IPS\\DropBlockFix.ips", PowerDoorFix = "IPS\\PowerDoorFix.ips", TourianShort = "IPS\\TourianShort.ips";

    public MainWindow() => InitializeComponent();

    private void generateButton_Click(object sender, EventArgs e) => GenerateButton();

    private void ipsCheckBox_CheckedChanged(object sender, EventArgs e) => IPSCheckBox();

    private void outputFolderButton_Click(object sender, EventArgs e) => OutputFolderButton();

    private void romPathButton_Click(object sender, EventArgs e) => RomPathButton();
    
    private void GenerateButton()
    {
        if (!F.Exists(BombBlockFix))
        {
            Close();
            return;
        }

        bool ips = ipsCheckBox.Checked, torizoNoSpeedBooster = torizoCheckBox.Checked;
        string romPath = romPathTextBox.Text, folderPath = outputFolderTextBox.Text, seedText = seedTextBox.Text;
        byte[] rom = null;

        if (!ips)
        {
            if (!F.Exists(romPath))
            {
                romPathTextBox.Text = InvalidPath;
                return;
            }

            rom = F.ReadAllBytes(romPath);
            if (rom.Length != RomSize)
            {
                romPathTextBox.Text = InvalidFile;
                return;
            }
        }

        if (!D.Exists(folderPath))
        {
            outputFolderTextBox.Text = InvalidPath;
            return;
        }

        generateButton.Enabled = false;

        int seed = int.TryParse(seedText, out int i) ? i : (!string.IsNullOrEmpty(seedText) ? seedText.GetHashCode() : 0);
        IPS patch = new(BombBlockFix);
        patch.Add(new IPS(DropBlockFix), false);
        patch.Add(new IPS(PowerDoorFix), false);
        patch.Add(new IPS(TourianShort), false);
        SM.Generate(ref patch, ref seed, out string spoiler, torizoNoSpeedBooster);

        string outPath = P.Combine(folderPath, FileName + seed);
        F.WriteAllText(outPath + "_Spoiler.txt", spoiler);
        if (ips) patch.WritePatch(outPath);
        else
        {
            patch.Apply(rom);
            F.WriteAllBytes(P.Combine(folderPath, outPath + Ext), rom);
        }

        seedTextBox.Text = seed.ToString();
        generateButton.Enabled = true;
    }

    private void IPSCheckBox()
    {
        bool ips = ipsCheckBox.Checked;
        romPathTextBox.Enabled = !ips;
        romPathButton.Enabled = !ips;
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