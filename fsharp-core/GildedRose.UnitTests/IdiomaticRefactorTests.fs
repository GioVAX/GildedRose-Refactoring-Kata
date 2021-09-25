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
    for i = 1 to 10 do
        app.UpdateQuality()
        let expected = {Name = "+5 Dexterity Vest"; SellIn = 10-i; Quality = 20-i}
        test <@ expected = items.[0] @>
