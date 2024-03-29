﻿using Raylib_cs;
using System.Numerics;

const int screenwidth = 1024;
const int screenheight = 768;
Raylib.InitWindow(screenwidth, screenheight, "Mall Runner");
Raylib.SetTargetFPS(60);


Character c = new();
Background b = new();
NPC n = new();
Enemy e = new();
Item f = new();

Random rnd = new();
int npcOutfit = rnd.Next(n.npctexture.Count);
int npcOutfit1 = rnd.Next(n.npctexture.Count);
int npcOutfit2 = rnd.Next(n.npctexture.Count);

string currentScene = "start";

float speed = 4.5f;
int avatarShown = 0;
int itemTexture = 0;
int timer = 0;
bool timerActive = false;
Vector2 enemyMovement = new Vector2(1, 0);
float enemySpeed = 2f;
bool itemActive = false;
bool marketLock = true;
bool sold = false;
bool sufficientFunds = true;
bool ending = false;
List<int> inventory = new();
int balance = 0;

Color alpha = new(255, 255, 255, 0);
int alphaVariable = 0;

Texture2D vendorTexture = Raylib.LoadTexture("NPCTextures/vendor.png");
Texture2D taxiTexture = Raylib.LoadTexture("Items/taxi.png");

Texture2D blackScreen = Raylib.LoadTexture("Backgrounds/black.png");


Rectangle player = new(0, 0, c.outfits[0].width, c.outfits[0].height);
Rectangle clothesStore = new(724, 568, 300, 200);
Rectangle supermarket = new(724, 0, 300, 200);
Rectangle market = new(0, 568, 300, 200);
Rectangle item = new(0, 0, 50, 50);
Rectangle vendor = new(0, 750, vendorTexture.width, vendorTexture.height);
Rectangle taxi = new((screenwidth/2) - (taxiTexture.width/2), 1725 - taxiTexture.height, taxiTexture.width, taxiTexture.height);

Camera2D camera = new();
camera.zoom = 1;
camera.rotation = 0;
camera.offset = new Vector2(screenwidth/2, screenheight/2);

static float WalkMechanicsX(float playerx, float speed)
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
static float WalkMechanicsY(float playery, float speed)
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
static float WalkMechanicsY2(float playery, float speed)
{
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S) && playery <= 1621 || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) && playery <= 1621)
    {
        playery += speed;
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W) && playery > 333 || Raylib.IsKeyDown(KeyboardKey.KEY_UP) && playery > 333)
    {
        playery -= speed;
    }
    return playery;
}

////////////////////////////////////////////////////////////
while (!Raylib.WindowShouldClose())
{
    //LOGIK
    if(timerActive){
        timer++;
    }
    switch (currentScene) //Switch istället för massa if-satser rakt under varandra för varje currentScene
    {
        case "start":
            player.x = 0;
            player.y = 0;
            market.x = 0;
            market.y = 568;
            avatarShown = 0;
            enemySpeed = 2f;
            marketLock = true;
            balance = 0;
            timer = 0;
            timerActive = false;
            ending = false;
            alphaVariable = 0;
            inventory.Clear(); //Återställer alla dessa värden till deras ursprung varje gång man kommer till start-screen
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                timerActive = true;
                currentScene = "mall";
            }
        break;

        case "mall":
            player.x = WalkMechanicsX(player.x, speed);
            player.y = WalkMechanicsY(player.y, speed);
            if (Raylib.CheckCollisionRecs(player, supermarket))
            {
                currentScene = "loading";
            }
            if (Raylib.CheckCollisionRecs(player, clothesStore))
            {
                currentScene = "charselect";
            }
            if (Raylib.CheckCollisionRecs(player, market)){
                if (!marketLock){
                    currentScene = "market";
                    player.x = screenwidth/2;
                    player.y = 455;
                    market.x = (screenheight/2);
                    market.y = 440 - market.height;
                }
            }

            //NPCS
            for (var i = 0; i < 2; i++) //Skapar en rektangel för två npcs
            {
                Rectangle r = n.npcs[i];
                r.x = (i + 1) * 310; //Gör så att varje rektangel får ett x värde i en rad
                r.y += (speed/2);
                if (r.y >= 768){
                    r.y = -300;
                    npcOutfit = rnd.Next(n.npctexture.Count); //Byter sprite på NPCn så att det ser ut som en annan som går
                    npcOutfit1 = rnd.Next(n.npctexture.Count);
                }
                n.npcs[i] = r; //Applicerar de nya x och y värderna på rektanglarna i listan npcs.
            }
            Rectangle r2 = n.npcrect2;
            r2.x += (speed/2);
            if (r2.x >= 1024){
                r2.x = -300;
                npcOutfit2 = rnd.Next(n.npctexture.Count);
            }
            n.npcrect2 = r2;
            //NPCS
        break;

        case "loading":
            player.x = 0;
            player.y = 0;
            e.enemyRect.x = 700;
            e.enemyRect.y = 500;
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)){
                currentScene = "supermarket";
            }
        break;

        case "supermarket":
            player.x = WalkMechanicsX(player.x, speed);
            player.y = WalkMechanicsY(player.y, speed);
            Vector2 playerPos = new Vector2(player.x, player.y);
            Vector2 enemyPos = new Vector2(e.enemyRect.x, e.enemyRect.y);
            Vector2 diff = playerPos - enemyPos;
            Vector2 enemyDirection = Vector2.Normalize(diff);
            enemyMovement = enemyDirection * enemySpeed;
            e.enemyRect.x += enemyMovement.X;
            e.enemyRect.y += enemyMovement.Y;
            if (!itemActive){ //Flyttar föremålet till en random plats när man plockar upp den
                item.x = rnd.Next(1, 975);
                item.y = rnd.Next(1, 719);
                itemTexture = rnd.Next(1, f.items.Count);
                itemActive = true;
            }
            if(Raylib.CheckCollisionRecs(player, item)){
                inventory.Add(rnd.Next(50, 101));
                enemySpeed += 0.1f;
                itemActive = false;
                marketLock = false;
            }
            if (Raylib.CheckCollisionRecs(player, e.enemyRect))
            {
                currentScene = "gameover";
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)){
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

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) || Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                player.x = 0;
                player.y = 0;
                currentScene = "mall";
            }
        break;

        case "gameover":
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                currentScene = "start";
            }
        break;

        case "market":
            player.x = WalkMechanicsX(player.x, speed);
            player.y = WalkMechanicsY2(player.y, speed);
            if(player.y >= 392 && player.y <= 1346){
                camera.target = new Vector2((screenwidth/2), player.y);
            }
            if (Raylib.CheckCollisionRecs(player, market)){
                currentScene = "mall";
                player.x = 0;
                player.y = 0;
                market.x = 0;
                market.y = 568;
            }
            if (Raylib.CheckCollisionRecs(player, vendor)){
                currentScene = "sell";
            }
            if (Raylib.CheckCollisionRecs(player, taxi)){
                currentScene = "taxi";
            }
        break;

        case "sell":
            if (inventory.Count() != 0){
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_Y)){
                    balance += inventory.Sum();
                    inventory.Clear();
                    sold = true;
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_N) && inventory.Count() != 0 || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) && inventory.Count == 0){
                sold = false;
                player.x = 206;
                player.y = 820;
                currentScene = "market";
            }
        break;

        case "taxi":
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_Y)){
                if (balance < 1000){
                    sufficientFunds = false;
                }
                else{
                    balance -= 1000;
                    ending = true;
                }
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_N) || Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER) && !sufficientFunds){
                sufficientFunds = true;
                player.x = 475;
                player.y = 1445;
                currentScene = "market";
            }
            if (ending){
                timerActive = false;
                if (alphaVariable < 255){
                    alphaVariable++;
                    alpha.a = (byte)alphaVariable;
                }
                else{
                    currentScene = "endscene";
                }
            }
        break;

        case "endscene":
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER)){
                currentScene = "start";
            }
        break;
    }


    //GRAFIK
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.BLACK);


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
            Raylib.DrawTexture(b.backgrounds[6], 0, 568, Color.WHITE);
            Raylib.DrawText("Market", 100, 600, 30, Color.BLACK);
            if (marketLock){
                Raylib.DrawTexture(f.items[0], 125, 650, Color.WHITE);
            }
            Raylib.DrawTexture(n.npctexture[npcOutfit], (int)n.npcs[0].x, (int)n.npcs[0].y, Color.WHITE);
            Raylib.DrawTexture(n.npctexture[npcOutfit1], (int)n.npcs[1].x, (int)n.npcs[1].y, Color.WHITE);
            Raylib.DrawTexture(n.npctexture[npcOutfit2], (int)n.npcrect2.x, (int)n.npcrect2.y, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
        break;

        case "loading":
            Raylib.DrawTexture(b.backgrounds[0], 0, 0, Color.WHITE);
            Raylib.DrawText("Press SPACE to start!", 250, 325, 45, Color.RED);
            Raylib.DrawText("Steal as much stuff as you can!", 255, 375, 32, Color.BLACK);
            Raylib.DrawText("Don't get caught by the guard!", 260, 400, 32, Color.BLACK);
            Raylib.DrawText("Press ENTER to exit the store.", 260, 425, 32, Color.BLACK);
        break;
        
        case "supermarket":
            Raylib.DrawTexture(b.backgrounds[5], 0, 0, Color.WHITE);
            Raylib.DrawTexture(f.items[itemTexture], (int)item.x, (int)item.y, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
            Raylib.DrawTexture(e.enemyImage, (int)e.enemyRect.x, (int)e.enemyRect.y, Color.WHITE);
            Raylib.DrawText($"Items picked up: {inventory.Count}", 0, 0, 25, Color.BLACK);
        break;

        case "charselect":
            Raylib.DrawTexture(b.backgrounds[2], 0, 0, Color.WHITE);
            Raylib.DrawText("SELECT YOUR OUTFIT", 250, 300, 50, Color.WHITE);
            Raylib.DrawText("(right or left arrow)", 375, 375, 30, Color.WHITE);
            Raylib.DrawText("(ENTER to select outfit)", 340, 405, 30, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], 485, 550, Color.WHITE);
        break;

        case "gameover":
            Raylib.DrawTexture(b.backgrounds[7], 0, 0, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], 400, 550, Color.WHITE);
            Raylib.DrawText("Busted!", 400, 350, 50, Color.RED);
            Raylib.DrawText("Press ENTER to try again!", 300, 400, 30, Color.RED);
        break;

        case "market":
            Raylib.BeginMode2D(camera);
            Raylib.DrawTexture(b.backgrounds[8], 0, 0, Color.WHITE);
            Raylib.DrawTexture(vendorTexture, (int)vendor.x, (int)vendor.y, Color.WHITE);
            Raylib.DrawTexture(taxiTexture, (int)taxi.x, (int)taxi.y, Color.WHITE);
            Raylib.DrawTexture(c.outfits[avatarShown], (int)player.x, (int)player.y, Color.WHITE);
            Raylib.EndMode2D();
        break;

        case "sell":
            Raylib.DrawTexture(b.backgrounds[9], 0, 0, Color.WHITE);
            Raylib.DrawText("BALANCE:", 800, 625, 32, Color.GOLD);
            Raylib.DrawText($"${balance}", 800, 675, 38, Color.GOLD);
            if (inventory.Count() == 0 && sold == false){
                Raylib.DrawText("Your inventory is empty!", 25, 600, 32, Color.WHITE);
                Raylib.DrawText("Press ENTER to leave", 25, 650, 32, Color.WHITE);
            }
            else{
                if (sold){
                    Raylib.DrawText("Sold!", 25, 600, 32, Color.WHITE);
                    Raylib.DrawText("Press ENTER to leave", 25, 650, 32, Color.WHITE);
                }
                else{
                    Raylib.DrawText($"Would you like to sell your items for ${inventory.Sum()}?", 25, 550, 32, Color.WHITE);
                    Raylib.DrawText("Press Y for yes", 25, 675, 32, Color.WHITE);
                    Raylib.DrawText("Press N for no", 25, 725, 32, Color.WHITE);
                }
            }
        break;

        case "taxi":
            Raylib.DrawTexture(b.backgrounds[10], 0, 0, Color.WHITE);
            if (sufficientFunds){
                Raylib.DrawText($"A taxi ride costs $1000", 25, 550, 32, Color.WHITE);
                Raylib.DrawText("Do you want to leave?", 25, 600, 32, Color.WHITE);
                Raylib.DrawText("Press Y for yes", 25, 675, 32, Color.WHITE);
                Raylib.DrawText("Press N for no", 25, 725, 32, Color.WHITE);
            }
            else{
                Raylib.DrawText("Insufficient funds!", 25, 600, 32, Color.WHITE);
                Raylib.DrawText("Press ENTER to leave", 25, 650, 32, Color.WHITE);
            }
            Raylib.DrawText("BALANCE:", 800, 625, 32, Color.GOLD);
            Raylib.DrawText($"${balance}", 800, 675, 38, Color.GOLD);
            if (ending){
                Raylib.DrawTexture(blackScreen, 0, 0, alpha);
            }
        break;

        case "endscene":
            Raylib.DrawText("Congratulations!", 0, 0, 32, Color.WHITE);
            Raylib.DrawText($"You finished the game in {timer/60} seconds", 0, 100, 32, Color.WHITE);
            Raylib.DrawText($"You ended with ${balance} remaining", 0, 200, 32, Color.WHITE);
            Raylib.DrawText("Press ENTER to play again", 0, 300, 32, Color.WHITE);
            Raylib.DrawText("Press ESC to exit the game", 0, 400, 32, Color.WHITE);
        break;
    }
    Raylib.EndDrawing();
}