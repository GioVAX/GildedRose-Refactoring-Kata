module GildedRose.IdiomaticRefcatorTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

[<Fact>]
let ``test foo`` () =
    let item = {Name = "foo"; SellIn = 0; Quality = 0}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {item with SellIn = -1; Quality = 0}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test dexterity vest`` () =
    let item = {Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {item with SellIn = 9; Quality = 19}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test dexterity vest multiple times`` () =
    let item = {Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expSellIn = 10 - i

        let expected = 
            { item with 
                SellIn = expSellIn
                Quality = 
                    if expSellIn < 0 then
                        System.Math.Max(0,20-i+expSellIn)
                    else
                        20-i
            }

        test <@ expected = items.[0] @>


[<Fact>]
let ``test aged brie`` () =
    let item = {Name = "Aged Brie"; SellIn = 2; Quality = 0}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {item with SellIn = 1; Quality = 1}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test aged brie multiple times`` () =
    let item = {Name = "Aged Brie"; SellIn = 2; Quality = 0}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expSellIn = 2 - i
        let expected = 
            { item with
                SellIn = expSellIn
                Quality = 
                    if expSellIn < 0 then
                        System.Math.Min(i-expSellIn, 50)
                    else
                        i
            }
        
        test <@ expected = items.[0] @>

[<Fact>]
let ``test elixir mongoose`` () =
    let item = {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {item with SellIn = 4; Quality = 6}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test elixir mongoose multiple times`` () =
    let item = {Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7}
    let items = new List<Item>()
    items.Add(item)

    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expSellIn = 5 - i

        let expected = 
            { item with 
                SellIn = expSellIn
                Quality =             
                    if expSellIn < 0 then
                        System.Math.Max(0,7-i+expSellIn)
                    else
                        7-i
            }
        
        test <@ expected = items.[0] @>

[<Fact>]
let ``test sulfuras sellin = 0`` () =
    let item = {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = item

    test <@ expected = items.[0] @>


[<Fact>]
let ``test sulfuras SellIn = 0 multiple times`` () =
    let item = {Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expected = item
        
        test <@ expected = items.[0] @>


[<Fact>]
let ``test sulfuras sellIn = -1`` () =
    let item = {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80}
    let items = new List<Item>()
    items.Add(item)
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = item

    test <@ expected = items.[0] @>

[<Fact>]
let ``test sulfuras sellIn = -1 multiple times`` () =
    let item = {Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80}
    let items = new List<Item>()
    items.Add(item)

    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expSellIn = 5 - i

        let expected = item 
        
        test <@ expected = items.[0] @>
        