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
  public Background() //Döpt till samma som klassen, så koden körs varje gång en instans skapas
  {
    backgrounds.Add(Raylib.LoadTexture("titlescreen.png"));
    backgrounds.Add(Raylib.LoadTexture("charselectbg.png"));
  }
}

class Door{
  public List<Rectangle> doors = new();
  // foreach (door d in doors)
  // {
    
  // }
}