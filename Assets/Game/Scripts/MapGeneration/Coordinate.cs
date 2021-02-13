public class Coordinate {
  public int tileX;
  public int tileY;

  public Coordinate(int x, int y) {
    tileX = x;
    tileY = y;
  }
  
  public override int GetHashCode()             
  {  
    return (tileX + ":" + tileY).GetHashCode(); 
  }
  
  public override bool Equals(object obj) 
  { 
    return Equals(obj as Coordinate); 
  }
  
  public bool Equals(Coordinate obj)
  { 
    return obj != null && obj.tileX == this.tileX && obj.tileY == this.tileY; 
  }
}