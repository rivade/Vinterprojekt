using Raylib_cs;
using System.Numerics;


const int screenwidth = 1024;
const int screenheight = 768;
Raylib.InitWindow(screenwidth, screenheight, "Mall Runner");
Raylib.SetTargetFPS(60);

Character c = new Character();
Background b = new Background();

string currentScene = "start";

float speed = 4.5f;
int avatarShown = 0;
List<string> inventory = new();



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
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D) && c.player.x < (screenwidth - c.outfits[0].width) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) && c.player.x < (screenwidth - 66))
            {
            c.player.x += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A) && c.player.x > 0 || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) && c.player.x > 0)
            {
            c.player.x -= speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && c.player.y + c.outfits[0].height < screenheight || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && c.player.y + 108 < screenheight)
            {
            c.player.y += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && c.player.y > 0 || Raylib.IsKeyDown(KeyboardKey.KEY_UP) && c.player.y > 0)
            {
            c.player.y -= speed;
            }
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
            Raylib.DrawTexture(c.outfits[avatarShown], (int)c.player.x, (int)c.player.y, Color.WHITE);
        break;
    }

    Raylib.EndDrawing();
}