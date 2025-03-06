using System;

namespace PearlCalculatorLib.PearlCalculationLib.Utility
{

    public enum GameVersion {
        version_1_11_to_1_21_1,
        version_1_21_2_plus,
        unknown
    }

    public class GameVersionUtils {

        public static GameVersion TryParse(string s) {
            switch (s) {
                case "1.11-1.21.1":
                    return GameVersion.version_1_11_to_1_21_1;
                case "1.21.2+":
                    return GameVersion.version_1_21_2_plus;
                default:
                    return GameVersion.unknown;
            }
        }

        public static string ToString(GameVersion version) {
            switch (version) {
                case GameVersion.version_1_11_to_1_21_1:
                    return "1.11-1.21.1";
                case GameVersion.version_1_21_2_plus:
                    return "1.21.2+";
                default:
                    return "unknown";
            }
        }

    }

}