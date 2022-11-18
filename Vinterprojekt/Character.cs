using Raylib_cs;

class Character {
  public List<Texture2D> outfits = new List<Texture2D>();
  public Character() //Döpt till samma som klassen, så koden körs varje gång en instans skapas
  {
    outfits.Add(Raylib.LoadTexture("a1.png"));
    outfits.Add(Raylib.LoadTexture("a2.png"));
    outfits.Add(Raylib.LoadTexture("a3.png"));
    outfits.Add(Raylib.LoadTexture("a4.png"));
    outfits.Add(Raylib.LoadTexture("a5.png"));
    outfits.Add(Raylib.LoadTexture("a6.png"));
  }
  public Rectangle player = new Rectangle(0, 0, 66, 108);
}