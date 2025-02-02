module GildedRose.IdiomaticRefcatorTests

open GildedRose
open Xunit
open Swensen.Unquote


let iterate startingItem transform =
    let items = [startingItem]

    let app = GildedRose(items)

    let x = [1..30] 
            |> List.fold
                (fun item _ -> 
                    let actual = app.UpdateQuality().[0]

                    let expected = transform item

                    test <@ expected = actual @>
                    expected
                )
                startingItem
    ()

[<Fact>]
let ``test foo`` () =
    let item = {Name = "foo"; SellIn = 0; Quality = 0}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = -1; Quality = 0}

    test <@ expected = actual @>

[<Fact>]
let ``test dexterity vest`` () =
    let item = {Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 9; Quality = 19}

    test <@ expected = actual @>

[<Fact>]
let ``test dexterity vest multiple times`` () =
    let startItem = {Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20}
    let transformer item = 
            { item with
                SellIn = item.SellIn - 1
                Quality = System.Math.Max(0, item.Quality - (if item.SellIn < 1 then 2 else 1))
            }

    iterate startItem transformer

[<Fact>]
let ``test aged brie`` () =
    let item = {Name = "Aged Brie"; SellIn = 2; Quality = 0}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 1; Quality = 1}

    test <@ expected = actual @>

[<Fact>]
let ``test aged brie multiple times`` () =
    let startItem = {Name = "Aged Brie"; SellIn = 2; Quality = 0}
    let transformer item = 
            { item with
                SellIn = item.SellIn - 1
                Quality = System.Math.Min(50, item.Quality + (if item.SellIn < 1 then 2 else 1))
            }

    iterate startItem transformer

[<Fact>]
let ``test elixir mongoose`` () =
    let item = {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 4; Quality = 6}

    test <@ expected = actual @>

[<Fact>]
let ``test elixir mongoose multiple times`` () =
    let startItem = {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7}
    let transformer item = 
            { item with
                SellIn = item.SellIn - 1
                Quality = System.Math.Max(0, item.Quality - (if item.SellIn < 1 then 2 else 1))
            }

    iterate startItem transformer

[<Fact>]
let ``test sulfuras sellin = 0`` () =
    let item = {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = item

    test <@ expected = actual @>


[<Fact>]
let ``test sulfuras SellIn = 0 multiple times`` () =
    iterate 
        {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80}
        id

[<Fact>]
let ``test sulfuras sellIn = -1`` () =
    let item = {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = item

    test <@ expected = actual @>

[<Fact>]
let ``test sulfuras sellIn = -1 multiple times`` () =
    iterate 
        {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80}
        id

[<Fact>]
let ``test TAFKAL80ETC 1`` () =
    let item = {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 14; Quality = 21}

    test <@ expected = actual @>

[<Fact>]
let ``test TAFKAL80ETC 1 multiple times`` () =
    let startItem = {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20}
    let transformer item = 
        let q =
            if item.SellIn < 1 then
                0
            else if item.Quality < 50 then
                item.Quality + 1
                + (if item.SellIn < 11 then 1 else 0)
                + (if item.SellIn < 6 then 1 else 0)
            else
                item.Quality

        { item with
            SellIn = item.SellIn - 1
            Quality = System.Math.Min(50,q)
        }

    iterate startItem transformer

[<Fact>]
let ``test TAFKAL80ETC 2`` () =
    let item = {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 9; Quality = 50}

    test <@ expected = actual @>

[<Fact>]
let ``test TAFKAL80ETC 2 multiple times`` () =
    let startItem = {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49}
    let transformer item = 
        let q =
            if item.SellIn < 1 then
                0
            else if item.Quality < 50 then
                item.Quality + 1
                + (if item.SellIn < 11 then 1 else 0)
                + (if item.SellIn < 6 then 1 else 0)
            else
                item.Quality

        { item with
            SellIn = item.SellIn - 1
            Quality = System.Math.Min(50,q)
        }

    iterate startItem transformer
    
[<Fact>]
let ``test TAFKAL80ETC 3`` () =
    let item = {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 4; Quality = 50}

    test <@ expected = actual @>

[<Fact>]
let ``test TAFKAL80ETC 3 multiple times`` () =
    let startItem = {Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49}
    let transformer item = 
        let q =
            if item.SellIn < 1 then
                0
            else if item.Quality < 50 then
                item.Quality + 1
                + (if item.SellIn < 11 then 1 else 0)
                + (if item.SellIn < 6 then 1 else 0)
            else
                item.Quality

        { item with
            SellIn = item.SellIn - 1
            Quality = System.Math.Min(50,q)
        }

    iterate startItem transformer
    
[<Fact>]
let ``test Mana Cake`` () =
    let item = {Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6}
    let items = [item]
    
    let app = GildedRose(items)
    let actual = app.UpdateQuality().[0]
    
    let expected = {item with SellIn = 2; Quality = 5}

    test <@ expected = actual @>

[<Fact>]
let ``test Mana Cake multiple times`` () =
    let startItem = {Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6}
    let transformer item = 
        { item with
            SellIn = item.SellIn - 1
            Quality = System.Math.Max(0, item.Quality - (if item.SellIn < 1 then 2 else 1))
        }

    iterate startItem transformer
    