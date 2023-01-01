using System.Collections;
using System.Collections.Generic;

public class EndGameChecker
{
    List<GamePosition> TruthTable = new List<GamePosition>();

    public bool isGameOver(GamePosition gamePosition) {
        return TruthTable.Contains(gamePosition);
    }

    public EndGameChecker() {
        // https://en.wikipedia.org/wiki/L_game#/media/File:L_Game_all_final_positions.svg
        // + each rotation and mirror
        // in these positions, the p1 just played and wins
        TruthTable.Add(new GamePosition(32772, 1136, 28928));
        TruthTable.Add(new GamePosition(2064, 1604, 28928));
        TruthTable.Add(new GamePosition(18, 3616, 28928));
        TruthTable.Add(new GamePosition(18, 3712, 28928));
        TruthTable.Add(new GamePosition(32776, 1136, 28928));
        TruthTable.Add(new GamePosition(32784, 1604, 28928));
        TruthTable.Add(new GamePosition(20, 3616, 28928));
        TruthTable.Add(new GamePosition(20, 3712, 28928));
        TruthTable.Add(new GamePosition(32896, 1136, 28928));
        TruthTable.Add(new GamePosition(33792, 116, 28928));
        TruthTable.Add(new GamePosition(2064, 1570, 28928));
        TruthTable.Add(new GamePosition(528, 3140, 28928));
        TruthTable.Add(new GamePosition(132, 18176, 113));
        TruthTable.Add(new GamePosition(260, 3712, 113));
        TruthTable.Add(new GamePosition(288, 3140, 57856));
        TruthTable.Add(new GamePosition(4098, 736, 59392));
        TruthTable.Add(new GamePosition(4097, 736, 59392));
        TruthTable.Add(new GamePosition(4112, 736, 59392));
        TruthTable.Add(new GamePosition(384, 1570, 59392));
        TruthTable.Add(new GamePosition(4224, 1570, 59392));
        TruthTable.Add(new GamePosition(4608, 226, 59392));
        TruthTable.Add(new GamePosition(132, 1856, 59392));
        TruthTable.Add(new GamePosition(130, 1856, 59392));
        TruthTable.Add(new GamePosition(384, 1604, 59392));
        TruthTable.Add(new GamePosition(132, 1808, 59392));
        TruthTable.Add(new GamePosition(130, 1808, 59392));
        TruthTable.Add(new GamePosition(1152, 802, 59392));
        TruthTable.Add(new GamePosition(18, 11776, 232));
        TruthTable.Add(new GamePosition(2050, 1808, 232));
        TruthTable.Add(new GamePosition(2112, 802, 29696));
        TruthTable.Add(new GamePosition(10, 8800, 51328));
        TruthTable.Add(new GamePosition(9, 8800, 51328));
        TruthTable.Add(new GamePosition(24, 8800, 51328));
        TruthTable.Add(new GamePosition(8196, 1136, 51328));
        TruthTable.Add(new GamePosition(8200, 1136, 51328));
        TruthTable.Add(new GamePosition(72, 8752, 51328));
        TruthTable.Add(new GamePosition(8448, 1604, 51328));
        TruthTable.Add(new GamePosition(8208, 1604, 51328));
        TruthTable.Add(new GamePosition(8196, 1856, 51328));
        TruthTable.Add(new GamePosition(8448, 1094, 51328));
        TruthTable.Add(new GamePosition(8208, 1094, 51328));
        TruthTable.Add(new GamePosition(9216, 116, 51328));
        TruthTable.Add(new GamePosition(18, 17600, 12832));
        TruthTable.Add(new GamePosition(16400, 1094, 12832));
        TruthTable.Add(new GamePosition(16896, 116, 3208));
        TruthTable.Add(new GamePosition(5, 17504, 12560));
        TruthTable.Add(new GamePosition(9, 17504, 12560));
        TruthTable.Add(new GamePosition(33, 17600, 12560));
        TruthTable.Add(new GamePosition(16385, 1248, 12560));
        TruthTable.Add(new GamePosition(16386, 1248, 12560));
        TruthTable.Add(new GamePosition(16386, 736, 12560));
        TruthTable.Add(new GamePosition(16386, 3616, 12560));
        TruthTable.Add(new GamePosition(16512, 1570, 12560));
        TruthTable.Add(new GamePosition(18432, 1570, 12560));
        TruthTable.Add(new GamePosition(16896, 226, 12560));
        TruthTable.Add(new GamePosition(16512, 550, 12560));
        TruthTable.Add(new GamePosition(18432, 550, 12560));
        TruthTable.Add(new GamePosition(9216, 226, 785));
        TruthTable.Add(new GamePosition(8320, 550, 50240));
        TruthTable.Add(new GamePosition(132, 8752, 50240));
        TruthTable.Add(new GamePosition(1152, 8752, 71));
        TruthTable.Add(new GamePosition(2112, 8752, 142));
        TruthTable.Add(new GamePosition(2064, 17504, 142));
        TruthTable.Add(new GamePosition(33, 11776, 142));
        TruthTable.Add(new GamePosition(257, 3616, 142));
        TruthTable.Add(new GamePosition(8320, 368, 36352));
        TruthTable.Add(new GamePosition(10240, 368, 142));
        TruthTable.Add(new GamePosition(10240, 1136, 142));
        TruthTable.Add(new GamePosition(2049, 8800, 142));
        TruthTable.Add(new GamePosition(4097, 3616, 142));
        TruthTable.Add(new GamePosition(8448, 226, 36352));
        TruthTable.Add(new GamePosition(18432, 368, 142));
        TruthTable.Add(new GamePosition(18432, 1136, 142));
        TruthTable.Add(new GamePosition(2064, 8800, 142));
        TruthTable.Add(new GamePosition(8193, 3616, 142));
        TruthTable.Add(new GamePosition(2056, 1856, 23));
        TruthTable.Add(new GamePosition(72, 18176, 23));
        TruthTable.Add(new GamePosition(384, 8800, 23));
        TruthTable.Add(new GamePosition(288, 8896, 23));
        TruthTable.Add(new GamePosition(528, 17600, 46));
        TruthTable.Add(new GamePosition(32776, 1856, 23));
        TruthTable.Add(new GamePosition(4104, 17504, 23));
        TruthTable.Add(new GamePosition(16640, 736, 23));
        TruthTable.Add(new GamePosition(16640, 2272, 23));
        TruthTable.Add(new GamePosition(16400, 2272, 5888));
        TruthTable.Add(new GamePosition(16392, 1856, 23));
        TruthTable.Add(new GamePosition(384, 17504, 23));
        TruthTable.Add(new GamePosition(8448, 736, 23));
        TruthTable.Add(new GamePosition(8448, 2272, 23));
        TruthTable.Add(new GamePosition(18432, 116, 5888));
        TruthTable.Add(new GamePosition(20480, 1604, 275));
        TruthTable.Add(new GamePosition(36864, 1604, 275));
        TruthTable.Add(new GamePosition(4608, 3140, 275));
        TruthTable.Add(new GamePosition(4100, 3616, 275));
        TruthTable.Add(new GamePosition(8196, 3616, 275));
        TruthTable.Add(new GamePosition(8196, 3616, 275));
        TruthTable.Add(new GamePosition(8196, 864, 275));
        TruthTable.Add(new GamePosition(2052, 8800, 275));
        TruthTable.Add(new GamePosition(132, 8800, 275));
        TruthTable.Add(new GamePosition(36, 11776, 275));
        TruthTable.Add(new GamePosition(2052, 25120, 275));
        TruthTable.Add(new GamePosition(132, 25120, 275));
        TruthTable.Add(new GamePosition(66, 11776, 4400));
        TruthTable.Add(new GamePosition(2050, 25120, 1100));
        TruthTable.Add(new GamePosition(18432, 802, 1100));
        TruthTable.Add(new GamePosition(33024, 1570, 2188));
        TruthTable.Add(new GamePosition(36864, 1570, 2188));
        TruthTable.Add(new GamePosition(40960, 1570, 2188));
        TruthTable.Add(new GamePosition(16386, 1856, 2188));
        TruthTable.Add(new GamePosition(32770, 1856, 2188));
        TruthTable.Add(new GamePosition(33792, 802, 2188));
        TruthTable.Add(new GamePosition(18, 17504, 2188));
        TruthTable.Add(new GamePosition(258, 17504, 2188));
        TruthTable.Add(new GamePosition(16386, 1136, 2188));
        TruthTable.Add(new GamePosition(18, 25664, 2188));
        TruthTable.Add(new GamePosition(258, 25664, 2188));
        TruthTable.Add(new GamePosition(66, 18176, 2188));
        TruthTable.Add(new GamePosition(8448, 3140, 547));
        TruthTable.Add(new GamePosition(260, 25664, 547));
        TruthTable.Add(new GamePosition(36, 18176, 35008));
    }
}