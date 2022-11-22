using Raylib_cs;

class Character {
  public List<Texture2D> outfits = new();
  public Character() //Döpt till samma som klassen, så koden körs varje gång en instans skapas
  {
    outfits.Add(Raylib.LoadTexture("a1.png"));
    outfits.Add(Raylib.LoadTexture("a2.png"));
    outfits.Add(Raylib.LoadTexture("a3.png"));
    outfits.Add(Raylib.LoadTexture("a4.png"));
    outfits.Add(Raylib.LoadTexture("a5.png"));
    outfits.Add(Raylib.LoadTexture("a6.png"));
  } 
}

class Background {
  public List<Texture2D> backgrounds = new();
  public Background()
  {
    backgrounds.Add(Raylib.LoadTexture("titlescreen.png"));
    backgrounds.Add(Raylib.LoadTexture("mall.png"));
    backgrounds.Add(Raylib.LoadTexture("charselectbg.png"));
    backgrounds.Add(Raylib.LoadTexture("supermarket.png"));
    backgrounds.Add(Raylib.LoadTexture("clothingstore.png"));
  }
}

class NPC{
  public List<Rectangle> npcs = new();
  public List<Texture2D> npctexture = new();
  public Rectangle npcrect = new(0, -300, 60, 108);
  public Rectangle npcrect2 = new(-300, 325, 60, 108);

  public NPC()
  {
  for (var i = 0; i < 2; i++){
    npcs.Add(npcrect);
  }
  npctexture.Add(Raylib.LoadTexture("npc.png"));
  npctexture.Add(Raylib.LoadTexture("npc2.png"));
  npctexture.Add(Raylib.LoadTexture("npc3.png"));
  }
}

class Enemy{
  public Texture2D enemyImage = Raylib.LoadTexture("cop.png");
  public Rectangle enemyRect = new(700, 500, 60, 148);
}