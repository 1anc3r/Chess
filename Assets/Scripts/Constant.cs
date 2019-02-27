// 游戏状态
public enum GameStatus
{
    Start = 0,  // 游戏开始

    Pick,       // 举棋
    Move,       // 落子
    Switch,     // 阵营转换

    End,        // 游戏结束
};

// 阵营转换
public enum GameSwitch
{
    White = -1, // 白方

    Undefined,

    Black,      // 黑方
};

// 棋盘坐标类型
public enum CoordType
{
    Pos = 0,    // 抽象坐标
    Vec = 1,    // 实际坐标
};

// 方格类型
public enum SquareType
{
    Undefined = 0,

    White,      // 白方格
    Black,      // 黑方格
};

// 方格染色
public enum SquareTint
{
    Undefined = 0,

    Pick,       // 起点
    Pass,       // 可行
    Kill,       // 可杀
};

// 棋子名字
public class PieceName
{
    public const string WhiteCenterKing = "White Center King";
    public const string WhiteCenterQueen = "White Center Queen";
    public const string WhiteLeftBishop = "White Left Bishop";
    public const string WhiteRightBishop = "White Right Bishop";
    public const string WhiteLeftKnight = "White Left Knight";
    public const string WhiteRightKnight = "White Right Knight";
    public const string WhiteLeftRook = "White Left Rook";
    public const string WhiteRightRook = "White Right Rook";
    public const string WhitePawnA = "White Pawn A";
    public const string WhitePawnB = "White Pawn B";
    public const string WhitePawnC = "White Pawn C";
    public const string WhitePawnD = "White Pawn D";
    public const string WhitePawnE = "White Pawn E";
    public const string WhitePawnF = "White Pawn F";
    public const string WhitePawnG = "White Pawn G";
    public const string WhitePawnH = "White Pawn H";

    public const string BlackCenterKing = "Black Center King";
    public const string BlackCenterQueen = "Black Center Queen";
    public const string BlackLeftBishop = "Black Left Bishop";
    public const string BlackRightBishop = "Black Right Bishop";
    public const string BlackLeftKnight = "Black Left Knight";
    public const string BlackRightKnight = "Black Right Knight";
    public const string BlackLeftRook = "Black Left Rook";
    public const string BlackRightRook = "Black Right Rook";
    public const string BlackPawnA = "Black Pawn A";
    public const string BlackPawnB = "Black Pawn B";
    public const string BlackPawnC = "Black Pawn C";
    public const string BlackPawnD = "Black Pawn D";
    public const string BlackPawnE = "Black Pawn E";
    public const string BlackPawnF = "Black Pawn F";
    public const string BlackPawnG = "Black Pawn G";
    public const string BlackPawnH = "Black Pawn H";
};

// 棋子类型
public enum PieceType
{
    WhiteKing = 0,      // 白王
    WhiteQueen,         // 白皇后
    WhiteBishop,        // 白主教
    WhiteKnight,        // 白骑士
    WhiteRook,          // 白车堡
    WhitePawn,          // 白卒

    Undefined = 6,

    BlackKing,          // 黑王
    BlackQueen,         // 黑皇后
    BlackBishop,        // 黑主教
    BlackKnight,        // 黑骑士
    BlackRook,          // 黑车堡
    BlackPawn,          // 黑卒
};