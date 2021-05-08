# PearlCalculatorBlazor

Just A Pearl Calculator Website Project

## Usages

This website supports multiple Browsers (Google, Edge, etc).

### Prerequisites

Please install the Dotnet Core runtime before proceeding.

- dotnet 5.0 (Link: https://dotnet.microsoft.com/download)

### PearlCalculationLib

Lib from /LegendsOfSky/PearlCalculatorCore (Link: https://github.com/LegendsOfSky/PearlCalculatorCore)

See more information from it

### How to use

Here is a tutorial about how to use

#### Set the FTL information

First, you need to set FTL information. Go to `Settings`. You can also import settings by clicking 'Import Settings'.

Here is the setting of the FTL.

| Name                        | Value Type   | Default Value     | Annotation                                                                                                              |
| --------------------------- | ------------ | ----------------- | ----------------------------------------------------------------------------------------------------------------------- |
| `North West TNT (X Axis)`   | double       | -0.884999990463257| `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `North West TNT (Y Axis)`   | double       | 170.5             | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `North West TNT (Z Axis)`   | double       | -0.884999990463257| `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `North East TNT (X Axis)`   | double       | 0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `North East TNT (Y Axis)`   | double       | 170.5             | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `North East TNT (Z Axis)`   | double       | -0.884999990463257| `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `South West TNT (X Axis)`   | double       | -0.884999990463257| `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `South West TNT (Y Axis)`   | double       | 170.5             | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `South West TNT (Z Axis)`   | double       | 0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `South East TNT (X Axis)`   | double       | 0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `South East TNT (Y Axis)`   | double       | 170.5             | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          |
| `South East TNT (Z Axis)`   | double       | 0.884999990463257 | `/data get entity @e[type=minecraft:tnt,limit=1]` or `/log tnt`and `/tick freeze` may help you                          | 
| `Pearl Y Coordinate`        | double       | 170.3472263892941 | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` and `/tick freeze` may help you                               | 
| `Pearl Y Momentum`          | double       | 0.2716278719434352| `/data get entity @e[type=minecraft:ender_pearl,limit=1]` and `/tick freeze` may help you                               | 
| `Default Red TNT Position`  | enum (select)| SouthEast         | Preset the position of the TNT duped from the red  array relative to the pearl without being changed by the slime block |
| `Default Blue TNT Position` | enum (select)| NorthWest         | Preset the position of the TNT duped from the blue array relative to the pearl without being changed by the slime block |

#### Set the Pearl and Destination information

Second, you need to set the pearl information and TNT/destination. Go to `General`. You can also import settings by clicking 'Import Settings'.

Here is the setting of the pearl information and TNT/destination.

| Name           | Value Type   | Default Value | Annotation                                                                                                              |
| -------------- | ------------ | ------------- | ----------------------------------------------------------------------------------------------------------------------- |
| `Pearl X`      | double       | 0             | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` or `/log stand `/tick freeze` may help you                    |
| `Pearl Z`      | double       | 0             | `/data get entity @e[type=minecraft:ender_pearl,limit=1]` or `/log stand `/tick freeze` may help you                    |
| `Destination X`| double       | 0             | This is the X-axis of where you are going                                                                               |
| `Destination Z`| double       | 0             | This is the Z-axis of where you are going                                                                               |
| `Max TNT`      | int          | 0             | The FTL max tnt amount of each side                                                                                     |
| `Direction`    | enum (radio) | North         | This will affect the pearl simulate direction. It will auto set by clicking the output of TNT amount                    |
| `Red TNT`      | int          | 0             | This is the Red side tnt which you store in the ROM of your FTL. It will auto set by clicking the output of TNT amount  |
| `Blue TNT`     | int          | 0             | This is the Blue side tnt which you store in the ROM of your FTL. It will auto set by clicking the output of TNT amount |

After setting the Destination information, you can click `Calculate TNT Amount` to output the permutations of the red/blue TNT.

You can click one of the outputs from the sidebar. It will auto input the `Direction`, `Red TNT`, and `Blue TNT`.

#### Set the output order

Then, we can set the order of output. Go to `Advanced`.

Don't forget to click 

Here is the setting of the order.

| Name         | Value Type      | Default Value | annotation                                                                                                              |
| ------------ | --------------- | ------------- | --------------------------------------------------------------------------- |
| `X`          | double (-1<X<1) | 0             | The offset between ender pearl x coordinate and lava pool center coordinate |
| `Z`          | double (-1<Z<1) | 0             | The offset between ender pearl z coordinate and lava pool center coordinate |
| `TNT Weight` | int             | 0             | The order of the output                                                     |

Click on the result of the amount you want.

Finally, click `Pearl Simulate` to see the Pearl flight path.
