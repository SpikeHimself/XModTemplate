# XModTemplate

XModTemplate is a Valheim mod that serves as a template for new mods. It does nothing.

This README was generated using T4 Text Templating, and will contain invalid URLs until someone uses XModTemplate and updates the information in `XModTemplate\ModInfo.cs`.

# Features

This mod is a template for other mod makers. By itself, it does nothing. It exists only to make life easy.

### Copy me

You can use the [GitHub template function](https://github.com/SpikeHimself/XModTemplate/generate) to create your own mod based on XModTemplate, and then rename the project files and namespaces.
Make sure you find all occurances of "XModTemplate" and change them to your own mod's name!

Pro tip: Open the project in Visual Studio, right-click the namespace in one of the code files, and choose `Rename`. You'll still have to rename some files, though!

# Project overview

The XModTemplate project contains some nifty tricks which should help you save some time and effort. I'll try to explain them in detail below;

### Jotunn core

XModTemplate is based on the [JotunnModStub](https://github.com/Valheim-Modding/JotunnModStub) and much (but not all) of the documentation found here is a near one-to-one copy of that.
I strongly advise familiarising yourself with what Jotunn is and what it is capable of. You should read [JotunnModStub](https://github.com/Valheim-Modding/JotunnModStub)'s documentation to get a grasp of how it is set up, but remember that XModTemplate does vary from it slightly.

[JotunnModStub](https://github.com/Valheim-Modding/JotunnModStub) comes with a file called `DoPrebuild.props`. This file contains a configuration option titled `ExecutePrebuild`. If you set this to `true`, Jotunn will automatically generate publicised versions of the game DLLs for you, whenever they are missing.
XModTemplate has this value set to  `true` by default.

XModTemplate inherits some NuGet packages from [JotunnModStub](https://github.com/Valheim-Modding/JotunnModStub). After you open the project for the first time, Visual Studio will alert you to the fact that some of these packages are missing. This is normal, as the packages themselves aren't included in XModTemplate's GitHub repo. Just click "Restore", and then restart Visual Studio when it has finished.

### The path to Valheim

The following file can not be included in the GitHub repo, as it can contain information that is unique to the computer the project is being developed on.

You need to create this file manually in your Solution directory (where your .sln file lives). This file should be named `Environment.props`.

```
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <!-- Valheim install folder. This is normally found automatically, uncomment to overwrite it. Needs to be your path to the base Valheim folder. -->
    <!-- <VALHEIM_INSTALL>X:\PathToYourSteamLibary\steamapps\common\Valheim</VALHEIM_INSTALL> -->

    <!-- This is the folder where your build gets copied to when using the post-build automations -->
    <MOD_DEPLOYPATH>$(VALHEIM_INSTALL)\BepInEx\plugins</MOD_DEPLOYPATH>
  </PropertyGroup>
</Project>
```

In a typical setup, Jotunn will find the correct path for `VALHEIM_INSTALL` itself, but in case it fails, you can correct it in this file.

The `MOD_DEPLOYPATH` that this file defines is the location where the post-build automation script will copy your mod to when debugging.


### Build automation

Included in XModTemplate is a file called `publish.ps1`. This is the post-build automation [PowerShell](https://learn.microsoft.com/en-us/powershell/) script which will be executed automatically every time you build your project.

Inside your `XModTemplate\XModTemplate.csproj` file, you can find a [Target element](https://learn.microsoft.com/en-us/visualstudio/msbuild/target-element-msbuild?view=vs-2022) called `JotunnPostBuildTaskWin`. This is where the script is called and supplied with all the parameters that it needs.

Depending on the configuration you have chosen in Visual Studio, the script does one of two things:

#### Debug

1. Generate a .mdb file from your .pdb file using `libraries\Debug\pdb2mdb.exe`
2. Copy your .dll, .pdb and .mdb to the location defined by `MOD_DEPLOYPATH` in `Environment.props`
3. Copy those same files also to the `Package\Debug` directory
4. Generate a debug-release file at `Release\XModTemplate-debug-new.zip` using the contents of `Package\Debug`


#### Release

1. Copy all files (except generation templates) from `XModTemplate\SolutionDir` to the Solution directory. This includes the GitHub readme and issue templates as well as the Thunderstore documentation files.
2. Copy your .dll to `Package\Release\plugin\XModTemplate\`
3. Copy the Thunderstore icon at `Images\icon.png` to `Package\Release\`
4. Generate a release file at `Release\XModTemplate-new.zip`


### Documentation templates

Are you ready to get ridiculous? Because frankly this is ridiculous.

When I started modding in early 2023, one of the first things that really started to irritate me about the process, was that I had to maintain documentation in two different formats: bbcode for Nexus Mods and markdown for Thunderstore (and GitHub). I released a few versions just dealing with the fact I had to update all of my documentation twice, but I very quickly grew allergic to this. I still itch when I think about it.

Honestly, the alternative I came up with is not much better, because now I have to maintain an entire project (the one you're looking at). But at least it's beautiful, so there's that.

Well, now I could take you through my entire thought process here, and explain the reasoning behind the design choices, but if you're anything like my wife, you're probably already yawning at the mere thought, so let's just skip that part altogether. That said, I do want to add a quick summary, so at least we know what we're talking about;

The publish.ps1 post-build automation script will copy all contents from the SolutionDir directory, minus the templates, to the "actual" solution directory, so the structure here is important. Files generated outside of the SolutionDir directory are copied separately.

Here is an overview of which templates exist for file generation, and where they live:

For Thunderstore, we need:
1. A manifest json file: Docs\SolutionDir\Package\Release\manifest.tt
2. A readme markdown file: Docs\SolutionDir\Package\Release\README.tt
3. A changelog markdown file: Docs\SolutionDir\Package\Release\CHANGELOG.tt

The files generated by these templates are physically included in the release .zip which is uploaded to Thunderstore.

Nexus doesn't care about the contents of the .zip, and most mod managers don't either, so we just use the exact same file, for no reason other than laziness, er, I mean, efficiency. So for Nexus, all we have is:
1. A documentation text file formatted with bbcode: Docs\README.Nexus.tt

Then the third target is GitHub. For this, the project includes the following files:
1. The project's README.md markdown file: Docs\SolutionDir\README.tt
2. An issue template for bug reports: Docs\SolutionDir\.github\ISSUE_TEMPLATE\bug_report.tt
2. An issue template for feature requests: Docs\SolutionDir\.github\ISSUE_TEMPLATE\feature_request.tt


Now we know where files are and what they're for, let's dive into the formatting thing. That was kind of the point, after all. 

In order to write documentation only once, something somehow has to convert one format to the other. I don't know how you would achieve that, besides writing a seperate program that does it for you, or something to that extent. Clearly I didn't like that idea, so I made things worse and wrote my own format. Kind of..

ManyFormats is a library I created (yes, for this particular purpose, but you can use it for other stuff too if you're crazy enough), that lets you do exactly this. And in quite a simple manner (ish). It provides a bunch of functions that you can use to convert (or, indeed, format) text. You can load this library into your project and use it in your T4 templates. Now the beautiful part: documentation does not exist yet! Wait no, that's bad. I'll get to that soon, I promise!

Here's an example of how to use it:

<#= mf.Heading("Documentation templates", size: HeadingSize.Medium) #>

This creates the header you can see above the paragraphs you are currently reading.

# Download and installation instructions (for players)

There is no point installing this mod. It does nothing. The text below only serves as an example.

XModTemplate is a [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/) plugin. As such, you must have BepInEx installed. Most other Valheim mods are also BepInEx plugins, so chances are you already have this.

XModTemplate makes use of the [Jotunn](https://www.nexusmods.com/valheim/mods/1138) library, so you must install that before installing XModTemplate. If you do not install Jotunn, XModTemplate will simply not be loaded by your game and it will not work.

I very strongly recommend using a mod manager such as [Vortex](https://www.nexusmods.com/site/mods/1) or [r2modman](https://valheim.thunderstore.io/package/ebkr/r2modman/). They will take care of everything for you and you don't have to worry about which files go where. I recommend against manual installation.
1. Make sure you have [BepInEx](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/) installed.
2. Install [Jotunn](https://www.nexusmods.com/valheim/mods/1138).
3. On [Nexus Mods](https://www.nexusmods.com/valheim/mods/-1) click 'Mod manager download', or on [Thunderstore](https://valheim.thunderstore.io/package/SpikeHimself/XModTemplate/) click 'Install with Mod Manager'.



# Bugs and Feature Requests

To report a bug, please navigate to the [Issues page](https://github.com/SpikeHimself/XModTemplate/issues), click [New issue](https://github.com/SpikeHimself/XModTemplate/issues/new/choose), choose `Bug report`, and fill out the template.

For feature requests, choose `Feature request` on the [New issue](https://github.com/SpikeHimself/XModTemplate/issues/new/choose) page.


# Installation instructions (for developers)

I will soon write a guide to get XModTemplate working in your development environment. For now, you can probably figure some stuff out by having a look at the [JotunnModStub](https://github.com/Valheim-Modding/JotunnModStub) project that XModTemplate is based on. Please bear in mind that the information there might have changed since XModTemplate was created, and that XModTemplate itself may over time have diverted from the steps laid out there. Again, a guide will follow soon!


# I did more too!

Please have a look at my other mods too! 

[XPortal](https://www.nexusmods.com/valheim/mods/2239) lets you select a portal destination from a list of existing portals, so that you don't have to match portal tags anymore.

[XStorage](https://www.nexusmods.com/valheim/mods/2290) lets you open multiple chests at once, rename them, and move items/stacks to the most suitable chest.


# Support me

My mods are free and will remain free, for everyone to use, edit or learn from. I lovingly poured many hours of hard work into these projects. If you enjoy my mods and want to support my work, please consider buying me a coffee :)

[<img src="https://cdn.buymeacoffee.com/buttons/v2/default-yellow.png" height="40" align="right" />](https://www.buymeacoffee.com/SpikeHimself)
