using Raylib_cs;
using System.Numerics;

public class Engine
{
    private Vector2 screenDim = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

    private Camera3D camera;

    Stick[] towers = {
        new Stick() {
            Position = new(-20, 0, 0)
        },
        new Stick() {
            Position = new(0, 0, 0)
        },
        new Stick() {
            Position = new(20, 0, 0)
        },
    };

    public Engine()
    {
        camera = new(new(-50, 50, -50), new(0, 0, 0), new(0, 1, 0), 70, CameraProjection.CAMERA_PERSPECTIVE);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            BeginContext();

            foreach (Stick s in towers)
            {
                s.Render();
            }

            EndContext();
        }
    }

    private void BeginContext()
    {
        Raylib.BeginDrawing();
        Raylib.BeginMode3D(camera);

        Raylib.ClearBackground(Color.WHITE);

        Raylib.UpdateCamera(ref camera);
    }

    private void EndContext()
    {
        Raylib.EndMode3D();
        Raylib.EndDrawing();
    }
}
