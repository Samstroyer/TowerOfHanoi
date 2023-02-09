using Raylib_cs;

// Tower Of Hanoi 
// Made by Samuel - TE20A
// Using stacks

Engine e;

Setup();
Draw();

void Setup()
{
    Raylib.InitWindow(800, 800, "Tower Of Hanoi");
    e = new();
}

void Draw()
{
    e.Run();
}