using System.Numerics;
using Raylib_cs;

public class Stick
{
    public Stack<Toroid> ToroidStack { get; set; }
    public Vector3 Position { get; set; } = new();


    public Stick()
    {
        ToroidStack = new();
    }

    public void Render()
    {
        Raylib.DrawCircle3D(Position, 10, new(1, 0, 0), -90f, Color.RED);

        if (ToroidStack.Count < 1) return;

        Vector3 offset = new(0, 2, 0);
        foreach (Toroid t in ToroidStack)
        {
            Raylib.DrawCircle3D(Position + offset, t.Size, new(1, 0, 0), -90f, t.C);
            offset += new Vector3(0, 2, 0);
        }
    }
}
