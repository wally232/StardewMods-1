﻿**Instant Grow Trees** is a [Stardew Valley](http://stardewvalley.net/) mod which causes trees to
grow instantly overnight if they have enough space.

## Contents
* [Install](#install)
* [Use](#use)
* [Configure](#configure)
* [Compatibility](#compatibility)
* [See also](#see-also)

## Install
1. [Install the latest version of SMAPI](https://smapi.io).
2. Install [this mod from Nexus mods](https://www.nexusmods.com/stardewvalley/mods/173).
3. Run the game using SMAPI.

## Use
Just install the mod and play the game. Any saplings or planted tree seeds that have enough space
will grow instantly overnight.

## Configure
### In-game settings
If you have [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098) installed,
you can click the cog button (⚙) on the title screen or the "mod options" button at the bottom of
the in-game menu to configure the mod. Hover the cursor over a field for details, or see the next
section.

![](screenshots/generic-config-menu.png)

### `config.json` file
The mod creates a `config.json` file the first time you run it. You can open the file in a text
editor to configure the mod:

group           | setting                    | effect
:-------------- | :------------------------- | :-----
`FruitTrees`    | `InstantlyAge`             | Whether fruit trees age instantly to iridium quality overnight. Default `false`.
&nbsp;          | `InstantlyGrow`            | Whether fruit trees grow instantly overnight. Default `true`.
&nbsp;          | `InstantlyGrowInWinter`    | Whether fruit trees also grow instantly in winter. Default `true`.
&nbsp;          | `InstantlyGrowWhenInvalid` | Whether fruit trees also grow instantly even if they normally wouldn't grow (e.g. too close to another tree). Default `false`.
`NonFruitTrees` | `InstantlyGrow`            | Whether non-fruit trees grow instantly overnight. Default `true`.
&nbsp;          | `InstantlyGrowInWinter`    | Whether non-fruit trees also grow instantly in winter. Default `true`.
&nbsp;          | `InstantlyGrowWhenInvalid` | Whether non-fruit trees also grow instantly even if they normally wouldn't grow (e.g. too close to another tree). Default `false`.

## Compatibility
* Works with Stardew Valley 1.5.5+ on Linux/Mac/Windows.
* Works in single-player, multiplayer, and split-screen mode.
* No known mod conflicts.

## See also
* [Release notes](release-notes.md)
* [Nexus mod](https://www.nexusmods.com/stardewvalley/mods/173)
