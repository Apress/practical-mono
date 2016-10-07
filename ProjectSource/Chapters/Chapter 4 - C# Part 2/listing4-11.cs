using System;

class MyCollection<ItemType>
{
  private const int MAX = 10;
  private ItemType[] items = new ItemType[MAX];
  private int count = 0;

  public void Add(ItemType item)
  {
    if (count < MAX)
      items[count++] = item;
    else
      throw new Exception("Error: Collection is Full");
  }

  public ItemType GetItem(int index)
  {
    if (index < count && index >= 0)
      return items[index];
    else
      throw new Exception("Error: Invalid index");
  }
}



class Test
{
  static void Main(string[] args)
  {
    MyIntCollection<int> ic = new MyIntCollection<int>();
    c.Add(4);
    c.Add(102);


    MyStringCollection<int> sc = new MyStringCollection<int>();
    sc.Add("Mark");
    sc.Add("Mamone");

  } 
}
