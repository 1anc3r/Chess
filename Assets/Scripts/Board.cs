using System.Collections.Generic;
using UnityEngine;

// 棋盘对象
public class Board
{
    private int stepCount = 0;                                      // 总步数
    private int stepSize = 0;                                       // 单步长
    private List<string> stepList = new List<string>();             // 落子记录

    private readonly string[] pieceStrArr = { "White Center King", "White Center Queen", "White Left Bishop", "White Right Bishop", "White Left Knight", "White Right Knight", "White Left Rook", "White Right Rook", "White Pawn A", "White Pawn B", "White Pawn C", "White Pawn D", "White Pawn E", "White Pawn F", "White Pawn G", "White Pawn H", "Black Center King", "Black Center Queen", "Black Left Bishop", "Black Right Bishop", "Black Left Knight", "Black Right Knight", "Black Left Rook", "Black Right Rook", "Black Pawn A", "Black Pawn B", "Black Pawn C", "Black Pawn D", "Black Pawn E", "Black Pawn F", "Black Pawn G", "Black Pawn H" };

    private List<Square> squares = new List<Square>();              // 方格列表
    private List<Piece> pieces = new List<Piece>();                 // 棋子列表

    public Piece pickPiece = null;                                  // 当前举棋
    public Coordinate pickCoord = null;                             // 当前举棋棋盘坐标
    private List<Coordinate> passCoords = new List<Coordinate>();   // 可行列表
    private List<Coordinate> killCoords = new List<Coordinate>();   // 可杀列表

    public Board()
    {
        InitSquares();
        InitPieces();
    }

    // 初始化方格列表，将Unity对象转换为抽象对象
    public void InitSquares()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                squares.Add(new Square(GameObject.Find("Square " + (char)('A' + j) + '-' + (i + 1))));
            }
        }
    }

    // 初始化棋子列表，将Unity对象转换为抽象对象
    public void InitPieces()
    {
        foreach (string pieceStr in pieceStrArr)
        {
            pieces.Add(new Piece(GameObject.Find(pieceStr)));
        }
    }

    // 举棋
    public void PickPiece(Coordinate coord)
    {
        if (coord == null)
        {
            return;
        }

        ClearSquares(); // 清除方格染色

        pickCoord = coord;
        pickPiece = FindPiece(pickCoord);
        CheckNextSteps(pickPiece);

        TintSquares(); // 方格染色
    }

    // 落子
    public void MovePiece(Coordinate coord)
    {
        if (pickPiece != null)
        {
            pickPiece.go.transform.localPosition = coord.vec;
            pickPiece.coord = coord;
            pickPiece.step++;
            stepList.Add(++stepCount + "--" + pickPiece.go.name + "--" + pickPiece.coord.ToString());
        }
        ClearSquares();
    }

    // 杀子
    public void KillPiece(Coordinate coord)
    {
        Piece piece = FindPiece(coord);
        if (pieces.Remove(piece))
        {
            Object.Destroy(piece.go);
        }
    }

    // 检查棋子所有可行可杀路径
    public void CheckNextSteps(Piece piece)
    {
        switch (piece.type)
        {
            case PieceType.WhitePawn:
            case PieceType.BlackPawn:
                {
                    bool canUp = true;
                    stepSize = (piece.step == 0) ? 3 : 2;
                    for (int i = 1; i < stepSize; i++)
                    {
                        if ((piece.type == PieceType.WhitePawn))
                        {
                            if (canUp && (canUp = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] + i, 1))) { }
                        }
                        else if ((piece.type == PieceType.BlackPawn))
                        {
                            if (canUp && (canUp = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] - i, 1))) { }
                        }
                    }
                    if ((piece.type == PieceType.WhitePawn))
                    {
                        CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1] + 1, 2);
                        CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1] + 1, 2);
                    }
                    else if ((piece.type == PieceType.BlackPawn))
                    {
                        CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1] - 1, 2);
                        CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1] - 1, 2);
                    }
                }
                break;
            case PieceType.WhiteRook:
            case PieceType.BlackRook:
                {
                    bool canUp = true, canRight = true, canBottom = true, canLeft = true;
                    stepSize = 8;
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (canUp && (canUp = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] + i))) { }
                        if (canRight && (canRight = CheckNextStep(piece.coord.pos[0] + i, piece.coord.pos[1]))) { }
                        if (canBottom && (canBottom = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] - i))) { }
                        if (canLeft && (canLeft = CheckNextStep(piece.coord.pos[0] - i, piece.coord.pos[1]))) { }
                    }
                }
                break;
            case PieceType.WhiteKnight:
            case PieceType.BlackKnight:
                bool canUpRight = true, canRightUp = true, canRightBottom = true, canBottomRight = true,
                     canBottomLeft = true, canLeftBottom = true, canLeftUp = true, canUpLeft = true;
                if (canUpRight && (canUpRight = CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1] + 2))) { }
                if (canRightUp && (canRightUp = CheckNextStep(piece.coord.pos[0] + 2, piece.coord.pos[1] + 1))) { }
                if (canRightBottom && (canRightBottom = CheckNextStep(piece.coord.pos[0] + 2, piece.coord.pos[1] - 1))) { }
                if (canBottomRight && (canBottomRight = CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1] - 2))) { }
                if (canBottomLeft && (canBottomLeft = CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1] - 2))) { }
                if (canLeftBottom && (canLeftBottom = CheckNextStep(piece.coord.pos[0] - 2, piece.coord.pos[1] - 1))) { }
                if (canLeftUp && (canLeftUp = CheckNextStep(piece.coord.pos[0] - 2, piece.coord.pos[1] + 1))) { }
                if (canUpLeft && (canUpLeft = CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1] + 2))) { }
                break;
            case PieceType.WhiteBishop:
            case PieceType.BlackBishop:
                {
                    bool canNorthEast = true, canSouthEast = true, canSouthWest = true, canNorthWest = true;
                    stepSize = 8;
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (canNorthEast && (canNorthEast = CheckNextStep(piece.coord.pos[0] + i, piece.coord.pos[1] + i))) { }
                        if (canSouthEast && (canSouthEast = CheckNextStep(piece.coord.pos[0] + i, piece.coord.pos[1] - i))) { }
                        if (canSouthWest && (canSouthWest = CheckNextStep(piece.coord.pos[0] - i, piece.coord.pos[1] - i))) { }
                        if (canNorthWest && (canNorthWest = CheckNextStep(piece.coord.pos[0] - i, piece.coord.pos[1] + i))) { }
                    }
                }
                break;
            case PieceType.WhiteQueen:
            case PieceType.BlackQueen:
                {
                    bool canUp = true, canRight = true, canBottom = true, canLeft = true,
                        canNorthEast = true, canSouthEast = true, canSouthWest = true, canNorthWest = true;
                    stepSize = 8;
                    for (int i = 1; i < stepSize; i++)
                    {
                        if (canUp && (canUp = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] + i))) { }
                        if (canRight && (canRight = CheckNextStep(piece.coord.pos[0] + i, piece.coord.pos[1]))) { }
                        if (canBottom && (canBottom = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] - i))) { }
                        if (canLeft && (canLeft = CheckNextStep(piece.coord.pos[0] - i, piece.coord.pos[1]))) { }
                        if (canNorthEast && (canNorthEast = CheckNextStep(piece.coord.pos[0] + i, piece.coord.pos[1] + i))) { }
                        if (canSouthEast && (canSouthEast = CheckNextStep(piece.coord.pos[0] + i, piece.coord.pos[1] - i))) { }
                        if (canSouthWest && (canSouthWest = CheckNextStep(piece.coord.pos[0] - i, piece.coord.pos[1] - i))) { }
                        if (canNorthWest && (canNorthWest = CheckNextStep(piece.coord.pos[0] - i, piece.coord.pos[1] + i))) { }
                    }
                }
                break;
            case PieceType.WhiteKing:
            case PieceType.BlackKing:
                {
                    bool canUp = true, canRight = true, canBottom = true, canLeft = true,
                        canNorthEast = true, canSouthEast = true, canSouthWest = true, canNorthWest = true;
                    if (canUp && (canUp = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] + 1))) { }
                    if (canRight && (canRight = CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1]))) { }
                    if (canBottom && (canBottom = CheckNextStep(piece.coord.pos[0], piece.coord.pos[1] - 1))) { }
                    if (canLeft && (canLeft = CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1]))) { }
                    if (canNorthEast && (canNorthEast = CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1] + 1))) { }
                    if (canSouthEast && (canSouthEast = CheckNextStep(piece.coord.pos[0] + 1, piece.coord.pos[1] - 1))) { }
                    if (canSouthWest && (canSouthWest = CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1] - 1))) { }
                    if (canNorthWest && (canNorthWest = CheckNextStep(piece.coord.pos[0] - 1, piece.coord.pos[1] + 1))) { }
                }
                break;
        }
    }

    // 检查棋子下一步可行可杀
    public bool CheckNextStep(int x, int y, int flag = 0)
    {
        Coordinate coord = new Coordinate(x, y);
        if (coord.IsVaild())
        {
            Piece piece = FindPiece(coord);
            if (piece == null && (flag == 0 || flag == 1))
            {
                passCoords.Add(coord);
            }
            else if (piece != null && piece.IsEnemy(pickPiece.type) && (flag == 0 || flag == 2))
            {
                killCoords.Add(coord);
                return false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    // 找到方格抽象对象
    public Square FindSquare(Coordinate coord)
    {
        if (coord == null)
        {
            return null;
        }
        foreach (Square square in squares)
        {
            if (square.coord.pos[0] == coord.pos[0]
             && square.coord.pos[1] == coord.pos[1])
            {
                return square;
            }
        }
        return null;
    }

    // 找到棋子抽象对象
    public Piece FindPiece(Coordinate coord)
    {
        if (coord == null)
        {
            return null;
        }
        foreach (Piece piece in pieces)
        {
            if (piece.coord.pos[0] == coord.pos[0]
             && piece.coord.pos[1] == coord.pos[1])
            {
                return piece;
            }
        }
        return null;
    }

    // 对所有可行可杀方格染色
    public void TintSquares()
    {
        if (pickPiece != null) // 对举棋方格染色
        {
            TintSquare(pickPiece.coord, SquareTint.Pick);
        }
        foreach (Coordinate passCoord in passCoords) // 对所有可行方格染色
        {
            TintSquare(passCoord, SquareTint.Pass);
        }
        foreach (Coordinate killCoord in killCoords) // 对所有可杀方格染色
        {
            TintSquare(killCoord, SquareTint.Kill);
        }
    }

    // 对单个方格染色
    public bool TintSquare(Coordinate coord, SquareTint type)
    {
        if (coord == null)
        {
            return false;
        }
        Square square = null;
        if ((square = FindSquare(coord)) != null && square.go != null)
        {
            MeshRenderer renderer = null;
            if ((renderer = square.go.GetComponent<MeshRenderer>()) != null && renderer.material != null)
            {
                Color color = new Color32(223, 210, 192, 255);
                if (type == SquareTint.Pick) // 举棋为蓝色
                {
                    color = new Color32(33, 150, 243, 255);
                }
                else if (type == SquareTint.Pass) // 可行为绿色
                {
                    color = new Color32(76, 175, 80, 255);
                }
                else if (type == SquareTint.Kill) // 可杀为红色
                {
                    color = new Color32(244, 67, 54, 255);
                }
                square.tint = type;
                renderer.material.color = color;
                return true;
            }
        }
        return false;
    }

    // 清除所有可行可杀方格染色
    public void ClearSquares()
    {
        if (pickCoord != null)
        {
            ClearSquare(pickCoord);
        }
        foreach (Coordinate passCoord in passCoords)
        {
            ClearSquare(passCoord);
        }
        foreach (Coordinate killCoord in killCoords)
        {
            ClearSquare(killCoord);
        }
        pickCoord = null;
        passCoords.Clear();
        killCoords.Clear();
    }

    // 清除单个方格染色
    public bool ClearSquare(Coordinate coord)
    {
        if (coord == null)
        {
            return false;
        }
        Square square = null;
        if ((square = FindSquare(coord)) != null && square.go != null)
        {
            MeshRenderer renderer = null;
            if ((renderer = square.go.GetComponent<MeshRenderer>()) != null && renderer.material != null)
            {
                Color color = new Color32(223, 210, 192, 255);
                if (square.type == SquareType.White)
                {
                    color = new Color32(223, 210, 192, 255);
                }
                else if (square.type == SquareType.Black)
                {
                    color = new Color32(42, 40, 40, 255);
                }
                else
                {
                    return false;
                }
                square.tint = SquareTint.Undefined;
                renderer.material.color = color;
                return true;
            }
        }
        return false;
    }
}
