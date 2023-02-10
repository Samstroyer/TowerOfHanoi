using System.Numerics;
using Raylib_cs;

public class Stick
{
    public Stack<Toroid> ToroidStack { get; set; }
    public Vector3 Position { get; set; } = new();

    private static double bounce = 0;

    public Stick()
    {
        ToroidStack = new();
    }

    public void Render()
    {
        Raylib.DrawCircle3D(Position, 10, new(1, 0, 0), -90f, Color.RED);

        if (ToroidStack.Count < 1) return;

        Toroid[] drawArr = ToroidStack.ToArray();

        Vector3 offset = new(0, 2, 0);
        for (int i = drawArr.Length - 1; i >= 0; i--)
        {
            Raylib.DrawCircle3D(Position + offset, drawArr[i].Size, new(1, 0, 0), -90f, drawArr[i].C);
            offset += new Vector3(0, 2, 0);
        }
    }

    public void SelectedAnimation()
    {
        if (ToroidStack.Count < 1) return;

        Toroid t = ToroidStack.Peek();

        float yOffset = (float)Math.Sin(bounce) * 3;

        Raylib.DrawCircle3D(Position - new Vector3(0, yOffset - 14, 0), t.Size, new(1, 0, 0), -90f, t.C);
        bounce += 0.005;
    }

    public Toroid TransferToroid()
    {
        return ToroidStack.Pop();
    }

    public Toroid PeekToroid()
    {
        return ToroidStack.Peek();
    }

    public bool CanAccept(int size)
    {
        if (ToroidStack.Count <= 0) return true;

        Toroid t = ToroidStack.Peek();

        if (t.Size > size) return true;
        else return false;
    }
}
