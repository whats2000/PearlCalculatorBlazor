# PearlCalculatorBlazor

Just A Pearl Calculator Website Project

![GitHub](https://img.shields.io/github/license/whats2000/PearlCalculatorBlazor)

[简体中文](README_zh-CN.md) | [繁體中文](README_zh-TW.md)

## Usages

This website supports multiple Browsers (Google, Edge, etc.).

### Prerequisites

Please install the Dotnet Core runtime before proceeding.

- dotnet 6.0 (Link: https://dotnet.microsoft.com/download)

### PearlCalculationLib

Lib from /LegendsOfSky/PearlCalculatorCore (Link: https://github.com/LegendsOfSky/PearlCalculatorCore)

See more information from it

## Supported Language

- English
- Español
- 繁體中文
- 简体中文

## How to use

Here is a tutorial about how to use

### Set the FTL information

First, you need to set FTL information. Go to `Settings`. You can also import settings by clicking `Import Settings`.

Here is the setting of the FTL.

| Name                          | Value Type | Default Value      | Annotation                                                                                                                                                                                  |
|-------------------------------|------------|--------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Game Version`                | enum       | 1.11-1.21.1        | The minecraft version that the cannon was designed for                                                                                                                                      |
| `North West TNT (X Axis)`     | double     | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `North West TNT (Y Axis)`     | double     | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `North West TNT (Z Axis)`     | double     | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `North East TNT (X Axis)`     | double     | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `North East TNT (Y Axis)`     | double     | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `North East TNT (Z Axis)`     | double     | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `South West TNT (X Axis)`     | double     | -0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `South West TNT (Y Axis)`     | double     | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `South West TNT (Z Axis)`     | double     | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `South East TNT (X Axis)`     | double     | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `South East TNT (Y Axis)`     | double     | 170.5              | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `South East TNT (Z Axis)`     | double     | 0.884999990463257  | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` and `/tick step 1` may help you                                                                           |
| `Pearl Y Motion Mode`         | enum       | Normal Projection  | Determines if the Y motion cancellation technique is used, in the website, select Normal Projection means don't use Y motion cancellation technique otherwise use it                        |
| `Pearl Y Coordinate`          | double     | 170.3472263892941  | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` and `/tick freeze` and `/tick step 1` may help you                                                                                |
| `Pearl Y Momentum`            | double     | 0.2716278719434352 | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` and `/tick freeze` and `/tick step 1` may help you                                                                                |
| `Pearl Y Position (Original)` | double     | 170.34722638929412 | Remove the piston used to cancel Y motion and get the pearl's Y position at the explosion tick, this setting only show when `Pearl Y Motion Mode` is set to `Perfect Horizontal Projection` |
| `Pearl Y Position (Adjusted)` | double     | 170.0              | Place the piston used to cancel Y motion and get the pearl's Y position at the end of the tick, this setting only show when `Pearl Y Motion Mode` is set to `Perfect Horizontal Projection` |
| `Pearl Y Coordinate`          | double     | 170.3472263892941  | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` and `/tick freeze` and `/tick step 1` may help you                                                                                |
| `Pearl Y Momentum`            | double     | 0.2716278719434352 | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` and `/tick freeze` and `/tick step 1` may help you                                                                                |
| `Default Red TNT Position`    | enum       | SouthEast          | Preset the position of the TNT duped from the red  array relative to the pearl without being changed by the slime block                                                                     |
| `Default Blue TNT Position`   | enum       | NorthWest          | Preset the position of the TNT duped from the blue array relative to the pearl without being changed by the slime block                                                                     |

### Set the Pearl and Destination information

Second, you need to set the pearl information and TNT/destination. Go to `General`. You can also import settings by
clicking `Import Settings`.

Here is the setting of the pearl information and TNT/destination.

| Name            | Value Type | Default Value | Annotation                                                                                                                 |
|-----------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------|
| `Pearl X`       | double     | 0             | `/data get entity @e[type=minecraft:ender_pearl,limit=1]`  `/tick freeze` and `/tick step 1` may help you                  |
| `Pearl Z`       | double     | 0             | `/data get entity @e[type=minecraft:ender_pearl,limit=1]`, `/tick freeze` and `/tick step 1` may help you                  |
| `Destination X` | double     | 0             | This is the X-axis of where you are going                                                                                  |
| `Destination Z` | double     | 0             | This is the Z-axis of where you are going                                                                                  |
| `Max TNT`       | int        | 0             | The FTL max tnt amount of each side                                                                                        |
| `Direction`     | enum       | North         | This will affect the pearl simulation direction. It will be auto set by clicking the output of TNT amount                  |
| `Red TNT`       | int        | 0             | This is the Red side tnt which you store in the ROM of your FTL. It will be auto set by clicking the output of TNT amount  |
| `Blue TNT`      | int        | 0             | This is the Blue side tnt which you store in the ROM of your FTL. It will be auto set by clicking the output of TNT amount |

After setting the Destination information, you can click `Calculate TNT Amount` to output the list of the result.

You can click one of the outputs from the sidebar. It will auto input the `Direction`, `Red TNT`, and `Blue TNT`.

### Set the output order

Then, we can set the order of output. Go to `Advanced`.

Here is the setting of the order.

| Name                  | Value Type | Default Value           | Annotation                                                                  |
|-----------------------|------------|-------------------------|-----------------------------------------------------------------------------|
| `X`                   | double     | 0 (-1<X<1)              | The offset between ender pearl x coordinate and lava pool center coordinate |
| `Z`                   | double     | 0 (-1<Z<1)              | The offset between ender pearl z coordinate and lava pool center coordinate |
| `TNT Weight`          | int        | 0                       | The order of the output                                                     |
| `Result Sort Control` | enum       | Mixed Weighted Distance | The order of the output, open graph mode to see the result visually         |

Click on the result of the amount you want.

Finally, click `Pearl Simulate` to see the Pearl flight path.

### Additional Tools

You can go to the `TNT Encoding` page to encode the TNT amount to the FTL ROM.

| Name                             | Value Type | Default Value | Annotation                                                                                                                                                                             |
|----------------------------------|------------|---------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Red TNT Encoding Configuration`  | string     |               | The encoding of the Red TNT amount. In the website input, use `,` to separate the number. For example, `1, 2, 4, 8, 10, 20, 40, 80, 160`, this wlll convert to `int[] in calculation`  |
| Blue TNT Encoding Configuration` | string     |               | The encoding of the Blue TNT amount. In the website input, use `,` to separate the number. For example, `1, 2, 4, 8, 10, 20, 40, 80, 160`, this wlll convert to `int[] in calculation` |

Click the `Calculate TNT Encoding Combination` to get the result.

### Export and Import

You can export and import the settings by clicking the `Export Settings` and `Import Settings` under the `General` page.

### Graphical Output

You can click the `Graph Mode` to see the graphical output.

**This is a new feature that is still under development. Feel free open an issue if you have any suggestions.**

## Swap Cannon Settings

On the top you can see the Swap Icon with a following name of a cannon. Click on it to swap the cannon settings.

In the edit sidebar, you can easily

- Change the cannon name.
- Delete the cannon.
- Add a new cannon.
- Reset the cannon.
- Copy the cannon settings.

**This is a new feature that is still under development. Feel free open an issue if you have any suggestions.**

### SVG Icon Reference

- [Moon Dark Theme SVG Vector](https://www.svgrepo.com/svg/304625/moon-dark-theme): Under CC0 License
- [Sun Light Theme SVG Vector](https://www.svgrepo.com/svg/304624/sun-light-theme): Under CC0 License
- [Coordinates SVG Vector](https://www.svgrepo.com/svg/95272/coordinates): Under CC0 License
