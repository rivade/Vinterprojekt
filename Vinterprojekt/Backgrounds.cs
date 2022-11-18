using Raylib_cs;

class Background {
  public List<Texture2D> backgrounds = new List<Texture2D>();
  public Background() //Döpt till samma som klassen, så koden körs varje gång en instans skapas
  {
    backgrounds.Add(Raylib.LoadTexture("titlescreen.png"));
    backgrounds.Add(Raylib.LoadTexture("charselectbg.png"));
  }
}