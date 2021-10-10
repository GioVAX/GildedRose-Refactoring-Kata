﻿namespace GildedRose

type Item = { Name: string; SellIn: int; Quality: int }

type GildedRose(items:Item list) =
  let mutable _items = items

  let transform item =
    // if retItem.Name <> "Aged Brie" && retItem.Name <> "Backstage passes to a TAFKAL80ETC concert" then
    //   if retItem.Quality > 0 then
    //     if retItem.Name <> "Sulfuras, Hand of Ragnaros" then
    //       retItem <- { retItem with Quality = (retItem.Quality - 1) }
    // else
    //    if retItem.Quality < 50 then
    //     retItem <- { retItem with Quality = (retItem.Quality + 1) }
    //     if retItem.Name = "Backstage passes to a TAFKAL80ETC concert" then
    //       if retItem.SellIn < 11 then
    //         if retItem.Quality < 50 then
    //           retItem <- { retItem with Quality = (retItem.Quality + 1) }
    //       if retItem.SellIn < 6 then
    //         if retItem.Quality < 50 then
    //           retItem <- { retItem with Quality = (retItem.Quality + 1) }
    let q1 = 
      item.Quality +
      if item.Name <> "Aged Brie" && item.Name <> "Backstage passes to a TAFKAL80ETC concert" then
        if item.Quality > 0 then
          if item.Name <> "Sulfuras, Hand of Ragnaros" then -1 else 0
        else 0
      else
        if item.Quality < 50 then
          1 + 
          if item.Name = "Backstage passes to a TAFKAL80ETC concert" then
            (if item.SellIn < 11 && item.Quality < 49 then 1 else 0)
            +
            (if item.SellIn < 6 && item.Quality < 49 then 1 else 0)
          else 0
        else 0

    // if retItem.Name <> "Sulfuras, Hand of Ragnaros" then
    //   retItem <- { retItem with SellIn  = (retItem.SellIn - 1) }
    let s = 
      item.SellIn +
      if item.Name <> "Sulfuras, Hand of Ragnaros" then -1 else 0

    // if retItem.SellIn < 0 then
    //   if retItem.Name <> "Aged Brie" then
    //     if retItem.Name <> "Backstage passes to a TAFKAL80ETC concert" then
    //       if retItem.Quality > 0 then
    //         if retItem.Name <> "Sulfuras, Hand of Ragnaros" then
    //           retItem <- { retItem with Quality   = (retItem.Quality  - 1) }
    //     else
    //       retItem <- { retItem with Quality   = (retItem.Quality  - retItem.Quality) }
    //   else
    //     if retItem.Quality < 50 then
    //       retItem <- { retItem with Quality   = (retItem.Quality + 1) }
    let q2 =
      q1 +
      if s < 0 then
        if item.Name <> "Aged Brie" then
          if item.Name <> "Backstage passes to a TAFKAL80ETC concert" then
            if q1 > 0 then
              if item.Name <> "Sulfuras, Hand of Ragnaros" then -1 else 0
            else 0
          else
            -q1
        else
          if q1 < 50 then 1 else 0
      else 0
    
    {item with Quality = q2; SellIn = s}

  member this.Items = _items
  member private this.Items with set value = _items <- value

  member this.UpdateQuality() =
    this.Items <- this.Items |> List.map transform
    this.Items

module Program =

  let toString item =
    sprintf "%s, %d, %d" item.Name item.SellIn item.Quality
  
  let generateDayReport day items =
    List.concat 
      [
        [sprintf "-------- day %d --------" day; "name, sellIn, quality"];
        items |> List.map toString;
        [""]
      ]
              
  [<EntryPoint>]
  let main argv =
    let items = 
      [{Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20};
      {Name = "Aged Brie"; SellIn = 2; Quality = 0};
      {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7};
      {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80};
      {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80};
      {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20};
      {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49};
      {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49}; 
      {Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6}]

    let app = GildedRose(items)

    let header = ["OMGHAI!"]
    let initialState = generateDayReport 0 app.Items
    let days = 
      ([1..30]
        |> List.collect
          (fun i ->
            app.UpdateQuality()
            |> generateDayReport i
          )
      )

    List.concat
      [header; initialState; days]
    |> List.iter (printfn "%s")
    0
