using System;
using System.Collections.Generic;

public class Region : IComparable<Region> {
  private HashSet<Coordinate> tiles = new HashSet<Coordinate>();
  private HashSet<Coordinate> edgeTiles = new HashSet<Coordinate>();
  private int size = 0;

  public Region() {
  }

  public Region(int x, int y, int[,] map) {

  }

  public int GetSize()
  {
    return size;
  }

  public int CompareTo(Region otherRegion) {
    return otherRegion.GetSize().CompareTo (size);
  }

  public void Invert()
  {
    
  }
}