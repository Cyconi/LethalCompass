using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: AssemblyTitle(LethalCompanyCompass.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(LethalCompanyCompass.BuildInfo.Company)]
[assembly: AssemblyProduct(LethalCompanyCompass.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + LethalCompanyCompass.BuildInfo.Author)]
[assembly: AssemblyTrademark(LethalCompanyCompass.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(LethalCompanyCompass.BuildInfo.Version)]
[assembly: AssemblyFileVersion(LethalCompanyCompass.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(LethalCompanyCompass.LethalCompanyCompass), LethalCompanyCompass.BuildInfo.Name, LethalCompanyCompass.BuildInfo.Version, LethalCompanyCompass.BuildInfo.Author, LethalCompanyCompass.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]