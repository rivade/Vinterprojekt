using Raylib_cs;

class Character {
  public List<Texture2D> outfits = new();
  public Character() //Döpt till samma som klassen, så koden körs varje gång en instans skapas (Fick koden av Micke)
  {
    outfits.Add(Raylib.LoadTexture("Characters/a1.png"));
    outfits.Add(Raylib.LoadTexture("Characters/a2.png"));
    outfits.Add(Raylib.LoadTexture("Characters/a3.png"));
    outfits.Add(Raylib.LoadTexture("Characters/a4.png"));
    outfits.Add(Raylib.LoadTexture("Characters/a5.png"));
    outfits.Add(Raylib.LoadTexture("Characters/a6.png"));
  } 
}

class Background {
  public List<Texture2D> backgrounds = new();
  public Background()
  {
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/titlescreen.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/mall.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/charselectbg.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/supermarket.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/clothingstore.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/storefloor.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/market.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/jail.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/marketbg.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/vendor.png"));
    backgrounds.Add(Raylib.LoadTexture("Backgrounds/taxi.png"));
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
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc2.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc3.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc4.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc5.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc6.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc7.png"));
  npctexture.Add(Raylib.LoadTexture("NPCTextures/npc8.png"));
  }
}

class Enemy{
  public Texture2D enemyImage = Raylib.LoadTexture("Characters/cop.png");
  public Rectangle enemyRect = new(700, 500, 60, 148);
}

class Item{
  public List<Texture2D> items = new();
  public Item(){
    items.Add(Raylib.LoadTexture("Items/lock.png"));
    items.Add(Raylib.LoadTexture("Items/item.png"));
    items.Add(Raylib.LoadTexture("Items/item2.png"));
    items.Add(Raylib.LoadTexture("Items/item3.png"));
    items.Add(Raylib.LoadTexture("Items/item4.png"));
    items.Add(Raylib.LoadTexture("Items/item5.png"));
    items.Add(Raylib.LoadTexture("Items/item6.png"));
  }
}