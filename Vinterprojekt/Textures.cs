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
  public NPC(){
    //npctexture.Add
  }
}

class Enemy{
  public Texture2D enemyImage = Raylib.LoadTexture("cop.png");
  public Rectangle enemyRect = new(700, 500, 60, 148);
}