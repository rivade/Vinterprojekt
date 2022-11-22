using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;
Raylib.InitWindow(screenwidth, screenheight, "Mall Runner");
Raylib.SetTargetFPS(60);


Character c = new();
Background b = new();
NPC n = new();
Enemy e = new();

Random npcgenerator = new();
int npcoutfit = npcgenerator.Next(n.npctexture.Count);
int npcoutfit1 = npcgenerator.Next(n.npctexture.Count);
int npcoutfit2 = npcgenerator.Next(n.npctexture.Count);

string currentScene = "start";

float speed = 4.5f;
int avatarShown = 0;
Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 3;
List<string> inventory = new();
Rectangle player = new(0, 0, c.outfits[0].width, c.outfits[0].height);
Rectangle clothesstore = new(724, 568, 300, 200);
Rectangle supermarket = new(724, 0, 300, 200);


static float walkMechanicsX(float playerx, float speed)
{
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
static float walkMechanicsY(float playery, float speed)
{
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
while (!Raylib.WindowShouldClose())
{
    //LOGIK
    switch (currentScene)
    { //Switch istället för massa if-satser rakt under varandra för varje currentScene
        case "start":
            player.x = 0;
            player.y = 0;
            avatarShown = 0;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {
                currentScene = "mall";
            }
            break;

        case "mall":
            player.x = walkMechanicsX(player.x, speed);
            player.y = walkMechanicsY(player.y, speed);
            if (Raylib.CheckCollisionRecs(player, supermarket))
            {
                currentScene = "supermarket";
            }
            if (Raylib.CheckCollisionRecs(player, clothesstore))
            {
                currentScene = "charselect";
            }
            for (var i = 0; i < 2; i++) //Skapar en rektangel för varje föremål i listan n.npcs
            {
                Rectangle r = n.npcs[i];
                r.x = (i + 1) * 200; //Gör så att varje rektangel får ett x värde i en rad
                r.y += speed;
                if (r.y >= 768){
                    r.y = -300;
                    npcoutfit = npcgenerator.Next(3);
                    npcoutfit1 = npcgenerator.Next(3);
                }
                n.npcs[i] = r; //Applicerar de nya x och y värderna på rektanglarna i listan npcs.
            }
            Rectangle r2 = n.npcrect2;
            r2.x += speed;
            if (r2.x >= 1024){
                r2.x = -300;
                npcoutfit2 = npcgenerator.Next(3);
            }
            n.npcrect2 = r2;
        break;

        case "supermarket":
            player.x = walkMechanicsX(player.x, speed);
            player.y = walkMechanicsY(player.y, speed);
            Vector2 playerPos = new Vector2(player.x, player.y);
            Vector2 enemyPos = new Vector2(e.enemyRect.x, e.enemyRect.y);
            Vector2 diff = playerPos - enemyPos;
            Vector2 enemyDirection = Vector2.Normalize(diff);
            enemyMovement = enemyDirection * enemySpeed;
            e.enemyRect.x += enemyMovement.X;
            e.enemyRect.y += enemyMovement.Y;
            if (Raylib.CheckCollisionRecs(player, e.enemyRect))
            {
                currentScene = "gameover";
            }
            break;

        case "charselect":
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                if (avatarShown < 5)
                {
                    avatarShown++;
                    System.Threading.Thread.Sleep(175); //Förhindrar applikationen från att bläddra medans piltangenten är nedtryckt så man hinner reagera
                }
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                if (avatarShown > 0)
                {
                    avatarShown--;
                    System.Threading.Thread.Sleep(175);
                }
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
            {
                player.x = 0;
                player.y = 0;
                currentScene = "mall";
            }
            break;

        case "game":
            player.x = walkMechanicsX(player.x, speed);
            player.y = walkMechanicsY(player.y, speed);
            break;

        case "gameover":
            if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER))
            {
                currentScene = "start";
            }
            break;
    }


    //GRAFIK
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    switch (currentScene)
    {
        case "start":
            Raylib.DrawTexture(b.backgrounds[0], 0, 0, Color.WHITE);
            Raylib.DrawText("Mall Runner!", 350, 350, 50, Color.RED);
            Raylib.DrawText("Press SPACE to start", 325, 425, 32, Color.BLACK);
        break;

        case "mall":
            Raylib.DrawTexture(b.backgrounds[1], 0, 0, Color.WHITE);
            Raylib.DrawTexture(b.backgrounds[3], 724, 0, Color.WHITE);
            Raylib.DrawText("Supermarket", 780, 65, 30, Color.BLACK);
            Raylib.DrawText("(Enter to steal)", 780, 100, 20, Color.BLACK);
            Raylib.DrawTexture(b.backgrounds[4], 724, 568, Color.WHITE);
            Raylib.DrawText("Clothing Store", 760, 650, 30, Color.BLACK);
            Raylib.DrawTexture(n.npctexture[npcoutfit], (int)n.npcs[0].x, (int)n.npcs[0].y, Color.WHITE);
            Raylib.DrawTexture(n.npctexture[npcoutfit1], (int)n.npcs[1].x, (int)n.npcs[1].y, Color.WHITE);
            Raylib.DrawTexture(n.npctexture[npcoutfit2], (int)n.npcrect2.x, (int)n.npcrect2.y, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
        break;

        case "supermarket":
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
            Raylib.DrawTexture(e.enemyImage, (int)e.enemyRect.x, (int)e.enemyRect.y, Color.WHITE);
        break;

        case "charselect":
            Raylib.DrawTexture(b.backgrounds[2], 0, 0, Color.WHITE);
            Raylib.DrawText("SELECT YOUR OUTFIT", 250, 300, 50, Color.WHITE);
            Raylib.DrawText("(right or left arrow)", 375, 375, 30, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], 485, 550, Color.WHITE);
        break;

        case "game":
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
            break;

        case "gameover":
            Raylib.DrawText("Game Over!", 400, 300, 40, Color.BLACK);
            Raylib.DrawText("Press ENTER to start over", 400, 400, 30, Color.BLACK);
        break;
    }

    Raylib.EndDrawing();
}