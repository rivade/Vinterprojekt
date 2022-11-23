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

Random rnd = new();
int npcoutfit = rnd.Next(n.npctexture.Count);
int npcoutfit1 = rnd.Next(n.npctexture.Count);
int npcoutfit2 = rnd.Next(n.npctexture.Count);

string currentScene = "start";

float speed = 4.5f;
int avatarShown = 0;
Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 2f;
bool itemactive = false;
List<int> inventory = new();
Rectangle player = new(0, 0, c.outfits[0].width, c.outfits[0].height);
Rectangle clothesstore = new(724, 568, 300, 200);
Rectangle supermarket = new(724, 0, 300, 200);
Rectangle item = new(0, 0, 50, 50);

Texture2D itempic = Raylib.LoadTexture("item.png");

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
            enemySpeed = 2f;
            inventory.Clear();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                currentScene = "mall";
            }
            break;

        case "mall":
            player.x = walkMechanicsX(player.x, speed);
            player.y = walkMechanicsY(player.y, speed);
            if (Raylib.CheckCollisionRecs(player, supermarket))
            {
                currentScene = "loading";
            }
            if (Raylib.CheckCollisionRecs(player, clothesstore))
            {
                currentScene = "charselect";
            }

            //NPCS
            for (var i = 0; i < 2; i++) //Skapar en rektangel för varje föremål i listan n.npcs
            {
                Rectangle r = n.npcs[i];
                r.x = (i + 1) * 200; //Gör så att varje rektangel får ett x värde i en rad
                r.y += (speed/2);
                if (r.y >= 768){
                    r.y = -300;
                    npcoutfit = rnd.Next(n.npctexture.Count); //Byter sprite på NPCn så att det ser ut som en annan som går
                    npcoutfit1 = rnd.Next(n.npctexture.Count);
                }
                n.npcs[i] = r; //Applicerar de nya x och y värderna på rektanglarna i listan npcs.
            }
            Rectangle r2 = n.npcrect2;
            r2.x += (speed/2);
            if (r2.x >= 1024){
                r2.x = -300;
                npcoutfit2 = rnd.Next(n.npctexture.Count);
            }
            n.npcrect2 = r2;
            //NPCS
        break;

        case "loading":
            player.x = 0;
            player.y = 0;
            e.enemyRect.x = 700;
            e.enemyRect.y = 500;
            System.Threading.Thread.Sleep(7500); //Simulerar en loading screen men ger egentligen bara spelaren tid att läsa instruktioner
            currentScene = "supermarket";
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
            if (!itemactive){ //Flyttar föremålet till en random plats när man plockar upp den
                item.x = rnd.Next(1, 975);
                item.y = rnd.Next(1, 719);
                itemactive = true;
            }
            if(Raylib.CheckCollisionRecs(player, item)){
                inventory.Add(rnd.Next(50, 101));
                enemySpeed += 0.1f;
                itemactive = false;
            }
            if (Raylib.CheckCollisionRecs(player, e.enemyRect))
            {
                currentScene = "gameover";
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)){
                Console.WriteLine(inventory.Sum());
                player.x = 0;
                player.y = 0;
                currentScene = "mall";
            }
        break;

        case "charselect":
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                if (avatarShown < 5)
                {
                    avatarShown++;
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                if (avatarShown > 0)
                {
                    avatarShown--;
                }
            }

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
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
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
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

        case "loading":
            Raylib.DrawTexture(b.backgrounds[0], 0, 0, Color.WHITE);
            Raylib.DrawText("Loading Supermarket", 250, 325, 50, Color.BLACK);
            Raylib.DrawText("Steal as much stuff as you can!", 255, 375, 32, Color.BLACK);
            Raylib.DrawText("Don't get caught by the guard!", 260, 400, 32, Color.BLACK);
            Raylib.DrawText("Press ENTER to exit the store.", 260, 425, 32, Color.BLACK);
        break;

        case "supermarket":
            Raylib.DrawTexture(b.backgrounds[5], 0, 0, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
            Raylib.DrawTexture(e.enemyImage, (int)e.enemyRect.x, (int)e.enemyRect.y, Color.WHITE);
            Raylib.DrawTexture(itempic, (int)item.x, (int)item.y, Color.WHITE);
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