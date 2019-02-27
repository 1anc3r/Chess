using UnityEngine;

// 方格抽象对象
public class Square
{
    public GameObject go;       // 方格Unity对象
    public SquareType type;     // 方格类型
    public SquareTint tint;     // 方格染色
    public Coordinate coord;    // 方格棋盘坐标

    public Square(GameObject go)
    {
        this.go = go;
        type = NameToType(go.transform.parent.transform.name);
        tint = SquareTint.Undefined;
        coord = new Coordinate(go.transform.localPosition);
    }

    // 方块名字转类型
    public SquareType NameToType(string name)
    {
        if (name.Contains("White"))
        {
            return SquareType.White;
        }
        else if (name.Contains("Black"))
        {
            return SquareType.Black;
        }
        return SquareType.Undefined;
    }
}
