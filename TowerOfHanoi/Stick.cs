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
        Raylib.DrawCircle3D(Position, 10, new(0, 0, 0), 0f, Color.RED);
    }
}
