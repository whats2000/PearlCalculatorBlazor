namespace PearlCalculatorLib.PearlCalculationLib.World
{

    public enum GameVersion {
        Version111To1211,
        Version1212Plus,
        Unknown
    }

    public static class GameVersionUtils {

        public static GameVersion TryParse(string s) {
            switch (s) {
                case "1.11-1.21.1":
                    return GameVersion.Version111To1211;
                case "1.21.2+":
                    return GameVersion.Version1212Plus;
                default:
                    return GameVersion.Unknown;
            }
        }

        public static string ToString(GameVersion version) {
            switch (version) {
                case GameVersion.Version111To1211:
                    return "1.11-1.21.1";
                case GameVersion.Version1212Plus:
                    return "1.21.2+";
                default:
                    return "unknown";
            }
        }
    }
}