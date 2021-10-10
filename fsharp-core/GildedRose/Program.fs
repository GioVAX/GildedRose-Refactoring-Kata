namespace GildedRose

type Item = { Name: string; SellIn: int; Quality: int }

type GildedRose(items:Item list) =
  let mutable _items = items

  let agedBrieTransformer item =
    let q = item.Quality + (if item.Quality < 50 then 1 else 0)
    let s = item.SellIn - 1
    let q' = 
      q + 
      if s < 0 && q < 50 then 1 else 0
    {item with Quality = q'; SellIn = s}

  let tafkaTransformer = id
  let sulfurasTransformer = id
  let dexVestTransformer = id
  let mongooseTransformer = id
  let manaCakeTransformer = id

  let transformersMap = 
    Map.empty.
      Add("+5 Dexterity Vest", dexVestTransformer).
      Add("Aged Brie", agedBrieTransformer).
      Add("Elixir of the Mongoose", mongooseTransformer).
      Add("Sulfuras, Hand of Ragnaros", sulfurasTransformer).
      Add("Backstage passes to a TAFKAL80ETC concert", tafkaTransformer).
      Add("Conjured Mana Cake", manaCakeTransformer)

  let getTransformer s =
    match transformersMap.TryFind s with
    | Some f -> f
    | None -> id

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

  member this.transform' (item:Item) =
    let f = getTransformer item.Name
    f item

  member this.UpdateQuality() =
    _items <- _items |> List.map transform
    _items

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
    let initialState = generateDayReport 0 items
    let dailyReports = 
      ([1..30]
        |> List.collect
          (fun i ->
            app.UpdateQuality()
            |> generateDayReport i
          )
      )

    List.concat
      [header; initialState; dailyReports]
    |> List.iter (printfn "%s")
    0
