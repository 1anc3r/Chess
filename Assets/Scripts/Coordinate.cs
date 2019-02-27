using UnityEngine;

// 棋盘坐标
public class Coordinate
{
    public int[] pos = new int[2];              // 抽象坐标
    public Vector3 vec = Vector3.zero;          // 实际坐标
    private const float transformers = 4.5f;    // 抽象坐标与实际坐标之间的偏移

    // 通过抽象坐标构造棋盘坐标
    public Coordinate(int x, int y)
    {
        this.pos = new int[2] { x, y };
        vec = new Vector3(transformers - pos[0], 0, transformers - pos[1]);
    }

    // 通过抽象坐标构造棋盘坐标
    public Coordinate(int[] pos)
    {
        this.pos = pos;
        vec = new Vector3(transformers - pos[0], 0, transformers - pos[1]);
    }

    // 通过实际坐标构造棋盘坐标
    public Coordinate(Vector3 vec)
    {
        this.vec = vec;
        pos[0] = (int)(transformers - vec.x);
        pos[1] = (int)(transformers - vec.z);
    }

    // 通过抽象坐标设置棋盘坐标
    public void SetCoordinate(int[] pos)
    {
        this.pos = pos;
        vec = new Vector3(transformers - pos[0], 0, transformers - pos[1]);
    }

    // 通过实际坐标设置棋盘坐标
    public void SetCoordinate(Vector3 vec)
    {
        this.vec = vec;
        pos[0] = (int)(transformers - vec.x);
        pos[1] = (int)(transformers - vec.z);
    }

    // 判断棋盘坐标是否有效
    public bool IsVaild()
    {
        if(pos[0] > 0 && pos[0] < 9 && pos[1] > 0 && pos[1] < 9)
        {
            return true;
        }
        return false;
    }

    // 将棋盘坐标转换为字符串
    public override string ToString()
    {
        return (char)('A' + pos[0]) + "-" + (pos[1] + 1);
    }
}
