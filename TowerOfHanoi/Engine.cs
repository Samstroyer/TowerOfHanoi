using Raylib_cs;
using System.Numerics;

public class Engine
{
    private Vector2 screenDim = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

    private Camera3D camera;

    private enum Selected
    {
        Left = 0,
        Center = 1,
        Right = 2
    }

    bool up = false;
    Selected selected = Selected.Left;
    byte selectedIndex = 0;

    bool win = false;

    Stick[] towers = {
        new Stick() {
            Position = new(-20, 0, 0),
        },
        new Stick()
        {
            Position = new(0, 0, 0)
        },
        new Stick()
        {
            Position = new(20, 0, 0)
        },
    };

    public Engine()
    {
        towers[0].ToroidStack.Push(new Toroid(8, Color.RED));
        towers[0].ToroidStack.Push(new Toroid(6, Color.ORANGE));
        towers[0].ToroidStack.Push(new Toroid(4, Color.YELLOW));
        towers[0].ToroidStack.Push(new Toroid(2, Color.GREEN));
        camera = new(new(50, 50, 50), new(0, 0, 0), new(0, 1, 0), 70, CameraProjection.CAMERA_PERSPECTIVE);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            BeginContext();

            GetSelected();
            MouseBinds();

            if (up) towers[(int)selected].SelectedAnimation();
            foreach (Stick s in towers)
            {
                s.Render();
            }

            CheckWin();

            EndContext();

            if (win) WinScreen();
        }
    }

    private void WinScreen()
    {
        bool displayWin = true;
        while (displayWin)
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.WHITE);

            Raylib.DrawText("You Win!", 200, 200, 64, Color.BLUE);
            Raylib.DrawText("Press Escape to exit", 100, 400, 64, Color.BLUE);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE)) displayWin = false;

            Raylib.EndDrawing();
        }
    }

    private void CheckWin()
    {
        if (towers[2].ToroidStack.Count == 4) win = true;
    }

    private void MouseBinds()
    {
        if (!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) return;

        var position = Raylib.GetMousePosition();
        if (!up)
        {
            if (position.X < 266)
            {
                if (towers[0].ToroidStack.Count > 0)
                {
                    up = !up;
                    selectedIndex = 0;
                }
            }
            else if (position.X < 532)
            {
                if (towers[1].ToroidStack.Count > 0)
                {
                    up = !up;
                    selectedIndex = 1;
                }
            }
            else
            {
                if (towers[2].ToroidStack.Count > 0)
                {
                    up = !up;
                    selectedIndex = 2;
                }
            }
        }
        else
        {
            if (position.X < 266)
            {
                if (selectedIndex == 0) return;
                if (towers[selectedIndex].ToroidStack.Count <= 0) return;

                Toroid t = towers[selectedIndex].PeekToroid();
                if (towers[0].CanAccept(t.Size))
                {
                    t = towers[selectedIndex].TransferToroid();
                    towers[0].ToroidStack.Push(t);
                }
            }

            else if (position.X < 532)
            {
                if (selectedIndex == 1) return;
                if (towers[selectedIndex].ToroidStack.Count <= 0) return;

                Toroid t = towers[selectedIndex].PeekToroid();
                if (towers[1].CanAccept(t.Size))
                {
                    t = towers[selectedIndex].TransferToroid();
                    towers[1].ToroidStack.Push(t);
                }
            }
            else
            {
                if (selectedIndex == 2) return;
                if (towers[selectedIndex].ToroidStack.Count <= 0) return;

                Toroid t = towers[selectedIndex].PeekToroid();
                if (towers[2].CanAccept(t.Size))
                {
                    t = towers[selectedIndex].TransferToroid();
                    towers[2].ToroidStack.Push(t);
                }
            }
            up = !up;
        }

    }

    private void GetSelected()
    {
        var position = Raylib.GetMousePosition();

        if (position.X < 266)
        {
            selected = Selected.Left;
        }
        else if (position.X < 532)
        {
            selected = Selected.Center;
        }
        else
        {
            selected = Selected.Right;
        }

        Console.WriteLine(selectedIndex);
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

        Hud();

        Raylib.EndDrawing();
    }

    private void Hud()
    {
        Raylib.DrawRectangle(0, 600, 266, 200, Color.GREEN);
        Raylib.DrawRectangleLines(0, 600, 266, 200, Color.BLACK);

        Raylib.DrawRectangle(266, 600, 266, 200, Color.GREEN);
        Raylib.DrawRectangleLines(266, 600, 266, 200, Color.BLACK);

        Raylib.DrawRectangle(532, 600, 268, 200, Color.GREEN);
        Raylib.DrawRectangleLines(532, 600, 268, 200, Color.BLACK);
    }
}
