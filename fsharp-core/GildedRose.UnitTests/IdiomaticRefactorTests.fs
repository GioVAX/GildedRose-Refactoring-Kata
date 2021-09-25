module GildedRose.IdiomaticRefcatorTests

open GildedRose
open System.Collections.Generic
open Xunit
open Swensen.Unquote

[<Fact>]
let ``test foo`` () =
    let items = new List<Item>()  
    items.Add({Name = "foo"; SellIn = 0; Quality = 0})
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {Name = "foo"; SellIn = -1; Quality = 0}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test dexterity vest`` () =
    let items = new List<Item>()  
    items.Add({Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {Name = "+5 Dexterity Vest"; SellIn = 9; Quality = 19}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test dexterity vest multiple times`` () =
    let items = new List<Item>()  
    items.Add({Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20})
    
    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expSellIn = 10 - i

        let expected = 
            if expSellIn < 0 then
                {Name = "+5 Dexterity Vest"; SellIn = expSellIn; Quality = System.Math.Max(0,20-i+expSellIn)}
            else
                {Name = "+5 Dexterity Vest"; SellIn = expSellIn; Quality = 20-i}

        test <@ expected = items.[0] @>


[<Fact>]
let ``test aged brie`` () =
    let items = new List<Item>()  
    items.Add({Name = "Aged Brie"; SellIn = 2; Quality = 0})
    
    let app = GildedRose(items)
    app.UpdateQuality()
    
    let expected = {Name = "Aged Brie"; SellIn = 1; Quality = 1}

    test <@ expected = items.[0] @>

[<Fact>]
let ``test aged brie multiple times`` () =
    let items = new List<Item>()  
    items.Add({Name = "Aged Brie"; SellIn = 2; Quality = 0})
    
    let app = GildedRose(items)
    for i = 1 to 30 do
        app.UpdateQuality()
        let expSellIn = 2 - i
        let expected = 
            if expSellIn < 0 then
                {Name = "Aged Brie"; SellIn = expSellIn; Quality = System.Math.Min(i-expSellIn, 50)}
            else
                {Name = "Aged Brie"; SellIn = expSellIn; Quality = i}
        
        test <@ expected = items.[0] @>

