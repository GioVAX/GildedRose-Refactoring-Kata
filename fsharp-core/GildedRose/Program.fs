namespace GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type GildedRose(items:Item list) =
  member val Items = items with get, set
  //let mutable items = itms

  member this.transform item =
    let mutable retItem = item
    if retItem.Name <> "Aged Brie" && retItem.Name <> "Backstage passes to a TAFKAL80ETC concert" then
      if retItem.Quality > 0 then
        if retItem.Name <> "Sulfuras, Hand of Ragnaros" then
          retItem <- { retItem with Quality = (retItem.Quality - 1) }
    else
       if retItem.Quality < 50 then
        retItem <- { retItem with Quality = (retItem.Quality + 1) }
        if retItem.Name = "Backstage passes to a TAFKAL80ETC concert" then
          if retItem.SellIn < 11 then
            if retItem.Quality < 50 then
              retItem <- { retItem with Quality = (retItem.Quality + 1) }
          if retItem.SellIn < 6 then
            if retItem.Quality < 50 then
              retItem <- { retItem with Quality = (retItem.Quality + 1) }
    if retItem.Name <> "Sulfuras, Hand of Ragnaros" then
      retItem <- { retItem with SellIn  = (retItem.SellIn - 1) }
    if retItem.SellIn < 0 then
      if retItem.Name <> "Aged Brie" then
        if retItem.Name <> "Backstage passes to a TAFKAL80ETC concert" then
          if retItem.Quality > 0 then
            if retItem.Name <> "Sulfuras, Hand of Ragnaros" then
              retItem <- { retItem with Quality   = (retItem.Quality  - 1) }
        else
          retItem <- { retItem with Quality   = (retItem.Quality  - retItem.Quality) }
      else
        if retItem.Quality < 50 then
          retItem <- { retItem with Quality   = (retItem.Quality + 1) }
    retItem

  member this.UpdateQuality() =
    Items <- Items |> List.map this.transform
    ()


module Program =
  [<EntryPoint>]
  let main argv =
    printfn "OMGHAI!"
    let items = 
      [{Name = "Aged Brie"; SellIn = 2; Quality = 0};
      {Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20};
      {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7};
      {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80};
      {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80};
      {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20};
      {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49};
      {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49}; 
      {Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6}]

    let app = GildedRose(items)
    for i = 0 to 30 do
      printfn "-------- day %d --------" i
      printfn "name, sellIn, quality"
      for j = 0 to items.Length - 1 do
        printfn "%s, %d, %d" items.[j].Name items.[j].SellIn items.[j].Quality
      printfn ""
      app.UpdateQuality()
    0
