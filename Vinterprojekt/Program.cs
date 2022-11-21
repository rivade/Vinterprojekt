using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;
Raylib.InitWindow(screenwidth, screenheight, "Mall Runner");
Raylib.SetTargetFPS(60);

Character c = new();
Background b = new();

string currentScene = "start";

float speed = 4.5f;
int avatarShown = 0;
List<string> inventory = new();
Rectangle player = new(0, 0, c.outfits[0].width, c.outfits[0].height);

static float walkMechanicsX(float playerx, float speed){
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && playerx < (screenwidth - 66) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && playerx < (screenwidth - 66)) //66 i detta fall är bredden på spriten i pixlar och förhindrar spelaren från att gå utanför skärmen.
    {
        playerx += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && playerx > 0 || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && playerx > 0)
    {
        playerx -= speed;
    }
    return playerx;
}
static float walkMechanicsY(float playery, float speed){
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && playery + 108 < screenheight || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && playery + 108 < screenheight) //108 är höjden på spriten och förhindrar spelaren från att gå utanför skärmen
    {
        playery += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && playery > 0 || Raylib.IsKeyDown(KeyboardKey.KEY_UP) && playery > 0)
    {
        playery -= speed;
    }
    return playery;
}


////////////////////////////////////////////////////////////
while (!Raylib.WindowShouldClose()){
    //LOGIK
    switch (currentScene){ //Switch istället för massa if-satser rakt under varandra för varje currentScene
        case "start":
            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)){
                currentScene = "charselect";
            }
        break;

        case "charselect":
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)){
                if (avatarShown < 5){
                    avatarShown++;
                    System.Threading.Thread.Sleep(150); //Förhindrar applikationen från att bläddra medans piltangenten är nedtryckt så man hinner reagera
                }
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)){
                if (avatarShown > 0){
                    avatarShown--;
                    System.Threading.Thread.Sleep(150);
                }
            }

            if(Raylib.IsKeyDown(KeyboardKey.KEY_ENTER)){
                currentScene = "game";
            }
        break;

        case "game":
            player.x = walkMechanicsX(player.x, speed);
            player.y = walkMechanicsY(player.y, speed);
        break;
    }

    
    //GRAFIK
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    switch(currentScene){
        case "start":
            Raylib.DrawTexture(b.backgrounds[0], 0, 0, Color.WHITE);
            Raylib.DrawText("Mall Runner!", 350, 350, 50, Color.RED);
            Raylib.DrawText("Press SPACE to start", 325, 425, 32, Color.BLACK);
        break;

        case "charselect":
            Raylib.DrawTexture(b.backgrounds[1], 0, 0, Color.WHITE);
            Raylib.DrawText("SELECT YOUR OUTFIT", 250, 300, 50, Color.WHITE);
            Raylib.DrawText("(right or left arrow)", 375, 375, 30, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], 485, 550, Color.WHITE);
        break;

        case "game":
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
        break;
    }

    Raylib.EndDrawing();
}